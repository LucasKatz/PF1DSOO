﻿using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Proyecto_Club_Deportivo
{
    public partial class Form5 : Form
    {
        private Persona persona;
        private string connectionString;

        // Constructor recibe el alumno (datos) y la cadena de conexión
        public Form5(Persona persona, string connectionString)
        {
            InitializeComponent();
            this.persona = persona ?? throw new ArgumentNullException(nameof(persona));
            this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        // Handler del botón "Registrar Socio"
        private void altaSocio_Click(object sender, EventArgs e)
        {
            RegistrarAlumno(true);
        }

        // Handler del botón "Registrar No Socio"
        private void altaNoSocio_Click(object sender, EventArgs e)
        {
            RegistrarAlumno(false);
        }

        // Método que hace la inserción en usuariosRegistrados y luego en Socios/NoSocios
        private void RegistrarAlumno(bool esSocio)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    using (MySqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            // 1) Insertar en usuariosRegistrados (tabla principal)
                            string insertUsuario = @"
INSERT INTO usuariosRegistrados
    (nombre, apellido, tipo_documento, dni, telefono, email, nacimiento, apto_medico, genero)
VALUES
    (@nombre, @apellido, @tipo_documento, @dni, @telefono, @email, @nacimiento, @apto, @genero);";

                            using (MySqlCommand cmd = new MySqlCommand(insertUsuario, conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@nombre", persona.Nombre ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@apellido", persona.Apellido ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@tipo_documento", persona.TipoDocumento ?? "DNI");
                                cmd.Parameters.AddWithValue("@dni", persona.DNI ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@telefono", persona.Telefono ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@email", persona.Email ?? (object)DBNull.Value);

                                if (DateTime.TryParse(persona.Nacimiento, out DateTime fechaNacimiento))
                                    cmd.Parameters.AddWithValue("@nacimiento", fechaNacimiento);
                                else
                                    cmd.Parameters.AddWithValue("@nacimiento", DBNull.Value);

                                cmd.Parameters.AddWithValue("@apto", persona.Apto ?? "NO");
                                cmd.Parameters.AddWithValue("@genero", persona.Genero ?? "X");

                                cmd.ExecuteNonQuery();
                            }

                            // 2) Obtener el id del usuario recién insertado
                            long usuarioId;
                            using (MySqlCommand cmdId = new MySqlCommand("SELECT LAST_INSERT_ID();", conn, tran))
                            {
                                usuarioId = Convert.ToInt64(cmdId.ExecuteScalar());
                            }

                            // 3) Insertar en la tabla correspondiente (Socios o NoSocios)
                            string insertSegundaTabla = esSocio ?
                                "INSERT INTO Socios(usuario_id) VALUES (@usuario_id);" :
                                "INSERT INTO NoSocios(usuario_id) VALUES (@usuario_id);";

                            using (MySqlCommand cmd2 = new MySqlCommand(insertSegundaTabla, conn, tran))
                            {
                                cmd2.Parameters.AddWithValue("@usuario_id", usuarioId);
                                cmd2.ExecuteNonQuery();
                            }

                            // Confirmar transacción
                            tran.Commit();

                            MessageBox.Show(
                                $"Registro completado correctamente.\nID asignado: {usuarioId}\nTipo: {(esSocio ? "SOCIO" : "NO SOCIO")}",
                                "Registro OK",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );

                            // Si abriste Form5 con ShowDialog() en Form4, devolvemos OK para que Form4 continúe
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        catch (Exception exInner)
                        {
                            // Si algo falla, revertimos la transacción
                            try { tran.Rollback(); } catch { }

                            MessageBox.Show("Error al completar el registro: " + exInner.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexión o ejecución: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Salida/cancelación: cerramos el formulario devolviendo DialogResult.Cancel
        private void salida_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

