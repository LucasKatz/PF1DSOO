using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace Proyecto_Club_Deportivo
{
    public partial class registroCuota : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

        public registroCuota()
        {
            InitializeComponent();

            tipoDocu.Items.Add("DNI");
            tipoDocu.Items.Add("LE");
            tipoDocu.Items.Add("LC");
            tipoDocu.Items.Add("DU");
            tipoDocu.Items.Add("PASAPORTE");
            tipoDocu.SelectedIndex = 0;
            tipoDocu.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        // 📌 Cargar actividades al abrir el formulario
        private void registroCuota_Load(object sender, EventArgs e)
        {
            CargarActividades();
        }

        private void CargarActividades()
        {
            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();
                    string query = "SELECT id, nombre, precio FROM Actividades";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        comboActividad.DataSource = dt;
                        comboActividad.DisplayMember = "nombre";
                        comboActividad.ValueMember = "id";
                        comboActividad.SelectedIndex = -1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar actividades: " + ex.Message);
                }
            }
        }

        // 📌 Mostrar precio automáticamente al seleccionar una actividad
        private void comboActividad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboActividad.SelectedValue == null) return;

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();
                    string query = "SELECT precio FROM Actividades WHERE id = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@id", comboActividad.SelectedValue);
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                            txtPrecio.Text = Convert.ToDecimal(result).ToString("0.00");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener precio: " + ex.Message);
                }
            }
        }

        // 📌 Buscar usuario en la base de datos
        private void buscar_Click(object sender, EventArgs e)
        {
            string tipo = tipoDocu.SelectedItem.ToString();
            string numero = docuValue.Text.Trim();

            if (string.IsNullOrEmpty(numero))
            {
                MessageBox.Show("Por favor ingrese el número de documento.");
                return;
            }

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();

                    string query = @"SELECT u.id, u.nombre, u.apellido,
                                            CASE WHEN s.usuario_id IS NOT NULL THEN 'SI' ELSE 'NO' END AS socio
                                     FROM usuariosRegistrados u
                                     LEFT JOIN Socios s ON u.id = s.usuario_id
                                     WHERE u.tipo_documento = @tipo AND u.dni = @dni;";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@tipo", tipo);
                        cmd.Parameters.AddWithValue("@dni", numero);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtNombre.Text = reader["nombre"].ToString();
                                txtApellido.Text = reader["apellido"].ToString();
                                txtSocio.Text = reader["socio"].ToString();
                                txtUserID.Text = reader["id"].ToString(); // Campo oculto o no visible
                            }
                            else
                            {
                                MessageBox.Show("No se encontró ningún usuario con esos datos.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar el usuario: " + ex.Message);
                }
            }
        }

        // 📌 Registrar el pago en la base de datos
        private void btnRegistrarPago_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserID.Text))
            {
                MessageBox.Show("Debe buscar un usuario antes de registrar un pago.");
                return;
            }

            if (comboActividad.SelectedValue == null)
            {
                MessageBox.Show("Seleccione una actividad.");
                return;
            }

            decimal precio;
            if (!decimal.TryParse(txtPrecio.Text, out precio))
            {
                MessageBox.Show("Precio inválido.");
                return;
            }

            int usuarioId = Convert.ToInt32(txtUserID.Text);
            int actividadId = Convert.ToInt32(comboActividad.SelectedValue);
            DateTime fechaPago = DateTime.Now;

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();

                    string insertQuery = @"INSERT INTO Pagos (usuario_id, actividad_id, monto, fecha_pago)
                                           VALUES (@usuario_id, @actividad_id, @monto, @fecha_pago);";

                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conexion))
                    {
                        cmd.Parameters.AddWithValue("@usuario_id", usuarioId);
                        cmd.Parameters.AddWithValue("@actividad_id", actividadId);
                        cmd.Parameters.AddWithValue("@monto", precio);
                        cmd.Parameters.AddWithValue("@fecha_pago", fechaPago);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Pago registrado correctamente.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al registrar el pago: " + ex.Message);
                }
            }
        }


    }
}
