using System;
using System.Collections.Generic;
using System.Drawing;
using static Tetris.Constants;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Timers;

namespace Tetris {
    [Serializable]
    public class Game {
        [NonSerialized]
        private MainForm _mainForm;
        private Rectangle _view;
        private GamePlayView _playView;
        private GameInfoView _infoView;
        private bool _gameOver;
        [NonSerialized]
        private System.Timers.Timer _gameTimer; 

        public Game() : this(null)
        {
        }

        public Game(MainForm mainForm) {
            _mainForm = mainForm;
            _view = new Rectangle();
            _playView = new GamePlayView(mainForm);
            _infoView = new GameInfoView(this);
            _gameOver = false;
            makeTimer();
            /*_gameTimer.Elapsed += gameTick;
            _gameTimer.AutoReset = true;
            _gameTimer.Start();*/
        }

        public int Score {
            get {
                return _infoView.getScore();
            }
        }

        public int Lines {
            get {
                return _infoView.getLines();
            }
        }

        public int Level {
            get {
                return _infoView.getLevel();
            }
        }

        public void makeTimer()
        {

            System.Timers.Timer createdTimer = new System.Timers.Timer();

            int interval = Convert.ToInt32(Constants.GAME_INITAIL_SPEED * Constants.ONE_SECOND_MILLIS);
            
            for (int i = 1; i < _infoView.getLevel(); i++)
            {
                interval = Convert.ToInt32(interval * Constants.GAME_LEVEL_SPEED_MULTIPLIER);
            }

            createdTimer.Interval = interval;

            _gameTimer = createdTimer;

            //test code>>>
            _gameTimer.Elapsed += gameTick;
            _gameTimer.AutoReset = true;
            _gameTimer.Start();
            //<<<
        }

        public void updateTimer(int level)
        {
            int newInterval = Convert.ToInt32(_gameTimer.Interval * Constants.GAME_LEVEL_SPEED_MULTIPLIER);
            if(newInterval >= Constants.GAME_MAX_SPEED * Constants.ONE_SECOND_MILLIS)
            {
                _gameTimer.Interval = newInterval;
            }
        }

        public void postGameTick(int tickResult)
        {
            if(tickResult == -1)
            {
                _gameTimer.Stop();
                _mainForm.gameOver();
                _gameOver = true;
            }
            else if(tickResult > 0)
            {
                _infoView.addToLine(tickResult);
            }
        }

        public void gameTick(object sender, EventArgs args) 
        {
            int result = _playView.gameTick();
            postGameTick(result);
            _mainForm.Invalidate();
        }

        public void draw(Graphics g) {
           
            g.FillRectangle(new SolidBrush(Constants.BACKGROUND_COLOR), _view);

            Rectangle modifiedView = _view.addMargin(Constants.GAME_MIN_MARGIN_AREA);   //gets the new game area with respect to margins
            modifiedView = modifiedView.resizeByAspectRatio(GAME_ASPECT_WIDTH_RATIO + INFO_ASPECT_WIDTH_RATIO, GAME_ASPECT_HEIGHT_RATIO);
            modifiedView = modifiedView.centerWithinBounds(_view);

            Tuple<Rectangle, Rectangle> splitView = modifiedView.splitAtWidth(Convert.ToInt32(modifiedView.Width * Constants.GAME_VIEW_SPLIT));
            Rectangle gameRect = splitView.Item1;
            Rectangle infoRect = splitView.Item2;

            gameRect.X -= GAME_MIN_MARGIN_AREA / 2;
            infoRect.X += GAME_MIN_MARGIN_AREA / 2;
            
            _playView.view = gameRect;
            _infoView.view = infoRect;
            _playView.draw(g);
            _infoView.addNextBlock(_playView.nextPiece);
            _infoView.draw(g);
        }

        public void rotatePiece()
        {
            if (!_gameOver && _playView.rotatePiece())
            {
                _mainForm.PlayRotateSound();
                _mainForm.Invalidate();
            }
        }

        public void movePieceRight()
        {
            if (!_gameOver)
            {
                _playView.movePieceRight();
                _mainForm.Invalidate();
            }
        }

        public void movePieceLeft()
        {
            if (!_gameOver)
            {
                _playView.movePieceLeft();
                _mainForm.Invalidate();
            }
        }

        public void movePieceDown()
        {
            if (!_gameOver)
            {
                _playView.movePieceDown();

                //test code>>>

                _infoView.addToScore(1);

                //<<<
                _mainForm.Invalidate();
                _gameTimer.Stop();
                _gameTimer.Start();
            }
        }

        public void slamPiece()
        {
            if (!_gameOver)
            {
                Tuple<int, int> result = _playView.slamPiece();
                postGameTick(result.Item1);
                _infoView.addToScore(result.Item2 * 2);
                _mainForm.Invalidate();
            }

        }

        public Rectangle view {
            get {
                return _view;
            }

            set {
                _view = value;
            }
        }//view property

        public MainForm MainForm {
            get {
                return _mainForm;
            }

            set {
                _mainForm = value;
                _playView.MainForm = value;
                _infoView.MainForm = value;
            }
        }

        public System.Timers.Timer GameTimer {
            get {
                return _gameTimer;
            }

            set {
                _gameTimer = value;
            }
        }

        public bool GameOver {
            get {
                return _gameOver;
            }

            set {
                _gameOver = value;
            }
        }

        public void addCheatLevel()
        {
            _infoView.addToLevel();
        }

    }//game class
}//tetris namespace
