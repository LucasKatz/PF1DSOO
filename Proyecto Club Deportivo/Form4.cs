using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Club_Deportivo
{
    public partial class Form4 : Form


    {
        private string connectionString = "Server=localhost;Database=proyectoclub;Uid=clubuser;Pwd=Grupo12;";


        private void RegistrarSocio()
        {
            // Se pasan a string los valores de cada comboBox
            string nombre = nombre_value.Text.Trim();
            string apellido = apellido_value.Text.Trim();
            string dni = documento_value.Text.Trim();
            string telefono = tel_value.Text.Trim();
            string email = email_value.Text.Trim();
            string nacimiento = nacimiento_value.Text.Trim(); 
            string apto = apto_value.SelectedItem?.ToString(); 
            string genero = genero_value.SelectedItem?.ToString(); 
            string esSocio = socio_value.SelectedItem?.ToString(); 
            string tipoDocumento = tipoDocu.SelectedItem?.ToString();


            // Validación simple
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(dni))
            {
                MessageBox.Show("Por favor completá los campos obligatorios.");
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    int usuarioId = 0;

                    string queryUsuario = @"INSERT INTO usuariosRegistrados
(nombre, apellido, tipo_documento, dni, telefono, email, nacimiento, apto_medico, genero)
VALUES (@nombre, @apellido, @tipo_documento, @dni, @telefono, @email, @nacimiento, @apto, @genero);
SELECT LAST_INSERT_ID();";

                    using (MySqlCommand cmd = new MySqlCommand(queryUsuario, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@apellido", apellido);
                        cmd.Parameters.AddWithValue("@dni", dni);
                        cmd.Parameters.AddWithValue("@telefono", telefono);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@tipo_documento", tipoDocumento ?? "DNI");

                        if (DateTime.TryParse(nacimiento, out DateTime fechaNacimiento))
                            cmd.Parameters.AddWithValue("@nacimiento", fechaNacimiento);
                        else
                            cmd.Parameters.AddWithValue("@nacimiento", DBNull.Value);

                        cmd.Parameters.AddWithValue("@apto", apto ?? "NO");
                        cmd.Parameters.AddWithValue("@genero", genero ?? "OTRO");

                        // Ejecuta la inserción y obtiene el ID del usuario recién insertado
                        usuarioId = Convert.ToInt32(cmd.ExecuteScalar());

                        MessageBox.Show("✅ Usuario registrado exitosamente.", "Registro completado",
               MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Limpia los campos después de registrar
                        nombre_value.Clear();
                        apellido_value.Clear();
                        documento_value.Clear();
                        tel_value.Clear();
                        email_value.Clear();
                        nacimiento_value.Clear();
                        apto_value.SelectedIndex = 0;
                        genero_value.SelectedIndex = 0;
                        socio_value.SelectedIndex = 0;
                        tipoDocu.SelectedIndex = 0;
                    }

                    // Insertar en la tabla correspondiente
                    if (esSocio == "SI")
                    {
                        using (MySqlCommand cmdSocio = new MySqlCommand("INSERT INTO Socios(usuario_id) VALUES (@usuario_id)", conn))
                        {
                            cmdSocio.Parameters.AddWithValue("@usuario_id", usuarioId);
                            cmdSocio.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (MySqlCommand cmdNoSocio = new MySqlCommand("INSERT INTO NoSocios(usuario_id) VALUES (@usuario_id)", conn))
                        {
                            cmdNoSocio.Parameters.AddWithValue("@usuario_id", usuarioId);
                            cmdNoSocio.ExecuteNonQuery();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }



        public Form4()
        {
            InitializeComponent();

            socio_value.Items.Add("SI");
            socio_value.Items.Add("NO");
            socio_value.SelectedIndex = 0;
            socio_value.DropDownStyle = ComboBoxStyle.DropDownList;

            apto_value.Items.Add("SI");
            apto_value.Items.Add("NO");
            apto_value.SelectedIndex = 0;
            apto_value.DropDownStyle = ComboBoxStyle.DropDownList;

            genero_value.Items.Add("F");
            genero_value.Items.Add("M");
            genero_value.Items.Add("X");
            genero_value.SelectedIndex = 0;
            genero_value.DropDownStyle = ComboBoxStyle.DropDownList;


            tipoDocu.Items.Add("DNI");
            tipoDocu.Items.Add("LE");
            tipoDocu.Items.Add("LC");
            tipoDocu.Items.Add("DU");
            tipoDocu.Items.Add("PASAPORTE");

            tipoDocu.SelectedIndex = 0;  // selecciona el primero por defecto
            tipoDocu.DropDownStyle = ComboBoxStyle.DropDownList;  

        }


        private void nombre_value_TextChanged(object sender, EventArgs e)
        {

        }




       
        private void socio_value_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seleccion = socio_value.SelectedItem.ToString();


        }



        private void registro_Click(object sender, EventArgs e)
        {

            RegistrarSocio();
        }

        private void tel_value_TextChanged(object sender, EventArgs e)
        {

        }

        private void tel_value_KeyPress(object sender, KeyPressEventArgs e)
        {
            // No permite letras
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        private void email_value_TextChanged(object sender, EventArgs e)
        {

        }

        private void nacimiento_value_TextChanged(object sender, EventArgs e)
        {

        }

        private void apto_value_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void genero_value_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tipoDocu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

}
