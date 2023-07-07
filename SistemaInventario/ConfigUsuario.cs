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
    public partial class ConfigUsuario : Form
    {
        SqlConnection Miconexion7;

        public ConfigUsuario(SqlConnection Miconexion)
        {
            InitializeComponent();
            Miconexion7 = Miconexion;
        }

        private void ConfigUsuario_Load(object sender, EventArgs e)
        {
            Miconexion7.Open();
            string cadena = "SELECT Usuario, Nombre, Contra AS 'Contraseña' FROM Usuarios";

            SqlDataAdapter Adaptador = new SqlDataAdapter(cadena, Miconexion7);

            DataSet Conjunto = new DataSet();
            Adaptador.Fill(Conjunto, "USUARIOS");

            dgvUsers.DataSource = Conjunto;
            dgvUsers.DataMember = "USUARIOS";

            Miconexion7.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtUser.ReadOnly = false;
            txtName.ReadOnly = false;
            txtContra.ReadOnly = false;
            lblTexto.Visible = true;
            lblPresione.Visible = false;
            lblAnadir.Visible = true;

            if (txtName.Text == "" || txtUser.Text == "" || txtContra.Text == "")
            {
                txtUser.Focus();
            }
            else
            {
                try
                {
                    Miconexion7.Open();
                    string Nombre = txtName.Text;
                    string Usuario = txtUser.Text;
                    string Contra = txtContra.Text;

                    string cadena = "INSERT INTO Usuarios(Usuario, Nombre, Contra) VALUES" + "('" + Usuario + "', '" + Nombre + "', '" + Contra + "');";

                    SqlCommand comando = new SqlCommand(cadena, Miconexion7);
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Los datos han sido agregados correctamente.");

                    Miconexion7.Close();

                    DialogResult msg = MessageBox.Show("¿Desea agregar otro usuario?", "Agregar usuario", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (msg == DialogResult.Yes)
                    {
                        Inicializar();
                    }
                    else
                    {
                        lblPresione.Visible = true;
                        lblTexto.Visible = false;
                        txtContra.ReadOnly = true;
                        txtName.ReadOnly = true;
                        txtUser.Text = "";
                        txtContra.Text = "";
                        txtName.Text = "";
                        lblAnadir.Visible = false;
                    }

                }
                catch (SqlException)
                {

                    MessageBox.Show("No se ha podido realizar la operación", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Miconexion7.Close();
                }
            }
        }
        private void Inicializar()
        {
            txtUser.Text = "";
            txtName.Text = "";
            txtContra.Text = "";
            txtUser.Focus();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Miconexion7.Open();
            string cadena = "SELECT Usuario, Nombre, Contra AS 'Contraseña' FROM Usuarios";

            SqlDataAdapter Adaptador = new SqlDataAdapter(cadena, Miconexion7);

            DataSet Conjunto = new DataSet();
            Adaptador.Fill(Conjunto, "USUARIOS");

            dgvUsers.DataSource = Conjunto;
            dgvUsers.DataMember = "USUARIOS";

            Miconexion7.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            txtUser.ReadOnly = false;
            txtUser.Focus();
            lblIngrese.Visible = true;
            btnBusqueda.Visible = true;
            btnGuardar.Visible = true;
            lblAnadir.Visible = false;
            lblTexto.Visible = false;
            lblPresione.Visible = false;

            if (txtUser.Text == "")
            {
                MessageBox.Show("Ingrese el usuario que desea modificar.", "Información");
            }

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

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnRestaurar.Visible = false;
            btnMaximizar.Visible = true;
        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            txtName.ReadOnly = false;
            txtContra.ReadOnly = false;
            try
            {
                Miconexion7.Open();
                string Usuario = txtUser.Text;
                string cadena = "SELECT * FROM Usuarios WHERE (Usuario = '" + Usuario + "' )";

                SqlCommand Comando = new SqlCommand(cadena, Miconexion7);
                SqlDataReader leer = Comando.ExecuteReader();
                if (leer.Read())
                {
                    txtName.Text = leer["Nombre"].ToString();
                    txtContra.Text = leer["Contra"].ToString();

                    Miconexion7.Close();
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("No se ha podido ingresar a los registros", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Miconexion7.Close();

            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtUser.Text == "" || txtName.Text == "" || txtContra.Text == "")
            {
                MessageBox.Show("Inserción no válida. Por favor, completar los datos en los campos correspondientes.", "Información");
                txtUser.Focus();
            }
            else
            {
                try
                {
                    Miconexion7.Open();

                    string Usuario = txtUser.Text;
                    string Nombre = txtName.Text;
                    string Contra = txtContra.Text;


                    string cadena = "UPDATE Usuarios SET Nombre= '" + Nombre + "', Contra= '" + Contra + "'" + "WHERE (Usuario= '" + Usuario + "');";

                    SqlCommand comando = new SqlCommand(cadena, Miconexion7);

                    comando.ExecuteNonQuery();
                    MessageBox.Show("El usuario ha sido modificado exitosamente", "Información", MessageBoxButtons.OK);

                    Miconexion7.Close();

                    DialogResult msg = MessageBox.Show("¿Desea modificar otro usuario?", "Modificar usuario", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (msg == DialogResult.Yes)
                    {
                        Inicializar();
                    }
                    else
                    {
                        lblPresione.Visible = true;
                        txtUser.Text = "";
                        txtName.Text = "";
                        txtContra.Text = "";
                        txtContra.ReadOnly = true;
                        txtName.ReadOnly = true;
                        lblIngrese.Visible = false;
                        btnBusqueda.Visible = false;
                        btnGuardar.Visible = false;
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show("No se ha podido realizar la modificación", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Miconexion7.Close();
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            txtUser.ReadOnly = false;
            txtUser.Focus();
            lblEliminar.Visible = true;
            lblAnadir.Visible = false;
            lblIngrese.Visible = false;
            lblPresione.Visible = false;
            lblTexto.Visible = false;
            btnGuardar.Visible = false;
            btnBusqueda.Visible = true;
            btnElimina.Visible = true;


        }

        private void btnElimina_Click(object sender, EventArgs e)
        {
            if (txtUser.Text == "")
            {
                MessageBox.Show("Eliminación no válida. Por favor, ingrese el Usuario que desea eliminar.", "Alerta");
            }
            else
            {
                try
                {
                    Miconexion7.Open();

                    string Usuario = txtUser.Text;
                    string cadena = "DELETE FROM Usuarios WHERE(Usuario= '" + Usuario + "')";

                    SqlCommand comando = new SqlCommand(cadena, Miconexion7);
                    comando.ExecuteNonQuery();
                    MessageBox.Show("El usuario ha sido eliminado correctamente", "Información", MessageBoxButtons.OK);
                    Miconexion7.Close();

                    DialogResult msg = MessageBox.Show("¿Desea eliminar otro usuario?", "Eliminar  usuario", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (msg == DialogResult.Yes)
                    {
                        Inicializar();
                    }
                    else
                    {
                        btnElimina.Visible = false;
                        lblEliminar.Visible = false;
                        lblPresione.Visible = true;
                        btnBusqueda.Visible = false;
                        txtUser.Text = "";
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show("No se ha podido eliminar el producto", "Alerta");
                }
                finally
                {
                    Miconexion7.Close();
                }
            }
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            this.Close();
            PantInicial PI = new PantInicial();
            PI.Show();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            this.Close();
            PProductos PP = new PProductos(Miconexion7);
            PP.Show();
        }

        private void btnGastos_Click(object sender, EventArgs e)
        {
            this.Close();
            PGastos PG = new PGastos(Miconexion7);
            PG.Show();
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

        private void btnConfigAcerca_Click(object sender, EventArgs e)
        {
            AcercaDe AD = new AcercaDe();
            AD.Show();
        }
    }
}
