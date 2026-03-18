namespace Whatsapp2
{
    public partial class LogsForm : Form
    {
        public LogsForm()
        {
            InitializeComponent();
        }

        public void AddLog(string texto)
        {
            if (InvokeRequired)
            {
                BeginInvoke(() => AddLog(texto));
                return;
            }

            rtbLogs.AppendText($"[{DateTime.Now:HH:mm:ss}] {texto}{Environment.NewLine}");
            rtbLogs.SelectionStart = rtbLogs.TextLength;
            rtbLogs.ScrollToCaret();
        }
    }
}
