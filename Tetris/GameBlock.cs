using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class GameBlock
    {
        private Rectangle _bounds;
        private Point _location;

        public GameBlock(Rectangle bounds = new Rectangle(), Point location = new Point())
        {
            _bounds = bounds;
            _location = location;
        }

        public Rectangle bounds 
        {
            get { return _bounds; }

            set { _bounds = value; }
        }

        public Point location
        {
            get { return _location; }
            set { _location = value; }
        }

        public void draw(Graphics g)
        {
            //Test Code
            g.FillRectangle(Brushes.Black, _bounds);
        }
    }
}
