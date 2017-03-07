using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris {
    public class Game {
        private Rectangle _view;

        public Game() {
            _view = new Rectangle();
        }

        public void draw(Graphics g) {
            g.FillRectangle(new SolidBrush(Constants.BACKGROUND_COLOR), _view);

            //TODO Calculate Margin Based on Aspect Ratio
            int gamePlayWidth = Convert.ToInt32(_view.Width / Constants.GAME_PLAY_VIEW_AREA);
            int gameInfoWidth = Convert.ToInt32(_view.Width / Constants.GAME_INFO_VIEW_AREA);
        }

        public Rectangle view {
            get {
                return _view;
            }

            set {
                _view = value;
            }
        }
    }
}
