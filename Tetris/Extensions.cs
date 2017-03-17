using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris {
    public static class Extensions {
        public static Rectangle addMargin(this Rectangle rect, int margin) {
            rect.X += margin;
            rect.Y += margin;
            rect.Height -= margin * 2;
            rect.Width -= margin * 2;
            return rect;
        }

        public static Rectangle addTopMargin(this Rectangle rect, int margin) {
            rect.Y += margin;
            rect.Height -= margin;
            return rect;
        }

        public static Rectangle addBottomMargin(this Rectangle rect, int margin) {
            rect.Height -= margin;
            return rect;
        }

        public static Rectangle addLeftMargin(this Rectangle rect, int margin) {
            rect.X += margin;
            rect.Width -= margin;
            return rect;
        }

        public static Rectangle addRightMargin(this Rectangle rect, int margin) {
            rect.Width -= margin;
            return rect;
        }

        public static Rectangle resizeByAspectRatio(this Rectangle rect, float widthRatio, float heightRatio)
        {
            float widthtemp = rect.Width / widthRatio;
            float heighttemp = rect.Height / heightRatio;

            float lowest = widthtemp < heighttemp ? widthtemp : heighttemp;

            rect.Width = Convert.ToInt32(lowest * widthRatio);
            rect.Height = Convert.ToInt32(lowest * heightRatio);

            return rect;
        }

        public static Rectangle resizeHeightToAspectRatio(this Rectangle rect, float ratio) {
            rect.Height = Convert.ToInt32(rect.Width / ratio);
            return rect;
        }

        public static Rectangle reszieWidthToAspectRatio(this Rectangle rect, float ratio) {
            rect.Width = Convert.ToInt32(rect.Height * ratio);
            return rect;
        }

        public static Rectangle centerWithinBounds(this Rectangle rect, Rectangle bounds) {
            int boundsCenterH = bounds.Height / 2;
            int boundsCenterW = bounds.Width / 2;

            int rectCenterH = rect.Height / 2;
            int rectCenterW = rect.Width / 2;

            rect.X = bounds.X + boundsCenterW - rectCenterW;
            rect.Y = bounds.Y + boundsCenterH - rectCenterH;

            return rect;
        }

        public static Tuple<Rectangle, Rectangle> splitAtWidth(this Rectangle rect, int width) {
            Rectangle rect1 = new Rectangle(rect.X, rect.Y, width, rect.Height);
            Rectangle rect2 = new Rectangle(rect.X + width, rect.Y, rect.Width - width, rect.Height);
            return Tuple.Create(rect1, rect2);
        }

        public static Tuple<Rectangle, Rectangle> splitAtHeight(this Rectangle rect, int height) {
            Rectangle rect1 = new Rectangle(rect.X, rect.Y, rect.Width, height);
            Rectangle rect2 = new Rectangle(rect.X, rect.Y + height, rect.Width, rect.Height - height);
            return Tuple.Create(rect1, rect2);
        }

        public static Rectangle[] splitVertically(this Rectangle rect, int numRects)
        {

            float splitRatio = 1f / numRects;
            int newHeight = (int)(rect.Height * splitRatio);
            int changingY = rect.Y;

            Rectangle[] rects = new Rectangle[numRects];

            for(int i = 0; i < numRects; i++)
            {
                rects[i] = new Rectangle( rect.X, changingY, rect.Width, newHeight);
                changingY += rects[i].Height;//make sure right math??
            }


            /*
            Rectangle rect1 = new Rectangle(rect.X, rect.Y, rect.Width, (int)((rect.Height) * splitRatio));
            Rectangle rect2 = new Rectangle(rect.X, rect1.Y + rect1.Height, rect.Width, (int)(rect.Height * splitRatio));
            Rectangle rect3 = new Rectangle(rect.X, rect1.Y + rect1.Height + rect2.Height, rect.Width, (int)(rect.Height * splitRatio));
            Rectangle rect4 = new Rectangle(rect.X, rect1.Y + rect1.Height + rect2.Height + rect3.Height, rect.Width, (int)(rect.Height * splitRatio));
            */

            return rects;
        }
    }
}
