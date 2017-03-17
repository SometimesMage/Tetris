using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class GamePiece
    {
        private List<GameBlock> _blocks;
        private GameBlock _pivot;

        public GamePiece(GameBlock[] blocks, GameBlock pivot)
        {
            _blocks = new List<GameBlock>(blocks);
            _pivot = pivot;
        }

        public List<GameBlock> getBlocks()
        {
            return _blocks;
        }

        public GameBlock pivot {
            get {
                return _pivot;
            }

            set {
                _pivot = value;
            }
        }

        public bool canMoveDown(List<GameBlock> grid)
        {
            return false;
        }

        public void moveDown()
        {
            _blocks.ForEach(block =>
            {
                var loc = block.location;
                loc.Y++;
                block.location = loc;
            });
        }

        public void moveLeft()
        {
            bool canMove = !_blocks.Any(block => block.location.X == 0);
            if(canMove)
            {
                _blocks.ForEach(block =>
                {
                    var loc = block.location;
                    loc.X--;
                    block.location = loc;
                });
            }
        }

        public void moveRight()
        {
            bool canMove = !_blocks.Any(block => block.location.X == Constants.GRID_WIDITH - 1);
            if (canMove)
            {
                _blocks.ForEach(block =>
                {
                    var loc = block.location;
                    loc.X++;
                    block.location = loc;
                });
            }
        }

    }//gamepiece class
}//tetris namespace
