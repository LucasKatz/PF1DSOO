namespace Proyecto_Club_Deportivo
{
    partial class Form4
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
            nombre_value = new TextBox();
            documento_value = new TextBox();
            apellido_value = new TextBox();
            nombre = new Label();
            apellido = new Label();
            dni = new Label();
            socio = new Label();
            registro = new Button();
            telefono = new Label();
            tel_value = new TextBox();
            apto_value = new ComboBox();
            genero_value = new ComboBox();
            Apto = new Label();
            email = new Label();
            email_value = new TextBox();
            nacimiento = new Label();
            nacimiento_value = new TextBox();
            Género = new Label();
            tipoDocu = new ComboBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // nombre_value
            // 
            nombre_value.Location = new Point(339, 78);
            nombre_value.Name = "nombre_value";
            nombre_value.Size = new Size(226, 27);
            nombre_value.TabIndex = 0;
            // 
            // documento_value
            // 
            documento_value.Location = new Point(526, 177);
            documento_value.Name = "documento_value";
            documento_value.Size = new Size(226, 27);
            documento_value.TabIndex = 1;
            // 
            // apellido_value
            // 
            apellido_value.Location = new Point(339, 126);
            apellido_value.Name = "apellido_value";
            apellido_value.Size = new Size(226, 27);
            apellido_value.TabIndex = 2;
            // 
            // nombre
            // 
            nombre.AutoSize = true;
            nombre.ForeColor = SystemColors.ButtonHighlight;
            nombre.Location = new Point(132, 78);
            nombre.Name = "nombre";
            nombre.Size = new Size(64, 20);
            nombre.TabIndex = 3;
            nombre.Text = "Nombre";
            // 
            // apellido
            // 
            apellido.AutoSize = true;
            apellido.ForeColor = SystemColors.ButtonHighlight;
            apellido.Location = new Point(129, 126);
            apellido.Name = "apellido";
            apellido.Size = new Size(66, 20);
            apellido.TabIndex = 4;
            apellido.Text = "Apellido";
            // 
            // dni
            // 
            dni.AutoSize = true;
            dni.ForeColor = SystemColors.ButtonHighlight;
            dni.Location = new Point(132, 176);
            dni.Name = "dni";
            dni.Size = new Size(39, 20);
            dni.TabIndex = 5;
            dni.Text = "Tipo";
            // 
            // socio
            // 
            socio.Location = new Point(0, 0);
            socio.Name = "socio";
            socio.Size = new Size(100, 23);
            socio.TabIndex = 23;
            // 
            // registro
            // 
            registro.Location = new Point(357, 569);
            registro.Name = "registro";
            registro.Size = new Size(94, 29);
            registro.TabIndex = 9;
            registro.Text = "Registrar";
            registro.UseVisualStyleBackColor = true;
            registro.Click += registro_Click;
            // 
            // telefono
            // 
            telefono.AutoSize = true;
            telefono.ForeColor = SystemColors.ButtonHighlight;
            telefono.Location = new Point(132, 237);
            telefono.Name = "telefono";
            telefono.Size = new Size(67, 20);
            telefono.TabIndex = 10;
            telefono.Text = "Teléfono";
            // 
            // tel_value
            // 
            tel_value.Location = new Point(339, 237);
            tel_value.Name = "tel_value";
            tel_value.Size = new Size(226, 27);
            tel_value.TabIndex = 11;
            tel_value.KeyPress += tel_value_KeyPress;
            // 
            // apto_value
            // 
            apto_value.FormattingEnabled = true;
            apto_value.Location = new Point(339, 397);
            apto_value.Name = "apto_value";
            apto_value.Size = new Size(151, 28);
            apto_value.TabIndex = 12;
            // 
            // genero_value
            // 
            genero_value.FormattingEnabled = true;
            genero_value.Location = new Point(339, 457);
            genero_value.Name = "genero_value";
            genero_value.Size = new Size(151, 28);
            genero_value.TabIndex = 13;
            // 
            // Apto
            // 
            Apto.AutoSize = true;
            Apto.ForeColor = SystemColors.ButtonHighlight;
            Apto.Location = new Point(132, 400);
            Apto.Name = "Apto";
            Apto.Size = new Size(83, 20);
            Apto.TabIndex = 14;
            Apto.Text = "Apto Físico";
            // 
            // email
            // 
            email.AutoSize = true;
            email.ForeColor = SystemColors.ButtonHighlight;
            email.Location = new Point(132, 291);
            email.Name = "email";
            email.Size = new Size(46, 20);
            email.TabIndex = 15;
            email.Text = "Email";
            // 
            // email_value
            // 
            email_value.Location = new Point(339, 284);
            email_value.Name = "email_value";
            email_value.Size = new Size(226, 27);
            email_value.TabIndex = 16;
            // 
            // nacimiento
            // 
            nacimiento.AutoSize = true;
            nacimiento.ForeColor = SystemColors.ButtonHighlight;
            nacimiento.Location = new Point(132, 338);
            nacimiento.Name = "nacimiento";
            nacimiento.Size = new Size(149, 20);
            nacimiento.TabIndex = 17;
            nacimiento.Text = "Fecha de Nacimiento";
            // 
            // nacimiento_value
            // 
            nacimiento_value.Location = new Point(339, 338);
            nacimiento_value.Name = "nacimiento_value";
            nacimiento_value.PlaceholderText = "dd/mm/aaaa";
            nacimiento_value.Size = new Size(226, 27);
            nacimiento_value.TabIndex = 18;
            // 
            // Género
            // 
            Género.AutoSize = true;
            Género.ForeColor = SystemColors.ButtonHighlight;
            Género.Location = new Point(132, 457);
            Género.Name = "Género";
            Género.Size = new Size(57, 20);
            Género.TabIndex = 19;
            Género.Text = "Género";
            // 
            // tipoDocu
            // 
            tipoDocu.FormattingEnabled = true;
            tipoDocu.Location = new Point(339, 176);
            tipoDocu.Name = "tipoDocu";
            tipoDocu.Size = new Size(151, 28);
            tipoDocu.TabIndex = 20;
            // 
            // button1
            // 
            button1.Location = new Point(656, 619);
            button1.Name = "button1";
            button1.Size = new Size(122, 47);
            button1.TabIndex = 21;
            button1.Text = "Salir";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(800, 687);
            Controls.Add(button1);
            Controls.Add(tipoDocu);
            Controls.Add(Género);
            Controls.Add(nacimiento_value);
            Controls.Add(nacimiento);
            Controls.Add(email_value);
            Controls.Add(email);
            Controls.Add(Apto);
            Controls.Add(genero_value);
            Controls.Add(apto_value);
            Controls.Add(tel_value);
            Controls.Add(telefono);
            Controls.Add(registro);
            Controls.Add(socio);
            Controls.Add(dni);
            Controls.Add(apellido);
            Controls.Add(nombre);
            Controls.Add(apellido_value);
            Controls.Add(documento_value);
            Controls.Add(nombre_value);
            Name = "Form4";
            Text = "Form4";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Label nombre;
        private Label apellido;
        private Label dni;
        private Label socio;
        private ComboBox comboBox1;
        private Button registro;
        private Label telefono;
        private TextBox textBox4;
        private ComboBox comboBox2;
        private ComboBox comboBox3;
        private Label Apto;
        private Label email;
        private TextBox textBox5;
        private Label nacimiento;
        private TextBox textBox6;
        private Label Género;
        private TextBox nombre_value;
        private TextBox documento_value;
        private TextBox apellido_value;
        private TextBox tel_value;
        private ComboBox apto_value;
        private ComboBox genero_value;
        private TextBox email_value;
        private TextBox nacimiento_value;
        private ComboBox tipoDocu;
        private Button button1;
    }
}