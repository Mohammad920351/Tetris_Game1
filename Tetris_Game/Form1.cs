using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris_Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            Frm_Board frm_Board = new Frm_Board();
            frm_Board.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.BackgroundImage = Properties.Resources.bg2;
            //int newscore = (int)Math.Floor((100 *(double) Math.Pow((double)(1 + (double)1 / 2), 2)));
            //double newscore1 =  Math.Pow((double)(1 + (double)(1 / 2)), 2);
            //decimal newscore2 =  (decimal) 1 / 2;

            //textBox1.Text = newscore.ToString()+" : "+newscore1.ToString()+":"+newscore2.ToString();
            textBox1.Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
