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
        public static readonly float GAME_INITAIL_SPEED = 1.0f;
        public static readonly float GAME_MAX_SPEED = GAME_INITAIL_SPEED / 5f;
        public static readonly float GAME_DOWN_SPEED_MULTIPLIER = .5f;
        public static readonly float GAME_LEVEL_SPEED_MODIFIER = 0.25f;

        //SCORES
        public static readonly int GAME_LINE_SCORE = 100;
        public static readonly int GAME_COMBO_SCORE_BONUS = 50;
        public static readonly int GAME_SLAM_PER_BLOCK_SCORE = 1;

        //COLORS
        public static readonly Color BACKGROUND_COLOR = Color.FromArgb(255, 0, 0, 0);

        //FONT
        public static readonly int LARGEST_FONT_SIZE = 54;
        public static readonly int SMALLEST_FONT_SIZE = 10;
        public static readonly String DEFAULT_FONT_TYPE = "Arial";
    }
}
