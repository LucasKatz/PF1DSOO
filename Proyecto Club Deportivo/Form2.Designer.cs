namespace Proyecto_Club_Deportivo
{
    partial class formPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            altaSocio = new Button();
            registroPago = new Button();
            randomFunction = new Button();
            SuspendLayout();
            // 
            // altaSocio
            // 
            altaSocio.Location = new Point(114, 91);
            altaSocio.Name = "altaSocio";
            altaSocio.Size = new Size(180, 83);
            altaSocio.TabIndex = 0;
            altaSocio.Text = "Registrar Alumno";
            altaSocio.UseVisualStyleBackColor = true;
            altaSocio.Click += AltaSocio_Click;
            // 
            // registroPago
            // 
            registroPago.Location = new Point(114, 246);
            registroPago.Name = "registroPago";
            registroPago.Size = new Size(180, 83);
            registroPago.TabIndex = 2;
            registroPago.Text = "Registrar Pago";
            registroPago.UseVisualStyleBackColor = true;
            registroPago.Click += RegistroPago_Click;
            // 
            // randomFunction
            // 
            randomFunction.Location = new Point(416, 246);
            randomFunction.Name = "randomFunction";
            randomFunction.Size = new Size(180, 83);
            randomFunction.TabIndex = 3;
            randomFunction.Text = "Random";
            randomFunction.UseVisualStyleBackColor = true;
            randomFunction.Click += RandomFunction_Click;
            // 
            // formPrincipal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(randomFunction);
            Controls.Add(registroPago);
            Controls.Add(altaSocio);
            Name = "formPrincipal";
            Text = "formPrincipal";
            Load += Form2_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button altaSocio;
        private Button registroPago;
        private Button randomFunction;
    }
}