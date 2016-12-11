using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Steve_1._0;

namespace Steve_1._0
{

    class Player : Entity
    {
        #region Vars
        #region Private
        static int gravity = 10;
        private int normal = 10;
        private int jumpHeight = 6;
        private Timer picTimer = new Timer() { Interval = Form1.RefreshRate * 4 };
        private int ySpeed;
        private Point startLocation;
        private bool isJumping = false;
        private int Leg = 1;
        private int LegCount;
        #endregion
        #region Public
        public static int Gravity
        {
            get
            {
                return gravity;
            }

            set
            {
                gravity = value;
            }
        }
        public int JumpHeight
        {
            get
            {
                return jumpHeight;
            }

            set
            {
                jumpHeight = value;
            }
        }


        public Timer PicTimer
        {
            get
            {
                return picTimer;
            }

            set
            {
                picTimer = value;
            }
        }

        public int YSpeed
        {
            get
            {
                return ySpeed;
            }

            set
            {
                ySpeed = value;
            }
        }

        public Point StartLocation
        {
            get
            {
                return startLocation;
            }

            set
            {
                startLocation = value;
            }
        }

        public bool OnAir
        {
            get
            {
                return isJumping;
            }

            set
            {
                isJumping = value;
            }
        }

        public int Normal
        {
            get
            {
                return normal;
            }

            set
            {
                normal = value;
            }
        }

        #endregion
        #endregion
        #region Construction
        public Player(string Character)
        {
            if (Form1.ShowBorders)
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            ImageDir += Character.ToString();
            ImageLocation = ImageDir + @"\Stand.png";
            SizeMode = PictureBoxSizeMode.StretchImage;
            Image = Image.FromFile(ImageLocation);
            LegCount= System.IO.Directory.GetFiles(ImageDir+@"\Step\").Length;
            Size = Image.Size;
            PicTimer.Tick += Step_Timer;
            PicTimer.Start();
        }
        #endregion
        #region Timer Handlers
        private void Step_Timer(object sender, EventArgs e)
        {
            if (!OnAir)
            {
                Step();
                ImageLocation = ImageDir + @"\Step\" + Leg.ToString() + ".png";
            }
            else
            {
                ImageLocation = ImageDir + @"\Jump.png";
            }
        }
        #endregion
        #region Functions
        public void Jump()
        {
            if (!OnAir)
            {
                Normal = 0;
                StartLocation = Location;
                OnAir = true;
                ySpeed = Gravity * JumpHeight;
                Point t = Location;
                t.Offset(0, -ySpeed);
                Location = t;
            }
        }
        private void Forces(object sender, EventArgs e)
        {
            ySpeed += Normal - Gravity;
            var temp = Location;
            temp.Offset(0, -ySpeed);
            Location = temp;
        }
        public void Step()
        {
            if (Leg < LegCount)
                Leg++;
            else
            Leg = 1;
        }
        public void SetChar(string Ch)
        {
            ImageDir = new Entity().ImageDir + Ch.ToString();
            ImageLocation = ImageDir + @"\Stand.png";
            SizeMode = PictureBoxSizeMode.StretchImage;
            Image = Image.FromFile(ImageLocation);
            LegCount = System.IO.Directory.GetFiles(ImageDir + @"\Step\").Length;
        }
        #endregion
    }
}
