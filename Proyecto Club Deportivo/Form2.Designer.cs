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
            pagoActividad = new Button();
            pagoCuota = new Button();
            listarDeudores = new Button();
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
            altaSocio.Click += RegistrarAlumno_Click;
            // 
            // pagoActividad
            // 
            pagoActividad.Location = new Point(416, 226);
            pagoActividad.Name = "pagoActividad";
            pagoActividad.Size = new Size(180, 83);
            pagoActividad.TabIndex = 3;
            pagoActividad.Text = "Registrar Actividad";
            pagoActividad.UseVisualStyleBackColor = true;
            pagoActividad.Click += RegistrarActividad_Click;
            // 
            // pagoCuota
            // 
            pagoCuota.Location = new Point(416, 91);
            pagoCuota.Name = "pagoCuota";
            pagoCuota.Size = new Size(180, 83);
            pagoCuota.TabIndex = 4;
            pagoCuota.Text = "Registrar Pago";
            pagoCuota.UseVisualStyleBackColor = true;
            pagoCuota.Click += pagoCuota_Click;
            // 
            // listarDeudores
            // 
            listarDeudores.Location = new Point(114, 226);
            listarDeudores.Name = "listarDeudores";
            listarDeudores.Size = new Size(180, 83);
            listarDeudores.TabIndex = 5;
            listarDeudores.Text = "Listar Vencimientos";
            listarDeudores.UseVisualStyleBackColor = true;
            listarDeudores.Click += listarDeudores_Click;
            // 
            // button1
            // 
            button1.Location = new Point(474, 356);
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
            ClientSize = new Size(698, 415);
            Controls.Add(button1);
            Controls.Add(listarDeudores);
            Controls.Add(pagoCuota);
            Controls.Add(pagoActividad);
            Controls.Add(altaSocio);
            Name = "formPrincipal";
            Text = "formPrincipal";
            ResumeLayout(false);
        }

        #endregion

        private Button altaSocio;
        private Button pagoActividad;
        private Button pagoCuota;
        private Button listarDeudores;
        private Button button1;
    }
}