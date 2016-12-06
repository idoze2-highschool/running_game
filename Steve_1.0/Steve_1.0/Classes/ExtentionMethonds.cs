using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Steve_1._0
{
    public static class ExtentionMethonds
    {
        public static System.Drawing.Size Resize(this Size Org, double Modifier)
        {
            return new Size(Convert.ToInt32(Org.Width * Modifier),Convert.ToInt32(Org.Height * Modifier));
        }
        public static Point RelativeLocation(this Control cont,Control Parent)
        {
            Control c = cont;
            Point retval = new Point(0, 0);
            for (; c.Parent != Parent; c = c.Parent)
            { retval.Offset(c.Location); }
            return retval;
        }
    }
}
