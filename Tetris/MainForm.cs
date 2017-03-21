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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Xml;
using System.Xml.Serialization;

namespace Tetris {
    //Created by Nick Peterson and Daric Sage
    //Features for Possible Extra Credit Include:
    //Dynamically resizable
    //Ghost Game Piece
    public partial class MainForm : Form {

        private delegate void SoundCallbackDelegate();

        private MediaPlayer _musicPlayer;
        private MediaPlayer _pausePlayer;
        private MediaPlayer _unpausePlayer;
        private MediaPlayer _rotatePlayer;
        private MediaPlayer _blockPlayer;
        private MediaPlayer _slamPlayer;
        private MediaPlayer _levelUpPlayer;
        private MediaPlayer _gameOverPlayer;
        private MediaPlayer _linePlayer;

        private Game _game;
        private Timer _resizeTimer;

        private bool _gameover;
        private int _highscore;

        public MainForm() {

            InitializeComponent();
            DoubleBuffered = true;
            this._game = new Game(this);
            mstripGo.Enabled = true;
            mstripPause.Enabled = false;
            _game.GameTimer.Stop();
          
            this._resizeTimer = new Timer();
            _resizeTimer.Tick += resizeTimer_Tick;

            _musicPlayer = new MediaPlayer();
            _pausePlayer = new MediaPlayer();
            _unpausePlayer = new MediaPlayer();
            _rotatePlayer = new MediaPlayer();
            _blockPlayer = new MediaPlayer();
            _slamPlayer = new MediaPlayer();
            _levelUpPlayer = new MediaPlayer();
            _gameOverPlayer = new MediaPlayer();
            _linePlayer = new MediaPlayer();
            _musicPlayer.Open(new Uri(@"Sounds\Tetris.wav", UriKind.Relative));
            _pausePlayer.Open(new Uri(@"Sounds\pause.wav", UriKind.Relative));
            _unpausePlayer.Open(new Uri(@"Sounds\unpause.wav", UriKind.Relative));
            _rotatePlayer.Open(new Uri(@"Sounds\rotate.wav", UriKind.Relative));
            _blockPlayer.Open(new Uri(@"Sounds\block.wav", UriKind.Relative));
            _slamPlayer.Open(new Uri(@"Sounds\slam.wav", UriKind.Relative));
            _levelUpPlayer.Open(new Uri(@"Sounds\level-up.wav", UriKind.Relative));
            _gameOverPlayer.Open(new Uri(@"Sounds\game-over.wav", UriKind.Relative));
            _linePlayer.Open(new Uri(@"Sounds\line.wav", UriKind.Relative));

            _musicPlayer.MediaEnded += _musicPlayer_MediaEnded;
            _pausePlayer.MediaEnded += mediaEnded;
            _unpausePlayer.MediaEnded += mediaEnded;
            _rotatePlayer.MediaEnded += mediaEnded;
            _blockPlayer.MediaEnded += mediaEnded;
            _slamPlayer.MediaEnded += mediaEnded;
            _levelUpPlayer.MediaEnded += mediaEnded;
            _gameOverPlayer.MediaEnded += mediaEnded;
            _linePlayer.MediaEnded += mediaEnded;

            _musicPlayer.Volume = 0.2;
            _linePlayer.Volume = 0.3;
            _levelUpPlayer.Volume = 1.0;

            _musicPlayer.Play();

            //Get Highscore
            if(File.Exists("highscore.txt"))
            {
                StreamReader reader = new StreamReader(new FileStream("highscore.txt", FileMode.Open));
                _highscore = Convert.ToInt32(reader.ReadLine());
                reader.Close();
                reader.Dispose();
            }

        }

        public void StopMusic()
        {
            if (mstripTop.InvokeRequired)
            {
                SoundCallbackDelegate d = new SoundCallbackDelegate(StopMusic);
                this.Invoke(d);
            }
            else
            {
                _musicPlayer.Stop();
            }
        }

        public void PlayGameOverSound()
        {
            if (mstripTop.InvokeRequired)
            {
                SoundCallbackDelegate d = new SoundCallbackDelegate(PlayGameOverSound);
                this.Invoke(d);
            }
            else
            {
                _gameOverPlayer.Play();
            }
        }

        public void PlayRotateSound()
        {
            if (mstripTop.InvokeRequired)
            {
                SoundCallbackDelegate d = new SoundCallbackDelegate(PlayRotateSound);
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
                SoundCallbackDelegate d = new SoundCallbackDelegate(PlaySlamSound);
                this.Invoke(d);
            }
            else
            {
                _slamPlayer.Play();
            }
        }

        public void PlayLeveUpSound()
        {
            if (mstripTop.InvokeRequired)
            {
                SoundCallbackDelegate d = new SoundCallbackDelegate(PlayLeveUpSound);
                this.Invoke(d);
            }
            else
            {
                _levelUpPlayer.Play();
            }
        }

        public void PlayLineSound()
        {
            if (mstripTop.InvokeRequired)
            {
                SoundCallbackDelegate d = new SoundCallbackDelegate(PlayLineSound);
                this.Invoke(d);
            }
            else
            {
                _linePlayer.Play();
            }
        }

        public void gameOver()
        {
            this.mstripPause.Enabled = false;
            this.mstripGo.Enabled = false;
            this._gameover = true;
            StopMusic();
            PlayGameOverSound();
            Invalidate();
            

            if(_game.Score > _highscore)
            {
                MessageBox.Show("Highscore!!!!\n" 
                    + "Score: " + _game.Score + "\n"
                    + "Level: " + _game.Level, "Game Over");
                _highscore = _game.Score;
                StreamWriter writer = new StreamWriter(new FileStream("highscore.txt", FileMode.Create));
                writer.WriteLine(Convert.ToString(_highscore));
                writer.Flush();
                writer.Close();
                writer.Dispose();
            }
            else
            {
                MessageBox.Show("Score: " + _game.Score + "\n"
                     + "Level: " + _game.Level, "Game Over");
            }
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
            _game = new Game(this);
            mstripPause.Enabled = true;
            mstripGo.Enabled = false;
            _musicPlayer.Play();
            _gameover = false;
        }

        private void mstripSave_Click(object sender, EventArgs e)
        {
            this.mstripPause_Click(sender, e);
            DialogResult result = this.saveFileDialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, _game);
                stream.Close();
            }
        }

        private void mstripLoad_Click(object sender, EventArgs e)
        {
            this.mstripPause_Click(sender, e);
            DialogResult result = this.openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                _game = (Game) formatter.Deserialize(stream);
                _game.MainForm = this;
                _game.makeTimer();
                _game.GameTimer.Stop();
                _gameover = _game.GameOver;
                if(_gameover)
                {
                    mstripPause.Enabled = false;
                    mstripGo.Enabled = false;
                }
                Invalidate();
                stream.Close();
            }
        }

        private void mstripExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mstripGo_Click(object sender, EventArgs e)
        {
            if(_gameover)
            {
                this.mstripGo.Enabled = false;
            }

            if (!this.mstripPause.Enabled && !_gameover)
            {
                this.mstripGo.Enabled = false;
                this.mstripPause.Enabled = true;
                _game.GameTimer.Start();
                _unpausePlayer.Play();
                _musicPlayer.Play();
                Invalidate();
            }
        }

        private void mstripPause_Click(object sender, EventArgs e)
        {
            if(_gameover)
            {
                this.mstripPause.Enabled = false;
            }

            if (!this.mstripGo.Enabled && !_gameover)
            {
                this.mstripPause.Enabled = false;
                this.mstripGo.Enabled = true;
                _game.GameTimer.Stop();
                _musicPlayer.Pause();
                _pausePlayer.Play();
                Invalidate();
            }
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mstripPause_Click(sender, e);
            DialogResult result = MessageBox.Show("Do you want to save?", "Exiting...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
            if(result == DialogResult.Cancel)
            {
                e.Cancel = true;
            } else if(result == DialogResult.Yes)
            {
                mstripSave_Click(sender, e);
            }
        }

        private void mainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!mstripPause.Enabled)
                return;
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
                case Keys.Home:
                    _game.addCheatLevel();
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
                "Created by Daric Sage and Nick Peterson\n" +
                "Highscore: " + _highscore + "\n\n" +
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
            //Dispose Stuff
        }
    }//form
}//namespace tetris
