using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace Proyecto_Club_Deportivo
{
    public partial class Form7 : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

        public Form7()
        {
            InitializeComponent();
            CargarUsuariosConVencimientoHoy();
        }

        private void CargarUsuariosConVencimientoHoy()
        {
            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();

                    // 🗓️ Consulta: usuarios cuyos pagos vencen HOY
                    string query = @"
SELECT 
    u.id AS UsuarioID,
    u.nombre AS Nombre,
    u.apellido AS Apellido,
    a.nombre AS Actividad,
    p.monto AS Monto,
    DATE(p.fecha_vencimiento) AS FechaVencimiento
FROM Pagos p
INNER JOIN usuariosRegistrados u ON p.usuario_id = u.id
LEFT JOIN Actividades a ON p.actividad_id = a.id
WHERE DATE(p.fecha_vencimiento) = CURDATE();
";


                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvActividades.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los datos: " + ex.Message);
                }
            }
        }


    }
}
