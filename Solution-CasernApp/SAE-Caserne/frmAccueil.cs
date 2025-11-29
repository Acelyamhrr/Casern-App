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
using UCHabilitations;
using UserControlMission;

namespace SAE_Caserne
{
    public partial class frmAccueil : Form
    {
        private AffichageEngin affichageEngin;
        private tableauDeBord tableau = new tableauDeBord();
        private AjouterMission ajouterMission;
        private AffichagePompiers affichagePompiers;
        Login login = new Login();
        private int matrPompier = 0;
        bool administrateur = false;
        statistiques stats = new statistiques();

        public frmAccueil()
        {
            InitializeComponent();
           
          
        }

        private void frmAccueil_Load(object sender, EventArgs e)
        {
            string req;
            DataTable schemaTable = Connexion.Connec.GetSchema("Tables");
            string liste = "";
            for (int i = 0; i < schemaTable.Rows.Count; i++)
            {
                string nomTable = schemaTable.Rows[i][2].ToString();
                req = @"select * from " + nomTable;
                SQLiteCommand cd = new SQLiteCommand(req, Connexion.Connec);
                SQLiteDataAdapter da = new SQLiteDataAdapter(cd);
                da.Fill(MesDatas.DsGlobal, nomTable);
                liste += nomTable + "\n";
            }
            


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


            // instance de la classe AffichagePompiers
            affichagePompiers = new AffichagePompiers(tabControl1, matrPompier, administrateur);
            affichagePompiers.ChargerCaserne(cboGestPompiers);


            DataTable dtCasernes = MesDatas.DsGlobal.Tables["caserne"];
            DataTable dtNatureSinistre = MesDatas.DsGlobal.Tables["naturesinistre"];
            DataTable dtEngins = MesDatas.DsGlobal.Tables["engin"];
            DataTable dtPompiers = MesDatas.DsGlobal.Tables["pompier"];
            DataTable dtPosseder = MesDatas.DsGlobal.Tables["passer"];
            DataTable dtTypeEngin = MesDatas.DsGlobal.Tables["typeengin"];
            DataTable dtNecessiter = MesDatas.DsGlobal.Tables["necessiter"];
            DataTable dtMission = MesDatas.DsGlobal.Tables["Mission"];
            DataTable dtEmbarquer = MesDatas.DsGlobal.Tables["embarquer"];
            DataTable dtAffectation = MesDatas.DsGlobal.Tables["Affectation"];
           

            // Instancier la classe AjouterMission avec les DataTables et contrôles nécessaires
            ajouterMission = new AjouterMission(
                dtCasernes,
                dtNatureSinistre,
                dtEngins,
                dtPompiers,
                dtPosseder,
                dtTypeEngin,
                dtNecessiter,
                dtMission,
                dtEmbarquer,
                dtAffectation,
                btn_cequipe,
                cmb_nsinistre,
                cmb_cequipe,
                tb_motif,
                tb_rue,
                tb_cpostal,
                tb_ville,
                pnl_ucontrol,
                pnl_ucontrl2
                );
        }
        
      
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            administrateur = false;
            pnl_login.Visible = true;

        }

        private void btn_tdb_Click(object sender, EventArgs e)
        {

            lbl_titre.Text = "Tableaux de Bord";
            tabControl1.SelectedIndex = 0;
            tableau.afficherMissions(pnl_tdb);
        }

        private void btnengin_Click(object sender, EventArgs e)
        {
            lbl_titre.Text = "Gestion des engins";
            tabControl1.SelectedIndex = 1;
            
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

        

        private void btnNmission_Click(object sender, EventArgs e)
        {
            lbl_titre.Text = "Nouvelle Mission";
            tabControl1.SelectedIndex = 2;
            
        }

        private void cboCaserne_SelectedIndexChanged(object sender, EventArgs e)
        {
            affichageEngin.ChangerEngin();
        }

        private void btnAllerPrem_Click(object sender, EventArgs e)
        {
            affichageEngin.AllerPremier();
        }

        private void btnPrecedent_Click(object sender, EventArgs e)
        {
            affichageEngin.AllerPrecedent();
        }

        private void btnSuivant_Click(object sender, EventArgs e)
        {
            affichageEngin.AllerSuivant();
        }

        private void btnAllerFin_Click(object sender, EventArgs e)
        {
            affichageEngin.AllerDernier();
        }

        private void btnTpersonnel_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 4;
            lbl_titre.Text = "Tableau du Personnel";
        }

        private void btnSeConnecter_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txtIdentifiant.Text.Length == 0 || txtMDP.Text.Length == 0)
            {
                errorProvider1.SetError(btnSeConnecter, "Veuillez renseigner un identifiant et un mot de passe.");
            }
            else
            {
                bool idCorrects = login.verifIdMdp(txtIdentifiant.Text, txtMDP.Text);

                if (!idCorrects)
                {
                    errorProvider1.SetError(btnSeConnecter, "Identifiant ou mot de passe incorrect.");
                }
                else
                {
                    administrateur = true;
                    pnl_login.Visible = false;
                }
            }

            txtIdentifiant.Clear();
            txtMDP.Clear();

        }

        private void cboGestPompiers_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idCaserne = 0;

            if (cboGestPompiers.SelectedValue is DataRowView drv)
            {
                idCaserne = Convert.ToInt32(drv["id"]);
            }
            else if (cboGestPompiers.SelectedValue != null)
            {
                idCaserne = Convert.ToInt32(cboGestPompiers.SelectedValue);
            }

            if (idCaserne > 0)
            {
                affichagePompiers.ChargerPompiersDeCaserne(idCaserne, pnlGestPomp);
            }

        }



        private void btnAjtPomp_Click(object sender, EventArgs e)
        {
            if (administrateur)
            {
                tabControl1.SelectedIndex = 5;

                cboGradeDispo.DataSource = MesDatas.DsGlobal.Tables["Grade"];
                cboGradeDispo.DisplayMember = "libelle";
                cboGradeDispo.ValueMember = "code";

                // dataTable temporaire pour stocker le type 
                DataTable dtTypes = new DataTable();
                dtTypes.Columns.Add("codeType");
                dtTypes.Columns.Add("libelleType");

                dtTypes.Rows.Add("v", "Volontaire");
                dtTypes.Rows.Add("p", "Professionnel");

                cboTypeDispo.DataSource = dtTypes;
                cboTypeDispo.DisplayMember = "libelleType";
                cboTypeDispo.ValueMember = "codeType";

                cboCaserneAffect.DataSource = MesDatas.DsGlobal.Tables["Caserne"];
                cboCaserneAffect.DisplayMember = "nom";
                cboCaserneAffect.ValueMember = "id";

                // ComboBox Sexe
                DataTable dtSexe = new DataTable();
                dtSexe.Columns.Add("codeSexe");
                dtSexe.Columns.Add("libelleSexe");

                dtSexe.Rows.Add("m", "Masculin");
                dtSexe.Rows.Add("f", "Féminin");

                cboSexe.DataSource = dtSexe;
                cboSexe.DisplayMember = "libelleSexe";
                cboSexe.ValueMember = "codeSexe";

                txtNomAjt.Clear();
                txtPrenomAjt.Clear();
                dtpDateNaiss.Value = DateTime.Today;
                txtTelAjt.Clear();
                dtpDateEntree.Value = DateTime.Today;
                cboGradeDispo.SelectedIndex = -1;
                cboSexe.SelectedIndex = -1;

                txtMatricule.Text = affichagePompiers.GetProchainMatirucle().ToString();
                affichagePompiers.AjouterUChabilitation(pnlInfosPomp, btnAjtHab);
            }
            else
            {
                MessageBox.Show("Vous n'avez pas les droits nécessaires pour ajouter un pompier.");
            }

        }

        private void btnCreerPompier_Click(object sender, EventArgs e)
        {
            if (!ValiderChamps())
            {
                MessageBox.Show("Veuillez remplir tous les champs obligatoires.", "Champs manquants", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string nom = txtNomAjt.Text;
                string prenom = txtPrenomAjt.Text;
                DateTime dateNaiss = dtpDateNaiss.Value;
                string portable = txtTelAjt.Text;
                DateTime dateEntree = dtpDateEntree.Value;
                string grade = cboGradeDispo.SelectedValue.ToString(); // faut rajouter un try catch sinon sa crash
                string type = cboTypeDispo.SelectedValue.ToString();
                int idCaserne = Convert.ToInt32(cboCaserneAffect.SelectedValue);
                string sexe = cboSexe.SelectedValue.ToString();

                Dictionary<int, string> habilitations = new Dictionary<int, string>();

                foreach (Control ctrl in pnlInfosPomp.Controls)
                {
                    if (ctrl is UChabilitation uc)
                    {

                        int idHab = Convert.ToInt32(uc.CboHab.SelectedValue);
                        string dateObtention = (uc.DateObtention).ToString("yyyy-MM-dd");
                        if (!habilitations.ContainsKey(idHab))
                        {
                            MessageBox.Show($"Ajout de l'habilitation {idHab} avec date {dateObtention}.");
                            habilitations.Add(idHab, dateObtention);

                        }
                        else
                        {
                            MessageBox.Show($"Habilitation {idHab} déjà ajoutée, elle est ignorée.");
                        }



                    }
                }


                affichagePompiers.AjouterPompier(sexe, nom, prenom, dateNaiss, portable, dateEntree, type, grade, idCaserne, habilitations);

                MessageBox.Show("Pompier ajouté avec succès !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la récupération des valeurs :" + ex.Message);
            }

        }

        private bool ValiderChamps()
        {
            errorProvider1.Clear();
            bool valide = true;

            if (string.IsNullOrWhiteSpace(txtNomAjt.Text))
            {
                errorProvider1.SetError(txtNomAjt, "Le nom est requis.");
                valide = false;
            }

            if (string.IsNullOrWhiteSpace(txtPrenomAjt.Text))
            {
                errorProvider1.SetError(txtPrenomAjt, "Le prénom est requis.");
                valide = false;
            }

            if (string.IsNullOrWhiteSpace(txtTelAjt.Text))
            {
                errorProvider1.SetError(txtTelAjt, "Le numéro de téléphone est requis.");
                valide = false;
            }

            if (cboGradeDispo.SelectedIndex == -1)
            {
                errorProvider1.SetError(cboGradeDispo, "Veuillez sélectionner un grade.");
                valide = false;
            }

            if (cboTypeDispo.SelectedIndex == -1)
            {
                errorProvider1.SetError(cboTypeDispo, "Veuillez sélectionner un type.");
                valide = false;
            }

            if (cboCaserneAffect.SelectedIndex == -1)
            {
                errorProvider1.SetError(cboCaserneAffect, "Veuillez sélectionner une caserne.");
                valide = false;
            }

            if (cboSexe.SelectedIndex == -1)
            {
                errorProvider1.SetError(cboSexe, "Veuillez sélectionner un sexe.");
                valide = false;
            }

            return valide;
        }


        private void btnAjtHab_Click(object sender, EventArgs e)
        {
            affichagePompiers.AjouterUChabilitation(pnlInfosPomp, btnAjtHab);
        }

        private void btnNonAdmin_Click(object sender, EventArgs e)
        {
           errorProvider1.Clear();
           pnl_login.Visible = false;
           txtIdentifiant.Clear();
           txtMDP.Clear(); 

        }

        private void btnConnexSuite_Click(object sender, EventArgs e)
        {
            pnl_login.Visible = true;
        }

        private void btnValiderModif_Click(object sender, EventArgs e)
        {
            TabPage tabModif = tabControl1.TabPages[6];
            Panel panelInfos = (Panel)tabModif.Controls["pnl_modifInfos"];

            // Appelle la méthode qui fait tout
            affichagePompiers.ValiderTransaction(panelInfos);

            int idCaserne = 0;

            if (cboGestPompiers.SelectedValue is DataRowView drv)
            {
                idCaserne = Convert.ToInt32(drv["id"]);
            }
            else if (cboGestPompiers.SelectedValue != null)
            {
                idCaserne = Convert.ToInt32(cboGestPompiers.SelectedValue);
            }

            if (idCaserne > 0)
            {
                affichagePompiers.ChargerPompiersDeCaserne(idCaserne, pnlGestPomp);
            }

        }

        private void btnRetourModif_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 4;
        }

        private void btnRetourAjtPomp_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 4;
        }

        private void cboChoixCaserneStat_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabControl3.Visible = true;
            stats.statCaserne(cboChoixCaserneStat, tabPage5, tabPage6);
        }

        private void btnStatistiques_Click_1(object sender, EventArgs e)
        {
            cboChoixCaserneStat.Items.Clear();
            stats.rempliCboChoixCaserne(cboChoixCaserneStat);
            stats.statEnsembleCasernes(tabPageIntervParSinistre, tabPageTopHabilit, tabPompiersHabilites);
            tabControl1.SelectedIndex = 7;
            lbl_titre.Text = "Statistiques";
        }

        private void btn_annuler_Click(object sender, EventArgs e)
        {
            tb_cpostal.Clear();
            tb_motif.Clear();
            tb_rue.Clear();
            tb_ville.Clear();
        }

     

        private void btnAnnulerAjtPomp_Click(object sender, EventArgs e)
        {
            txtNomAjt.Clear();
            txtPrenomAjt.Clear();
            txtTelAjt.Clear();
            cboSexe.SelectedIndex = -1;
            cboGradeDispo.SelectedIndex = -1;
            cboCaserneAffect.SelectedIndex = -1;
            cboTypeDispo.SelectedIndex = -1;
            txtMatricule.Clear();
        }

        private void btn_annulerModifInfo_Click(object sender, EventArgs e)
        {
            affichagePompiers.AnnulerModification();
        }

        private void btn_retourPlusInfo_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 4;
        }

        private void picQuitterApp_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtNomModif_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtIdentifiant_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtIdentifiant_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                btnSeConnecter.PerformClick();
            }
        }

        private void pnl_tdb_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tb_cpostal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
