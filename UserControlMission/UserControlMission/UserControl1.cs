using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserControlMission
{
    public partial class UserControl1: UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        public UserControl1(string missionType, string missionDate, string caserne, string missionDetails, string id, string imagePath)
        {
            InitializeComponent();
            this.lbl_type.Text = missionType;   
            this.lbl_date.Text = missionDate;
            this.lbl_caserne.Text = caserne;
            this.lbl_mission.Text = missionDetails;
            this.lbl_id.Text = id;
            this.pictureBox1.Image = Image.FromFile(imagePath);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        [Description("Le type de la mission")]
        public String MissionType
        {
            get
            {
                return this.lbl_type.Text;
            }
            set
            {
                this.lbl_type.Text = value;
            }
        }

        [Description("La date de la mission")]
        public String MissionDate
        {
            get
            {
                return this.lbl_date.Text;
            }
            set
            {
                this.lbl_date.Text = value;
            }
        }

        [Description("Le nom de la caserne")]
        public String Caserne
        {
            get
            {
                return this.lbl_caserne.Text;
            }
            set
            {
                this.lbl_caserne.Text = value;
            }
        }

        [Description("Les détails de la mission")]
        public String MissionDetails
        {
            get
            {
                return this.lbl_mission.Text;
            }
            set
            {
                this.lbl_mission.Text = value;
            }
        }

        [Description("L'ID de la mission")]
        public String MissionId
        {
            get
            {
                return this.lbl_id.Text;
            }
            set
            {
                this.lbl_id.Text = value;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e) { }
        
        private void pictureBox1_Click(object sender, EventArgs e) { }
       
        private void lbl_type_Click(object sender, EventArgs e) { }
        
        private void lbl_caserne_Click(object sender, EventArgs e) { }
       
        private void lbl_mission_Click(object sender, EventArgs e) { }
        
        private void lbl_date_Click(object sender, EventArgs e) { }
       
        private void lbl_id_Click(object sender, EventArgs e) { }
       
    }
}
