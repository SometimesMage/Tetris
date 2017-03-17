using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris {
    public partial class MainForm : Form {

        private delegate void PauseDelegate();
        private delegate void ResumeDelegate();
      
        private Game game;
        private Timer resizeTimer;

        private Game game;
        private PauseDelegate pauser;
        private ResumeDelegate resumer;

        public MainForm() {

            InitializeComponent();
            DoubleBuffered = true;
            this.game = new Game();
          
            pauser = game.pauseGame;
            pauser += drawPause;
          
            this.resizeTimer = new Timer();
            resizeTimer.Tick += resizeTimer_Tick;
        }

        private void resizeTimer_Tick(object sender, EventArgs e)
        {
            Invalidate();
            resizeTimer.Stop();
        }

        private void mainForm_Paint(object sender, PaintEventArgs e)
        {
            Rectangle gameView = this.ClientRectangle;
            gameView.Y = mstripTop.Height;
            gameView.Height = gameView.Height - gameView.Y;

            game.view = gameView;
            game.draw(e.Graphics);

            if (!this.mstripPause.Enabled)
            {
                drawPause();
            }
        }

        private void drawPause()
        {
            using (Graphics g = CreateGraphics())
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(180, 180, 180, 180)), ClientRectangle);
            }
            //TODO doesnt stay when trying to resize
        }

        private void mstripNew_Click(object sender, EventArgs e)
        {
            //start game and setup
            //??disable the 'game' mstrip??
            //enable 'pause' mstrip
        }

        private void mstripSave_Click(object sender, EventArgs e)
        {
            this.saveFileDialog.ShowDialog();
        }

        private void mstripLoad_Click(object sender, EventArgs e)
        {
            this.openFileDialog.ShowDialog();
        }

        private void mstripExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mstripGo_Click(object sender, EventArgs e)
        {
            //resume timer
            //disable pause
            //??disable 'game' mstrip??
            this.mstripPause.Enabled = true;
            Invalidate();
        }

        private void mstripPause_Click(object sender, EventArgs e)
        {
            //pause the timer
            //disable go
            //??enable 'game' mstrip??
            this.mstripPause.Enabled = false;
            pauser();
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //ask if they want to save?
        }

        private void mainForm_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void mainForm_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void mainForm_Resize(object sender, EventArgs e) {
            resizeTimer.Interval = 100; //Milliseconds
            resizeTimer.Start();
        }


    }//form
}//namespace tetris
