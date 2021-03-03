using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BlockBreak
{
    public partial class ScoreForm : Form
    {
        public ScoreForm()
        {
            InitializeComponent();
        }

        int highScore;

        private void ScoreForm_Load(object sender, EventArgs e)
        {
            GetScore();
        }

        //ファイルからハイスコアを取得
        public void GetScore()
        {
            string filePath = Directory.GetCurrentDirectory() + "\\HighScore.csv";

            using (var streamReader = new StreamReader(filePath))
            {
                highScore = int.Parse(streamReader.ReadToEnd());

                //var line = streamReader.ReadLine();
                ////highScore=int.Parse(line);
                //while (!streamReader.EndOfStream)
                //{
                //    highScore = int.Parse(line);
                //}
            }
            label1.Text = highScore.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void ScoreForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Visible = false;
            e.Cancel = true;
        }
    }
}
