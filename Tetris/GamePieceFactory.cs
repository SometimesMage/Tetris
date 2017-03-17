using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class GamePieceFactory
    {
        private static GamePieceFactory _instance = new GamePieceFactory();

        public static GamePieceFactory Instance => _instance;

        public GamePiece createGamePiece(GamePieces piece)
        {
            int centerBlock = Constants.GRID_WIDITH / 2;
            switch(piece)
            {
                case GamePieces.L_RIGHT:
                    GameBlock b1 = new GameBlock(location: new Point(centerBlock, -1));
                    GameBlock b2 = new GameBlock(location: new Point(centerBlock, 0));
                    GameBlock b3 = new GameBlock(location: new Point(centerBlock, 1));
                    GameBlock b4 = new GameBlock(location: new Point(centerBlock + 1, 1));

                    return new GamePiece(new GameBlock[]{b1, b2, b3, b4}, b2);
            }
            return null;
        }
    }

    public enum GamePieces
    {
        L_RIGHT,
        L_LEFT,
        CURVE_RIGHT,
        CURVE_LEFT,
        T,
        LINE,
        BLOCK
    }
}
