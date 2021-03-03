using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Doubutsu_Shogi
{
    class StaticData
    {
        public static Grid[,] grids = new Grid[3, 4];
        public static string movingKoma="None";
        public static int iTemp;
        public static int jTemp;
        public static IPEndPoint endPoint;
        public static string Message;
        public static List<string> komaList_player1 = new List<string>();
        public static List<string> komaList_player2 = new List<string>();
        public static bool finished = false;
        
    }
}
