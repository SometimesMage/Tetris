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
            string titleString = _title;
            string detailString = Convert.ToString(_detail);

            Tuple<Font, SizeF> titleTuple = adjustedFont(new Font(Constants.DEFAULT_FONT_TYPE, Constants.LARGEST_FONT_SIZE), titleString, g);
            Font titleFont = titleTuple.Item1;
            SizeF titleSize = titleTuple.Item2;

            Tuple<Font, SizeF> detailTuple = adjustedFont(titleFont, detailString, g);
            Font detailFont = detailTuple.Item1;
            SizeF detailSize = detailTuple.Item2;
           
            //where should centering math be put?? this looks atrocious>>>
            g.DrawString(_title, titleFont, Brushes.Purple, _box.X + ((_box.Width - titleSize.Width)/2), _box.Y);
            g.DrawString(Convert.ToString(_detail), detailFont, Brushes.Purple,
                _box.X + ((_box.Width - detailSize.Width) / 2),
                _box.Y + titleSize.Height + ((((_box.Y + _box.Height)-(_box.Y + titleSize.Height)) -detailSize.Height)/2));
            //<<<
        }

    }
}
