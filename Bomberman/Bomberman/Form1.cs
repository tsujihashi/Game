using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Bomberman
{
    public partial class Form1 : Form
    {
        //各種変数
        //------------------------------------------------------------------------------------------------------------------------------------
        #region 変数
        public int field_height;
        public int field_width;
        public int Ox;
        public int Oy;
        public Enums.FIELD_TYPE field_Type;
        Graphics g;
        public bool started = false;
        public Enums.FIELD_TYPE[,] field;
        Random r=new Random();
        Player player = new Player();
        Stopwatch sw = new Stopwatch();
        List<Point> firePosition = new List<Point>();
        public int InitialPower;
        public bool isGameOver;
        List<Enemy> enemies = new List<Enemy>();
        public bool isFirst = true;
        int enemyNum;
        bool refresh;
        //List<Point> itemPoints = new List<Point>();
        Point Goal;
        public int itemNum;
        List<Item> items = new List<Item>();
        int grid_Size = 16;
        int player_Size = 12;
        int bomb_Size = 10;
        int enemy_Size = 12;
        int InitialBombNum;
        TimeSpan ts = new TimeSpan();
        List<string[]> highScoreList = new List<string[]>();
        Format format=new Format();

        string upKey;
        string downKey;
        string leftKey;
        string rightKey;
        string bombKey;
        #endregion
        //------------------------------------------------------------------------------------------------------------------------------------

        public Form1()
        {
            InitializeComponent();

            timer1.Interval = 1000;

            field_Type = new Enums.FIELD_TYPE();

            Ox = 200;
            Oy = 150;

            label_Time.Top = Oy - 40;
            label_Time.Left = Ox;
            label_Time.Font = new Font("Arial", 10);           

            label_Status.Top = Oy - 20;
            label_Status.Left = Ox;

            DoubleBuffered = true;

        }

        //初期化ボタン
        private void button_Initialize_Click(object sender, EventArgs e)
        {
            started = true;
            sw.Stop();
            sw.Reset();
            var time = format.ChangeFormatToMMSS(sw);            
            label_Time.Text = time;
            label_Time.Visible = true;

            timer1.Stop();

            Cursor = Cursors.WaitCursor;
            Initialize();

            //フォームのKeyPreviewを切り替える
            KeyPreview = !KeyPreview;

            Cursor = Cursors.Default;
        }

        //スタートボタン
        private void buttonStart_Click(object sender, EventArgs e)
        {
            started = true;
            timer1.Start();            
            sw.Start();
            for (int i = 0; i < player.bombs.Count; i++)
            {
                player.bombs[i].life.Start();
            }
        }      

        //一時停止ボタン
        private void button_Stop_Click(object sender, EventArgs e)
        {
            started = false;
            timer1.Stop();
            sw.Stop();
            for(int i = 0; i < player.bombs.Count; i++)
            {
                player.bombs[i].life.Stop();
            }
        }

        //初期化
        public void Initialize()
        {            
            //パラメータの取得
            field_height = (int)numericUpDown_Height.Value;
            field_width = (int)numericUpDown_Width.Value;
            InitialPower = (int)numericUpDown_power.Value;
            InitialBombNum = (int)numericUpDown_BombNum.Value;
            enemyNum = (int)numericUpDown_enemy.Value;
            itemNum = (int)numericUpDown_ItemNum.Value;
            upKey = textBox_UpKey.Text;
            downKey = textBox_DownKey.Text;
            leftKey = textBox_LeftKey.Text;
            rightKey = textBox_RightKey.Text;
            bombKey = textBox_BombKey.Text;

            field = new Enums.FIELD_TYPE[field_width, field_height];                    

            enemies = new List<Enemy>();

            isGameOver = false;

            Goal = new Point(field_width - 2, field_height - 2);

            label_Status.Visible = false;
            

            for (int i = 0; i < field_width; i++)
            {
                for (int j = 0; j < field_height; j++)
                {
                    if ((i == 0 || i == field_width - 1)|| (j == 0 || j == field_height - 1))
                    {
                        field[i, j] = Enums.FIELD_TYPE.wall;
                    }
                    else if (i % 2 == 0&&j%2==0)
                    {
                        field[i, j] = Enums.FIELD_TYPE.wall;
                    }
                    else
                    {
                        int rand = r.Next(0, 3);
                        if (rand % 3 == 0)
                        {
                            //3分の1はブロック
                            field[i, j] = Enums.FIELD_TYPE.block;
                        }
                        else
                        {
                            //3分の2は通路
                            field[i, j] = Enums.FIELD_TYPE.path;
                        }

                        //プレーヤーの初期位置周辺
                        field[1, 1] = Enums.FIELD_TYPE.path;
                        field[1, 2] = Enums.FIELD_TYPE.path;
                        field[2, 1] = Enums.FIELD_TYPE.path;
                        field[1, 3] = Enums.FIELD_TYPE.block;
                        field[3, 1] = Enums.FIELD_TYPE.block;

                        //ゴールの初期位置は必ず通路
                        field[Goal.X,Goal.Y]= Enums.FIELD_TYPE.path;
                    }
                }
            }

            //プレーヤーの初期化
            player.isAlive = true;
            player.x = 1;
            player.y = 1;
            player.bombNum = InitialBombNum;
            player._power = InitialPower;
            player.bombs.Clear();
            //field[player.x, player.y] = Enums.Field_type.player;
           
            //敵の初期化
            for(int i = 0; i < enemyNum; i++)
            {
                int typeIdx = r.Next(0, (int)Enums.ENEMY_TYPE.max);               

                Enemy enm = new Enemy(typeIdx);

                enm.x = r.Next(1, field_width);
                enm.y = r.Next(1, field_height);

                //通路以外の部分やプレーヤーの周辺に出現した場合はやり直し
                //（敵数が多過ぎると無限ループにはまるので注意）
                while (field[enm.x, enm.y] != Enums.FIELD_TYPE.path
                    || (enm.x == 1 && enm.y == 1)
                    || (enm.x == 1 && enm.y == 2)
                    || (enm.x == 2 && enm.y == 1)
                    || CheckEnemies(enm.x, enm.y))
                {
                    enm.x = r.Next(1, field_width);
                    enm.y = r.Next(1, field_height);
                }

                enm.direction = Enums.DIRECTION.left;                

                //field[enm.x, enm.y] = Enums.Field_type.enemy;
                
                enemies.Add(enm);
            }

            //ランダムなブロックの下にアイテムを設置    
            items.Clear();
            List<Point> pointList = new List<Point>();
            while (items.Count < itemNum)
            {
                Item item = new Item();
                int typeIdx = r.Next(0, (int)Enums.ITEM_TYPE.max);
                switch (typeIdx)
                {
                    case 0: item.type = Enums.ITEM_TYPE.fireUp; break;
                    case 1: item.type = Enums.ITEM_TYPE.bombUp; break;
                    case 2: item.type = Enums.ITEM_TYPE.togeBomb; break;                   
                }
                bool added = false;
                while (!added)
                {
                    int randX = r.Next(1, field_width - 1);
                    int randY = r.Next(1, field_height - 1);
                    Point p = new Point(randX, randY);
                    if (field[randX, randY] == Enums.FIELD_TYPE.block && !pointList.Contains(p))
                    {
                        pointList.Add(p);
                        added = true;
                        item.x = p.X;
                        item.y = p.Y;
                        items.Add(item);
                    }
                }
            }

            //ゴールの初期化
            field[field_width - 2, field_height - 2] = Enums.FIELD_TYPE.goal;

            //再描画
            refresh = true;
            Refresh();

            textBox_Log.Text += "Initialize Succeeded\r\n";
        }

        //描画
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (refresh)
            {
                //Console.Clear();

                //int grid_Size = 16;
                //int player_Size = 12;
                //int bomb_Size = 10;
                //int enemy_Size = 12;

                if (started)
                {
                    g = e.Graphics;

                    for (int i = 0; i < field_width; i++)
                    {
                        for (int j = 0; j < field_height; j++)
                        {
                            switch (field[i, j])
                            {
                                case Enums.FIELD_TYPE.wall:
                                    g.FillRectangle(Brushes.Green, Ox + i * grid_Size, Oy + j * grid_Size, grid_Size, grid_Size);
                                    break;
                                case Enums.FIELD_TYPE.path:
                                    g.FillRectangle(Brushes.Gray, Ox + i * grid_Size, Oy + j * grid_Size, grid_Size, grid_Size);
                                    break;
                                case Enums.FIELD_TYPE.block:
                                    g.FillRectangle(Brushes.Brown, Ox + i * grid_Size, Oy + j * grid_Size, grid_Size, grid_Size);
                                    break;
                                case Enums.FIELD_TYPE.fire:
                                    g.FillRectangle(Brushes.Red, Ox + i * grid_Size, Oy + j * grid_Size, grid_Size, grid_Size);
                                    break;                               
                                case Enums.FIELD_TYPE.goal:
                                    g.FillRectangle(Brushes.LightBlue, Ox + i * grid_Size, Oy + j * grid_Size, grid_Size, grid_Size);
                                    break;
                                //case Enums.FIELD_TYPE.player_and_goal:
                                //    g.FillRectangle(Brushes.LightBlue, Ox + i * grid_Size, Oy + j * grid_Size, grid_Size, grid_Size);
                                //    if (player.isAlive)
                                //    {
                                //        g.FillEllipse(Brushes.White, Ox + player.x * grid_Size + (grid_Size - player_Size) / 2,
                                //                                     Oy + player.y * grid_Size + (grid_Size - player_Size) / 2,
                                //                                     player_Size,
                                //                                     player_Size);
                                //    }
                                //    break;
                                //case Enums.FIELD_TYPE.enemy_and_goal:
                                //    g.FillRectangle(Brushes.LightBlue, Ox + i * grid_Size, Oy + j * grid_Size, grid_Size, grid_Size);
                                //    g.FillEllipse(Brushes.Blue, Ox + i * grid_Size + (grid_Size - enemy_Size) / 2,
                                //                               Oy + j * grid_Size + (grid_Size - enemy_Size) / 2,
                                //                               enemy_Size,
                                //                               enemy_Size);
                                //    break;
                            }

                            //アイテム等の描画
                            for (int k = 0; k < items.Count; k++)
                            {
                                if (i == items[k].x && j == items[k].y)
                                {
                                    g.FillRectangle(Brushes.Yellow, Ox + i * grid_Size, Oy + j * grid_Size, grid_Size, grid_Size);
                                    Font f = new Font("Arial", 8);
                                    switch (items[k].type)
                                    {
                                        case Enums.ITEM_TYPE.fireUp:
                                            g.DrawString("F", f, Brushes.Black, Ox + i * grid_Size, Oy + j * grid_Size);
                                            break;
                                        case Enums.ITEM_TYPE.bombUp:
                                            g.DrawString("B", f, Brushes.Black, Ox + i * grid_Size, Oy + j * grid_Size);
                                            break;
                                        case Enums.ITEM_TYPE.togeBomb:
                                            g.DrawString("T", f, Brushes.Black, Ox + i * grid_Size, Oy + j * grid_Size);
                                            break;
                                    }
                                    if (field[i, j] == Enums.FIELD_TYPE.block)
                                    {
                                        //ブロックの上書き
                                        g.FillRectangle(Brushes.Brown, Ox + i * grid_Size, Oy + j * grid_Size, grid_Size, grid_Size);
                                    }
                                }
                            }

                            //プレーヤーの描画
                            if (i == player.x && j == player.y)
                            {
                                if (player.isAlive)
                                {
                                    g.FillEllipse(Brushes.White, Ox + player.x * grid_Size + (grid_Size - player_Size) / 2,
                                                                 Oy + player.y * grid_Size + (grid_Size - player_Size) / 2,
                                                                 player_Size,
                                                                 player_Size);
                                }
                            }

                            //敵の描画
                            for(int k = 0; k < enemies.Count; k++)
                            {
                                if (i == enemies[k].x && j == enemies[k].y)
                                {
                                    Brush br;
                                    switch((int)enemies[k].type)
                                    {
                                        case 0: br = Brushes.Blue;  break;
                                        case 1: br = Brushes.Purple; break;
                                        case 2: br = Brushes.Pink; break;
                                        default:br = Brushes.White; break;

                                    }
                                    g.FillEllipse(br, Ox + i * grid_Size + (grid_Size - enemy_Size) / 2,
                                                              Oy + j * grid_Size + (grid_Size - enemy_Size) / 2,
                                                              enemy_Size,
                                                              enemy_Size);
                                }
                            }

                            //爆弾の描画
                            for (int k = 0; k < player.bombs.Count; k++)
                            {
                                if (i == player.bombs[k].x && j == player.bombs[k].y)
                                {
                                    g.FillEllipse(Brushes.Black, Ox + i * grid_Size + (grid_Size - bomb_Size) / 2,
                                                               Oy + j * grid_Size + (grid_Size - bomb_Size) / 2,
                                                               bomb_Size,
                                                               bomb_Size);
                                }
                            }                 

                            
                        }
                    }                   
                }

                //コンソール（デバッグ用）
                //------------------------------------------------------------------------------------------------------------------------------------
                #region コンソール
                if (checkBox_Console.Checked)
                {
                    Console.Write("\r\n");
                    Console.Write("Time: "+format.ChangeFormatToMMSS(sw)+"\r\n");
                    for (int j = 0; j < field_height; j++)
                    {
                        for (int i = 0; i < field_width; i++)
                        {
                            switch (field[i, j])
                            {
                                case Enums.FIELD_TYPE.wall:
                                    Console.Write("■");
                                    break;
                                case Enums.FIELD_TYPE.block:
                                    Console.Write("□");
                                    break;
                                case Enums.FIELD_TYPE.path:
                                    if (CheckPlayer(i, j)) Console.Write("◎");
                                    else if (CheckBomb(i, j)) Console.Write("●");
                                    else if (CheckEnemies(i, j)) Console.Write("△");
                                    else Console.Write("　");
                                    break;
                                case Enums.FIELD_TYPE.fire:
                                    Console.Write("＊");
                                    break;
                                case Enums.FIELD_TYPE.goal:
                                    Console.Write("Ｇ");
                                    break;
                            }

                            if (i == field_width - 1)
                            {
                                Console.Write("\r\n");
                            }
                        }
                    }
                    Console.Write("<Player>\r\n");
                    Console.Write("x = " + player.x + 
                        ", y = " + player.y + 
                        ", BombNum = " + player.bombNum + 
                        ", _power = " + player._power + 
                        ", TogeBomb = "+player.togeBomb +
                        "\r\n");
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if (i == 0) Console.Write("<Enemies>\r\n");
                        Console.Write("enemies[" + i + "]: " +
                            "x = " + enemies[i].x +
                            ", y = " + enemies[i].y +
                            ", Direction = " + enemies[i].direction + "\r\n");
                    }
                    for (int i = 0; i < player.bombs.Count; i++)
                    {
                        if (i == 0) Console.Write("<Bombs>\r\n");
                        Console.Write("bomb[" + i + "]: " +
                           "x = " + player.bombs[i].x +
                           ", y = " + player.bombs[i].y + "\r\n");
                    }
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (i == 0) Console.Write("<Items>\r\n");
                        Console.Write("items[" + i + "]: " +
                           "x = " + items[i].x +
                           ", y = " + items[i].y +
                           ", Type = " + items[i].type + "\r\n");
                    }
                    //textBox_Log.Text += "Form Refreshed.\r\n";
                }
                #endregion
                //------------------------------------------------------------------------------------------------------------------------------------
            }
            refresh = false;
        }

        //キー入力イベント
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            string pressedKey = e.KeyCode.ToString();
            //textBox_Log.Text += "”" + key + "”pressed.\r\n";

            Enums.DIRECTION direction;

            int _x = player.x;
            int _y = player.y;
            bool bombFlag = false;

            //上移動
            if (pressedKey == upKey)
            {
                direction = Enums.DIRECTION.up;
                player.y--;
            }
            //下移動
            else if (pressedKey == downKey)
            {
                direction = Enums.DIRECTION.down;
                player.y++;
            }
            //左移動
            else if (pressedKey == leftKey)
            {
                direction = Enums.DIRECTION.left;
                player.x--;
            }
            //右移動
            else if (pressedKey == rightKey)
            {
                direction = Enums.DIRECTION.right;
                player.x++;
            }
            //爆弾を設置
            else if (pressedKey == bombKey)
            {
                if (player.bombs.Count < player.bombNum)
                {
                    Bomb b = new Bomb();
                    b.x = player.x;
                    b.y = player.y;
                    b.power = player._power;
                    //b.on = true;
                    b.life.Start();
                    player.bombs.Add(b);
                    bombFlag = true;

                    //textBox_Log.Text += "Bomb created. x = " + b.x + ",y = " + b.y;
                    //field[b.x, b.y] = Enums.Field_type.player_and_bomb;                        
                }
            }
           

            //壁,ブロックがあると進めない            
            switch (field[player.x, player.y])
            {
                case Enums.FIELD_TYPE.block:
                case Enums.FIELD_TYPE. wall:
                case Enums.FIELD_TYPE.bomb:
                    player.x = _x;
                    player.y = _y;
                    break;
            }
            //爆弾があると進めない
            for (int i = 0; i < player.bombs.Count; i++)
            {
                if (player.x == player.bombs[i].x && player.y == player.bombs[i].y)
                {
                    player.x = _x;
                    player.y = _y;
                }
            }

            //アイテムがあれば取得
            for(int i = 0; i < items.Count; i++)
            {
                if (player.x == items[i].x && player.y == items[i].y)
                {
                    g = CreateGraphics();
                    label_Status.Visible = true;
                    label_Status.Font = new Font("Arial", 10);
                    label_Status.BackColor = Color.Transparent;
                    label_Status.Left= Ox;
                    label_Status.Top = Oy - 20;
                   // Font f = new Font("Arial", 14);
                    //Font ff=Font.FontFamily.
                    switch (items[i].type)
                    {                        
                        case Enums.ITEM_TYPE.fireUp:
                            player._power++;
                            label_Status.Text = "Fire +1";
                            label_Status.ForeColor = Color.Red;
                            //g.DrawString("Fire +1", f, Brushes.Red, (float)200, (float)200/*(float)label7.Left , (float)label7.Top*/);
                            break;
                        case Enums.ITEM_TYPE.bombUp:
                            player.bombNum++;
                            label_Status.Text = "Bomb +1";
                            label_Status.ForeColor = Color.Blue;
                            //g.DrawString("Bomb +1", f, Brushes.Red, (float)200, (float)200/*(float)label7.Left , (float)label7.Top*/);
                            break;
                        case Enums.ITEM_TYPE.togeBomb:
                            player.togeBomb = true;
                            label_Status.Text = "TogeBomb";
                            label_Status.ForeColor = Color.Olive;
                            //g.DrawString("TogeBomb", f, Brushes.Red, (float)200, (float)200/*(float)label7.Left , (float)label7.Top*/);
                            break;                         
                       
                    }
                   
                    items.RemoveAt(i);
                   
                }
            }

            //衝突判定
            isGameOver = CheckHit(player, enemies);
            if (isGameOver)
            {
                GameOver();
            }

            //ゲームクリアneedrevise
            if (field[player.x, player.y] == Enums.FIELD_TYPE.goal
                && enemies.Count == 0)
            {
                //field[player.x, player.y] = Enums.FIELD_TYPE.player_and_goal;
                GameClear();
            }

            refresh = true;
            Refresh();
        }

        //一定時間ごとのイベント
        private void timer1_Tick(object sender, EventArgs e)
        {
            var time = format.ChangeFormatToMMSS(sw);
            label_Time.Text = time;           

            //敵の移動
            for (int i = 0; i < enemies.Count; i++)
            {
                int _x = enemies[i].x;
                int _y = enemies[i].y;

                switch (enemies[i].direction)
                {
                    case Enums.DIRECTION.up:
                        enemies[i].y--;
                        break;
                    case Enums.DIRECTION.down:
                        enemies[i].y++;
                        break;
                    case Enums.DIRECTION.left:
                        enemies[i].x--;
                        break;
                    case Enums.DIRECTION.right:
                        enemies[i].x++;
                        break;
                }

                //壁,ブロック，爆弾，別の敵があると進めない                                          
                if(field[enemies[i].x, enemies[i].y] == Enums.FIELD_TYPE.block
                    || field[enemies[i].x, enemies[i].y] == Enums.FIELD_TYPE.wall                    
                    || CheckBomb(enemies[i].x, enemies[i].y)
                    || CheckMultiEnemies(i))
                {
                    bool canMove = false;

                    //爆弾が食べれる場合
                    if(CheckBomb(enemies[i].x, enemies[i].y) 
                        && enemies[i].canEatBomb)
                    {
                        canMove = true;
                        for (int j = 0; j < player.bombs.Count; j++)
                        {
                            if (enemies[i].x == player.bombs[j].x && enemies[i].y == player.bombs[j].y)
                            {
                                player.bombs.RemoveAt(j);
                                break;
                            }
                        }
                    }         
                    //ブロックを通れる場合           
                    if(field[enemies[i].x, enemies[i].y] == Enums.FIELD_TYPE.block 
                        && enemies[i].canPassBlock)
                    {
                        canMove = true;
                    }
                    //それ以外の場合は進めない
                    if(!canMove)
                    {
                        enemies[i].x = _x;
                        enemies[i].y = _y;
                        bool decided = false;
                        bool up_Checked = false;
                        bool down_Checked = false;
                        bool left_Checked = false;
                        bool right_Checked = false;

                        //進行方向を決める                        
                        while (!decided)
                        {
                            int idx = r.Next(0, 4);
                            switch (idx)
                            {
                                //上移動を試みる
                                case 0:
                                    if (field[enemies[i].x, enemies[i].y - 1] != Enums.FIELD_TYPE.wall
                                       && field[enemies[i].x, enemies[i].y - 1] != Enums.FIELD_TYPE.block
                                       && !CheckBomb(enemies[i].x, enemies[i].y - 1)
                                       && !CheckEnemies(enemies[i].x, enemies[i].y - 1))
                                    {
                                        enemies[i].direction = Enums.DIRECTION.up;
                                        enemies[i].y--;
                                        decided = true;
                                    }
                                    up_Checked = true;
                                    break;
                                //下移動を試みる
                                case 1:
                                    if (field[enemies[i].x, enemies[i].y + 1] != Enums.FIELD_TYPE.wall
                                       && field[enemies[i].x, enemies[i].y + 1] != Enums.FIELD_TYPE.block
                                       && !CheckBomb(enemies[i].x, enemies[i].y + 1)
                                       && !CheckEnemies(enemies[i].x, enemies[i].y + 1))
                                    {
                                        enemies[i].direction = Enums.DIRECTION.down;
                                        enemies[i].y++;
                                        decided = true;
                                    }
                                    down_Checked = true;
                                    break;
                                //左移動を試みる
                                case 2:
                                    if (field[enemies[i].x - 1, enemies[i].y] != Enums.FIELD_TYPE.wall
                                       && field[enemies[i].x - 1, enemies[i].y] != Enums.FIELD_TYPE.block
                                       && !CheckBomb(enemies[i].x - 1, enemies[i].y)
                                       && !CheckEnemies(enemies[i].x - 1, enemies[i].y))
                                    {
                                        enemies[i].direction = Enums.DIRECTION.left;
                                        enemies[i].x--;
                                        decided = true;
                                    }
                                    left_Checked = true;
                                    break;
                                //右移動を試みる
                                case 3:
                                    if (field[enemies[i].x + 1, enemies[i].y] != Enums.FIELD_TYPE.wall
                                       && field[enemies[i].x + 1, enemies[i].y] != Enums.FIELD_TYPE.block
                                       && !CheckBomb(enemies[i].x + 1, enemies[i].y)
                                       && !CheckEnemies(enemies[i].x + 1, enemies[i].y))
                                    {
                                        enemies[i].direction = Enums.DIRECTION.right;
                                        enemies[i].x++;
                                        decided = true;
                                    }
                                    right_Checked = true;
                                    break;
                            }

                            //どの方向にも進めない場合、whileを強制的に抜ける
                            if (up_Checked && down_Checked && left_Checked && right_Checked)
                            {
                                break;
                            }
                        }
                    }

                                  
                }

                //交差点で方向を変える
                if (enemies[i].canChangeDirectionAtCross
                    && enemies[i].x % 2 == 1
                    && enemies[i].y % 2 == 1)
                {
                    switch (enemies[i].direction)
                    {
                        case Enums.DIRECTION.up:
                            switch (r.Next(0, 3))
                            {
                                case 0: enemies[i].direction = enemies[i].direction = Enums.DIRECTION.up; break;
                                case 1: enemies[i].direction = enemies[i].direction = Enums.DIRECTION.left; break;
                                case 2: enemies[i].direction = enemies[i].direction = Enums.DIRECTION.right; break;
                            }
                            break;
                        case Enums.DIRECTION.down:
                            switch (r.Next(0, 3))
                            {
                                case 0: enemies[i].direction = enemies[i].direction = Enums.DIRECTION.down; break;
                                case 1: enemies[i].direction = enemies[i].direction = Enums.DIRECTION.left; break;
                                case 2: enemies[i].direction = enemies[i].direction = Enums.DIRECTION.right; break;
                            }
                            break;
                        case Enums.DIRECTION.left:
                            switch (r.Next(0, 3))
                            {
                                case 0: enemies[i].direction = enemies[i].direction = Enums.DIRECTION.up; break;
                                case 1: enemies[i].direction = enemies[i].direction = Enums.DIRECTION.down; break;
                                case 2: enemies[i].direction = enemies[i].direction = Enums.DIRECTION.left; break;
                            }
                            break;
                        case Enums.DIRECTION.right:
                            switch (r.Next(0, 3))
                            {
                                case 0: enemies[i].direction = enemies[i].direction = Enums.DIRECTION.up; break;
                                case 1: enemies[i].direction = enemies[i].direction = Enums.DIRECTION.down; break;
                                case 2: enemies[i].direction = enemies[i].direction = Enums.DIRECTION.right; break;
                            }
                            break;
                    }
                }
            }

            //敵との接触判定
            isGameOver = CheckHit(player, enemies);            

            //爆発による火を消す
            for (int i = 0; i < firePosition.Count; i++)
            {
                field[firePosition[i].X, firePosition[i].Y] = Enums.FIELD_TYPE.path;
            }
            firePosition.Clear();
            //refresh = true;
            //Refresh();

            //爆発
            for(int b = 0; b < player.bombs.Count &&!isGameOver; b++)
            {
                if (player.bombs[b].life.Elapsed > player.bombs[b].limit)
                {
                    field[player.bombs[b].x, player.bombs[b].y] = Enums.FIELD_TYPE.fire;

                    bool fireLimit_Up = false;
                    bool fireLimit_Down = false;
                    bool fireLimit_Left = false;
                    bool fireLimit_Right = false;

                    //ブロックを壊す&自分が当たるとゲームオーバー
                    for (int i = 1; i <= player.bombs[b].power; i++)
                    {
                        //上
                        if (player.bombs[b].y - i >= 0 && !fireLimit_Up)
                        {
                            FireMethod(ref fireLimit_Up, player.bombs[b].x, player.bombs[b].y - i);
                        }
                        //下
                        if (player.bombs[b].y + i < field_height && !fireLimit_Down)
                        {
                            FireMethod(ref fireLimit_Down, player.bombs[b].x, player.bombs[b].y + i);
                        }
                        //左
                        if (player.bombs[b].x - i >= 0 && !fireLimit_Left)
                        {
                            FireMethod(ref fireLimit_Left, player.bombs[b].x - i, player.bombs[b].y);
                        }
                        //右
                        if (player.bombs[b].x + i < field_width && !fireLimit_Right)
                        {
                            FireMethod(ref fireLimit_Right, player.bombs[b].x + i, player.bombs[b].y );
                        }
                        firePosition.Add(new Point(player.bombs[b].x, player.bombs[b].y));
                    }
                    //爆弾と同じマスでもアウト
                    if ((player.bombs[b].x == player.x) && (player.bombs[b].y == player.y))
                    {
                        isGameOver = true;
                    }

                    player.bombs.RemoveAt(b);
                    b--;
                }
            }

            refresh = true;
            Refresh();
            if (isGameOver)
            {
                GameOver();
            }
        }

        //ハイスコア
        private void button_HighScore_Click(object sender, EventArgs e)
        {                        
            HighScore.ReadHighScore(10);           

            Form_HighScore f = new Form_HighScore();
            f.Show();
        }

        //関数の定義
        //------------------------------------------------------------------------------------------------------------------------------------
        #region 関数
        /// <summary>
        /// ゲームオーバー
        /// </summary>
        public void GameOver()
        {
            player.isAlive = false;
            refresh = true;
            Refresh();
            timer1.Stop();
            MessageBox.Show("GAME OVER!!");
        }

        /// <summary>
        /// ゲームクリア
        /// </summary>
        public void GameClear()
        {
            refresh = true;
            Refresh();
            timer1.Stop();
            MessageBox.Show("GAME CLEARED!!");
        }

        /// <summary>
        /// プレーヤーと敵の衝突判定
        /// </summary>
        /// <param name="player"></param>
        /// <param name="enemies"></param>
        /// <returns></returns>
        public bool CheckHit(Player player,List<Enemy>enemies)
        {            
            for (int i = 0; i < enemies.Count; i++)
            {
                if (player.x == enemies[i].x && player.y == enemies[i].y)
                {
                    return true;                    
                }
            }
            return false;
        }

        /// <summary>
        /// 爆発域の処理
        /// </summary>
        /// <param name="fireLimit"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        public void FireMethod(ref bool fireLimit,int X, int Y)
        {
            if ((field[X, Y] == Enums.FIELD_TYPE.block
                || field[X, Y] == Enums.FIELD_TYPE.path))
            {
                //同じ方向で2つ以上のブロックは壊せない
                if (field[X, Y] == Enums.FIELD_TYPE.block
                    &&!player.togeBomb)
                {
                    fireLimit = true;
                }

                field[X, Y] = Enums.FIELD_TYPE.fire;
                Point p = new Point(X, Y);
                firePosition.Add(p);
            }
            else if (field[X, Y] == Enums.FIELD_TYPE.wall)
            {
                fireLimit = true;
            }

            //敵が爆発を喰らった場合
            for (int j = 0; j < enemies.Count; j++)
            {
                if (X == enemies[j].x && Y == enemies[j].y)
                {
                    field[enemies[j].x, enemies[j].y] = Enums.FIELD_TYPE.fire;
                    firePosition.Add(new Point(enemies[j].x, enemies[j].y));
                    enemies.RemoveAt(j);
                }
            }
            //プレーヤーが爆発を喰らった場合
            if ((X == player.x) && (Y == player.y))
            {
                isGameOver = true;
                //break;
            }
        }

        /// <summary>
        /// 一つのマスに複数の敵が重なるかどうか判定
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public bool CheckMultiEnemies(int i)
        {
            bool multi = false;
            for (int j = 0; j < enemies.Count; j++)
            {
                if (i!=j
                    && enemies[i].x == enemies[j].x && enemies[i].y == enemies[j].y)
                {
                    multi = true;
                }
            }
            return multi;
        }

        /// <summary>
        /// 対象のマスに爆弾があるかどうか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool CheckBomb(int x, int y)
        {
            bool bombExists = false;
            for(int i = 0; i < player.bombs.Count; i++)
            {
                if (x == player.bombs[i].x && y == player.bombs[i].y)
                {
                    bombExists = true;
                }
            }
            return bombExists;
        }

        /// <summary>
        /// 対象のマスにプレーヤーがいるかどうか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool CheckPlayer(int x, int y)
        {
            bool playerExists = false;
            if (x == player.x && y == player.y) playerExists = true;
            return playerExists;
        }

        /// <summary>
        /// 対象のマスに敵がいるかどうか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool CheckEnemies(int x, int y)
        {
            bool enemyExists = false;
            for (int i = 0; i < enemies.Count; i++)
            {
                if (x == enemies[i].x && y == enemies[i].y)
                {
                    enemyExists = true;
                }
            }
            return enemyExists;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        #endregion
        //------------------------------------------------------------------------------------------------------------------------------------
    }
}
