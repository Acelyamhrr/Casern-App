using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserControlAjouterMission
{
    public partial class UserControl1: UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        public UserControl1(string lbltypeengin, string lblnuméro, string imagePath)
        {
            InitializeComponent();
            this.lbl_TypeEngin.Text = lbltypeengin;
            this.lbl_Numero.Text = lblnuméro;
            this.pictureBox1.Image = Image.FromFile(imagePath);

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        [Description("le type de l'engin")]
        public string typeEngin
        {
            get {
                return this.lbl_TypeEngin.Text;
            }
            set
            {
                this.lbl_TypeEngin.Text= value;
            }
        }
        [Description("le numero")]
        public string numero
        {
            get
            {
                return this.lbl_Numero.Text;
            }
            set
            {
                this.lbl_Numero.Text= value;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lbl_TypeEngin_Click(object sender, EventArgs e)
        {

        }

        private void lbl_Numero_Click(object sender, EventArgs e)
        {

        }
    }
}
