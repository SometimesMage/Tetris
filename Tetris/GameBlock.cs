using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class GameBlock
    {
        private Rectangle _bounds;

        public GameBlock(Rectangle bounds = new Rectangle())
        {
            _bounds = bounds;
        }

        public Rectangle bounds 
        {
            get {
                return _bounds;
            }

            set {
                _bounds = value;
            }
        }

        public void draw(Graphics g)
        {
            //Test Code
            g.FillRectangle(Brushes.Black, _bounds);
        }
    }
}
