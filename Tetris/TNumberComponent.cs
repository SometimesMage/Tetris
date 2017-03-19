using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    [Serializable]
    public class TNumberComponent : TComponents<int>
    {
        public TNumberComponent(String title) : base(title)
        {

        }

        public override void draw(Graphics g)
        {
            string titleString = _title;
            string detailString = Convert.ToString(_detail);

            Tuple<Font, SizeF> titleTuple = _box.adjustedFont(new Font(Constants.DEFAULT_FONT_TYPE, Constants.LARGEST_FONT_SIZE), titleString, g);
            Font titleFont = titleTuple.Item1;
            SizeF titleSize = titleTuple.Item2;

            Tuple<Font, SizeF> detailTuple = _box.adjustedFont(titleFont, detailString, g);
            Font detailFont = detailTuple.Item1;
            SizeF detailSize = detailTuple.Item2;

            //center title horizontally
            g.DrawString(_title, titleFont, Brushes.Purple, _box.X + ((_box.Width - titleSize.Width) / 2), _box.Y);

            //center detail horizontally and vertically
            Rectangle rectBounds = (new Rectangle(_box.X, Convert.ToInt32(titleSize.Height) + _box.Y,
                _box.Width, _box.Height - Convert.ToInt32(titleSize.Height)));
            
            Rectangle toDrawIn = new Rectangle(0, 0, Convert.ToInt32(detailSize.Width), Convert.ToInt32(detailSize.Height)).centerWithinBounds(rectBounds);

            g.DrawString(Convert.ToString(_detail), detailFont, Constants.DEFAULT_BRUSH_COLOR, new PointF(toDrawIn.X, toDrawIn.Y));


        }
        

    }
}
