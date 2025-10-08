using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Proyecto_Club_Deportivo
{
    public partial class ingresoForm : Form
    {
        public ingresoForm()
        {
            InitializeComponent();
            textPassword.UseSystemPasswordChar = true; // para mayor seguridad en password se ocultan caracteres
            this.Load += ingresoForm_Load;
        }

        public class ConexionBD
        {
            private MySqlConnection conexion;

            public ConexionBD()
            {
                string cadenaConexion = "Server=localhost;Database=ProyectoClub;Uid=clubuser;Pwd=Grupo12;";
                conexion = new MySqlConnection(cadenaConexion);
            }

            public bool ProbarConexion()
            {
                try
                {
                    conexion.Open();
                    conexion.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error en la conexión: " + ex.Message,
                        "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        // Aca se prueba la conexión y devuelve mensaje
        private void ingresoForm_Load(object sender, EventArgs e)

        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            ConexionBD conexion = new ConexionBD();

            if (conexion.ProbarConexion())
            {
                MessageBox.Show("✅ Conexión exitosa a la base de datos", "Conexión",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("❌ Error al conectar con la base de datos", "Conexión",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void botonIngresar_Click(object sender, EventArgs e)
        {
            string usuario = textUsuario.Text;
            string password = textPassword.Text;

            if (usuario == "Admin" && password == "Admin123")
            {
                formPrincipal principal = new formPrincipal();
                principal.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Credenciales incorrectas", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void botonLimpiar_Click(object sender, EventArgs e)
        {
            textUsuario.Clear();
            textPassword.Clear();
        }


        private void botonSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void ingresoForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}
