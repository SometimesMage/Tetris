using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris {
    //Created by Nick Peterson and Daric Sage
    //Features for Possible Extra Credit Include:
    //Dynamically resizable
    //Ghost Game Piece
    static class Program {
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
