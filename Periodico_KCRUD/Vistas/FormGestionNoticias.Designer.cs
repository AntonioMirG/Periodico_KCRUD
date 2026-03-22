namespace Periodico_KCRUD.Vistas
{
    partial class FormGestionNoticias
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
            dgvNoticias = new DataGridView();
            txtTitulo = new TextBox();
            txtContenido = new TextBox();
            btnGuardar = new Button();
            btnEliminar = new Button();
            label1 = new Label();
            label2 = new Label();
            btnActualizar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvNoticias).BeginInit();
            SuspendLayout();
            // 
            // dgvNoticias
            // 
            dgvNoticias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNoticias.Location = new Point(26, 51);
            dgvNoticias.Name = "dgvNoticias";
            dgvNoticias.Size = new Size(507, 365);
            dgvNoticias.TabIndex = 0;
            dgvNoticias.CellClick += dgvNoticias_CellClick;
            // 
            // txtTitulo
            // 
            txtTitulo.Location = new Point(564, 78);
            txtTitulo.Name = "txtTitulo";
            txtTitulo.Size = new Size(100, 23);
            txtTitulo.TabIndex = 1;
            // 
            // txtContenido
            // 
            txtContenido.Location = new Point(564, 150);
            txtContenido.Multiline = true;
            txtContenido.Name = "txtContenido";
            txtContenido.Size = new Size(100, 23);
            txtContenido.TabIndex = 2;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(564, 217);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(75, 23);
            btnGuardar.TabIndex = 3;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(658, 217);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(75, 23);
            btnEliminar.TabIndex = 4;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(564, 60);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 5;
            label1.Text = "Titulo";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(564, 132);
            label2.Name = "label2";
            label2.Size = new Size(63, 15);
            label2.TabIndex = 6;
            label2.Text = "Contenido";
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(564, 270);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(75, 23);
            btnActualizar.TabIndex = 7;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click_1;
            // 
            // FormGestionNoticias
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnActualizar);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnEliminar);
            Controls.Add(btnGuardar);
            Controls.Add(txtContenido);
            Controls.Add(txtTitulo);
            Controls.Add(dgvNoticias);
            Name = "FormGestionNoticias";
            Text = "FormGestionNoticias";
            ((System.ComponentModel.ISupportInitialize)dgvNoticias).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvNoticias;
        private TextBox txtTitulo;
        private TextBox txtContenido;
        private Button btnGuardar;
        private Button btnEliminar;
        private Label label1;
        private Label label2;
        private Button btnActualizar;
    }
}