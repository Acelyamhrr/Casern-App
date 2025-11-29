namespace controleStatistiquesEngin
{
    partial class ctrlStatsEngins
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
            this.picImageEngin = new System.Windows.Forms.PictureBox();
            this.lblNomEngin = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picImageEngin)).BeginInit();
            this.SuspendLayout();
            // 
            // picImageEngin
            // 
            this.picImageEngin.Location = new System.Drawing.Point(7, 33);
            this.picImageEngin.Name = "picImageEngin";
            this.picImageEngin.Size = new System.Drawing.Size(58, 59);
            this.picImageEngin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picImageEngin.TabIndex = 0;
            this.picImageEngin.TabStop = false;
            // 
            // lblNomEngin
            // 
            this.lblNomEngin.AutoSize = true;
            this.lblNomEngin.Location = new System.Drawing.Point(14, 10);
            this.lblNomEngin.Name = "lblNomEngin";
            this.lblNomEngin.Size = new System.Drawing.Size(51, 20);
            this.lblNomEngin.TabIndex = 1;
            this.lblNomEngin.Text = "label1";
            this.lblNomEngin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(14, 95);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(51, 20);
            this.lblDesc.TabIndex = 2;
            this.lblDesc.Text = "label2";
            this.lblDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ctrlStatsEngins
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.lblNomEngin);
            this.Controls.Add(this.picImageEngin);
            this.Name = "ctrlStatsEngins";
            this.Size = new System.Drawing.Size(75, 127);
            ((System.ComponentModel.ISupportInitialize)(this.picImageEngin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picImageEngin;
        private System.Windows.Forms.Label lblNomEngin;
        private System.Windows.Forms.Label lblDesc;
    }
}
