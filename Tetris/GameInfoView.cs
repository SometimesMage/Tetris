using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris {
    public class GameInfoView {

        private Rectangle _view;

        //each label for the splits
        private TComponents _nextBlockBox;
        private TComponents _scoreBox;
        private TComponents _linesBox;
        private TComponents _levelBox;

        //each 'descriptor' for splits
        private TComponents _nextBlock;
        private TComponents _score;
        private TComponents _lines;
        private TComponents _level;

        //IDEA: a list of TComponents, escpecially useful for drawing to iterate through and draw each


        public GameInfoView(Rectangle rect) {
            _view = rect;

            _nextBlockBox = new InfoLabelComponent("Next Block");
            _scoreBox = new InfoLabelComponent("Score");
            _linesBox = new InfoLabelComponent("Lines");
            _levelBox = new InfoLabelComponent("Level");

            //hard-coded for display test
            _nextBlock = new InfoLabelComponent("null");
            _score = new InfoLabelComponent("0");
            _lines = new InfoLabelComponent("0");
            _level = new InfoLabelComponent("1");

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

            Rectangle[] components = _view.splitVertically(Constants.GAME_INFO_VIEW_SPLIT4);

            setComponents(components);

            //if we have list of tcomponents then we iterate through and call draw
            //and inside each infolabelcomponent can call draw on its own tcomponent
            //which is an infodetailcomponent

            _nextBlockBox.draw(g);
            _scoreBox.draw(g);
            _linesBox.draw(g);
            _levelBox.draw(g);

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

            Rectangle[] splits1 = components[0].splitVertically(Constants.GAME_INFO_VIEW_SPLIT2);
            Rectangle[] splits2 = components[1].splitVertically(Constants.GAME_INFO_VIEW_SPLIT2);
            Rectangle[] splits3 = components[2].splitVertically(Constants.GAME_INFO_VIEW_SPLIT2);
            Rectangle[] splits4 = components[3].splitVertically(Constants.GAME_INFO_VIEW_SPLIT2);

            _nextBlockBox.box = splits1[0];
            _scoreBox.box = splits2[0];
            _linesBox.box = splits3[0];
            _levelBox.box = splits4[0];

            _nextBlock.box = splits1[1];
            _score.box = splits2[1];
            _lines.box = splits3[1];
            _level.box = splits4[1];


        }
    }
}
