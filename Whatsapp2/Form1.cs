using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Whatsapp2
{
    public partial class Form1 : Form
    {
        private sealed class ChatMessage
        {
            public required string Texto { get; init; }
            public required bool EsPropio { get; init; }
            public DateTime Fecha { get; init; } = DateTime.Now;
        }

        private sealed class ChatSession
        {
            public required string Ip { get; init; }
            public required int Puerto { get; init; }
            public List<ChatMessage> Mensajes { get; } = new();
            public bool Online { get; set; }
            public string Nombre => $"{Ip}:{Puerto}";
            public override string ToString() => Online ? $"🟢 {Nombre}" : $"🔴 {Nombre}";
        }

        private readonly List<ChatSession> _chats = new();
        private readonly object _chatsLock = new();

        private int _localPort = 5000;
        private TcpListener? _listener;
        private CancellationTokenSource? _cts;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartNetworking();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopNetworking();
        }

        private void lstChats_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshSelectedChatView();
        }

        private void rtbChat_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnConfiguracion_Click(object sender, EventArgs e)
        {
            using var configForm = new ConfiguracionForm(_localPort);
            if (configForm.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            _localPort = configForm.PuertoLocal;
            StartNetworking();
        }

        private void btnNuevoChat_Click(object sender, EventArgs e)
        {
            using var nuevoChat = new NuevoChatForm();
            if (nuevoChat.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            AddChat(nuevoChat.IpRemota, nuevoChat.PuertoRemoto, selectAfterAdd: true);
        }

        private async void btnEnviar_Click(object sender, EventArgs e)
        {
            var chat = GetSelectedChat();
            if (chat is null)
            {
                MessageBox.Show(this, "Selecciona un chat.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var texto = txtMensaje.Text.Trim();
            if (string.IsNullOrWhiteSpace(texto))
            {
                return;
            }

            AppendMessage(chat, texto, esPropio: true);
            txtMensaje.Clear();

            var payload = $"FROM:{_localPort};MSG:{texto}";
            var enviado = await SendPayloadAsync(chat, payload);
            if (!enviado)
            {
                AppendMessage(chat, "No entregado (peer desconectado)", esPropio: false);
            }
        }

        private void StartNetworking()
        {
            StopNetworking();

            _cts = new CancellationTokenSource();

            try
            {
                _listener = new TcpListener(IPAddress.Any, _localPort);
                _listener.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                _listener.Start();
            }
            catch (SocketException ex)
            {
                _listener = null;
                _cts.Cancel();
                MessageBox.Show(
                    this,
                    $"No se pudo abrir el puerto local {_localPort}.\n\n{ex.Message}\n\nPrueba con otro puerto en Configuración.",
                    "Error de socket",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                lblChatTitle.Text = "Error al iniciar listener";
                lblEstadoConexion.Text = "Offline";
                lblEstadoConexion.ForeColor = Color.Firebrick;
                return;
            }

            _ = Task.Run(() => ListenLoopAsync(_cts.Token));
            _ = Task.Run(() => MonitorChatsAsync(_cts.Token));

            lblChatTitle.Text = $"Chats (Local {_localPort})";
        }

        private void StopNetworking()
        {
            try
            {
                _cts?.Cancel();
                _listener?.Stop();
            }
            catch
            {
            }
        }

        private async Task ListenLoopAsync(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var client = await _listener!.AcceptTcpClientAsync(cancellationToken);
                    _ = Task.Run(() => HandleIncomingAsync(client, cancellationToken), cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex)
            {
                BeginInvoke(() => MessageBox.Show(this, ex.Message, "Error listener", MessageBoxButtons.OK, MessageBoxIcon.Error));
            }
        }

        private async Task HandleIncomingAsync(TcpClient client, CancellationToken cancellationToken)
        {
            using (client)
            using (var stream = client.GetStream())
            {
                var buffer = new byte[4096];
                var bytesRead = await stream.ReadAsync(buffer, cancellationToken);
                if (bytesRead <= 0)
                {
                    return;
                }

                var payload = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                var remoteIp = ((IPEndPoint)client.Client.RemoteEndPoint!).Address.ToString();
                ParsePayload(payload, out var fromPort, out var tipo, out var cuerpo);

                if (fromPort <= 0)
                {
                    return;
                }

                var chat = AddChat(remoteIp, fromPort, selectAfterAdd: false);
                SetChatStatus(chat, true);

                if (tipo == "PING")
                {
                    return;
                }

                if (tipo == "MSG")
                {
                    AppendMessage(chat, cuerpo, esPropio: false);
                }
            }
        }

        private async Task MonitorChatsAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                ChatSession[] chats;
                lock (_chatsLock)
                {
                    chats = _chats.ToArray();
                }

                foreach (var chat in chats)
                {
                    var online = await SendPayloadAsync(chat, $"FROM:{_localPort};PING", silent: true);
                    SetChatStatus(chat, online);
                }

                try
                {
                    await Task.Delay(3000, cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }

        private async Task<bool> SendPayloadAsync(ChatSession chat, string payload, bool silent = false)
        {
            try
            {
                using var client = new TcpClient();
                await client.ConnectAsync(IPAddress.Parse(chat.Ip), chat.Puerto);
                using var stream = client.GetStream();
                var bytes = Encoding.UTF8.GetBytes(payload);
                await stream.WriteAsync(bytes);
                return true;
            }
            catch
            {
                if (!silent)
                {
                    SetChatStatus(chat, false);
                }

                return false;
            }
        }

        private ChatSession AddChat(string ip, int puerto, bool selectAfterAdd)
        {
            ChatSession? chat;
            lock (_chatsLock)
            {
                chat = _chats.FirstOrDefault(c => c.Ip == ip && c.Puerto == puerto);
                if (chat is null)
                {
                    chat = new ChatSession { Ip = ip, Puerto = puerto };
                    _chats.Add(chat);
                }
            }

            if (InvokeRequired)
            {
                BeginInvoke(() => AddChatToList(chat, selectAfterAdd));
            }
            else
            {
                AddChatToList(chat, selectAfterAdd);
            }

            return chat;
        }

        private void AddChatToList(ChatSession chat, bool selectAfterAdd)
        {
            if (!lstChats.Items.Contains(chat))
            {
                lstChats.Items.Add(chat);
            }

            if (selectAfterAdd)
            {
                lstChats.SelectedItem = chat;
            }
        }

        private void SetChatStatus(ChatSession chat, bool online)
        {
            if (chat.Online == online)
            {
                return;
            }

            chat.Online = online;

            if (InvokeRequired)
            {
                BeginInvoke(() => SetChatStatus(chat, online));
                return;
            }

            var index = lstChats.Items.IndexOf(chat);
            if (index >= 0)
            {
                var selected = lstChats.SelectedItem;
                lstChats.BeginUpdate();
                lstChats.Items.RemoveAt(index);
                lstChats.Items.Insert(index, chat);
                if (selected is not null)
                {
                    lstChats.SelectedItem = selected;
                }
                lstChats.EndUpdate();
            }

            if (ReferenceEquals(GetSelectedChat(), chat))
            {
                lblEstadoConexion.Text = online ? "Online" : "Offline";
                lblEstadoConexion.ForeColor = online ? Color.SeaGreen : Color.Firebrick;
            }
        }

        private void AppendMessage(ChatSession chat, string texto, bool esPropio)
        {
            if (InvokeRequired)
            {
                BeginInvoke(() => AppendMessage(chat, texto, esPropio));
                return;
            }

            lock (_chatsLock)
            {
                chat.Mensajes.Add(new ChatMessage
                {
                    Texto = texto,
                    EsPropio = esPropio,
                    Fecha = DateTime.Now
                });
            }

            if (ReferenceEquals(GetSelectedChat(), chat))
            {
                RefreshSelectedChatView();
            }
        }

        private void RefreshSelectedChatView()
        {
            var chat = GetSelectedChat();
            if (chat is null)
            {
                rtbChat.Clear();
                lblChatTitle.Text = $"Chats (Local {_localPort})";
                lblEstadoConexion.Text = "-";
                lblEstadoConexion.ForeColor = Color.DimGray;
                return;
            }

            lblChatTitle.Text = chat.Nombre;
            lblEstadoConexion.Text = chat.Online ? "Online" : "Offline";
            lblEstadoConexion.ForeColor = chat.Online ? Color.SeaGreen : Color.Firebrick;

            rtbChat.Clear();
            foreach (var msg in chat.Mensajes)
            {
                rtbChat.SelectionStart = rtbChat.TextLength;
                rtbChat.SelectionLength = 0;
                rtbChat.SelectionAlignment = msg.EsPropio ? HorizontalAlignment.Right : HorizontalAlignment.Left;
                rtbChat.AppendText($"[{msg.Fecha:HH:mm:ss}] {msg.Texto}{Environment.NewLine}");
            }

            rtbChat.SelectionStart = rtbChat.TextLength;
            rtbChat.SelectionLength = 0;
            rtbChat.SelectionAlignment = HorizontalAlignment.Left;
            rtbChat.ScrollToCaret();
        }

        private ChatSession? GetSelectedChat()
        {
            return lstChats.SelectedItem as ChatSession;
        }

        private static void ParsePayload(string payload, out int fromPort, out string tipo, out string cuerpo)
        {
            fromPort = 0;
            tipo = string.Empty;
            cuerpo = string.Empty;

            var fromPrefix = "FROM:";
            var separator = ";";

            if (!payload.StartsWith(fromPrefix, StringComparison.Ordinal))
            {
                return;
            }

            var sepIndex = payload.IndexOf(separator, StringComparison.Ordinal);
            if (sepIndex < 0)
            {
                return;
            }

            var portText = payload[fromPrefix.Length..sepIndex];
            if (!int.TryParse(portText, out fromPort))
            {
                fromPort = 0;
                return;
            }

            var rest = payload[(sepIndex + 1)..];
            if (rest == "PING")
            {
                tipo = "PING";
                return;
            }

            if (rest.StartsWith("MSG:", StringComparison.Ordinal))
            {
                tipo = "MSG";
                cuerpo = rest[4..];
            }
        }
    }
}
