using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris {
    public class GameInfoView {

        private Rectangle _view;

        //each 'descriptor' for splits
        private TComponents _nextBlock;
        private TComponents _score;
        private TComponents _lines;
        private TComponents _level;

        //IDEA: a list of TComponents, escpecially useful for drawing to iterate through and draw each


        public GameInfoView(Rectangle rect) {
            _view = rect;

            //hard-coded for display test
            _nextBlock = new TNextBlockComponent();
            _score = new TNumberComponent("Score");
            _lines = new TNumberComponent("Lines");
            _level = new TNumberComponent("Level");

            //
            //  instead have infolabelcomponents have ref to a infodetailcomponent??
            //
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

            //if we have list of tcomponents then we iterate through and call draw
            //and inside each infolabelcomponent can call draw on its own tcomponent
            //which is an infodetailcomponent

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


            //hard-coded because if we move infodetailcomponents inside of infolabelcomponents
            //then we can iterate through a list of gameinfoviews tcomponents and do this in
            //a loop like above

            _nextBlock.box = components[0];
            _score.box = components[1];
            _lines.box = components[2];
            _level.box = components[3];


        }
    }
}
