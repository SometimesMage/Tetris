using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris {
    [Serializable]
    public class GameInfoView {

        private delegate void TimerDel(int level);

        private Rectangle _view;

        private TimerDel timerNotifier;
        
        private TComponents<GamePieces> _nextBlock;
        private TComponents<int> _score;
        private TComponents<int> _lines;
        private TComponents<int> _level;
        
        public GameInfoView(Game game, Rectangle view = new Rectangle()) {
            _view = view;
            
            _nextBlock = new TNextBlockComponent();
            _score = new TNumberComponent("Score");
            _lines = new TNumberComponent("Lines");
            _level = new TNumberComponent("Level");

            _level.detail = 1;

            timerNotifier = game.updateTimer;
        }


        public Rectangle view {
            get {
                return _view;
            }

            set {
                _view = value;
            }
        }

        public int getLevel()
        {
            return _level.detail;
        }

        public void addNextBlock(GamePieces nextPiece)
        {
            _nextBlock.detail = nextPiece;
        }

        public void addToScore(int scoreToAdd)
        {
            if(scoreToAdd > 0)
            {
              _score.detail += scoreToAdd * _level.detail;
            }
        }

        public void addToLine(int linesToAdd)
        {
            if(linesToAdd > 0)
            {
                int combo = linesToAdd - Constants.GAME_COMBO_LINES;
                int score = (linesToAdd * Constants.GAME_LINE_SCORE + combo * Constants.GAME_COMBO_SCORE_BONUS);

                if (_lines.detail + linesToAdd >= Constants.GAME_LINES_PER_LEVEL)
                {
                    addToLevel();
                }
                _lines.detail = (_lines.detail + linesToAdd) % 10;

                addToScore(score);
            }
        }

        public void addToLevel()
        {
            _level.detail += (Constants.GAME_LEVEL_INCREMENT);
            timerNotifier(_level.detail);
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
