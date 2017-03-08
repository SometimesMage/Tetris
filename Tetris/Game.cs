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

            Rectangle modifiedView = _view.addMargin(Constants.GAME_MIN_MARGIN_AREA);   //gets the new game area with respect to margins

            //>>>   Calculate Margin Based on Aspect Ratio

            //TODO check over and modify to fit our needs (i think this will work)

            float widthtemp = modifiedView.Width / 5;
            float heighttemp = modifiedView.Height / 9;

            float lowest = widthtemp < heighttemp ? widthtemp : heighttemp;

            float newwidth = lowest * 5;
            float newheight = lowest * 9;

            g.FillRectangle(new SolidBrush(Color.Red), new Rectangle(0, 24, (int)newwidth, (int)newheight));

            //<<<

            Tuple<Rectangle, Rectangle> splitView = modifiedView.splitAtWidth(Convert.ToInt32(modifiedView.Width * Constants.GAME_VIEW_SPLIT));
            //g.FillRectangle(new SolidBrush(Color.Red), splitView.Item1);
            //g.FillRectangle(new SolidBrush(Color.Beige), splitView.Item2);
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
