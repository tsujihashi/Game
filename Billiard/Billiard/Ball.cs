using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Billiard
{
    public class Ball
    {
        public Vector Position;
        public Vector Speed;
        public Vector InitialSpeed;
        public double Radius;
        public double Mass;
        public Vector Acceleration;
        public Vector Deceleration;
        public bool inPocket;
        public Brush color;
        

        public Ball(int i)
        {
            Position.X = 200+i  * 30;
            Position.Y = 100;
            Speed.X = 0;
            Speed.Y = 0;
            Radius = 8;
            Mass = 50;
            Acceleration.X = 0;
            Acceleration.Y = 0;
            inPocket = false;

            switch (i)
            {
                case 0:color = Brushes.White;
                    break;
                case 1:color = Brushes.Yellow;
                    break;
                case 2:color = Brushes.Blue;
                    break;
                case 3:color = Brushes.Red;
                    break;
                case 4:color = Brushes.Purple;
                    break;
                case 5:color = Brushes.Orange;
                    break;
                case 6:color = Brushes.DarkGreen;
                    break;
                case 7:color = Brushes.DarkMagenta;
                    break;
                case 8:color = Brushes.Black;
                    break;
                //case 8:color=Color.


            }
                
        }
    }
}
