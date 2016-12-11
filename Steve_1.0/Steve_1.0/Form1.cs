using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Steve_1._0;

namespace Steve_1._0
{
    public partial class Form1 : Form
    {
        #region Vars
        static Player Player;
        Button Start;
        public static Entity LogoBox;
        public static Random R = new Random();
        public static string Character = "Duck";
        public static bool ShowBorders = false;
        public static int FloorCount = 5;
        public static double FloorMovement = 12;
        public static Size GlobalSize = new Size(FloorCount * 121, 300);
        public static Point FloorLocation = new Point(0, 200);
        public static int RefreshRate = 30;
        public static int ObsChance = 40;
        public Panel GameBox = new Panel() { Size = GlobalSize, Location = new Point(0, 0), BorderStyle = BorderStyle.FixedSingle, AutoSize = false, AutoSizeMode = AutoSizeMode.GrowAndShrink };
        static Timer ScoreUpdater = new Timer() { Interval = 10 };
        static Timer JumpHandler = new Timer() { Interval = RefreshRate };
        static Timer MainScroller = new Timer() { Interval = RefreshRate };
        static Timer Info = new Timer() { Interval = ScoreUpdater.Interval };
        Queue<FloorPart> FLL;
        public static double Score = 0;
        public static Label label1;
        #endregion
        #region Main
        public Form1()
        {
            InitializeComponent();
            #region Horizonal_Timer
            MainScroller.Tick += HorizonalHandler_Tick;
            MainScroller.Start();
            #endregion
            #region Jump_Timer
            JumpHandler.Tick += JumpHandler_Tick;
            JumpHandler.Start();
            #endregion
            #region Score_Timer
            ScoreUpdater.Tick += ScoreUpdater_Tick;
            #endregion
            #region Info_Timer
            Info.Tick += Info_Tick;
            #endregion
            #region Form Properties
            MaximumSize = GlobalSize;
            MinimumSize = GlobalSize;
            ControlBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            KeyDown += Form1_KeyDown;

            #endregion
            #region Drawing Main Control
            Controls.Add(GameBox);
            #endregion
            #region FloorParts Creation
            FLL = new Queue<FloorPart>();
            for (int i = 0; i < FloorCount + 1; i++)
            {
                FloorPart temp = new FloorPart(R.Next(1, FloorPart.FloorParts + 1));
                if (i > 0)
                    temp.Location = new Point(FLL.Last().Location.X + FLL.Last().Width, FLL.Last().Location.Y);
                FLL.Enqueue(temp);
                GameBox.Controls.Add(FLL.Last());
            }
            #endregion
            #region Player Creation
            Player = new Player(Character);
            Player.BackColor = Color.Transparent;
            Player.Size = Player.Size.Resize(0.6);
            Player.Location = new Point(70, FloorLocation.Y - 30);
            GameBox.Controls.Add(Player);
            Player.BringToFront();
            #endregion
            #region Start Button Creation
            Start = new Button() { Location = new Point(GameBox.Size.Width / 2, GameBox.Size.Height / 2), Size = new Size(50, 50), Text = "Press To Start" };
            Start.FlatStyle = FlatStyle.Flat;
            Controls.Add(Start);
            Start.BringToFront();
            Start.Click += Start_Click;
            #endregion
            #region Score Label
            label1 = new Label();
            label1.Location = new Point(200, 50);
            label1.AutoSize = true;
            label1.Font = new Font("Serif", 16, FontStyle.Regular);
            label1.ForeColor = Color.Cyan;
            GameBox.Controls.Add(label1);
            label1.BringToFront();
            #endregion
            #region Logo Creation
            LogoBox = new Entity();
            LogoBox.SizeMode = PictureBoxSizeMode.StretchImage;
            LogoBox.ImageLocation = LogoBox.ImageDir + @"\logo.png";
            LogoBox.Location = new Point(200, 50);
            GameBox.Controls.Add(LogoBox);
            #endregion
        }
        #endregion
        #region Handlers
        void HorizonalHandler_Tick(object sender, EventArgs e)
        {
            if (!IsPlayerBlocked())
            {
                #region Floor Scroll
                if (FLL.Peek().Bounds.Right > 0)
                {
                    foreach (FloorPart C in FLL)
                    {
                        Point temp = C.Location;
                        temp.Offset(-Convert.ToInt32(FloorMovement), 0);
                        C.Location = temp;
                        if (C.HasObs())
                        {
                            temp = C.OBS.Location; temp.Offset(-Convert.ToInt32(FloorMovement), 0); C.OBS.Location = temp;
                        }
                    }
                    if (ScoreUpdater.Enabled)
                    {
                        if (!LogoBox.IsDisposed)
                        {
                            if (LogoBox.Bounds.Right > GameBox.Bounds.Left)
                            {
                                Point Temp = LogoBox.Location;
                                Temp.Offset(-(int)FloorMovement, 0);
                                LogoBox.Location = Temp;
                            }
                            else
                                LogoBox.Dispose();
                        }
                    }
                }
                else
                {
                    FLL.Peek().Dispose();
                    FLL.Dequeue();
                    bool HasSpace = !FLL.Last().HasObs();
                    FLL.Enqueue(new FloorPart(R.Next(1, FloorPart.FloorParts + 1)) { Location = new Point(FLL.Last().Location.X + FLL.Last().Width, FLL.Last().Location.Y) });
                     FLL.Peek().Parent.Controls.Add(FLL.Last());
                    if (HasSpace)
                    {
                        if (ScoreUpdater.Enabled)
                        {
                            FLL.Last().AddObs(ObsChance);
                            if (FLL.Last().HasObs())
                            {
                                FLL.Last().Parent.Controls.Add(FLL.Last().OBS);
                                FLL.Last().OBS.BringToFront();
                            }

                        }
                    }
                }
                #endregion
            }
            else
            {
                Game_Over();
            }
        }
        void JumpHandler_Tick(object sender, EventArgs e)
        {
            if ((Grounded() || IsPlayerBlocked()))
            {
                Player.Normal = 10;
                Player.OnAir = false;
                Player.YSpeed = 0;
            }
            else
            {
                Player.Normal = 0;
                Player.OnAir = true;
            }
            Player.YSpeed += Player.Normal - Player.Gravity;
            var temp = Player.Location;
            temp.Offset(0, -Player.YSpeed);
            Player.Location = temp;
        }
        void ScoreUpdater_Tick(object sender, EventArgs e)
        {

            Score += 0.1;
            FloorMovement += 0.005;
        }
        void Start_Click(object sender, EventArgs e)
        {
            Start.Dispose();
            ScoreUpdater.Start();
            Info.Start();
        }
        void Info_Tick(object sender, EventArgs e)
        {
            label1.Text = ((int)Score).ToString();
            label1.ForeColor = Color.FromArgb(0, (int)(R.Next(255) / 2), (int)(R.Next(255) / 2));
        }
        void Game_Over()
        {
            MainScroller.Enabled = false;
            JumpHandler.Enabled = false;
            Player.PicTimer.Enabled = false;
            Info.Enabled = false;
            Close();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                case Keys.Up:
                case Keys.W:
                    if (MainScroller.Enabled)
                        Player.Jump();
                    break;
                case Keys.Escape:
                    Application.Exit();
                    break;
                case Keys.Y:
                    if (Character == "Steve")
                        Character = "Duck";
                    else
                        Character = "Steve";
                    Player.SetChar(Character);
                    break;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private bool IsPlayerBlocked()
        {
            if (FLL.ElementAt(1).HasObs())
                return (FLL.ElementAt(1).OBS.Bounds.IntersectsWith(Player.Bounds));
            else
                return false;
        }
        private bool Grounded()
        {
            if (Player.Bounds.IntersectsWith(new Rectangle(0, 220, FLL.ElementAt(1).Width, 45)))
            {
                Player.Location = new Point(Player.Location.X, 226 - Player.Height);
                return true;
            }
            #region HH
            //else if (FLL.ElementAt(1).HasObs())
            //{
            //    Rectangle T = new Rectangle(FLL.ElementAt(1).OBS.Bounds.Left, FLL.ElementAt(1).OBS.Bounds.Top - 3, FLL.ElementAt(1).OBS.Bounds.Width, 3);
            //    if (Duck.Bounds.IntersectsWith(T))
            //        return true;
            //}
            #endregion
            return false;
        }
        #endregion

    }
}
