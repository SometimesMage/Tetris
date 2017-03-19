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

            GameBlock b1;
            GameBlock b2;
            GameBlock b3;
            GameBlock b4;

            switch (piece)
            {

                case GamePieces.L_RIGHT:
                    b1 = new GameBlock(location: new Point(centerBlock, -1));
                    b1.Color = Constants.L_RIGHT_PIECE_COLOR;
                    b2 = new GameBlock(location: new Point(centerBlock, 0));
                    b2.Color = Constants.L_RIGHT_PIECE_COLOR;
                    b3 = new GameBlock(location: new Point(centerBlock, 1));
                    b3.Color = Constants.L_RIGHT_PIECE_COLOR;
                    b4 = new GameBlock(location: new Point(centerBlock + 1, 1));
                    b4.Color = Constants.L_RIGHT_PIECE_COLOR;

                    return new GamePiece(new GameBlock[]{b1, b2, b3, b4}, b2);

                case GamePieces.L_LEFT:
                    b1 = new GameBlock(location: new Point(centerBlock, -1));
                    b1.Color = Constants.L_LEFT_PIECE_COLOR;
                    b2 = new GameBlock(location: new Point(centerBlock, 0));
                    b2.Color = Constants.L_LEFT_PIECE_COLOR;
                    b3 = new GameBlock(location: new Point(centerBlock, 1));
                    b3.Color = Constants.L_LEFT_PIECE_COLOR;
                    b4 = new GameBlock(location: new Point(centerBlock + 1, -1));
                    b4.Color = Constants.L_LEFT_PIECE_COLOR;

                    return new GamePiece(new GameBlock[] { b1, b2, b3, b4 }, b2);

                case GamePieces.BLOCK:
                    b1 = new GameBlock(location: new Point(centerBlock, 0));
                    b1.Color = Constants.BLOCK_PIECE_COLOR;
                    b2 = new GameBlock(location: new Point(centerBlock, -1));
                    b2.Color = Constants.BLOCK_PIECE_COLOR;
                    b3 = new GameBlock(location: new Point(centerBlock + 1, -1));
                    b3.Color = Constants.BLOCK_PIECE_COLOR;
                    b4 = new GameBlock(location: new Point(centerBlock + 1, 0));
                    b4.Color = Constants.BLOCK_PIECE_COLOR;

                    return new GamePiece(new GameBlock[] { b1, b2, b3, b4 }, null);

                case GamePieces.T:
                    b1 = new GameBlock(location: new Point(centerBlock, 0));
                    b1.Color = Constants.T_PIECE_COLOR;
                    b2 = new GameBlock(location: new Point(centerBlock -1, 0));
                    b2.Color = Constants.T_PIECE_COLOR;
                    b3 = new GameBlock(location: new Point(centerBlock + 1, 0));
                    b3.Color = Constants.T_PIECE_COLOR;
                    b4 = new GameBlock(location: new Point(centerBlock, -1));
                    b4.Color = Constants.T_PIECE_COLOR;

                    return new GamePiece(new GameBlock[] { b1, b2, b3, b4 }, b1);

                case GamePieces.LINE:
                    b1 = new GameBlock(location: new Point(centerBlock, -1));
                    b1.Color = Constants.LINE_GAME_PIECE_COLOR;
                    b2 = new GameBlock(location: new Point(centerBlock, 0));
                    b2.Color = Constants.LINE_GAME_PIECE_COLOR;
                    b3 = new GameBlock(location: new Point(centerBlock, + 1));
                    b3.Color = Constants.LINE_GAME_PIECE_COLOR;
                    b4 = new GameBlock(location: new Point(centerBlock, + 2));
                    b4.Color = Constants.LINE_GAME_PIECE_COLOR;

                    return new GamePiece(new GameBlock[] { b1, b2, b3, b4 }, b2);

                case GamePieces.CURVE_LEFT:
                    b1 = new GameBlock(location: new Point(centerBlock, -1));
                    b1.Color = Constants.CURVE_LEFT_PIECE_COLOR;
                    b2 = new GameBlock(location: new Point(centerBlock, 0));
                    b2.Color = Constants.CURVE_LEFT_PIECE_COLOR;
                    b3 = new GameBlock(location: new Point(centerBlock - 1, 0));
                    b3.Color = Constants.CURVE_LEFT_PIECE_COLOR;
                    b4 = new GameBlock(location: new Point(centerBlock - 1, +1));
                    b4.Color = Constants.CURVE_LEFT_PIECE_COLOR;

                    return new GamePiece(new GameBlock[] { b1, b2, b3, b4 }, b2);

                case GamePieces.CURVE_RIGHT:
                    b1 = new GameBlock(location: new Point(centerBlock, -1));
                    b1.Color = Constants.CURVE_RIGHT_PEICE_COLOR;
                    b2 = new GameBlock(location: new Point(centerBlock, 0));
                    b2.Color = Constants.CURVE_RIGHT_PEICE_COLOR;
                    b3 = new GameBlock(location: new Point(centerBlock + 1, 0));
                    b3.Color = Constants.CURVE_RIGHT_PEICE_COLOR;
                    b4 = new GameBlock(location: new Point(centerBlock + 1, +1));
                    b4.Color = Constants.CURVE_RIGHT_PEICE_COLOR;

                    return new GamePiece(new GameBlock[] { b1, b2, b3, b4 }, b2);


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
