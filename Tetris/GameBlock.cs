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
        private Color _color;

        public GameBlock(Rectangle bounds = new Rectangle(), Point location = new Point())
        {
            _bounds = bounds;
            _location = location;
            _color = Color.Black;
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

        public Color Color { get { return _color; } set { _color = value; } }

        public void moveDown()
        {
            _location.Y++;
        }

        public void draw(Graphics g, bool isGhost)
        {
            Color color = Color.FromArgb(isGhost ? 100 : 255, _color);
            SolidBrush brush = new SolidBrush(color);

            g.FillRectangle(brush, _bounds);
            g.DrawRectangle(Pens.Black, _bounds);

            brush.Dispose();
        }
    }
}
