using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Xiangqi{
    class Program{
        static void Main(string[] args){
            //棋子枚举类型
            public Piece{
                无子 = 0,
                红车 = 1, 红马 = 2, 红相 = 3, 红士 = 4, 红帅 = 5, 红炮 = 6, 红卒 = 7,
                蓝车 = 8, 蓝马 = 9, 蓝象 = 10, 蓝士 = 11, 蓝将 = 12, 蓝炮 = 13, 蓝兵 = 14
            }
            //玩家类型
            public enum Player{
                无 = 0, 红 = 1, 蓝 = 2
            }
            //操作类型
            public enum Operation{
                拾子, 落子
            }
            //操作步骤
            public class Step{
                public Player _player;
                public Piece _pickChess;
                public int _pickRow;
                public int _pickCol;
                public Piece _dropChess;
                public int _dropRow;
                public int _dropCol;
            }
            //构造方法
        public Chess()
        {
            //调用象棋初始化方法
            Initialize();
        }

        //初始化方法
        public void Initialize()
        {
            //初始化当前的走棋方
            _curPlayer = Player.无;
            //初始化当前的走棋操作
            _curOperation = Operation.拾子;

            //保存拾起的棋子
            _pickChess = Piece.无子;
            //保存拾起棋子的行号和列号
            _pickRow = 0;
            _pickCol = 0;

            //清空list表中的走棋步骤
            _stepList.Clear();

            //把棋盘上棋子值都设为无子
            for (int i = 1; i <= 10; i++)
                for (int j = 1; j <= 9; j++)
                {
                    _chess[i, j] = Piece.无子;
                }
        }

        //类方法，象棋开局
        public void Begin(Player firstPlayer)
        {
            //象棋初始化
            Initialize();

            //初始化当前走棋方
            _curPlayer = firstPlayer;

            //初始化蓝方棋子
            _chess[1, 1] = Piece.蓝车; _chess[1, 2] = Piece.蓝马; _chess[1, 3] = Piece.蓝象;
            _chess[1, 4] = Piece.蓝士; _chess[1, 5] = Piece.蓝将; _chess[1, 6] = Piece.蓝士;
            _chess[1, 7] = Piece.蓝象; _chess[1, 8] = Piece.蓝马; _chess[1, 9] = Piece.蓝车;
            _chess[3, 2] = Piece.蓝炮; _chess[3, 8] = Piece.蓝炮;
            _chess[4, 1] = Piece.蓝兵; _chess[4, 3] = Piece.蓝兵; _chess[4, 5] = Piece.蓝兵;
            _chess[4, 7] = Piece.蓝兵; _chess[4, 9] = Piece.蓝兵;
            //初始化红方棋子
            _chess[10, 1] = Piece.红车; _chess[10, 2] = Piece.红马; _chess[10, 3] = Piece.红相;
            _chess[10, 4] = Piece.红士; _chess[10, 5] = Piece.红帅; _chess[10, 6] = Piece.红士;
            _chess[10, 7] = Piece.红相; _chess[10, 8] = Piece.红马; _chess[10, 9] = Piece.红车;
            _chess[8, 2] = Piece.红炮; _chess[8, 8] = Piece.红炮;
            _chess[7, 1] = Piece.红卒; _chess[7, 3] = Piece.红卒; _chess[7, 5] = Piece.红卒;
            _chess[7, 7] = Piece.红卒; 
            _chess[7, 9] = Piece.红卒;
        }

        //类方法，拾起棋子
        public bool PickChess(int pickRow, int pickCol)
        {
            //如果当前走棋方不为无且走棋操作为拾子且拾起的是走棋方的棋子
            if (_curPlayer != Player.无 && _curOperation == Operation.拾子 &&
                _chess[pickRow, pickCol].ToString().IndexOf(_curPlayer.ToString()) == 0)
            {
                //保存拾起的棋子值，行号和列号
                _pickChess = _chess[pickRow, pickCol];
                //保存拾子位置的行号和列号
                _pickRow = pickRow;
                _pickCol = pickCol;

                //切换走棋操作为落子
                _curOperation = Operation.落子;
                return true;
            }
            else
                return false;

        }
        