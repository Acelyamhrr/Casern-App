using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace controleStatistiquesEngin
{
    public partial class ctrlStatsEngins : UserControl
    {

        public ctrlStatsEngins()
        {
            InitializeComponent();
        }

        public ctrlStatsEngins(string nomEngin, string descEngin, string imagePath)
        {
            InitializeComponent();
            this.lblNomEngin.Text = nomEngin;
            this.lblDesc.Text = descEngin;
            this.picImageEngin.Image = Image.FromFile(imagePath);
            this.picImageEngin.SizeMode = PictureBoxSizeMode.StretchImage;
        }



        [Description("Le type de la mission")]
        public String nomEngin
        {
            get
            {
                return this.lblNomEngin.Text;
            }
            set
            {
                this.lblNomEngin.Text = nomEngin;
            }
        }


        [Description("Description de la mission")]
        public String descEngin
        {
            get
            {
                return this.lblDesc.Text;
            }
            set
            {
                this.lblDesc.Text = descEngin;
            }
        }
    }
}
