using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris {
    public class GameInfoView {

        private Rectangle _view;
        
        private TComponents _nextBlock;
        private TComponents _score;
        private TComponents _lines;
        private TComponents _level;

        //IDEA: a list of TComponents, escpecially useful for drawing to iterate through and draw each


        public GameInfoView(Rectangle rect) {
            _view = rect;
            
            _nextBlock = new TNextBlockComponent();
            _score = new TNumberComponent("Score");
            _lines = new TNumberComponent("Lines");
            _level = new TNumberComponent("Level");
            
        }


        public Rectangle view {
            get {
                return _view;
            }

            set {
                _view = value;
            }
        }

        public void draw(Graphics g) {

            
                g.FillRectangle(new SolidBrush(Color.Beige), _view);

                Rectangle[] components = _view.splitVertically(Constants.GAME_INFO_RECTS);

                setComponents(components);

                _nextBlock.draw(g);
                _score.draw(g);
                _lines.draw(g);
                _level.draw(g);

        }

        private void setComponents(Rectangle[] components)
        {


            /*Rectangle[] splits;

            for(int i = 0; i < components.Length; i++)
            {

                splits = components[i].splitVertically(Constants.GAME_INFO_VIEW_SPLIT2);


            }*/

            _nextBlock.box = components[0];
            _score.box = components[1];
            _lines.box = components[2];
            _level.box = components[3];


        }
    }
}
