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
            tableau.Chargement(this); 

        }

       




    }
}
