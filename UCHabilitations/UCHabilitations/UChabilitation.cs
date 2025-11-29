using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UCHabilitations
{
    public partial class UChabilitation: UserControl
    {
        public UChabilitation()
        {
            InitializeComponent();
        }

        public UChabilitation(string habilitation, DateTime dateObtention)
        {
            InitializeComponent();
            this.cboHab.SelectedItem = habilitation;
            this.dtpHab.Value = dateObtention;
        }

        public int Habilitation
        {
            get
            {
                return (int)cboHab.SelectedValue;
            }
            set
            {
                cboHab.SelectedValue = value; 
            }


        }

        public ComboBox CboHab
        {
            get { return cboHab; }
        }

        public DateTime DateObtention
        {
            get
            {
                return dtpHab.Value;
            }
            set
            {
                dtpHab.Value = value;
            }
        }
    }
}
