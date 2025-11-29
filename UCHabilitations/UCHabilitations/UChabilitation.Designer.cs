namespace UCHabilitations
{
    partial class UChabilitation
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.cboHab = new System.Windows.Forms.ComboBox();
            this.dtpHab = new System.Windows.Forms.DateTimePicker();
            this.lblHabilitationAjt = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cboHab
            // 
            this.cboHab.FormattingEnabled = true;
            this.cboHab.Location = new System.Drawing.Point(6, 28);
            this.cboHab.Name = "cboHab";
            this.cboHab.Size = new System.Drawing.Size(225, 24);
            this.cboHab.TabIndex = 0;
            // 
            // dtpHab
            // 
            this.dtpHab.Location = new System.Drawing.Point(267, 26);
            this.dtpHab.Name = "dtpHab";
            this.dtpHab.Size = new System.Drawing.Size(200, 22);
            this.dtpHab.TabIndex = 1;
            // 
            // lblHabilitationAjt
            // 
            this.lblHabilitationAjt.AutoSize = true;
            this.lblHabilitationAjt.Location = new System.Drawing.Point(3, 9);
            this.lblHabilitationAjt.Name = "lblHabilitationAjt";
            this.lblHabilitationAjt.Size = new System.Drawing.Size(74, 16);
            this.lblHabilitationAjt.TabIndex = 2;
            this.lblHabilitationAjt.Text = "Habilitation";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(264, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Date d\'obtention";
            // 
            // UChabilitation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblHabilitationAjt);
            this.Controls.Add(this.dtpHab);
            this.Controls.Add(this.cboHab);
            this.Name = "UChabilitation";
            this.Size = new System.Drawing.Size(475, 61);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboHab;
        private System.Windows.Forms.DateTimePicker dtpHab;
        private System.Windows.Forms.Label lblHabilitationAjt;
        private System.Windows.Forms.Label label1;
    }
}
