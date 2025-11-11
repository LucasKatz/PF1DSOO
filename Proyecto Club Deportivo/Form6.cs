using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static Proyecto_Club_Deportivo.ingresoForm;
using System.Collections.Generic;

namespace Proyecto_Club_Deportivo
{
    public partial class registroCuota : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

        public registroCuota()
        {
            InitializeComponent();

            // Tipos de documento
            tipoDocu.Items.Add("DNI");
            tipoDocu.Items.Add("LE");
            tipoDocu.Items.Add("LC");
            tipoDocu.Items.Add("DU");
            tipoDocu.Items.Add("PASAPORTE");
            tipoDocu.SelectedIndex = 0;
            tipoDocu.DropDownStyle = ComboBoxStyle.DropDownList;

            txtPrecio.Text = "0.00";
            comboActividad.Enabled = false;
            carnet.Visible = false;

            metodoPago.Items.Clear();
            metodoPago.Items.Add("Transferencia");
            metodoPago.Items.Add("Efectivo");
            metodoPago.Items.Add("Tarjeta");
            metodoPago.DropDownStyle = ComboBoxStyle.DropDownList;
            metodoPago.SelectedIndex = 0;

            comboCuotas.Items.Clear();
            comboCuotas.DropDownStyle = ComboBoxStyle.DropDownList;
            comboCuotas.SelectedIndex = -1;
            comboCuotas.Visible = false;
        }

        private void registroCuota_Load(object sender, EventArgs e)
        {
            CargarActividades();
            CargarCuotasDisponibles();
        }

        private void CargarCuotasDisponibles()
        {
            var cuotasDisponibles = new List<Cuota>
            {
                new Cuota { Cantidad = 3, Interes = 0.05m, Descripcion = "3 cuotas (5% interés)" },
                new Cuota { Cantidad = 6, Interes = 0.10m, Descripcion = "6 cuotas (10% interés)" }
            };

            comboCuotas.DataSource = cuotasDisponibles;
            comboCuotas.DisplayMember = "Descripcion";
            comboCuotas.ValueMember = "Cantidad";
        }

        private void CargarActividades()
        {
            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();
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

        private void comboActividad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboActividad.SelectedValue == null)
            {
                txtPrecio.Text = "0.00";
                return;
            }

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
                        txtPrecio.Text = result != null ? Convert.ToDecimal(result).ToString("0.00") : "0.00";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener precio: " + ex.Message);
                }
            }
        }

        private void buscar_Click(object sender, EventArgs e)
        {
            string tipo = tipoDocu.SelectedItem?.ToString() ?? "";
            string numero = docuValue.Text.Trim();

            if (string.IsNullOrEmpty(tipo) || string.IsNullOrEmpty(numero))
            {
                MessageBox.Show("Complete tipo y número de documento antes de buscar.");
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
                                    WHERE u.tipo_documento = @tipo AND u.numDocumento = @numDocumento;";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@tipo", tipo);
                        cmd.Parameters.AddWithValue("@numDocumento", numero);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtNombre.Text = reader["nombre"].ToString();
                                txtApellido.Text = reader["apellido"].ToString();
                                txtSocio.Text = reader["socio"].ToString();
                                txtUserID.Text = reader["id"].ToString();

                                comboActividad.Enabled = true;

                                if (reader["socio"].ToString().Trim().ToUpper() == "SI")
                                {
                                    CargarSoloCuota();
                                    carnet.Visible = true;
                                }
                                else
                                {
                                    CargarActividades();
                                    carnet.Visible = false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Usuario no encontrado.");
                                txtNombre.Clear();
                                txtApellido.Clear();
                                txtSocio.Clear();
                                txtUserID.Clear();
                                comboActividad.Enabled = false;
                                carnet.Visible = false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar usuario: " + ex.Message);
                }
            }
        }

        private void CargarSoloCuota()
        {
            comboActividad.DataSource = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("nombre", typeof(string));
            dt.Columns.Add("precio", typeof(decimal));
            dt.Rows.Add(11, "Cuota mensual", 12000);
            comboActividad.DataSource = dt;
            comboActividad.DisplayMember = "nombre";
            comboActividad.ValueMember = "id";
            comboActividad.SelectedIndex = 0;
            txtPrecio.Text = "12000";
        }

        private DayOfWeek ConvertirDia(string dia)
        {
            return dia.ToLower() switch
            {
                "lunes" => DayOfWeek.Monday,
                "martes" => DayOfWeek.Tuesday,
                "miércoles" or "miercoles" => DayOfWeek.Wednesday,
                "jueves" => DayOfWeek.Thursday,
                "viernes" => DayOfWeek.Friday,
                "sábado" or "sabado" => DayOfWeek.Saturday,
                "domingo" => DayOfWeek.Sunday,
                _ => throw new ArgumentException($"Día inválido: {dia}")
            };
        }

        private void metodoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metodoPago.SelectedItem == null) return;

            string metodo = metodoPago.SelectedItem.ToString();
            comboCuotas.Visible = metodo.Equals("Tarjeta", StringComparison.OrdinalIgnoreCase);

            if (decimal.TryParse(txtPrecio.Text, out decimal precioBase))
            {
                decimal precioMostrado = precioBase;

                if (metodo.Equals("Efectivo", StringComparison.OrdinalIgnoreCase))
                {
                    precioMostrado = Math.Round(precioBase * 0.90m, 2);
                    descuento.Visible = true;
                    descuento.Text = $"Descuento aplicado (-10%): ${precioMostrado:0.00}";
                }
                else
                {
                    descuento.Visible = false;
                }

                txtPrecio.Text = precioMostrado.ToString("0.00");
            }
        }

        public class Cuota
        {
            public int Cantidad { get; set; }
            public decimal Interes { get; set; }
            public string Descripcion { get; set; }
            public override string ToString() => Descripcion;
        }

        private void comboCuotas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboCuotas.SelectedItem is Cuota cuotaSeleccionada && decimal.TryParse(txtPrecio.Text, out decimal precioBase))
            {
                decimal precioFinal = precioBase * (1 + cuotaSeleccionada.Interes);
                txtPrecio.Text = precioFinal.ToString("0.00");
                MessageBox.Show($"Pago en {cuotaSeleccionada.Cantidad} cuotas con {cuotaSeleccionada.Interes * 100}% de interés.\nMonto total: ${precioFinal:0.00}");
            }
        }

        // ======= CLASE PAGO =======
        public class Pago
        {
            public int UsuarioId { get; set; }
            public int ActividadId { get; set; }
            public decimal Monto { get; set; }
            public DateTime FechaPago { get; set; }
            public DateTime FechaVencimiento { get; set; }
            public string Metodo { get; set; }
            public int? Cuotas { get; set; }
            public string Concepto { get; set; }

            public void InsertarEnBD(MySqlConnection conexion)
            {
                string insertQuery = @"INSERT INTO Pagos (usuario_id, actividad_id, monto, fecha_pago, metodo, cuotas)
                                       VALUES (@usuario_id, @actividad_id, @monto, @fecha_pago, @metodo, @cuotas);";

                using (MySqlCommand cmd = new MySqlCommand(insertQuery, conexion))
                {
                    cmd.Parameters.AddWithValue("@usuario_id", UsuarioId);
                    cmd.Parameters.AddWithValue("@actividad_id", ActividadId);
                    cmd.Parameters.AddWithValue("@monto", Monto);
                    cmd.Parameters.AddWithValue("@fecha_pago", FechaPago);
                    cmd.Parameters.AddWithValue("@metodo", Metodo);
                    cmd.Parameters.AddWithValue("@cuotas", Cuotas.HasValue ? (object)Cuotas.Value : DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ======= REGISTRAR PAGO =======
        private void btnRegistrarPago_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserID.Text))
            {
                MessageBox.Show("Debe buscar una persona antes de registrar un pago.");
                return;
            }

            if (comboActividad.SelectedValue == null)
            {
                MessageBox.Show("Seleccione una actividad.");
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text, out decimal precioIngresado))
            {
                MessageBox.Show("Precio inválido.");
                return;
            }

            int usuarioId = Convert.ToInt32(txtUserID.Text);
            int actividadId = Convert.ToInt32(comboActividad.SelectedValue);
            string metodoSeleccionado = metodoPago.SelectedItem?.ToString() ?? "Efectivo";
            int? cuotas = comboCuotas?.SelectedItem is Cuota c ? c.Cantidad : null;
            DateTime fechaPago = DateTime.Now;
            DateTime fechaVencimiento;

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();

                    // Verificar último pago
                    string lastPagoQuery = @"SELECT fecha_pago FROM Pagos 
                                             WHERE usuario_id = @usuario_id AND actividad_id = @actividad_id
                                             ORDER BY fecha_pago DESC LIMIT 1;";
                    using (MySqlCommand cmd = new MySqlCommand(lastPagoQuery, conexion))
                    {
                        cmd.Parameters.AddWithValue("@usuario_id", usuarioId);
                        cmd.Parameters.AddWithValue("@actividad_id", actividadId);
                        object lastObj = cmd.ExecuteScalar();

                        if (lastObj != null && lastObj != DBNull.Value)
                        {
                            DateTime lastPagoDate = Convert.ToDateTime(lastObj);
                            DateTime proximoVencimiento = comboActividad.Text.ToUpper().Contains("CUOTA")
                                ? lastPagoDate.AddMonths(1)
                                : lastPagoDate.AddDays(7);

                            if (proximoVencimiento > DateTime.Now)
                            {
                                MessageBox.Show($"Ya existe un pago vigente. Próximo vencimiento: {proximoVencimiento:dd/MM/yyyy}");
                                return;
                            }
                        }
                    }

                    fechaVencimiento = comboActividad.Text.ToUpper().Contains("CUOTA") ? fechaPago.AddMonths(1) : fechaPago.AddDays(7);

                    decimal montoFinal = metodoSeleccionado.Equals("Efectivo", StringComparison.OrdinalIgnoreCase)
                        ? Math.Round(precioIngresado * 0.90m, 2)
                        : precioIngresado;

                    Pago pago = new Pago
                    {
                        UsuarioId = usuarioId,
                        ActividadId = actividadId,
                        Monto = montoFinal,
                        FechaPago = fechaPago,
                        FechaVencimiento = fechaVencimiento,
                        Metodo = metodoSeleccionado,
                        Cuotas = cuotas,
                        Concepto = comboActividad.Text
                    };

                    pago.InsertarEnBD(conexion);

                    GenerarComprobantePDF(txtNombre.Text, txtApellido.Text, tipoDocu.SelectedItem.ToString(),
                        docuValue.Text, montoFinal.ToString("0.00"), pago.Concepto,
                        pago.FechaPago, pago.FechaVencimiento, pago.Metodo, pago.Cuotas);

                    MessageBox.Show("Pago registrado y comprobante generado correctamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al registrar el pago: " + ex.Message);
                }
            }
        }

        // ========== PDF comprobante ==========
        private void GenerarComprobantePDF(string nombre, string apellido, string tipoDoc, string numeroDoc,
                                           string monto, string concepto, DateTime fechaPago, DateTime fechaVencimiento,
                                           string metodoPagoComprobante, int? cuotas)
        {
            try
            {
                string carpeta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Comprobantes");
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);

                string nombreArchivo = $"Comprobante_{nombre}_{apellido}_{fechaPago:yyyyMMdd_HHmmss}.pdf";
                string rutaArchivo = Path.Combine(carpeta, nombreArchivo);

                Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
                PdfWriter.GetInstance(doc, new FileStream(rutaArchivo, FileMode.Create));
                doc.Open();

                Paragraph titulo = new Paragraph("Comprobante de Pago",
                    new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 18, iTextSharp.text.Font.BOLD))
                { Alignment = Element.ALIGN_CENTER };
                doc.Add(titulo);
                doc.Add(new Paragraph(" "));

                PdfPTable tabla = new PdfPTable(2) { WidthPercentage = 100 };
                tabla.AddCell("Nombre:"); tabla.AddCell(nombre);
                tabla.AddCell("Apellido:"); tabla.AddCell(apellido);
                tabla.AddCell("Documento:"); tabla.AddCell($"{tipoDoc} {numeroDoc}");
                tabla.AddCell("Monto:"); tabla.AddCell($"$ {monto}");
                tabla.AddCell("Concepto:"); tabla.AddCell(concepto);
                tabla.AddCell("Fecha de pago:"); tabla.AddCell(fechaPago.ToString("dd/MM/yyyy"));
                tabla.AddCell("Método:"); tabla.AddCell(metodoPagoComprobante);

                if (metodoPagoComprobante.Equals("Tarjeta", StringComparison.OrdinalIgnoreCase))
                {
                    tabla.AddCell("Cuotas:");
                    tabla.AddCell(cuotas.HasValue ? cuotas.Value.ToString() : "-");
                }

                tabla.AddCell("Vencimiento:"); tabla.AddCell(fechaVencimiento.ToString("dd/MM/yyyy"));
                doc.Add(tabla);
                doc.Add(new Paragraph("\nGracias por su pago.",new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.ITALIC)));

                doc.Close();

                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = rutaArchivo,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar PDF: " + ex.Message);
            }
        }

        private void carnet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtUserID.Text))
            {
                MessageBox.Show("Debe completar los datos antes de generar el carnet.");
                return;
            }

            GenerarCarnetPDF(txtNombre.Text, txtApellido.Text, txtUserID.Text, DateTime.Now);
        }

        private void GenerarCarnetPDF(string nombre, string apellido, string numeroSocio, DateTime fechaAlta)
        {
            try
            {
                string carpeta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Carnets");
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);

                string nombreArchivo = $"Carnet_{nombre}_{apellido}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
                string ruta = Path.Combine(carpeta, nombreArchivo);

                iTextSharp.text.Rectangle tamaño = new iTextSharp.text.Rectangle(250, 150);
                using (FileStream fs = new FileStream(ruta, FileMode.Create))
                using (Document doc = new Document(tamaño, 5, 5, 5, 5))
                {
                    PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                    doc.Open();

                    PdfContentByte fondo = writer.DirectContentUnder;
                    fondo.SetColorFill(new iTextSharp.text.BaseColor(173, 216, 230));
                    fondo.Rectangle(0, 0, tamaño.Width, tamaño.Height);
                    fondo.Fill();

                    var fuenteTitulo = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE);

                    var fuenteCampo = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);

                    var fuenteDato = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);

                    PdfPTable tablaPrincipal = new PdfPTable(2);
                    tablaPrincipal.TotalWidth = tamaño.Width - 10;
                    tablaPrincipal.LockedWidth = true;
                    tablaPrincipal.SetWidths(new float[] { 1f, 2f });
                    tablaPrincipal.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;


                    PdfPCell celdaFoto = new PdfPCell(new Phrase("Foto 4x4", fuenteDato))
                    {
                        FixedHeight = 80,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE,
                        Border = iTextSharp.text.Rectangle.NO_BORDER,
                        BackgroundColor = new BaseColor(220, 220, 220)
                    };
                    tablaPrincipal.AddCell(celdaFoto);

                    PdfPTable tablaDatos = new PdfPTable(2);
                    tablaDatos.WidthPercentage = 100;
                    tablaDatos.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;


                    PdfPCell celdaTitulo = new PdfPCell(new Phrase("Club Deportivo 29", fuenteTitulo))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        Border = iTextSharp.text.Rectangle.NO_BORDER,
                        BackgroundColor = new BaseColor(0, 102, 204),
                        Padding = 3
                    };
                    tablaDatos.AddCell(celdaTitulo);
                    tablaDatos.AddCell(new Phrase("Nombre", fuenteDato));
                    tablaDatos.AddCell(new Phrase(nombre, fuenteCampo));
                    tablaDatos.AddCell(new Phrase("Apellido", fuenteDato));
                    tablaDatos.AddCell(new Phrase(apellido, fuenteCampo));
                    tablaDatos.AddCell(new Phrase("N° Socio", fuenteDato));
                    tablaDatos.AddCell(new Phrase(numeroSocio, fuenteCampo));
                    tablaDatos.AddCell(new Phrase("Desde", fuenteDato));
                    tablaDatos.AddCell(new Phrase(fechaAlta.ToString("dd/MM/yyyy"), fuenteCampo));

                    PdfPCell celdaDatos = new PdfPCell(tablaDatos) { Border = iTextSharp.text.Rectangle.NO_BORDER
                    };
                    tablaPrincipal.AddCell(celdaDatos);

                    doc.Add(tablaPrincipal);
                    doc.Close();
                }

                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = ruta,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar carnet PDF: " + ex.Message);
            }
        }
    }
}

