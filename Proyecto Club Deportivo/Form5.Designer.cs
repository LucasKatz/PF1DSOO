namespace Proyecto_Club_Deportivo
{
    partial class Form5
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
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(113, 156);
            button1.Name = "button1";
            button1.Size = new Size(94, 50);
            button1.TabIndex = 0;
            button1.Text = "Registrar Socio";
            button1.UseVisualStyleBackColor = true;
            button1.Click += altaSocio_Click;
            // 
            // button2
            // 
            button2.Location = new Point(412, 156);
            button2.Name = "button2";
            button2.Size = new Size(94, 58);
            button2.TabIndex = 1;
            button2.Text = "Registra No Socio";
            button2.UseVisualStyleBackColor = true;
            button2.Click += altaNoSocio_Click;
            // 
            // button3
            // 
            button3.Location = new Point(412, 351);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 2;
            button3.Text = "Salir";
            button3.UseVisualStyleBackColor = true;
            button3.Click += salida_Click;
            // 
            // Form5
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(626, 450);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Form5";
            Text = "Form5";
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
    }
}