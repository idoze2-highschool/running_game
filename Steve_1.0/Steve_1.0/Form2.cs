using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Steve_1._0
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            Location = new Point(300, 300);
            StartPosition = FormStartPosition.Manual;
            button2.Select();
            KeyDown += Form2_KeyDown;
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyData)
            {
                case Keys.Enter:
                    button2_Click(sender, e);
                    break;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = "You Scored: "+((int)Form1.Score).ToString()+".";
            Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
