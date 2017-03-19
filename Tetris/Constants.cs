using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris {
    public class Constants {
        //VIEWS
        public static readonly float GAME_VIEW_SPLIT = 2f / 3f;
        public static readonly float GAME_ASPECT_WIDTH_RATIO = 5f;
        public static readonly float GAME_ASPECT_HEIGHT_RATIO = 9f;
        public static readonly float INFO_ASPECT_WIDTH_RATIO = ((1 - GAME_VIEW_SPLIT) / GAME_VIEW_SPLIT) * GAME_ASPECT_WIDTH_RATIO;
        public static readonly float INFO_ASPECT_HEIGHT_RATIO = GAME_ASPECT_HEIGHT_RATIO;
        public static readonly int GAME_MIN_MARGIN_AREA = 5;
        public static readonly int GAME_INFO_RECTS = 4;
        public static readonly int GAME_MIN_INFO_MARGIN = 5;

        //SPEEDS
        public static readonly float GAME_INITAIL_SPEED = 0.5f;
        public static readonly float GAME_MAX_SPEED = GAME_INITAIL_SPEED / 5f;
        public static readonly float GAME_DOWN_SPEED_MULTIPLIER = .5f;
        public static readonly float GAME_LEVEL_SPEED_MODIFIER = 0.25f;
        public static readonly float GAME_LEVEL_SPEED_MULTIPLIER = 0.75f;
        public static readonly int ONE_SECOND_MILLIS = 1000;

        //SCORES
        public static readonly int GAME_LINE_SCORE = 100;
        public static readonly int GAME_COMBO_SCORE_BONUS = 50;
        public static readonly int GAME_SLAM_PER_BLOCK_SCORE = 1;
        public static readonly int GAME_LEVEL_INCREMENT = 1;
        public static readonly int GAME_LINES_PER_LEVEL = 10;
        public static readonly int GAME_COMBO_LINES = 1;

        //COLORS
        public static readonly Color BACKGROUND_COLOR = Color.FromArgb(0, 0, 0);
        public static readonly Color GAME_BACKGROUND_COLOR = Color.White;
        public static readonly Color GAME_BACKGROUND_COLOR_2 = Color.SkyBlue;
        public static readonly Color INFO_BACKGROUND_COLOR = Color.Silver;
        public static readonly Brush DEFAULT_BRUSH_COLOR = Brushes.Black;
        public static readonly Color LINE_GAME_PIECE_COLOR = Color.FromArgb(0, 240, 240);
        public static readonly Color L_LEFT_PIECE_COLOR = Color.FromArgb(0, 0, 240);
        public static readonly Color L_RIGHT_PIECE_COLOR = Color.FromArgb(240, 160, 0);
        public static readonly Color CURVE_LEFT_PIECE_COLOR = Color.FromArgb(0, 240, 0);
        public static readonly Color CURVE_RIGHT_PEICE_COLOR = Color.FromArgb(240, 0, 0);
        public static readonly Color T_PIECE_COLOR = Color.FromArgb(160, 0, 240);
        public static readonly Color BLOCK_PIECE_COLOR = Color.FromArgb(240, 240, 0);

        //FONT
        public static readonly int LARGEST_FONT_SIZE = 54;
        public static readonly int SMALLEST_FONT_SIZE = 10;
        public static readonly String DEFAULT_FONT_TYPE = "Arial";

        //GAME GRID
        public static readonly int GRID_WIDITH = 10;
        public static readonly int GRID_HEIGHT = 18;
    }
}
