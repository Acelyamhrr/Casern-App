namespace controlePompiersHabilit
{
    partial class ctrlPompHabilit
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
            this.picHabilitation = new System.Windows.Forms.PictureBox();
            this.lblNomPompier = new System.Windows.Forms.Label();
            this.lblTypeHabil = new System.Windows.Forms.Label();
            this.lblDateDobtention = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picHabilitation)).BeginInit();
            this.SuspendLayout();
            // 
            // picHabilitation
            // 
            this.picHabilitation.Location = new System.Drawing.Point(28, 21);
            this.picHabilitation.Name = "picHabilitation";
            this.picHabilitation.Size = new System.Drawing.Size(105, 89);
            this.picHabilitation.TabIndex = 5;
            this.picHabilitation.TabStop = false;
            // 
            // lblNomPompier
            // 
            this.lblNomPompier.AutoSize = true;
            this.lblNomPompier.Location = new System.Drawing.Point(155, 55);
            this.lblNomPompier.Name = "lblNomPompier";
            this.lblNomPompier.Size = new System.Drawing.Size(51, 20);
            this.lblNomPompier.TabIndex = 4;
            this.lblNomPompier.Text = "label2";
            // 
            // lblTypeHabil
            // 
            this.lblTypeHabil.AutoSize = true;
            this.lblTypeHabil.Location = new System.Drawing.Point(155, 21);
            this.lblTypeHabil.Name = "lblTypeHabil";
            this.lblTypeHabil.Size = new System.Drawing.Size(51, 20);
            this.lblTypeHabil.TabIndex = 3;
            this.lblTypeHabil.Text = "label1";
            // 
            // lblDateDobtention
            // 
            this.lblDateDobtention.AutoSize = true;
            this.lblDateDobtention.Location = new System.Drawing.Point(155, 90);
            this.lblDateDobtention.Name = "lblDateDobtention";
            this.lblDateDobtention.Size = new System.Drawing.Size(51, 20);
            this.lblDateDobtention.TabIndex = 6;
            this.lblDateDobtention.Text = "label3";
            // 
            // ctrlPompHabilit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblDateDobtention);
            this.Controls.Add(this.picHabilitation);
            this.Controls.Add(this.lblNomPompier);
            this.Controls.Add(this.lblTypeHabil);
            this.Name = "ctrlPompHabilit";
            this.Size = new System.Drawing.Size(235, 131);
            ((System.ComponentModel.ISupportInitialize)(this.picHabilitation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picHabilitation;
        private System.Windows.Forms.Label lblNomPompier;
        private System.Windows.Forms.Label lblTypeHabil;
        private System.Windows.Forms.Label lblDateDobtention;
    }
}
