namespace Whatsapp2
{
    public partial class ConfiguracionForm : Form
    {
        public int PuertoLocal => (int)nudPuertoLocal.Value;

        public ConfiguracionForm(int puertoLocal)
        {
            InitializeComponent();
            nudPuertoLocal.Value = puertoLocal;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
