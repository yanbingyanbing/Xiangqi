using System;
using System.Collections.Generic;
using System.Text;

namespace Xiangqi{
    class GameBoard{

        public int Letter_To_Number_swtich(char horizontal){
            int h = 0;
            switch(horizontal){
                case 'a': h = 0; break;
                case 'b': h = 1; break;
                case 'c': h = 2; break;
                case 'd': h = 3; break;
                case 'e': h = 4; break;
                case 'f': h = 5; break;
                case 'g': h = 6; break;
                case 'h': h = 7; break;
                case 'i': h = 8; break;
                default: break;
            }
            return h;
        }

         /*public char Number_To_Letter_swtich(int number){
            Char letter = 'a';
            switch(number){
                case 0: letter = 'a'; break;
                case 1: letter = 'b'; break;
                case 2: letter = 'c'; break;
                case 3: letter = 'd'; break;
                case 4: letter = 'e'; break;
                case 5: letter = 'f'; break;
                case 6: letter = 'g'; break;
                case 7: letter = 'h'; break;
                case 8: letter = 'i'; break;
                default: break;
            }
            return h;
        }*/

        //给棋盘所有棋子赋值
        public void GiveThePiece()
        {
            for ( int i = 0; i < theboard.GetLength(0); i++)
            {
                for ( int j = 0; j < theboard.GetLength(1); j++)
                {
                    theboard[i,j] = new Piece( i,Letter_To_Number_swtich(j));//棋盘的(x，y)
                    theboard[i, j].type = Piece_type.blank;                //  棋盘的属性
                    theboard[i, j].side = Player_side.blank;
                }
            }
            //棋盘上9a这个点对应页面的（9,0）这个点 
            theboard[9, 0] = new Rook( 9,'a', Player_side.red);             //每个棋子的起始位置
            theboard[9, 1] = new Horse(9,'b',  Player_side.red);
            theboard[9, 2] = new Elephant(9,'c',  Player_side.red);
            theboard[9, 3] = new Advisor(9,'d',  Player_side.red);
            theboard[9, 4] = new General(9,'e',  Player_side.red);
            theboard[9, 5] = new Advisor(9,'f',  Player_side.red);
            theboard[9, 6] = new Elephant(9,'g',  Player_side.red);
            theboard[9, 7] = new Horse(9,'h',  Player_side.red);
            theboard[9, 8] = new Rook(9,'i',  Player_side.red);
            theboard[7, 1] = new Cannon(7,'b',  Player_side.red);
            theboard[7, 7] = new Cannon(7,'h',  Player_side.red);
            theboard[6, 0] = new Soldier(6,'a',  Player_side.red);
            theboard[6, 2] = new Soldier(6,'c',  Player_side.red);
            theboard[6, 4] = new Soldier(6,'e',  Player_side.red);
            theboard[6, 6] = new Soldier(6,'g',  Player_side.red);
            theboard[6, 8] = new Soldier(6,'i',  Player_side.red);

            theboard[0, 0] = new Rook(0,'a',  Player_side.black);
            theboard[0, 1] = new Horse(0,'b',  Player_side.black);
            theboard[0, 2] = new Elephant(0,'c',  Player_side.black);
            theboard[0, 3] = new Advisor(0,'d',  Player_side.black);
            theboard[0, 4] = new General(0,'e',  Player_side.black);
            theboard[0, 5] = new Advisor(0,'f',  Player_side.black);
            theboard[0, 6] = new Elephant(0,'g',  Player_side.black);
            theboard[0, 7] = new Horse(0,'h',  Player_side.black);
            theboard[0, 8] = new Rook(0,'i',  Player_side.black);
            theboard[2, 1] = new Cannon( 2,'b', Player_side.black) ;
            theboard[2, 7] = new Cannon(2,'h',  Player_side.black);
            theboard[3, 0] = new Soldier(3,'a',  Player_side.black);
            theboard[3, 2] = new Soldier(3,'c',  Player_side.black);
            theboard[3, 4] = new Soldier(3,'e',  Player_side.black);
            theboard[3, 6] = new Soldier(3,'g',  Player_side.black);
            theboard[3, 8] = new Soldier(3,'i',  Player_side.black);
        }

   

           
        
    }
}