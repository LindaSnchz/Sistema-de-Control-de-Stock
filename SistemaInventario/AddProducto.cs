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
    public partial class AddProducto : Form
    {
        SqlConnection Miconexion3;
        public AddProducto(SqlConnection Miconexion2)
        {
            InitializeComponent();
            Miconexion3 = Miconexion2;
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == "" || cmbCategoria.SelectedItem.ToString() == "" || txtCostoU.Text == "" || txtPrecioU.Text == "")
            {
                MessageBox.Show("Insercion no valida. Por favor, completar los campos correspondientes");
                txtNombre.Focus();
            }
            else
            {
                try
                {
                    Miconexion3.Open();
                    string Nombre = txtNombre.Text;
                    string Categoria = cmbCategoria.SelectedItem.ToString();
                    decimal Cantidad = numCant.Value;
                    double Costo = Convert.ToDouble(txtCostoU.Text);
                    double Precio = Convert.ToDouble(txtPrecioU.Text);

                    string cadena = "INSERT INTO Productos(Nombre, Categoria, Cantidad_dispo, Costo_unitario, Precio_unitario) VALUES" + "('" + Nombre + "', '" + Categoria + "', " + Cantidad + ", " + Costo + ", " + Precio + ");";

                    SqlCommand comando = new SqlCommand(cadena, Miconexion3);
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Los datos han sido agregados correctamente.");

                    Miconexion3.Close();

                    DialogResult msg = MessageBox.Show("¿Desea agregar otro producto?", "Agregar producto", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
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
                    
                    MessageBox.Show("No se ha podido realizar la operación", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Miconexion3.Close();
                }
            }
        }

        public void Inicializar()
        {
            txtNombre.Text = "";
            cmbCategoria.SelectedItem = null;
            numCant.Value = 0;
            txtCostoU.Text = "";
            txtPrecioU.Text = "";
            txtNombre.Focus();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddProducto_Load(object sender, EventArgs e)
        {
            txtNombre.Focus();
        }
    }
}
