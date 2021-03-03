using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Billiard
{
    public partial class Form1 : Form
    {
        Timer timer = new Timer();
        Pen forcePen;
        Brush brush;
        Graphics g;
        int upBound = 50;
        int downBound = 200;
        int rightBound = 500;
        int leftBound = 100;

        Vector[] pocketPos=new Vector[6];

        int ballID=int.MaxValue;
        int ballNum;
        Ball[] ball = new Ball[1000];
        
        double Myu;
        double G;
        double E;
        double forceGain;

        bool SettingForce = false;
        Vector startMousePos;
        Vector mousePos;
        Vector force;
        Vector cpVec;

        int Counter;

        public Form1()
        {
            InitializeComponent();

            label8.Visible = false;
            label8.Font = new Font("Arial", 20);
            label8.ForeColor = Color.Red;

            DoubleBuffered = true;

            Size = new System.Drawing.Size(700, 400);

            pictureBox1.BackColor = Color.Navy;

            timer.Interval = 5;
            timer.Tick += new EventHandler(Update);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Initiallize();
            timer.Start();
        }

        public void Initiallize()
        {
            label8.Visible = false;
            //for(int i = 0; i < 6; i++)
            //{
            //    Vector pPos = new Vector();
            //    pocketPos[i] = pPos;
            //}
            pocketPos[0] = new Vector(leftBound, upBound);
            pocketPos[1] = new Vector(leftBound, downBound);
            pocketPos[2] = new Vector((rightBound+leftBound)/2, upBound);
            pocketPos[3] = new Vector((rightBound+leftBound)/2, downBound);
            pocketPos[4] = new Vector(rightBound, upBound);
            pocketPos[5] = new Vector(rightBound, downBound);

            ballNum = (int)numericUpDown_ballNum.Value;
            //ボールを作成
            for(int i = 0; i < ballNum; i++)
            {
                Ball b = new Ball(i);
                ball[i] = b;
            }

            //物理パラメータを決定
            Myu = (double)numericUpDown_friction.Value;
            G = 9.8;
            E = (double)numericUpDown_E.Value;
            forceGain = (double)numericUpDown_force.Value;

            //ベクトルの初期化
            startMousePos = new Vector(0, 0);
            mousePos = new Vector(0, 0);
            force = new Vector(0, 0);
            cpVec = new Vector(0, 0);
        }

        //timer.Tickイベント
        private void Update(object sender,EventArgs e)
        {
            
            for(int i = 0; i < ballNum; i++)
            {
                if (!ball[i].inPocket)
                {
                    if (ball[i].Speed.Length != 0)
                    {
                        // ball[i].InitialSpeed = ball[i].Speed;
                        ball[i].Speed += ball[i].Deceleration;
                        ball[i].Position += ball[i].Speed;
                    }


                    // 左右の壁でのバウンド
                    if (ball[i].Position.X + ball[i].Radius > rightBound || ball[i].Position.X - ball[i].Radius < leftBound)
                    {
                        ball[i].Speed.X *= -E;
                        ball[i].Speed.Y *= E;
                        ball[i].Deceleration.X *= -1;
                        ball[i].Position += ball[i].Speed;
                    }

                    // 上下の壁でバウンド
                    if (ball[i].Position.Y - ball[i].Radius < upBound || ball[i].Position.Y + ball[i].Radius > downBound)
                    {
                        ball[i].Speed.Y *= -E;
                        ball[i].Speed.X *= E;
                        ball[i].Deceleration.Y *= -1;
                        ball[i].Position += ball[i].Speed;
                    }

                    //他のボールとの衝突判定
                    for (int j = i + 1; j < ballNum; j++)
                    {
                        if ((ball[i].Position - ball[j].Position).Length < (ball[i].Radius + ball[j].Radius))
                        {
                            ball[i].InitialSpeed = ball[i].Speed;
                            ball[j].InitialSpeed = ball[j].Speed;

                            ball[i].Speed
                            = ((ball[i].Mass - ball[j].Mass) * ball[i].InitialSpeed + 2 * ball[j].Mass * ball[j].InitialSpeed)
                            / (ball[i].Mass + ball[j].Mass);

                            ball[j].Speed
                                = ((ball[j].Mass - ball[i].Mass) * ball[j].InitialSpeed + 2 * ball[i].Mass * ball[i].InitialSpeed)
                                / (ball[i].Mass + ball[j].Mass);


                            if (ball[i].Speed.Length != 0)
                            {
                                ball[i].Deceleration = -Myu * G * ball[i].Speed / ball[i].Speed.Length;
                            }
                            else
                            {
                                ball[i].Deceleration.X = 0;
                                ball[i].Deceleration.Y = 0;
                            }

                            if (ball[j].Speed.Length != 0)
                            {
                                ball[j].Deceleration = -Myu * G * ball[j].Speed / ball[j].Speed.Length;
                            }
                            else
                            {
                                ball[j].Deceleration.X = 0;
                                ball[j].Deceleration.Y = 0;
                            }
                            //ball[i].Deceleration = -ball[i].Deceleration.Length * ball[i].Speed / ball[i].Speed.Length;
                            //ball[j].Deceleration = -ball[j].Deceleration.Length * ball[j].Speed / ball[j].Speed.Length;
                        }
                    }

                    //ポケットに入ったかどうかを判定
                    for (int j=0; j < 6; j++)
                    {
                        if((ball[i].Position - pocketPos[j]).Length < ball[i].Radius + 10)
                        {
                            ball[i].Speed.X = 0;
                            ball[i].Speed.Y = 0;
                            ball[i].Deceleration.X = 0;
                            ball[i].Deceleration.Y = 0;
                            ball[i].inPocket = true;
                        }
                        
                    }

                    
                }
               
                Invalidate();

                //動いているボールが停止したとき
                if (0<ball[i].Speed.Length&& ball[i].Speed.Length<0.1) 
                {
                    ball[i].Speed.X = 0;
                    ball[i].Speed.Y = 0;
                    ball[i].Deceleration.X = 0;
                    ball[i].Deceleration.Y = 0;
                    //Counter++;
                    //timer.Stop();
                    //ballID = int.MaxValue;
                }


                Counter = 0;
                for(int j = 0; j < ballNum; j++)
                {
                    if (ball[j].inPocket)
                    {
                        Counter++;
                    }
                }
                if (Counter == ballNum)
                {
                    //MessageBox.Show("Game Cleared");
                    pictureBox1.BackColor = Color.Navy;
                    timer.Stop();
                    label8.Visible = true;
                }

                //全てのボールが停止していたとき
                Counter = 0;
                for (int j = 0; j < ballNum; j++)
                {
                    if (ball[j].Speed.X == 0 && ball[j].Speed.Y == 0)
                    {
                        Counter++;
                    }
                }
                if (Counter == ballNum)
                {
                    pictureBox1.BackColor = Color.Navy;
                    timer.Stop();
                }

                
                
            }

            //Vector initialBallSpeed = ballSpeed;
            //ballSpeed -= deceleration;
            //ballPos += ballSpeed;

            //// 左右の壁でのバウンド
            //if (ballPos.X + ballRadius > this.Bounds.Width - 15 || ballPos.X - ballRadius < 0)
            //{
            //    ballSpeed.X *= -1;
            //    deceleration.X *= -1;
            //}

            //// 上下の壁でバウンド
            //if (ballPos.Y - ballRadius < 0 || ballPos.Y + ballRadius > this.Bounds.Height-30) 
            //{
            //    ballSpeed.Y *= -1;
            //    deceleration.Y *= -1;
            //}

            //Invalidate();

            //if (Math.Abs(ballSpeed.X) < 1 && Math.Abs(ballSpeed.Y) < 1) 
            //{
            //    timer.Stop();
            //}
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            forcePen = new Pen(Color.Brown, 5);
            g.DrawRectangle(forcePen, leftBound, upBound, rightBound - leftBound, downBound - upBound);
            brush = Brushes.Green;
            g.FillRectangle(brush, leftBound, upBound, rightBound - leftBound, downBound - upBound);
            g.FillPie(Brushes.Black, leftBound - 10, upBound - 10, 20, 20, 0, 90);
            g.FillPie(Brushes.Black, leftBound - 10, downBound - 10, 20, 20, 270, 90);
            g.FillPie(Brushes.Black, leftBound - 10 + (rightBound - leftBound) / 2, upBound - 10, 20, 20, 0, 180);
            g.FillPie(Brushes.Black, leftBound - 10 + (rightBound - leftBound) / 2, downBound - 10, 20, 20, 180, 180);
            g.FillPie(Brushes.Black, rightBound - 10, upBound - 10, 20, 20, 90, 90);
            g.FillPie(Brushes.Black, rightBound - 10, downBound - 10, 20, 20, 180, 90);



            //brush = Brushes.White; //ボールの色
            for(int i = 0; i < ballNum; i++)
            {
                if (!ball[i].inPocket)
                {
                    brush = ball[i].color;
                    var px = (float)ball[i].Position.X - (float)ball[i].Radius;
                    var py = (float)ball[i].Position.Y - (float)ball[i].Radius;
                    g.FillEllipse(brush, px, py, (float)ball[i].Radius * 2, (float)ball[i].Radius * 2);

                    if (i != 0)
                    {
                        Brush whiteBrush = Brushes.White;
                        var px_ = (float)(ball[i].Position.X - ball[i].Radius * 0.6);
                        var py_ = (float)(ball[i].Position.Y - ball[i].Radius * 0.6);
                        g.FillEllipse(whiteBrush, px_, py_, (float)(ball[i].Radius * 0.6 * 2), (float)(ball[i].Radius * 0.6 * 2));

                        Font font = new Font("Arial", 7);
                        Brush blackBrush = Brushes.Black;
                        g.DrawString(i.ToString(), font, blackBrush, (float)ball[i].Position.X - 5, (float)ball[i].Position.Y - 5);
                    }
                }
                
               
            }
            //var px = (float)ballPos.X - (float)ballRadius;
            //var py = (float)ballPos.Y - (float)ballRadius;
            //g.FillEllipse(brush, px, py, (float)ballRadius * 2, (float)ballRadius * 2);

            if (SettingForce)
            {
                System.Drawing.Point sp = Cursor.Position;
                System.Drawing.Point cp = PointToClient(sp);
                mousePos.X = cp.X;
                mousePos.Y = cp.Y;

                forcePen = new Pen(Color.Red,3);
                forcePen.EndCap = LineCap.ArrowAnchor;
                g.DrawLine(forcePen, (float)mousePos.X, (float)mousePos.Y,(float)startMousePos.X, (float)startMousePos.Y/*, (float)mousePos.X, (float)mousePos.Y*/);
            }

            //for(int i = 0; i*50 < this.Right; i++)
            //{
            //    pen = new Pen(Color.Gray, 1);
            //    g.DrawLine(pen, i * 50, 0, i * 50, this.Bottom);

            //}      
            if (ball[0] != null)
            {
                label5.Text = ("速度: (" + ball[0].Speed.X.ToString("0.0") + ", " + ball[0].Speed.Y.ToString("0.0") + ")");
                label6.Text = ("減速度: (" + ball[0].Deceleration.X.ToString("0.00") + ", " + ball[0].Deceleration.Y.ToString("0.00") + ")");
                label7.Text = ("加速度: (" + ball[0].Acceleration.X.ToString("0.00") + ", " + ball[0].Acceleration.Y.ToString("0.00") + ")");
            }
           
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //初期化
            startMousePos.X = 0;
            startMousePos.Y = 0;

            System.Drawing.Point sp = Cursor.Position;
            System.Drawing.Point cp = PointToClient(sp);
            //Vector spVec = new Vector(sp.X, sp.Y);
            cpVec = new Vector(cp.X, cp.Y);

            for(int i = 0; i < ballNum; i++)
            {
                if ((ball[i].Position - cpVec).Length < ball[i].Radius)
                {
                    SettingForce = true;
                    startMousePos = ball[i].Position;
                    ballID = i;
                    break;
                }
            }
            //if ((ballPos - cpVec).Length < ballRadius)
            //{
            //    SettingForce = true;
            //    startMousePos = ballPos;
            //}

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (SettingForce)
            {
                Invalidate();
                //force = ballPos - cpVec;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (SettingForce)
            {
                SettingForce = false;
                //cpVec = mousePos;
                for(int i = 0; i < ballNum; i++)
                {
                    if (i == ballID)
                    {
                       // ballID = int.MaxValue;
                        force = (ball[i].Position - mousePos) * forceGain;
                        ball[i].Acceleration = force / ball[i].Mass;
                        ball[i].Speed += ball[i].Acceleration;                       
                        ball[i].Deceleration = -Myu * G * (ball[i].Acceleration / ball[i].Acceleration.Length);
                        ball[i].Acceleration.X = 0;
                        ball[i].Acceleration.Y = 0;
                        break;
                    }
                }
                //force = (ballPos - mousePos)*forceGain;
                ////MessageBox.Show(ballPos.ToString() + "\r\n" + cpVec.ToString() + "\r\n" + forceVector.ToString());

                ////ボールに初速を与える
                //acceleration = force/ ballMass;
                //ballSpeed += acceleration;

                ////摩擦による減速度
                //deceleration = Myu * G * (acceleration / acceleration.Length);

                timer.Start();
                pictureBox1.BackColor = Color.LightGreen;
                Invalidate();
            }            
        }
    }
}
