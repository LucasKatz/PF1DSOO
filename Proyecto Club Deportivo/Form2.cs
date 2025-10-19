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



        private void RegistrarActividad_Click(object sender, EventArgs e)
        {
            FormActividades form = new FormActividades();
            form.ShowDialog();
        }

        private void pagoCuota_Click(object sender, EventArgs e)
        {
            registroCuota form = new registroCuota();
            form.ShowDialog();
        }



        private void listarDeudores_Click(object sender, EventArgs e)
        {

        }

        //Boton de salida de aplicación
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
