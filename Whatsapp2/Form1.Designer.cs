namespace Whatsapp2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainerMain = new SplitContainer();
            lstChats = new ListBox();
            panelLeftHeader = new Panel();
            btnLogs = new Button();
            btnNuevoChat = new Button();
            btnConfiguracion = new Button();
            lblChats = new Label();
            rtbChat = new RichTextBox();
            panelMessageInput = new Panel();
            btnEnviar = new Button();
            txtMensaje = new TextBox();
            panelRightHeader = new Panel();
            lblEstadoConexion = new Label();
            lblChatTitle = new Label();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            panelLeftHeader.SuspendLayout();
            panelMessageInput.SuspendLayout();
            panelRightHeader.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainerMain
            // 
            splitContainerMain.Dock = DockStyle.Fill;
            splitContainerMain.FixedPanel = FixedPanel.Panel1;
            splitContainerMain.Location = new Point(0, 0);
            splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            splitContainerMain.Panel1.BackColor = Color.WhiteSmoke;
            splitContainerMain.Panel1.Controls.Add(lstChats);
            splitContainerMain.Panel1.Controls.Add(panelLeftHeader);
            // 
            // splitContainerMain.Panel2
            // 
            splitContainerMain.Panel2.BackColor = Color.White;
            splitContainerMain.Panel2.Controls.Add(rtbChat);
            splitContainerMain.Panel2.Controls.Add(panelMessageInput);
            splitContainerMain.Panel2.Controls.Add(panelRightHeader);
            splitContainerMain.Size = new Size(980, 620);
            splitContainerMain.SplitterDistance = 280;
            splitContainerMain.TabIndex = 0;
            // 
            // lstChats
            // 
            lstChats.BorderStyle = BorderStyle.None;
            lstChats.Dock = DockStyle.Fill;
            lstChats.DrawMode = DrawMode.OwnerDrawFixed;
            lstChats.Font = new Font("Segoe UI", 10F);
            lstChats.FormattingEnabled = true;
            lstChats.ItemHeight = 24;
            lstChats.Location = new Point(0, 50);
            lstChats.Name = "lstChats";
            lstChats.Size = new Size(280, 570);
            lstChats.TabIndex = 1;
            lstChats.DrawItem += lstChats_DrawItem;
            lstChats.MouseDown += lstChats_MouseDown;
            lstChats.SelectedIndexChanged += lstChats_SelectedIndexChanged;
            // 
            // panelLeftHeader
            // 
            panelLeftHeader.BackColor = Color.Gainsboro;
            panelLeftHeader.Controls.Add(btnLogs);
            panelLeftHeader.Controls.Add(btnNuevoChat);
            panelLeftHeader.Controls.Add(btnConfiguracion);
            panelLeftHeader.Controls.Add(lblChats);
            panelLeftHeader.Dock = DockStyle.Top;
            panelLeftHeader.Location = new Point(0, 0);
            panelLeftHeader.Name = "panelLeftHeader";
            panelLeftHeader.Padding = new Padding(12, 0, 0, 0);
            panelLeftHeader.Size = new Size(280, 50);
            panelLeftHeader.TabIndex = 0;
            // 
            // btnLogs
            // 
            btnLogs.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLogs.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLogs.Location = new Point(140, 9);
            btnLogs.Name = "btnLogs";
            btnLogs.Size = new Size(40, 32);
            btnLogs.TabIndex = 3;
            btnLogs.Text = "L";
            btnLogs.UseVisualStyleBackColor = true;
            btnLogs.Click += btnLogs_Click;
            // 
            // btnNuevoChat
            // 
            btnNuevoChat.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNuevoChat.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnNuevoChat.Location = new Point(186, 9);
            btnNuevoChat.Name = "btnNuevoChat";
            btnNuevoChat.Size = new Size(40, 32);
            btnNuevoChat.TabIndex = 2;
            btnNuevoChat.Text = "+";
            btnNuevoChat.UseVisualStyleBackColor = true;
            btnNuevoChat.Click += btnNuevoChat_Click;
            // 
            // btnConfiguracion
            // 
            btnConfiguracion.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnConfiguracion.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnConfiguracion.Location = new Point(232, 9);
            btnConfiguracion.Name = "btnConfiguracion";
            btnConfiguracion.Size = new Size(40, 32);
            btnConfiguracion.TabIndex = 1;
            btnConfiguracion.Text = "⚙";
            btnConfiguracion.UseVisualStyleBackColor = true;
            btnConfiguracion.Click += btnConfiguracion_Click;
            // 
            // lblChats
            // 
            lblChats.AutoSize = true;
            lblChats.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblChats.Location = new Point(12, 14);
            lblChats.Name = "lblChats";
            lblChats.Size = new Size(60, 25);
            lblChats.TabIndex = 0;
            lblChats.Text = "Chats";
            // 
            // rtbChat
            // 
            rtbChat.BackColor = Color.White;
            rtbChat.BorderStyle = BorderStyle.None;
            rtbChat.Dock = DockStyle.Fill;
            rtbChat.Font = new Font("Segoe UI", 10F);
            rtbChat.Location = new Point(0, 50);
            rtbChat.Name = "rtbChat";
            rtbChat.ReadOnly = true;
            rtbChat.Size = new Size(696, 510);
            rtbChat.TabIndex = 1;
            rtbChat.Text = "";
            rtbChat.TextChanged += rtbChat_TextChanged;
            // 
            // panelMessageInput
            // 
            panelMessageInput.BackColor = Color.WhiteSmoke;
            panelMessageInput.Controls.Add(btnEnviar);
            panelMessageInput.Controls.Add(txtMensaje);
            panelMessageInput.Dock = DockStyle.Bottom;
            panelMessageInput.Location = new Point(0, 560);
            panelMessageInput.Name = "panelMessageInput";
            panelMessageInput.Padding = new Padding(10);
            panelMessageInput.Size = new Size(696, 60);
            panelMessageInput.TabIndex = 2;
            // 
            // btnEnviar
            // 
            btnEnviar.Dock = DockStyle.Right;
            btnEnviar.Location = new Point(596, 10);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(90, 40);
            btnEnviar.TabIndex = 1;
            btnEnviar.Text = "Enviar";
            btnEnviar.UseVisualStyleBackColor = true;
            btnEnviar.Click += btnEnviar_Click;
            // 
            // txtMensaje
            // 
            txtMensaje.Dock = DockStyle.Fill;
            txtMensaje.Font = new Font("Segoe UI", 10F);
            txtMensaje.Location = new Point(10, 10);
            txtMensaje.Multiline = true;
            txtMensaje.Name = "txtMensaje";
            txtMensaje.PlaceholderText = "Escribe un mensaje";
            txtMensaje.Size = new Size(676, 40);
            txtMensaje.TabIndex = 0;
            txtMensaje.KeyDown += txtMensaje_KeyDown;
            // 
            // panelRightHeader
            // 
            panelRightHeader.BackColor = Color.Gainsboro;
            panelRightHeader.Controls.Add(lblEstadoConexion);
            panelRightHeader.Controls.Add(lblChatTitle);
            panelRightHeader.Dock = DockStyle.Top;
            panelRightHeader.Location = new Point(0, 0);
            panelRightHeader.Name = "panelRightHeader";
            panelRightHeader.Padding = new Padding(12, 0, 0, 0);
            panelRightHeader.Size = new Size(696, 50);
            panelRightHeader.TabIndex = 0;
            // 
            // lblEstadoConexion
            // 
            lblEstadoConexion.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblEstadoConexion.AutoSize = true;
            lblEstadoConexion.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEstadoConexion.ForeColor = Color.Firebrick;
            lblEstadoConexion.Location = new Point(613, 17);
            lblEstadoConexion.Name = "lblEstadoConexion";
            lblEstadoConexion.Size = new Size(71, 20);
            lblEstadoConexion.TabIndex = 1;
            lblEstadoConexion.Text = "Offline";
            // 
            // lblChatTitle
            // 
            lblChatTitle.AutoSize = true;
            lblChatTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblChatTitle.Location = new Point(12, 14);
            lblChatTitle.Name = "lblChatTitle";
            lblChatTitle.Size = new Size(176, 25);
            lblChatTitle.TabIndex = 0;
            lblChatTitle.Text = "Selecciona un chat";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(980, 620);
            Controls.Add(splitContainerMain);
            MinimumSize = new Size(820, 500);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WhatsApp";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            panelLeftHeader.ResumeLayout(false);
            panelLeftHeader.PerformLayout();
            panelMessageInput.ResumeLayout(false);
            panelMessageInput.PerformLayout();
            panelRightHeader.ResumeLayout(false);
            panelRightHeader.PerformLayout();
            ResumeLayout(false);
        }

        private SplitContainer splitContainerMain;
        private Panel panelLeftHeader;
        private Button btnLogs;
        private Button btnNuevoChat;
        private Button btnConfiguracion;
        private Label lblChats;
        private ListBox lstChats;
        private Panel panelRightHeader;
        private Label lblEstadoConexion;
        private Label lblChatTitle;
        private Panel panelMessageInput;
        private Button btnEnviar;
        private TextBox txtMensaje;
        private RichTextBox rtbChat;

        #endregion
    }
}
