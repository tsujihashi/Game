using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordStudy
{
    public partial class Form1 : Form
    {
        Form_Data fd = new Form_Data();
        Random r = new Random();
        Encoding enc = Encoding.GetEncoding("shift_jis");

        public Form1()
        {
            InitializeComponent();

            label1.Font = new Font("Arial", 14,FontStyle.Bold);
            label2.Font = new Font("Arial", 12);

            StaticData.files = Directory.GetFiles(
                Directory.GetCurrentDirectory() + "\\WordList", "*", SearchOption.AllDirectories)
                .ToList();
            for(int i = 0; i < StaticData.files.Count; i++)
            {
                comboBox1.Items.Add(StaticData.files[i].Replace(Directory.GetCurrentDirectory() + "\\WordList\\", "").Replace(".csv", ""));
            }

            //GetWordFromFile();
            //int idx = r.Next(0, StaticData.wordList.Count);
            //label1.Text = StaticData.wordList[idx][0];
            //label2.Text = StaticData.wordList[idx][1];
            label1.Visible = false;
            label2.Visible = false;
            button_next.Enabled = false;

        }


        private void button_answer_Click(object sender, EventArgs e)
        {
            label2.Visible = true;
            button_next.Enabled = true;
        }

        private void button_next_Click(object sender, EventArgs e)
        {
            int idx = r.Next(0, StaticData.selectedWordList.words.Count);
            label1.Text = StaticData.selectedWordList.words[idx].problem;
            label2.Text = StaticData.selectedWordList.words[idx].answer;
            label2.Visible = false;
            button_next.Enabled = false;
        }

        private void button_edit_Click(object sender, EventArgs e)
        {
            fd = new Form_Data();
            fd.Show();
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            int idx = r.Next(0, StaticData.selectedWordList.words.Count);
            label1.Text = StaticData.selectedWordList.words[idx].problem;
            label2.Text = StaticData.selectedWordList.words[idx].answer;
            label1.Visible = true;
            button_next.Enabled = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            new GetWord().GetWordFromFile(StaticData.files[comboBox1.SelectedIndex]);
            StaticData.selectedWordList.idx = comboBox1.SelectedIndex;
            //fd.comboBox2.SelectedIndex = comboBox1.SelectedIndex;
        }
    }
}
