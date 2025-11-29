using iText.Layout.Element;
using monUC;
using Pinpon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UCajtHabModif;
using UCHabilitations;

namespace SAE_Caserne.Classe
{
    internal class AffichagePompiers
    {
        TabControl tab;
        int matrPompier = 0;
        Dictionary<int, DateTime> habilitationsAAjouter = new Dictionary<int, DateTime>();
        List<int> habilitationsAsupprimer = new List<int>();
        bool administrateur = false;


        public AffichagePompiers(TabControl tab, int matr, bool administrateur)
        {
            this.tab = tab;
            this.matrPompier = matr;
            this.administrateur = administrateur;
        }

        public void ChargerCaserne(ComboBox cbo)
        {
            DataTable dt = new DataTable();
            string requete = @"select * from Caserne order by nom";
            SQLiteDataAdapter da = new SQLiteDataAdapter(requete, Connexion.Connec);
            da.Fill(dt);

            cbo.DataSource = dt;
            cbo.DisplayMember = "nom";
            cbo.ValueMember = "id";
        }

        public void ChargerPompiersDeCaserne(int idCaserne, Panel pnl)
        {

            pnl.Controls.Clear();

            string req = @"select p.matricule, p.nom || ' ' || p.prenom as nom, g.libelle as grade, p.type as type, p.enMission, p.enConge, p.nom, p.prenom, a.idCaserne, p.codeGrade, p.portable 
                    from Pompier p
                    join Grade g on p.codeGrade = g.code
                    join Affectation a on p.matricule = a.matriculePompier
                    where idCaserne = " + idCaserne + @"
                        and a.dateFin is null
                    order by p.nom, p.prenom";

            SQLiteCommand cmdPomp = new SQLiteCommand(req, Connexion.Connec);
            SQLiteDataReader readerPomp = cmdPomp.ExecuteReader();

            int y = 10;

            while (readerPomp.Read())
            {
                int matricule = readerPomp.GetInt32(0);
                string nom = readerPomp.GetString(1);
                string grade = readerPomp.GetString(2);
                string type = readerPomp.GetString(3);
                bool enMission = readerPomp.GetBoolean(4);
                bool enConge = readerPomp.GetBoolean(5);
                string nomPompier = readerPomp.GetString(6);
                string prenomPompier = readerPomp.GetString(7);
                string idCas = readerPomp.GetInt32(8).ToString();
                string codeGrade = readerPomp.GetString(9);
                string portable = readerPomp.GetString(10);

                string statut = "Disponible";
                int statutEnConge = 0;
                if (enMission)
                {

                    statut = "En mission";
                }
                else if (enConge)
                {
                    statut = "En congé";
                    statutEnConge = 1;
                }

                string typeOk = type;
                if (type == "p")
                {
                    typeOk = "Professionnel";
                }
                else if (type == "v")
                {
                    typeOk = "Volontaire";
                }

                CartePompier uc = new CartePompier(nom, grade, typeOk, statut, matricule, nomPompier, prenomPompier, statutEnConge, idCas, codeGrade, portable, type);
                uc.Name = "uc" + matricule;
                uc.Left = 10;
                uc.Top = y;
                uc.Width = pnl.Width - 20;
                uc.Details = AfficherDetails;
                uc.Modifier = AfficherModif;
                uc.Matricule = matricule;

                pnl.Controls.Add(uc);
                y += uc.Height + 5;
            }

            readerPomp.Close();
        }

        public void AfficherDetails(object sender, EventArgs args)
        {
            tab.SelectedIndex = 3;

            Button btn = (Button)sender;
            Control ctrl = btn.Parent;
            while (ctrl != null && !(ctrl is CartePompier))
            {
                ctrl = ctrl.Parent;
            }

            CartePompier uc = ctrl as CartePompier;

            TabPage tabDetails = tab.TabPages[3];
            Panel pnlFiche = (Panel)tabDetails.Controls["pnlInfosPlusFiche"];
            Panel pnlPlusInfosPerso = (Panel)tabDetails.Controls["pnlPlusInfosPerso"];
            Panel pnlHab = (Panel)tabDetails.Controls["pnlHab"];
            Panel pnlAffect = (Panel)tabDetails.Controls["pnlAffect"];

            TextBox txtNomPrenomPlus = (TextBox)tabDetails.Controls["txtNomPrenomPlus"];
            TextBox txtCasernePlus = (TextBox)tabDetails.Controls["txtCasernePlus"];
            TextBox txtMatrGrade = (TextBox)tabDetails.Controls["txtMatrGradePlus"];
            Label lblNomPrenom = (Label)pnlFiche.Controls["lblNomPrenomPlus"];
            Label lblGradeType = (Label)pnlFiche.Controls["lblGradeType"];
            Label tel = (Label)pnlPlusInfosPerso.Controls["lblTelPlus"];
            Label lblDateNaiss = (Label)pnlPlusInfosPerso.Controls["lblDateNaissPlus"];
            Label lblBip = (Label)pnlPlusInfosPerso.Controls["lblBipPlus"];
            Label lblEnConge = (Label)pnlFiche.Controls["lblEnConge"];
            PictureBox pic = (PictureBox)pnlFiche.Controls["picInfosPlus"];


            int idPompier = uc.Matricule;

            string req = @"select p.portable, p.dateNaissance, p.sexe, p.bip, p.enMission, p.enConge, g.libelle, p.type
                    from Pompier p
                    join Grade g
                    on p.codeGrade = g.code
                    where p.matricule = " + idPompier;

            SQLiteCommand cmd = new SQLiteCommand(req, Connexion.Connec);
            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string portable = reader.GetString(0);
                DateTime dateNaissance = reader.GetDateTime(1);
                string sexe = reader.GetString(2);
                int bip = reader.GetInt32(3);
                int enMission = reader.GetInt32(4);
                int enConge = reader.GetInt32(5);
                string gradeLibelle = reader.GetString(6);
                string type = reader.GetString(7);

                if (type == "p")
                {
                    type = "Professionnel";
                }
                else if (type == "v")
                {
                    type = "Volontaire";
                }

                // Affectation aux composants
                tel.Text = portable.ToString();
                lblDateNaiss.Text = dateNaissance.ToString("dd/MM/yyyy");
                lblBip.Text = bip.ToString();

                // Gestion de l'état
                if (enMission == 1)
                {
                    lblEnConge.Text = "En mission";
                    lblEnConge.ForeColor = Color.Red;
                }
                else if (enConge == 1)
                {
                    lblEnConge.Text = "En congé";
                    lblEnConge.ForeColor = Color.Red;
                }
                else
                {
                    lblEnConge.Text = "Disponible";
                    lblEnConge.ForeColor = Color.Green;
                }

                // Informations générales
                txtNomPrenomPlus.Text = "Fiche détaillé - " + uc.Nom;
                lblNomPrenom.Text = uc.Nom;
                lblGradeType.Text = gradeLibelle + " - " + type;

                pic.Image = System.Drawing.Image.FromFile(@"..\..\..\ImagesSexePompier\" + sexe + ".png");


            }
            reader.Close();


            ComboBox cboGestPompier = (ComboBox)tab.TabPages[4].Controls["cboGestPompiers"];
            //txtCasernePlus.Text = cboGestPompier.Text;



            ListBox lstAffect = (ListBox)pnlAffect.Controls["lstAffections"];
            ListBox lstHabil = (ListBox)pnlHab.Controls["lstHabilitations"];


            AfficherAffectionsPompier(idPompier, lstAffect);
            AfficherHabilitationsPompier(idPompier, lstHabil);
        }

        public void AfficherAffectionsPompier(int idPompier, ListBox lst)
        {
            lst.Items.Clear();
            lst.ScrollAlwaysVisible = true;

            string req = @"select a.dateA, a.dateFin, c.nom
                    from Affectation a
                    join Caserne c
                    on a.idCaserne = c.id
                    where a.matriculePompier = " + idPompier + @"
                        and a.dateFin is not null
                    order by a.dateFin desc";

            SQLiteCommand cmd = new SQLiteCommand(req, Connexion.Connec);
            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                DateTime dateDebut = reader.GetDateTime(0);
                DateTime dateFin = reader.GetDateTime(1);
                string nomCaserne = reader.GetString(2);

                lst.Items.Add($"{nomCaserne} (du {dateDebut.ToString("dd/MM/yyy")} au {dateFin.ToString("dd/MM/yyy")})");
            }

            reader.Close();
        }

        public void AfficherHabilitationsPompier(int idPompier, ListBox lst)
        {
            lst.Items.Clear();

            string req = @"select h.libelle,h.descriptif, p.dateObtention
                    from Passer p
                    join Habilitation h on p.idHabilitation = h.id
                    where p.matriculePompier = " + idPompier + @"
                    order by p.dateObtention desc";

            SQLiteCommand cmd = new SQLiteCommand(req, Connexion.Connec);
            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string libelle = reader.GetString(0);
                string descriptif = reader.GetString(1);
                DateTime date = reader.GetDateTime(2);

                lst.Items.Add($"{libelle} ({date.ToString("dd/MM/yyyy")}) - {descriptif}");
            }

            reader.Close();
        }

        public void AjouterPompier(string sexe, string nom, string prenom, DateTime dateNaiss, string tel, DateTime dateEmbauche, string type, string codeGrade, int idCaserne, Dictionary<int, string> habilitations)
        {

            try
            {
                // on génere le matricule suivant
                int prochainMatricule = GetProchainMatirucle();


                string dateNaissStr = dateNaiss.ToString("yyyy-MM-dd");
                string dateEmbaucheStr = dateEmbauche.ToString("yyyy-MM-dd");

                nom = nom.Replace("'", "''");
                prenom = prenom.Replace("'", "''");
                tel = tel.Replace("'", "''");
                type = type.Replace("'", "''");
                codeGrade = codeGrade.Replace("'", "''");
                sexe = sexe.Replace("'", "''");

                string reqAjt = @"insert into Pompier(matricule, nom, prenom, sexe, dateNaissance, portable, bip, type, dateEmbauche, codeGrade, enMission, enConge)
                    values (" + prochainMatricule + ", '" + nom + "', '" + prenom + "', '" + sexe + "', '" + dateNaissStr + "', '" + tel + "', '" + prochainMatricule + "', '" + type + "', '" + dateEmbaucheStr + "', '" + codeGrade + "', 0, 0)";
                SQLiteCommand cmdInsertPompier = new SQLiteCommand(reqAjt, Connexion.Connec);
                cmdInsertPompier.ExecuteNonQuery();

                string reqAffectation = "insert into Affectation (matriculePompier, dateA, idCaserne) " +
                                                "values (" + prochainMatricule + ", '" + dateEmbaucheStr + "', " + idCaserne + ")";
                SQLiteCommand cmdInsertAffect = new SQLiteCommand(reqAffectation, Connexion.Connec);
                cmdInsertAffect.ExecuteNonQuery();


                foreach (KeyValuePair<int, string> kvp in habilitations)
                {
                    int idHab = kvp.Key;
                    string dateObtention = kvp.Value;

                    dateObtention = dateObtention.Replace("'", "''");

                    string reqPasser = @"insert into Passer (matriculePompier, idHabilitation, dateObtention)
                    values (" + prochainMatricule + ", " + idHab + ",'" + dateObtention + "')";
                    SQLiteCommand cmdInsertPasser = new SQLiteCommand(reqPasser, Connexion.Connec);
                    cmdInsertPasser.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'ajout du pompier : " + ex.Message);
            }

        }

        public int GetProchainMatirucle()
        {
            string req = @"select max(matricule) from Pompier";
            SQLiteCommand cmd = new SQLiteCommand(req, Connexion.Connec);
            SQLiteDataReader r = cmd.ExecuteReader();

            r.Read();
            int prochainMatricule = r.GetInt32(0) + 1;

            r.Close();
            return prochainMatricule;


        }

        public void AjouterUChabilitation(Panel pnl, Button btnAjt)
        {
            UChabilitation uc = new UChabilitation();

            int y = 10;

            // on recup la position du dernier control
            if (pnl.Controls.Count > 0)
            {
                Control dernier = pnl.Controls[pnl.Controls.Count - 1];
                y = dernier.Bottom + 10;
            }

            uc.Name = "ucHabilitation" + pnl.Controls.Count;
            uc.Left = 10;
            uc.Top = y;
            uc.Width = pnl.Width - 20;

            DataTable dtHab = MesDatas.DsGlobal.Tables["Habilitation"];
            uc.CboHab.DataSource = dtHab;
            uc.CboHab.DisplayMember = "libelle";
            uc.CboHab.ValueMember = "id";

            pnl.Controls.Add(uc);

            btnAjt.Top = uc.Bottom + 10;

        }

        public void AfficherModif(object sender, EventArgs args)
        {
            if (administrateur)
            {

                tab.SelectedIndex = 6;

                Button btn = (Button)sender;
                Control ctrl = btn.Parent;
                while (ctrl != null && !(ctrl is CartePompier))
                {
                    ctrl = ctrl.Parent;
                }

                CartePompier uc = ctrl as CartePompier;
                tab.TabPages[6].Tag = uc.Matricule;
                matrPompier = uc.Matricule;
                TabPage tabModif = tab.TabPages[6];

                Panel pnl_modifInfos = (Panel)tabModif.Controls["pnl_modifInfos"];
                GroupBox grpInfosPersoModif = (GroupBox)pnl_modifInfos.Controls["grpInfosPersoModif"];
                GroupBox infosPro = (GroupBox)pnl_modifInfos.Controls["grpInfosProModif"];

                TextBox txtNom = (TextBox)grpInfosPersoModif.Controls["txtNomModif"];
                TextBox txtPrenom = (TextBox)grpInfosPersoModif.Controls["txtPrenomModif"];
                TextBox txtTel = (TextBox)grpInfosPersoModif.Controls["txtTelModif"];
                CheckBox chkEnConge = (CheckBox)infosPro.Controls["chkStatutModif"];

                txtNom.Text = uc.NomPomp;
                txtNom.Enabled = false;
                txtPrenom.Text = uc.PrenomPomp;
                txtPrenom.Enabled = false;

                if (uc.EnConges == 1)
                {
                    chkEnConge.Checked = true;
                }
                else
                {
                    chkEnConge.Checked = false;
                }

                ComboBox cboCaserneModif = (ComboBox)infosPro.Controls["cboCaserneModif"];
                ChargerCaserne(cboCaserneModif);

                ComboBox cboGrade = (ComboBox)infosPro.Controls["cboGradeModif"];


                ComboBox cboType = (ComboBox)infosPro.Controls["cboTypeModif"];

                cboGrade.DataSource = MesDatas.DsGlobal.Tables["Grade"];
                cboGrade.DisplayMember = "libelle";
                cboGrade.ValueMember = "code";

                // dataTable temporaire pour stocker le type 
                DataTable dtTypes = new DataTable();
                dtTypes.Columns.Add("codeType");
                dtTypes.Columns.Add("libelleType");

                dtTypes.Rows.Add("v", "Volontaire");
                dtTypes.Rows.Add("p", "Professionnel");

                cboType.DataSource = dtTypes;
                cboType.DisplayMember = "libelleType";
                cboType.ValueMember = "codeType";

                cboCaserneModif.SelectedValue = uc.idCas;
                cboGrade.SelectedValue = uc.CodeGrade;
                cboType.SelectedValue = uc.typePomp;
                txtTel.Text = uc.Portable;


                FlowLayoutPanel flpHab = (FlowLayoutPanel)pnl_modifInfos.Controls["flpHabilitations"];

                int matricule = uc.Matricule;

                ChargerHabilitations(matricule, flpHab, pnl_modifInfos);



            }
            else
            {
                MessageBox.Show("Vous n'avez pas les droits pour modifier les informations des pompiers.");
            }

        }

        private void ChargerHabilitations(int matricule, FlowLayoutPanel flp, Panel pnl)
        {
            flp.Controls.Clear();

            string req = @"select h.id, h.libelle, p.dateObtention
                    from Passer p
                    join Habilitation h on p.idHabilitation = h.id
                    where p.matriculePompier = " + matricule + @"
                    order by p.dateObtention desc";

            SQLiteCommand cmd = new SQLiteCommand(req, Connexion.Connec);
            SQLiteDataReader reader = cmd.ExecuteReader();

            // habilitations existantes
            while (reader.Read())
            {
                int idHab = reader.GetInt32(0);
                string libelle = reader.GetString(1);
                DateTime date = reader.GetDateTime(2);

                ucHabModif uc = new ucHabModif(idHab, libelle, date);
                uc.Suppr = SupprimerHabilitation;
                flp.Controls.Add(uc);

            }
            reader.Close();

            // ajt un hab
            UChabilitation ucAjt = new UChabilitation();
            ucAjt.Name = "ucAjt";
            ucAjt.Location = new Point(flp.Location.X, flp.Location.Y + flp.Height + 10);

            DataTable dtHab = MesDatas.DsGlobal.Tables["Habilitation"];
            ucAjt.CboHab.DataSource = dtHab;
            ucAjt.CboHab.DisplayMember = "libelle";
            ucAjt.CboHab.ValueMember = "id";

            pnl.Controls.Add(ucAjt);

            // btn pour valider l'ajout
            Button btnAjt = new Button();
            btnAjt.Text = "Ajouter une habilitation";
            btnAjt.AutoSize = true;
            btnAjt.Name = "btnAjtHab";
            btnAjt.Location = new Point(ucAjt.Location.X, ucAjt.Location.Y + ucAjt.Height + 10);
            btnAjt.Click += AjouterHabilitation;
            pnl.Controls.Add(btnAjt);

        }

        private void SupprimerHabilitation(object sender, EventArgs e)
        {
            ucHabModif uc = (ucHabModif)sender;
            FlowLayoutPanel flp = (FlowLayoutPanel)uc.Parent;
            habilitationsAsupprimer.Add(Convert.ToInt32(uc.IdHabilitation));
            flp.Controls.Remove(uc);

        }

        private void AjouterHabilitation(object sender, EventArgs e)
        {
            TabPage tabModif = tab.TabPages[6];

            Panel pnl_modifInfos = (Panel)tabModif.Controls["pnl_modifInfos"];
            FlowLayoutPanel flp = (FlowLayoutPanel)pnl_modifInfos.Controls["flpHabilitations"];
            UChabilitation ucAjt = (UChabilitation)pnl_modifInfos.Controls["ucAjt"];

            int idHab = Convert.ToInt32(ucAjt.CboHab.SelectedValue);
            DateTime date = ucAjt.DateObtention;

            if (!habilitationsAAjouter.ContainsKey(idHab))
            {
                habilitationsAAjouter.Add(idHab, date);

                ucHabModif uc = new ucHabModif(idHab, ucAjt.CboHab.Text, date);
                uc.Suppr = SupprimerHabAjout;

                flp.Controls.Add(uc);
            }
            else
            {
                MessageBox.Show("Cette habilitation a déjà été ajoutée.");
            }

        }

        private void SupprimerHabAjout(object sender, EventArgs e)
        {
            ucHabModif uc = (ucHabModif)sender;
            FlowLayoutPanel flp = (FlowLayoutPanel)uc.Parent;
            int idHab = Convert.ToInt32(uc.IdHabilitation);
            flp.Controls.Remove(uc);
            habilitationsAAjouter.Remove(idHab);



        }

        public void ValiderTransaction(Panel pnl)
        {
            GroupBox grpPerso = (GroupBox)pnl.Controls["grpInfosPersoModif"];
            GroupBox grpPro = (GroupBox)pnl.Controls["grpInfosProModif"];




            SQLiteTransaction maTransac = null;
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = Connexion.Connec;

            try
            {
                maTransac = Connexion.Connec.BeginTransaction();
                cmd.Transaction = maTransac;

                ComboBox cboType = (ComboBox)grpPro.Controls["cboTypeModif"];
                TextBox txtPortable = (TextBox)grpPerso.Controls["txtTelModif"];
                ComboBox cboGrade = (ComboBox)grpPro.Controls["cboGradeModif"];
                ComboBox cboCaserne = (ComboBox)grpPro.Controls["cboCaserneModif"];
                CheckBox enConge = (CheckBox)grpPro.Controls["chkStatutModif"];
                DateTime date = DateTime.Now;

                //Récupération des données actuelles du pompier
                cmd.CommandText = "SELECT portable, enConge, codeGrade, dateEmbauche FROM Pompier WHERE matricule = " + matrPompier;
                SQLiteDataReader reader = cmd.ExecuteReader();
                bool doitMajPompier = false;

                if (reader.Read())
                {

                    string portableActuel = reader.GetString(0);
                    bool enCongeActuel = reader.GetBoolean(1);
                    string codeGradeActuel = reader.GetString(2);
                    DateTime dateEmbaucheActuelle = reader.GetDateTime(3);
                    reader.Close();

                    if (portableActuel != txtPortable.Text || enCongeActuel != enConge.Checked || codeGradeActuel != cboGrade.SelectedValue.ToString())
                    {
                        doitMajPompier = true;
                    }
                }
                else
                {
                    reader.Close();
                    MessageBox.Show("Aucun pompier trouvé avec la matricule " + matrPompier);
                }

                //Mise à jour du pompier
                if (doitMajPompier)
                {
                    string req = @"UPDATE Pompier SET
                        portable = '" + txtPortable.Text.Replace("'", "''") + @"',
                        enConge = " + (enConge.Checked ? 1 : 0) + @",
                        codeGrade = '" + cboGrade.SelectedValue + @"',
                        dateEmbauche = '" + date.ToString("yyyy-MM-dd") + @"'
                        WHERE matricule = " + matrPompier;
                    cmd.CommandText = req;
                    cmd.ExecuteNonQuery();
                }

                //verif affectation actuelle
                cmd.CommandText = "SELECT idCaserne " +
                    "FROM Affectation " +
                    "WHERE matriculePompier = " + matrPompier +
                    " AND dateFin IS NULL";
                object resCaserne = cmd.ExecuteScalar();

                if (resCaserne != null && Convert.ToInt32(resCaserne) != Convert.ToInt32(cboCaserne.SelectedValue))
                {
                    //Clôture de l’ancienne
                    cmd.CommandText = "UPDATE Affectation " +
                        "SET dateFin = '" + date.ToString("yyyy-MM-dd").Replace("'", "''") + "' " +
                        "WHERE matriculePompier = " + matrPompier +
                        " AND dateFin IS NULL";
                    cmd.ExecuteNonQuery();

                    // ajt de la nouvelle affectation
                    cmd.CommandText = "INSERT INTO Affectation (matriculePompier, dateA, idCaserne) VALUES (" +
                                      matrPompier + ", '" + date.ToString("yyyy-MM-dd").Replace("'", "''") + "', " + cboCaserne.SelectedValue + ")";
                    cmd.ExecuteNonQuery();
                }

                // Suppression des habilitations
                foreach (int idHab in habilitationsAsupprimer)
                {
                    cmd.CommandText = "DELETE FROM Passer " +
                        "WHERE matriculePompier = " + matrPompier +
                        " AND idHabilitation = " + idHab;
                    cmd.ExecuteNonQuery();
                }

                // Ajout des nouvelles habilitations
                foreach (KeyValuePair<int, DateTime> kvp in habilitationsAAjouter)
                {
                    int idHab = kvp.Key;
                    DateTime dateObtention = kvp.Value;
                    string req = "INSERT INTO Passer (matriculePompier, idHabilitation, dateObtention) " +
                        "VALUES (" + matrPompier + ", " + idHab + ", '" + dateObtention.ToString("yyyy-MM-dd").Replace("'", "''") + "')";
                    cmd.CommandText = req;
                    cmd.ExecuteNonQuery();
                }

                maTransac.Commit();
                MessageBox.Show("Pompier mis à jour !");

            }
            catch (Exception ex)
            {
                maTransac.Rollback();
                MessageBox.Show("Transaction annulée !" + ex.Message);
            }
        }


        public void AnnulerModification()
        {
            TabPage tabModif = tab.TabPages[6];
            Panel pnl_modifInfos = (Panel)tabModif.Controls["pnl_modifInfos"];
            int matricule = (int)tabModif.Tag;

            string req = @"select p.nom, p.prenom, p.portable, p.enConge, p.codeGrade, p.type, a.idCaserne 
                   from Pompier p
                   join Affectation a on p.matricule = a.matriculePompier and a.dateFin IS NULL
                   where p.matricule = " + matricule;

            SQLiteCommand cmd = new SQLiteCommand(req, Connexion.Connec);
            SQLiteDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                TextBox txtNom = (TextBox)pnl_modifInfos.Controls["grpInfosPersoModif"].Controls["txtNomModif"];
                TextBox txtPrenom = (TextBox)pnl_modifInfos.Controls["grpInfosPersoModif"].Controls["txtPrenomModif"];
                TextBox txtTel = (TextBox)pnl_modifInfos.Controls["grpInfosPersoModif"].Controls["txtTelModif"];
                CheckBox chkEnConge = (CheckBox)pnl_modifInfos.Controls["grpInfosProModif"].Controls["chkStatutModif"];
                ComboBox cboCaserneModif = (ComboBox)pnl_modifInfos.Controls["grpInfosProModif"].Controls["cboCaserneModif"];
                ComboBox cboGrade = (ComboBox)pnl_modifInfos.Controls["grpInfosProModif"].Controls["cboGradeModif"];
                ComboBox cboType = (ComboBox)pnl_modifInfos.Controls["grpInfosProModif"].Controls["cboTypeModif"];

                txtNom.Text = reader.GetString(0);
                txtPrenom.Text = reader.GetString(1);
                txtTel.Text = reader.GetString(2);
                chkEnConge.Checked = reader.GetBoolean(3);
                cboGrade.SelectedValue = reader.GetString(4);
                cboType.SelectedValue = reader.GetString(5);
                cboCaserneModif.SelectedValue = reader.GetInt32(6);

                txtNom.Enabled = false;
                txtPrenom.Enabled = false;

                reader.Close();
                FlowLayoutPanel flpHab = (FlowLayoutPanel)pnl_modifInfos.Controls["flpHabilitations"];
                ChargerHabilitations(matricule, flpHab, pnl_modifInfos);
                habilitationsAAjouter.Clear();
                habilitationsAsupprimer.Clear();
            }
            else
            {
                reader.Close();
                MessageBox.Show("Erreur lors du chargement des données initiales du pompier.");
            }
        }
    }
}

