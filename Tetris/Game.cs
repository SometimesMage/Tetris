using System;
using System.Collections.Generic;
using System.Drawing;
using static Tetris.Constants;
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

            Rectangle modifiedView = _view.addMargin(Constants.GAME_MIN_MARGIN_AREA);   //gets the new game area with respect to margins
            modifiedView = modifiedView.resizeByAspectRatio(GAME_ASPECT_WIDTH_RATIO + INFO_ASPECT_WIDTH_RATIO, GAME_ASPECT_HEIGHT_RATIO);
            modifiedView = modifiedView.centerWithinBounds(_view);

            Tuple<Rectangle, Rectangle> splitView = modifiedView.splitAtWidth(Convert.ToInt32(modifiedView.Width * Constants.GAME_VIEW_SPLIT));
            Rectangle gameRect = splitView.Item1;
            Rectangle infoRect = splitView.Item2;

            gameRect.X -= GAME_MIN_MARGIN_AREA / 2;
            infoRect.X += GAME_MIN_MARGIN_AREA / 2;

            //Test Code
            GameInfoView infoView = new GameInfoView(infoRect);
            infoView.draw(g);
            
        }

        public void pauseGame()
        {
            //TODO pause related operations
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
