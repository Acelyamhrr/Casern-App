using Pinpon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAE_Caserne.Classe
{
    public partial class FrmCommentaire : Form
    {
        private string idMission;
        private Dictionary<string, string> reparationDict = new Dictionary<string, string>();

        public string CompteRendu
        {
            get { return txtCompteRendu.Text; }
        }

        public Dictionary<string, string> Reparations
        {
            get { return reparationDict; }
        }

        public FrmCommentaire()
        {
            InitializeComponent();
        }

        public FrmCommentaire(string idMission)
        {
            InitializeComponent();
            this.idMission = idMission;

            // recup des engins mobilisés 
            DataTable dt = MesDatas.DsGlobal.Tables["PartirAvec"];
            List<DataRow> engins = new List<DataRow>();
            int compteur = 0;

            foreach (DataRow row in dt.Rows)
            {
                if (row["idMission"].ToString() == idMission)
                {
                    engins.Add(row);
                    compteur++;
                }
            }


            int y = 10;
            foreach (DataRow row in engins)
            {
                string codeType = row["codeTypeEngin"].ToString();
                int numero = Convert.ToInt32(row["numeroEngin"]);
                string enginLabel = codeType + "-" +numero;

                Label lbl = new Label();
                lbl.Text = enginLabel;
                lbl.Left = 10;
                lbl.Top = y + 5;
                lbl.Width = 100;

                TextBox txt = new TextBox();
                txt.Left = 120;
                txt.Top = y;
                txt.Width = 400;
                txt.Tag = codeType + "|" +numero;

                panelEngins.Controls.Add(lbl);
                panelEngins.Controls.Add(txt);
                y += 30;
            }

            btnOK.DialogResult = DialogResult.OK;
            btnAnnuler.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            reparationDict.Clear();
            foreach (Control ctrl in panelEngins.Controls)
            {
                if (ctrl is TextBox tb && tb.Tag is string key)
                {
                    string value = tb.Text.Trim();
                    if (!string.IsNullOrWhiteSpace(value))
                        reparationDict.Add(key, value);
                }
            }
        }
    }
}