using System;

namespace XiangQi
{
    class Program
    {
        static void Main(string[] args)
        {
            GameBoard board = new GameBoard();
            GameDisplay play = new GameDisplay();
            board.GiveThePiece();
            board.DisplayBorad();

            board.CalculateValidMoves(board.SelectPiece(board.AskSelectPiece()));
        }
    }
}

