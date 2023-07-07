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
    public partial class AddGastos : Form
    {
        SqlConnection Miconexion7;
        public AddGastos(SqlConnection Miconexion6)
        {
            InitializeComponent();
            Miconexion7 = Miconexion6;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtDescrip.Text == "" || cmbCategoria.SelectedItem.ToString() == "" || txtGasto.Text == "")
            {
                MessageBox.Show("Insercion no valida. Por favor, completar los campos correspondientes");
                txtDescrip.Focus();
            }
            else
            {
                try
                {
                    Miconexion7.Open();
                    string Descripcion = txtDescrip.Text;
                    string Categoria = cmbCategoria.SelectedItem.ToString();
                    double Gasto = Convert.ToDouble(txtGasto.Text);

                    string cadena = "INSERT INTO Gastos(Descripcion, Categoria, Gasto) VALUES" + "('" + Descripcion + "', '" + cmbCategoria.SelectedItem.ToString() + "', " + Gasto + ");";

                    SqlCommand comando = new SqlCommand(cadena, Miconexion7);
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Los datos han sido agregados correctamente.");

                    Miconexion7.Close();

                    DialogResult msg = MessageBox.Show("Desea agregar otro gasto?", "Agregar gasto", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
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

                    MessageBox.Show("No se ha podido realizar la operacion", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Miconexion7.Close();
                }
            }
        }
        private void Inicializar()
        {
            txtDescrip.Text = "";
            txtGasto.Text = "";
            cmbCategoria.SelectedItem = null;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Inicializar();
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
