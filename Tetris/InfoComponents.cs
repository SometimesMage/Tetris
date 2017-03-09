using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetris
{
    class InfoComponents
    {

        private Rectangle _box;
        private String _title;

        public InfoComponents(String title)
        {
            _box = new Rectangle();
            _title = title;
        }

        public Rectangle box
        {
            get
            {
                return _box;
            }
            set
            {
                _box = value;
            }
        }

        public void draw(Graphics g)
        {

            //Test Code
            g.DrawString(_title, new Font("Arial", 12), new SolidBrush(Color.Purple), _box.X, _box.Y);
            g.DrawLine(new Pen(Color.Red, 5), _box.X, _box.Y, _box.X, _box.Y + _box.Height);

        }

    }
}
