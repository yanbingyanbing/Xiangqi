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
        
    }

    class Advisor : Piece{   //仕

    }

    class Horse : Piece{   //马

    }

    class Cannon : Piece{   //炮

    }

    class Elephant : Piece{   //象

    }

    class Rook : Piece{   //车

    }

    class Soldier : Piece{   //兵

    }
}