using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Windows.Forms;

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
                //Credenciales en app.Config por cuestiones de seguridad
                string password = Environment.GetEnvironmentVariable("DB_PASSWORD");

               
                string baseConnection = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

                
                string cadenaConexion = string.Format(baseConnection, password);

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
                MessageBox.Show("Conexión exitosa a la base de datos", "Conexión",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(" Error al conectar con la base de datos", "Conexión",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Botón de Login
        private void botonIngresar_Click(object sender, EventArgs e)
        {
            string usuario = textUsuario.Text.Trim();
            string password = textPassword.Text.Trim();

            // Validación de campos vacíos
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "Campos incompletos",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return; 
            }

            // Credenciales en app.Config por cuestiones de seguridad
            string adminUser = ConfigurationManager.AppSettings["AdminUser"];
            string adminPassword = ConfigurationManager.AppSettings["AdminPassword"];

            if (usuario == adminUser && password == adminPassword)
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


        //Limpia el texto de los campos a completar

        private void botonLimpiar_Click(object sender, EventArgs e)
        {
            textUsuario.Clear();
            textPassword.Clear();
        }


        //Boton para salir de la app

        private void botonSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
       
    }
}
