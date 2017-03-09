using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris {
    public class GameInfoView {

        private Rectangle _view;
        private InfoComponents _nextBlockBox;
        private InfoComponents _scoreBox;
        private InfoComponents _linesBox;
        private InfoComponents _levelBox;


        public GameInfoView(Rectangle rect) {
            _view = rect;
            _nextBlockBox = new InfoComponents("Next Block");
            _scoreBox = new InfoComponents("Score");
            _linesBox = new InfoComponents("Lines");
            _levelBox = new InfoComponents("Level");
        }


        public Rectangle view {
            get {
                return _view;
            }

            set {
                _view = value;
            }
        }

        public void draw(Graphics g) {

            g.FillRectangle(new SolidBrush(Color.Beige), _view);

            Tuple<Rectangle, Rectangle, Rectangle, Rectangle> components = _view.splitVertically(Constants.GAME_INFO_VIEW_SPLIT);

            setComponents(components);

            _nextBlockBox.draw(g);
            _scoreBox.draw(g);
            _linesBox.draw(g);
            _levelBox.draw(g);

        }

        private void setComponents(Tuple<Rectangle, Rectangle, Rectangle, Rectangle> components)
        {
            _nextBlockBox.box = components.Item1;
            _scoreBox.box = components.Item2;
            _linesBox.box = components.Item3;
            _levelBox.box = components.Item4;
        }
    }
}
