using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris {
    public class GameInfoView {
        private Rectangle _view;

        public GameInfoView() {
            _view = new Rectangle();
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

        }
    }
}
