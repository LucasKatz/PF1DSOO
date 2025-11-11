namespace Proyecto_Club_Deportivo
{
    partial class registroCuota
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
            TipoDocuRegistroPago = new Label();
            tipoDocu = new ComboBox();
            docuValue = new TextBox();
            buscar = new Button();
            Nombre = new Label();
            Apellido = new Label();
            Socio = new Label();
            txtNombre = new TextBox();
            txtApellido = new TextBox();
            txtSocio = new TextBox();
            concepto = new Label();
            comboActividad = new ComboBox();
            precio = new Label();
            txtPrecio = new TextBox();
            btnRegistrarPago = new Button();
            txtUserID = new TextBox();
            button1 = new Button();
            button2 = new Button();
            carnet = new Button();
            metodo = new Label();
            metodoPago = new ComboBox();
            comboCuotas = new ComboBox();
            cuotas = new Label();
            descuento = new Label();
            label1 = new Label();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // TipoDocuRegistroPago
            // 
            TipoDocuRegistroPago.AutoSize = true;
            TipoDocuRegistroPago.ForeColor = SystemColors.ButtonHighlight;
            TipoDocuRegistroPago.Location = new Point(229, 106);
            TipoDocuRegistroPago.Name = "TipoDocuRegistroPago";
            TipoDocuRegistroPago.Size = new Size(39, 20);
            TipoDocuRegistroPago.TabIndex = 0;
            TipoDocuRegistroPago.Text = "Tipo";
            // 
            // tipoDocu
            // 
            tipoDocu.FormattingEnabled = true;
            tipoDocu.Location = new Point(333, 108);
            tipoDocu.Name = "tipoDocu";
            tipoDocu.Size = new Size(151, 28);
            tipoDocu.TabIndex = 1;
            // 
            // docuValue
            // 
            docuValue.Location = new Point(555, 107);
            docuValue.Name = "docuValue";
            docuValue.Size = new Size(176, 27);
            docuValue.TabIndex = 2;
            // 
            // buscar
            // 
            buscar.Location = new Point(801, 107);
            buscar.Name = "buscar";
            buscar.Size = new Size(94, 29);
            buscar.TabIndex = 3;
            buscar.Text = "Buscar";
            buscar.UseVisualStyleBackColor = true;
            buscar.Click += buscar_Click;
            // 
            // Nombre
            // 
            Nombre.AutoSize = true;
            Nombre.ForeColor = SystemColors.ButtonHighlight;
            Nombre.Location = new Point(230, 165);
            Nombre.Name = "Nombre";
            Nombre.Size = new Size(64, 20);
            Nombre.TabIndex = 4;
            Nombre.Text = "Nombre";
            // 
            // Apellido
            // 
            Apellido.AutoSize = true;
            Apellido.ForeColor = SystemColors.ButtonHighlight;
            Apellido.Location = new Point(228, 220);
            Apellido.Name = "Apellido";
            Apellido.Size = new Size(66, 20);
            Apellido.TabIndex = 5;
            Apellido.Text = "Apellido";
            // 
            // Socio
            // 
            Socio.AutoSize = true;
            Socio.ForeColor = SystemColors.ButtonHighlight;
            Socio.Location = new Point(230, 278);
            Socio.Name = "Socio";
            Socio.Size = new Size(46, 20);
            Socio.TabIndex = 6;
            Socio.Text = "Socio";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(333, 165);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(176, 27);
            txtNombre.TabIndex = 7;
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(333, 220);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(176, 27);
            txtApellido.TabIndex = 8;
            // 
            // txtSocio
            // 
            txtSocio.Location = new Point(333, 278);
            txtSocio.Name = "txtSocio";
            txtSocio.Size = new Size(176, 27);
            txtSocio.TabIndex = 9;
            // 
            // concepto
            // 
            concepto.AutoSize = true;
            concepto.ForeColor = SystemColors.ButtonHighlight;
            concepto.Location = new Point(229, 341);
            concepto.Name = "concepto";
            concepto.Size = new Size(126, 20);
            concepto.TabIndex = 10;
            concepto.Text = "Concepto a Pagar";
            // 
            // comboActividad
            // 
            comboActividad.FormattingEnabled = true;
            comboActividad.Location = new Point(409, 341);
            comboActividad.Name = "comboActividad";
            comboActividad.Size = new Size(151, 28);
            comboActividad.TabIndex = 11;
            comboActividad.SelectedIndexChanged += comboActividad_SelectedIndexChanged;
            // 
            // precio
            // 
            precio.AutoSize = true;
            precio.ForeColor = SystemColors.ButtonHighlight;
            precio.Location = new Point(230, 403);
            precio.Name = "precio";
            precio.Size = new Size(50, 20);
            precio.TabIndex = 12;
            precio.Text = "Precio";
            // 
            // txtPrecio
            // 
            txtPrecio.Location = new Point(333, 403);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(176, 27);
            txtPrecio.TabIndex = 13;
            txtPrecio.TextChanged += comboActividad_SelectedIndexChanged;
            // 
            // btnRegistrarPago
            // 
            btnRegistrarPago.Location = new Point(601, 509);
            btnRegistrarPago.Name = "btnRegistrarPago";
            btnRegistrarPago.Size = new Size(190, 29);
            btnRegistrarPago.TabIndex = 14;
            btnRegistrarPago.Text = "Registrar Pago";
            btnRegistrarPago.UseVisualStyleBackColor = true;
            btnRegistrarPago.Click += btnRegistrarPago_Click_1;
            // 
            // txtUserID
            // 
            txtUserID.Location = new Point(555, 165);
            txtUserID.Name = "txtUserID";
            txtUserID.Size = new Size(176, 27);
            txtUserID.TabIndex = 15;
            txtUserID.Visible = false;
            // 
            // button1
            // 
            button1.Location = new Point(1082, 631);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 16;
            button1.Text = "Salir";
            button1.UseVisualStyleBackColor = true;
       
            // 
            // button2
            // 
            button2.Location = new Point(945, 631);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 17;
            button2.Text = "Volver";
            button2.UseVisualStyleBackColor = true;
        
            // 
            // carnet
            // 
            carnet.Location = new Point(601, 564);
            carnet.Name = "carnet";
            carnet.Size = new Size(190, 29);
            carnet.TabIndex = 18;
            carnet.Text = "Generar Carnet";
            carnet.UseVisualStyleBackColor = true;
            carnet.Click += carnet_Click;
            // 
            // metodo
            // 
            metodo.AutoSize = true;
            metodo.ForeColor = SystemColors.ButtonHighlight;
            metodo.Location = new Point(717, 348);
            metodo.Name = "metodo";
            metodo.Size = new Size(120, 20);
            metodo.TabIndex = 19;
            metodo.Text = "Método de Pago";
            // 
            // metodoPago
            // 
            metodoPago.FormattingEnabled = true;
            metodoPago.Location = new Point(888, 348);
            metodoPago.Name = "metodoPago";
            metodoPago.Size = new Size(194, 28);
            metodoPago.TabIndex = 20;
            metodoPago.SelectedIndexChanged += metodoPago_SelectedIndexChanged;
            // 
            // comboCuotas
            // 
            comboCuotas.FormattingEnabled = true;
            comboCuotas.Items.AddRange(new object[] { "1 3 6" });
            comboCuotas.Location = new Point(888, 400);
            comboCuotas.Name = "comboCuotas";
            comboCuotas.Size = new Size(194, 28);
            comboCuotas.TabIndex = 21;
            // 
            // cuotas
            // 
            cuotas.AutoSize = true;
            cuotas.ForeColor = SystemColors.ButtonHighlight;
            cuotas.Location = new Point(717, 403);
            cuotas.Name = "cuotas";
            cuotas.Size = new Size(54, 20);
            cuotas.TabIndex = 22;
            cuotas.Text = "Cuotas";
            // 
            // descuento
            // 
            descuento.AutoSize = true;
            descuento.ForeColor = SystemColors.ButtonHighlight;
            descuento.Location = new Point(230, 467);
            descuento.Name = "descuento";
            descuento.Size = new Size(152, 20);
            descuento.TabIndex = 23;
            descuento.Text = "Precio con Descuento";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(589, 285);
            label1.Name = "label1";
            label1.Size = new Size(153, 20);
            label1.TabIndex = 24;
            label1.Text = "Fecha de Habilitación";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(785, 285);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(176, 27);
            textBox1.TabIndex = 25;
            // 
            // registroCuota
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1359, 698);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(descuento);
            Controls.Add(cuotas);
            Controls.Add(comboCuotas);
            Controls.Add(metodoPago);
            Controls.Add(metodo);
            Controls.Add(carnet);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(txtUserID);
            Controls.Add(btnRegistrarPago);
            Controls.Add(txtPrecio);
            Controls.Add(precio);
            Controls.Add(comboActividad);
            Controls.Add(concepto);
            Controls.Add(txtSocio);
            Controls.Add(txtApellido);
            Controls.Add(txtNombre);
            Controls.Add(Socio);
            Controls.Add(Apellido);
            Controls.Add(Nombre);
            Controls.Add(buscar);
            Controls.Add(docuValue);
            Controls.Add(tipoDocu);
            Controls.Add(TipoDocuRegistroPago);
            Name = "registroCuota";
            Text = "Registro Pago";
            Load += registroCuota_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label TipoDocuRegistroPago;
        private ComboBox tipoDocu;
        private TextBox docuValue;
        private Button buscar;
        private Label Nombre;
        private Label Apellido;
        private Label Socio;
        private TextBox txtNombre;
        private TextBox txtApellido;
        private TextBox txtSocio;
        private Label concepto;
        private ComboBox comboActividad;
        private Label precio;
        private TextBox txtPrecio;
        private Button btnRegistrarPago;
        private TextBox txtUserID;
        private Button button1;
        private Button button2;
        private Button carnet;
        private Label metodo;
        private ComboBox metodoPago;
        private ComboBox comboCuotas;
        private Label cuotas;
        private Label descuento;
        private Label label1;
        private TextBox textBox1;
    }
}