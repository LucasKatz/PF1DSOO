namespace Proyecto_Club_Deportivo
{
    partial class Form7
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
            dgvActividades = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvActividades).BeginInit();
            SuspendLayout();
            // 
            // dgvActividades
            // 
            dgvActividades.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvActividades.Location = new Point(-1, 3);
            dgvActividades.Name = "dgvActividades";
            dgvActividades.RowHeadersWidth = 51;
            dgvActividades.Size = new Size(801, 381);
            dgvActividades.TabIndex = 1;
            //dgvActividades.CellContentClick += dgvActividades_CellContentClick;
            // 
            // Form7
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvActividades);
            Name = "Form7";
            Text = "Form7";
            ((System.ComponentModel.ISupportInitialize)dgvActividades).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvActividades;
    }
}