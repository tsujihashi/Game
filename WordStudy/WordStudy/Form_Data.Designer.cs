namespace WordStudy
{
    partial class Form_Data
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Problem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Answer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label_wordNum = new System.Windows.Forms.Label();
            this.button_AddWord = new System.Windows.Forms.Button();
            this.textBox_Problem = new System.Windows.Forms.TextBox();
            this.textBox_Answer = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button_AddWordList = new System.Windows.Forms.Button();
            this.textBox_WordListName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsm_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.button_ShowCsv = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Problem,
            this.Answer});
            this.dataGridView1.Location = new System.Drawing.Point(30, 165);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(458, 388);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // Problem
            // 
            this.Problem.HeaderText = "問題";
            this.Problem.Name = "Problem";
            // 
            // Answer
            // 
            this.Answer.HeaderText = "解答";
            this.Answer.Name = "Answer";
            this.Answer.Width = 300;
            // 
            // label_wordNum
            // 
            this.label_wordNum.AutoSize = true;
            this.label_wordNum.Location = new System.Drawing.Point(27, 147);
            this.label_wordNum.Name = "label_wordNum";
            this.label_wordNum.Size = new System.Drawing.Size(60, 15);
            this.label_wordNum.TabIndex = 1;
            this.label_wordNum.Text = "単語数：";
            // 
            // button_AddWord
            // 
            this.button_AddWord.Location = new System.Drawing.Point(23, 125);
            this.button_AddWord.Name = "button_AddWord";
            this.button_AddWord.Size = new System.Drawing.Size(75, 23);
            this.button_AddWord.TabIndex = 2;
            this.button_AddWord.Text = "追加";
            this.button_AddWord.UseVisualStyleBackColor = true;
            this.button_AddWord.Click += new System.EventHandler(this.button_AddWord_Click);
            // 
            // textBox_Problem
            // 
            this.textBox_Problem.Location = new System.Drawing.Point(6, 45);
            this.textBox_Problem.Name = "textBox_Problem";
            this.textBox_Problem.Size = new System.Drawing.Size(100, 22);
            this.textBox_Problem.TabIndex = 3;
            this.textBox_Problem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_Problem_KeyDown);
            // 
            // textBox_Answer
            // 
            this.textBox_Answer.Location = new System.Drawing.Point(6, 97);
            this.textBox_Answer.Name = "textBox_Answer";
            this.textBox_Answer.Size = new System.Drawing.Size(158, 22);
            this.textBox_Answer.TabIndex = 4;
            this.textBox_Answer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_Answer_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox_Answer);
            this.groupBox1.Controls.Add(this.button_AddWord);
            this.groupBox1.Controls.Add(this.textBox_Problem);
            this.groupBox1.Location = new System.Drawing.Point(506, 165);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(188, 165);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "新しい単語を追加";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "解答";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "問題";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(30, 41);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(152, 23);
            this.comboBox2.TabIndex = 6;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.button_AddWordList);
            this.groupBox2.Controls.Add(this.textBox_WordListName);
            this.groupBox2.Location = new System.Drawing.Point(241, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(161, 124);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "新しい単語帳を追加";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "名前";
            // 
            // button_AddWordList
            // 
            this.button_AddWordList.Location = new System.Drawing.Point(15, 82);
            this.button_AddWordList.Name = "button_AddWordList";
            this.button_AddWordList.Size = new System.Drawing.Size(75, 23);
            this.button_AddWordList.TabIndex = 1;
            this.button_AddWordList.Text = "追加";
            this.button_AddWordList.UseVisualStyleBackColor = true;
            this.button_AddWordList.Click += new System.EventHandler(this.button_AddWordList_Click);
            // 
            // textBox_WordListName
            // 
            this.textBox_WordListName.Location = new System.Drawing.Point(15, 54);
            this.textBox_WordListName.Name = "textBox_WordListName";
            this.textBox_WordListName.Size = new System.Drawing.Size(140, 22);
            this.textBox_WordListName.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "単語帳";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_Delete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(196, 32);
            // 
            // tsm_Delete
            // 
            this.tsm_Delete.Name = "tsm_Delete";
            this.tsm_Delete.Size = new System.Drawing.Size(195, 28);
            this.tsm_Delete.Text = "この行を削除(D)";
            this.tsm_Delete.Click += new System.EventHandler(this.tsm_Delete_Click);
            // 
            // button_ShowCsv
            // 
            this.button_ShowCsv.Location = new System.Drawing.Point(515, 399);
            this.button_ShowCsv.Name = "button_ShowCsv";
            this.button_ShowCsv.Size = new System.Drawing.Size(117, 23);
            this.button_ShowCsv.TabIndex = 9;
            this.button_ShowCsv.Text = "csvファイルを開く";
            this.button_ShowCsv.UseVisualStyleBackColor = true;
            this.button_ShowCsv.Click += new System.EventHandler(this.button_ShowCsv_Click);
            // 
            // Form_Data
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 565);
            this.Controls.Add(this.button_ShowCsv);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label_wordNum);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form_Data";
            this.Text = "Form_Data";
            this.Load += new System.EventHandler(this.Form_Data_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_Data_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label_wordNum;
        private System.Windows.Forms.Button button_AddWord;
        private System.Windows.Forms.TextBox textBox_Problem;
        private System.Windows.Forms.TextBox textBox_Answer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_AddWordList;
        private System.Windows.Forms.TextBox textBox_WordListName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Problem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Answer;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsm_Delete;
        private System.Windows.Forms.Button button_ShowCsv;
    }
}