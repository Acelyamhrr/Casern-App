using Pinpon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace SAE_Caserne.Classe
{
    internal class AffichageEngin
    {
        private BindingSource bsEngins = new BindingSource();

        private ComboBox cboCaserne;
        private Label lblNumEngin, lblDateRecep, lblMission, lblPanne;
        private PictureBox picEngin;

        // constructeur
        public AffichageEngin(ComboBox cbo, Label num, Label date, Label mission, Label panne, PictureBox pic)
        {
            cboCaserne = cbo;
            lblNumEngin = num;
            lblDateRecep = date;
            lblMission = mission;
            lblPanne = panne;
            picEngin = pic;
        }

        public void Initialiser()
        {
            ChargerDonnees();
            ChargerCaserne();
            AjouterColonneEtat();
            InitialiserBindings();

        }

        private void ChargerDonnees()
        {
            string requete = @"SELECT c.idCaserne || '-' || c.codeTypeEngin || '-' || c.numero AS Num, c.*
                    FROM Engin c;";

            SQLiteDataAdapter da = new SQLiteDataAdapter(requete, Connexion.Connec);

            if (MesDatas.DsGlobal.Tables.Contains("Engins"))
            {
                MesDatas.DsGlobal.Tables["Engins"].Clear();
            }
            da.Fill(MesDatas.DsGlobal, "Engins");
        }

        private void ChargerCaserne()
        {
            cboCaserne.DisplayMember = "nom";
            cboCaserne.ValueMember = "id";
            cboCaserne.DataSource = MesDatas.DsGlobal.Tables["Caserne"];
        }

        private void AjouterColonneEtat()
        {
            var table = MesDatas.DsGlobal.Tables["Engins"];

            if (!table.Columns.Contains("EtatMission"))
                table.Columns.Add("EtatMission", typeof(string));
            if (!table.Columns.Contains("EtatPanne"))
                table.Columns.Add("EtatPanne", typeof(string));

            foreach (DataRow row in table.Rows)
            {
                bool mission = Convert.ToInt32(row["enMission"]) == 1;
                bool panne = Convert.ToInt32(row["enPanne"]) == 1;

                row["EtatMission"] = mission ? "En mission" : "Disponible";
                row["EtatPanne"] = panne ? "En panne ✓" : "En panne ✗";
            }

        }

        private void InitialiserBindings()
        {
            bsEngins.DataSource = MesDatas.DsGlobal.Tables["Engins"];

            lblNumEngin.DataBindings.Add("Text", bsEngins, "Num");
            lblDateRecep.DataBindings.Add("Text", bsEngins, "dateReception");
            lblMission.DataBindings.Add("Text", bsEngins, "EtatMission");
            lblPanne.DataBindings.Add("Text", bsEngins, "EtatPanne");
            AfficherImageEngin();

        }

        public void AllerSuivant()
        {
            try
            {
                bsEngins.MoveNext();
                AfficherImageEngin();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la navigation vers l'engin suivant : " + ex.Message);
            }
        }

        public void AllerPrecedent()
        {
            try
            {
                bsEngins.MovePrevious();
                AfficherImageEngin();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la navigation vers l'engin précédent : " + ex.Message);
            }
        }

        public void AllerPremier()
        {
            try
            {
                bsEngins.MoveFirst();
                AfficherImageEngin();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la navigation vers le premier engin : " + ex.Message);
            }
        }

        public void AllerDernier()
        {
            try
            {
                bsEngins.MoveLast();
                AfficherImageEngin();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la navigation vers le dernier engin : " + ex.Message);
            }
        }

        private void AfficherImageEngin()
        {
            if (bsEngins.Position < 0)
            {
                picEngin.Image = null;
                return;
            }

            DataRowView currentRow = bsEngins.Current as DataRowView;
            string codeType = currentRow["codeTypeEngin"].ToString();
            string cheminImage = @"..\\..\\..\\ImagesEngins\\" + codeType + ".png";

            picEngin.Image = System.IO.File.Exists(cheminImage) ? Image.FromFile(cheminImage) : null;
        }

        public void ChangerEngin()
        {
            if (cboCaserne.SelectedValue == null)
            {
                return;
            }

            int idCaserne = Convert.ToInt32(cboCaserne.SelectedValue);
            bsEngins.Filter = $"idCaserne = {idCaserne}";
            bsEngins.Position = 0;

            // Recharger l'image aussi
            AfficherImageEngin();
        }




    }
}
