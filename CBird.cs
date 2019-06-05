using MoleShooterFinal.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoleShooterFinal
{
    class CBird : CImageBase
    {
        public CBird() : base(Resources.flyingbird)
        {
            _birdHotSpot.X = Left + 20;
            _birdHotSpot.Y = Top - 1;
            _birdHotSpot.Width = 34;
            _birdHotSpot.Height = 83;
        }
        private Rectangle _birdHotSpot = new Rectangle();

        public void Update(int X, int Y)
        {
            Left = X;
            Top = Y;
            _birdHotSpot.X = Left + 20;
            _birdHotSpot.Y = Top - 1;
        }
        public bool Hit(int X, int Y)
        {
            Rectangle c = new Rectangle(X, Y, 1, 1);
            if (_birdHotSpot.Contains(c))
            {
                return true;
            }
            return false;
        }
    }
}
