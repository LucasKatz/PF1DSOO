namespace Proyecto_Club_Deportivo
{
    partial class ingresoForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            botonIngresar = new Button();
            botonLimpiar = new Button();
            botonSalir = new Button();
            Usuario = new Label();
            Password = new Label();
            textUsuario = new TextBox();
            textPassword = new TextBox();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // botonIngresar
            // 
            botonIngresar.Location = new Point(237, 310);
            botonIngresar.Name = "botonIngresar";
            botonIngresar.Size = new Size(162, 54);
            botonIngresar.TabIndex = 0;
            botonIngresar.Text = "Ingresar";
            botonIngresar.UseVisualStyleBackColor = true;
            botonIngresar.Click += botonIngresar_Click;
            // 
            // botonLimpiar
            // 
            botonLimpiar.Location = new Point(237, 401);
            botonLimpiar.Name = "botonLimpiar";
            botonLimpiar.Size = new Size(162, 53);
            botonLimpiar.TabIndex = 1;
            botonLimpiar.Text = "Limpiar";
            botonLimpiar.UseVisualStyleBackColor = true;
            botonLimpiar.Click += botonLimpiar_Click;
            // 
            // botonSalir
            // 
            botonSalir.Location = new Point(237, 503);
            botonSalir.Name = "botonSalir";
            botonSalir.Size = new Size(162, 53);
            botonSalir.TabIndex = 2;
            botonSalir.Text = "Salir";
            botonSalir.UseVisualStyleBackColor = true;
            botonSalir.Click += botonSalir_Click;
            // 
            // Usuario
            // 
            Usuario.AutoSize = true;
            Usuario.ForeColor = SystemColors.ButtonHighlight;
            Usuario.Location = new Point(130, 161);
            Usuario.Name = "Usuario";
            Usuario.Size = new Size(59, 20);
            Usuario.TabIndex = 3;
            Usuario.Text = "Usuario";
            Usuario.UseMnemonic = false;
            // 
            // Password
            // 
            Password.AutoSize = true;
            Password.ForeColor = SystemColors.ButtonHighlight;
            Password.Location = new Point(130, 215);
            Password.Name = "Password";
            Password.Size = new Size(70, 20);
            Password.TabIndex = 4;
            Password.Text = "Password";
            // 
            // textUsuario
            // 
            textUsuario.Location = new Point(262, 161);
            textUsuario.Name = "textUsuario";
            textUsuario.Size = new Size(125, 27);
            textUsuario.TabIndex = 5;
            // 
            // textPassword
            // 
            textPassword.Location = new Point(262, 215);
            textPassword.Name = "textPassword";
            textPassword.Size = new Size(125, 27);
            textPassword.TabIndex = 6;
            //
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.avatar;
            pictureBox1.Location = new Point(225, 31);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(197, 96);
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
           
            // 
            // ingresoForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(607, 605);
            Controls.Add(pictureBox1);
            Controls.Add(textPassword);
            Controls.Add(textUsuario);
            Controls.Add(Password);
            Controls.Add(Usuario);
            Controls.Add(botonSalir);
            Controls.Add(botonLimpiar);
            Controls.Add(botonIngresar);
            Name = "ingresoForm";
            Text = "Formulario de Ingreso";
     
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button botonIngresar;
        private Button botonLimpiar;
        private Button botonSalir;
        private Label Usuario;
        private Label Password;
        private TextBox textUsuario;
        private TextBox textPassword;
        private PictureBox pictureBox1;
    }
}
