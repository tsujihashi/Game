using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    public class Enums
    {
        public enum FIELD_TYPE
        {
            wall,
            path,
            block,            
            goal,
            fire,
            //player,
            //enemy,
            bomb,
            player_and_bomb,
            player_and_goal,
            enemy_and_goal,
            powerItem,
            bombItem,
            max
        };

        public enum DIRECTION
        {
            up,
            down,
            left,
            right,
            max
        };

        public enum ITEM_TYPE
        {
            fireUp,
            bombUp,
            togeBomb,
            max
        };

        public enum ENEMY_TYPE
        {
            /// <summary>
            /// プロペン：特徴なし
            /// </summary>
            Propen,
            /// <summary>
            /// キンカル：ブロックをすり抜ける
            /// </summary>
            Kinkal,
            /// <summary>
            /// パクパ：爆弾を食べる
            /// </summary>
            Pakupa,

            max
        };
    }
}
