using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    /// <summary>
    /// 爆弾
    /// </summary>
    public class Bomb
    {
        public int x = 0;
        public int y = 0;
        public int power = 1;
        public bool on = false;
        public bool canPenetrate = false;


        public TimeSpan limit = new TimeSpan(30000000); //3s
        public Stopwatch life = new Stopwatch();

        //public Bomb(int x, int y, int power, bool on, limi)
        //{

        //}
    }
}
