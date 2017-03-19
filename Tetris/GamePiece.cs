using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    [Serializable]
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
            foreach(GameBlock block in _blocks)
            {
                var underBlock = block.location;
                underBlock.Y++;
                bool isFree = !grid.Any(gridBlock => gridBlock.location.Equals(underBlock)) && underBlock.Y < Constants.GRID_HEIGHT;
                if (!isFree)
                    return false;
            }

            return true;
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

        public bool canMoveLeft(List<GameBlock> grid)
        {
            foreach (GameBlock block in _blocks)
            {
                var leftblock = block.location;
                leftblock.X--;
                bool isFree = !grid.Any(gridBlock => gridBlock.location.Equals(leftblock)) && leftblock.X >= 0;
                if (!isFree)
                    return false;
            }

            return true;
        }

        public void moveLeft()
        {
            _blocks.ForEach(block =>
            {
                var loc = block.location;
                loc.X--;
                block.location = loc;
            });
        }

        public bool canMoveRight(List<GameBlock> grid)
        {
            foreach (GameBlock block in _blocks)
            {
                var rightBlock = block.location;
                rightBlock.X++;
                bool isFree = !grid.Any(gridBlock => gridBlock.location.Equals(rightBlock)) && rightBlock.X < Constants.GRID_WIDITH;
                if (!isFree)
                    return false;
            }

            return true;
        }

        public void moveRight()
        {
            _blocks.ForEach(block =>
            {
                var loc = block.location;
                loc.X++;
                block.location = loc;
            });
        }

        public bool canRotate(List<GameBlock> grid)
        {
            if (_pivot == null)
                return false;

            foreach(GameBlock block in _blocks)
            {
                Point blockLoc = rotateBlockAroundPivot(block);

                bool isFree = !grid.Any(gridBlock => gridBlock.location.Equals(blockLoc)) 
                    && blockLoc.X >= 0 && blockLoc.X < Constants.GRID_WIDITH
                    && blockLoc.Y < Constants.GRID_HEIGHT;

                if (!isFree)
                    return false;
            }

            return true;
        }

        public void rotate()
        {
            if (_pivot == null)
                return;

            foreach(GameBlock block in _blocks)
            {
                if (block.Equals(_pivot))
                    continue;

                block.location = rotateBlockAroundPivot(block);
            }
        }

        private Point rotateBlockAroundPivot(GameBlock block)
        {
            var pivotLoc = _pivot.location;
            var blockLoc = block.location;

            var x = blockLoc.X - pivotLoc.X;
            var y = blockLoc.Y - pivotLoc.Y;

            blockLoc.X = -y + pivotLoc.X;
            blockLoc.Y = x + pivotLoc.Y;

            return blockLoc;
        }

        public GamePiece createGhostPiece(List<GameBlock> grid)
        {
            List<GameBlock> ghostBlocks = new List<GameBlock>();
            foreach(GameBlock block in _blocks)
            {
                ghostBlocks.Add(new GameBlock(new Rectangle(block.bounds.X, block.bounds.Y, block.bounds.Width, block.bounds.Height),
                    new Point(block.location.X, block.location.Y)));
            }

            GamePiece ghost = new GamePiece(ghostBlocks.ToArray(), ghostBlocks[0]);


            while (ghost.canMoveDown(grid))
                ghost.moveDown();

            return ghost;
        }

    }//gamepiece class
}//tetris namespace
