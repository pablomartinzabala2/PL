namespace PELUQUERIA
{
    partial class FrmApertura
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
            this.Grupo = new System.Windows.Forms.GroupBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Nombre = new System.Windows.Forms.Label();
            this.txtFechaCierre = new System.Windows.Forms.MaskedTextBox();
            this.txtFechaApertura = new System.Windows.Forms.MaskedTextBox();
            this.txtTotalCaja = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Grupo.SuspendLayout();
            this.SuspendLayout();
            // 
            // Grupo
            // 
            this.Grupo.Controls.Add(this.label3);
            this.Grupo.Controls.Add(this.txtTotalCaja);
            this.Grupo.Controls.Add(this.btnCerrar);
            this.Grupo.Controls.Add(this.btnGrabar);
            this.Grupo.Controls.Add(this.txtTotal);
            this.Grupo.Controls.Add(this.label2);
            this.Grupo.Controls.Add(this.label1);
            this.Grupo.Controls.Add(this.Nombre);
            this.Grupo.Controls.Add(this.txtFechaCierre);
            this.Grupo.Controls.Add(this.txtFechaApertura);
            this.Grupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Grupo.Location = new System.Drawing.Point(12, 8);
            this.Grupo.Name = "Grupo";
            this.Grupo.Size = new System.Drawing.Size(461, 237);
            this.Grupo.TabIndex = 16;
            this.Grupo.TabStop = false;
            this.Grupo.Text = "Apertura y cierre de caja";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnCerrar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCerrar.Location = new System.Drawing.Point(194, 138);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(94, 30);
            this.btnCerrar.TabIndex = 88;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnGrabar
            // 
            this.btnGrabar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnGrabar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGrabar.Location = new System.Drawing.Point(84, 138);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(94, 30);
            this.btnGrabar.TabIndex = 87;
            this.btnGrabar.Text = "Abrir";
            this.btnGrabar.UseVisualStyleBackColor = true;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // txtTotal
            // 
            this.txtTotal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTotal.Location = new System.Drawing.Point(118, 95);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(153, 23);
            this.txtTotal.TabIndex = 86;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(16, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 17);
            this.label2.TabIndex = 85;
            this.label2.Text = "Monto inicial";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(16, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 84;
            this.label1.Text = "Cierre";
            // 
            // Nombre
            // 
            this.Nombre.AutoSize = true;
            this.Nombre.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Nombre.Location = new System.Drawing.Point(16, 37);
            this.Nombre.Name = "Nombre";
            this.Nombre.Size = new System.Drawing.Size(63, 17);
            this.Nombre.TabIndex = 83;
            this.Nombre.Text = "Apertura";
            // 
            // txtFechaCierre
            // 
            this.txtFechaCierre.Location = new System.Drawing.Point(118, 66);
            this.txtFechaCierre.Mask = "00/00/0000";
            this.txtFechaCierre.Name = "txtFechaCierre";
            this.txtFechaCierre.Size = new System.Drawing.Size(91, 23);
            this.txtFechaCierre.TabIndex = 82;
            this.txtFechaCierre.ValidatingType = typeof(System.DateTime);
            // 
            // txtFechaApertura
            // 
            this.txtFechaApertura.Location = new System.Drawing.Point(118, 37);
            this.txtFechaApertura.Mask = "00/00/0000";
            this.txtFechaApertura.Name = "txtFechaApertura";
            this.txtFechaApertura.Size = new System.Drawing.Size(91, 23);
            this.txtFechaApertura.TabIndex = 81;
            this.txtFechaApertura.ValidatingType = typeof(System.DateTime);
            // 
            // txtTotalCaja
            // 
            this.txtTotalCaja.Location = new System.Drawing.Point(118, 184);
            this.txtTotalCaja.Name = "txtTotalCaja";
            this.txtTotalCaja.Size = new System.Drawing.Size(100, 23);
            this.txtTotalCaja.TabIndex = 89;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(16, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 17);
            this.label3.TabIndex = 90;
            this.label3.Text = "Total Caja";
            // 
            // FrmApertura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(479, 257);
            this.Controls.Add(this.Grupo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmApertura";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmApertura";
            this.Load += new System.EventHandler(this.FrmApertura_Load);
            this.Grupo.ResumeLayout(false);
            this.Grupo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Grupo;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnGrabar;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Nombre;
        private System.Windows.Forms.MaskedTextBox txtFechaCierre;
        private System.Windows.Forms.MaskedTextBox txtFechaApertura;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTotalCaja;
    }
}