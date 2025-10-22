

using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;

namespace Proyecto_Club_Deportivo
{
    public partial class FormActividades : Form
    {
        // Cadena de conexión desde App.config
        private string connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

        public FormActividades()
        {
            InitializeComponent();

            // Conectar el evento Load
            this.Load += FormActividades_Load;
        }

        // Evento Load
        private void FormActividades_Load(object sender, EventArgs e)
        {
            // Primero validar conexión
            if (ProbarConexion())
            {
                CargarActividades();
            }
        }

        // Método para probar la conexión
        private bool ProbarConexion()
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(connectionString))
                {
                    conexion.Open();
                    // MessageBox.Show("✅ Conexión exitosa a la base de datos");
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error de conexión: " + ex.Message);
                return false;
            }
        }

        // Método para cargar actividades en el DataGridView
        private void CargarActividades()
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(connectionString))
                {
                    conexion.Open();

                    string query = @"SELECT  nombre, rango_min, rango_max, cupoTotal, cupoDisponible, horario, precio 
                                     FROM Actividades;";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);


                    //MessageBox.Show("Filas encontradas: " + dt.Rows.Count);

                    dgvActividades.DataSource = dt;
                    dgvActividades.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    dgvActividades.Columns["rango_min"].HeaderText = "Edad Mínima";
                    dgvActividades.Columns["rango_max"].HeaderText = "Edad Máxima";

                    // Estilos
                    dgvActividades.BackgroundColor = Color.White;
                    dgvActividades.EnableHeadersVisualStyles = true;
                    // Permitir múltiples líneas en las celdas
                    dgvActividades.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    dgvActividades.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las actividades: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

