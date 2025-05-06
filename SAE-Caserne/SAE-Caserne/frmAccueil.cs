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
using Pinpon;
using SAE_Caserne.Classe;
using UserControlMission;

namespace SAE_Caserne
{
    public partial class frmAccueil : Form
    {
        public frmAccueil()
        {
            InitializeComponent();
            


        }

        private void frmAccueil_Load(object sender, EventArgs e)
        {
            tableauDeBord tableau = new tableauDeBord();
            tableau.Chargement();
            tableau.afficherMissions(pnl_tdb);
            

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pnl_tdb_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pnl_tdb.Visible = false;
            pnl_nvlmission.Visible = true;
        }

        private void btn_tdb_Click(object sender, EventArgs e)
        {
            pnl_tdb.Visible = true ;
            pnl_nvlmission.Visible = false ;
        }
    }
}
