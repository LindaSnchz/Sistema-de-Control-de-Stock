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
    public partial class PProductos : Form
    {
        SqlConnection Miconexion2;
        public PProductos(SqlConnection Miconexion)
        {
            InitializeComponent();
            Miconexion2 = Miconexion;
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
            this.Close();
            PanelSubmenuConfig.Visible = false;
            ConfigUsuario CU = new ConfigUsuario(Miconexion2);
            CU.Show();
        }

        private void btnConfigAcerca_Click(object sender, EventArgs e)
        {
            PanelSubmenuConfig.Visible = false;
            AcercaDe AD = new AcercaDe();
            AD.Show();
        }

        private void btnAnadir_Click(object sender, EventArgs e)
        {
            AddProducto AP = new AddProducto(Miconexion2);
            AP.Show();
        }

        private void PProductos_Load(object sender, EventArgs e)
        {
            Miconexion2.Open();
            string cadena = "SELECT ID, Nombre, Categoria, Cantidad_dispo AS 'Cantidad disponible', Costo_unitario AS 'Costo unitario', Precio_unitario AS 'Precio unitario' FROM Productos";

            SqlDataAdapter Adaptador = new SqlDataAdapter(cadena, Miconexion2);

            DataSet Conjunto = new DataSet();
            Adaptador.Fill(Conjunto, "PRODUCTOS");

            dgvProductos.DataSource = Conjunto;
            dgvProductos.DataMember = "PRODUCTOS";

            Miconexion2.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Miconexion2.Open();

            string refresh = "SELECT ID, Nombre, Categoria, Cantidad_dispo AS 'Cantidad disponible', Costo_unitario AS 'Costo unitario', Precio_unitario AS 'Precio unitario' FROM Productos";

            SqlDataAdapter Adaptador = new SqlDataAdapter(refresh, Miconexion2);
            DataSet Conjunto = new DataSet();
            Adaptador.Fill(Conjunto, "PRODUCTOS");

            dgvProductos .DataSource = Conjunto;
            dgvProductos.DataMember = "PRODUCTOS";

            Miconexion2.Close();
        }

        private void btnModif_Click(object sender, EventArgs e)
        {
            ModifProducto MP = new ModifProducto(Miconexion2);
            MP.Show();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            BorrarProductos BP = new BorrarProductos(Miconexion2);
            BP.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "-")
            {
                Miconexion2.Open();

                string refresh = "SELECT ID, Nombre, Categoria, Cantidad_dispo AS 'Cantidad disponible', Costo_unitario AS 'Costo unitario', Precio_unitario AS 'Precio unitario' FROM Productos";

                SqlDataAdapter Adaptador = new SqlDataAdapter(refresh, Miconexion2);
                DataSet Conjunto = new DataSet();
                Adaptador.Fill(Conjunto, "PRODUCTOS");

                dgvProductos.DataSource = Conjunto;
                dgvProductos.DataMember = "PRODUCTOS";

                Miconexion2.Close();
            }
            else
            {
                Miconexion2.Open();
                string cadena = "SELECT ID, Nombre, Categoria, Cantidad_dispo AS 'Cantidad disponible', Costo_unitario AS 'Costo unitario', Precio_unitario AS 'Precio unitario' FROM Productos WHERE Categoria= '" + comboBox1.SelectedItem.ToString() + "'";

                SqlDataAdapter Adaptador = new SqlDataAdapter(cadena, Miconexion2);
                DataSet Conjunto = new DataSet();
                Adaptador.Fill(Conjunto, "PRODUCTOS");

                dgvProductos.DataSource = Conjunto;
                dgvProductos.DataMember = "PRODUCTOS";

                Miconexion2.Close();
            }
            
        }

        private void btnGastos_Click(object sender, EventArgs e)
        {
            this.Close();
            PGastos PG = new PGastos(Miconexion2);
            PG.Show();
        }
    }
}
