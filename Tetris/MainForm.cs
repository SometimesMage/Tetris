using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris {
    public partial class MainForm : Form {

        private delegate void PauseDelegate();
        private delegate void ResumeDelegate();

        private Assembly _assembly;
        private Stream _musicStream;

        internal SoundPlayer _musicPlayer;


        private Game _game;
        private Timer _resizeTimer;

        private PauseDelegate _pauser;
        private ResumeDelegate _resumer;

        public MainForm() {

            InitializeComponent();
            DoubleBuffered = true;
            this._game = new Game(this);
          
            _pauser = _game.pauseGame;
            _resumer = _game.resumeGame;
          
            this._resizeTimer = new Timer();
            _resizeTimer.Tick += resizeTimer_Tick;

            _assembly = Assembly.GetExecutingAssembly();
            _musicStream = _assembly.GetManifestResourceStream("Tetris.Sounds.Tetris.wav");
            _musicPlayer = new SoundPlayer(_musicStream);
            _musicPlayer.PlayLooping();
        }

        private void resizeTimer_Tick(object sender, EventArgs e)
        {
            Invalidate();
            _resizeTimer.Stop();
        }

        private void mainForm_Paint(object sender, PaintEventArgs e)
        {
            Rectangle gameView = this.ClientRectangle;
            gameView.Y = mstripTop.Height;
            gameView.Height = gameView.Height - gameView.Y;

            _game.view = gameView;
            _game.draw(e.Graphics);

            if (!this.mstripPause.Enabled)
            {
                drawPause(e.Graphics);
            }
        }

        private void drawPause(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.FromArgb(180, 180, 180, 180)), ClientRectangle);
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
            if (!this.mstripPause.Enabled)
            {
                this.mstripGo.Enabled = false;
                this.mstripPause.Enabled = true;
                _resumer();
                _musicPlayer.PlayLooping();
                Invalidate();
            }
        }

        private void mstripPause_Click(object sender, EventArgs e)
        {
            //pause the timer
            //disable go
            //??enable 'game' mstrip??
            if (!this.mstripGo.Enabled)
            {
                this.mstripPause.Enabled = false;
                this.mstripGo.Enabled = true;
                _pauser();
                _musicPlayer.Stop();
                Invalidate();
            }
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //ask if they want to save?
        }

        private void mainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Right:
                    _game.movePieceRight();
                    break;
                case Keys.Left:
                    _game.movePieceLeft();
                    break;
                case Keys.Down:
                    _game.movePieceDown();
                    break;
                case Keys.Up:
                    _game.rotatePiece();
                    break;
                case Keys.Space:
                    _game.slamPiece();
                    break;
            }
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        private void mainForm_Resize(object sender, EventArgs e) {
            /*
            resizeTimer.Interval = 100; //Milliseconds
            resizeTimer.Start();*/
            Invalidate();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.mstripPause.Enabled = false;
            _pauser();
            _musicPlayer.Stop();
            Invalidate();

            MessageBox.Show("Tetris v1.0.0\n" +
                "Created by Daric Sage and Nick Peterson",
                "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
            /*
               "Licenses:\n" +
                "\"bloop1.wav\" created by Sergenious licensed under creative commons.\n" +
                "\"level up.wav\" created by Cebeeno Rossley licensed under creative commons.\n" +
                "\"jump2.wav\" created by LloydEvans09 licensed under creative commons.\n" +
                "These sounds can be found on http://freesound.org"
             */
        }

        private void controlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.mstripPause.Enabled = false;
            _pauser();
            _musicPlayer.Stop();
            Invalidate();

            MessageBox.Show("Left Arrow: Moves game piece left.\n" +
                "Right Arrow: Moves game piece right.\n" +
                "Down Arrow: Moves game piece down one block.\n" +
                "Up Arrow: Rotates game piece counter-clockwise.\n" +
                "Space: Slams game piece.\n" +
                "Home: Increases level by 1. (Cheat)",
                "Controls", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }//form
}//namespace tetris
