using MySql.Data.MySqlClient;
using Proyecto_Club_Deportivo.Datos;
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
            textPassword.UseSystemPasswordChar = true; // ocultar caracteres del password
            this.Load += ingresoForm_Load;
        }

        public class ConexionBD
        {
            private MySqlConnection conexion;

            public ConexionBD()
            {
                conexion = Conexion.GetInstancia().CrearConexion();
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
                MessageBox.Show("Error al conectar con la base de datos", "Conexión",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void botonIngresar_Click(object sender, EventArgs e)
        {
            string usuario = textUsuario.Text.Trim();
            string password = textPassword.Text.Trim();

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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

        private void botonLimpiar_Click(object sender, EventArgs e)
        {
            textUsuario.Clear();
            textPassword.Clear();
        }

        private void botonSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

