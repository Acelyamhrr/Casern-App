using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using controlePompiersHabilit;
using controleStatistiquesEngin;
using controleStatistiquesIntervParType;
using Pinpon;

namespace SAE_Caserne.Classe
{
    internal class statistiques
    {
        public statistiques() { }
        public void rempliCboChoixCaserne(ComboBox cbo)
        {
            try
            {
                string requete = @"SELECT * FROM Caserne;";
                SQLiteConnection connection = Connexion.Connec;
                SQLiteCommand cd = new SQLiteCommand(requete, connection);
                SQLiteDataReader dr = cd.ExecuteReader();

                while (dr.Read())
                {
                    cbo.Items.Add(dr["nom"].ToString());
                    //vu que id caserne va dans l'ordre de 1 à (ici) 4 on fera selectedindex+1
                }

            }
            catch (SQLiteException)
            {
                MessageBox.Show("Erreur de requête SQL");
            }
            finally
            {

            }
        }


        public void statEnsembleCasernes(TabPage IntervParSinistre, TabPage TopHabilit, TabPage PompierParHabilit)
        {
            try
            {
                IntervParSinistre.Controls.Clear(); // On vide le TabPage avant de le remplir
                TopHabilit.Controls.Clear(); // On vide le TabPage avant de le remplir  
                PompierParHabilit.Controls.Clear(); // On vide le TabPage avant de le remplir
                SQLiteConnection connection = Connexion.Connec;


                string requeteIntervParSinistre = @"SELECT n.libelle AS ""Type d'intervention"",
                                                    count(*) AS ""Nbr interventions"",
                                                    n.id AS ""nature sinistre""
                                                    FROM Mission m 
                                                    JOIN NatureSinistre n 
                                                    ON m.idNatureSinistre = n.id GROUP BY(n.id)
                                                    ORDER BY n.id DESC";


                SQLiteCommand cdIntervParSinistre = new SQLiteCommand(requeteIntervParSinistre, connection);
                SQLiteDataAdapter daIntervParSinistre = new SQLiteDataAdapter(cdIntervParSinistre);
                DataTable dtIntervParSinistre = new DataTable();
                daIntervParSinistre.Fill(dtIntervParSinistre);


                for (int i = 0; i < dtIntervParSinistre.Rows.Count; i++) //Pour Drawing.Point pas à 0,0 au debut
                {

                    int hauteur = (i / 3);


                    controleStatistiquesIntervParType.ctrlIntervParTypeSin statsIntervParType = new ctrlIntervParTypeSin(dtIntervParSinistre.Rows[i][0].ToString(),
                        dtIntervParSinistre.Rows[i][1].ToString() + " fois", @"..\\..\\..\\ImagesSinistres\\" + dtIntervParSinistre.Rows[i][2].ToString() + ".png");


                    statsIntervParType.BackColor = System.Drawing.Color.FromArgb(75, 79, 85);
                    statsIntervParType.AutoSize = true; // Automatically adjust size based on content  
                    IntervParSinistre.Controls.Add(statsIntervParType);
                    statsIntervParType.Size = new System.Drawing.Size(320, 120);
                    statsIntervParType.Location = new System.Drawing.Point(10 + (i % 3) * 345 , 20 + hauteur * 140);
                }




                string requeteTopHabilit = @"SELECT h.libelle AS ""Type d'habilitation"",
                                    count(*) AS ""Solicitation"", 
                                    h.id AS ""idHabilitation""
                                    FROM Mobiliser m
                                    JOIN Habilitation h ON m.idHabilitation = h.id
                                    GROUP BY(h.id)
                                    ORDER BY count(*) DESC";

                SQLiteCommand cdTopHabilit = new SQLiteCommand(requeteTopHabilit, connection);
                SQLiteDataAdapter daTopHabilit = new SQLiteDataAdapter(cdTopHabilit);
                DataTable dtTopHabilit = new DataTable();
                daTopHabilit.Fill(dtTopHabilit);




                for (int i = 0; i < dtTopHabilit.Rows.Count; i++) //Pour Drawing.Point pas à 0,0 au debut
                {

                    int hauteur = (i / 2);


                    controleStatistiquesIntervParType.ctrlIntervParTypeSin ctrlTopHabilit = new ctrlIntervParTypeSin(dtTopHabilit.Rows[i][0].ToString(),
                        dtTopHabilit.Rows[i][1].ToString() + " fois", @"..\\..\\..\\ImagesHabilitations\\" + dtTopHabilit.Rows[i][2].ToString() + ".png");


                    ctrlTopHabilit.BackColor = System.Drawing.Color.FromArgb(75, 79, 85);
                    ctrlTopHabilit.AutoSize = true; // Automatically adjust size based on content  
                    TopHabilit.Controls.Add(ctrlTopHabilit);
                    ctrlTopHabilit.Size = new System.Drawing.Size(470, 120); // Set a fixed size for the control
                    ctrlTopHabilit.Location = new System.Drawing.Point(20 + (i % 2) * 530, 20 + hauteur * 140);
                }









                string requetePompierParHabilit = @"SELECT h.libelle AS ""Habilitation"", 
                                CONCAT(pinpon.nom, ' ', pinpon.prenom) AS ""Nom"",
                                pass.dateObtention AS ""Date d'obtention"",
                                h.id AS ""idHabilitation""
                                FROM Habilitation h
                                LEFT JOIN Passer pass ON h.id = pass.idHabilitation
                                LEFT JOIN Pompier pinpon ON pass.matriculePompier = pinpon.matricule
                                ORDER BY h.id ASC";

                SQLiteCommand cdPompierParHabilit = new SQLiteCommand(requetePompierParHabilit, connection);
                SQLiteDataAdapter daPompierParHabilit = new SQLiteDataAdapter(cdPompierParHabilit);
                DataTable dtPompierParHabilit = new DataTable();
                daPompierParHabilit.Fill(dtPompierParHabilit);


                for (int i = 0; i < dtPompierParHabilit.Rows.Count; i++) //Pour Drawing.Point pas à 0,0 au debut
                {

                    int hauteur = (i / 2);



                    controlePompiersHabilit.ctrlPompHabilit ctrlPompHabilitee = new ctrlPompHabilit(dtPompierParHabilit.Rows[i][0].ToString(),
                        dtPompierParHabilit.Rows[i][1].ToString() + " fois", "Obtenue le : " + dtPompierParHabilit.Rows[i][2].ToString(),
                        @"..\\..\\..\\ImagesHabilitations\\" + dtPompierParHabilit.Rows[i][3].ToString() + ".png");


                    ctrlPompHabilitee.BackColor = System.Drawing.Color.FromArgb(75, 79, 85);
                    ctrlPompHabilitee.AutoSize = true; // Automatically adjust size based on content  
                    PompierParHabilit.Controls.Add(ctrlPompHabilitee);
                    ctrlPompHabilitee.Size = new System.Drawing.Size(400, 120);
                    ctrlPompHabilitee.Location = new System.Drawing.Point(20 + (i % 2) * 600, 20 + hauteur * 140);
                }



            }
            catch (SQLiteException)
            {
                MessageBox.Show("Erreur de requête SQL");
            }

        }

        public void statCaserne(ComboBox cbo, TabPage pageEnginUtil, TabPage pageCumulHeureEngin)
        {
            try
            {
                pageEnginUtil.Controls.Clear();
                pageCumulHeureEngin.Controls.Clear();

                SQLiteConnection connection = Connexion.Connec;


                string requeteEnginPlusUtilis = @"SELECT CONCAT(codeTypeEngin, numeroEngin) AS ""Engin"",
                                                count(*) AS ""Nombre"",
                                                codeTypeEngin AS ""codeImage""
                                                FROM PartirAvec 
                                                WHERE idCaserne =" + (cbo.SelectedIndex + 1) + @"
                                                GROUP BY codeTypeEngin, numeroEngin 
                                                ORDER BY count(*) DESC;";

                SQLiteCommand cdEnginPlusUtilis = new SQLiteCommand(requeteEnginPlusUtilis, connection);
                SQLiteDataAdapter daEnginPlusUtilis = new SQLiteDataAdapter(cdEnginPlusUtilis);
                DataTable dtEnginPlusUtilis = new DataTable();
                daEnginPlusUtilis.Fill(dtEnginPlusUtilis);




                for (int i = 0; i < dtEnginPlusUtilis.Rows.Count; i++) //Pour Drawing.Point pas à 0,0 au debut
                {

                    int hauteur = (i / 6);


                    controleStatistiquesEngin.ctrlStatsEngins statsengins = new ctrlStatsEngins(dtEnginPlusUtilis.Rows[i][0].ToString(),
                        dtEnginPlusUtilis.Rows[i][1].ToString() + " fois", @"..\\..\\..\\ImagesEngins\\" + dtEnginPlusUtilis.Rows[i][2].ToString() + ".png");


                    statsengins.BackColor = System.Drawing.Color.FromArgb(75, 79, 85);
                    statsengins.AutoSize = true; // Automatically adjust size based on content  
                    pageEnginUtil.Controls.Add(statsengins);
                    statsengins.Location = new System.Drawing.Point(10 + (i % 6) * (150 + 10), 20 + hauteur * 260);
                }






                string requetetotalHeureEngin = @"SELECT CONCAT(p.codeTypeEngin, p.numeroEngin) AS ""Engin"",
                                        ROUND((SUM((julianday(m.dateHeureRetour) - julianday(m.dateHeureDepart)))*24), 0)
                                                                            AS ""Cumul heures d'utilisation"",
                                        p.codeTypeEngin AS ""codeImage""
                                        FROM Mission m join PartirAvec p
                                        ON m.idCaserne = p.idCaserne
                                        WHERE m.idCaserne =" + (cbo.SelectedIndex + 1) + @"
                                        GROUP BY Engin";

                SQLiteCommand cdtotalHeureEngin = new SQLiteCommand(requetetotalHeureEngin, connection);
                SQLiteDataAdapter datotalHeureEngin = new SQLiteDataAdapter(cdtotalHeureEngin);
                DataTable dttotalHeureEngin = new DataTable();
                datotalHeureEngin.Fill(dttotalHeureEngin);

                for (int i = 0; i < dtEnginPlusUtilis.Rows.Count; i++) //Pour Drawing.Point pas à 0,0 au debut
                {

                    int hauteur = (i / 6);


                    controleStatistiquesEngin.ctrlStatsEngins statsengins = new ctrlStatsEngins(dttotalHeureEngin.Rows[i][0].ToString(),
                        dttotalHeureEngin.Rows[i][1].ToString() + " heure(s)", @"..\\..\\..\\ImagesEngins\\" + dttotalHeureEngin.Rows[i][2].ToString() + ".png");


                    statsengins.BackColor = System.Drawing.Color.FromArgb(75, 79, 85);
                    statsengins.AutoSize = true; // Automatically adjust size based on content  
                    pageCumulHeureEngin.Controls.Add(statsengins);
                    //statsengins.Location = new System.Drawing.Point(i * (60 + 10), 20 + hauteur * statsengins.Height);
                    statsengins.Location = new System.Drawing.Point(10 + (i % 6) * (150 + 10), 20 + hauteur * 150);
                }

            }

            catch (SQLiteException)
            {
                MessageBox.Show("Erreur de requête SQL");
            }


        }
    }
}
