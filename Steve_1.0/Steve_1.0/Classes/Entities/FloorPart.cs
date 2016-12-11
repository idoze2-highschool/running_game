using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Steve_1._0
{
    class FloorPart : Entity
    {
        #region Vars
        private static int floorParts;
        public static int FloorParts
        {
            get { return FloorPart.floorParts; }
            set { FloorPart.floorParts = value; }
        }
        private int partId;
        public int PartId
        {
            get { return partId; }
            set { partId = value; }
        }
        private Obstacle oBS = null;
        public Obstacle OBS
        {
            get { return oBS; }
            set { oBS = value; }
        }
        #endregion
        #region Construction
        public FloorPart(int PartId)
        {
            if (Form1.ShowBorders)
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            Location = new Point(0, Form1.FloorLocation.Y);
            ImageDir += @"\Entities\Floorpart\";
            floorParts = System.IO.Directory.GetFiles(ImageDir).Length;
            this.PartId = PartId;
            ImageLocation = ImageDir + PartId + ".png";
            Image Image = Image.FromFile(ImageLocation);
            SizeMode = PictureBoxSizeMode.StretchImage;
            Size = Image.Size;
        } 
        #endregion
        void FloorPart_Disposed(object sender, EventArgs e)
        {
            if (HasObs())
                oBS.Dispose();
        }
        public static Size SizeOfPart(int Id)
        {
            return Image.FromFile(new FloorPart(Id).ImageDir).Size;
        }
        public void AddObs()
        {
            int Part = Form1.R.Next(1,3);
            oBS = new Obstacle(Part, (Control)this, new Point(Form1.R.Next(0, this.Size.Width - Obstacle.SizeOfPart(Part).Width) + this.Location.X, this.Location.Y - Obstacle.SizeOfPart(Part).Height+10));
        }
        public void AddObs(int Chance)
        {
            if (Form1.R.Next(1, 101) <= Chance)
                AddObs();
        }
        public bool HasObs()
        {
            return OBS != null;
        }
    }
}
