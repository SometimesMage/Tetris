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

        public GamePlayView()
        {
            _view = new Rectangle();
        }

        public Rectangle view
        {
            get
            {
                return _view;
            }
            set
            {
                _view = value;
            }
        }

        public void draw(Graphics g)
        {
            //Draw Background
            g.FillRectangle(new SolidBrush(Constants.GAME_BACKGROUND_COLOR), _view);

            int blockWidth = _view.Width / Constants.GRID_WIDITH;
            int blockHeight = _view.Height / Constants.GRID_HEIGHT;

            //Draw Grid
            for(int x = 0; x < Constants.GRID_WIDITH; x++) {
                for(int y = 0; y <Constants.GRID_HEIGHT; y++) {
                    g.DrawRectangle(Pens.Black, _view.X + (x * blockWidth), _view.Y + (y * blockHeight), blockWidth, blockHeight);
                }
            }
        }

    }
}
