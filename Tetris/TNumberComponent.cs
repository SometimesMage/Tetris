using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class TNumberComponent : TComponents
    {
        private int _detail;

        public TNumberComponent(String title) : base(title)
        {

        }

        public int Detail
        {
            get
            {
                return _detail;
            }

            set
            {
                _detail = value;
            }
        }

        public override void draw(Graphics g)
        {
            g.DrawString(_title, new Font("Arial", 16), new SolidBrush(Color.Purple), _box.X, _box.Y);
            g.DrawString(Convert.ToString(_detail), new Font("Arial", 16), new SolidBrush(Color.Purple), _box.X + _box.Width / 2, _box.Y + _box.Height / 2);
        }
    }
}
