using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace WordStudy
{
    public partial class Form_Data : Form
    {
        WordList wl = new WordList();
        Encoding enc = Encoding.GetEncoding("shift_jis");
        int rowIdx ;
        private bool isNewXlsFile = false;
        private Microsoft.Office.Interop.Excel.Application xls = null;     // Excel自体 
        private Workbook book = null;       // ブック 
        private Worksheet sheet = null;     // シート

        public Form_Data()
        {
            InitializeComponent();
        }

        private void Form_Data_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < StaticData.files.Count; i++)
            {
                comboBox2.Items.Add(StaticData.files[i].Replace(Directory.GetCurrentDirectory() + "\\WordList\\", "").Replace(".csv", ""));
            }
            if (StaticData.selectedWordList.listName != null)
            {
                comboBox2.SelectedIndex = StaticData.selectedWordList.idx;
            }           
            label_wordNum.Text = "単語数：" + StaticData.selectedWordList.words.Count;
            dataGridView1.Rows.Clear();
            for (int i = 0; i < StaticData.selectedWordList.words.Count; i++)
            {
                dataGridView1.Rows.Add(StaticData.selectedWordList.words[i].problem, StaticData.selectedWordList.words[i].answer);
            }
        }

        private void button_AddWord_Click(object sender, EventArgs e)
        {
            if(textBox_Problem != null && textBox_Answer != null)
            {
                AddWord();
            }           
        }

        private void Form_Data_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                AddWord();
            }
        }       

        private void textBox_Answer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter&&textBox_Problem!=null&&textBox_Answer!=null)
            {
                AddWord();
            }
        }

        private void textBox_Problem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && textBox_Problem != null && textBox_Answer != null)
            {
                AddWord();
            }
        }

        private void AddWord()
        {
            wl.listName = comboBox2.SelectedItem.ToString();
            string filePath = Directory.GetCurrentDirectory() + "\\WordList\\" + wl.listName + ".csv";
            bool append = true; //末尾に追加
            StreamWriter sw = new StreamWriter(filePath, append, enc); 
            sw.Write(textBox_Problem.Text + "," + textBox_Answer.Text + "\r\n");
            sw.Close();
            StaticData.wordList.Add(new string[] { textBox_Problem.Text, textBox_Answer.Text });
            dataGridView1.Rows.Add(textBox_Problem.Text, textBox_Answer.Text);
            textBox_Problem.Text = "";
            textBox_Answer.Text = "";
            label_wordNum.Text = "単語数：" + StaticData.wordList.Count;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filePath = StaticData.files[comboBox2.SelectedIndex];
            wl = new WordList();
            wl.listName = filePath.Replace(Directory.GetCurrentDirectory() + "\\WordList\\", "").Replace(".csv", "");
            StreamReader sr = new StreamReader(filePath, enc);
            while (!sr.EndOfStream)
            {
                var values = sr.ReadLine().Split(',');
                Word word = new Word(values);
                wl.words.Add(word);
            }
            sr.Close();
            label_wordNum.Text = "単語数：" + wl.words.Count;
            dataGridView1.Rows.Clear();
            for (int i = 0; i < wl.words.Count; i++)
            {
                dataGridView1.Rows.Add(wl.words[i].problem, wl.words[i].answer);
            }
        }

        private void button_AddWordList_Click(object sender, EventArgs e)
        {
            string filePath = Directory.GetCurrentDirectory() + "\\WordList\\" + textBox_WordListName.Text+".csv";
            bool newName = true;
            if (textBox_WordListName.Text == "")
            {
                newName = false;
            }
            for(int i = 0; i < StaticData.files.Count; i++)
            {
                if (StaticData.files[i] == filePath)
                {
                    newName = false;
                    break;
                }                
            }
            if (newName)
            {
                StreamWriter sw = new StreamWriter(filePath);
                sw.Close();
                StaticData.files.Add(Directory.GetCurrentDirectory() + "\\WordList\\" + textBox_WordListName.Text + ".csv");
                comboBox2.Items.Add(textBox_WordListName.Text);
                MessageBox.Show("単語帳「" + textBox_WordListName.Text + "」を作成しました。");
                comboBox2.SelectedIndex = comboBox2.Items.Count - 1;
            }
            else
            {
                MessageBox.Show("その名前は使用できません。","", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //columnIdx = e.ColumnIndex;
                rowIdx = e.RowIndex;
                System.Drawing.Point cp = Cursor.Position;                
                contextMenuStrip1.Show(cp);
            }
        }

        private void tsm_Delete_Click(object sender, EventArgs e)
        {
            
            for(int i = 0; i < wl.words.Count; i++)
            {
                Console.Write(wl.words[i].problem + "," + dataGridView1.Rows[rowIdx].Cells[0].Value.ToString() + "\r\n");
                Console.Write(wl.words[i].answer + "," + dataGridView1.Rows[rowIdx].Cells[1].Value.ToString() + "\r\n");
                //Console.Write()
                if (wl.words[i].problem == dataGridView1.Rows[rowIdx].Cells[0].Value.ToString()
                    && wl.words[i].answer == dataGridView1.Rows[rowIdx].Cells[1].Value.ToString())
                {
                    wl.words.RemoveAt(i);
                    break;
                }
            }
            bool append = false; //上書き
            StreamWriter sw = new StreamWriter(
                Directory.GetCurrentDirectory() + "\\WordList\\" + wl.listName + ".csv", append, enc); 
            for(int i=0;i< wl.words.Count; i++)
            {
                sw.Write(wl.words[i].problem + "," + wl.words[i].answer + "\r\n");
            }
            sw.Close();
            dataGridView1.Rows.RemoveAt(rowIdx);
        }

        private void button_ShowCsv_Click(object sender, EventArgs e)
        {
            if (wl.listName != null)
            {
                xls = new Microsoft.Office.Interop.Excel.Application();
                // Excelを非表示:レスポンス向上
                ExcelVisibleToggle(xls, false);     
                // 既存ファイルオープン
                string filePath = Directory.GetCurrentDirectory() + "\\WordList\\" + wl.listName + ".csv";
                this.book = xls.Workbooks.Open(filePath);
                // シートを選択
                sheet = book.Sheets[wl.listName];
                // Excel表示
                ExcelVisibleToggle(xls, true);      
                //オブジェクトの開放
                Marshal.ReleaseComObject(sheet);
                Marshal.ReleaseComObject(book);
                Marshal.ReleaseComObject(xls);
            }
           
        }

        /// <summary>
        /// Excelの表示と非表示の切り替え
        /// </summary>
        /// <param name="xls"></param>
        /// <param name="setting"></param>
        public void ExcelVisibleToggle(Microsoft.Office.Interop.Excel.Application xls, bool setting)
        {
            if (xls.Visible == !setting)
            {
                xls.Visible = setting;
            }
        }
    }
}
