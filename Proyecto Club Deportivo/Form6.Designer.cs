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
            SuspendLayout();
            // 
            // TipoDocuRegistroPago
            // 
            TipoDocuRegistroPago.AutoSize = true;
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
            Nombre.Location = new Point(230, 165);
            Nombre.Name = "Nombre";
            Nombre.Size = new Size(64, 20);
            Nombre.TabIndex = 4;
            Nombre.Text = "Nombre";
            // 
            // Apellido
            // 
            Apellido.AutoSize = true;
            Apellido.Location = new Point(228, 220);
            Apellido.Name = "Apellido";
            Apellido.Size = new Size(66, 20);
            Apellido.TabIndex = 5;
            Apellido.Text = "Apellido";
            // 
            // Socio
            // 
            Socio.AutoSize = true;
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
            btnRegistrarPago.Location = new Point(601, 462);
            btnRegistrarPago.Name = "btnRegistrarPago";
            btnRegistrarPago.Size = new Size(94, 29);
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
            // registroCuota
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1359, 698);
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
            Text = "Registro Cuota";
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
    }
}