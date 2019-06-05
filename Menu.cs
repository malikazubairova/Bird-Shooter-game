using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoleShooterFinal
{
    class Menu 
    {
        int cenX;
        int cenY;
        int offsX;
        int offsY;

        public Menu(int Cx, int Cy, int offX, int offY)
        {
            cenX = Cx;
            cenY = Cy;
            offsX = offX;
            offsY = offY;
        }
        public void Draw(Graphics b, Pen p)
        {
            Rectangle Menu1 = new Rectangle(cenX - 139 + offsX, cenY + offsY, 297, 43);
            // b.FillRectangle(BrShirt, Menu1);
            b.DrawRectangle(p, Menu1);

            Point[] Menu2 = new Point[]{
                    new Point(cenX - 139 + offsX, cenY + offsY),
                    new Point(cenX - 139 + offsX, cenY  + 20 + offsY),

                    new Point(cenX - 139 + offsX, cenY + 40 + offsY),
                    new Point(cenX - 139 + offsX, cenY + 60 + offsY)
            };
            //   b.FillPolygon(BrShirt, Menu2);
            b.DrawPolygon(p, Menu2);


        }
    }
}
