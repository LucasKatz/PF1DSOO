using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Proyecto_Club_Deportivo
{
    // Clase para manejar la conexión a la base de datos
    public class ConexionBD
    {
        private MySqlConnection conexion;

        public ConexionBD()
        {
           
            string cadenaConexion = "Server=localhost;Database=ProyectoClub;Uid=clubuser;Pwd=Grupo12;";

            conexion = new MySqlConnection(cadenaConexion);
        }

        public MySqlConnection GetConnection()
        {
            return conexion;
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

    // Formulario de login
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
            textPassword.UseSystemPasswordChar = true; // Ocultar caracteres del password
        }

        private void formularioLogin(object sender, EventArgs e)
        {
            // Probar conexión cuando carga el formulario
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
                // Abrir el formulario principal
                formPrincipal principal = new formPrincipal();
                principal.Show();

                // Ocultar el formulario de login
                this.Hide();
            }
            else
            {
                // Credenciales incorrectas
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

        /*private void textUsuario_TextChanged(object sender, EventArgs e)
        {
            // Opcional: lógica al escribir en usuario
        }

        private void textPassword_TextChanged(object sender, EventArgs e)
        {
            // Opcional: lógica al escribir en password
        }*/
    }
}

