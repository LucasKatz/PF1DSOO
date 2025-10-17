using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace Proyecto_Club_Deportivo
{
    public partial class Form4 : Form
    {
        // Lee la cadena de conexión desde App.config
        private string connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

        public Form4()
        {
            InitializeComponent();

            // Configurar ComboBox Apto Médico
            apto_value.Items.Add("SI");
            apto_value.Items.Add("NO");
            apto_value.SelectedIndex = 0;
            apto_value.DropDownStyle = ComboBoxStyle.DropDownList;

            // Configurar ComboBox Género
            genero_value.Items.Add("F");
            genero_value.Items.Add("M");
            genero_value.Items.Add("X");
            genero_value.SelectedIndex = 0;
            genero_value.DropDownStyle = ComboBoxStyle.DropDownList;

            // Configurar ComboBox Tipo Documento
            tipoDocu.Items.Add("DNI");
            tipoDocu.Items.Add("LE");
            tipoDocu.Items.Add("LC");
            tipoDocu.Items.Add("DU");
            tipoDocu.Items.Add("PASAPORTE");
            tipoDocu.SelectedIndex = 0;
            tipoDocu.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void registro_Click(object sender, EventArgs e)
        {
            // ✅ VALIDACIÓN COMPLETA DE CAMPOS OBLIGATORIOS
            if (string.IsNullOrWhiteSpace(nombre_value.Text) ||
                string.IsNullOrWhiteSpace(apellido_value.Text) ||
                string.IsNullOrWhiteSpace(documento_value.Text) ||
                string.IsNullOrWhiteSpace(tel_value.Text) ||
                string.IsNullOrWhiteSpace(email_value.Text) ||
                string.IsNullOrWhiteSpace(nacimiento_value.Text) ||
                apto_value.SelectedItem == null ||
                genero_value.SelectedItem == null ||
                tipoDocu.SelectedItem == null)
            {
                MessageBox.Show("Por favor, completá todos los campos antes de continuar.",
                    "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Se crea un objeto con los valores del Alumno
            Alumno nuevoAlumno = new Alumno
            {
                Nombre = nombre_value.Text.Trim(),
                Apellido = apellido_value.Text.Trim(),
                DNI = documento_value.Text.Trim(),
                Telefono = tel_value.Text.Trim(),
                Email = email_value.Text.Trim(),
                Nacimiento = nacimiento_value.Text.Trim(),
                Apto = apto_value.SelectedItem?.ToString(),
                Genero = genero_value.SelectedItem?.ToString(),
                TipoDocumento = tipoDocu.SelectedItem?.ToString()
            };

            // Se busca si el alumno ya existe
            if (AlumnoYaRegistrado(nuevoAlumno.TipoDocumento, nuevoAlumno.DNI))
            {
                MessageBox.Show("El alumno ya se encuentra registrado en la base de datos.",
                    "Registro duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Si no existe, redirige al Form5
            Form5 opcionesForm = new Form5(nuevoAlumno, connectionString);
            opcionesForm.Show();
            this.Hide();
        }

        // Función que busca al alumno para chequear que no esté registrado
        private bool AlumnoYaRegistrado(string tipoDocumento, string dni)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM usuariosRegistrados WHERE tipo_documento = @tipo AND dni = @dni";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tipo", tipoDocumento);
                    cmd.Parameters.AddWithValue("@dni", dni);

                    try
                    {
                        conn.Open();
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al verificar el alumno: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        private void tel_value_KeyPress(object sender, KeyPressEventArgs e)
        {
            // No permite letras
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

    // Clase Alumno para enviar los datos al siguiente formulario
    public class Alumno
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Nacimiento { get; set; }
        public string Apto { get; set; }
        public string Genero { get; set; }
        public string TipoDocumento { get; set; }
    }
}

