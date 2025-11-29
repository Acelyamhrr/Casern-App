namespace SAE_Caserne.Classe
{
    partial class FrmCommentaire
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
            this.lblCR = new System.Windows.Forms.Label();
            this.lblEngins = new System.Windows.Forms.Label();
            this.panelEngins = new System.Windows.Forms.Panel();
            this.txtCompteRendu = new System.Windows.Forms.TextBox();
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblCR
            // 
            this.lblCR.AutoSize = true;
            this.lblCR.Location = new System.Drawing.Point(10, 10);
            this.lblCR.Name = "lblCR";
            this.lblCR.Size = new System.Drawing.Size(98, 16);
            this.lblCR.TabIndex = 4;
            this.lblCR.Text = "Compte-rendu :";
            // 
            // lblEngins
            // 
            this.lblEngins.Location = new System.Drawing.Point(10, 100);
            this.lblEngins.Name = "lblEngins";
            this.lblEngins.Size = new System.Drawing.Size(200, 23);
            this.lblEngins.TabIndex = 2;
            this.lblEngins.Text = "Réparations des engins :";
            // 
            // panelEngins
            // 
            this.panelEngins.AutoScroll = true;
            this.panelEngins.Location = new System.Drawing.Point(10, 130);
            this.panelEngins.Name = "panelEngins";
            this.panelEngins.Size = new System.Drawing.Size(560, 180);
            this.panelEngins.TabIndex = 1;
            // 
            // txtCompteRendu
            // 
            this.txtCompteRendu.Location = new System.Drawing.Point(10, 30);
            this.txtCompteRendu.Multiline = true;
            this.txtCompteRendu.Name = "txtCompteRendu";
            this.txtCompteRendu.Size = new System.Drawing.Size(560, 60);
            this.txtCompteRendu.TabIndex = 0;
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.Location = new System.Drawing.Point(480, 320);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(80, 23);
            this.btnAnnuler.TabIndex = 0;
            this.btnAnnuler.Text = "Annuler";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(399, 320);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "Valider";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FrmCommentaire
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 353);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.txtCompteRendu);
            this.Controls.Add(this.panelEngins);
            this.Controls.Add(this.lblEngins);
            this.Controls.Add(this.lblCR);
            this.Name = "FrmCommentaire";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Clôture de la mission";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCR;
        private System.Windows.Forms.Label lblEngins;
        private System.Windows.Forms.Panel panelEngins;
        private System.Windows.Forms.TextBox txtCompteRendu;
        private System.Windows.Forms.Button btnAnnuler;
        private System.Windows.Forms.Button btnOK;
    }
}