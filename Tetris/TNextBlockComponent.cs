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

            Image pieceImage = null;

            switch(_detail)
            {
                case GamePieces.BLOCK:
                    pieceImage = Image.FromFile("Images\\Block.png");
                    break;
                case GamePieces.CURVE_LEFT:
                    pieceImage = Image.FromFile("Images\\Curve_Left.png");
                    break;
                case GamePieces.CURVE_RIGHT:
                    pieceImage = Image.FromFile("Images\\Curve_Right.png");
                    break;
                case GamePieces.LINE:
                    pieceImage = Image.FromFile("Images\\Line.png");
                    break;
                case GamePieces.L_LEFT:
                    pieceImage = Image.FromFile("Images\\L_Left.png");
                    break;
                case GamePieces.L_RIGHT:
                    pieceImage = Image.FromFile("Images\\L_Right.png");
                    break;
                case GamePieces.T:
                    pieceImage = Image.FromFile("Images\\T.png");
                    break;
            }

            Rectangle rectBounds = (new Rectangle(_box.X, Convert.ToInt32(titleSize.Height) + _box.Y,
                _box.Width, _box.Height - Convert.ToInt32(titleSize.Height)));
            Rectangle imageBounds = rectBounds.addMargin(10).resizeByAspectRatio(pieceImage.Size.Width, pieceImage.Size.Height);
            imageBounds = imageBounds.centerWithinBounds(rectBounds);
            
            g.DrawImage(pieceImage, imageBounds);
            pieceImage.Dispose();
        }
    }
}
