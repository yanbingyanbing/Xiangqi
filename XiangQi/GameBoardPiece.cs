using System;
using System.Collections.Generic;
using System.Text;

namespace XiangQi
{
    public enum Piece_type
    {
        blank,
        General,
        Advisor,
        Elephant,
        Horse,
        Rook,
        Cannon,
        Soldier
    }
    public enum Player_side
    {
        blank,
        black,
        red
    }
    public class Board
    {
        public void h()
        {
            Piece[,] board = new Piece[10, 9];
            GameBoard b = new GameBoard();
            board = b.Return_pieces();
        }
    }

    public class Piece
    {
       public Player_side side;
       public char letter_horizontal;
       public int vertical;
       public Piece_type type;
        public void Setletter_horizontal(int number_horizontal)
        {
            this.letter_horizontal = NumberToletter_switch(number_horizontal);
        }
        public void Setvertical(int vertical)
        {
            this.vertical = vertical;
        }
        public int LetterTonumber_swtich(char horizontal)
        {
            int h = 0;
            switch (horizontal)
            {
                case 'a': h = 0; break;
                case 'b': h = 1; break;
                case 'c': h = 2; break;
                case 'd': h = 3; break;
                case 'e': h = 4; break;
                case 'f': h = 5; break;
                case 'g': h = 6; break;
                case 'h': h = 7; break;
                case 'i': h = 8; break;
                default:
                    break;
            }
            return h;
        }
        public char NumberToletter_switch(int number)
        {
            char letter='a';
            switch (number)
            {
                case 0: letter = 'a';break;
                case 1: letter = 'b';break;
                case 2: letter = 'c';break;
                case 3: letter = 'd';break;
                case 4: letter = 'e';break;
                case 5: letter = 'f';break;
                case 6: letter = 'g';break;
                case 7: letter = 'h';break;
                case 8: letter = 'i';break;
                default:break;
            }
            return letter;
        }
        public Piece(char letter_horizontal, int vertical)
        {
            this.letter_horizontal = letter_horizontal;
            this.vertical = vertical;

        }
       
        public override string ToString()
        {
            return base.ToString();
        }
    }
    class General : Piece
    {
        public GameBoard theboard = new GameBoard();
        public int number_horizontal;
        public General(char letter_horizontal, int vertical, Player_side side)
        : base(letter_horizontal, vertical)
        {
            number_horizontal = LetterTonumber_swtich(letter_horizontal);
            this.type = Piece_type.General;
            this.side = side;
        }
        public void Setnumber_horizontal(int number_horizontal)
        {
            this.number_horizontal = number_horizontal;
        }
        public void Setvertical(int vertical)
        {
            this.vertical = vertical;
        }
    }
    class Advisor : Piece
    {
        public GameBoard theboard = new GameBoard();
        public int number_horizontal;
        public Advisor(char letter_horizontal, int vertical,Player_side side)
            : base(letter_horizontal, vertical)
        {
            number_horizontal = LetterTonumber_swtich(letter_horizontal);
            this.type = Piece_type.Advisor;
            this.side = side;
        }
        public void Setnumber_horizontal(int number_horizontal)
        {
            this.number_horizontal = number_horizontal;
        }
        public void Setvertical(int vertical)
        {
            this.vertical = vertical;
        }
        public MovePoint[] AdvisorMove = new MovePoint[4];
        public MovePoint[] CanMoveTo(int number_horizontal, int vertical,Player_side side)//显示这颗棋子可以去的地方
        {
            MovePoint Left_Botton = new MovePoint(number_horizontal - 1, vertical + 1,side);
            MovePoint Right_Botton = new MovePoint(number_horizontal + 1, vertical + 1,side);
            MovePoint Left_Top = new MovePoint(number_horizontal - 1, vertical - 1,side);
            MovePoint Right_Top = new MovePoint(number_horizontal + 1, vertical - 1,side);
            //仕只可以走周围四格所以这里有四个
            AdvisorMove[0] = Left_Botton;
            AdvisorMove[1] = Right_Botton;
            AdvisorMove[2] = Left_Top;
            AdvisorMove[3] = Right_Top;
            return AdvisorMove;
        }
        public void ValidMovement(MovePoint[] movePoints)//最终判定可以去的地方
        {
            for (int i = 0; i < movePoints.Length; i++)
            {
                int[] num = new int[2];
                num[0] = movePoints[i].vertical;
                num[1] = movePoints[i].horizontal;
                if (movePoints[i].horizontal >= 3 && movePoints[i].horizontal <= 5)//判定是否在红&黑城内- 横坐标
                {
                    if (movePoints[i].side == Player_side.red)//红棋子
                    {
                        if (movePoints[i].vertical >= 7 && movePoints[i].vertical <= 9)//判定红色方要走的地方是否在红色方城内- 纵坐标
                        {
                            movePoints[i].CanReach = true;//可以去的格子就给个true
                        }
                    }
                    else if (movePoints[i].side == Player_side.black)//黑棋子
                    {
                        if (movePoints[i].vertical >= 0 && movePoints[i].vertical <= 3)//判定黑色方要走的地方是否在黑色方城内- 纵坐标
                        {
                            /*if (theboard(num[0],num[1]).tpye == Piece_type.blank)//
                            {

                            }*/
                                movePoints[i].CanReach = true;//可以去的格子就给个true
                        }
                    }
                }
            }
        }
        public MovePoint[] PlaceCango()//得到最终的FirstTry数组，里面可以去的格子已经变成了true
        {
            return AdvisorMove;
        }
        public void Give(MovePoint[] movePoints)//把能去的格子坐标打出来，
        {   
            for(int i = 0; i < movePoints.Length; i++) 
            {
                if (movePoints[i].CanReach == true) 
                {
                    Console.Write(NumberToletter_switch(movePoints[i].horizontal));//返回char横坐标
                    Console.Write(",");
                    Console.WriteLine(movePoints[i].vertical);//返回int竖坐标
                }
                    

            }
            
        }

    }
    class Horse : Piece 
    {
        public int number_horizontal;
        public Horse(char letter_horizontal, int vertical, Player_side side)
        : base(letter_horizontal, vertical)
        {
            number_horizontal = LetterTonumber_swtich(letter_horizontal);
            this.type = Piece_type.Horse;
            this.side = side;
        }
        public void Setnumber_horizontal(int number_horizontal)
        {
            this.number_horizontal = number_horizontal;
        }
        public void Setvertical(int vertical)
        {
            this.vertical = vertical;
        }
    }
    class Cannon : Piece
    {
        public int number_horizontal;
        public Cannon(char letter_horizontal, int vertical,Player_side side)
        : base(letter_horizontal, vertical) 
        {
            number_horizontal = LetterTonumber_swtich(letter_horizontal);
            this.type = Piece_type.Cannon;
            this.side = side;
        }
        public void Setnumber_horizontal(int number_horizontal)
        {
            this.number_horizontal = number_horizontal;
        }
        public void Setvertical(int vertical)
        {
            this.vertical = vertical;
        }
        MovePoint[] ConnonMove = new MovePoint[17];
        public MovePoint[] CanMoveTo(int number_horizontal,int vertical,Player_side side)
        {
            for(int i = 0; i < 9; i++)//水平位置可以去的格子
            {
                if (i < number_horizontal)
                {
                    ConnonMove[i] = new MovePoint(i, vertical, side);
                }
                if (i == number_horizontal) { }
                if (i > number_horizontal)
                {
                    ConnonMove[i - 1] = new MovePoint(i, vertical, side);
                }
            }
            for (int i = 0; i < 10; i++)//纵坐标能去的格子
            {
                
                if (i < vertical)
                {
                    ConnonMove[i+8] = new MovePoint(number_horizontal, i, side);
                }
                if (i == vertical) { }
                if (i > vertical)
                {
                    ConnonMove[i+8] = new MovePoint(number_horizontal, i, side);
                }
            }
            return ConnonMove;
        }
    }
    class Elephant : Piece
    {
        public int number_horizontal;
        public Elephant(char letter_horizontal, int vertical, Player_side side)
            : base(letter_horizontal, vertical)
        {
            number_horizontal = LetterTonumber_swtich(letter_horizontal);       //方法的属性
            this.type = Piece_type.Elephant;
            this.side = side;
        }
        public void Setnumber_horizontal(int number_horizontal)
        {
            this.number_horizontal = number_horizontal;
        }
        public int Getnumber_horizontal()
        {
            return number_horizontal;
        }
        MovePoint[] ElephantTry = new MovePoint[4];
        MovePoint[] Valid_ElephantTry = new MovePoint[4];
        public MovePoint[] CanMoveTo(int number_horizontal, int vertical, Player_side side)//显示这颗棋子可以去的地方
        {
            MovePoint Left_Botton = new MovePoint(number_horizontal - 2, vertical + 2, side);
            MovePoint Right_Botton = new MovePoint(number_horizontal + 2, vertical + 2, side);
            MovePoint Left_Top = new MovePoint(number_horizontal - 2, vertical - 2, side);
            MovePoint Right_Top = new MovePoint(number_horizontal + 2, vertical - 2, side);
            //象只可以走周围四格所以这里有四个
            ElephantTry[0] = Left_Botton;
            ElephantTry[1] = Right_Botton;
            ElephantTry[2] = Left_Top;
            ElephantTry[3] = Right_Top;
            return ElephantTry;
        }
        public MovePoint[] Elephant_validMove(int number_horizontal, int vertical, Player_side side)//显示这颗棋子可以去的地方
        {
            MovePoint Left_Botton = new MovePoint(number_horizontal - 1, vertical + 1, side);
            MovePoint Right_Botton = new MovePoint(number_horizontal + 1, vertical + 1, side);
            MovePoint Left_Top = new MovePoint(number_horizontal - 1, vertical - 1, side);
            MovePoint Right_Top = new MovePoint(number_horizontal + 1, vertical - 1, side);
            //象只可以走周围四格所以这里有四个象脚
            Valid_ElephantTry[0] = Left_Botton;
            Valid_ElephantTry[1] = Right_Botton;
            Valid_ElephantTry[2] = Left_Top;
            Valid_ElephantTry[3] = Right_Top;
            return Valid_ElephantTry;
        }
        public void ValidMovement(MovePoint[] movePoints)//最终判定可以去的地方
        {
            for (int i = 0; i < movePoints.Length; i++)
            {
                if (movePoints[i].vertical >= 5 && movePoints[i].vertical <= 9)//判定是否过河,红色在下
                {
                    if (movePoints[i].side == Player_side.red)
                    {
                        if (movePoints[i].vertical >= 7 && movePoints[i].vertical <= 9)//判定红色方是否别象脚
                        {
                            movePoints[i].CanReach = true;//可以去的格子就给个true
                        }
                    }
                }
                if (movePoints[i].vertical >= 0 && movePoints[i].vertical <= 4)//判定是否过河,黑色在上
                {
                    if (movePoints[i].side == Player_side.black)
                    {
                        if (movePoints[i].vertical >= 0 && movePoints[i].vertical <= 3)//判定黑色方是否别象脚
                        {
                            movePoints[i].CanReach = true;//可以去的格子就给个true
                        }
                    }
                }
            }
        }
        public MovePoint[] PlaceCango()//得到最终的FirstTry数组，里面可以去的格子已经变成了true
        {
            return ElephantTry;
        }
        public void Give(MovePoint[] movePoints)//把能去的格子坐标打出来，
        {
            for (int i = 0; i < movePoints.Length; i++)
            {
                if (movePoints[i].CanReach == true)
                {
                    Console.Write(NumberToletter_switch(movePoints[i].horizontal));//返回char横坐标
                    Console.Write(",");
                    Console.WriteLine(movePoints[i].vertical);//返回int竖坐标
                }
            }
        }
    }
    class Rook : Piece
    {
        public int number_horizontal;
        public Rook(char letter_horizontal, int vertical, Player_side side)
        : base(letter_horizontal, vertical)
        {
            number_horizontal = LetterTonumber_swtich(letter_horizontal);
            this.type = Piece_type.Rook;
            this.side = side;
        }
        public void Setnumber_horizontal(int number_horizontal)
        {
            this.number_horizontal = number_horizontal;
        }
        public void Setvertical(int vertical)
        {
            this.vertical = vertical;
        }
        public MovePoint[] RookMove = new MovePoint[17];
        public MovePoint[] CanMoveTo(int number_horizontal, int vertical, Player_side side)
        {
            for (int i = 0; i < 9; i++)//水平位置可以去的格子
            {
                if (i < number_horizontal)
                {
                    RookMove[i] = new MovePoint(i, vertical, side);
                }
                if (i == number_horizontal) { }
                if (i > number_horizontal)
                {
                    RookMove[i - 1] = new MovePoint(i, vertical, side);
                }
            }
            for (int i = 0; i < 10; i++)//纵坐标能去的格子
            {
                if (i < vertical)
                {
                    RookMove[i + 8] = new MovePoint(number_horizontal, i, side);
                }
                if (i == vertical) { }
                if (i > vertical)
                {
                    RookMove[i + 8] = new MovePoint(number_horizontal, i, side);
                }
            }
            return RookMove;
        }
        public void ValidMovement(MovePoint[] movePoints)//最终判定可以去的地方 
        {

        }
        public MovePoint[] PlaceCango()//得到最终的FirstTry数组，里面可以去的格子已经变成了true
        {
            return RookMove;
        }
        public void Give(MovePoint[] movePoints)//把能去的格子坐标打出来，
        {
            for (int i = 0; i < movePoints.Length; i++)
            {
                if (movePoints[i].CanReach == true)
                {
                    Console.Write(NumberToletter_switch(movePoints[i].horizontal));//返回char横坐标
                    Console.Write(",");
                    Console.WriteLine(movePoints[i].vertical);//返回int竖坐标
                }
            }
        }
    }
    class Soldier : Piece
    {
        public int number_horizontal;
        public Soldier(char letter_horizontal, int vertical, Player_side side)
            : base(letter_horizontal, vertical)
        {
            number_horizontal = LetterTonumber_swtich(letter_horizontal);
            this.type = Piece_type.Soldier;
            this.side = side;
        }
        public void setnumber_horizontal(int number_horizontal)
        {
            this.number_horizontal = number_horizontal;
        }
        public int getnumber_horizontal()
        {
            return number_horizontal;
        }
        public MovePoint[] SoldierMove = new MovePoint[4];
        public MovePoint[] CanMoveTo(int number_horizontal, int vertical, Player_side side)//显示这颗棋子可以去的地方
        {
            MovePoint Go_Ahead = new MovePoint(number_horizontal + 0, vertical + 1, side);
            MovePoint Turn_Left = new MovePoint(number_horizontal - 1, vertical + 0, side);
            MovePoint Turn_Right = new MovePoint(number_horizontal + 1, vertical + 0, side);
            MovePoint Go_Ahead_2 = new MovePoint(number_horizontal + 0, vertical - 1, side);
            SoldierMove[0] = Go_Ahead;
            SoldierMove[1] = Turn_Left;
            SoldierMove[2] = Turn_Right;
            SoldierMove[3] = Go_Ahead_2;
            return SoldierMove;
        }
        public void ValidMovement(MovePoint[] movePoints)
        {
            for (int i = 0; i < 4; i++)
            {
                if (movePoints[i].vertical >= 5 && movePoints[i].vertical <= 9)//红区
                {
                    if (movePoints[i].side == Player_side.red)//红棋子
                    {
                        if (movePoints[i].horizontal >= 0 && movePoints[i].horizontal <= 8)
                        {
                            movePoints[0].CanReach = false;
                            movePoints[1].CanReach = false;
                            movePoints[2].CanReach = false;
                            movePoints[3].CanReach = true;
                        }
                    }
                    else if (movePoints[i].side == Player_side.black)//黑棋子
                    {
                        movePoints[0].CanReach = true;
                        movePoints[1].CanReach = true;
                        movePoints[2].CanReach = true;
                        movePoints[3].CanReach = false;
                    }
                }
                else if (movePoints[i].horizontal >= 0 && movePoints[i].horizontal <= 4)//黑区
                {
                    if (movePoints[i].side == Player_side.red)//红棋子
                    {
                        movePoints[0].CanReach = false;
                        movePoints[1].CanReach = true;
                        movePoints[2].CanReach = true;
                        movePoints[3].CanReach = true;
                    }
                    else if (movePoints[i].side == Player_side.black)//黑棋子
                    {
                        if (movePoints[i].vertical >= 0 && movePoints[i].vertical <= 8)
                        {
                            movePoints[0].CanReach = true;
                            movePoints[1].CanReach = false;
                            movePoints[2].CanReach = false;
                            movePoints[3].CanReach = false;
                        }
                    }
                }
            }
        }
        public MovePoint[] PlaceCango()//得到最终的FirstTry数组，里面可以去的格子已经变成了true
        {
            return SoldierMove;
        }
        public void Give(MovePoint[] movePoints)//把能去的格子坐标打出来，
        {
            for (int i = 0; i < movePoints.Length; i++)
            {
                if (movePoints[i].CanReach == true)
                {
                    Console.Write(NumberToletter_switch(movePoints[i].horizontal));//返回char横坐标
                    Console.Write(",");
                    Console.WriteLine(movePoints[i].vertical);//返回int竖坐标
                }
            }
        }
    }

}
   
 
    


