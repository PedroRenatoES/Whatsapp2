using System.Net;

namespace Whatsapp2
{
    public partial class NuevoChatForm : Form
    {
        public string IpRemota => txtIpRemota.Text.Trim();
        public int PuertoRemoto => (int)nudPuertoRemoto.Value;

        public NuevoChatForm()
        {
            InitializeComponent();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (!IPAddress.TryParse(IpRemota, out _))
            {
                MessageBox.Show(this, "Ingresa una IP válida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIpRemota.Focus();
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
