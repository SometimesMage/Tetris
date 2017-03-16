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

        //temporary...move to different class
        //gets largest font size to draw the specified string in the _box (field of this class)
        protected Tuple<Font, SizeF> adjustedFont(Font startFont, String toDraw, Graphics g)
        {

            Font toReturn = startFont;
            SizeF potentialSize = new SizeF();
            
                int i;
                for (i = Convert.ToInt32(startFont.Size); i >= 4; i--)
                {
                    potentialSize = g.MeasureString(toDraw, toReturn = new Font(Constants.DEFAULT_FONT_TYPE, i));
                    if (potentialSize.Width <= _box.Width)
                    {
                        //return toReturn;
                        break;
                    }
                }


            return Tuple.Create(toReturn, potentialSize);
            //return toReturn;
        }

        public abstract void draw(Graphics g);

    }
}
