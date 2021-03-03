namespace BlockBreak
{
    partial class ParameterForm
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
            this.numericUpDown_XSpeed = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown_YSpeed = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_ChangeRate = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDown_BlockNum = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_playerWidth = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_XSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_YSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ChangeRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BlockNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_playerWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown_XSpeed
            // 
            this.numericUpDown_XSpeed.Location = new System.Drawing.Point(205, 35);
            this.numericUpDown_XSpeed.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDown_XSpeed.Name = "numericUpDown_XSpeed";
            this.numericUpDown_XSpeed.Size = new System.Drawing.Size(46, 19);
            this.numericUpDown_XSpeed.TabIndex = 0;
            this.numericUpDown_XSpeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "ボール速度（X方向）";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "ボール速度（Y方向）";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "ボール速度変化率（X方向）";
            // 
            // numericUpDown_YSpeed
            // 
            this.numericUpDown_YSpeed.Location = new System.Drawing.Point(205, 71);
            this.numericUpDown_YSpeed.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDown_YSpeed.Name = "numericUpDown_YSpeed";
            this.numericUpDown_YSpeed.Size = new System.Drawing.Size(46, 19);
            this.numericUpDown_YSpeed.TabIndex = 0;
            this.numericUpDown_YSpeed.Value = new decimal(new int[] {
            2,
            0,
            0,
            -2147483648});
            // 
            // numericUpDown_ChangeRate
            // 
            this.numericUpDown_ChangeRate.DecimalPlaces = 1;
            this.numericUpDown_ChangeRate.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_ChangeRate.Location = new System.Drawing.Point(205, 111);
            this.numericUpDown_ChangeRate.Name = "numericUpDown_ChangeRate";
            this.numericUpDown_ChangeRate.Size = new System.Drawing.Size(46, 19);
            this.numericUpDown_ChangeRate.TabIndex = 0;
            this.numericUpDown_ChangeRate.Value = new decimal(new int[] {
            15,
            0,
            0,
            65536});
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(106, 324);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(61, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "ブロック数";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(61, 188);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "プレーヤー幅";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(61, 221);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "label4";
            // 
            // numericUpDown_BlockNum
            // 
            this.numericUpDown_BlockNum.Location = new System.Drawing.Point(205, 149);
            this.numericUpDown_BlockNum.Name = "numericUpDown_BlockNum";
            this.numericUpDown_BlockNum.Size = new System.Drawing.Size(46, 19);
            this.numericUpDown_BlockNum.TabIndex = 4;
            this.numericUpDown_BlockNum.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numericUpDown_playerWidth
            // 
            this.numericUpDown_playerWidth.DecimalPlaces = 1;
            this.numericUpDown_playerWidth.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDown_playerWidth.Location = new System.Drawing.Point(205, 186);
            this.numericUpDown_playerWidth.Name = "numericUpDown_playerWidth";
            this.numericUpDown_playerWidth.Size = new System.Drawing.Size(46, 19);
            this.numericUpDown_playerWidth.TabIndex = 4;
            this.numericUpDown_playerWidth.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(205, 219);
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(46, 19);
            this.numericUpDown3.TabIndex = 4;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(61, 255);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(163, 16);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "プレーヤー幅を徐々に狭くする";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(61, 277);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(157, 16);
            this.checkBox2.TabIndex = 5;
            this.checkBox2.Text = "ボール速度を徐々に速くする";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // ParameterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 359);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.numericUpDown3);
            this.Controls.Add(this.numericUpDown_playerWidth);
            this.Controls.Add(this.numericUpDown_BlockNum);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown_ChangeRate);
            this.Controls.Add(this.numericUpDown_YSpeed);
            this.Controls.Add(this.numericUpDown_XSpeed);
            this.Name = "ParameterForm";
            this.Text = "パラメータ調整";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ParameterForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_XSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_YSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ChangeRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BlockNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_playerWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.NumericUpDown numericUpDown_XSpeed;
        public System.Windows.Forms.NumericUpDown numericUpDown_YSpeed;
        public System.Windows.Forms.NumericUpDown numericUpDown_ChangeRate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.NumericUpDown numericUpDown_BlockNum;
        public System.Windows.Forms.NumericUpDown numericUpDown_playerWidth;
        public System.Windows.Forms.NumericUpDown numericUpDown3;
        public System.Windows.Forms.CheckBox checkBox1;
        public System.Windows.Forms.CheckBox checkBox2;
    }
}