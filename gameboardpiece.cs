using System;

namespace XiangQi{
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

    class General : Piece{   //将
        public GameBoard theboard = new GameBoard();
    }

    class Advisor : Piece{   //仕
        public GameBoard theboard = new GameBoard();
    }

    class Horse : Piece{   //马
        public GameBoard theboard = new GameBoard();
    }

    class Cannon : Piece{   //炮
        public GameBoard theboard = new GameBoard();
    }

    class Elephant : Piece{   //象
        public GameBoard theboard = new GameBoard();
    }

    class Rook : Piece{   //车
        public GameBoard theboard = new GameBoard();
    }

    class Soldier : Piece{   //兵
        public GameBoard theboard = new GameBoard();
    }
}