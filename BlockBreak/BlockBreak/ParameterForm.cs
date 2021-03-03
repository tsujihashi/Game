using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockBreak
{
    public partial class ParameterForm : Form
    {
        //public Form1 f1;

        public ParameterForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //f1 = new Form1();
            this.Visible = false;
            //f1.ballSpeed.X = (double)numericUpDown_XSpeed.Value;
            //f1.ballSpeed.Y = (double)numericUpDown_YSpeed.Value;
            //this.Close();
        }

        private void ParameterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Visible = false;
            e.Cancel = true;
        }
    }
}
