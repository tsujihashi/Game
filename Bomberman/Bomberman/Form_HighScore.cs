using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bomberman
{
    public partial class Form_HighScore : Form
    {
        Format format = new Format();
        int displayNum = 15;

        public Form_HighScore()
        {
            InitializeComponent();
        }

        private void Form_HighScore_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            displayNum = Math.Min(displayNum, HighScore.highScoreList.Count);
            for(int i = 0; i < displayNum; i++)
            {
                string record = format.ChangeFormatToMMSS( HighScore.highScoreList[i][0]);
                string name = HighScore.highScoreList[i][1];
                string date = HighScore.highScoreList[i][2];
                dataGridView1.Rows.Add(record, name, date);
            }           
        }
    }
}
