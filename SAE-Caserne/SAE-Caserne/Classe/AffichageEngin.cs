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
        private BindingSource bsCaserne = new BindingSource();
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
            try
            {
                chargerDonnees();
                creerRelation();
                lierSources();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement initial : " + ex.Message);
            }
        }

        public void AllerSuivant() 
        {
            try
            {
                bsEngins.MoveNext();
                afficherEngin();
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
                afficherEngin();
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
                afficherEngin();
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
                afficherEngin();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la navigation vers le dernier engin : " + ex.Message);
            }
        }

        public void ChangementCaserne() 
        {
            try
            {
                afficherEngin();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du changement de caserne : " + ex.Message);
            }
        }

        private void chargerDonnees()
        {
            try
            {
                // charger les casernes
                string reqCaserne = "select * from Caserne";
                SQLiteConnection connecCas = Connexion.Connec;
                SQLiteCommand cmdCas = new SQLiteCommand(reqCaserne, connecCas);
                SQLiteDataAdapter daCaserne = new SQLiteDataAdapter(cmdCas);

                if (MesDatas.DsGlobal.Tables.Contains("Caserne"))
                {
                    MesDatas.DsGlobal.Tables["Caserne"].Clear();
                }
                daCaserne.Fill(MesDatas.DsGlobal, "Caserne");

                // charger les engins
                string reqEngin = "select * from Engin";
                SQLiteConnection connecEng = Connexion.Connec;
                SQLiteCommand cmdEng = new SQLiteCommand(reqEngin, connecEng);
                SQLiteDataAdapter daEng = new SQLiteDataAdapter(cmdEng);

                if (MesDatas.DsGlobal.Tables.Contains("Engin"))
                {
                    MesDatas.DsGlobal.Tables["Engin"].Clear();
                }
                daEng.Fill(MesDatas.DsGlobal, "Engin");
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Erreur SQL lors du chargement des données : " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des données : " + ex.Message);
            }
            finally
            {
                Connexion.FermerConnexion();
            }
        }

        private void creerRelation()
        {
            try
            {
                if (!MesDatas.DsGlobal.Relations.Contains("Caserne_Engins"))
                {
                    MesDatas.DsGlobal.Relations.Add("Caserne_Engins",
                        MesDatas.DsGlobal.Tables["Caserne"].Columns["id"],
                        MesDatas.DsGlobal.Tables["Engin"].Columns["idCaserne"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la création de la relation : " + ex.Message);
            }
        }

        private void lierSources()
        {
            try
            {
                bsCaserne.DataSource = MesDatas.DsGlobal;
                bsCaserne.DataMember = "Caserne";
                cboCaserne.DataSource = bsCaserne;
                cboCaserne.DisplayMember = "Nom";
                cboCaserne.ValueMember = "Id";

                bsEngins.DataSource = bsCaserne;
                bsEngins.DataMember = "Caserne_Engins";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la liaison des sources de données : " + ex.Message);
            }
        }

        private void afficherEngin()
        {
            try
            {
                if (bsEngins.Current is DataRowView engin)
                {
                    lblNumEngin.Text = engin["idCaserne"].ToString() + "-" + engin["codeTypeEngin"].ToString() + "-" + engin["numero"].ToString();
                    lblDateRecep.Text = Convert.ToDateTime(engin["dateReception"]).ToString("dd/MM/yyyy");

                    if (Convert.ToInt32(engin["enMission"]) == 1)
                    {
                        lblMission.Text = "En mission ✔";
                    }
                    else
                    {
                        lblMission.Text = "En mission ✘";
                    }

                    if (Convert.ToInt32(engin["enPanne"]) == 1)
                    {
                        lblPanne.Text = "En panne ✔";
                    }
                    else
                    {
                        lblPanne.Text = "En panne ✘";
                    }

                    string codeType = engin["codeTypeEngin"].ToString();
                    string cheminImage = @"..\\..\\..\\ImagesEngins\\" + codeType + ".png";

                    if (System.IO.File.Exists(cheminImage))
                    {
                        picEngin.SizeMode = PictureBoxSizeMode.Zoom;
                        picEngin.Image = Image.FromFile(cheminImage);
                    }
                    else
                    {
                        picEngin.Image = null;
                    }


                }
                else
                {
                    lblNumEngin.Text = "";
                    lblDateRecep.Text = "";
                    lblMission.Text = "";
                    lblPanne.Text = "";
                    picEngin.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'affichage de l'engin : " + ex.Message);

                // Réinitialiser les labels quand il y a erreur
                lblNumEngin.Text = "";
                lblDateRecep.Text = "";
                lblMission.Text = "";
                lblPanne.Text = "";
            }
        }

    }
}
