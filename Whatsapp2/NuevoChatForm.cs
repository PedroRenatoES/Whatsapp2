using System.Net;

namespace Whatsapp2
{
    public partial class NuevoChatForm : Form
    {
        public string NombreChat => txtNombre.Text.Trim();
        public string IpRemota => txtIpRemota.Text.Trim();
        public int PuertoRemoto => (int)nudPuertoRemoto.Value;

        public NuevoChatForm()
            : this(string.Empty, string.Empty, 5000, "Nuevo chat")
        {
        }

        public NuevoChatForm(string nombre, string ipRemota, int puertoRemoto, string titulo)
        {
            InitializeComponent();
            Text = titulo;
            txtNombre.Text = nombre;
            txtIpRemota.Text = ipRemota;
            nudPuertoRemoto.Value = puertoRemoto;
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
