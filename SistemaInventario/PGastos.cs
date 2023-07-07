using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaInventario
{
    public partial class PGastos : Form
    {
        SqlConnection Miconexion6;
        public PGastos(SqlConnection Miconexion)
        {
            InitializeComponent();
            Miconexion6 = Miconexion;
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            this.Close();
            PantInicial pI = new PantInicial();
            pI.Show();
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


        private void btnConfiguracion_Click(object sender, EventArgs e)
        {
            PanelSubmenuConfig.Visible = true;
        }

        private void btnConfigUsuarios_Click(object sender, EventArgs e)
        {
            this.Close();
            PanelSubmenuConfig.Visible = false;
            ConfigUsuario CU = new ConfigUsuario(Miconexion6);
            CU.Show();
        }

        private void btnConfigAcerca_Click(object sender, EventArgs e)
        {
            PanelSubmenuConfig.Visible = false;
            AcercaDe AD = new AcercaDe();
            AD.Show();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            this.Close();
            PProductos PP = new PProductos(Miconexion6);
            PP.Show();
        }

        private void btnGastos_Click(object sender, EventArgs e)
        {
            this.Close();
            PGastos PG = new PGastos(Miconexion6);
            PG.Show();
        }

        private void PGastos_Load(object sender, EventArgs e)
        {
            Miconexion6.Open();
            string cadena = "SELECT ID_Gasto AS 'ID', Descripcion, Categoria, Gasto AS 'Gasto realizado' FROM Gastos";

            SqlDataAdapter Adaptador = new SqlDataAdapter(cadena, Miconexion6);

            DataSet Conjunto = new DataSet();
            Adaptador.Fill(Conjunto, "GASTOS");

            dgvGastos.DataSource = Conjunto;
            dgvGastos.DataMember = "GASTOS";

            Miconexion6.Close();
        }

        private void btnAnadir_Click(object sender, EventArgs e)
        {
            AddGastos AG = new AddGastos(Miconexion6);
            AG.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "-")
            {
                Miconexion6.Open();

                string refresh = "SELECT ID_Gasto AS 'ID', Descripcion, Categoria, Gasto AS 'Gasto realizado' FROM Gastos";

                SqlDataAdapter Adaptador = new SqlDataAdapter(refresh, Miconexion6);
                DataSet Conjunto = new DataSet();
                Adaptador.Fill(Conjunto, "GASTOS");

                dgvGastos.DataSource = Conjunto;
                dgvGastos.DataMember = "GASTOS";

                Miconexion6.Close();
            }
            else
            {
                Miconexion6.Open();
                string cadena = "SELECT ID_Gasto AS 'ID', Descripcion, Categoria, Gasto AS 'Gasto realizado' FROM Gastos WHERE Categoria= '" + comboBox1.SelectedItem.ToString() + "'";

                SqlDataAdapter Adaptador = new SqlDataAdapter(cadena, Miconexion6);
                DataSet Conjunto = new DataSet();
                Adaptador.Fill(Conjunto, "GASTOS");

                dgvGastos.DataSource = Conjunto;
                dgvGastos.DataMember = "GASTOS";

                Miconexion6.Close();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Miconexion6.Open();

            string refresh = "SELECT ID_Gasto AS 'ID', Descripcion, Categoria, Gasto AS 'Gasto realizado' FROM Gastos";

            SqlDataAdapter Adaptador = new SqlDataAdapter(refresh, Miconexion6);
            DataSet Conjunto = new DataSet();
            Adaptador.Fill(Conjunto, "GASTOS");

            dgvGastos.DataSource = Conjunto;
            dgvGastos.DataMember = "GASTOS";

            Miconexion6.Close();
        }

        private void btnModif_Click(object sender, EventArgs e)
        {
            ModifGasto MG = new ModifGasto(Miconexion6);
            MG.Show();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            BorrarGasto BG = new BorrarGasto(Miconexion6);
            BG.Show();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void PanelSup_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
