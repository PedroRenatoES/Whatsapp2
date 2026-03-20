using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace Whatsapp2
{
    public partial class Form1 : Form
    {
        // ===== Modelos de chat =====
        private sealed class ChatMessage
        {
            public required string Texto { get; init; }
            public required bool EsPropio { get; init; }
            public DateTime Fecha { get; init; } = DateTime.Now;
        }

        private sealed class ChatSession
        {
            public string Alias { get; set; } = string.Empty;
            public string Ip { get; set; } = string.Empty;
            public int Puerto { get; set; }
            public List<ChatMessage> Mensajes { get; } = new();
            public bool Online { get; set; }
            public string Nombre => !string.IsNullOrWhiteSpace(Alias) ? Alias : $"{Ip}:{Puerto}";
            public override string ToString() => Online ? $"🟢 {Nombre}" : $"🔴 {Nombre}";
        }

        // ===== Estado principal =====
        private readonly List<ChatSession> _chats = new();
        private readonly object _chatsLock = new();

        private int _localPort = 5001;
        private string _claveCompartida = "clave-demo";
        private TcpListener? _listener;
        private CancellationTokenSource? _cts;
        private LogsForm? _logsForm;

        // ===== Accesos rápidos de UI =====
        private ChatSession? SelectedChat => lstChats.SelectedItem as ChatSession;

        // ===== Inicialización y ciclo de vida =====
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

        // ===== Eventos de UI: lista de chats =====
        private void lstChats_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshSelectedChatView();
        }

        private void lstChats_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index < 0 || e.Index >= lstChats.Items.Count)
            {
                return;
            }

            var chat = (ChatSession)lstChats.Items[e.Index]!;
            var textColor = (e.State & DrawItemState.Selected) == DrawItemState.Selected ? SystemColors.HighlightText : SystemColors.ControlText;

            var buttonRect = new Rectangle(e.Bounds.Right - 26, e.Bounds.Top + 3, 20, e.Bounds.Height - 6);

            var textRect = new Rectangle(e.Bounds.Left + 6, e.Bounds.Top + 3, e.Bounds.Width - 38, e.Bounds.Height - 6);
            TextRenderer.DrawText(e.Graphics, chat.ToString(), e.Font!, textRect, textColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

            ControlPaint.DrawButton(e.Graphics, buttonRect, ButtonState.Flat);
            TextRenderer.DrawText(e.Graphics, "✎", e.Font!, buttonRect, textColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            e.DrawFocusRectangle();
        }

        private void lstChats_MouseDown(object sender, MouseEventArgs e)
        {
            var index = lstChats.IndexFromPoint(e.Location);
            if (index < 0 || index >= lstChats.Items.Count)
            {
                return;
            }

            var chat = (ChatSession)lstChats.Items[index]!;
            var itemRect = lstChats.GetItemRectangle(index);
            var buttonRect = new Rectangle(itemRect.Right - 26, itemRect.Top + 3, 20, itemRect.Height - 6);
            if (buttonRect.Contains(e.Location))
            {
                EditChat(chat);
            }
        }

        // ===== Eventos de UI: envío/configuración =====
        private void txtMensaje_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true;
                btnEnviar.PerformClick();
            }
        }

        private void btnConfiguracion_Click(object sender, EventArgs e)
        {
            using var configForm = new ConfiguracionForm(_localPort, _claveCompartida);
            if (configForm.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            _localPort = configForm.PuertoLocal;
            _claveCompartida = configForm.ClaveCompartida;
            StartNetworking();
        }

        private void btnLogs_Click(object sender, EventArgs e)
        {
            if (_logsForm is null || _logsForm.IsDisposed)
            {
                _logsForm = new LogsForm();
            }

            _logsForm.Show();
            _logsForm.BringToFront();
        }

        private void btnNuevoChat_Click(object sender, EventArgs e)
        {
            using var nuevoChat = new NuevoChatForm();
            if (nuevoChat.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            UpsertChat(nuevoChat.NombreChat, nuevoChat.IpRemota, nuevoChat.PuertoRemoto, selectAfterAdd: true);
        }

        private async void btnEnviar_Click(object sender, EventArgs e)
        {
            var chat = SelectedChat;
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

            var encryptedText = EncryptText(texto, _claveCompartida);
            _logsForm?.AddLog($"OUT {chat.Nombre} :: {encryptedText}");
            var payload = $"FROM:{_localPort};MSG:{encryptedText}";
            var enviado = await SendPayloadAsync(chat, payload);
            if (!enviado)
            {
                AppendMessage(chat, "No entregado (peer desconectado)", esPropio: false);
            }
        }

        // ===== Red TCP: listener y recepción =====
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

                var chat = UpsertChat(string.Empty, remoteIp, fromPort, selectAfterAdd: false);
                SetChatStatus(chat, true);

                if (tipo == "PING")
                {
                    return;
                }

                if (tipo == "MSG")
                {
                    _logsForm?.AddLog($"IN  {chat.Nombre} :: {cuerpo}");
                    var textoPlano = TryDecryptText(cuerpo, _claveCompartida, out var desencriptado)
                        ? desencriptado
                        : "[No se pudo desencriptar el mensaje]";

                    AppendMessage(chat, textoPlano, esPropio: false);
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

        // ===== Gestión de chats =====
        private ChatSession UpsertChat(string alias, string ip, int puerto, bool selectAfterAdd)
        {
            var isNew = false;
            ChatSession? chat;
            lock (_chatsLock)
            {
                chat = _chats.FirstOrDefault(c => c.Ip == ip && c.Puerto == puerto);
                if (chat is null)
                {
                    chat = new ChatSession { Alias = alias, Ip = ip, Puerto = puerto };
                    _chats.Add(chat);
                    isNew = true;
                }
                else if (!string.IsNullOrWhiteSpace(alias) && chat.Alias != alias)
                {
                    chat.Alias = alias;
                }
            }

            void SyncUi()
            {
                if (isNew)
                {
                    lstChats.Items.Add(chat);
                }

                if (selectAfterAdd)
                {
                    lstChats.SelectedItem = chat;
                }

                lstChats.Invalidate();
            }

            if (InvokeRequired) BeginInvoke(SyncUi); else SyncUi();
            return chat;
        }

        private void EditChat(ChatSession chat)
        {
            using var editarForm = new NuevoChatForm(chat.Alias, chat.Ip, chat.Puerto, "Editar chat");
            if (editarForm.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            bool existe;

            lock (_chatsLock)
            {
                existe = _chats.Any(c => !ReferenceEquals(c, chat) && c.Ip == editarForm.IpRemota && c.Puerto == editarForm.PuertoRemoto);
            }

            if (existe)
            {
                MessageBox.Show(this, "Ya existe un chat con esa IP y puerto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lock (_chatsLock)
            {
                chat.Alias = editarForm.NombreChat;
                chat.Ip = editarForm.IpRemota;
                chat.Puerto = editarForm.PuertoRemoto;
            }

            var index = lstChats.Items.IndexOf(chat);
            if (index >= 0)
            {
                lstChats.Items[index] = chat;
                lstChats.SelectedItem = chat;
            }

            RefreshSelectedChatView();
            lstChats.Invalidate();
        }

        // ===== Estado, mensajes y render del chat =====
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

            lstChats.Invalidate();

            if (ReferenceEquals(SelectedChat, chat))
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

            if (ReferenceEquals(SelectedChat, chat))
            {
                RefreshSelectedChatView();
            }
        }

        private void RefreshSelectedChatView()
        {
            var chat = SelectedChat;
            if (chat is null)
            {
                lstMensajes.Items.Clear();
                lblChatTitle.Text = $"Chats (Local {_localPort})";
                lblEstadoConexion.Text = "-";
                lblEstadoConexion.ForeColor = Color.DimGray;
                return;
            }

            lblChatTitle.Text = chat.Nombre;
            lblEstadoConexion.Text = chat.Online ? "Online" : "Offline";
            lblEstadoConexion.ForeColor = chat.Online ? Color.SeaGreen : Color.Firebrick;

            lstMensajes.BeginUpdate();
            lstMensajes.Items.Clear();
            foreach (var msg in chat.Mensajes)
            {
                lstMensajes.Items.Add(msg);
            }
            lstMensajes.EndUpdate();

            if (lstMensajes.Items.Count > 0)
            {
                lstMensajes.TopIndex = lstMensajes.Items.Count - 1;
            }
        }

        private void lstMensajes_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= lstMensajes.Items.Count)
                return;

            var msg = (ChatMessage)lstMensajes.Items[e.Index]!;
            var size = TextRenderer.MeasureText(e.Graphics, msg.Texto, lstMensajes.Font, new Size(lstMensajes.Width - 100, int.MaxValue), TextFormatFlags.WordBreak);
            e.ItemHeight = size.Height + 35;
        }

        private void lstMensajes_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= lstMensajes.Items.Count)
                return;

            e.DrawBackground();

            var msg = (ChatMessage)lstMensajes.Items[e.Index]!;
            var isOwn = msg.EsPropio;

            var maxWidth = lstMensajes.Width - 100;
            var textSize = TextRenderer.MeasureText(e.Graphics, msg.Texto, lstMensajes.Font, new Size(maxWidth, int.MaxValue), TextFormatFlags.WordBreak);
            using var smallFont = new Font(lstMensajes.Font.FontFamily, 8);
            var timeSize = TextRenderer.MeasureText(e.Graphics, msg.Fecha.ToString("HH:mm"), smallFont, new Size(maxWidth, int.MaxValue));

            var bubbleWidth = Math.Max(textSize.Width, timeSize.Width) + 20;
            var bubbleHeight = textSize.Height + timeSize.Height + 15;

            var x = isOwn ? lstMensajes.Width - bubbleWidth - 30 : 10;
            var y = e.Bounds.Top + 5;

            var bg = isOwn ? Color.FromArgb(220, 248, 198) : Color.White;
            using var brush = new SolidBrush(bg);

            var rect = new Rectangle(x, y, bubbleWidth, bubbleHeight);
            int radius = 10;
            using var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.FillPath(brush, path);

            var textRect = new Rectangle(x + 10, y + 10, textSize.Width, textSize.Height);
            TextRenderer.DrawText(e.Graphics, msg.Texto, lstMensajes.Font, textRect, Color.Black, TextFormatFlags.WordBreak);

            var timeRect = new Rectangle(x + bubbleWidth - timeSize.Width - 10, y + bubbleHeight - timeSize.Height - 5, timeSize.Width, timeSize.Height);
            TextRenderer.DrawText(e.Graphics, msg.Fecha.ToString("HH:mm"), smallFont, timeRect, Color.Gray);
        }

        // ===== Protocolo de mensajes =====
        private static void ParsePayload(string payload, out int fromPort, out string tipo, out string cuerpo)
        {
            fromPort = 0;
            tipo = string.Empty;
            cuerpo = string.Empty;

            var parts = payload.Split(';', 2);
            if (parts.Length != 2 || !parts[0].StartsWith("FROM:", StringComparison.Ordinal) || !int.TryParse(parts[0][5..], out fromPort)) return;

            if (parts[1] == "PING") { tipo = "PING"; return; }
            if (parts[1].StartsWith("MSG:", StringComparison.Ordinal)) { tipo = "MSG"; cuerpo = parts[1][4..]; }
        }

        // ===== Cifrado AES-256 =====
        private static string EncryptText(string plainText, string sharedKey)
        {
            using var sha = SHA256.Create();
            var key = sha.ComputeHash(Encoding.UTF8.GetBytes(sharedKey));

            using var aes = Aes.Create();
            aes.KeySize = 256;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = key;
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            var result = new byte[aes.IV.Length + cipherBytes.Length];
            Buffer.BlockCopy(aes.IV, 0, result, 0, aes.IV.Length);
            Buffer.BlockCopy(cipherBytes, 0, result, aes.IV.Length, cipherBytes.Length);
            return Convert.ToBase64String(result);
        }

        private static bool TryDecryptText(string encryptedText, string sharedKey, out string plainText)
        {
            try
            {
                var allBytes = Convert.FromBase64String(encryptedText);
                if (allBytes.Length < 17)
                {
                    plainText = string.Empty;
                    return false;
                }

                var iv = new byte[16];
                var cipherBytes = new byte[allBytes.Length - 16];
                Buffer.BlockCopy(allBytes, 0, iv, 0, 16);
                Buffer.BlockCopy(allBytes, 16, cipherBytes, 0, cipherBytes.Length);

                using var sha = SHA256.Create();
                var key = sha.ComputeHash(Encoding.UTF8.GetBytes(sharedKey));

                using var aes = Aes.Create();
                aes.KeySize = 256;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;

                using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                var plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                plainText = Encoding.UTF8.GetString(plainBytes);
                return true;
            }
            catch
            {
                plainText = string.Empty;
                return false;
            }
        }
    }
}
