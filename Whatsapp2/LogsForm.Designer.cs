namespace Whatsapp2
{
    partial class LogsForm
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
            rtbLogs = new RichTextBox();
            SuspendLayout();
            // 
            // rtbLogs
            // 
            rtbLogs.Dock = DockStyle.Fill;
            rtbLogs.Font = new Font("Consolas", 9F);
            rtbLogs.Location = new Point(0, 0);
            rtbLogs.Name = "rtbLogs";
            rtbLogs.ReadOnly = true;
            rtbLogs.Size = new Size(760, 420);
            rtbLogs.TabIndex = 0;
            rtbLogs.Text = "";
            // 
            // LogsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(760, 420);
            Controls.Add(rtbLogs);
            Name = "LogsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Logs de cifrado";
            ResumeLayout(false);
        }

        private RichTextBox rtbLogs;

        #endregion
    }
}
