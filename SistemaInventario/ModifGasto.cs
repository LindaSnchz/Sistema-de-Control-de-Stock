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
    public partial class ModifGasto : Form
    {
        SqlConnection Miconexion8;
        public ModifGasto(SqlConnection Miconexion6)
        {
            InitializeComponent();
            Miconexion8 = Miconexion6;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                Miconexion8.Open();

                int ID = Convert.ToInt32(txtID.Text);
                string cadena = "SELECT * FROM Gastos WHERE (ID_Gasto = " + ID + " )";

                SqlCommand Comando = new SqlCommand(cadena, Miconexion8);
                SqlDataReader leer = Comando.ExecuteReader();
                if (leer.Read())
                {
                    txtDescripcion.Text = leer["Descripcion"].ToString();
                    cmbCategoria.SelectedItem = leer["Categoria"].ToString();
                    txtGasto.Text = leer["Gasto"].ToString();

                    Miconexion8.Close();
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("No se ha podido ingresar a los registros", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Miconexion8.Close();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (txtDescripcion.Text == "" || cmbCategoria.SelectedItem.ToString() == "" || txtGasto.Text == "")
            {
                MessageBox.Show("Inserción no válida. Por favor, completar los datos en los campos correspondientes.", "Información");
                txtDescripcion.Focus();
            }
            else
            {
                try
                {
                    Miconexion8.Open();

                    int ID = Convert.ToInt32(txtID.Text);
                    string Descripcion = txtDescripcion.Text;
                    string Categoria = cmbCategoria.SelectedItem.ToString();
                    double Gasto = Convert.ToDouble(txtGasto.Text);

                    string cadena = "UPDATE Gastos SET Descripcion= '" + Descripcion + "', Categoria= '" + Categoria + "', Gasto= " + Gasto + "" + "WHERE (ID_Gasto= " + ID + ");";

                    SqlCommand comando = new SqlCommand(cadena, Miconexion8);

                    comando.ExecuteNonQuery();
                    MessageBox.Show("El gasto ha sido modificado exitosamente", "Informacion", MessageBoxButtons.OK);

                    Miconexion8.Close();

                    DialogResult msg = MessageBox.Show("¿Desea modificar otro producto?", "Modificar producto", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (msg == DialogResult.Yes)
                    {
                        Inicializar();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show("No se ha podido realizar la modificación", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Miconexion8.Close();
                }
            }
        }

        private void Inicializar()
        {
            txtID.Text = "";
            txtDescripcion.Text = "";
            txtGasto.Text = "";
            cmbCategoria.SelectedItem = null;
            txtID.Focus();
        }

        private void ModifGasto_Load(object sender, EventArgs e)
        {
            txtID.Focus();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Inicializar();
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
