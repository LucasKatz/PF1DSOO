using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using iTextSharp.text;
using System.Drawing;
using iTextSharp.text.pdf;
using System.IO;


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
                    // 📌 Excluimos la actividad 'CUOTA' o 'Cuota mensual' según el nombre en la base
                    string query = "SELECT id, nombre, precio FROM Actividades WHERE UPPER(nombre) <> 'CUOTA' AND UPPER(nombre) <> 'CUOTA MENSUAL'";

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
                                txtUserID.Text = reader["id"].ToString();

                                // 📌 Ahora aplicamos el filtro según si es socio o no
                                if (reader["socio"].ToString() == "SI")
                                {
                                    CargarSoloCuota(); // Muestra solo "Cuota mensual"
                                }
                                else
                                {
                                    CargarActividades(); // Muestra todas las actividades
                                }
                            }
                            else
                            {
                                MessageBox.Show("No se encontró ningún usuario con esos datos.");
                                LimpiarCampos();
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

        // 📌 Cargar solo la opción "Cuota mensual"
        private void CargarSoloCuota()
        {
            comboActividad.DataSource = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("nombre", typeof(string));
            dt.Columns.Add("precio", typeof(decimal));

            // Supongamos que la cuota mensual cuesta 5000 (puedes reemplazarlo con un valor real)
            dt.Rows.Add(0, "Cuota mensual", 5000);

            comboActividad.DataSource = dt;
            comboActividad.DisplayMember = "nombre";
            comboActividad.ValueMember = "id";
            comboActividad.SelectedIndex = 0;

            txtPrecio.Text = "5000"; // Muestra precio predeterminado
        }

        // 📌 Limpiar campos del formulario (opcional)
        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtSocio.Clear();
            txtUserID.Clear();
            comboActividad.DataSource = null;
            txtPrecio.Clear();
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

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
            {
                MessageBox.Show("Precio inválido.");
                return;
            }

            int usuarioId = Convert.ToInt32(txtUserID.Text);
            int actividadId = Convert.ToInt32(comboActividad.SelectedValue);
            DateTime fechaPago = DateTime.Now;
            DateTime fechaVencimiento = fechaPago.AddMonths(1); // Ejemplo: vence en 1 mes

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
                    }

                    // ✅ Después del registro, generar el PDF
                    GenerarComprobantePDF(
                        txtNombre.Text,
                        txtApellido.Text,
                        tipoDocu.SelectedItem.ToString(),
                        docuValue.Text,
                        precio.ToString("0.00"),
                        comboActividad.Text,
                        fechaPago,
                        fechaVencimiento
                    );

                    MessageBox.Show("Pago registrado correctamente. Se generó el comprobante en formato PDF.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al registrar el pago: " + ex.Message);
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
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


        private void GenerarComprobantePDF(string nombre, string apellido, string tipoDoc, string numeroDoc,
                                     string monto, string concepto, DateTime fechaPago, DateTime fechaVencimiento)
        {
            try
            {
                // 📂 Carpeta de destino: Escritorio\Comprobantes
                string carpetaComprobantes = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    "Comprobantes"
                );

                // Crear carpeta si no existe
                if (!Directory.Exists(carpetaComprobantes))
                    Directory.CreateDirectory(carpetaComprobantes);

                // 🧾 Nombre del archivo (con fecha y hora)
                string nombreArchivo = $"Comprobante_{nombre}_{apellido}_{fechaPago:yyyyMMdd_HHmmss}.pdf";
                string rutaArchivo = Path.Combine(carpetaComprobantes, nombreArchivo);

                // 📄 Crear el documento PDF
                Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
                PdfWriter.GetInstance(doc, new FileStream(rutaArchivo, FileMode.Create));
                doc.Open();

                // 🏷️ Título centrado
                Paragraph titulo = new Paragraph("Comprobante de Pago",
                    new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 18, iTextSharp.text.Font.BOLD));
                titulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(titulo);
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph(" "));

                // 📋 Tabla con los datos del pago
                PdfPTable tabla = new PdfPTable(2);
                tabla.WidthPercentage = 100;

                tabla.AddCell("Nombre:");
                tabla.AddCell(nombre);
                tabla.AddCell("Apellido:");
                tabla.AddCell(apellido);
                tabla.AddCell("Tipo y N° de documento:");
                tabla.AddCell($"{tipoDoc} {numeroDoc}");
                tabla.AddCell("Monto pagado:");
                tabla.AddCell($"$ {monto}");
                tabla.AddCell("Concepto pagado:");
                tabla.AddCell(concepto);
                tabla.AddCell("Fecha de pago:");
                tabla.AddCell(fechaPago.ToString("dd/MM/yyyy"));
                tabla.AddCell("Vencimiento:");
                tabla.AddCell(fechaVencimiento.ToString("dd/MM/yyyy"));

                doc.Add(tabla);

                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph("Gracias por su pago.",
                    new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.ITALIC)));

                doc.Close();

                // ✅ Abrir automáticamente el PDF
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = rutaArchivo,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el PDF: " + ex.Message);
            }
        }


    }
}
