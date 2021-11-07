using System;
using System.Collections.Generic;
using System.Text;

namespace XiangQi
{
    class GameDisplay
    {
        public void DisplayBorad()
        {
            Piece[,] P = new Piece[10, 9];
            GameBoard board = new GameBoard();
            P=board.Return_pieces();
            for (int i = 0; i < P.GetLength(0); i++)
            {
                for (int j = 0; j < P.GetLength(1); j++)
                {
                    if (j == 0)
                    {
                        Console.WriteLine(i);
                    }
                   /* switch (board.Return_pieces().Piece_type)
                    {
                        case Piece_type.blank:Console.Write("十");break;
                        case Piece_type.Rook:Console.Write("车");break;
                        case Piece_type.Horse:Console.Write("马");break;
                        case Piece_type.Elephant:Console.Write("象");break;
                        case Piece_type.Advisor: Console.Write("士"); break;
                        case Piece_type.General:Console.Write("帅");break;
                        case Piece_type.Cannon:Console.Write("炮");break;
                        case Piece_type.Soldier:Console.Write("兵");break;
                        default:break;
                    }*/
                    if (j == 8)
                    {
                        Console.Write('\n');
                    }
                }
            }
        }
        public void PieceJudge(Piece piece)
        {
            switch (piece.type)
            {

            }
        }
        public void AskSelectPiece()
        {
            Console.WriteLine("Which piece do you want to move?");

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
        public void AskMovePiece()
        {

        }
    }
}

