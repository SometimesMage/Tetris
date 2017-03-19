using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris {
    [Serializable]
    public class GameInfoView {

        private Rectangle _view;
        
        private TComponents<GamePieces> _nextBlock;
        private TComponents<int> _score;
        private TComponents<int> _lines;
        private TComponents<int> _level;

        //IDEA: a list of TComponents, escpecially useful for drawing to iterate through and draw each


        public GameInfoView(Rectangle view = new Rectangle()) {
            _view = view;
            
            _nextBlock = new TNextBlockComponent();
            _score = new TNumberComponent("Score");
            _lines = new TNumberComponent("Lines");
            _level = new TNumberComponent("Level");
            _level.detail = 1;
        }


        public Rectangle view {
            get {
                return _view;
            }

            set {
                _view = value;
            }
        }

        public void addNextBlock(GamePieces nextPiece)
        {
            _nextBlock.detail = nextPiece;
        }

        public void addToScore(int scoreToAdd)
        {
            if(scoreToAdd > 0)
            {
              _score.detail += scoreToAdd;
            }
        }

        public void addToLine(int linesToAdd)
        {
            if(linesToAdd > 0)
            {
                int combo = linesToAdd - Constants.GAME_COMBO_LINES;
                int score = (linesToAdd * Constants.GAME_LINE_SCORE + combo * Constants.GAME_COMBO_SCORE_BONUS) * _level.detail;

                if (_lines.detail + linesToAdd >= Constants.GAME_LINES_PER_LEVEL)
                {
                    addToLevel();
                }
                _lines.detail = (_lines.detail + linesToAdd) % 10;

                addToScore(score);
            }
        }

        private void addToLevel()
        {
            _level.detail += (Constants.GAME_LEVEL_INCREMENT);
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
            _nextBlock.box = components[0];
            _score.box = components[1];
            _lines.box = components[2];
            _level.box = components[3];
        }
    }
}
