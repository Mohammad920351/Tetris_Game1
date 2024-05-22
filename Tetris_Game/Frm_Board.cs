using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using WMPLib;


//modify RotatePuzzle() and CheckForSto() in PuzzleSlice.cs
namespace Tetris_Game
{
    public partial class Frm_Board : Form
    {
        public Puzzel _currentPuzzel;
        public List<Puzzel> _puzzels = new List<Puzzel>();
        public BoardGame _board;

        public Puzzel _nextPuzzle;
        public BoardGame _nextShowBoard;
        public bool Pause { get; set; }

        public WindowsMediaPlayer sound_Game { get; set; }

        public WindowsMediaPlayer sound_DelRow { get; set; }
        public WindowsMediaPlayer sound_PzlRotate { get; set; }
        public WindowsMediaPlayer sound_PzlMoveLR { get; set; }

        public Frm_Board()
        {




            InitializeComponent();
            
            ///Reset Game
            Score = 0;
            Pause = false;
            lbl_score.Text = "0";


            ///
            new Thread(() =>
            {
                sound_Game = new WindowsMediaPlayer();
                //sound_Game.URL = @"C:\Users\Mr PC\Music\music-game-action--3(tabanmusic.com).mp3";
                //sound_Game.URL = @"C:\Users\Mr PC\source\repos\Tetris_Game\Tetris_Game\Resources\Tetris.mp3";
                sound_Game.URL = @"D:\c#\Tetris_Game\Tetris_Game\Resources\fun_level_(underscore)_proud_music_preview.mp3";
            
                sound_Game.settings.setMode("loop", true);
                //sound_Game.settings.rate = 1;

                //axMusicPlayer.controls.play();
                

                sound_Game.controls.play();
            }).Start();
            //Sound_Play(sound_Game, @"C:\Users\Mr PC\Music\music-game-action--3(tabanmusic.com).mp3");


            ///draw Game panel
            BoardGame boardGame = new BoardGame(pnl_GameSpace);
            boardGame.DrawingPanel();

            _board = boardGame;

            ///draw ShowNext panel
            BoardGame board_showNext = new BoardGame(pnl_ShowNext);
            board_showNext.DrawingPanel();
            _nextShowBoard = board_showNext;

            CreateNewPuzzle();
            AddPzlToMainBoard();
            CreateNewPuzzle();

            timer1.Enabled = true;
            
        }
        private void AddPzlToMainBoard()
        {
            _nextPuzzle.MemoryPnlSlice.Clear();
            _currentPuzzel = _nextPuzzle;
            _nextPuzzle = null;
            _puzzels.Add(_currentPuzzel);
            _currentPuzzel.boardGame = _board;

        }
        public void CreateNewPuzzle()
        {
            Random rnd = new Random();
            string model = (rnd.Next(1, 6).ToString());
            Puzzel puzzel = CreatePuzzel.CreatePuzzle(model, _nextShowBoard);           
            _nextPuzzle= puzzel;
            _nextShowBoard.pnl_SliceList.ToList().ForEach(pnlpix =>
            {
                pnlpix.Controls.Clear();

                pnlpix.isFree = true;
                pnlpix.takenBy = null;

            });
            _nextPuzzle.DrowPuzell(1,-4);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Pause == true) return;
           
            if (_puzzels.Any(p => p.isStoped == false) == false)
            {

                AddPzlToMainBoard();
                CreateNewPuzzle();

            }

            foreach (var item in _puzzels)
            {
                item.CheckForStop();
                if (item.isStoped == false)
                {
                    item.Move('D');

                    item.DrowPuzell();
                }
               
            }
            if(_puzzels.All(pzl=> pzl.isStoped == true))
            {
                this.CheckForDelRow();

            }


        }
       

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

      
        public int KeyCouter = 0;
        public Keys lastKey;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == lastKey) KeyCouter++;
            else KeyCouter = 0;
            lastKey = keyData;
            
            if (true)

            {
                if (KeyCouter > 3)
                {
                    KeyCouter = 2;
                    Thread.Sleep(10);
                }

                Sound_Play(sound_PzlMoveLR, @"C:\Users\Mr PC\Music\game effect\mediabaz.net-Mouse-Click-00-m.mp3");

                char Dir = 'U';
                switch (keyData)
                {
                    case Keys.Down:
                        Dir = 'D';
                      
                        break;
                    case Keys.Right:
                        Dir = 'R';                        

                        break;

                    case Keys.Left:
                       
                        Dir = 'L';                      
                       
                        break;
                    case Keys.A:
                        foreach (var puzzle in _puzzels)
                        {
                            if (puzzle.isStoped == false)
                            {
                                Sound_Play(sound_PzlRotate, @"C:\Users\Mr PC\Music\game effect\MediaBaz.net-Notifications and Buttons -04.mp3");

                                puzzle.RotatePuzzle();
                                puzzle.DrowPuzell();
                            }

                        }
                        break;
                        
                    case Keys.Space:
                        if (Pause == false) { Pause = true; sound_Game.controls.pause(); }
                        else if (Pause == true) { Pause = false; sound_Game.controls.play(); }

                        break;

                    default:
                        break;

                }

                foreach (var item in _puzzels)
                {
                    item.Move(Dir);
                    item.DrowPuzell();

                }
            }
            //else if (keyData == Keys.A)
            //{
               
            //}
            

            return base.ProcessCmdKey(ref msg, keyData);
        }
        public void CheckForDelRow()
        {
            int rowCount_Del = 0;
            
            for (int row = 0; row <= _board.RowCount - 1; row++)
            {
                bool AllowDelRow = true;

                if (_board.pnl_SliceList.Any(pix => pix.row == row && pix.isFree == true))
                {
                    //textBox1.Text = "not yet";

                    AllowDelRow = false;
                }
                if (AllowDelRow == true) 
                {
                    rowCount_Del++;
                    Sound_Play(sound_DelRow, @"C:\Users\Mr PC\Music\game effect\MediaBaz.net-Notifications and Buttons -02.mp3");
                    
                    ///delete row
                    _board.pnl_SliceList.Where(pix => pix.row == row).ToList()
                            .ForEach(pix =>
                            {
                                pix.takenBy.PzlSlicesList.Where(pzlpix => pzlpix.row == row).ToList()
                                .ForEach(pzlpix => pix.takenBy.PzlSlicesList.Remove(pzlpix));
                                pix.isFree = true;
                                pix.takenBy = null;
                                pix.Controls.Clear();

                            });

                    _puzzels.Where(p => p.isStoped == true).SelectMany(p => p.PzlSlicesList).Where(pzlpix => pzlpix.row < row).ToList()
                    .ForEach(pzlpix =>
                    {
                   // while (_board.pnl_SliceList.All(pix => pix.row == pzlpix.row + 1 && pix.column == pzlpix.column && pzlpix)
                   //)
                   // {
                        pzlpix.row++;
                        
                        //}
                    });
                    _puzzels.Where(p => p.isStoped == true ).ToList()
                        .ForEach(p => 
                        {
                            p.DrowPuzell();
                        });
                }
            }
            for (int count=0; count <= rowCount_Del-1; count++)
            {
                GameScore(count);

            }
        }
        public void Sound_Play(WindowsMediaPlayer sound,string path)
        {
            new System.Threading.Thread(() => {
                this.Invoke(new Action(() => {
                    sound = new WindowsMediaPlayer();
                    sound.URL = path;

                    sound.controls.play();
                    //Thread.Sleep(100);
                }));


            }).Start();
        }
        public int Score { get; set; }
        public decimal b=1;
        public void GameScore(int Rowcount_forDel)
        {
            int newscore = (int)Math.Floor((100 *(double) Math.Pow((double)(1 + (decimal)1 / 2), Rowcount_forDel)));

            Score += newscore;
            lbl_score.Text = Score.ToString();
        }
        private void Frm_Board_Leave(object sender, EventArgs e)
        {
            sound_Game.controls.stop();
        }

        private void Frm_Board_Layout(object sender, LayoutEventArgs e)
        {
            

        }
    }
}
