using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace controlePompiersHabilit
{
    public partial class ctrlPompHabilit: UserControl
    {
        public ctrlPompHabilit()
        {
            InitializeComponent();
        }

        public ctrlPompHabilit(string typeHabil, string nomPompier, string dateObtention, string imagePath)
        {
            InitializeComponent();
            this.lblTypeHabil.Text = typeHabil;
            this.lblNomPompier.Text = nomPompier;
            this.lblDateDobtention.Text = dateObtention;
            this.picHabilitation.Image = Image.FromFile(imagePath);
            this.picHabilitation.SizeMode = PictureBoxSizeMode.StretchImage;
        }



        [Description("Type d'habilitation")]
        public String typeHabil
        {
            get
            {
                return this.lblTypeHabil.Text;
            }
            set
            {
                this.lblTypeHabil.Text = typeHabil;
            }
        }


        [Description("Nom du pompier")]
        public String nomPompier
        {
            get
            {
                return this.lblNomPompier.Text;
            }
            set
            {
                this.lblNomPompier.Text = nomPompier;
            }
        }

        [Description("DdN Pompier")]
        public String dateObtention
        {
            get
            {
                return this.lblDateDobtention.Text;
            }
            set
            {
                this.lblDateDobtention.Text = dateObtention;
            }
        }

    }
}
