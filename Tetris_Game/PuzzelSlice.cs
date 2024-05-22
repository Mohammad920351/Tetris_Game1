using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace Tetris_Game
{
    public class Puzzel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Name { get; set; }
        public List<PixelPnl> PzlSlicesList;
        public List<PixelPnl> MemoryPnlSlice;
        public int[,] SlicePointList;
        public bool isStoped { get; set; }
        public int stopMode = 0;

        public BoardGame boardGame;
        public Color color;
        public Puzzel(int[,] slicePointList, Color color, BoardGame board)

        {
            boardGame = board;


            PzlSlicesList = new List<PixelPnl>();
            this.SlicePointList = slicePointList;
            this.color = color;

            //SlicePointList = slicePointList;


            MemoryPnlSlice = new List<PixelPnl>();

        }
        public void Create(int[,] SlicePointList, Color color)
        {

            for (int i = 0; i < SlicePointList.GetLength(0); i++)
            {
                PixelPnl newSlice = new PixelPnl()
                {
                    row = SlicePointList[i, 0],
                    column = SlicePointList[i, 1],
                    BackColor = color
                };
                PzlSlicesList.Add(newSlice);
            }
        }
        public void DrowPuzell(int r=0,int c=0)
        {
            if (this.isStoped == true)
            {
                //return;
            }
            foreach (var pnlSlice in this.MemoryPnlSlice)
            {
                pnlSlice.Controls.Clear();
                pnlSlice.isFree = true;
                pnlSlice.takenBy = null;

            }
            this.MemoryPnlSlice.Clear();

            foreach (var item in boardGame.pnl_SliceList)
            {

                foreach (var item1 in PzlSlicesList)
                {
                    if (item1.row == item.row-r && item1.column == item.column-c)
                    {
                        item.Controls.Add(item1);
                        item.takenBy = this;
                        item.isFree = false;
                        //item.puzleslice = item1;
                        this.MemoryPnlSlice.Add(item);

                    }
                }

            }
          
        }
        public void Move(char Dir)
        {
            //this.CheckForStop();

            if (this.isStoped == true || this.FreeToMove(Dir) == false)
            {
                return;
            }


            foreach (var item in PzlSlicesList)
            {
                switch (Dir)
                {
                    case 'U':

                        break;
                    case 'D':
                        item.row++;
                        break;
                    case 'R':
                        item.column++;

                        break;
                    case 'L':
                        item.column--;

                        break;
                    default:

                        break;
                }
            }

            //for (int i = 0; i < SlicesList.Count-1; i++)
            //{
            //    SlicesList[i].row++;

            //}


        }
        public void CheckForStop()
        {
            foreach (var pzlpix in PzlSlicesList)
            {
                if (pzlpix.row >= boardGame.RowCount - 1)
                {
                    //if (stopMode == 0) { stopMode = 1; }
                    //else if (stopMode == 1) { stopMode = 2; }
                    //break;
                    isStoped = true;
                }
               else if (boardGame.pnl_SliceList.Any(pix => pix.row == pzlpix.row + 1 && pix.column == pzlpix.column && pix.takenBy?.isStoped == true))
                {
                    //if (stopMode == 0) { stopMode = 1; }
                    //else if (stopMode == 1) { stopMode = 2; }
                    //break;
                    isStoped = true;
                }
            }
            //if (stopMode == 2) { isStoped = true; }

        }

        public void Input()
        {
            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();

            if (Console.KeyAvailable)
            {
                keyInfo = Console.ReadKey(true);
                MessageBox.Show(keyInfo.KeyChar.ToString());
            }
        }
        public bool FreeToMove(char Dir)
        {
            var freeMove = true;
            int r = 0; int c = 0;
            //bool outOfBoard = false;

            switch (Dir)
            {
                case 'U':

                    break;
                case 'D':
                    r = 1; c = 0;
                    break;
                case 'R':
                    r = 0; c = 1;

                    break;
                case 'L':
                    r = 0; c = -1;
                    break;
                default:

                    break;
            }

            foreach (var slice in PzlSlicesList)
            {
                if (boardGame.pnl_SliceList.Any(Pix => Pix.row == slice.row + r && Pix.column == slice.column + c &&
                Pix.takenBy != this && Pix.isFree == false))
                {

                    freeMove = false;
                }
                else if (slice.column + c > boardGame.RowCount - 1 || slice.column + c < 0)
                {
                    freeMove = false;
                }
                else if (slice.row + r > boardGame.RowCount - 1)
                {
                    freeMove = false;
                }
            }
            return freeMove;
        }
        public void RotatePuzzle()
        {
            //bool rotateDone = false;
            var minR = PzlSlicesList.OrderBy(p => p.row).ToList()[0].row;
            var maxR = PzlSlicesList.OrderByDescending(p => p.row).ToList()[0].row;
            var minC = PzlSlicesList.OrderBy(p => p.column).ToList()[0].column;
            var maxC = PzlSlicesList.OrderByDescending(p => p.column).ToList()[0].column;

            foreach (var pzlpix in PzlSlicesList) ///Chek for free to rotate
            {
                //int[] listI = { 1, 2, 3 };
                //foreach (var i in listI)
                //{

                var pzlpixR = pzlpix.row;
                var pzlpixC = pzlpix.column;
                var c = maxC - (pzlpixR - minR);
                var r = maxR - (maxC - pzlpixC);
                if (boardGame.pnl_SliceList.Any(pix => pix.row == r && pix.column == c && pix.takenBy != this & pix.isFree == false))
                {
                    //break;
                    return;
                }
                //}


            }
            foreach (var pzlpix in PzlSlicesList)
            {
                var pzlpixR = pzlpix.row;
                var pzlpixC = pzlpix.column;
                pzlpix.column = maxC - (pzlpixR - minR);
                pzlpix.row = maxR - (maxC - pzlpixC);

            }
            for (int i = minR; i <= maxR; i++)
            {

            }
        }

        //public bool FreeToMoveDown()
        //{
        //    var freeDown = true;
        //    foreach (var slice in PzlSlicesList)
        //    {
        //        if (boardGame.pnl_SliceList.Any(s => s.row == slice.row + 1 && s.column == slice.column &&
        //        s.takenBy != this && s.isFree == false))
        //        {

        //            freeDown = false;
        //        }

        //    }
        //    return freeDown;
        //}
    }
    public static class CreatePuzzel
    {



        public static Puzzel CreatePuzzle(string model, BoardGame boardGame)
        {

            Color color = Color.Blue;
            int[,] SlicePointList;
            int[,] SlicePointList1 = { { 0, 5 }, { 0, 6 }, { 0, 7 }, { 1, 7 } };
            int[,] SlicePointList2 = { { 1, 5 }, { 0, 5 }, { 0, 6 }, { 0, 7 } };
            int[,] SlicePointList3 = { { 0, 5 }, { 0, 6 }, { 0, 7 }, { 0, 8 } };
            int[,] SlicePointList4 = { { 0, 5 }, { 0, 6 }, { 0, 7 }, { 1, 6 } };
            int[,] SlicePointList5 = { { 0, 5 }, { 0, 6 }, { 1, 5 }, { 1, 6 } };



            switch (model)
            {
                case "1":
                    SlicePointList = SlicePointList1;
                    color = Color.Blue;
                    break;
                case "2":
                    SlicePointList = SlicePointList2;
                    color = Color.Red;
                    break;
                case "3":
                    SlicePointList = SlicePointList3;
                    color = Color.Green;
                    break;
                case "4":
                    SlicePointList = SlicePointList4;
                    color = Color.Yellow;
                    break;
                case "5":
                    SlicePointList = SlicePointList5;
                    color = Color.BurlyWood;
                    break;
                default:
                    SlicePointList = SlicePointList1;
                    break;
            }

            Puzzel puzzel1 = new Puzzel(SlicePointList, color, boardGame);
            puzzel1.Create(SlicePointList, color);
            return puzzel1;
        }


    }
    //public class Puzzel1 : Puzzel
    //{
    //    public Puzzel1(  BoardGame board) : base(  board)
    //    {
    //        int[,] SlicePointList = { { 0, 2 }, { 0, 3 }, { 0, 4 }, { 1, 4 } };
    //        color = Color.Blue;
    //    }
    //}
    //public class PuzzelSlice : PixelPnl
    //{

    //    public PuzzelSlice()
    //    {



    //    }
    //}
}
