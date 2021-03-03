using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace BlockBreak
{
    public partial class Form1 : Form
    {
        ParameterForm f2 = new ParameterForm();
        ScoreForm f3 = new ScoreForm();

        Stopwatch sw = new Stopwatch();
        string time;
        Random r = new Random();
        Vector ballPos;
        public Vector ballSpeed;//=new Vector(-1,-2);
        int ballRadius;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        Graphics g;
        Pen pen;
        Brush brush;
        int blockNum;
        Vector[] blockPos;
        int blockHeight;
        int blockWidth;
        bool[] blockCrushed;
        System.Drawing.Point sp;
        System.Drawing.Point cp;
        float mouseX;
        float mouseY;
        double playerHeight;
        double playerWidth;
        Vector playerpos;
        int crushedCounter;
        bool started;
        bool gameOver;
        bool cleared;

        public Form1()
        {
            InitializeComponent();

            //フォームサイズ固定
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            //フォーム背景色
            this.BackColor = Color.Black;
            //マウス座標ラベル
            label1.ForeColor = Color.White;
            //ゲームオーバーラベル
            label2.ForeColor = Color.Red;
            label2.Font = new Font("Arial", 30);
            //時間ラベル
            label3.ForeColor = Color.White;
            label3.Font = new Font("Arial", 20);

            label4.ForeColor = Color.White;
            label5.ForeColor = Color.White;
            label6.ForeColor = Color.White;

            //ちらつき防止
            this.DoubleBuffered = true;
            //ゲームの初期化
            Initialize();
           
            timer.Interval = 15;
            timer.Tick += new EventHandler(Update);           
        }

        private void Update(object sender, EventArgs e)
        {
            //時間表示を更新
            time = sw.Elapsed.ToString(@"mm\:ss");
            //ゲームオーバー判定フラグ
            gameOver = false;

            // ボールの移動
            ballPos += ballSpeed;
            //ボール速度を徐々にUP
            if (f2.checkBox2.Checked)
            {
                ballSpeed *= 1.0001;                           
            }
            //プレーヤー幅を徐々に狭くする
            if (f2.checkBox1.Checked)
            {
                playerWidth *= 0.9999;
            }

            // 左右の壁でのバウンド
            if (ballPos.X + ballRadius > this.Bounds.Width - 15 || ballPos.X - ballRadius < 0)
            {
                ballSpeed.X *= -1;
            }

            // 上の壁でバウンド
            if (ballPos.Y - ballRadius < 25)
            {
                ballSpeed.Y *= -1;
            }
           
            //下端（ゲームオーバー）
            System.Drawing.Point cpform = PointToClient(new System.Drawing.Point(this.Right, this.Bottom));
            if (ballPos.Y + ballRadius > cpform.Y-80)
            {
                gameOver = true;        
            }

            crushedCounter = 0;
            cleared = false;
           
            //ブロックとの当たり判定
            for (int i = 0; i < blockNum; i++)
            {              

                if (!blockCrushed[i] && ballPos.X > blockPos[i].X && ballPos.X < blockPos[i].X + blockWidth
               && ballPos.Y > blockPos[i].Y && ballPos.Y < blockPos[i].Y + blockHeight)
                {
                    //MessageBox.Show("");
                    blockCrushed[i] = true;
                    ballSpeed.Y *= -1;
                }

                if (blockCrushed[i])
                {
                    crushedCounter++;
                    if (crushedCounter == blockNum)
                    {
                        cleared = true;
                    }
                }     
                              
                   
            }

            sp = Cursor.Position;
            cp = PointToClient(sp);
            mouseX = cp.X;
            mouseY = cp.Y;

            //プレーヤーとの当たり判定
            if (ballPos.X > mouseX - playerWidth / 2 && ballPos.X < mouseX + playerWidth / 2
                && ballPos.Y > playerpos.Y-playerHeight/2 && ballPos.Y < playerpos.Y + playerHeight / 2)
            {
                ballSpeed.Y *= -1;
                
                //端に当てると速くなる
                if (ballPos.X < mouseX - playerWidth / 4 || ballPos.X > mouseX + playerWidth / 4)
                {
                    ballSpeed.X *= 1.5;
                }
                //中心に当てると遅くなる
                if (mouseX - playerWidth / 8 < ballPos.X && ballPos.X < mouseX + playerWidth / 8)
                {
                    ballSpeed.X /= 1.5;
                }
            }

            // 再描画
            Invalidate();

            if (cleared)
            {
                timer.Stop();
                started = false;
                f3.GetScore();
                if(int.Parse(f3.label1.Text)>(int)sw.Elapsed.Seconds)
                using (var streamWriter = new StreamWriter("HighScore.csv"))
                {
                    streamWriter.WriteLine((int)sw.Elapsed.Seconds);
                }
                MessageBox.Show("Game Cleared!");               
            }

            if (gameOver)
            {
                //button_Reset.Enabled = false;
                label2.Visible = true;                
                button_Stop.Enabled = false;
                timer.Stop();
                started = false;
                label2.Text = "Game Over";
                //Initialize();
            }
        }

        private void Draw(object sender, PaintEventArgs e)
        {
           

            g = e.Graphics;

            pen = Pens.White;
            g.DrawLine(pen, PointToClient(new System.Drawing.Point(0, this.Bottom - 80)), PointToClient(new System.Drawing.Point(this.Right, this.Bottom - 80)));
            

            SolidBrush Brush = new SolidBrush(Color.Aqua);
            brush = Brushes.Yellow;

            float px = (float)this.ballPos.X - ballRadius;
            float py = (float)this.ballPos.Y - ballRadius;

            g.FillEllipse(Brush, px, py, this.ballRadius * 2, this.ballRadius * 2);

            for(int i = 0; i < blockNum; i++)
            {
                if (!blockCrushed[i])
                {
                    g.FillRectangle(brush, (float)blockPos[i].X+1, (float)blockPos[i].Y+1, (float)blockWidth-2, (float)blockHeight-2);
                }
            }
            //if (!blockCrushed)
            //{
            //    g.FillRectangle(brush, (float)blockPos.X, (float)blockPos.Y, (float)width, (float)height);
            //}

            brush = Brushes.LightGreen;
            sp = Cursor.Position;
            cp = PointToClient(sp);
            mouseX = cp.X;
            mouseY = cp.Y;

            if (started)
            {
                g.FillRectangle(brush, mouseX - (float)playerWidth / 2, (float)playerpos.Y, (float)playerWidth, (float)playerHeight);
            }
           

            label1.Text = ("X:" + mouseX + "  Y:" + mouseY);
            label4.Text = ("ボール速度:(" + ballSpeed.X.ToString("F1") + ", " + ballSpeed.Y.ToString("F1") + ")");
            label5.Text = ("プレーヤー幅:" + playerWidth.ToString("F1"));

            int blockCounter = 0;
            for(int i = 0; i < blockNum; i++)
            {
                if (!blockCrushed[i])
                {
                    blockCounter++;
                }
            }
            label6.Text = ("残りブロック:" + blockCounter);

            if (started)label3.Text = time;

            
        }

        public void Initialize()
        {
            label2.Visible = false;
            label3.Text = "00:00";

            pen = Pens.White;

            g = CreateGraphics();
            g.DrawLine(pen, PointToClient(new System.Drawing.Point(0, this.Bottom - 50)), PointToClient(new System.Drawing.Point(this.Width, this.Bottom-50)));
            g.Dispose();

            //ボール
            this.ballPos = new Vector(200, 200);
            this.ballSpeed = new Vector((double)f2.numericUpDown_XSpeed.Value, (double)f2.numericUpDown_YSpeed.Value);
            this.ballRadius = 5;
            
            //ブロック           
            blockNum = (int)f2.numericUpDown_BlockNum.Value;
            blockHeight = 10;
            blockWidth = 50;
            blockPos = new Vector[blockNum];
            blockCrushed = new bool[blockNum];           

            for (int i = 0; i < blockNum; i++)
            {
                blockPos[i] = new Vector(100 + (i % 5) * blockWidth, 100 + blockHeight * (i - (i % 5)) / 5);
                blockCrushed[i] = false;
            }

            //プレーヤー
            playerHeight = 10;
            playerWidth = (int)f2.numericUpDown_playerWidth.Value;
            playerpos = new Vector(200, 300);
  

        }

        //スタートボタン
        private void button_Start_Click(object sender, EventArgs e)
        {
            button_Reset.Enabled = true;
            started = true;
            Initialize();
            timer.Start();
            sw.Reset();
            sw.Start();
        }

        private void パラメータ調整ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ParameterForm f2 = new ParameterForm();    
            f2.Show();
        }

        //リセットボタン
        private void button_Reset_Click(object sender, EventArgs e)
        {
            //button_Reset.Enabled = true;
            //started = true;
            //Initialize();
            //timer.Start();
            //Thread.Sleep(20);
            //timer.Stop();
            //sw.Reset();            
        }
              

        private void ハイスコアＨToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f3.Show();
        }

        //Stop/Restartボタン
        private void button_Stop_Click(object sender, EventArgs e)
        {
            if (timer.Enabled == true)
            {
                timer.Stop();
            }
            else
            {
                timer.Start();
            }
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
