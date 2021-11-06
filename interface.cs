using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Media;
namespace MyChess
{
    public partial class FormMain : Form
    {

        Chess chess = new Chess();

        //保存棋盘（细框）的左上角坐标
        private Point _leftTop = new Point(60, 60);

        //保存棋子半径
        private int _pieceR = 29;

        private int _dropRow = 0;
        private int _dropCol = 0;


        //保存当前所在点坐标
        private Point _curMousePoint = new Point(0, 0);

        private Color _markColor = Color.Yellow;

        //保存棋盘格子的行高和列数
        private int _rowHeight = 60;
        private int _colWidth = 60;

        private Bitmap _deskBmp = new Bitmap("desktop.jpg");//只装载一次，减少运行时间

        //保存红蓝双方14种棋子
        private Bitmap[] _pieceBmp = new Bitmap[15];

        //保存红方头像位图
        private Bitmap _redBmp = new Bitmap("红方头像.bmp");

        //保存蓝方头像位图
        private Bitmap _blueBmp = new Bitmap("蓝方头像.bmp");


        private void DrawBoard(Graphics g)
        {
            g.DrawImage(_deskBmp, new Point(0, 0));

            Pen thickPen = new Pen(Color.Black, 6);
            Pen thinPen = new Pen(Color.Black, 2);

            int gap = (int)(_rowHeight * 0.15);

            g.DrawRectangle(thickPen, new Rectangle(_leftTop.X - gap, _leftTop.Y - gap, _colWidth * 8 + 2 * gap, _rowHeight * 9 + 2 * gap));

            for (int row = 1; row <= 10; row++)
            {
                g.DrawLine(thinPen,new Point(_leftTop.X,_leftTop.Y+_rowHeight*(row-1)),
                                   new Point(_leftTop.X+8*_colWidth,_leftTop.Y+_rowHeight*(row-1)));
            }

            for (int col = 1; col <= 9; col++)
            {
                g.DrawLine(thinPen, new Point(_leftTop.X + (col - 1) * _colWidth, _leftTop.Y),
                                    new Point(_leftTop.X + (col - 1) * _colWidth, _leftTop.Y + _rowHeight * 4));
                g.DrawLine(thinPen, new Point(_leftTop.X + (col - 1) * _colWidth, _leftTop.Y + _rowHeight * 5),
                                    new Point(_leftTop.X + (col - 1) * _colWidth, _leftTop.Y + _rowHeight * 9));
            }

            g.DrawLine(thinPen, new Point(_leftTop.X, _leftTop.Y + _rowHeight * 4),
                                new Point(_leftTop.X, _leftTop.Y + _rowHeight * 5));
            g.DrawLine(thinPen, new Point(_leftTop.X + 8 * _colWidth, _leftTop.Y + _rowHeight * 4),
                                new Point(_leftTop.X + 8 * _colWidth, _leftTop.Y + _rowHeight * 5));
            
            //上方九宫格交叉线
            g.DrawLine(thinPen,new Point(_leftTop.X + 3 * _colWidth,_leftTop.Y),
                               new Point(_leftTop.X + 5 * _colWidth,_leftTop.Y + 2 * _rowHeight));
            g.DrawLine(thinPen,new Point(_leftTop.X + 5 * _colWidth, _leftTop.Y),
                               new Point(_leftTop.X + 3 * _colWidth, _leftTop.Y + 2 * _rowHeight));

            g.DrawLine(thinPen,new Point(_leftTop.X + 3 * _colWidth, _leftTop.Y + 7 * _rowHeight),
                               new Point(_leftTop.X + 5 * _colWidth, _leftTop.Y + 9 * _rowHeight));
            g.DrawLine(thinPen,new Point(_leftTop.X + 5 * _colWidth, _leftTop.Y + 7 * _rowHeight),
                               new Point(_leftTop.X + 3 * _colWidth, _leftTop.Y + 9 * _rowHeight));

            Font font1 = new Font("隶书", (float)(_rowHeight * 0.8), FontStyle.Regular, GraphicsUnit.Pixel);
            SolidBrush brush = new SolidBrush(Color.Black);
            g.DrawString("楚河", font1, brush, new Point(_leftTop.X + _colWidth, (int)(_leftTop.Y + _rowHeight * 4.1)));
            g.DrawString("汉界", font1, brush, new Point(_leftTop.X + _colWidth * 5, (int)(_leftTop.Y + _rowHeight * 4.1)));

            Font font2 = new Font("黑体", (float)(_rowHeight * 0.6), FontStyle.Regular, GraphicsUnit.Pixel);
            for (int row = 1; row <= 10; row++)
            {
                g.DrawString(row.ToString(), font2, brush, new Point((int)(_leftTop.X + _colWidth * 8.6),
                                                                     (int)(_leftTop.Y - _rowHeight * 0.4 + _rowHeight * (row - 1))));
            }

            string[] colNumber = new string[9]{"一","二","三","四","五","六","七","八","九"};
            Font font3 = new Font("黑体",(float)(_rowHeight*0.5),FontStyle.Regular,GraphicsUnit.Pixel);
            for(int col=1;col<=9;col++)
            {
                g.DrawString(colNumber[col-1],font3,brush,new Point((int)(_leftTop.X-_colWidth*0.3+_colWidth*(col-1)),
                                                                    (int)(_leftTop.Y+_rowHeight*9.6)));
            }

            g.DrawString("蓝方",font3,brush,new Point(_leftTop.X+8*_colWidth+95,_leftTop.Y+(int)(2.2*_rowHeight)));
            g.DrawString("红方",font3,brush,new Point(_leftTop.X+8*_colWidth+95,_leftTop.Y+(int)(6.4*_rowHeight)));

            DrawCampMark(g, new Point(_leftTop.X + _colWidth * 1, _leftTop.Y + _rowHeight * 2), true, true);
            DrawCampMark(g, new Point(_leftTop.X + _colWidth * 7, _leftTop.Y + _rowHeight * 2), true, true);

            DrawCampMark(g, new Point(_leftTop.X + _colWidth * 0, _leftTop.Y + _rowHeight * 3), false, true);
            DrawCampMark(g, new Point(_leftTop.X + _colWidth * 2, _leftTop.Y + _rowHeight * 3), true, true);
            DrawCampMark(g, new Point(_leftTop.X + _colWidth * 4, _leftTop.Y + _rowHeight * 3), true, true);
            DrawCampMark(g, new Point(_leftTop.X + _colWidth * 6, _leftTop.Y + _rowHeight * 3), true, true);
            DrawCampMark(g, new Point(_leftTop.X + _colWidth * 8, _leftTop.Y + _rowHeight * 3), true, false);

            DrawCampMark(g, new Point(_leftTop.X + _colWidth * 0, _leftTop.Y + _rowHeight * 6), false, true);
            DrawCampMark(g, new Point(_leftTop.X + _colWidth * 2, _leftTop.Y + _rowHeight * 6), true, true);
            DrawCampMark(g, new Point(_leftTop.X + _colWidth * 4, _leftTop.Y + _rowHeight * 6), true, true);
            DrawCampMark(g, new Point(_leftTop.X + _colWidth * 6, _leftTop.Y + _rowHeight * 6), true, true);
            DrawCampMark(g, new Point(_leftTop.X + _colWidth * 8, _leftTop.Y + _rowHeight * 6), true, false);

            DrawCampMark(g, new Point(_leftTop.X + _colWidth * 1, _leftTop.Y + _rowHeight * 7), true, true);
            DrawCampMark(g, new Point(_leftTop.X + _colWidth * 7, _leftTop.Y + _rowHeight * 7), true, true);

            //绘制拾子位置的标记
            DrawPickDropMark(g, chess._pickRow, chess._pickCol);
            //绘制落子位置的标记
            DrawPickDropMark(g, _dropRow, _dropCol);

            //在相应的位置绘制走棋方的头像
            if (chess._curPlayer == Player.蓝)
                g.DrawImage(_blueBmp, new Point(_leftTop.X + 8 * _colWidth + 95, _leftTop.Y + 1 * _rowHeight - 10));
            else if (chess._curPlayer == Player.红)
                g.DrawImage(_redBmp, new Point(_leftTop.X + 8 * _colWidth + 95, _leftTop.Y + 7 * _rowHeight + 10));
        }

        //自定义：绘制炮和兵的营地标志
        public void DrawCampMark(Graphics g, Point center, Boolean drawLeft, Boolean drawRight)
        {
            //偏移量和线段长度
            int offset = (int)(_rowHeight * 0.08), length = (int)(_rowHeight * 0.16);
            //直角坐标
            Point corner = new Point();
            //画笔对象
            Pen thinPen = new Pen(Color.Black, 1);
            //是否需要绘制左边标志
            if (drawLeft == true)
            {
                corner.X = center.X - offset;
                corner.Y = center.Y - offset;
                g.DrawLine(thinPen, new Point(corner.X, corner.Y), new Point(corner.X - length, corner.Y));
                g.DrawLine(thinPen, new Point(corner.X, corner.Y), new Point(corner.X, corner.Y - length));
                corner.X = center.X - offset;
                corner.Y = center.Y + offset;
                g.DrawLine(thinPen, new Point(corner.X, corner.Y), new Point(corner.X - length, corner.Y));
                g.DrawLine(thinPen, new Point(corner.X, corner.Y), new Point(corner.X, corner.Y + length));
            }
            //是否需要绘制右边标志
            if (drawRight == true)
            {
                corner.X = center.X + offset;
                corner.Y = center.Y - offset;
                g.DrawLine(thinPen, new Point(corner.X, corner.Y), new Point(corner.X + length, corner.Y));
                g.DrawLine(thinPen, new Point(corner.X, corner.Y), new Point(corner.X, corner.Y - length));
                corner.X = center.X + offset;
                corner.Y = center.Y + offset;
                g.DrawLine(thinPen, new Point(corner.X, corner.Y), new Point(corner.X + length, corner.Y));
                g.DrawLine(thinPen, new Point(corner.X, corner.Y), new Point(corner.X, corner.Y + length));
            }
        }

        public void DrawPiece(Graphics g)
        {
            for (int row = 1; row <= 10; row++)
            {
                for (int col = 1; col <= 9; col++)
                {
                    //如果该位子存在棋子
                    if (chess._chess[row, col] != Piece.无子)
                    {
                        //在棋盘交点绘制棋子
                        g.DrawImage(_pieceBmp[(int)chess._chess[row, col]], new Point(_leftTop.X + (col - 1) * _colWidth - _pieceR, _leftTop.Y + (row - 1) * _rowHeight - _pieceR));
                    }
                }
            }

            //在当前鼠标点位置绘制的棋子，以显示出拾起的棋子随鼠标而动的效果
            if (chess._pickChess != Piece.无子)
            {
                g.DrawImage(_pieceBmp[(int)chess._pickChess], new Point(_curMousePoint.X - _pieceR, _curMousePoint.Y - _pieceR));

            }
        }

        //自定义类方法：把鼠标点击位置坐标转化成棋盘的行号和列号
        public bool ConvertPointToRowCol(Point p, out int row, out int col)
        {
            //获取与鼠标点击位置距离最近的棋盘交叉的行号
            row = (p.Y - _leftTop.Y) / _rowHeight + 1;
            //如果鼠标点Y坐标超过棋盘行高的中线，则行号需要加1
            if (((p.Y - _leftTop.Y) % _rowHeight) >= _rowHeight / 2)
                row = row + 1;

            //获取与鼠标点击位置距离最近的棋盘交叉点的列号
            col = (p.X - _leftTop.X) / _colWidth + 1;
            //如果鼠标点x坐标超过棋盘列宽的中线，则列号需要加1
            if (((p.X - _leftTop.X) % _colWidth) >= _colWidth / 2)
                col = col + 1;

            //获取与鼠标点击位置距离最近的棋盘交叉点的坐标
            Point chessP = new Point();
            chessP.X = _leftTop.X + _colWidth * (col - 1);
            chessP.Y = _leftTop.Y + _rowHeight * (row - 1);

            //判断是否落在棋子半径之内，且在10行9列之内
            double dist = Math.Sqrt(Math.Pow(p.X - chessP.X, 2) + Math.Pow(p.Y - chessP.Y, 2));
            if ((dist <= _pieceR) && (row <= 10) && (row >= 1) && (col <= 9) && (col >= 1))
            {
                //返回true，表示该点击为有效点击
                return true;
            }
            else
            {
                //行号和列号设置为0，并返回false，表示该点击为无效点击
                row = 0; col = 0;
                return false;
            }
        }

        //自定义类方法：播放声音文件
        public void PlaySound(string wavFile)
        {
            //装载声音文件（需要添加System.Media命名空间）
            SoundPlayer soundPlay = new SoundPlayer(wavFile);
            //使用新线程播放声音
            soundPlay.Play();
            //注意：soundPlay.PlaySync()也可以播放声音，该方法使用用户界面（UI）线程播放，会导致用户界面的停顿
        }

        public FormMain()
        {
            InitializeComponent();

            //根据屏幕的分辨率设置棋盘格子的行高和列宽
            _rowHeight = Screen.PrimaryScreen.Bounds.Size.Height / 13;
            _colWidth = _rowHeight;

            //设置棋盘上左上角坐标
            _leftTop.X = _leftTop.Y;
            _leftTop.Y = _rowHeight * 2;
            
            for (int i = 1; i <= 14; i++)
            {
                _pieceBmp[i] = new Bitmap(((Piece)i).ToString() + ".bmp");
                _pieceBmp[i].MakeTransparent(Color.White);
            }

            //设置红蓝方头像位图的透明色
            _redBmp.MakeTransparent(Color.White);
            _blueBmp.MakeTransparent(Color.White);
        }

        //自定义类方法：绘制拾子或落子位置的标记
        public void DrawPickDropMark(Graphics g, int row, int col)
        {
            if (row != 0)
            {
                Pen pen = new Pen(Color.Yellow, 4);
                Point p = new Point(_leftTop.X + _colWidth * (col - 1), _leftTop.Y + _rowHeight * (row - 1));

                g.DrawLine(pen, p.X - _pieceR, p.Y - _pieceR, p.X - _pieceR / 2, p.Y - _pieceR);
                g.DrawLine(pen, p.X - _pieceR, p.Y - _pieceR, p.X - _pieceR, p.Y - _pieceR / 2);

                g.DrawLine(pen, p.X + _pieceR, p.Y - _pieceR, p.X + _pieceR / 2, p.Y - _pieceR);
                g.DrawLine(pen, p.X + _pieceR, p.Y - _pieceR, p.X + _pieceR, p.Y - _pieceR / 2);

                g.DrawLine(pen, p.X - _pieceR, p.Y + _pieceR, p.X - _pieceR / 2, p.Y + _pieceR);
                g.DrawLine(pen, p.X - _pieceR, p.Y + _pieceR, p.X - _pieceR, p.Y + _pieceR / 2);

                g.DrawLine(pen, p.X + _pieceR, p.Y + _pieceR, p.X + _pieceR / 2, p.Y + _pieceR);
                g.DrawLine(pen, p.X + _pieceR, p.Y + _pieceR, p.X + _pieceR, p.Y + _pieceR / 2);
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void FormMain_Paint(object sender, PaintEventArgs e)
        {
            DrawBoard(e.Graphics);

            DrawPiece(e.Graphics);
        }

        private void MenuItemBegin_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你是否需要开局？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //红方先走
                chess.Begin(Player.红);

                _dropRow = 0;
                _dropCol = 0;

                //使窗口失效，并发送paint消息，从而触发paint事件相应方法重绘棋盘和棋子
                Invalidate();

                PlaySound("Sounds\\begin.wav");
            }
        }

        private void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //把鼠标点击位置设置为行号和列号
                int row, col;
                bool valid = ConvertPointToRowCol(new Point(e.X, e.Y), out row, out col);

                //判断鼠标点击是否有效
                if (valid == true)
                {
                    if (chess._curOperation == Operation.拾子)
                    {
                        if (chess.PickChess(row, col) == true)
                            chess._chess[row, col] = Piece.无子;
                    }
                    else if (chess._curOperation == Operation.落子)
                    {

                        if (chess.DropChess(row, col) == true )
                        {
                            if (chess.IsOver() != Player.无)
                            {
                                MessageBox.Show(chess.IsOver() + "赢了", "提示");
                            }

                            _dropRow = row;
                            _dropCol = col;
                        }
                    }
                    Invalidate();
                }
            }
            else if (e.Button == MouseButtons.Right)
            { 
                if (chess.UndoPickChess() == true)
                {
                    chess._chess[chess._pickRow, chess._pickCol] = chess._pickChess;
                    chess._pickRow = 0;
                    chess._pickCol = 0;
                    chess._pickChess = Piece.无子;
                }
                //强制刷新窗口
                Invalidate();
                
            }
        }

        private void FormMain_MouseMove(object sender, MouseEventArgs e)
        {
            //保存当前鼠标所在点的坐标
            _curMousePoint = e.Location;
            
            //判断是否已经拾起棋子，以决定是否需要强制刷新窗口
            if (chess._pickChess != Piece.无子)
            {
                Invalidate();
            }
        }

        private void MenuItemUndo_Click(object sender, EventArgs e)
        {
            chess.UndoLastStep();
        }

        private void MenuItemSave_Click(object sender, EventArgs e)
        {
            //显示保存残局对话框
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //创建一个文件流对剑，用于写文件（需要添加System.IO命名空间）                
                FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create);
                //创建一个文件流对象相对应的二进制写入流对象
                BinaryWriter bw = new BinaryWriter(fs);

                chess.WriteTo(bw);

                //关闭有关文件流对象
                bw.Close();
                fs.Close();
            }
        }

        private void MenuItemOpen_Click(object sender, EventArgs e)
        {
            //显示打开残局对话框
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //创建一个文件流对象，用于读文件
                FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);

                //创建一个与文件流对象相对应的二进制读入流对象
                BinaryReader br = new BinaryReader(fs);

                chess.ReadFrom(br);

                //关闭有关文件流对象
                br.Close();
                fs.Close();

                //强制刷新窗口（触发Paint事件）
                Invalidate();
            }
        }
    }
}

