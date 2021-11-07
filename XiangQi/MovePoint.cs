using System;
using System.Collections.Generic;
using System.Text;

namespace XiangQi
{
    class MovePoint
    {
        public int vertical;
        public int horizontal;
        public Player_side side;
        public Boolean CanReach;
        public MovePoint(int horizontal, int vertical,Player_side side)
        {
            this.vertical = vertical;
            this.horizontal = horizontal;
            this.side = side;
            CanReach = false;
        }
        public int getvertical()
        {
            return vertical;
        }
        public int gethorizontal()
        {
            return horizontal;
        }
    }
}
