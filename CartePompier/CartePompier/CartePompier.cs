using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace monUC
{

    public delegate void BoutonAction(object sender, EventArgs e);
    public partial class CartePompier: UserControl
    {
        public CartePompier()
        {
            InitializeComponent();
        }

        public int Matricule { get; set; }
        public string NomPomp { get; set; }
        public string PrenomPomp { get; set; }

        public int EnConges { get; set; }

        public string idCas { get; set; }
        public string CodeGrade { get; set; }
        public string Portable { get; set; }

        public string typePomp { get; set; } // Type de pompier (ex: Sapeur, Caporal, etc.)


        public CartePompier(string nom, string grade, string type, string statut, int matricule, string nomPomp, string prenomPomp, int enConge, string idCas, string codeGrade, string portable, string typeOk)
        {
            InitializeComponent();
            this.lblNom.Text = nom;
            this.lblGrade.Text = grade;
            this.lblType.Text = type;
            this.lblStatut.Text = statut;
            this.Matricule = matricule;
            this.typePomp = typeOk;
            this.NomPomp = nomPomp;
            this.PrenomPomp = prenomPomp;
            this.EnConges = enConge;
            this.idCas = idCas;
            this.CodeGrade = codeGrade;
            this.Portable = portable;
        }

        [Description("Nom du pompier affiché")]
        public String Nom
        {
            get
            {
                return this.lblNom.Text;
            }
            set
            {
                this.lblNom.Text = value;
            }
        }

        [Description("Grade du pompier affiché")]
        public String Grade
        {
            get
            {
                return this.lblGrade.Text;
            }
            set
            {
                this.lblGrade.Text = value;
            }
        }

        [Description("Type du pompier affiché")]
        public String Type
        {
            get
            {
                return this.lblType.Text;
            }
            set
            {
                this.lblType.Text = value;
            }
        }

        [Description("Statut du pompier affiché")]
        public String Statut
        {
            get
            {
                return this.lblStatut.Text;
            }
            set
            {
                this.lblStatut.Text = value;

                switch (value)
                {
                    case "Disponible":
                        this.ForeColor = Color.LightGreen;
                        break;
                    case "En mission":
                        this.ForeColor = Color.LightYellow;
                        break;
                    case "En congé":
                        this.ForeColor = Color.MistyRose;
                        break;
                    default:
                        this.ForeColor = SystemColors.Control;
                        break;

                }

            }
        }

        // delegates
        public BoutonAction Details;
        public BoutonAction Modifier;

        private void btnDetails_Click(object sender, EventArgs e)
        {
            if (this.Details != null)
            {
                this.Details(sender, e);
            }
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            if (this.Modifier != null)
            {
                this.Modifier(sender, e);
            }
        }



    }
}
