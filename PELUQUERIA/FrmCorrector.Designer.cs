namespace PELUQUERIA
{
    partial class FrmCorrector
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
            this.btnCorregir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCorregir
            // 
            this.btnCorregir.Location = new System.Drawing.Point(159, 80);
            this.btnCorregir.Name = "btnCorregir";
            this.btnCorregir.Size = new System.Drawing.Size(112, 53);
            this.btnCorregir.TabIndex = 0;
            this.btnCorregir.Text = "Corregir";
            this.btnCorregir.UseVisualStyleBackColor = true;
            this.btnCorregir.Click += new System.EventHandler(this.btnCorregir_Click);
            // 
            // FrmCorrector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 357);
            this.Controls.Add(this.btnCorregir);
            this.Name = "FrmCorrector";
            this.Text = "FrmCorrector";
            this.Load += new System.EventHandler(this.FrmCorrector_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCorregir;
    }
}