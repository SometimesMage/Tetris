using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetris
{
    public abstract class TComponents<T>   //T for tetris
    {

        protected Rectangle _box;
        protected String _title;
        protected T _detail;

        public TComponents(String title)
        {
            _box = new Rectangle();
            _title = title;
        }

        public T detail
        {
            get
            {
                return _detail;
            }
            set
            {
                _detail = value;
            }
        }

        public Rectangle box
        {
            get
            {
                return _box;
            }
            set
            {
                _box = value;
            }
        }

        public abstract void draw(Graphics g);

    }
}
