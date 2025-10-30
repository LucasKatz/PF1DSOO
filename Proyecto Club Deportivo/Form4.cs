using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Text.RegularExpressions;
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

            // ✅ VALIDACIÓN 1: Teléfono con 10 dígitos
            if (!Regex.IsMatch(tel_value.Text.Trim(), @"^\d{10}$"))
            {
                MessageBox.Show("El número de teléfono debe contener exactamente 10 dígitos.",
                    "Teléfono inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ VALIDACIÓN 2: Formato de email válido
            if (!Regex.IsMatch(email_value.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Por favor, ingresá una dirección de correo válida (por ejemplo: usuario@dominio.com).",
                    "Correo inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ VALIDACIÓN 3: Fecha de nacimiento con formato xx/xx/xxxx
            if (!Regex.IsMatch(nacimiento_value.Text.Trim(), @"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}$"))
            {
                MessageBox.Show("La fecha de nacimiento debe tener el formato DD/MM/AAAA (por ejemplo: 25/10/1990).",
                    "Fecha inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Si todas las validaciones pasan, se crea el objeto Persona
            Persona nuevoPersona = new Persona
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

            // Se busca si la persona ya se encuentra registrada
            if (AlumnoYaRegistrado(nuevoPersona.TipoDocumento, nuevoPersona.DNI))
            {
                MessageBox.Show("La persona ya se encuentra registrada en la base de datos.",
                    "Registro duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Si no existe, redirige al Form5
            Form5 opcionesForm = new Form5(nuevoPersona, connectionString);
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
                        MessageBox.Show("Error al verificar el alumno: " + ex.Message,
                            "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close(); // Cierra el form actual

            // Busca si formPrincipal sigue abierto en memoria
            formPrincipal principal = Application.OpenForms["formPrincipal"] as formPrincipal;

            if (principal != null)
            {
                principal.Show();
            }
            else
            {
                // Si por alguna razón no estaba abierto, lo crea de nuevo
                principal = new formPrincipal();
                principal.Show();
            }
        }
    }

    // Clase Persona para enviar los datos al siguiente formulario
    public class Persona
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
