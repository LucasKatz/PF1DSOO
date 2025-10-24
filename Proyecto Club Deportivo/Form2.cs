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
    public partial class formPrincipal : Form
    {
        public formPrincipal()
        {
            InitializeComponent();
        }


        //Boton que nos lleva al formulario para registrar al usuario
        private void RegistrarAlumno_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
        }


        //Boton que nos muestra el listado de actividades con sus respectivos datos
        private void RegistrarActividad_Click(object sender, EventArgs e)
        {
            FormActividades form = new FormActividades();
            form.ShowDialog();
        }

        //Boton que nos lleva al formulario de registro de pagos

        private void pagoCuota_Click(object sender, EventArgs e)
        {
            registroCuota form = new registroCuota();
            form.ShowDialog();
        }

        //Boton que nos muestra el listado de deudores

        private void listarDeudores_Click(object sender, EventArgs e)
        {
            Form7 form = new Form7();
            form.ShowDialog();
        }

        //Boton de salida de aplicación
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
