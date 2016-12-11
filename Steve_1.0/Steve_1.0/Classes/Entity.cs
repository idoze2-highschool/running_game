using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Steve_1._0
{
    public class Entity:PictureBox
    {
        private string imageDir = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf("Running Game") + ("Running Game").Length) + @"\Images\";
        public string ImageDir { get { return imageDir; } set { imageDir = value; } }
        //public static string GetImageDir() { return new Entity().ImageDir; }
        public Entity()
        {
            BackColor = Color.Transparent;
        }
    }
}
