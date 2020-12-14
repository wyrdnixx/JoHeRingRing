namespace JoHeRingRing
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblAlertInfo = new System.Windows.Forms.Label();
            this.pbRinger = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbRinger)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAlertInfo
            // 
            this.lblAlertInfo.AutoSize = true;
            this.lblAlertInfo.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlertInfo.Location = new System.Drawing.Point(12, 12);
            this.lblAlertInfo.Name = "lblAlertInfo";
            this.lblAlertInfo.Size = new System.Drawing.Size(118, 24);
            this.lblAlertInfo.TabIndex = 2;
            this.lblAlertInfo.Text = "lblAlertinfo";
            // 
            // pbRinger
            // 
            this.pbRinger.Image = global::JoHeRingRing.Properties.Resources.bell;
            this.pbRinger.Location = new System.Drawing.Point(8, 57);
            this.pbRinger.Name = "pbRinger";
            this.pbRinger.Size = new System.Drawing.Size(466, 522);
            this.pbRinger.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbRinger.TabIndex = 1;
            this.pbRinger.TabStop = false;
            this.pbRinger.Click += new System.EventHandler(this.pbRinger_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 588);
            this.Controls.Add(this.lblAlertInfo);
            this.Controls.Add(this.pbRinger);
            this.Name = "Form1";
            this.Text = "JoHeRingRing";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbRinger)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pbRinger;
        private System.Windows.Forms.Label lblAlertInfo;
    }
}

