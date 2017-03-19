using System;
using System.Collections.Generic;
using System.Drawing;
using static Tetris.Constants;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris {
    public class Game {
        private MainForm _mainForm;
        private Rectangle _view;
        private GamePlayView _playView;
        private GameInfoView _infoView;
        private System.Timers.Timer _gameTimer; 

        public Game(MainForm mainForm) {
            _mainForm = mainForm;
            _view = new Rectangle();
            _playView = new GamePlayView(mainForm);
            _infoView = new GameInfoView();

            _gameTimer = new System.Timers.Timer();
            _gameTimer.Interval = Convert.ToInt32(Constants.GAME_INITAIL_SPEED * 1000);
            _gameTimer.Elapsed += gameTick;
            _gameTimer.AutoReset = true;
            _gameTimer.Start();
        }

        public void postGameTick(int tickResult)
        {
            if(tickResult == -1)
            {
                _gameTimer.Stop();
                _mainForm.gameOver();
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
            _infoView.draw(g);
        }

        public void pauseGame()
        {
            _gameTimer.Stop();
        }

        public void resumeGame()
        {
            _gameTimer.Start();
        }

        public void rotatePiece()
        {
            if (_playView.rotatePiece())
            {
                _mainForm.PlayRotateSound();
                _mainForm.Invalidate();
            }
        }

        public void movePieceRight()
        {
            _playView.movePieceRight();
            _mainForm.Invalidate();
        }

        public void movePieceLeft()
        {
            _playView.movePieceLeft();
            _mainForm.Invalidate();
        }

        public void movePieceDown()
        {
            _playView.movePieceDown();
            _mainForm.Invalidate();
            _gameTimer.Stop();
            _gameTimer.Start();
        }

        public void slamPiece()
        {
            int result = _playView.slamPiece();
            postGameTick(result);
            _mainForm.Invalidate();

        }

        public Rectangle view {
            get {
                return _view;
            }

            set {
                _view = value;
            }
        }//view property
    }//game class
}//tetris namespace
