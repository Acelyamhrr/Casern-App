using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserControlVehicule
{
    public partial class UserControl1: UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        public UserControl1(string det, string type)
        {
                
            InitializeComponent();
            this.lbl_det.Text = det;
            this.lbl_type.Text = type;
        }
        public string type
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

        public string det
        {
            get
            {
                return this.lbl_det.Text;
            }
            set
            {
                this.lbl_det.Text = value;
            }
        }
        private void lbl_type_Click(object sender, EventArgs e)
        {

        }

        private void lbl_det_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void lbl_type_Click_1(object sender, EventArgs e)
        {

        }
    }
}
