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
                        c.nom AS caserne
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

        public void afficherMissions(Panel panel)
        {
            DataTable dt = MesDatas.DsGlobal.Tables["MissionEnCours"];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];

                string id = row["id"].ToString();
                string missionType = row["missionType"].ToString();
                string missionDate = row["missionDate"].ToString();
                string missionDetails = row["missionDetails"].ToString();
                string caserne = row["caserne"].ToString();

                UserControlMission.UserControl1 newUserControl = new UserControlMission.UserControl1(
                    missionType,
                    missionDate,
                    caserne,
                    missionDetails,
                    id,
                    @"C:\Users\Cl1en\Downloads\test.jpg" 
                );

                newUserControl.Location = new Point(20, 40 + (i * 120));
                
                panel.Controls.Add(newUserControl);
            }
        }

        
    }
}
