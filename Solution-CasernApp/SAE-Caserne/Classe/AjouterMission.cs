using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SAE_Caserne.Classe;
using System.Drawing;
using Pinpon;
using System.Drawing.Text;
using UCHabilitations;
using Org.BouncyCastle.Crypto;

public partial class AjouterMission : Form
{
    private DataTable m_casernesTable;
    private DataTable m_natureSinistreTable;
    private DataTable m_enginsTable;
    private DataTable m_pompiersTable;
    private DataTable m_possederTable;
    private DataTable m_typeEnginTable;
    private DataTable m_necessiterTable;
    private DataTable m_missionTable;
    private DataTable m_embarquerTable;
    private DataTable m_affectationTable;

    private ComboBox m_cmb_nsinistre;
    private ComboBox m_cmb_cequipe;
    private TextBox m_tb_motif;
    private TextBox m_tb_rue;
    private TextBox m_tb_cpostal;
    private TextBox m_tb_ville;
    private Panel m_pnl;
    private Panel m_pnlp;

    private int m_positionY = 0;

    public AjouterMission(
        DataTable casernes,
        DataTable natureSinistre,
        DataTable engins,
        DataTable pompiers,
        DataTable posseder,
        DataTable typeEngin,
        DataTable necessiter,
        DataTable mission,
        DataTable embarquer,
        DataTable affectation,
        Button btn_cequipe,
        ComboBox cmb_nsinistre,
        ComboBox cmb_cequipe,
        TextBox tb_motif,
        TextBox tb_rue,
        TextBox tb_cpostal,
        TextBox tb_ville,
        Panel pnl,
        Panel pnlp)
    {
        m_casernesTable = casernes;
        m_natureSinistreTable = natureSinistre;
        m_enginsTable = engins;
        m_pompiersTable = pompiers;
        m_possederTable = posseder;
        m_typeEnginTable = typeEngin;
        m_necessiterTable = necessiter;
        m_missionTable = mission;
        m_embarquerTable = embarquer;
        m_affectationTable = affectation;

        m_cmb_nsinistre = cmb_nsinistre;
        m_cmb_cequipe = cmb_cequipe;
        m_tb_motif = tb_motif;
        m_tb_rue = tb_rue;
        m_tb_cpostal = tb_cpostal;
        m_tb_ville = tb_ville;
        m_pnl = pnl;
        m_pnlp = pnlp;

        InitialiserInterface();
        btn_cequipe.Click += btn_cequipe_Click;
    }

    private void InitialiserInterface()
    {
        m_cmb_nsinistre.DataSource = m_natureSinistreTable;
        m_cmb_nsinistre.DisplayMember = "Libelle";
        m_cmb_nsinistre.ValueMember = "Id";

        m_cmb_cequipe.DataSource = m_casernesTable;
        m_cmb_cequipe.DisplayMember = "Nom";
        m_cmb_cequipe.ValueMember = "Id";
    }

    private void btn_cequipe_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(m_tb_motif.Text) ||string.IsNullOrWhiteSpace(m_tb_rue.Text) || string.IsNullOrWhiteSpace(m_tb_cpostal.Text) || string.IsNullOrWhiteSpace(m_tb_ville.Text))
        {
            MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // Code à exécuter si tous les champs sont remplis
        MessageBox.Show("Tous les champs sont remplis correctement.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
        int idNature = Convert.ToInt32(m_cmb_nsinistre.SelectedValue);
        int idCaserneInitiale = Convert.ToInt32(m_cmb_cequipe.SelectedValue);
        string motif = m_tb_motif.Text;

        string adresse = m_tb_rue.Text + ", " + m_tb_cpostal.Text + " " + m_tb_ville.Text;
        List<int> listeCasernes = ObtenirListeCasernes(idCaserneInitiale);
        
        // trouver la caserne qui peut faire la mission
        foreach (int idCaserne in listeCasernes)
        {
            List<string> typesEnginsRequis = ObtenirTypesEnginsRequis(idNature);
            List<DataRow> enginsDisponibles = FiltrerEnginsDisponibles(idCaserne);
            List<DataRow> pompiersDispo = FiltrerPompiersDisponibles(idCaserne);

            List<DataRow> enginsMission = new List<DataRow>();
            List<DataRow> pompiersMission = new List<DataRow>();
            bool missionValide = true;
            retirerUserControl(); // retirer les UserControls de la page
            // pour chaque type d’engin de la mission
            foreach (string codeType in typesEnginsRequis)
            {
                DataRow engin = TrouverEnginParType(enginsDisponibles, codeType);
                if (engin == null)
                {
                    missionValide = false; // pas d'engin pour cette mission
                    break;
                }
                enginsMission.Add(engin);

                List<(int idHab, int nb)> besoins = ObtenirBesoins(codeType);

                foreach ((int idHab, int nb) in besoins)
                {
                    List<DataRow> candidats = TrouverPompiersHabilites(pompiersDispo, idHab, nb);
                    if (candidats.Count < nb)
                    {
                        missionValide = false; // pas assez de pompier
                        break;
                    }

                    pompiersMission.AddRange(candidats);
                    RetirerPompiers(pompiersDispo, candidats); // évite les doublons
                }
                if (!missionValide)
                {
                    break;
                }
            }
            if (missionValide)
            {
                DataRow nouvelleMission = m_missionTable.NewRow();

                // +1 au dernier id existant
                int nouvelIdMission = 1;
                if (m_missionTable.Rows.Count > 0)
                {
                    int maxId = 0;
                    foreach (DataRow r in m_missionTable.Rows)
                    {
                        int id = Convert.ToInt32(r["id"]);
                        if (id > maxId) maxId = id;
                    }
                    nouvelIdMission = maxId + 1;
                }

                nouvelleMission["id"] = nouvelIdMission;
                nouvelleMission["dateHeureDepart"] = DateTime.Now;
                nouvelleMission["motifAppel"] = motif;
                nouvelleMission["adresse"] = m_tb_rue.Text;
                nouvelleMission["cp"] = m_tb_cpostal.Text;
                nouvelleMission["ville"] = m_tb_ville.Text;
                nouvelleMission["idNatureSinistre"] = idNature;
                nouvelleMission["idCaserne"] = idCaserne;
                nouvelleMission["terminee"] = 0;
                nouvelleMission["compteRendu"] = "";

                // ajout de la mission à la table
                m_missionTable.Rows.Add(nouvelleMission);
                // mettre engin en mission
                foreach (DataRow engin in enginsMission)
                {
                    engin["EnMission"] = true;
                }

                foreach (DataRow engin in enginsMission)
                {
                    DataRow rowPartirAvec = MesDatas.DsGlobal.Tables["PartirAvec"].NewRow();
                    rowPartirAvec["idMission"] = nouvelIdMission;
                    rowPartirAvec["numeroEngin"] = engin["numero"];
                    rowPartirAvec["codeTypeEngin"] = engin["codeTypeEngin"];
                    rowPartirAvec["idCaserne"] = idCaserne;
                    MesDatas.DsGlobal.Tables["PartirAvec"].Rows.Add(rowPartirAvec);
                }

                m_positionY = 35;

                // ajouter un UserControl pour chaque engin affecté
                foreach (DataRow engin in enginsMission)
                {
                    string codeType = engin["codeTypeEngin"].ToString();
                    string numero = engin["Numero"].ToString();

                    // trouver le nom du type d'engin
                    string nomType = "";
                    foreach (DataRow row in m_typeEnginTable.Rows)
                    {
                        if (row["Code"].ToString() == codeType)
                        {
                            nomType = row["code"].ToString();
                            break;
                        }
                    }
                    
                    ajouterUserControl(nomType, numero);
                    
                }

                // mettre pompier en mission
                foreach (DataRow pompier in pompiersMission)
                {
                    pompier["EnMission"] = true;
                }
                m_positionY = 35;
                foreach (DataRow pompier in pompiersMission)
                {
                    // si la colonne s'appelle bien "Id"
                    string matricule = pompier["Matricule"].ToString();
                    string idHabilitation = "";

                    foreach (DataRow posseder in m_possederTable.Rows)
                    {
                        if (posseder["matriculePompier"].ToString() == matricule)
                        {
                            idHabilitation = posseder["idHabilitation"].ToString();
                        }
                    }
                    ajouterUserControlPompier(matricule.ToString(), idHabilitation);
                    ajouterPompierTable(matricule, idHabilitation, nouvelIdMission);


                }
                MessageBox.Show("Mission attribuée à la caserne " + TrouverNomCaserne(idCaserne) + " avec " + enginsMission.Count + " engin(s) et " + pompiersMission.Count + " pompier(s) affecté(s)");
                // recharger le tdb
                new tableauDeBord().Chargement();
                return;
            }
        }
        MessageBox.Show("Aucune caserne peut géré cette mission");

        m_tb_motif.Clear();
        m_tb_cpostal.Clear();
        m_tb_rue.Clear();
        m_tb_ville.Clear();

    }

    private void ajouterPompierTable(string matricule, string idHabilitation, int nouveIdMission)
    {
        DataRow mobiliserRow = MesDatas.DsGlobal.Tables["Mobiliser"].NewRow();
        mobiliserRow["idMission"] = nouveIdMission;
        mobiliserRow["matriculePompier"] = matricule;
        mobiliserRow["idHabilitation"] = idHabilitation;
        MesDatas.DsGlobal.Tables["Mobiliser"].Rows.Add(mobiliserRow);
    }
    // liste des caserne à tester
    private List<int> ObtenirListeCasernes(int idInitiale)
    {
        List<int> ids = new List<int>();
        foreach (DataRow row in m_casernesTable.Rows)
        {
            ids.Add(Convert.ToInt32(row["Id"]));
        }
        ids.Remove(idInitiale);
        ids.Insert(0, idInitiale);
        return ids;
    }

    private List<string> ObtenirTypesEnginsRequis(int idNature)
    {
        List<string> resultats = new List<string>();
        foreach (DataRow row in m_necessiterTable.Rows)
        {
            if (Convert.ToInt32(row["idnatureSinistre"]) == idNature)
            {
                string code = row["codeTypeEngin"].ToString();
                if (!resultats.Contains(code))
                {
                    resultats.Add(code);
                }
            }
        }
        return resultats;
    }

    // engin dispo dans idcaserne
    private List<DataRow> FiltrerEnginsDisponibles(int idCaserne)
    {
        List<DataRow> engins = new List<DataRow>();
        foreach (DataRow row in m_enginsTable.Rows)
        {
            if (!Convert.ToBoolean(row["EnMission"]) && !Convert.ToBoolean(row["EnPanne"]) && Convert.ToInt32(row["IdCaserne"]) == idCaserne)
            {
                engins.Add(row);
            }
        }
        return engins;
    }

    // pompier disponible dans idcaserne
    private List<DataRow> FiltrerPompiersDisponibles(int idCaserne)
    {
        List<DataRow> pompiers = new List<DataRow>();
        foreach (DataRow p in m_pompiersTable.Rows)
        {
            if (!Convert.ToBoolean(p["EnMission"]) && !Convert.ToBoolean(p["EnConge"]))
            {
                int idCaserneActuelle = ObtenirIdCasernePompier(Convert.ToInt32(p["Matricule"]));
                if (idCaserneActuelle == idCaserne)
                {
                    pompiers.Add(p);
                }
            }
        }
        return pompiers;
    }

    // Chercher engin correspondand au type demandé
    private DataRow TrouverEnginParType(List<DataRow> engins, string codeType)
    {
        foreach (DataRow e in engins)
        {
            if (e["CodeTypeEngin"].ToString() == codeType)
            {
                return e;
            }
        }
        return null; // Aucun engin trouver
    }

    // habilitation et nb personne requise pour un type d’engin 
    private List<(int, int)> ObtenirBesoins(string codeType)
    {
        List<(int, int)> resultats = new List<(int, int)>();
        foreach (DataRow row in m_embarquerTable.Rows)
        {
            if (row["codeTypeEngin"].ToString() == codeType)
            {
                resultats.Add((Convert.ToInt32(row["idHabilitation"]), Convert.ToInt32(row["nombre"])));
            }
        }
        return resultats;
    }

    // cherche les pompiers habilité avec l'id habilitation
    private List<DataRow> TrouverPompiersHabilites(List<DataRow> pompiers, int idHab, int nbMax)
    {
        List<DataRow> candidats = new List<DataRow>();
        foreach (DataRow p in pompiers)
        {
            int matricule = Convert.ToInt32(p["Matricule"]);
            foreach (DataRow h in m_possederTable.Rows)
            {
                if (Convert.ToInt32(h["matriculePompier"]) == matricule && Convert.ToInt32(h["idHabilitation"]) == idHab)
                {
                    candidats.Add(p);
                    break;
                }
            }
            if (candidats.Count == nbMax)
            {
                break;  // tout les candidat sont trouvé
            }
        }
        return candidats;
    }

    // retire les pompiers pour éviter les doublons 
    void RetirerPompiers(List<DataRow> pompiersDispo, List<DataRow> candidats)
    {
        foreach (var candidat in candidats)
        {
            pompiersDispo.Remove(candidat);
        }
    }

    // id de la caserne ou est affcter le matricule du pompier
    private int ObtenirIdCasernePompier(int matricule)
    {
        int id = -1;
        DateTime dateMax = DateTime.MinValue;
        foreach (DataRow row in m_affectationTable.Rows)
        {
            if (Convert.ToInt32(row["matriculepompier"]) == matricule
                && (row["dateFin"] == DBNull.Value || Convert.ToDateTime(row["dateFin"]) > DateTime.Now))
            {
                DateTime d = Convert.ToDateTime(row["dateA"]);
                if (d > dateMax)
                {
                    dateMax = d;
                    id = Convert.ToInt32(row["idcaserne"]);
                }
            }
        }
        return id;
    }

    //  return le nom d’une caserne grace a son id 
    private string TrouverNomCaserne(int id)
    {
        foreach (DataRow row in m_casernesTable.Rows)
        {
            if (Convert.ToInt32(row["Id"]) == id)
            {
                return row["Nom"].ToString();
            }
        }
        return ""; // pas trouvé
    }


    public void ajouterUserControl(string typeEngin, string numeroEngin)
    {
        
        UserControl control = new UserControlAjouterMission.UserControl1(typeEngin, numeroEngin, @"../../Image/GestionDesEngins.png");
        control.Location = new Point(55, m_positionY);
        m_pnl.Controls.Add(control);
        m_positionY += 80;
    }

    public void ajouterUserControlPompier(string matricule, string habilitation)
    {
        UserControl control = new UserControlAjouterMission.UserControl1(matricule, habilitation, @"../../Image/sapeur-pompier.png");
        control.Location = new Point(55, m_positionY);
       
        m_pnlp.Controls.Add(control);
        m_positionY += 80;
    }

    public void retirerUserControl()
    {
        for (int i = m_pnl.Controls.Count - 1; i >= 0; i--)
        {
            if (m_pnl.Controls[i] is UserControl)
            {
                m_pnl.Controls.RemoveAt(i);
            }
        }

        for (int i = m_pnlp.Controls.Count - 1; i >= 0; i--)
        {
            if (m_pnlp.Controls[i] is UserControl)
            {
                m_pnlp.Controls.RemoveAt(i);
            }
        }
    }
}
