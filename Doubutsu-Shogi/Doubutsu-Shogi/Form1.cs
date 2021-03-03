using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Doubutsu_Shogi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        UdpClient client = new UdpClient();
        int x0_board = 200;
        int y0_board = 100;
        int width_board = 50;
        int height_board = 50;
        int x0_komadai_player1 = 400;
        int y0_komadai_player1 = 100;
        int height_komadai_player1 = 50;
        int x0_komadai_player2 = 100;
        int y0_komadai_player2 = 100;
        int height_komadai_player2 = 50;



        int turn = 1;

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = CreateGraphics();
            //int x0 = 200;
            //int y0 = 100;
            //int width = 50;
            //int height = 50;
            //Grid grid = new Grid();
            Grid[,] gr = StaticData.grids;
            
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    g.DrawRectangle(Pens.Black, x0_board + i * width_board, y0_board + j * height_board, width_board, height_board);
                    Grid grid = new Grid();
                    gr[i, j] = grid;
                  
                }
            }
            
            //初期配置
            gr[0, 0].gridType = "Giraffe";
            gr[0, 0].Player = 2;
            gr[0, 1].gridType = "None";
            gr[0, 1].Player = 0;
            gr[0, 2].gridType = "None";
            gr[0, 2].Player = 0;
            gr[0, 3].gridType = "Elephant";
            gr[0, 3].Player = 1;
            gr[1, 0].gridType = "Lion";
            gr[1, 0].Player = 2;
            gr[1, 1].gridType = "Chick";
            gr[1, 1].Player = 2;
            gr[1, 2].gridType = "Chick";
            gr[1, 2].Player = 1;
            gr[1, 3].gridType = "Lion";
            gr[1, 3].Player = 1;
            gr[2, 0].gridType = "Elephant";
            gr[2, 0].Player = 2;
            gr[2, 1].gridType = "None";
            gr[2, 1].Player = 0;
            gr[2, 2].gridType = "None";
            gr[2, 2].Player = 0;
            gr[2, 3].gridType = "Giraffe";
            gr[2, 3].Player = 1;

            turn = 1;

            //Font font = new Font("Arial", 35);
            //for(int i = 0; i < 3; i++)
            //{
            //    for(int j = 0; j < 4; j++)
            //    {
            //        if (gr[i, j].gridType == "Elephant")
            //        {
            //            g.DrawString("E", font, Brushes.Black, x0 + i * width, y0 + j * height);
            //        }
            //        else if (gr[i, j].gridType == "Chick")
            //        {
            //            g.DrawString("C", font, Brushes.Black, x0 + i * width, y0 + j * height);
            //        }
            //        else if (gr[i, j].gridType == "Lion")
            //        {
            //            g.DrawString("L", font, Brushes.Black, x0 + i * width, y0 + j * height);
            //        }
            //        else if (gr[i, j].gridType == "Giraffe")
            //        {
            //            g.DrawString("G", font, Brushes.Black, x0 + i * width, y0 + j * height);
            //        }

            //    }
            //}
            Refresh();

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Point mousePos = Cursor.Position;
            mousePos=PointToClient(mousePos);
            double mouseX = mousePos.X;
            double mouseY = mousePos.Y;
            Grid[,] gr = StaticData.grids;


            //MessageBox.Show(mouseX.ToString() + ", " + mouseY.ToString());
            for (int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    if(x0_board+i*width_board<mouseX
                        &&mouseX< x0_board +(i+1) * width_board
                        &&y0_board+j*height_board<mouseY
                        && mouseY < y0_board + (j + 1) * height_board)
                    {
                        //ひよこ
                        if (gr[i, j].gridType == "Chick"&& StaticData.movingKoma=="None"&&gr[i,j].Player==turn)
                        {

                            if (j != 0 && gr[i, j - 1].Player!=turn)
                            {
                                //一つ前のマスを移動可能にする
                                gr[i, j - 1].marked = true;
                                StaticData.movingKoma = "Chick";
                                StaticData.iTemp = i;
                                StaticData.jTemp = j;
                            }                                                     

                           
                        }

                        //ライオン
                        else if (gr[i, j].gridType == "Lion" && StaticData.movingKoma == "None" && gr[i, j].Player == turn)
                        {
                            bool canMove = false;
                            for (int k = i - 1; k < i + 2; k++)
                            {
                                for (int l = j - 1; l < j + 2; l++)
                                {
                                    if (0 <= k && k <= 2 && 0 <= l && l <= 3 && gr[k, l].Player != turn)
                                    {
                                        gr[k, l].marked = true;
                                        canMove = true;
                                       
                                    }

                                }
                            }
                            if (canMove)
                            {
                                StaticData.movingKoma = "Lion";
                                StaticData.iTemp = i;
                                StaticData.jTemp = j;
                            }
                        }

                        //象
                        else if (gr[i, j].gridType == "Elephant" && StaticData.movingKoma == "None" && gr[i, j].Player == turn)
                        {
                            bool canMove = false;
                            if (0 <= i - 1 && 0 <= j - 1 && gr[i - 1, j - 1].Player != turn)
                            {
                                gr[i - 1, j - 1].marked = true;
                                canMove = true;                                                         
                            }
                            if (0 <=i - 1 && j + 1<=3 && gr[i - 1, j + 1].Player != turn)
                            {
                                gr[i - 1, j + 1].marked = true;
                                canMove = true;
                            }
                            if (i + 1<=2 && 0<=j - 1 && gr[i + 1, j - 1].Player != turn)
                            {
                                gr[i + 1, j - 1].marked = true;
                                canMove = true;
                            }
                            if (i + 1<=2 && j + 1 <= 3 && gr[i + 1, j + 1].Player != turn)
                            {
                                gr[i + 1, j + 1].marked = true;
                                canMove = true;
                            }

                            if (canMove)
                            {
                                StaticData.movingKoma = "Elephant";
                                StaticData.iTemp = i;
                                StaticData.jTemp = j;
                            }
                            
                        }
                        //きりん
                        else if (gr[i, j].gridType == "Giraffe" && StaticData.movingKoma == "None" && gr[i, j].Player == turn)
                        {
                            bool canMove = false;
                            if (0 <= i - 1&& gr[i - 1, j].Player != turn)
                            {
                                gr[i - 1, j].marked = true;
                                canMove = true;
                            }
                            if (i + 1<=2&& gr[i + 1, j].Player != turn)
                            {
                                gr[i + 1, j].marked = true;
                                canMove = true;
                            }
                            if (0 <= j - 1 && gr[i, j - 1].Player != turn)
                            {
                                gr[i, j - 1].marked = true;
                                canMove = true;
                            }
                            if (j + 1 <= 3 && gr[i, j + 1].Player != turn)
                            {
                                gr[i, j + 1].marked = true;
                                canMove = true;
                            }

                            if (canMove)
                            {
                                StaticData.movingKoma = "Giraffe";
                                StaticData.iTemp = i;
                                StaticData.jTemp = j;
                            }

                        }

                        //にわとり（ひよこから進化）
                        else if (gr[i, j].gridType == "Chicken" && StaticData.movingKoma == "None" && gr[i, j].Player == turn)
                        {
                            bool canMove = false;
                            if (0 <= i - 1 && gr[i - 1, j].Player != turn)
                            {
                                gr[i - 1, j].marked = true;
                                canMove = true;
                            }
                            if (i + 1 <= 2 && gr[i + 1, j].Player != turn)
                            {
                                gr[i + 1, j].marked = true;
                                canMove = true;
                            }
                            if (0 <= j - 1 && gr[i, j - 1].Player != turn)
                            {
                                gr[i, j - 1].marked = true;
                                canMove = true;
                            }
                            if (j + 1 <= 3 && gr[i, j + 1].Player != turn)
                            {
                                gr[i, j + 1].marked = true;
                                canMove = true;
                            }
                            if (0 <= i - 1 && 0 <= j - 1 && gr[i - 1, j - 1].Player != turn)
                            {
                                gr[i - 1, j - 1].marked = true;
                                canMove = true;
                            }
                            if (i + 1 <= 2 && 0 <= j - 1 && gr[i + 1, j - 1].Player != turn)
                            {
                                gr[i + 1, j - 1].marked = true;
                                canMove = true;
                            }

                            if (canMove)
                            {
                                StaticData.movingKoma = "Chicken";
                                StaticData.iTemp = i;
                                StaticData.jTemp = j;
                            }

                        }

                        //移動先（マーク付）
                        if (gr[i, j].marked)
                        {
                            //全マスのマークをはずす
                            for(int k = 0; k < 3; k++)
                            {
                                for(int l = 0; l < 4; l++)
                                {
                                    gr[k, l].marked = false;
                                }
                            }

                            //相手の駒をとる
                            if (gr[i, j].Player == 2 && turn == 1)                            
                            {
                                string koma_captured = gr[i, j].gridType;
                                if (koma_captured == "Lion")
                                {
                                    StaticData.finished = true;
                                   
                                }
                                else
                                {
                                    StaticData.komaList_player1.Add(koma_captured);
                                }                               
                            }
                            if (gr[i, j].Player == 1 && turn == 2)
                            {
                                string koma_captured = gr[i, j].gridType;
                                if (koma_captured == "Lion")
                                {
                                    StaticData.finished = true;
                                  
                                }
                                else
                                {
                                    StaticData.komaList_player2.Add(koma_captured);
                                }                                
                            }

                            //ひよこは相手陣一段目にたどり着くとにわとりに進化  
                            if(j==0&&StaticData.movingKoma=="Chick")
                            {
                                gr[i, j].gridType = "Chicken";
                            }
                            else
                            {
                                gr[i, j].gridType = StaticData.movingKoma;
                            }

                            gr[i, j].Player = turn;     
                            gr[StaticData.iTemp, StaticData.jTemp].gridType = "None";
                            gr[StaticData.iTemp, StaticData.jTemp].Player = 0;
                            StaticData.movingKoma = "None";
                            if (turn == 1)
                            {
                                turn = 2;
                            }
                            else
                            {
                                turn = 1;
                            }

                        }
                    }
                 
                }
            }

            for (int i = 0; i < StaticData.komaList_player1.Count; i++)
            {
                if(x0_komadai_player1<mouseX
                    &&mouseX<x0_komadai_player1+ height_komadai_player1
                    && y0_komadai_player1+i*height_komadai_player1<mouseY
                    &&mouseY< y0_komadai_player1 + (i+1) * height_komadai_player1)
                {
                   for(int k = 0; k < 3; k++)
                    {
                        for(int l = 0; l < 4; l++)
                        {
                            if (gr[k, l].gridType == "None")
                            {
                                gr[k, l].marked = true;
                            }
                        }
                    }

                    StaticData.movingKoma = StaticData.komaList_player1[i];
                    StaticData.komaList_player1.RemoveAt(i);
                }
            }

            for (int i = 0; i < StaticData.komaList_player2.Count; i++)
            {

            }


            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = CreateGraphics();            
            //Grid grid = new Grid();
            Grid[,] gr = StaticData.grids;

            

            Font font = new Font("Arial", 35);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (gr[i, j] != null)
                    {
                        g.DrawRectangle(Pens.Black, x0_board + i * width_board, y0_board + j * height_board, width_board, height_board);

                        Brush brush;

                        if (gr[i, j].Player == 1)
                        {
                            brush = Brushes.SandyBrown;
                        }
                        else 
                        {
                            brush = Brushes.SaddleBrown;
                        }

                        if (gr[i, j].gridType == "Elephant")
                        {
                            g.FillRectangle(brush, x0_board + i * width_board + 5, y0_board + j * height_board + 5, width_board - 10, height_board - 10);
                            g.DrawString("E", font, Brushes.Black, x0_board + i * width_board, y0_board + j * height_board);
                        }
                        else if (gr[i, j].gridType == "Chick")
                        {
                            g.FillRectangle(brush, x0_board + i * width_board + 5, y0_board + j * height_board + 5, width_board - 10, height_board - 10);
                            g.DrawString("C", font, Brushes.Black, x0_board + i * width_board, y0_board + j * height_board);
                        }
                        else if (gr[i, j].gridType == "Lion")
                        {
                            g.FillRectangle(brush, x0_board + i * width_board + 5, y0_board + j * height_board + 5, width_board - 10, height_board - 10);
                            g.DrawString("L", font, Brushes.Black, x0_board + i * width_board, y0_board + j * height_board);
                        }
                        else if (gr[i, j].gridType == "Giraffe")
                        {
                            g.FillRectangle(brush, x0_board + i * width_board + 5, y0_board + j * height_board + 5, width_board - 10, height_board - 10);
                            g.DrawString("G", font, Brushes.Black, x0_board + i * width_board, y0_board + j * height_board);
                        }
                        else if (gr[i, j].gridType == "Chicken")
                        {
                            g.FillRectangle(brush, x0_board + i * width_board + 5, y0_board + j * height_board + 5, width_board - 10, height_board - 10);
                            g.DrawString("C", font, Brushes.Red, x0_board + i * width_board, y0_board + j * height_board);
                        }

                        if (gr[i, j].marked)
                        {
                            g.FillEllipse(Brushes.Red, x0_board + i * width_board + width_board / 2 - 5, y0_board + j * height_board + height_board / 2 - 5, 10, 10);
                        }
                    }

                   
                }
                

            }

            //持ち駒の描画
            for (int i = 0; i < StaticData.komaList_player1.Count; i++)
            {
                Brush brush = Brushes.SandyBrown;

                if (StaticData.komaList_player1[i] == "Elephant")
                {
                    g.FillRectangle(brush, x0_komadai_player1+5, y0_komadai_player1+5+i*height_komadai_player1, height_komadai_player1 - 10, height_board - 10);
                    g.DrawString("E", font, Brushes.Black, x0_komadai_player1, y0_komadai_player1+i * height_komadai_player1);
                }

                if (StaticData.komaList_player1[i] == "Giraffe")
                {
                    g.FillRectangle(brush, x0_komadai_player1 + 5, y0_komadai_player1 + 5 + i * height_komadai_player1, height_komadai_player1 - 10, height_board - 10);
                    g.DrawString("G", font, Brushes.Black, x0_komadai_player1, y0_komadai_player1 + i * height_komadai_player1);
                }

                if (StaticData.komaList_player1[i] == "Chick"|| StaticData.komaList_player1[i] == "Chicken")
                {
                    g.FillRectangle(brush, x0_komadai_player1 + 5, y0_komadai_player1 + 5 + i * height_komadai_player1, height_komadai_player1 - 10, height_board - 10);
                    g.DrawString("C", font, Brushes.Black, x0_komadai_player1, y0_komadai_player1 + i * height_komadai_player1);
                }


            }

            for (int i = 0; i < StaticData.komaList_player2.Count; i++)
            {
                Brush brush = Brushes.SaddleBrown;

                if (StaticData.komaList_player2[i] == "Elephant")
                {
                    g.FillRectangle(brush, x0_komadai_player2 + 5, y0_komadai_player2 + 5 + i * height_komadai_player2, height_komadai_player2 - 10, height_board - 10);
                    g.DrawString("E", font, Brushes.Black, x0_komadai_player2, y0_komadai_player2 + i * height_komadai_player2);
                }

                if (StaticData.komaList_player2[i] == "Giraffe")
                {
                    g.FillRectangle(brush, x0_komadai_player2 + 5, y0_komadai_player2 + 5 + i * height_komadai_player2, height_komadai_player2 - 10, height_board - 10);
                    g.DrawString("G", font, Brushes.Black, x0_komadai_player2, y0_komadai_player2 + i * height_komadai_player2);
                }

                if (StaticData.komaList_player2[i] == "Chick" || StaticData.komaList_player2[i] == "Chicken")
                {
                    g.FillRectangle(brush, x0_komadai_player2 + 5, y0_komadai_player2 + 5 + i * height_komadai_player2, height_komadai_player2 - 10, height_board - 10);
                    g.DrawString("C", font, Brushes.Black, x0_komadai_player2, y0_komadai_player2 + i * height_komadai_player2);
                }


            }

            if (StaticData.finished)
            {
                if (turn == 1)
                {
                    turn = 2;
                }
                else
                {
                    turn = 1;
                }
                label4.Text = "プレーヤー" + turn.ToString() + "の勝ちです．";
               // MessageBox.Show("プレーヤー" + turn.ToString() + "の勝ちです．");
                
            }
            else
            {
                label4.Text = "プレーヤー" + turn + "の番です。";
            }
           // StaticData.finished = false;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            Thread listenThread = new Thread(new ThreadStart(listen));
            label3.Visible = true;
            
        }

        public void listen()
        {
            IPAddress ip = IPAddress.Parse(textBox1.Text);
            int port = int.Parse(textBox2.Text);
            client = new UdpClient(port);

            IPEndPoint remote = null;
            while (true)
            {
                var buffer = client.Receive(ref remote);
                StaticData.Message = Encoding.UTF8.GetString(buffer);
            }
            
        }
    }
}
