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

namespace MosaicGame
{
    public partial class Form1 : Form
    {
        Random r = new Random();
        List<Bitmap> bmpList = new List<Bitmap>();
        List<Tuple<int[],Color>> pixelColorList = new List<Tuple<int[], Color>>();
        int totalDrawNum = 0;
        const int drawNumPerTick = 100;
        Graphics g ;
        const int pixelSize = 3;
        Stopwatch sw = new Stopwatch();


        public Form1()
        {
            InitializeComponent();

            timer1.Interval = 100;
           
            

        }

        private void button_start_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            totalDrawNum = 0;
            for (int i=0; ;i++)
            {
                try
                {
                    Image img = Image.FromFile(Directory.GetCurrentDirectory() + "\\img\\" + i + ".jpg");
                    bmpList.Add(new Bitmap(img));
                }
                catch
                {
                    break;
                }               
            }

            int bmpIdx = r.Next(0, bmpList.Count);

            pictureBox1.Width = bmpList[bmpIdx].Width * pixelSize;
            pictureBox1.Height = bmpList[bmpIdx].Height * pixelSize;
            g = pictureBox1.CreateGraphics();

            for (int x = 0; x < bmpList[bmpIdx].Width; x++)
            {
                for(int y = 0; y < bmpList[bmpIdx].Height; y++)
                {
                    int[] xy = new int[2] { x, y };
                    Color color = bmpList[bmpIdx].GetPixel(x, y);
                    pixelColorList.Add(new Tuple<int[],Color>(xy, color));
                }
            }
            
            pixelColorList = pixelColorList.OrderBy(i => Guid.NewGuid()).ToList();
            
            timer1.Start();
            sw.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            int removeNum = 0;

            while(removeNum < drawNumPerTick)
            {
                if (pixelColorList.Count != 0)
                {
                    int x = pixelColorList[0].Item1[0];
                    int y = pixelColorList[0].Item1[1];
                    Color color = pixelColorList[0].Item2;
                    g.FillRectangle(new SolidBrush(color), x * pixelSize, y * pixelSize, pixelSize, pixelSize);
                    pixelColorList.RemoveAt(0);
                    removeNum++;
                    totalDrawNum++;
                }
                else
                {
                    timer1.Stop();
                    break;
                }
                       
            }
           

            label1.Text = "描画ピクセル数："+totalDrawNum.ToString();
            label2.Text = "経過時間："+sw.Elapsed.Seconds+"."+sw.Elapsed.Milliseconds+"秒";

            //Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Graphics g = e.Graphics;
            //for(int i=0;i<drawIdx)
            //g.FillRectangle(new SolidBrush())
        }
    }
}
