namespace Whatsapp2
{
    public partial class ConfiguracionForm : Form
    {
        public int PuertoLocal => (int)nudPuertoLocal.Value;
        public string ClaveCompartida => txtClaveCompartida.Text;

        public ConfiguracionForm(int puertoLocal, string claveCompartida)
        {
            InitializeComponent();
            nudPuertoLocal.Value = puertoLocal;
            txtClaveCompartida.Text = claveCompartida;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ClaveCompartida))
            {
                MessageBox.Show(this, "La clave compartida es obligatoria.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtClaveCompartida.Focus();
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
