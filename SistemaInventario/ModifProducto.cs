using System;
using System.CodeDom;
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
    public partial class ModifProducto : Form
    {
        SqlConnection Miconexion4;
        public ModifProducto(SqlConnection Miconexion2)
        {
            InitializeComponent();
            Miconexion4 = Miconexion2;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                Miconexion4.Open();

                int ID = Convert.ToInt32(txtID.Text);
                string cadena = "SELECT * FROM Productos WHERE (ID = " + ID + " )";

                SqlCommand Comando = new SqlCommand(cadena, Miconexion4);
                SqlDataReader leer = Comando.ExecuteReader();
                if (leer.Read())
                {
                    txtNombre.Text = leer["Nombre"].ToString();
                    cmbCategoria.SelectedItem = leer["Categoria"].ToString();
                    numCant.Value = Convert.ToDecimal(leer["Cantidad_dispo"].ToString());
                    txtCostoU.Text = leer["Costo_unitario"].ToString();
                    txtPrecioU.Text = leer["Precio_unitario"].ToString();

                    Miconexion4.Close();
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("No se ha podido ingresar a los registros", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Miconexion4.Close();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if(txtNombre.Text == "" || cmbCategoria.SelectedItem.ToString() == "" || txtCostoU.Text == "" || txtPrecioU.Text == "")
            {
                MessageBox.Show("Inserción no válida. Por favor, completar los datos en los campos correspondientes.", "Información");
                txtNombre.Focus();
            }
            else
            {
                try
                {
                    Miconexion4.Open();

                    int ID = Convert.ToInt32(txtID.Text);
                    string Nombre = txtNombre.Text;
                    string Categoria = cmbCategoria.SelectedItem.ToString();
                    decimal Cantidad = numCant.Value;
                    decimal Costo = Convert.ToInt32(txtCostoU.Text);
                    decimal Precio = Convert.ToInt32(txtPrecioU.Text);

                    string cadena = "UPDATE Productos SET Nombre= '" + Nombre + "', Categoria= '" + Categoria + "', Cantidad_dispo= " + Cantidad + ", Costo_unitario=" + Costo + ", Precio_unitario=" + Precio + "" + "WHERE (ID= " + ID + ");";

                    SqlCommand comando = new SqlCommand(cadena, Miconexion4);

                    comando.ExecuteNonQuery();
                    MessageBox.Show("El producto ha sido modificado exitosamente", "Informacion", MessageBoxButtons.OK);

                    Miconexion4.Close();

                    DialogResult msg = MessageBox.Show("Desea modificar otro producto?", "Modificar producto", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (msg == DialogResult.Yes)
                    {
                        Inicializar();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                catch(SqlException)
                {
                    MessageBox.Show("No se ha podido realizar la modificacion", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Miconexion4.Close();
                }
            }
        }
        public void Inicializar()
        {
            txtID.Text = "";
            txtNombre.Text = "";
            cmbCategoria.SelectedItem = null;
            numCant.Value = 0;
            txtCostoU.Text = "";
            txtPrecioU.Text = "";
            txtID.Focus();
        }

        private void ModifProducto_Load(object sender, EventArgs e)
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
