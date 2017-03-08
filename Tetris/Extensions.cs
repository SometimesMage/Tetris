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
    }
}
