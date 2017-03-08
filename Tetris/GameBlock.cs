using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris {
    public class GameBlock {
        private Rectangle _boundingBox;
        private Color _color;
        private Color _outlineColor;

        public GameBlock() {
            _boundingBox = new Rectangle();
            _color = Color.Black;
        }

        public Rectangle boundingBox {
            get {
                return _boundingBox;
            }

            set {
                _boundingBox = value;
            }
        }

        public Color color {
            get {
                return _color;
            }

            set {
                _color = value;
            }
        }

        public Color outlineColor {
            get {
                return _outlineColor;
            }

            set {
                _outlineColor = value;
            }
        }

        public void draw(Graphics g) {
            g.DrawRectangle(new Pen(_outlineColor, 1), _boundingBox);
            g.FillRectangle(new SolidBrush(_color), _boundingBox);
        }

    }
}
