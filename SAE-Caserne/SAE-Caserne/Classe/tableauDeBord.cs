using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pinpon;
using UserControlMission;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;


namespace SAE_Caserne.Classe
{
    internal class tableauDeBord
    {
        public tableauDeBord() { }

        public void Chargement()
        {
            try
            {
                string requete = @"
                    SELECT 
                        m.id,
                        ns.libelle AS missionType,
                        m.dateHeureDepart AS missionDate,
                        m.motifAppel AS missionDetails,
                        c.nom AS caserne,
                        m.terminee AS etatMission,
                        m.compteRendu AS compteRendu
                    FROM Mission m
                    LEFT JOIN NatureSinistre ns ON ns.id = m.idNatureSinistre
                    LEFT JOIN Caserne c ON c.id = m.idCaserne
                    ORDER BY m.dateHeureDepart ASC;
                ";
                SQLiteConnection connection = Connexion.Connec;
                SQLiteCommand command = new SQLiteCommand(requete, connection);
                SQLiteDataAdapter da = new SQLiteDataAdapter(command);

                if (MesDatas.DsGlobal.Tables.Contains("MissionEnCours")) {
                    MesDatas.DsGlobal.Tables["MissionEnCours"].Clear();
                }
                else
                {
                    MesDatas.DsGlobal.Tables.Add("MissionEnCours");
                }
                da.Fill(MesDatas.DsGlobal.Tables["MissionEnCours"]);
                Connexion.FermerConnexion();
                

            }
            catch
            {

            }
        }

        public void afficherMissions(Panel panel, int filtreEtat = 1)
        {

            int position = 0;
            DataTable dt = MesDatas.DsGlobal.Tables["MissionEnCours"];
            for (int i = panel.Controls.Count - 1; i >= 0; i--)
            {
                Control ctrl = panel.Controls[i];
                if (ctrl.Tag != null && ctrl.Tag.ToString() == "mission")
                {
                    panel.Controls.RemoveAt(i);
                    ctrl.Dispose();
                }
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
               
                int index = i;
                DataRow row = dt.Rows[i];
                int etatMission = Convert.ToInt32(row["etatMission"]);

               
                if (filtreEtat == 0 && etatMission == 1)
                {
                    continue; 
                }

                string id = row["id"].ToString();
                string missionType = row["missionType"].ToString();
                string missionDate = row["missionDate"].ToString();
                string missionDetails = row["missionDetails"].ToString();
                string caserne = row["caserne"].ToString();
               
                string compteRendu = row["compteRendu"].ToString();

                UserControlMission.UserControl1 newUserControl = new UserControlMission.UserControl1(
                    missionType,
                    missionDate,
                    caserne,
                    missionDetails,
                    id,
                    @"../../Image/mission.png"
                );

                UserContrltdb2.UserControl1 pdfUserControl = new UserContrltdb2.UserControl1(
                    @"../../Image/mission.png",
                    @"../../Image/mission.png"


                );

                pdfUserControl.PictureBox1Clicked += (sender, e) =>
                {
                    MessageBox.Show("Test click");
                    if(etat(index) == true)
                    {
                        GenererPdf.genererPdfTermine("caserne.pdf", id, missionType, missionDate, missionDetails, caserne, etatMission, compteRendu);
                        MessageBox.Show("le pdf à été générer");
                    }
                    else
                    {
                        GenererPdf.genererPdfEnCours("caserne.pdf", id, missionType, missionDate, missionDetails, caserne, etatMission, compteRendu);
                        MessageBox.Show("le pdf à été générer");
                    }
                    
                };

                pdfUserControl.PictureBox2Clicked += (sender, e) =>
                {
                    MessageBox.Show("Test click");
                };

                newUserControl.Location = new Point(20, 40 + (position * 120));
                pdfUserControl.Location = new Point(490, 40 + (position * 120));
               
                newUserControl.Tag = "mission";
                pdfUserControl.Tag = "mission";
               
                panel.Controls.Add(newUserControl);
                panel.Controls.Add(pdfUserControl);
                position++;
                
            }
        }

        public Boolean etat(int i)
        {
            DataTable dt = MesDatas.DsGlobal.Tables["MissionEnCours"];
            DataRow row = dt.Rows[i];
            int etat = Convert.ToInt32(row["etatMission"]);
            if(etat == 1)
            {
                return true;
            }
            return false;
            
        }

       


        
        

    }
}
