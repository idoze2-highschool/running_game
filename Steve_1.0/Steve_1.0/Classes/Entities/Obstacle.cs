using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Steve_1._0
{
    class Obstacle : Entity
    {
        #region Vars
        int objId;
        static int ObsCount;
        Control Floor;
        public int ObjId
        {
            get { return objId; }
            set { objId = value; }
        }
        #endregion
        #region Ctors
        private Obstacle(int ObjId)
        {
            if (Form1.ShowBorders)
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.objId = ObjId;
            ImageDir += @"Entities\Obstacles\";
            ImageLocation = ImageDir + objId + ".png";
            SizeMode = PictureBoxSizeMode.StretchImage;
            Image Image = Image.FromFile(ImageLocation);
            BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            Size = Image.Size;
            BackColor = Color.Transparent;
            ObsCount = System.IO.Directory.GetFiles(ImageDir).Length;
        }
        public Obstacle(int ObjId, Control Floor, Point Location)
            : this(ObjId)
        {
            this.Floor = Floor;
            this.Location = Location;
        }
        #endregion
        #region Handlers
        #endregion
        #region Functions
        public static System.Drawing.Size SizeOfPart(int Id)
        {
            return Image.FromFile(new Obstacle(Id).ImageLocation).Size;
        }
        #endregion
    }
}
