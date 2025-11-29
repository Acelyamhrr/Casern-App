namespace controleStatistiquesIntervParType
{
    partial class ctrlIntervParTypeSin
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
            this.lblTypeSinistre = new System.Windows.Forms.Label();
            this.lblNbrInterv = new System.Windows.Forms.Label();
            this.picSinistre = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picSinistre)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTypeSinistre
            // 
            this.lblTypeSinistre.AutoSize = true;
            this.lblTypeSinistre.Location = new System.Drawing.Point(142, 30);
            this.lblTypeSinistre.Name = "lblTypeSinistre";
            this.lblTypeSinistre.Size = new System.Drawing.Size(51, 20);
            this.lblTypeSinistre.TabIndex = 0;
            this.lblTypeSinistre.Text = "label1";
            // 
            // lblNbrInterv
            // 
            this.lblNbrInterv.AutoSize = true;
            this.lblNbrInterv.Location = new System.Drawing.Point(142, 75);
            this.lblNbrInterv.Name = "lblNbrInterv";
            this.lblNbrInterv.Size = new System.Drawing.Size(51, 20);
            this.lblNbrInterv.TabIndex = 1;
            this.lblNbrInterv.Text = "label2";
            // 
            // picSinistre
            // 
            this.picSinistre.Location = new System.Drawing.Point(15, 20);
            this.picSinistre.Name = "picSinistre";
            this.picSinistre.Size = new System.Drawing.Size(105, 89);
            this.picSinistre.TabIndex = 2;
            this.picSinistre.TabStop = false;
            // 
            // ctrlIntervParTypeSin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.picSinistre);
            this.Controls.Add(this.lblNbrInterv);
            this.Controls.Add(this.lblTypeSinistre);
            this.Name = "ctrlIntervParTypeSin";
            this.Size = new System.Drawing.Size(235, 131);
            ((System.ComponentModel.ISupportInitialize)(this.picSinistre)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblNbrInterv;
        private System.Windows.Forms.PictureBox picSinistre;
        private System.Windows.Forms.Label lblTypeSinistre;
    }
}
