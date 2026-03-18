namespace Whatsapp2
{
    partial class ConfiguracionForm
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
            lblPuertoLocal = new Label();
            nudPuertoLocal = new NumericUpDown();
            lblClaveCompartida = new Label();
            txtClaveCompartida = new TextBox();
            panelButtons = new Panel();
            btnCerrar = new Button();
            btnGuardar = new Button();
            tableLayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPuertoLocal).BeginInit();
            panelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.ColumnCount = 2;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanelMain.Controls.Add(lblPuertoLocal, 0, 0);
            tableLayoutPanelMain.Controls.Add(nudPuertoLocal, 1, 0);
            tableLayoutPanelMain.Controls.Add(lblClaveCompartida, 0, 1);
            tableLayoutPanelMain.Controls.Add(txtClaveCompartida, 1, 1);
            tableLayoutPanelMain.Controls.Add(panelButtons, 0, 2);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(12, 12);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.Padding = new Padding(5);
            tableLayoutPanelMain.RowCount = 3;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Size = new Size(416, 178);
            tableLayoutPanelMain.TabIndex = 0;
            tableLayoutPanelMain.SetColumnSpan(panelButtons, 2);
            // 
            // lblPuertoLocal
            // 
            lblPuertoLocal.Anchor = AnchorStyles.Left;
            lblPuertoLocal.AutoSize = true;
            lblPuertoLocal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPuertoLocal.Location = new Point(8, 21);
            lblPuertoLocal.Name = "lblPuertoLocal";
            lblPuertoLocal.Size = new Size(178, 23);
            lblPuertoLocal.TabIndex = 0;
            lblPuertoLocal.Text = "Puerto local (escucha)";
            // 
            // nudPuertoLocal
            // 
            nudPuertoLocal.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            nudPuertoLocal.Location = new Point(189, 16);
            nudPuertoLocal.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            nudPuertoLocal.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudPuertoLocal.Name = "nudPuertoLocal";
            nudPuertoLocal.Size = new Size(259, 27);
            nudPuertoLocal.TabIndex = 1;
            nudPuertoLocal.Value = new decimal(new int[] { 5000, 0, 0, 0 });
            // 
            // lblClaveCompartida
            // 
            lblClaveCompartida.Anchor = AnchorStyles.Left;
            lblClaveCompartida.AutoSize = true;
            lblClaveCompartida.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblClaveCompartida.Location = new Point(8, 73);
            lblClaveCompartida.Name = "lblClaveCompartida";
            lblClaveCompartida.Size = new Size(145, 23);
            lblClaveCompartida.TabIndex = 2;
            lblClaveCompartida.Text = "Clave compartida";
            // 
            // txtClaveCompartida
            // 
            txtClaveCompartida.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtClaveCompartida.Location = new Point(189, 68);
            txtClaveCompartida.Name = "txtClaveCompartida";
            txtClaveCompartida.PasswordChar = '*';
            txtClaveCompartida.Size = new Size(259, 27);
            txtClaveCompartida.TabIndex = 3;
            // 
            // panelButtons
            // 
            panelButtons.Controls.Add(btnCerrar);
            panelButtons.Controls.Add(btnGuardar);
            panelButtons.Dock = DockStyle.Fill;
            panelButtons.Location = new Point(8, 112);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(400, 58);
            panelButtons.TabIndex = 4;
            // 
            // btnCerrar
            // 
            btnCerrar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCerrar.DialogResult = DialogResult.Cancel;
            btnCerrar.Location = new Point(250, 15);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(90, 34);
            btnCerrar.TabIndex = 1;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            btnGuardar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnGuardar.Location = new Point(347, 15);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(90, 34);
            btnGuardar.TabIndex = 0;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // ConfiguracionForm
            // 
            AcceptButton = btnGuardar;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCerrar;
            ClientSize = new Size(440, 202);
            Controls.Add(tableLayoutPanelMain);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ConfiguracionForm";
            Padding = new Padding(12);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Configuración";
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPuertoLocal).EndInit();
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        private TableLayoutPanel tableLayoutPanelMain;
        private Label lblPuertoLocal;
        private NumericUpDown nudPuertoLocal;
        private Label lblClaveCompartida;
        private TextBox txtClaveCompartida;
        private Panel panelButtons;
        private Button btnCerrar;
        private Button btnGuardar;

        #endregion
    }
}
