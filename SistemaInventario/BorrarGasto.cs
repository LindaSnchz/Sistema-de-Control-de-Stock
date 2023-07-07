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
    public partial class BorrarGasto : Form
    {
        SqlConnection Miconexion9;
        public BorrarGasto(SqlConnection Miconexion6)
        {
            InitializeComponent();
            Miconexion9 = Miconexion6;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                Miconexion9.Open();
                int ID = Convert.ToInt32(txtID.Text);
                string cadena = "SELECT * FROM Gastos WHERE (ID_Gasto= " + ID + ")";

                SqlCommand comando = new SqlCommand(cadena, Miconexion9);
                SqlDataReader leer = comando.ExecuteReader();
                if (leer.Read())
                {
                    txtDescrip.Text = leer["Descripcion"].ToString();
                    txtGasto.Text = leer["Gasto"].ToString();

                    Miconexion9.Close();
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("No se ha podido ingresar a los registros", "Alerta");
            }
            finally
            {
                Miconexion9.Close();
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
                    Miconexion9.Open();

                    int ID = Convert.ToInt32(txtID.Text);
                    string cadena = "DELETE FROM Gastos WHERE(ID_Gasto= " + ID + ")";

                    SqlCommand comando = new SqlCommand(cadena, Miconexion9);
                    comando.ExecuteNonQuery();
                    MessageBox.Show("El gasto ha sido eliminado correctamente", "Información", MessageBoxButtons.OK);
                    Miconexion9.Close();

                    DialogResult msg = MessageBox.Show("¿Desea eliminar otro gasto?", "Eliminar  gasto", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
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
                finally
                {
                    Miconexion9.Close();
                }
            }
        }
        private void Eliminar()
        {
            txtID.Text = string.Empty;
            txtID.Focus();
            txtGasto.Text = string.Empty;
            txtDescrip.Text = string.Empty;
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

        private void BorrarGasto_Load(object sender, EventArgs e)
        {
            txtID.Focus();
        }
    }
}
