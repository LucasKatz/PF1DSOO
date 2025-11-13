using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic; // para Interaction.InputBox

namespace Proyecto_Club_Deportivo.Datos
{
    public class Conexion   // la clase debe ser PUBLICA
    {
        // Declaramos las variables 
        private string baseDatos;
        private string servidor;
        private string puerto;
        private string usuario;
        private string clave;
        private static Conexion? con = null;

        private Conexion()  // asignamos valores a las variables de la conexión
        {
            // variables usadas para la repetición de líneas de código
            bool correcto = false;
            int mensaje;

            // creamos las variables para recibir los datos desde el teclado
            string T_servidor = "Servidor";
            string T_puerto = "Puerto";
            string T_usuario = "Usuario";
            string T_clave = "Clave";    // se antepuso la T para indicar que vienen desde teclado

            while (!correcto)
            {
                // Armamos los cuadros de diálogo para el ingreso de datos
                T_servidor = Interaction.InputBox("Ingrese servidor", "DATOS DE INSTALACIÓN MySQL", T_servidor);
                T_puerto = Interaction.InputBox("Ingrese puerto", "DATOS DE INSTALACIÓN MySQL", T_puerto);
                T_usuario = Interaction.InputBox("Ingrese usuario", "DATOS DE INSTALACIÓN MySQL", T_usuario);
                T_clave = Interaction.InputBox("Ingrese clave", "DATOS DE INSTALACIÓN MySQL", T_clave);

                // Controlamos que los datos ingresados para acceder a MySQL sean correctos
                mensaje = (int)MessageBox.Show(
                    "Su ingreso: SERVIDOR = " + T_servidor +
                    " PUERTO= " + T_puerto +
                    " USUARIO: " + T_usuario +
                    " CLAVE: " + T_clave,
                    "AVISO DEL SISTEMA", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (mensaje != (int)DialogResult.Yes)  // DialogResult.Yes = 6
                {
                    MessageBox.Show("INGRESE NUEVAMENTE LOS DATOS");
                    correcto = false;
                }
                else
                {
                    correcto = true;
                }
            }

            // Reemplazamos los datos concretos que teníamos por las variables
            this.baseDatos = "Grupo12";
            this.servidor = T_servidor; // "localhost"
            this.puerto = T_puerto;   // "3306"
            this.usuario = T_usuario;  // "root"
            this.clave = T_clave;    // ""
        }

        // Proceso de interacción
        public MySqlConnection CrearConexion()
        {
            // instanciamos una conexión
            MySqlConnection cadena = new MySqlConnection();

            // el bloque try permite controlar errores
            try
            {
                cadena.ConnectionString = "datasource=" + this.servidor +
                                           ";port=" + this.puerto +
                                           ";username=" + this.usuario +
                                           ";password=" + this.clave +
                                           ";Database=" + this.baseDatos;
            }
            catch (Exception)
            {
                // Si querés, podés loguear la excepción antes de relanzarla.
                throw;
            }
            return cadena;
        }

        // Para evaluar la instancia de la conectividad
        public static Conexion GetInstancia()
        {
            if (con == null) // quiere decir que la instancia no fue creada aún
            {
                con = new Conexion(); // se crea una nueva
            }
            return con;
        }
    }
}




