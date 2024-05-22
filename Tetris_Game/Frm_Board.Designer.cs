namespace Tetris_Game
{
    partial class Frm_Board
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnl_GameSpace = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnl_ShowNext = new System.Windows.Forms.Panel();
            this.pnl_Score = new System.Windows.Forms.Panel();
            this.lbl_score = new System.Windows.Forms.Label();
            this.pnl_Score.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_GameSpace
            // 
            this.pnl_GameSpace.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnl_GameSpace.Location = new System.Drawing.Point(237, 52);
            this.pnl_GameSpace.Name = "pnl_GameSpace";
            this.pnl_GameSpace.Size = new System.Drawing.Size(548, 546);
            this.pnl_GameSpace.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pnl_ShowNext
            // 
            this.pnl_ShowNext.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnl_ShowNext.Location = new System.Drawing.Point(811, 52);
            this.pnl_ShowNext.Name = "pnl_ShowNext";
            this.pnl_ShowNext.Size = new System.Drawing.Size(245, 208);
            this.pnl_ShowNext.TabIndex = 1;
            // 
            // pnl_Score
            // 
            this.pnl_Score.Controls.Add(this.lbl_score);
            this.pnl_Score.Location = new System.Drawing.Point(50, 52);
            this.pnl_Score.Name = "pnl_Score";
            this.pnl_Score.Size = new System.Drawing.Size(128, 100);
            this.pnl_Score.TabIndex = 0;
            // 
            // lbl_score
            // 
            this.lbl_score.AutoSize = true;
            this.lbl_score.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lbl_score.ForeColor = System.Drawing.Color.Green;
            this.lbl_score.Location = new System.Drawing.Point(27, 35);
            this.lbl_score.Name = "lbl_score";
            this.lbl_score.Size = new System.Drawing.Size(0, 25);
            this.lbl_score.TabIndex = 2;
            // 
            // Frm_Board
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Tetris_Game.Properties.Resources.bg3;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1110, 610);
            this.Controls.Add(this.pnl_Score);
            this.Controls.Add(this.pnl_ShowNext);
            this.Controls.Add(this.pnl_GameSpace);
            this.Name = "Frm_Board";
            this.Text = "Frm_Board";
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.Frm_Board_Layout);
            this.Leave += new System.EventHandler(this.Frm_Board_Leave);
            this.pnl_Score.ResumeLayout(false);
            this.pnl_Score.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_GameSpace;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel pnl_ShowNext;
        private System.Windows.Forms.Panel pnl_Score;
        private System.Windows.Forms.Label lbl_score;
    }
}