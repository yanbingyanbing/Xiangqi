using System;
using System.Collections.Generic;
using System.Text;

namespace Xiangqi{
    //棋子类型
    public enum Piece_type{
        blank,
        General,
        Advisor,
        Elephant,
        Horse,
        Rook,
        Cannon,
        Soldier
    }

    //玩家类型
    public enum Player_side{
        blank,
        black,
        red
    }

    public class Board{
        Piece[,] board = new Piece[10, 9];
        GameBoard b = new GameBoard();
       
    }

    class General {   //将
        public GameBoard theboard = new GameBoard();
    }

    class Advisor {   //仕
        public GameBoard theboard = new GameBoard();
    }

    class Horse {   //马
        public GameBoard theboard = new GameBoard();
    }

    class Cannon {   //炮
        public GameBoard theboard = new GameBoard();
    }

    class Elephant {   //象
        public GameBoard theboard = new GameBoard();
    }

    class Rook {   //车
        public GameBoard theboard = new GameBoard();
    }

    class Soldier {   //兵
        public GameBoard theboard = new GameBoard();
    }
}