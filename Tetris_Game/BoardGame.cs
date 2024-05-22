using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace Tetris_Game
{
    public class BoardGame
    {
        public Panel pnl_board { get; set; }
        public List<PixelPnl> pnl_SliceList { get; set; }
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }

        public BoardGame(Panel panel)
        {
            pnl_board = panel;
            RowCount =(int) Math.Floor((decimal)panel.Height/(PixelPnl.height));
            ColumnCount =(int) Math.Floor((decimal)panel.Width/(PixelPnl.width));

            pnl_SliceList = new List<PixelPnl>();

        }

        public void DrawingPanel()
        {

            for (int r= 0;r< RowCount; r++) 
            {
                for(int c=0;c< ColumnCount; c++) 
                {
                    PixelPnl panelSlice = new PixelPnl()
                    {
                        row = r,
                        column = c,
                    };

                    panelSlice.Location = new Point( (c * panelSlice.Width),  (r * panelSlice.Height));
                    pnl_SliceList.Add(panelSlice);
                    pnl_board.Controls.Add(panelSlice);
                    //TextBox t = new TextBox();
                    //t.Text = $"c{c}: r{r}";
                    //PixelPnl.Controls.Add(t);
                }
            }
            


            pnl_board.Width = ColumnCount * (PixelPnl.width);
            pnl_board.Height = RowCount * (PixelPnl.height);

        }

    }
    public class PixelPnl : Panel
    {
        public static int width=40;
        public static int height=40;
        public int row { get; set; }
        public int column { get; set; }
        public bool isFree { get; set; }
        public Puzzel takenBy { get; set; }

        public Panel pnl_slice { get; set; }
        public PixelPnl puzleslice { get; set; }

        
        public PixelPnl()
        {
            isFree = true;
            Width=width;
            Height =height;
            //this.BackColor = Color.Red;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackColor = Color.DarkGray;
        }

    }
}
