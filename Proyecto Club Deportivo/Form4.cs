using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Proyecto_Club_Deportivo
{
    public partial class Form4 : Form
    {
        private string connectionString = "Server=localhost;Database=grupo12;Uid=clubuser;Pwd=Grupo12;";

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
            // Validación básica
            if (string.IsNullOrEmpty(nombre_value.Text) ||
                string.IsNullOrEmpty(apellido_value.Text) ||
                string.IsNullOrEmpty(documento_value.Text))
            {
                MessageBox.Show("Por favor completá los campos obligatorios antes de continuar.");
                return;
            }

            // Crear un objeto con los datos del formulario
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

            // Abrir el siguiente formulario y pasarle el alumno
            Form5 opcionesForm = new Form5(nuevoAlumno, connectionString);
            opcionesForm.Show();

            this.Hide();
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

        private void socio_value_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

    // Clase auxiliar para transferir los datos del alumno
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

