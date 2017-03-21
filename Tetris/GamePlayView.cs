using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Tetris
{
    //Created by Nick Peterson and Daric Sage
    //Features for Possible Extra Credit Include:
    //Dynamically resizable
    //Ghost Game Piece
    [Serializable]
    public class GamePlayView
    {

        private Rectangle _view;
        private List<GameBlock> _blocks;
        private GamePiece _gamePiece;
        [NonSerialized]
        private MainForm _mainForm;
        private Random _random;
        private int _seed;
        private GamePieces _nextPiece;

        public GamePlayView() : this(null) { }

        public GamePlayView(MainForm mainForm, Rectangle view = new Rectangle())
        {
            _view = view;
            _mainForm = mainForm;
            _seed = DateTime.Now.Millisecond;
            _random = new Random(_seed);
            _blocks = new List<GameBlock>(Constants.GRID_WIDITH * Constants.GRID_HEIGHT);
            GamePieces pieceType = (GamePieces) Enum.GetValues(typeof(GamePieces)).GetValue(_random.Next(0, 7));
            _gamePiece = GamePieceFactory.Instance.createGamePiece(pieceType);
            genNextPiece();
        }

        private GamePieces genNextPiece()
        {
            GamePieces pieceType = (GamePieces)Enum.GetValues(typeof(GamePieces)).GetValue(_random.Next(0, 7));
            GamePieces temp = _nextPiece;
            _nextPiece = pieceType;
            return temp;
        }

        public GamePieces nextPiece 
        {
            get {
                return _nextPiece;
            }
        }

        public Rectangle view
        {
            get { return _view; }

            set { _view = value; }
        }

        public MainForm MainForm {
            get {
                return _mainForm;
            }

            set {
                _mainForm = value;
            }
        }

        public int gameTick(bool slam = false)
        {
            if (_gamePiece.canMoveDown(_blocks))
            {
                _gamePiece.moveDown();
            }
            else
            {
                if(!slam)
                    _mainForm.PlayBlockSound();
                _blocks.AddRange(_gamePiece.getBlocks());
                _gamePiece = GamePieceFactory.Instance.createGamePiece(genNextPiece());

                //Line Complete Dectection
                int lines = 0;
                for(int i = 0; i < Constants.GRID_HEIGHT; i++)
                {
                    var row = from block in _blocks where block.location.Y == i select block;
                    if(row.Count() == Constants.GRID_WIDITH)
                    {
                        lines++;
                        _blocks.RemoveAll(block => block.location.Y == i);
                        _blocks.ForEach(block =>
                        {
                            if (block.location.Y < i)
                                block.moveDown();
                        });
                    }
                }

                //Game Over Dectection
                bool gameover = _blocks.Any(block => block.location.Y < 0);
                if (gameover)
                {
                    return -1;
                }

                return lines;
            }
            return 0;
        }

        public void draw(Graphics g)
        {
            //Draw Background
            LinearGradientBrush brush = new LinearGradientBrush(_view, Constants.GAME_BACKGROUND_COLOR, Constants.GAME_BACKGROUND_COLOR_2, 0.0f);
            g.FillRectangle(brush, _view);

            int blockWidth = _view.Width / Constants.GRID_WIDITH;
            int blockHeight = _view.Height / Constants.GRID_HEIGHT;
            int centerX = (_view.Width - blockWidth * Constants.GRID_WIDITH) / 2 + _view.X;
            int centerY = (_view.Height - blockHeight * Constants.GRID_HEIGHT) / 2 + _view.Y;

            //Draw Grid
            /*Pen pen = new Pen(Color.Blue);
            for(int x = 0; x <= Constants.GRID_WIDITH; x++) {
                int lineX = centerX + (x * blockWidth);
                g.DrawLine(pen, lineX, centerY, lineX, centerY + (blockHeight * Constants.GRID_HEIGHT));
            }

            for(int y = 0; y <= Constants.GRID_HEIGHT; y++) {
                int lineY = centerY + (y * blockHeight);
                g.DrawLine(pen, centerX, lineY, centerX + (blockWidth * Constants.GRID_WIDITH), lineY);
            }*/

            //Resize and Reposition GameBlocks
            _blocks.ForEach(block =>
            {
                if (block.location.Y < 0)
                    return;
                var bounds = block.bounds;
                bounds.Width = blockWidth;
                bounds.Height = blockHeight;
                bounds.X = block.location.X * blockWidth + centerX;
                bounds.Y = block.location.Y * blockHeight + centerY;
                block.bounds = bounds;
                block.draw(g, false);
            });

            //Draw Ghost Game Piece
            GamePiece ghost = _gamePiece.createGhostPiece(_blocks);
            ghost.getBlocks().ForEach(block =>
            {
                if (block.location.Y < 0)
                    return;

                var bounds = block.bounds;
                bounds.Width = blockWidth;
                bounds.Height = blockHeight;
                bounds.X = block.location.X * blockWidth + centerX;
                bounds.Y = block.location.Y * blockHeight + centerY;
                block.bounds = bounds;
                block.draw(g, true);
            });

            //Draw Game Piece
            _gamePiece.getBlocks().ForEach(block =>
            {
                if (block.location.Y < 0)
                    return;

                var bounds = block.bounds;
                bounds.Width = blockWidth;
                bounds.Height = blockHeight;
                bounds.X = block.location.X * blockWidth + centerX;
                bounds.Y = block.location.Y * blockHeight + centerY;
                block.bounds = bounds;
                block.draw(g, false);
            });
        }//draw method

        public void movePieceRight()
        {
            if(_gamePiece.canMoveRight(_blocks))
            {
                _gamePiece.moveRight();
            }
        }

        public void movePieceLeft()
        {
            if(_gamePiece.canMoveLeft(_blocks))
            {
                _gamePiece.moveLeft();
            }
        }

        public void movePieceDown()
        {
            gameTick();
        }

        public Tuple<int, int> slamPiece()
        {
            //TODO scoring with regards to slamming
            int slamLines = 0;
            while (_gamePiece.canMoveDown(_blocks))
            {
                _gamePiece.moveDown();
                slamLines++;
            }

            _mainForm.PlaySlamSound();
            int lines = gameTick(true);
            return new Tuple<int, int>(lines, slamLines);
        }

        public bool rotatePiece()
        {
            if (_gamePiece.canRotate(_blocks))
            {
                _gamePiece.rotate();
                return true;
            }

            return false;
        }

    }//gameplayview class
}//tetris namespace
