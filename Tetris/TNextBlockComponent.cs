using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    [Serializable]
    public class TNextBlockComponent : TComponents<GamePieces>
    {
        public TNextBlockComponent() : base("Next Block")
        {
        }

        public override void draw(Graphics g)
        {
            string titleString = _title;

            Tuple<Font, SizeF> titleTuple = _box.adjustedFont(new Font(Constants.DEFAULT_FONT_TYPE, Constants.LARGEST_FONT_SIZE), titleString, g);
            Font titleFont = titleTuple.Item1;
            SizeF titleSize = titleTuple.Item2;
            
            //center title horizontally
            g.DrawString(_title, titleFont, Constants.DEFAULT_BRUSH_COLOR, _box.X + ((_box.Width - titleSize.Width) / 2), _box.Y);
        }
    }
}
