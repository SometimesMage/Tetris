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

            Rectangle modifiedView = _view.addMargin(Constants.GAME_MIN_MARGIN_AREA);

            //TODO Calculate Margin Based on Aspect Ratio
            Tuple<Rectangle, Rectangle> splitView = modifiedView.splitAtWidth(Convert.ToInt32(modifiedView.Width * Constants.GAME_VIEW_SPLIT));
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
