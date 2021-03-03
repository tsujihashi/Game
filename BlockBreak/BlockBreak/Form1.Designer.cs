namespace BlockBreak
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.button_Start = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.設定SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.パラメータ調整ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ハイスコアＨToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button_Reset = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button_Stop = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Start
            // 
            this.button_Start.Location = new System.Drawing.Point(139, 402);
            this.button_Start.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(48, 25);
            this.button_Start.TabIndex = 0;
            this.button_Start.Text = "Start";
            this.button_Start.UseVisualStyleBackColor = true;
            this.button_Start.Click += new System.EventHandler(this.button_Start_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 434);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.設定SToolStripMenuItem,
            this.ハイスコアＨToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(412, 26);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 設定SToolStripMenuItem
            // 
            this.設定SToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.パラメータ調整ToolStripMenuItem});
            this.設定SToolStripMenuItem.Name = "設定SToolStripMenuItem";
            this.設定SToolStripMenuItem.Size = new System.Drawing.Size(62, 22);
            this.設定SToolStripMenuItem.Text = "設定(S)";
            // 
            // パラメータ調整ToolStripMenuItem
            // 
            this.パラメータ調整ToolStripMenuItem.Name = "パラメータ調整ToolStripMenuItem";
            this.パラメータ調整ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.パラメータ調整ToolStripMenuItem.Text = "パラメータ調整";
            this.パラメータ調整ToolStripMenuItem.Click += new System.EventHandler(this.パラメータ調整ToolStripMenuItem_Click);
            // 
            // ハイスコアＨToolStripMenuItem
            // 
            this.ハイスコアＨToolStripMenuItem.Name = "ハイスコアＨToolStripMenuItem";
            this.ハイスコアＨToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.ハイスコアＨToolStripMenuItem.Text = "ハイスコア(H)";
            this.ハイスコアＨToolStripMenuItem.Click += new System.EventHandler(this.ハイスコアＨToolStripMenuItem_Click);
            // 
            // button_Reset
            // 
            this.button_Reset.Enabled = false;
            this.button_Reset.Location = new System.Drawing.Point(44, 402);
            this.button_Reset.Name = "button_Reset";
            this.button_Reset.Size = new System.Drawing.Size(49, 23);
            this.button_Reset.TabIndex = 3;
            this.button_Reset.Text = "Reset";
            this.button_Reset.UseVisualStyleBackColor = true;
            this.button_Reset.Click += new System.EventHandler(this.button_Reset_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(137, 191);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Game Over";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(313, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Time";
            // 
            // button_Stop
            // 
            this.button_Stop.Location = new System.Drawing.Point(215, 403);
            this.button_Stop.Name = "button_Stop";
            this.button_Stop.Size = new System.Drawing.Size(87, 23);
            this.button_Stop.TabIndex = 6;
            this.button_Stop.Text = "Stop/Restart";
            this.button_Stop.UseVisualStyleBackColor = true;
            this.button_Stop.Click += new System.EventHandler(this.button_Stop_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(81, 434);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "ボール速度";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(201, 434);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "プレーヤー幅";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(313, 434);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "残りブロック";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 455);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button_Stop);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_Reset);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_Start);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "ブロック崩し";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Draw);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Start;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 設定SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem パラメータ調整ToolStripMenuItem;
        private System.Windows.Forms.Button button_Reset;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem ハイスコアＨToolStripMenuItem;
        private System.Windows.Forms.Button button_Stop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}

