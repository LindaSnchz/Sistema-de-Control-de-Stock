using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices; //libreria que permite que el codigo de arrastre funcione


namespace SistemaInventario
{
    public partial class PantInicial : Form
    {
        SqlConnection Miconexion = new SqlConnection("Data Source=LAPTOP-HKJ7G8S5\\SQLEXPRESS;database=Proyecto;Integrated Security=true");
        public PantInicial()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnRestaurar.Visible = false;
            btnMaximizar.Visible = true;
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        //codigo que me permite arrastrar la aplicacion con el mouse
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void PanelSup_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnConfiguracion_Click(object sender, EventArgs e)
        {
            PanelSubmenuConfig.Visible = true;
        }

        private void btnConfigUsuarios_Click(object sender, EventArgs e)
        {
            PanelSubmenuConfig.Visible = false;
            ConfigUsuario CU = new ConfigUsuario(Miconexion);
            this.Close();
            CU.Show();
        }

        private void btnConfigAcerca_Click(object sender, EventArgs e)
        {
            PanelSubmenuConfig.Visible = false;
            AcercaDe AD = new AcercaDe();
            AD.Show();
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            this.Hide();
            PProductos pP = new PProductos(Miconexion);
            pP.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Miconexion.Open();
            string cadena = "SELECT SUM(Costo_unitario*Cantidad_dispo) AS 'Costo total' from Productos";
            SqlCommand suma = new SqlCommand(cadena, Miconexion);
            string sumaCostos = Convert.ToString(suma.ExecuteScalar());
            lblCostos.Text = $"${sumaCostos}";

            cadena = "SELECT SUM(Precio_unitario*Cantidad_dispo) AS 'Valor total' from Productos";
            SqlCommand suma2 = new SqlCommand(cadena, Miconexion);
            string sumaPrecios = Convert.ToString(suma2.ExecuteScalar());
            lblValor.Text = $"${sumaPrecios}";

            lblGanancias.Text = $"${Convert.ToDouble(sumaPrecios) - Convert.ToDouble(sumaCostos)}";
            Miconexion.Close();
        }

        private void PantInicial_Load(object sender, EventArgs e)
        {
            Miconexion.Open();
            string cadena = "SELECT SUM(Costo_unitario*Cantidad_dispo) AS 'Costo total' from Productos";
            SqlCommand suma = new SqlCommand(cadena, Miconexion);
            string sumaCostos = Convert.ToString(suma.ExecuteScalar());
            lblCostos.Text = $"${sumaCostos}";

            cadena = "SELECT SUM(Precio_unitario*Cantidad_dispo) AS 'Valor total' from Productos";
            SqlCommand suma2 = new SqlCommand(cadena, Miconexion);
            string sumaPrecios = Convert.ToString(suma2.ExecuteScalar());
            lblValor.Text = $"${sumaPrecios}";

            lblGanancias.Text = $"${Convert.ToDouble(sumaPrecios) - Convert.ToDouble(sumaCostos)}";
            Miconexion.Close();

        }

        private void btnGastos_Click(object sender, EventArgs e)
        {
            this.Close();
            PGastos pG = new PGastos(Miconexion);
            pG.Show();
        }
    }
}
