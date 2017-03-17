using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetris
{
    public class GamePlayView
    {

        private Rectangle _view;
        private List<GameBlock> _blocks;
        private GamePiece _gamePiece;

        public GamePlayView(Rectangle view = new Rectangle())
        {
            _view = view;
            _blocks = new List<GameBlock>(Constants.GRID_WIDITH * Constants.GRID_HEIGHT);
            _gamePiece = GamePieceFactory.Instance.createGamePiece(GamePieces.L_RIGHT);
        }

        public Rectangle view
        {
            get { return _view; }

            set { _view = value; }
        }

        public void gameTick()
        {
            if (_gamePiece.canMoveDown(_blocks))
            {
                _gamePiece.moveDown();
            }
            else
            {
                _blocks.AddRange(_gamePiece.getBlocks());
                _gamePiece = GamePieceFactory.Instance.createGamePiece(GamePieces.L_RIGHT);
            }
            //TODO check if a line is complete
        }

        public void draw(Graphics g)
        {
            //Draw Background
            g.FillRectangle(new SolidBrush(Constants.GAME_BACKGROUND_COLOR), _view);

            int blockWidth = _view.Width / Constants.GRID_WIDITH;
            int blockHeight = _view.Height / Constants.GRID_HEIGHT;
            int centerX = (_view.Width - blockWidth * Constants.GRID_WIDITH) / 2 + _view.X;
            int centerY = (_view.Height - blockHeight * Constants.GRID_HEIGHT) / 2 + _view.Y;

            //Draw Grid
            Pen pen = new Pen(Color.Blue);
            for(int x = 0; x <= Constants.GRID_WIDITH; x++) {
                int lineX = centerX + (x * blockWidth);
                g.DrawLine(pen, lineX, centerY, lineX, centerY + (blockHeight * Constants.GRID_HEIGHT));
            }

            for(int y = 0; y <= Constants.GRID_HEIGHT; y++) {
                int lineY = centerY + (y * blockHeight);
                g.DrawLine(pen, centerX, lineY, centerX + (blockWidth * Constants.GRID_WIDITH), lineY);
            }

            //Resize and Reposition GameBlocks
            _blocks.ForEach(block =>
            {
                var bounds = block.bounds;
                bounds.Width = blockWidth;
                bounds.Height = blockHeight;
                bounds.X = block.location.X * blockWidth + centerX;
                bounds.Y = block.location.Y * blockHeight + centerY;
                block.bounds = bounds;
                block.draw(g, false);
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

        public void slamPiece()
        {
            while (_gamePiece.canMoveDown(_blocks))
            {
                _gamePiece.moveDown();
            }

            gameTick();
        }

        public void rotatePiece()
        {
            if (_gamePiece.canRotate(_blocks))
                _gamePiece.rotate();
        }

    }//gameplayview class
}//tetris namespace
