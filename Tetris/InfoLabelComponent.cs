using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetris
{
    class InfoLabelComponent : TComponents
    {

        /*
         * private TComponents detail;
         */

        public InfoLabelComponent(string title) : base(title)
        {
            /*
             * detail = new InfoDetailComponent(?);
             */
        }

        public override void draw(Graphics g)
        {
            g.DrawString(_title, new Font("Arial", 16), new SolidBrush(Color.Purple), _box.X, _box.Y);
            //detail.draw(g);

        }
    }
}
