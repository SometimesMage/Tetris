using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetris
{
    public class GamePlayView
    {

        private Rectangle _view;

        public GamePlayView()
        {
            _view = new Rectangle();
        }

        public Rectangle view
        {
            get
            {
                return _view;
            }
            set
            {
                _view = value;
            }
        }

        public void draw(Graphics g)
        {

        }

    }
}
