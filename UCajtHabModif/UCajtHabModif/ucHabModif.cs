using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UCajtHabModif
{
    public delegate void Supprimer(object sende, EventArgs e);
    public partial class ucHabModif: UserControl
    {
        public int IdHabilitation { get; set; }

        public ucHabModif()
        {
            InitializeComponent();
        }

        public ucHabModif(int idHab, string nom, DateTime date)
        {
            InitializeComponent();
            this.NomHabilitation = nom;
            this.DateObtention = date;
            this.lblNom.Text = nom;
            IdHabilitation = idHab;
        }

        private string nomHabilitaton;
        private DateTime dateObtention;

        public string NomHabilitation
        {
            get 
            { 
                return nomHabilitaton; 
            }
            set
            {
                nomHabilitaton = value;
                lblNom.Text = value;

                btnSupprimer.Left = lblNom.Right + 5;
                this.Width = btnSupprimer.Right + 5;
            }
        }

        public DateTime DateObtention
        {
            get 
            { 
                return dateObtention; 
            }
            set
            {
                dateObtention = value;
            }
        }

        public Supprimer Suppr;

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (this.Suppr != null)
            {
                this.Suppr(this, e);
            }
        }
    }
}
