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

        public GamePlayView(Rectangle view = new Rectangle())
        {
            _view = view;
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
        }

    }
}
