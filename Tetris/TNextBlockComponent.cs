using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class TNextBlockComponent : TComponents
    {
        public TNextBlockComponent() : base("Next Block")
        {
        }

        public override void draw(Graphics g)
        {
            g.DrawString(_title, new Font("Arial", 16), new SolidBrush(Color.Purple), _box.X, _box.Y);
        }
    }
}
