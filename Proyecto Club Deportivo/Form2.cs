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

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void AltaSocio_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
        }

        

        private void AltaNoSocio_Click(object sender, EventArgs e)
        {

        }

        private void RegistroPago_Click(object sender, EventArgs e)
        {

        }

        private void RandomFunction_Click(object sender, EventArgs e)
        {

        }
    }
}
