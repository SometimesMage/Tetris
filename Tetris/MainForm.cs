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
using System.Windows.Media;

namespace Tetris {
    public partial class MainForm : Form {

        private delegate void PauseDelegate();
        private delegate void ResumeDelegate();
        private delegate void SoundCallbackDelegate();

        private MediaPlayer _musicPlayer;
        private MediaPlayer _pausePlayer;
        private MediaPlayer _unpausePlayer;
        private MediaPlayer _rotatePlayer;
        private MediaPlayer _blockPlayer;
        private MediaPlayer _slamPlayer;

        private Game _game;
        private Timer _resizeTimer;

        private PauseDelegate _pauser;
        private ResumeDelegate _resumer;

        private bool _gameover;

        public MainForm() {

            InitializeComponent();
            DoubleBuffered = true;
            this._game = new Game(this);
          
            _pauser = _game.pauseGame;
            _resumer = _game.resumeGame;
          
            this._resizeTimer = new Timer();
            _resizeTimer.Tick += resizeTimer_Tick;

            _musicPlayer = new MediaPlayer();
            _pausePlayer = new MediaPlayer();
            _unpausePlayer = new MediaPlayer();
            _rotatePlayer = new MediaPlayer();
            _blockPlayer = new MediaPlayer();
            _slamPlayer = new MediaPlayer();
            _musicPlayer.Open(new Uri(@"Sounds\Tetris.wav", UriKind.Relative));
            _pausePlayer.Open(new Uri(@"Sounds\pause.wav", UriKind.Relative));
            _unpausePlayer.Open(new Uri(@"Sounds\unpause.wav", UriKind.Relative));
            _rotatePlayer.Open(new Uri(@"Sounds\rotate.wav", UriKind.Relative));
            _blockPlayer.Open(new Uri(@"Sounds\block.wav", UriKind.Relative));
            _slamPlayer.Open(new Uri(@"Sounds\slam.wav", UriKind.Relative));

            _musicPlayer.MediaEnded += _musicPlayer_MediaEnded;
            _pausePlayer.MediaEnded += mediaEnded;
            _unpausePlayer.MediaEnded += mediaEnded;
            _rotatePlayer.MediaEnded += mediaEnded;
            _blockPlayer.MediaEnded += mediaEnded;
            _slamPlayer.MediaEnded += mediaEnded;

            _musicPlayer.Volume = 0.3;
            _musicPlayer.Play();
        }

        public void PlayRotateSound()
        {
            if (mstripTop.InvokeRequired)
            {
                SoundCallbackDelegate d = new SoundCallbackDelegate(PlayBlockSound);
                this.Invoke(d);
            }
            else
            {
                _rotatePlayer.Play();
            }
        }

        public void PlayBlockSound()
        {
            if (mstripTop.InvokeRequired)
            {
                SoundCallbackDelegate d = new SoundCallbackDelegate(PlayBlockSound);
                this.Invoke(d);
            }
            else
            {
                _blockPlayer.Play();
            }
        }

        public void PlaySlamSound()
        {
            if (mstripTop.InvokeRequired)
            {
                SoundCallbackDelegate d = new SoundCallbackDelegate(PlayBlockSound);
                this.Invoke(d);
            }
            else
            {
                _slamPlayer.Play();
            }
        }

        public void gameOver()
        {
            this.mstripPause.Enabled = false;
            this.mstripGo.Enabled = false;
            this._gameover = true;
            Invalidate();
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
            g.FillRectangle(new SolidBrush(System.Drawing.Color.FromArgb(180, 180, 180, 180)), ClientRectangle);
            string text;
            if(_gameover)
            {
                text = "Game Over";
            }
            else
            {
                text = "Paused";
            }

            Tuple<Font, SizeF> tuple = ClientRectangle.adjustedFont(new Font(Constants.DEFAULT_FONT_TYPE, Constants.LARGEST_FONT_SIZE), text, g);
            Rectangle point = new Rectangle(0, 0, Convert.ToInt32(tuple.Item2.Width), Convert.ToInt32(tuple.Item2.Height)).centerWithinBounds(ClientRectangle);
            g.DrawString(text, tuple.Item1, System.Drawing.Brushes.Black, new PointF(point.X, point.Y));
        }

        private void mstripNew_Click(object sender, EventArgs e)
        {
            //start game and setup
            //??disable the 'game' mstrip??
            //enable 'pause' mstrip
        }

        private void mstripSave_Click(object sender, EventArgs e)
        {
            this.mstripPause_Click(sender, e);
            this.saveFileDialog.ShowDialog();

        }

        private void mstripLoad_Click(object sender, EventArgs e)
        {
            this.mstripPause_Click(sender, e);
            this.openFileDialog.ShowDialog();
        }

        private void mstripExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mstripGo_Click(object sender, EventArgs e)
        {
            if (!this.mstripPause.Enabled)
            {
                this.mstripGo.Enabled = false;
                this.mstripPause.Enabled = true;
                _resumer();
                _unpausePlayer.Play();
                _musicPlayer.Play();
                Invalidate();
            }
        }

        private void mstripPause_Click(object sender, EventArgs e)
        {
            if (!this.mstripGo.Enabled)
            {
                this.mstripPause.Enabled = false;
                this.mstripGo.Enabled = true;
                _pauser();
                _musicPlayer.Pause();
                _pausePlayer.Play();
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

        private void resizeTimer_Tick(object sender, EventArgs e)
        {
            Invalidate();
            _resizeTimer.Stop();
        }

        private void mainForm_Resize(object sender, EventArgs e) {
            /*
            resizeTimer.Interval = 100; //Milliseconds
            resizeTimer.Start();*/
            Invalidate();
        }

        private void _musicPlayer_MediaEnded(object sender, EventArgs e)
        {
            _musicPlayer.Position = TimeSpan.Zero;
        }

        private void mediaEnded(object sender, EventArgs e)
        {
            MediaPlayer player = sender as MediaPlayer;
            player.Position = TimeSpan.Zero;
            player.Stop();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.mstripPause_Click(sender, e);

            MessageBox.Show("Tetris v1.0.0\n" +
                "Created by Daric Sage and Nick Peterson\n\n" +
                "Licenses:\n" +
                "\"bloop1.wav\" created by Sergenious licensed under creative commons.\n" +
                "\"level up.wav\" created by Cebeeno Rossley licensed under creative commons.\n" +
                "\"jump2.wav\" created by LloydEvans09 licensed under creative commons.\n" +
                "These sounds can be found on http://freesound.org",
                "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void controlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.mstripPause_Click(sender, e);

            MessageBox.Show("Left Arrow: Moves game piece left.\n" +
                "Right Arrow: Moves game piece right.\n" +
                "Down Arrow: Moves game piece down one block.\n" +
                "Up Arrow: Rotates game piece counter-clockwise.\n" +
                "Space: Slams game piece.\n" +
                "Home: Increases level by 1. (Cheat)",
                "Controls", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Dispose stuff
        }
    }//form
}//namespace tetris
