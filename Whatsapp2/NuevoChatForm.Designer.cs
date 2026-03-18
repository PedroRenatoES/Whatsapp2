namespace Whatsapp2
{
    partial class NuevoChatForm
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
            tableLayoutPanelMain = new TableLayoutPanel();
            lblIpRemota = new Label();
            txtIpRemota = new TextBox();
            lblPuertoRemoto = new Label();
            nudPuertoRemoto = new NumericUpDown();
            panelButtons = new Panel();
            btnCancelar = new Button();
            btnCrear = new Button();
            tableLayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPuertoRemoto).BeginInit();
            panelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.ColumnCount = 2;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanelMain.Controls.Add(lblIpRemota, 0, 0);
            tableLayoutPanelMain.Controls.Add(txtIpRemota, 1, 0);
            tableLayoutPanelMain.Controls.Add(lblPuertoRemoto, 0, 1);
            tableLayoutPanelMain.Controls.Add(nudPuertoRemoto, 1, 1);
            tableLayoutPanelMain.Controls.Add(panelButtons, 0, 2);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(12, 12);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.Padding = new Padding(5);
            tableLayoutPanelMain.RowCount = 3;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Size = new Size(436, 176);
            tableLayoutPanelMain.TabIndex = 0;
            tableLayoutPanelMain.SetColumnSpan(panelButtons, 2);
            // 
            // lblIpRemota
            // 
            lblIpRemota.Anchor = AnchorStyles.Left;
            lblIpRemota.AutoSize = true;
            lblIpRemota.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblIpRemota.Location = new Point(8, 21);
            lblIpRemota.Name = "lblIpRemota";
            lblIpRemota.Size = new Size(93, 23);
            lblIpRemota.TabIndex = 0;
            lblIpRemota.Text = "IP remota";
            // 
            // txtIpRemota
            // 
            txtIpRemota.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtIpRemota.Location = new Point(178, 16);
            txtIpRemota.Name = "txtIpRemota";
            txtIpRemota.PlaceholderText = "Ej: 192.168.1.25";
            txtIpRemota.Size = new Size(250, 27);
            txtIpRemota.TabIndex = 1;
            // 
            // lblPuertoRemoto
            // 
            lblPuertoRemoto.Anchor = AnchorStyles.Left;
            lblPuertoRemoto.AutoSize = true;
            lblPuertoRemoto.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPuertoRemoto.Location = new Point(8, 73);
            lblPuertoRemoto.Name = "lblPuertoRemoto";
            lblPuertoRemoto.Size = new Size(127, 23);
            lblPuertoRemoto.TabIndex = 2;
            lblPuertoRemoto.Text = "Puerto remoto";
            // 
            // nudPuertoRemoto
            // 
            nudPuertoRemoto.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            nudPuertoRemoto.Location = new Point(178, 68);
            nudPuertoRemoto.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            nudPuertoRemoto.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudPuertoRemoto.Name = "nudPuertoRemoto";
            nudPuertoRemoto.Size = new Size(250, 27);
            nudPuertoRemoto.TabIndex = 3;
            nudPuertoRemoto.Value = new decimal(new int[] { 5000, 0, 0, 0 });
            // 
            // panelButtons
            // 
            panelButtons.Controls.Add(btnCancelar);
            panelButtons.Controls.Add(btnCrear);
            panelButtons.Dock = DockStyle.Fill;
            panelButtons.Location = new Point(8, 112);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(420, 56);
            panelButtons.TabIndex = 4;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(230, 12);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(90, 34);
            btnCancelar.TabIndex = 1;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnCrear
            // 
            btnCrear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCrear.Location = new Point(327, 12);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(90, 34);
            btnCrear.TabIndex = 0;
            btnCrear.Text = "Crear";
            btnCrear.UseVisualStyleBackColor = true;
            btnCrear.Click += btnCrear_Click;
            // 
            // NuevoChatForm
            // 
            AcceptButton = btnCrear;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancelar;
            ClientSize = new Size(460, 200);
            Controls.Add(tableLayoutPanelMain);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "NuevoChatForm";
            Padding = new Padding(12);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Nuevo chat";
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPuertoRemoto).EndInit();
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        private TableLayoutPanel tableLayoutPanelMain;
        private Label lblIpRemota;
        private TextBox txtIpRemota;
        private Label lblPuertoRemoto;
        private NumericUpDown nudPuertoRemoto;
        private Panel panelButtons;
        private Button btnCancelar;
        private Button btnCrear;

        #endregion
    }
}
