using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace controleStatistiquesIntervParType
{
    public partial class ctrlIntervParTypeSin: UserControl
    {
        public ctrlIntervParTypeSin()
        {
            InitializeComponent();
        }

        public ctrlIntervParTypeSin(string typeSinistre, string nbrIntervs, string imagePath)
        {
            InitializeComponent();
            this.lblTypeSinistre.Text = typeSinistre;
            this.lblNbrInterv.Text = nbrIntervs;
            this.picSinistre.Image = Image.FromFile(imagePath);
            this.picSinistre.SizeMode = PictureBoxSizeMode.StretchImage;
        }



        [Description("Type de sinistre")]
        public String typeSinistre
        {
            get
            {
                return this.lblTypeSinistre.Text;
            }
            set
            {
                this.lblTypeSinistre.Text = typeSinistre;
            }
        }


        [Description("Nombre d'interventions")]
        public String nbrIntervs
        {
            get
            {
                return this.lblNbrInterv.Text;
            }
            set
            {
                this.lblNbrInterv.Text = nbrIntervs;
            }
        }
    }
}
