using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MyChess
{
    //棋子枚举类型
    public enum Piece
    {
        无子 = 0,
        红车 = 1, 红马 = 2, 红相 = 3, 红士 = 4, 红帅 = 5, 红炮 = 6, 红卒 = 7,
        蓝车 = 8, 蓝马 = 9, 蓝象 = 10, 蓝士 = 11, 蓝将 = 12, 蓝炮 = 13, 蓝兵 = 14
    }

    public enum Player
    {
        无 = 0, 红 = 1, 蓝 = 2
    }

    //枚举类型 走棋操作
    public enum Operation
    {
        拾子, 落子
    }

    public class Step
    {
        public Player _player;
        public Piece _pickChess;
        public int _pickRow;
        public int _pickCol;
        public Piece _dropChess;
        public int _dropRow;
        public int _dropCol;

    }

    class Chess
    {
        //保存象棋棋盘所有棋子值
        public Piece[,] _chess = new Piece[11, 10];

        //保存当前的走棋方
        public Player _curPlayer = Player.无;

        //保存当前走棋操作
        public Operation _curOperation = Operation.拾子;

        //保存拾起的棋子
        public Piece _pickChess = Piece.无子;
        //保存拾起棋子的行号和列号
        public int _pickRow = 0;
        public int _pickCol = 0;



        //保存所有的走棋步骤
        public List<Step> _stepList = new List<Step>();

        public Step _lastStep
        {
            get
            {
                int stepCount = _stepList.Count;
                if (stepCount > 0)
                    return _stepList[stepCount - 1];
                else
                    return null;
            }
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
            _chess[7, 7] = Piece.红卒; _chess[7, 9] = Piece.红卒;
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

        //类方法，落下棋子(true代表落子成功，false代表落子失败)
        public bool DropChess(int dropRow, int dropCol)
        {
            //如果当前走棋方不为无且走棋操作为落子且规则验证成功
            if (_curPlayer != Player.无 && _curOperation == Operation.落子 &&
                MatchRules(_curPlayer, _pickRow, _pickCol, dropRow, dropCol) == true)
            {

                //保存走棋步骤到_stepList中
                Step tempStep = new Step();
                tempStep._player = _curPlayer;
                tempStep._pickChess = _pickChess;
                tempStep._pickRow = _pickRow;
                tempStep._pickCol = _pickCol;
                tempStep._dropChess = _chess[dropRow, dropCol];
                tempStep._dropRow = dropRow;
                tempStep._dropCol = dropCol;
                _stepList.Add(tempStep);

                //落子位置设置为拾起位置的值
                _chess[dropRow, dropCol] = _pickChess;

                //清空拾子位置的棋子值
                _chess[_pickRow, _pickCol] = Piece.无子;

                //初始化拾起的棋子
                _pickChess = Piece.无子;
                //初始化拾起棋子的行号和列号
                _pickRow = 0;
                _pickCol = 0;


                //切换走棋步骤为拾子
                _curOperation = Operation.拾子;

                //切换走棋方
                if (_curPlayer == Player.红)
                    _curPlayer = Player.蓝;
                else if (_curPlayer == Player.蓝)
                    _curPlayer = Player.红;
                return true;

            }
            else
                return false;
        }

        public bool UndoPickChess()
        {
            //如果当前走棋方不为无且已经拾起了棋子（即当前为落子）
            if (_curPlayer != Player.无 && _curOperation == Operation.落子)
            {
                _curOperation = Operation.拾子;
                return true;
            }
            else
                return false;
        }

        //悔棋（true代表成功，false代表失败）
        public bool UndoLastStep()
        {
            //获取走棋步骤数
            int stepCount = _stepList.Count;
            //判断是否存在走棋步骤
            if (stepCount > 0)
            {
                //取出最后一部走棋信息
                Player player = _stepList[_stepList.Count - 1]._player;
                Piece pickChess = _stepList[_stepList.Count - 1]._pickChess;
                int pickRow = _stepList[_stepList.Count - 1]._pickRow;
                int pickCol = _stepList[_stepList.Count - 1]._pickCol;
                Piece dropChess = _stepList[_stepList.Count - 1]._dropChess;
                int dropRow = _stepList[_stepList.Count - 1]._dropRow;
                int dropCol = _stepList[_stepList.Count - 1]._dropCol;

                //删除最后一部走棋步骤
                _stepList.RemoveAt(_stepList.Count - 1);

                //还原走棋步骤
                _chess[pickRow, pickCol] = pickChess;
                _chess[dropRow, dropCol] = dropChess;

                //初始化走棋操作为拾子
                _curOperation = Operation.拾子;
                //初始化拾起的棋子
                _pickChess = Piece.无子;
                //初始化拾起棋子的行号和列号
                _pickRow = 0;
                _pickCol = 0;

                //设置当前的走棋方
                _curPlayer = player;

                return true;
            }
            else
                return false;
        }

        //类方法判断象棋是否结束并返回获胜方
        public Player IsOver()
        {
            //获取走棋步骤数
            int stepCount = _stepList.Count;
            //判断是否存在走棋步骤
            if (stepCount > 0)
            {
                //获取最后一步走棋步骤中落下或被吃的棋子
                Piece dropChess = _stepList[stepCount - 1]._dropChess;
                //判断落下棋子（被吃）棋子是否为帅
                if (dropChess == Piece.蓝将)
                {
                    //禁止继续下棋
                    _curPlayer = Player.无;
                    //返回获胜方
                    return Player.红;
                }
                else if (dropChess == Piece.红帅)
                {
                    //禁止继续下棋
                    _curPlayer = Player.无;
                    //返回获胜方
                    return Player.蓝;
                }
                else
                    return Player.无;
            }
            else
                return Player.无;
        }

        //类方法：匹配走棋规则（true代表成功，false代表失败）
        public bool MatchRules(Player player, int pickRow, int pickCol, int dropRow, int dropCol)
        {
            //定义一个匹配标志
            bool matchFlag = false;

            //如果走棋方不为无且拾起的是走棋方棋子且落子位置为无子或走棋方棋子
            if ( player != Player.无 
                && !(pickCol == dropCol && pickRow == dropRow)
                && _pickChess.ToString().Substring(0, 1) == player.ToString()
                && (_chess[dropRow, dropCol] == Piece.无子 
                    || _chess[dropRow, dropCol].ToString().Substring(0, 1) != player.ToString()))
            {
                //匹配车的走棋规则
                if (_pickChess == Piece.蓝车 || _pickChess == Piece.红车)
                {
                    //需要用到的局部变量申明
                    bool blankFlag = false;
                    int max, min;
                    //如果横冲
                    if (pickRow == dropRow)
                    {
                        //比较起点列和落点列的大小
                        max = dropCol > pickCol ? dropCol : pickCol;
                        min = dropCol > pickCol ? pickCol : dropCol;

                        //判断移动路径上棋子的是否全为无子
                        blankFlag = true;
                        for (int i = min + 1; i <= max - 1; i++)
                        {
                            if (_chess[dropRow, i] != Piece.无子)
                                blankFlag = false;
                        }
                        if (blankFlag == true)
                            matchFlag = true;
                    }
                    //如果直撞
                    if (pickCol == dropCol)
                    {
                        //比较起点列和落点列的大小
                        max = dropRow > pickRow ? dropRow : pickRow;
                        min = dropRow > pickRow ? pickRow : dropRow;

                        //判断移动路径上棋子的是否全为无子
                        blankFlag = true;
                        for (int i = min + 1; i <= max - 1; i++)
                        {
                            if (_chess[i, dropCol] != Piece.无子)
                                blankFlag = false;
                        }
                        if (blankFlag == true)
                            matchFlag = true;
                    }
                }

                //匹配马的走棋规则
                if (_pickChess == Piece.蓝马 || _pickChess == Piece.红马)
                {
                    //如果横着走日字且不拌马脚
                    if (Math.Abs(pickRow - dropRow) == 1 && Math.Abs(pickCol - dropCol) == 2 &&
                        _chess[pickRow, (pickCol + dropCol) / 2] == Piece.无子)
                        matchFlag = true;

                    //如果竖着走日字且不拌马脚
                    if (Math.Abs(pickRow - dropRow) == 2 && Math.Abs(pickCol - dropCol) == 1 &&
                        _chess[(pickRow + dropRow) / 2, pickCol] == Piece.无子)
                        matchFlag = true;
                }

                //匹配象的走棋规则
                if (_pickChess == Piece.蓝象)
                {
                    //走田字，不拌象脚
                    if (Math.Abs(pickRow - dropRow) == 2 && Math.Abs(pickCol - dropCol) == 2
                        && _chess[(pickRow + dropRow) / 2, (pickCol + dropCol) / 2] == Piece.无子 && dropRow <= 5)
                        matchFlag = true;
                }
                if (_pickChess == Piece.红相)
                {
                    //走田字，不拌象脚
                    if (Math.Abs(pickRow - dropRow) == 2 && Math.Abs(pickCol - dropCol) == 2
                        && _chess[(pickRow + dropRow) / 2, (pickCol + dropCol) / 2] == Piece.无子 && dropRow >= 6)
                        matchFlag = true;
                }

                //匹配士的走棋规则
                if (_pickChess == Piece.蓝士)
                {
                    if (Math.Abs(pickRow - dropRow) == 1 && Math.Abs(pickCol - dropCol) == 1 && dropRow <= 3 && dropRow >= 1 && dropCol <= 6 && dropCol >= 4)
                        matchFlag = true;
                }
                if (_pickChess == Piece.红士)
                {
                    if (Math.Abs(pickRow - dropRow) == 1 && Math.Abs(pickCol - dropCol) == 1 && dropRow <= 10 && dropRow >= 8 && dropCol <= 6 && dropCol >= 4)
                        matchFlag = true;
                }

                //匹配红帅的走棋规则
                if (_pickChess == Piece.红帅)
                {
                    //如果在九宫格内移动
                    if (((Math.Abs(pickRow - dropRow) == 1 && pickCol == dropCol) || (Math.Abs(pickCol - dropCol) == 1 && pickRow == dropRow)) && dropRow >= 8 && dropRow <= 10 && dropCol >= 4 && dropCol <= 6)
                    {
                        matchFlag = true;
                    }
                    //如果两帅想面对面火拼
                    if (_chess[dropRow, dropCol] == Piece.蓝将)
                    {
                        //判断两帅是否在同一列上
                        if (pickCol == dropCol)
                        {
                            bool blankFlag = true;
                            //检查两帅之间是否存在棋子
                            for (int i = dropRow + 1; i <= pickRow - 1; i++)
                            {
                                if (_chess[i, pickCol] != Piece.无子)
                                {
                                    blankFlag = false;
                                    break;
                                }
                            }
                            if (blankFlag == true)
                                matchFlag = true;
                        }
                    }
                }
                //匹配蓝将的走棋规则
                if (_pickChess == Piece.蓝将)
                {
                    //如果在九宫格内移动
                    if (((Math.Abs(pickRow - dropRow) == 1 && pickCol == dropCol) || (Math.Abs(pickCol - dropCol) == 1 && pickRow == dropRow)) && dropRow >= 1 && dropRow <= 3 && dropCol >= 4 && dropCol <= 6)
                    {
                        matchFlag = true;
                    }
                    //如果两帅想面对面火拼
                    if (_chess[dropRow, dropCol] == Piece.红帅)
                    {
                        //判断两帅是否在同一列上
                        if (pickCol == dropCol)
                        {
                            bool blankFlag = true;
                            //检查两帅之间是否存在棋子
                            for (int i = pickRow + 1; i <= dropRow - 1; i++)
                            {
                                if (_chess[i, pickCol] != Piece.无子)
                                {
                                    blankFlag = false;
                                    break;
                                }
                            }
                            if (blankFlag == true)
                                matchFlag = true;
                        }
                    }
                }

                //匹配炮的走棋规则
                if (_pickChess == Piece.红炮 || _pickChess == Piece.蓝炮)
                {
                    //需要用到的局部变量申明
                    bool blankFlag = false;
                    int max, min;
                    //如果落点存在棋子时
                    if (_chess[dropRow, dropCol] != Piece.无子)
                    {
                        //如果横着走
                        if (pickRow == dropRow)
                        {
                            //比较起点列和落点列的大小
                            max = dropCol > pickCol ? dropCol : pickCol;
                            min = dropCol < pickCol ? dropCol : pickCol;

                            //统计移动路径上棋子的数量
                            int chessNum = 0;
                            for (int i = min + 1; i <= max - 1; i++)
                            {
                                if (_chess[dropRow, i] != Piece.无子)
                                    chessNum++;
                            }
                            if (chessNum == 1)
                                matchFlag = true;
                        }
                        //如果竖着走
                        if (pickCol == dropCol)
                        {
                            //比较起点列和落点列的大小
                            max = dropRow > pickRow ? dropRow : pickRow;
                            min = dropRow < pickRow ? dropRow : pickRow;

                            //统计移动路径上棋子的数量
                            int chessNum = 0;
                            for (int i = min + 1; i <= max - 1; i++)
                            {
                                if (_chess[i, dropCol] != Piece.无子)
                                    chessNum++;
                            }
                            if (chessNum == 1)
                                matchFlag = true;
                        }
                    }
                    //如果落点不存在棋子时
                    if (_chess[dropRow, dropCol] == Piece.无子)
                    {
                        //如果横着走
                        if (pickRow == dropRow)
                        {
                            //比较起点列和落点列的大小
                            max = dropCol > pickCol ? dropCol : pickCol;
                            min = dropCol < pickCol ? dropCol : pickCol;

                            blankFlag = true;
                            //检查两帅之间是否存在棋子
                            for (int i = min + 1; i <= max - 1; i++)
                            {
                                if (_chess[dropRow, i] != Piece.无子)
                                {
                                    blankFlag = false;
                                }
                            }
                            if (blankFlag == true)
                                matchFlag = true;
                        }
                        //如果竖着走
                        if (pickCol == dropCol)
                        {
                            //比较起点列和落点列的大小
                            max = dropRow > pickRow ? dropRow : pickRow;
                            min = dropRow < pickRow ? dropRow : pickRow;

                            blankFlag = true;
                            //检查两帅之间是否存在棋子
                            for (int i = min + 1; i <= max - 1; i++)
                            {
                                if (_chess[i, dropCol] != Piece.无子)
                                {
                                    blankFlag = false;
                                }
                            }
                            if (blankFlag == true)
                                matchFlag = true;
                        }
                    }
                }

                //匹配兵的走棋规则
                if (_pickChess == Piece.蓝兵)
                {
                    //不过河
                    if (pickRow <= 5)
                    {
                        if ((dropRow - pickRow) == 1 && (dropCol == pickCol))
                            matchFlag = true;
                    }
                    //过河
                    if (pickRow >= 6)
                    {
                        //如果横着走
                        if (dropRow == pickRow && Math.Abs(dropCol - pickCol) == 1)
                            matchFlag = true;
                        //如果竖着走
                        if (dropCol == pickCol && (dropRow - pickRow) == 1)
                            matchFlag = true;
                    }
                }
                if (_pickChess == Piece.红卒)
                {
                    //不过河
                    if (pickRow >= 6)
                    {
                        if ((dropRow - pickRow) == -1 && (dropCol == pickCol))
                            matchFlag = true;
                    }
                    //过河
                    if (pickRow <= 5)
                    {
                        //如果横着走
                        if (dropRow == pickRow && Math.Abs(dropCol - pickCol) == 1)
                            matchFlag = true;
                        //如果竖着走
                        if (dropCol == pickCol && (dropRow - pickRow) == -1)
                            matchFlag = true;
                    }
                }
            }
            return matchFlag;
        }


        //保存残局
        public void WriteTo(BinaryWriter binaryWriter)
        {
            //保存当前的走棋方
            binaryWriter.Write(_curPlayer.ToString());

            //逐一保存棋盘上的棋子值
            for (int row = 1; row <= 10; row++)
            {
                for (int col = 1; col <= 9; col++)
                    binaryWriter.Write(_chess[row, col].ToString());
            }

            //保存走棋步骤的数量
            binaryWriter.Write(_stepList.Count);
            //逐一保存走棋步骤详细信息
            for (int i = 0; i <= _stepList.Count - 1; i++)
            {
                binaryWriter.Write(_stepList[i]._player.ToString());
                binaryWriter.Write(_stepList[i]._pickChess.ToString());
                binaryWriter.Write(_stepList[i]._pickRow);
                binaryWriter.Write(_stepList[i]._pickCol);
                binaryWriter.Write(_stepList[i]._dropChess.ToString());
                binaryWriter.Write(_stepList[i]._dropRow);
                binaryWriter.Write(_stepList[i]._dropCol);
            }
        }
        //读取残局
        public void ReadFrom(BinaryReader binaryReader)
        {
            //象棋初始化
            Initialize();

            //读取当前走棋方
            _curPlayer = (Player)Enum.Parse(typeof(Player), binaryReader.ReadString());

            //逐一读取棋盘上棋子
            for (int row = 1; row <= 10; row++)
                for (int col = 1; col <= 9; col++)
                    _chess[row, col] = (Piece)Enum.Parse(typeof(Piece), binaryReader.ReadString());

            //读取走棋步骤数
            int stepCount = binaryReader.ReadInt32();
            //逐一读取走棋步骤详细信息
            for (int i = 0; i <= stepCount - 1; i++)
            {
                Step step = new Step();
                step._player = (Player)Enum.Parse(typeof(Player), binaryReader.ReadString());
                step._pickChess = (Piece)Enum.Parse(typeof(Piece), binaryReader.ReadString());
                step._pickRow = binaryReader.ReadInt32();
                step._pickCol = binaryReader.ReadInt32();
                step._dropChess = (Piece)Enum.Parse(typeof(Piece), binaryReader.ReadString());
                step._dropRow = binaryReader.ReadInt32();
                step._dropCol = binaryReader.ReadInt32();

                //添加走棋步骤到列表
                _stepList.Add(step);
            }
            //设置当前走棋操作
            _curOperation = Operation.拾子;

            //初始化
            _pickChess = Piece.无子;
            _pickRow = _pickCol = 0;
        }

    }
}

