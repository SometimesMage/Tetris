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

        public static Rectangle resizeHeightToAspectRatio(this Rectangle rect, float ratio) {
            rect.Height = Convert.ToInt32(rect.Width / ratio);
            return rect;
        }

        public static Rectangle reszieWidthToAspectRatio(this Rectangle rect, float ratio) {
            rect.Width = Convert.ToInt32(rect.Height * ratio);
            return rect;
        }
    }
}
