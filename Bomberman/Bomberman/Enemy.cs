using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    /// <summary>
    /// 敵
    /// </summary>
    public class Enemy
    {
        public Enums.DIRECTION direction = Enums.DIRECTION.left;
        public Enums.ENEMY_TYPE type;
        public int x;
        public int y;

        //特殊能力
        //--------------------------------------------------
        /// <summary>
        /// ブロックを通れる
        /// </summary>
        public bool canPassBlock = false;
        /// <summary>
        /// 爆弾を食べる
        /// </summary>
        public bool canEatBomb = false;
        /// <summary>
        /// 交差点で方向転換できる
        /// </summary>
        public bool canChangeDirectionAtCross = false;
        /// <summary>
        /// プレーヤーを追跡できる
        /// </summary>
        public bool canChasePlayer = false;
        //--------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="idx"></param>
        public Enemy(int idx)
        {
            switch (idx)
            {
                //プロペン
                case 0: this.type = Enums.ENEMY_TYPE.Propen; break;
                // キンカル
                case 1: this.type = Enums.ENEMY_TYPE.Kinkal;
                    canPassBlock = true;
                    canChangeDirectionAtCross = true;
                    break;
                //パクパ
                case 2: this.type = Enums.ENEMY_TYPE.Pakupa;
                    canEatBomb = true;
                    break;
            }
        }
    }


}
