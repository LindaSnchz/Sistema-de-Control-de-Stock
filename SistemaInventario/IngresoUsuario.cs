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

namespace SistemaInventario
{
    public partial class IngresoUsuario : Form
    {
        SqlConnection Miconexion = new SqlConnection("Data Source=LAPTOP-HKJ7G8S5\\SQLEXPRESS;database=Proyecto;Integrated Security=true");
        public IngresoUsuario()
        {
            InitializeComponent();
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            txtContra.UseSystemPasswordChar = false;
        }

        private void chkVerContra_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVerContra.Checked == false)
            {
                txtContra.UseSystemPasswordChar = true;
            }
            else
            {
                txtContra.UseSystemPasswordChar= false;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            Miconexion.Open();
            string cadena = "SELECT COUNT(*) FROM Usuarios WHERE Usuario = '" + txtUsuario.Text + "' and Contra = '" + txtContra.Text + "'";
            SqlDataAdapter Adaptador = new SqlDataAdapter(cadena, Miconexion);
            DataTable dt = new DataTable();
            Adaptador.Fill(dt);

            if (dt.Rows[0][0].ToString() == "1")
            {
                PantInicial PI = new PantInicial();
                PI.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario/contraseña incorrecta. Por favor, intentelo de nuevo","Alerta");
                txtUsuario.Focus();
            }

            Miconexion.Close();
            
        }
    }
}
