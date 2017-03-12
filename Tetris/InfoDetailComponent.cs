using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetris
{
    class InfoDetailComponent : TComponents
    {
        public InfoDetailComponent(string title) : base(title)
        {
            //anything in here??
        }

        public override void draw(Graphics g)
        {
            g.DrawString(_title, new Font("Arial", 16), new SolidBrush(Color.Purple), _box.X, _box.Y);
        }

    }
}
