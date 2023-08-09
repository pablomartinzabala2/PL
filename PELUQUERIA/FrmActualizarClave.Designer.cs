namespace PELUQUERIA
{
    partial class FrmActualizarClave
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
            this.Nombre = new System.Windows.Forms.Label();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.Grupo = new System.Windows.Forms.GroupBox();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.txtReingresoContrasenia = new System.Windows.Forms.TextBox();
            this.txtContraseniaNueva = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Grupo.SuspendLayout();
            this.SuspendLayout();
            // 
            // Nombre
            // 
            this.Nombre.AutoSize = true;
            this.Nombre.Location = new System.Drawing.Point(19, 41);
            this.Nombre.Name = "Nombre";
            this.Nombre.Size = new System.Drawing.Size(130, 17);
            this.Nombre.TabIndex = 0;
            this.Nombre.Text = "Ccontraseña actual";
            // 
            // txtClave
            // 
            this.txtClave.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtClave.Location = new System.Drawing.Point(173, 38);
            this.txtClave.Name = "txtClave";
            this.txtClave.Size = new System.Drawing.Size(168, 23);
            this.txtClave.TabIndex = 1;
            // 
            // Grupo
            // 
            this.Grupo.Controls.Add(this.btnGrabar);
            this.Grupo.Controls.Add(this.txtReingresoContrasenia);
            this.Grupo.Controls.Add(this.txtContraseniaNueva);
            this.Grupo.Controls.Add(this.label2);
            this.Grupo.Controls.Add(this.label1);
            this.Grupo.Controls.Add(this.txtClave);
            this.Grupo.Controls.Add(this.Nombre);
            this.Grupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Grupo.Location = new System.Drawing.Point(12, 12);
            this.Grupo.Name = "Grupo";
            this.Grupo.Size = new System.Drawing.Size(360, 186);
            this.Grupo.TabIndex = 16;
            this.Grupo.TabStop = false;
            this.Grupo.Text = "Contraseña de usuarios";
            // 
            // btnGrabar
            // 
            this.btnGrabar.Location = new System.Drawing.Point(173, 126);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(75, 30);
            this.btnGrabar.TabIndex = 17;
            this.btnGrabar.Text = "Grabar";
            this.btnGrabar.UseVisualStyleBackColor = true;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // txtReingresoContrasenia
            // 
            this.txtReingresoContrasenia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReingresoContrasenia.Location = new System.Drawing.Point(173, 97);
            this.txtReingresoContrasenia.Name = "txtReingresoContrasenia";
            this.txtReingresoContrasenia.Size = new System.Drawing.Size(168, 23);
            this.txtReingresoContrasenia.TabIndex = 19;
            // 
            // txtContraseniaNueva
            // 
            this.txtContraseniaNueva.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtContraseniaNueva.Location = new System.Drawing.Point(173, 67);
            this.txtContraseniaNueva.Name = "txtContraseniaNueva";
            this.txtContraseniaNueva.Size = new System.Drawing.Size(168, 23);
            this.txtContraseniaNueva.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 17);
            this.label2.TabIndex = 17;
            this.label2.Text = "Reingresar contraseña";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ccontraseña nueva";
            // 
            // FrmActualizarClave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(386, 206);
            this.Controls.Add(this.Grupo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmActualizarClave";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Actualización de contraseña";
            this.Load += new System.EventHandler(this.FrmActualizarClave_Load);
            this.Grupo.ResumeLayout(false);
            this.Grupo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Nombre;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.GroupBox Grupo;
        private System.Windows.Forms.TextBox txtReingresoContrasenia;
        private System.Windows.Forms.TextBox txtContraseniaNueva;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGrabar;
    }
}