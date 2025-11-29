using Org.BouncyCastle.Ocsp;
using Pinpon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
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


                DataTable dtMission = MesDatas.DsGlobal.Tables["Mission"];
                DataTable dtNature = MesDatas.DsGlobal.Tables["NatureSinistre"];
                DataTable dtCaserne = MesDatas.DsGlobal.Tables["Caserne"];
               
                if (MesDatas.DsGlobal.Tables.Contains("MissionEnCours"))
                {
                    MesDatas.DsGlobal.Tables["MissionEnCours"].Clear();
                }
                else
                {
                    DataTable newDt = new DataTable("MissionEnCours");
                    newDt.Columns.Add("id");
                    newDt.Columns.Add("missionType");
                    newDt.Columns.Add("missionDate");
                    newDt.Columns.Add("dateHeureRetour");
                    newDt.Columns.Add("missionDetails");
                    newDt.Columns.Add("caserne");
                    newDt.Columns.Add("etatMission");
                    newDt.Columns.Add("compteRendu");
                    newDt.Columns.Add("motifAppel");
                    newDt.Columns.Add("adresse");
                    newDt.Columns.Add("ville");
                    MesDatas.DsGlobal.Tables.Add(newDt);
                }

                DataTable dtResult = MesDatas.DsGlobal.Tables["MissionEnCours"];



                foreach (DataRow mission in dtMission.Rows)
                {
                    string idNature = mission["idNatureSinistre"].ToString();
                    string idCaserne = mission["idCaserne"].ToString();

                    string missionType = "Inconnu";
                    foreach (DataRow row in dtNature.Rows)
                    {
                        if (row["id"].ToString() == idNature)
                        {
                            missionType = row["libelle"].ToString();
                            break;
                        }
                    }

                    string caserne = "Inconnue";
                    foreach (DataRow row in dtCaserne.Rows)
                    {
                        if (row["id"].ToString() == idCaserne)
                        {
                            caserne = row["nom"].ToString();
                            break;
                        }
                    }

                    dtResult.Rows.Add(
                        mission["id"],
                        missionType,
                        mission["dateHeureDepart"],
                        mission["dateHeureRetour"],
                        mission["motifAppel"],
                        caserne,
                        mission["terminee"],
                        mission["compteRendu"],
                        mission["motifAppel"],
                        mission["adresse"],
                        mission["ville"]
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur chargement mémoire : " + ex.Message);
            }
        }

        public void afficherMissions(Panel panel, int filtreEtat = 1)
        {
            DataTable dtPompiers = MesDatas.DsGlobal.Tables["pompier"];
            DataTable dtMobiliser = MesDatas.DsGlobal.Tables["mobiliser"];
            DataTable dtPasser = MesDatas.DsGlobal.Tables["Passer"];
            DataTable dtHabilitations = MesDatas.DsGlobal.Tables["Habilitation"];
            DataTable dtEngin = MesDatas.DsGlobal.Tables["Engin"];
            DataTable dtTypeEngin = MesDatas.DsGlobal.Tables["TypeEngin"];
            DataTable dtEmbarquer = MesDatas.DsGlobal.Tables["Embarquer"];
            DataTable dtNecessiter = MesDatas.DsGlobal.Tables["Necessiter"];
            DataTable dtMissions = MesDatas.DsGlobal.Tables["Mission"];
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
                string heureRetour = row["dateHeureRetour"].ToString();
                string motifAppel = row["motifAppel"].ToString();
                string adresse = row["adresse"].ToString();
                string ville = row["ville"].ToString();

                string idf = $"id : {id}";
                UserControl1 newUserControl = new UserControl1(missionType, missionDate, caserne, missionDetails, idf, @"../../Image/mission.png");

                UserContrltdb2.UserControl1 pdfUserControl = new UserContrltdb2.UserControl1(@"../../Image/cloturermission.png", @"../../Image/pdf.png");

                pdfUserControl.PictureBox1Clicked += delegate (object sender, EventArgs e)
                {

                    if (etat(index))
                    {
                        MessageBox.Show("La mission est déjà terminée.");
                    }
                    else
                    {
                        FrmCommentaire form = new FrmCommentaire(id);
                        DialogResult result = form.ShowDialog();

                        if (result == DialogResult.OK)
                        {
                            CloturerMission(id, form.CompteRendu, form.Reparations);
                            Chargement();
                            afficherMissions(panel, filtreEtat);
                        }

                        form.Dispose();


                        
                    }
                    MessageBox.Show("Le PDF a été généré.");
                };

                pdfUserControl.PictureBox2Clicked += delegate (object sender, EventArgs e)
                {
                    if (etat(index))
                    {
                        List<string> listePompiers = ajouterPompier(id, dtMobiliser, dtPompiers, dtPasser, dtHabilitations);
                        List<string> listeEngin = ajouterEngin(id, dtMissions, dtNecessiter, dtTypeEngin);
                        GenererPdf.genererPdfTermine($"misssion{id}.pdf", id, missionType, missionDate, missionDetails, caserne, etatMission, compteRendu, listePompiers, heureRetour, motifAppel, adresse, ville, listeEngin);
                    }
                    else
                    {
                        List<string> listePompiers = ajouterPompier(id, dtMobiliser, dtPompiers, dtPasser, dtHabilitations);
                        List<string> listeEngin = ajouterEngin(id, dtMissions, dtNecessiter, dtTypeEngin);
                        GenererPdf.genererPdfEnCours("caserne.pdf", id, missionType, missionDate, missionDetails, caserne, etatMission, compteRendu, listePompiers, heureRetour, motifAppel, adresse, ville, listeEngin);
                        MessageBox.Show("Test");
                    }
                    MessageBox.Show("Le PDF a été généré.");
                };

                newUserControl.Location = new Point(90, 40 + (position * 170));
                pdfUserControl.Location = new Point(800, 40 + (position * 170));

                newUserControl.Tag = "mission";
                pdfUserControl.Tag = "mission";

                panel.Controls.Add(newUserControl);
                panel.Controls.Add(pdfUserControl);

                position++;
            }
        }

        public static List<string> ajouterPompier(string idMission, DataTable dtMobiliser,  DataTable dtPompiers, DataTable dtPasser, DataTable dtHabilitations)
        {
            List<string> pompiers = new List<string>();

            foreach (DataRow ligne in dtMobiliser.Rows)
            {
                if (ligne["idMission"].ToString() != idMission)
                    continue;

                string idPompier = ligne["matriculePompier"].ToString();
                string idHabilitationRequise = ligne["idHabilitation"].ToString(); // habilitation demandé

                // Chercher le pompier
                DataRow pompierInfo = null;
                foreach (DataRow p in dtPompiers.Rows)
                {
                    if (p["matricule"].ToString() == idPompier)
                    {
                        pompierInfo = p;
                        break;
                    }
                }

                if (pompierInfo != null)
                {
                    string nom = pompierInfo["nom"].ToString();
                    string prenom = pompierInfo["prenom"].ToString();

                  
                    string libelleHabilitation = "";

                    foreach (DataRow passerRow in dtPasser.Rows)
                    {
                        if (passerRow["matriculePompier"].ToString() == idPompier &&
                            passerRow["idHabilitation"].ToString() == idHabilitationRequise)
                        {
                            // Chercher le libellé
                            foreach (DataRow habilitationRow in dtHabilitations.Rows)
                            {
                                if (habilitationRow["id"].ToString() == idHabilitationRequise)
                                {
                                    libelleHabilitation = habilitationRow["libelle"].ToString();
                                    break;
                                }
                            }
                            break; // habilitation trouvé
                        }
                    }

                    string ligneTexte = $"{prenom} {nom}" + (!string.IsNullOrEmpty(libelleHabilitation) ? $" ({libelleHabilitation})" : "");
                    pompiers.Add(ligneTexte);
                }
            }

            return pompiers;
        }

       
        private int getId(string idMission, DataTable dtMissions)
        {
            foreach (DataRow row in dtMissions.Rows)
            {
                if (row["id"].ToString() == idMission)
                {
                    return Convert.ToInt32(row["idnatureSinistre"]);
                }
            }
            return -1; // ou une valeur qui indique "non trouvé"
        }

       
        private List<string> ajouterEngin(string idMission,DataTable dtMissions, DataTable dtNecessiter, DataTable dtTypeEngin)
        {
            List<string> nomsEngins = new List<string>();

            int idNature = getId(idMission, dtMissions);
            if (idNature == -1)
                return nomsEngins; // nature non trouvée

            List<string> codesTypes = new List<string>();

            // Récupérer les codes des types d'engins requis
            foreach (DataRow row in dtNecessiter.Rows)
            {
                if (Convert.ToInt32(row["idnatureSinistre"]) == idNature)
                {
                    string code = row["codeTypeEngin"].ToString();
                    if (!codesTypes.Contains(code))
                    {
                        codesTypes.Add(code);
                    }
                }
            }

            // Pour chaque code, récupérer le nom correspondant
            foreach (string code in codesTypes)
            {
                foreach (DataRow typeRow in dtTypeEngin.Rows)
                {
                    if (typeRow["code"].ToString() == code)
                    {
                        string nom = typeRow["nom"].ToString();
                        if (!nomsEngins.Contains(nom))
                        {
                            nomsEngins.Add(nom);
                        }
                        break;
                    }
                }
            }

            return nomsEngins;
        }





        public bool etat(int i)
        {
            DataTable dt = MesDatas.DsGlobal.Tables["MissionEnCours"];
            DataRow row = dt.Rows[i];
            int etat = Convert.ToInt32(row["etatMission"]);
            return etat == 1;
        }

        public void CloturerMission(string idMission, string compteRendu, Dictionary<string, string> reparation)
        {
            SQLiteTransaction transaction = null;
            try
            {
                // recup des engins mobilisés 
                DataTable dt = MesDatas.DsGlobal.Tables["Mobiliser"];
                List<DataRow> pomp = new List<DataRow>();
                int compteur = 0;

                foreach (DataRow row in dt.Rows)
                {

                    if (row["idMission"].ToString() == idMission)
                    {
                        pomp.Add(row);
                        compteur++;
                    }
                }

                DataRow missionRow = null;
                foreach (DataRow row in MesDatas.DsGlobal.Tables["Mission"].Rows)
                {
                    if (row["id"].ToString() == idMission)
                    {
                        missionRow = row;
                        break;
                    }
                }

                if (missionRow == null)
                {
                    MessageBox.Show("Mission non trouvée !");
                    return;
                }

                transaction = Connexion.Connec.BeginTransaction();
                SQLiteCommand cd = new SQLiteCommand();
                cd.Connection = Connexion.Connec;
                cd.Transaction = transaction;

                try
                {
                    cd.CommandText = $"SELECT COUNT(*) FROM Mission WHERE id = {missionRow["id"]}";
                    long count = (long)cd.ExecuteScalar();

                    if (count == 0)
                    {  // si la mission n'existe pas encore dans la base

                        string req = $@"
                            INSERT INTO Mission 
                            (id, dateHeureDepart, motifAppel, adresse, cp, ville, terminee, idNatureSinistre, idCaserne)
                            VALUES (
                                {missionRow["id"]},
                                '{missionRow["dateHeureDepart"]}',
                                '{missionRow["motifAppel"]}',
                                '{missionRow["adresse"]}',
                                '{missionRow["cp"]}',
                                '{missionRow["ville"]}',
                                {Convert.ToInt32(missionRow["terminee"])},
                                {missionRow["idNatureSinistre"]},
                                {missionRow["idCaserne"]}
                            )";
                        cd.CommandText = req;
                        int nb = cd.ExecuteNonQuery();
                        if (nb == 0)
                        {
                            MessageBox.Show("Échec de l'insertion de la mission");
                        }

                        // Insert pomp mobilisés
                        if (MesDatas.DsGlobal.Tables.Contains("Mobiliser"))
                        {
                            foreach (DataRow mobiliserRow in MesDatas.DsGlobal.Tables["Mobiliser"].Rows)
                            {
                                if (mobiliserRow["idMission"].ToString() == idMission)
                                {
                                    string matriculePompier = mobiliserRow["matriculePompier"].ToString();
                                    string mission = mobiliserRow["idMission"].ToString();
                                    string idHabilitation = $"'{mobiliserRow["idHabilitation"].ToString().Replace("'", "''")}'";

                                    req = $@"
                                    INSERT INTO Mobiliser 
                                    (matriculePompier, idMission, idHabilitation)
                                    VALUES 
                                    ('{matriculePompier.Replace("'", "''")}', 
                                     '{mission.Replace("'", "''")}', 
                                     {idHabilitation})";
                                    cd.CommandText = req;
                                    cd.ExecuteNonQuery();
                                }
                            }
                        }

                        // engins utilisés
                        if (MesDatas.DsGlobal.Tables.Contains("PartirAvec"))
                        {
                            foreach (DataRow partirAvecRow in MesDatas.DsGlobal.Tables["PartirAvec"].Rows)
                            {
                                if (partirAvecRow["idMission"].ToString() == idMission)
                                {
                                    string idCaserne = partirAvecRow["idCaserne"].ToString();
                                    string codeTypeEngin = partirAvecRow["codeTypeEngin"].ToString().Replace("'", "''");
                                    string numeroEngin = partirAvecRow["numeroEngin"].ToString().Replace("'", "''");
                                    string idMissionValue = partirAvecRow["idMission"].ToString();

                                    req = $@"
                                    INSERT INTO PartirAvec 
                                    (idCaserne, codeTypeEngin, numeroEngin, idMission)
                                    VALUES 
                                    ({idCaserne}, '{codeTypeEngin}', '{numeroEngin}', {idMissionValue})";
                                    cd.CommandText = req;
                                    cd.ExecuteNonQuery();
                                }
                            }
                        }

                    }

                    cd.CommandText = @"update Mission 
                            set terminee = 1,
                                dateHeureRetour = DATETIME('now'),
                                compteRendu = '" + compteRendu + @"'
                            where id = " + idMission;
                    cd.ExecuteNonQuery();

                    missionRow["terminee"] = 1;
                    missionRow["dateHeureRetour"] = DateTime.Now;
                    missionRow["compteRendu"] = compteRendu;

                    cd.CommandText = @"delete from Mobiliser
                             where idMission = " + idMission;
                    cd.ExecuteNonQuery();

                    foreach (KeyValuePair<string, string> kvp in reparation)
                    {
                        string[] keyParts = kvp.Key.Split('|');
                        string codeTypeEngin = keyParts[0];
                        int numeroEngin = int.Parse(keyParts[1]);
                        string commentaire = kvp.Value;

                        cd.CommandText = @"update PartirAvec
                        set reparationsEventuelles = '" + commentaire.Replace("'", "''") + @"'
                        where idMission = " + idMission + @"
                        and codeTypeEngin = '" + codeTypeEngin + @"'
                        and numeroEngin = " + numeroEngin;
                        cd.ExecuteNonQuery();

                        cd.CommandText = @"update Engin
                            set enPanne = 1
                            where codeTypeEngin = '" + codeTypeEngin + @"'
                            and numeroEngin = " + numeroEngin;
                        cd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    MessageBox.Show("Mission clôturée avec succès.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Erreur lors de la clôture de la mission : " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de connexion à la base de données : " + ex.Message);
            }
        }
    }
}

