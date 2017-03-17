using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetris
{
    public abstract class TComponents   //T for tetris
    {

        protected Rectangle _box;
        protected String _title;

        public TComponents(String title)
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

        protected Tuple<Font, SizeF> adjustedFont(Font startFont, String toDraw, Graphics g)
        {

            Font toReturn = startFont;
            SizeF potentialSize = new SizeF();
            
                int i;
                for (i = Convert.ToInt32(startFont.Size); i >= Constants.SMALLEST_FONT_SIZE; i--)
                {
                    potentialSize = g.MeasureString(toDraw, toReturn = new Font(Constants.DEFAULT_FONT_TYPE, i));
                    if (potentialSize.Width <= _box.Width)
                    {
                        break;
                    }
                }


            return Tuple.Create(toReturn, potentialSize);
        }

        public abstract void draw(Graphics g);

    }
}
