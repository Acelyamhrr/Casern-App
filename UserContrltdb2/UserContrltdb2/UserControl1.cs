using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserContrltdb2
{
    public partial class UserControl1: UserControl
    {
        public event EventHandler PictureBox1Clicked;
        public event EventHandler PictureBox2Clicked;
        public UserControl1()
        {
            InitializeComponent();
        }
        public UserControl1(String imagePath1, string imagPath2)
        {
            InitializeComponent();
            this.pictureBox1.Image = Image.FromFile(imagePath1);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureBox2.Image = Image.FromFile(imagPath2);
            this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }



        private void UserControl1_Load(object sender, EventArgs e){}
        private void pictureBox1_Click(object sender, EventArgs e){
            PictureBox1Clicked?.Invoke(this, e);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            PictureBox2Clicked?.Invoke(this, e);
        }
    }
}
