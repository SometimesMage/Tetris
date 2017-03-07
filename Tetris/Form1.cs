using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris {
    public partial class mainForm : Form {




        public mainForm() {

            InitializeComponent();
            //in partial class set inital disables
            
        }

        protected override void OnPaint(PaintEventArgs e)
        {



            base.OnPaint(e);
        }

        private void mstripNew_Click(object sender, EventArgs e)
        {
            //start game and setup
            //??disable the 'game' mstrip??
            //enable 'pause' mstrip
        }

        private void mstripSave_Click(object sender, EventArgs e)
        {
            this.saveFileDialog.ShowDialog();
        }

        private void mstripLoad_Click(object sender, EventArgs e)
        {
            this.openFileDialog.ShowDialog();
        }

        private void mstripExit_Click(object sender, EventArgs e)
        {
            //this.close()
        }

        private void mstripGo_Click(object sender, EventArgs e)
        {
            //resume timer
            //disable pause
            //??disable 'game' mstrip??
        }

        private void mstripPause_Click(object sender, EventArgs e)
        {
            //pause the timer
            //disable go
            //??enable 'game' mstrip??
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //ask if they want to save?
        }

        private void mainForm_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void mainForm_KeyUp(object sender, KeyEventArgs e)
        {

        }

    }//form
}//namespace tetris
