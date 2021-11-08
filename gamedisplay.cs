using System;
using System.Collections.Generic;
using System.Text;

namespace Xiangqi
{
    class GameDisplay{

        //画棋子
        public void DisplayBorad()
        {
            Piece[,] P = new Piece[10, 9];
            GameBoard board = new GameBoard();
            P=board.Return_pieces();
            for (int i = 0; i < theboard.GetLength(0); i++)
            {
                for (int j = 0; j < theboard.GetLength(1); j++)
                {
                    if (j == 0)//横坐标的左边界
                    {
                        Console.Write(i);
                        
                    }
                     switch (theboard[i,j].type)
                     {
                         case Piece_type.blank: Console.Write("十");break;
                         case Piece_type.Rook: Console.Write("车");break;
                         case Piece_type.Horse: Console.Write("马");break;
                         case Piece_type.Elephant: Console.Write("象");break;
                         case Piece_type.Advisor:  Console.Write("士"); break;
                         case Piece_type.General: Console.Write("帅");break;
                         case Piece_type.Cannon: Console.Write("炮");break;
                         case Piece_type.Soldier: Console.Write("兵");break;
                         default:break;
                     }
                    if (j == 8)//横坐标的右边界
                    {
                        Console.Write('\n');
                    }
                }
            }
        }
        
        //下棋落棋子
        public void AskSelectPiece()
        {
            Console.WriteLine("Which piece do you want to move?(eg:8d)");
            string inputa = Console.ReadLine();
           
            char h;
            int v = 0;
            foreach (char a in inputa)
            {
                if (a >= 'a' && a <= 'i')
                {
                    h = a;
                }

                else if (a >= '0' && a <= '9')
                {
                    v = v + a;
                }

                else
                {
                    Console.WriteLine("ERROR!Please input an existing location");
                }
            }

    }
}