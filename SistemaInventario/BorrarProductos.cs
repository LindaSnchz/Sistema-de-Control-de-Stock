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
    public partial class BorrarProductos : Form
    {
        SqlConnection Miconexion5;
        public BorrarProductos(SqlConnection Miconexion2)
        {
            InitializeComponent();
            Miconexion5 = Miconexion2;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                Miconexion5.Open();
                int ID = Convert.ToInt32(txtID.Text);
                string cadena = "SELECT * FROM Productos WHERE (ID = " + ID + ")";

                SqlCommand comando = new SqlCommand(cadena, Miconexion5);
                SqlDataReader leer = comando.ExecuteReader();
                if (leer.Read())
                {
                    txtNombre.Text = leer["Nombre"].ToString();
                    txtCategoria.Text = leer["Categoria"].ToString();

                    Miconexion5.Close();
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("No se ha podido ingresar a los registros", "Alerta");
            }
            finally
            {
                Miconexion5.Close();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                MessageBox.Show("Eliminacion no valida. Por favor, ingrese el ID del producto que desea eliminar.", "Alerta");
            }
            else
            {
                try
                {
                    Miconexion5.Open();

                    int ID = Convert.ToInt32(txtID.Text);
                    string cadena = "DELETE FROM Productos WHERE(ID= " + ID + ")";

                    SqlCommand comando = new SqlCommand(cadena, Miconexion5);
                    comando.ExecuteNonQuery();
                    MessageBox.Show("El producto ha sido eliminado correctamente", "Información", MessageBoxButtons.OK);
                    Miconexion5.Close();

                    DialogResult msg = MessageBox.Show("¿Desea eliminar otro producto?", "Eliminar  producto", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (msg == DialogResult.Yes)
                    {
                        Eliminar();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show("No se ha podido eliminar el producto", "Alerta");
                }
                finally { 
                    Miconexion5.Close();
                }
            }
        }

        private void Eliminar()
        {
            txtID.Text = "";
            txtNombre.Text = "";
            txtCategoria.Text = "";

            txtID.Focus();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
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
