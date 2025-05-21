using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iText.Layout.Element;
using Pinpon;
using SAE_Caserne.Classe;
using UserControlMission;

namespace SAE_Caserne
{
    public partial class frmAccueil : Form
    {
        private AffichageEngin affichageEngin;
        private tableauDeBord tableau = new tableauDeBord();
       
      

        public frmAccueil()
        {
            InitializeComponent();
           
          
        }

        private void frmAccueil_Load(object sender, EventArgs e)
        {
            

            // enlever les bouttons du tabControl
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            // charger les éléments dans pnl_tdb
          
            tableau.Chargement();
            tableau.afficherMissions(pnl_tdb);

            // charger les éléments de affichageEngin
            affichageEngin = new AffichageEngin(cboCaserne, lblNumEngin, lblDateRecep, lblMission, lblPanne, picEngin);
            affichageEngin.Initialiser();

            AjouterMission.RemplirComboboxNsinistre(cmb_nsinistre);

        }
        private void btnPrecedent_Click(object sender, EventArgs e)
        {
            affichageEngin.AllerPrecedent();

        }

        private void btnSuivant_Click(object sender, EventArgs e)
        {
            affichageEngin.AllerSuivant();
        }

        private void btnAllerPrem_Click(object sender, EventArgs e)
        {
            affichageEngin.AllerPremier();
        }

        private void btnAllerFin_Click(object sender, EventArgs e)
        {
            affichageEngin.AllerDernier();
        }

      
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_tdb_Click(object sender, EventArgs e)
        {

            tabControl1.SelectedIndex = 0;
        }

        private void btnPrecedent_Click_2(object sender, EventArgs e)
        {
            affichageEngin.AllerPrecedent();

        }

        private void btnSuivant_Click_2(object sender, EventArgs e)
        {
            affichageEngin.AllerSuivant();
        }

        private void btnAllerPrem_Click_2(object sender, EventArgs e)
        {
            affichageEngin.AllerPremier();
        }

        private void btnAllerFin_Click_2(object sender, EventArgs e)
        {
            affichageEngin.AllerDernier();
        }

        private void btnengin_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void pnl_tdb_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_enCours_Click(object sender, EventArgs e)
        {

        }

        private void chbx_enCours_CheckedChanged(object sender, EventArgs e)
        {
            if (chbx_enCours.Checked)
            {
                tableau.afficherMissions(pnl_tdb, 0);
            }
            else {
                tableau.afficherMissions(pnl_tdb, 1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pnl_login.Visible = false;
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmb_nsinistre_SelectedIndexChanged(object sender, EventArgs e)
        {

            AjouterMission.RemplirComboboxNsinistre(cmb_nsinistre);
            MessageBox.Sg
            MessageBox.Show(cmb_nsinistre.SelectedItem.ToString());
        }

        private void btnNmission_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }

        private void cmb_nsinistre_Click(object sender, EventArgs e)
        {
            AjouterMission.RemplirComboboxNsinistre(cmb_nsinistre);
        }
    }
}
