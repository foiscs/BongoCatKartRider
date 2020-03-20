using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Thread t;
        public bool[] last_keystroke = { false, false, false, false
                                     /*, false, false, false, false*/ };
        public bool[] keystroke = { false, false, false, false
                                /*, false, false, false, false*/ };
        public int[] key = { 17, 18, 16, 0
                         /*, 38, 40, 37, 39*/ };

        private Bitmap bg = Properties.Resources.empt;
        private Bitmap front = Properties.Resources.empt;


        public Form1()
        {
            InitializeComponent();

            Cat.Controls.Add(Hands);
            SetUp();
            Winking();
            FormClosed += new FormClosedEventHandler(this.Form1_FormClosed);
            Load += new EventHandler(this.Form1_Load);
        }
        private async void Winking()
        {
            await Task.Run(async () =>
            {
                Random rnd = new Random();
                while (true)
                {
                    await Task.Delay(rnd.Next(3000, 7000));
                    Cat.BackgroundImage = Properties.Resources.cat_wink;
                    await Task.Delay(rnd.Next(100, 250));
                    Cat.BackgroundImage = Properties.Resources.cat;
                }
            });
        }
        public void SetUp()
        {
            Hands.Image = Impose(front, Properties.Resources.unpressed_right,0,0);
            Hands.Image = Impose(front, Properties.Resources.unpressed_left, 270, 0);

            Cat.BackColor =  Color.White;
            Cat.BackgroundImage = Properties.Resources.cat;
            Cat.Image = Properties.Resources.piano;
        }

        private Bitmap SuperImpose(Bitmap largeBmp, Bitmap smallBmp, int x , int y)
        {
            Graphics g = Graphics.FromImage(largeBmp);
            g.CompositingMode = CompositingMode.SourceOver;
            smallBmp.MakeTransparent();
            g.DrawImageUnscaled(smallBmp, new Point(x, y));
            return largeBmp;
        }
        private Bitmap Impose(Bitmap largeBmp, Bitmap smallBmp, int x, int y)
        {
            Graphics g = Graphics.FromImage(largeBmp);
            g.CompositingMode = CompositingMode.SourceCopy;
            g.DrawImageUnscaled(smallBmp, new Point(x, y));
            return largeBmp;
        }
        private Bitmap UnSuperimpose(Bitmap largeBmp, Bitmap smallBmp, int x, int y)
        {
            for (int i = 0; i < smallBmp.Width; i++)
            {
                for (int j = 0; j < smallBmp.Height; j++)
                {
                    if (x + i >= 0 && x + i < largeBmp.Width
                        && y + j >= 0 && y + j < largeBmp.Height
                        && smallBmp.GetPixel(i, j).A != 0)
                    {
                        largeBmp.SetPixel(x + i, y + j, Color.Transparent);

                    }
                }
            }
            return largeBmp;
        }

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int nVirtKey);

        private void Form1_Load(object sender, EventArgs e)
        {
            t = new Thread(new ThreadStart(this.ThreadProc));
            t.Start();
        }
        private void ThreadProc()
        {
            while (true)
            {
                if (((int)Form1.GetAsyncKeyState(38) & 32768) > 0)
                    keystroke[0] = true;
                else
                    keystroke[0] = false;
                if (((int)Form1.GetAsyncKeyState(40) & 32768) > 0)
                    keystroke[1] = true;
                else
                    keystroke[1] = false;
                if (((int)Form1.GetAsyncKeyState(37) & 32768) > 0)
                    keystroke[2] = true;
                else
                    keystroke[2] = false;
                if (((int)Form1.GetAsyncKeyState(39) & 32768) > 0)
                    keystroke[3] = true;
                else
                    keystroke[3] = false;

                this.Invoke(new Action(() => DrawHands()));
                Thread.Sleep(10);
            }
        }
        private void DrawHands()
        {
            bool state = false;
            for (int i = 0; i < 4; i++)
            {
                if (last_keystroke[i] == false && keystroke[i] == true) 
                {
                    state = true;
                    last_keystroke[i] = true;
                }
                else if(last_keystroke[i] == true && keystroke[i] == false)
                {
                    state = true;
                    last_keystroke[i] = false;
                }
            }
            if(state)
            {
                if (keystroke[0] && keystroke[1])
                {
                    Hands.Image = Impose(front, Properties.Resources.pressed_key3, 0, 0);
                    Hands.BackgroundImage = SuperImpose(bg, Properties.Resources.key1, 87, 149);
                    Hands.BackgroundImage = SuperImpose(bg, Properties.Resources.key2, 163, 164);
                }
                else if (keystroke[0] || keystroke[1])
                {
                    if (keystroke[0])
                    {
                        Hands.BackgroundImage = UnSuperimpose(bg, Properties.Resources.key2, 163, 164);
                        Hands.BackgroundImage = SuperImpose(bg, Properties.Resources.key1, 87, 149);
                        Hands.Image = Impose(front, Properties.Resources.pressed_key1, 0, 0);
                    }
                    else
                    {
                        Hands.BackgroundImage = UnSuperimpose(bg, Properties.Resources.key1, 87, 149);
                        PressLastRight();
                    }
                    if (keystroke[1])
                    {
                        Hands.BackgroundImage = UnSuperimpose(bg, Properties.Resources.key1, 87, 149);
                        Hands.BackgroundImage = SuperImpose(bg, Properties.Resources.key2, 163, 164);
                        Hands.Image = Impose(front, Properties.Resources.pressed_key4, 0, 0);
                    }
                    else
                    {
                        Hands.BackgroundImage = UnSuperimpose(bg, Properties.Resources.key2, 163, 164);
                        PressLastRight();
                    }
                }
                else
                {
                    Hands.BackgroundImage = UnSuperimpose(bg, Properties.Resources.key1, 87, 149);
                    Hands.BackgroundImage = UnSuperimpose(bg, Properties.Resources.key2, 163, 164);
                    PressLastRight();
                }

                if (keystroke[2] && keystroke[3])
                {
                    Hands.Image = Impose(front, Properties.Resources.pressed_key6, 270, 0);
                    Hands.BackgroundImage = SuperImpose(bg, Properties.Resources.key3, 234, 181);
                    Hands.BackgroundImage = SuperImpose(bg, Properties.Resources.key4, 303, 195);
                }
                else if (keystroke[2] || keystroke[3])
                {
                    if (keystroke[2])
                    {
                        Hands.BackgroundImage = SuperImpose(bg, Properties.Resources.key3, 234, 181);
                        Hands.Image = Impose(front, Properties.Resources.pressed_key5, 270, 0);
                    }
                    else
                    {
                        Hands.BackgroundImage = UnSuperimpose(bg, Properties.Resources.key3, 234, 181);
                        PressLastleft();
                    }
                    if (keystroke[3])
                    {
                        Hands.BackgroundImage = SuperImpose(bg, Properties.Resources.key4, 303, 195);
                        Hands.Image = Impose(front, Properties.Resources.pressed_key8, 270, 0);
                    }
                    else
                    {
                        Hands.BackgroundImage = UnSuperimpose(bg, Properties.Resources.key4, 303, 195);
                        PressLastleft();
                    }
                }
                else
                {
                    Hands.BackgroundImage = UnSuperimpose(bg, Properties.Resources.key3, 234, 181);
                    Hands.BackgroundImage = UnSuperimpose(bg, Properties.Resources.key4, 303, 195);
                    PressLastleft();
                }
            }
        }
        private void PressLastRight()
        {
            try
            {
                if (keystroke[0])
                    Hands.Image = Impose(front, Properties.Resources.pressed_key1, 0, 0);
                else if (keystroke[1])
                    Hands.Image = Impose(front, Properties.Resources.pressed_key2, 0, 0);
                else
                    Hands.Image = Impose(front, Properties.Resources.unpressed_right, 0, 0);
            }
            catch
            {
                Hands.Image = Impose(front, Properties.Resources.unpressed_right, 0, 0);
            }
        }
        private void PressLastleft()
        {
            try {
                if (keystroke[2])
                    Hands.Image = Impose(front, Properties.Resources.pressed_key5, 270, 0);
                else if (keystroke[3])
                    Hands.Image = Impose(front, Properties.Resources.pressed_key8, 270, 0);
                else
                    Hands.Image = Impose(front, Properties.Resources.unpressed_left, 270, 0);
            }
            catch
            {
                Hands.Image = Impose(front, Properties.Resources.unpressed_left, 270, 0);
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.t.Abort();
        }
    }
}
