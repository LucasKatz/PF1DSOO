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
using static System.Runtime.InteropServices.JavaScript.JSType;
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

            // Inicializar valores
            txtPrecio.Text = "0.00";
            comboActividad.Enabled = false; // Deshabilitado hasta que haya usuario válido
            carnet.Visible = false;

            // Metodo de Pago
            metodoPago.Items.Clear();
            metodoPago.Items.Add("Efectivo");
            metodoPago.Items.Add("Transferencia");
            metodoPago.Items.Add("Tarjeta");
            metodoPago.DropDownStyle = ComboBoxStyle.DropDownList;
            metodoPago.SelectedIndex = 0;

            // --- Configurar comboCuotas (se muestra solo si se selecciona TARJETA como metodo de pago) ---
            if (comboCuotas != null)
            {
                comboCuotas.Items.Clear();
                comboCuotas.Items.Add("1");
                comboCuotas.Items.Add("3");
                comboCuotas.Items.Add("6");
                comboCuotas.DropDownStyle = ComboBoxStyle.DropDownList;
                comboCuotas.SelectedIndex = -1;
                comboCuotas.Visible = false;
            }
        }

        // Se cargan las actividades al abrir el formulario
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
                    // Si NO es socio, se excluye la opción CUOTA de las actividaes
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

        // El precio se muestra automaticamente al seleccionar la actividad, el precio inicial por defecto es 0
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

                        if (result != null)
                            txtPrecio.Text = Convert.ToDecimal(result).ToString("0.00");
                        else
                            txtPrecio.Text = "0.00";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener precio: " + ex.Message);
                }
            }
        }

        // El botón busca el usuario en la base de datos por tipo y N° de documento
        private void buscar_Click(object sender, EventArgs e)
        {
            // Obtener tipo y número de documento
            string tipo = tipoDocu.SelectedItem != null ? tipoDocu.SelectedItem.ToString() : "";
            string numero = docuValue.Text.Trim();

            // Validación de campos vacíos
            if (string.IsNullOrEmpty(tipo) || string.IsNullOrEmpty(numero))
            {
                MessageBox.Show("Por favor, seleccione el tipo de documento e ingrese el número antes de buscar.",
                                "Campos incompletos",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
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
                                // Se cargan datos en los campos
                                txtNombre.Text = reader["nombre"].ToString();
                                txtApellido.Text = reader["apellido"].ToString();
                                txtSocio.Text = reader["socio"].ToString();
                                txtUserID.Text = reader["id"].ToString();

                                // Se bloquean campos para que no puedan ser modificados manualmente
                                txtNombre.ReadOnly = true;
                                txtApellido.ReadOnly = true;
                                txtSocio.ReadOnly = true;
                                txtUserID.ReadOnly = true;

                                comboActividad.Enabled = true;

                                // Se muestra o se oculta el botón de carnet según si es socio
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
                                // Aviso de usuario no encontrado
                                MessageBox.Show("No se encontró ninguna persona registrada con los datos ingresados.",
                                                "Usuario no registrado",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Information);

                                // 🔹 Limpiar campos y bloquear controles
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
                    MessageBox.Show("Error al buscar el usuario: " + ex.Message,
                                    "Error de conexión",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }

        // Se carga  solo la opción "Cuota mensual"
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

        private DateTime CalcularProximaClase(MySqlConnection conexion, int actividadId, DateTime fechaActual)
        {
            string query = "SELECT horario FROM Actividades WHERE id = @id";

            using (MySqlCommand cmd = new MySqlCommand(query, conexion))
            {
                cmd.Parameters.AddWithValue("@id", actividadId);
                string horario = cmd.ExecuteScalar()?.ToString();

                if (string.IsNullOrEmpty(horario))
                    throw new Exception("No se encontró el horario de la actividad.");

                string[] dias = new string[] { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" };

                List<DayOfWeek> diasClase = new List<DayOfWeek>();
                foreach (var dia in dias)
                {
                    if (horario.Contains(dia, StringComparison.OrdinalIgnoreCase))
                    {
                        diasClase.Add(ConvertirDia(dia));
                    }
                }

                if (diasClase.Count == 0)
                    throw new Exception("No se pudieron identificar días válidos en el horario.");

                DateTime proximaClase = fechaActual.Date;
                // buscamos la próxima fecha que coincida con algún día de la actividad
                for (int i = 1; i <= 14; i++) // la función tiene un limite 2 semanas para evitar loop infinito
                {
                    proximaClase = proximaClase.AddDays(1);
                    if (diasClase.Contains(proximaClase.DayOfWeek))
                        return proximaClase;
                }

                // Si no se encuentra en 14 días, devolvemos +7 días como fallback
                return fechaActual.AddDays(7);
            }
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

        // Evento para cuando cambia el método de pago
        private void metodoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metodoPago.SelectedItem == null)
                return;

            string metodo = metodoPago.SelectedItem.ToString();

            if (metodo.Equals("Tarjeta", StringComparison.OrdinalIgnoreCase))
            {
                // Se muestra la cantidad de cuotas posibles
                if (comboCuotas != null)
                {
                    comboCuotas.Visible = true;
                    comboCuotas.SelectedIndex = 0; //Por defecto se sugiere 1 sola cuota al iniciar
                }
            }
            else
            {
                
                if (comboCuotas != null)
                {
                    comboCuotas.SelectedIndex = -1;
                    comboCuotas.Visible = false;
                }
            }

            // Si se selecciona Efectivo, recalcula precio en pantalla mostrando descuento
            if (decimal.TryParse(txtPrecio.Text, out decimal precioBase))
            {
                decimal precioMostrado = precioBase;
                if (metodo.Equals("Efectivo", StringComparison.OrdinalIgnoreCase))
                {
                    precioMostrado = Math.Round(precioBase * 0.90m, 2); // 10% off
                }
                // Muestra el precio con dos decimales
                txtPrecio.Text = precioMostrado.ToString("0.00");
            }
        }

        //  Registrar el pago
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
            DateTime fechaPago = DateTime.Now;
            DateTime fechaVencimiento;

            string metodoSeleccionado = metodoPago.SelectedItem != null ? metodoPago.SelectedItem.ToString() : "Efectivo";
            int? cuotas = null;
            if (metodoSeleccionado.Equals("Tarjeta", StringComparison.OrdinalIgnoreCase) && comboCuotas != null && comboCuotas.SelectedItem != null)
            {
                if (int.TryParse(comboCuotas.SelectedItem.ToString(), out int c)) cuotas = c;
            }

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();

                    // Validación de pago vigente
                    string lastPagoQuery = @"SELECT fecha_pago FROM Pagos 
                                             WHERE usuario_id = @usuario_id AND actividad_id = @actividad_id
                                             ORDER BY fecha_pago DESC LIMIT 1;";
                    using (MySqlCommand cmdLast = new MySqlCommand(lastPagoQuery, conexion))
                    {
                        cmdLast.Parameters.AddWithValue("@usuario_id", usuarioId);
                        cmdLast.Parameters.AddWithValue("@actividad_id", actividadId);

                        object lastObj = cmdLast.ExecuteScalar();
                        if (lastObj != null && lastObj != DBNull.Value)
                        {
                            DateTime lastPagoDate = Convert.ToDateTime(lastObj);
                            DateTime proximoVencimiento;
                            string nombreActividad = comboActividad.Text ?? "";

                            if (nombreActividad.ToUpper().Contains("CUOTA"))
                            {
                                proximoVencimiento = lastPagoDate.AddMonths(1);
                            }
                            else
                            {
                                // Se calcula a partir de la última fecha de pago
                                proximoVencimiento = CalcularProximaClase(conexion, actividadId, lastPagoDate);
                            }

                            if (proximoVencimiento > DateTime.Now)
                            {
                                MessageBox.Show($"Ya existe un pago vigente. El próximo vencimiento es {proximoVencimiento:dd/MM/yyyy}. No se puede registrar otro pago hasta esa fecha.",
                                                "Pago ya registrado",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }

                    // 2) Determinar vencimiento para este pago (se incluye en el comprobante)
                    if (comboActividad.Text.ToUpper().Contains("CUOTA"))
                        fechaVencimiento = fechaPago.AddMonths(1);
                    else
                        fechaVencimiento = CalcularProximaClase(conexion, actividadId, fechaPago);

                    //  3) Aplicar descuento si es efectivo (10%)
                    decimal montoFinal = precioIngresado;
                    if (metodoSeleccionado.Equals("Efectivo", StringComparison.OrdinalIgnoreCase))
                    {
                        
                        montoFinal = precioIngresado;
                    }

                    // Se cargan los datos del pago en la BB DD
                    string insertQuery = @"INSERT INTO Pagos (usuario_id, actividad_id, monto, fecha_pago, metodo, cuotas)
                                           VALUES (@usuario_id, @actividad_id, @monto, @fecha_pago, @metodo, @cuotas);";

                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conexion))
                    {
                        cmd.Parameters.AddWithValue("@usuario_id", usuarioId);
                        cmd.Parameters.AddWithValue("@actividad_id", actividadId);
                        cmd.Parameters.AddWithValue("@monto", montoFinal);
                        cmd.Parameters.AddWithValue("@fecha_pago", fechaPago);
                        cmd.Parameters.AddWithValue("@metodo", metodoSeleccionado);
                        if (cuotas.HasValue)
                            cmd.Parameters.AddWithValue("@cuotas", cuotas.Value);
                        else
                            cmd.Parameters.AddWithValue("@cuotas", DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }

                    //  5) Se genera el comprobante incluyendo metodo y cuotas
                    GenerarComprobantePDF(
                        txtNombre.Text,
                        txtApellido.Text,
                        tipoDocu.SelectedItem.ToString(),
                        docuValue.Text,
                        montoFinal.ToString("0.00"),
                        comboActividad.Text,
                        fechaPago,
                        fechaVencimiento,
                        metodoSeleccionado,
                        cuotas
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
            this.Close();

            formPrincipal principal = Application.OpenForms["formPrincipal"] as formPrincipal;
            if (principal != null)
            {
                principal.Show();
            }
            else
            {
                principal = new formPrincipal();
                principal.Show();
            }
        }


        //Función que genera el comprobante de pago en PDF
        private void GenerarComprobantePDF(string nombre, string apellido, string tipoDoc, string numeroDoc,
                                     string monto, string concepto, DateTime fechaPago, DateTime fechaVencimiento,
                                     string metodoPagoComprobante, int? cuotas)
        {
            try
            {
                string carpetaComprobantes = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    "Comprobantes"
                );

                if (!Directory.Exists(carpetaComprobantes))
                    Directory.CreateDirectory(carpetaComprobantes);

                string nombreArchivo = $"Comprobante_{nombre}_{apellido}_{fechaPago:yyyyMMdd_HHmmss}.pdf";
                string rutaArchivo = Path.Combine(carpetaComprobantes, nombreArchivo);

                Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
                PdfWriter.GetInstance(doc, new FileStream(rutaArchivo, FileMode.Create));
                doc.Open();

                Paragraph titulo = new Paragraph("Comprobante de Pago",
                    new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 18, iTextSharp.text.Font.BOLD));
                titulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(titulo);
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph(" "));

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

                
                tabla.AddCell("Método de pago:");
                tabla.AddCell(metodoPagoComprobante ?? "-");

                if (metodoPagoComprobante != null && metodoPagoComprobante.Equals("Tarjeta", StringComparison.OrdinalIgnoreCase))
                {
                    tabla.AddCell("Cuotas:");
                    tabla.AddCell(cuotas.HasValue ? cuotas.Value.ToString() : "-");
                }

                if (!concepto.Equals("Cuota", StringComparison.OrdinalIgnoreCase) && !concepto.ToUpper().Contains("CUOTA"))
                {
                    tabla.AddCell("Vencimiento:");
                    tabla.AddCell(fechaVencimiento.ToString("dd/MM/yyyy"));
                }

                doc.Add(tabla);
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph("Gracias por su pago.",
                    new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.ITALIC)));

                doc.Close();

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

        private void GenerarCarnetPDF(string nombre, string apellido, string numeroSocio, DateTime fechaAlta)
        {
            try
            {
                string carpetaCarnets = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    "Carnets"
                );

                if (!Directory.Exists(carpetaCarnets))
                    Directory.CreateDirectory(carpetaCarnets);


                string nombreArchivo = $"Carnet_{nombre}_{apellido}_{fechaAlta:yyyyMMdd_HHmmss}.pdf";
                string rutaArchivo = Path.Combine(carpetaCarnets, nombreArchivo);

                Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
                PdfWriter.GetInstance(doc, new FileStream(rutaArchivo, FileMode.Create));
                doc.Open();

                Paragraph titulo = new Paragraph("Carnet de Socio",
                    new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 18, iTextSharp.text.Font.BOLD));
                titulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(titulo);
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph(" "));

                PdfPTable tabla = new PdfPTable(2);
                tabla.WidthPercentage = 100;

                tabla.AddCell("Nombre:");
                tabla.AddCell(nombre);
                tabla.AddCell("Apellido:");
                tabla.AddCell(apellido);
                tabla.AddCell("N° de Socio:");
                tabla.AddCell(numeroSocio);
                tabla.AddCell("Fecha de Alta:");
                tabla.AddCell(fechaAlta.ToString("dd/MM/yyyy"));

                doc.Add(tabla);
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph("Bienvenido al club.",
                    new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.ITALIC)));

                doc.Close();

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

        private void carnet_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string apellido = txtApellido.Text;
            string numeroSocio = txtUserID.Text;
            DateTime fechaAlta = DateTime.Now;

            GenerarCarnetPDF(nombre, apellido, numeroSocio, fechaAlta);
        }



   

        private void comboMetodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string metodo = comboCuotas.SelectedItem.ToString();

            if (metodo == "Tarjeta")
            {
                comboCuotas.Items.Clear();
                comboCuotas.Items.Add("1");
                comboCuotas.Items.Add("3");
                comboCuotas.Items.Add("6");
                comboCuotas.Visible = true;
            }
            else
            {
                comboCuotas.Visible = false;
            }
        }
    }
}
