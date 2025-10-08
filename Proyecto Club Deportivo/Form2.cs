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

        private void AltaSocio_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
        }

        private void RegistroPago_Click(object sender, EventArgs e)
        {

        }

        private void RandomFunction_Click(object sender, EventArgs e)
        {

        }

        private void pagoCuota_Click(object sender, EventArgs e)
        {

        }

        private void inscripcionAct_Click(object sender, EventArgs e)
        {

        }

        private void listarDeudores_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
