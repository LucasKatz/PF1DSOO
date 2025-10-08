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
            pagoActividad = new Button();
            pagoCuota = new Button();
            listarDeudores = new Button();
            inscripcionAct = new Button();
            button1 = new Button();
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
            // pagoActividad
            // 
            pagoActividad.Location = new Point(416, 246);
            pagoActividad.Name = "pagoActividad";
            pagoActividad.Size = new Size(180, 83);
            pagoActividad.TabIndex = 3;
            pagoActividad.Text = "Pagar Actividad";
            pagoActividad.UseVisualStyleBackColor = true;
            pagoActividad.Click += RandomFunction_Click;
            // 
            // pagoCuota
            // 
            pagoCuota.Location = new Point(416, 91);
            pagoCuota.Name = "pagoCuota";
            pagoCuota.Size = new Size(180, 83);
            pagoCuota.TabIndex = 4;
            pagoCuota.Text = "Pagar Cuota";
            pagoCuota.UseVisualStyleBackColor = true;
            pagoCuota.Click += pagoCuota_Click;
            // 
            // listarDeudores
            // 
            listarDeudores.Location = new Point(688, 246);
            listarDeudores.Name = "listarDeudores";
            listarDeudores.Size = new Size(180, 83);
            listarDeudores.TabIndex = 5;
            listarDeudores.Text = "Listado de Deudores";
            listarDeudores.UseVisualStyleBackColor = true;
            listarDeudores.Click += listarDeudores_Click;
            // 
            // inscripcionAct
            // 
            inscripcionAct.Location = new Point(688, 91);
            inscripcionAct.Name = "inscripcionAct";
            inscripcionAct.Size = new Size(180, 83);
            inscripcionAct.TabIndex = 6;
            inscripcionAct.Text = "Inscribirse a Actividad";
            inscripcionAct.UseVisualStyleBackColor = true;
            inscripcionAct.Click += inscripcionAct_Click;
            // 
            // button1
            // 
            button1.Location = new Point(943, 356);
            button1.Name = "button1";
            button1.Size = new Size(122, 47);
            button1.TabIndex = 7;
            button1.Text = "Salir";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // formPrincipal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1086, 415);
            Controls.Add(button1);
            Controls.Add(inscripcionAct);
            Controls.Add(listarDeudores);
            Controls.Add(pagoCuota);
            Controls.Add(pagoActividad);
            Controls.Add(registroPago);
            Controls.Add(altaSocio);
            Name = "formPrincipal";
            Text = "formPrincipal";
            ResumeLayout(false);
        }

        #endregion

        private Button altaSocio;
        private Button registroPago;
        private Button pagoActividad;
        private Button pagoCuota;
        private Button listarDeudores;
        private Button inscripcionAct;
        private Button button1;
    }
}