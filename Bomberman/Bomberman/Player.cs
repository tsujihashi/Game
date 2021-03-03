using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    /// <summary>
    /// プレーヤー
    /// </summary>
    public class Player
    {
        public bool isAlive;
        public int x;
        public int y;
        //public Bomb[] bomb = new Bomb[100];
        public List<Bomb> bombs = new List<Bomb>();
        //public int generatedBombNum;
        public int bombNum;
        public int _power;
        public bool togeBomb;
        public void PutBomb()
        {

        }
    }
}
