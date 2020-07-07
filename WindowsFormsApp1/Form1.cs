using System;
using System.Drawing;
using System.Windows.Forms;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        circle[] cir;
        MainMenu MnMen1;
        MenuItem pnkt1;
        MenuItem pnkt2;
        public Form1()
        {
            InitializeComponent();
            pnkt1 = new MenuItem("Пуск", new EventHandler(timer1_Tick), Shortcut.Alt1);
            pnkt2 = new MenuItem("Стоп", new EventHandler(stop), Shortcut.Alt2);
            MnMen1 = new MainMenu(new MenuItem[] { pnkt1, pnkt2 });
            this.Menu = MnMen1;

            cir = new circle[4];
            cir[0] = new circle("Right",Brushes.Red, new Point(-50, 250),false);
            cir[1] = new circle("Left", Brushes.Blue, new Point(985, 250),true);
            cir[2] = new circle("Right", Brushes.Yellow, new Point(-50, 350), true);
            cir[3] = new circle("Left", Brushes.Purple, new Point(985, 350), false);
        }

        private void stop(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            cir[0].draw(e.Graphics);
            cir[1].draw(e.Graphics);
            cir[2].draw(e.Graphics);
            cir[3].draw(e.Graphics);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            StartProcess(cir[0], cir[1]);
            StartProcess(cir[3], cir[2]);
            Invalidate();
        }
        private void StartProcess(circle a, circle b)
        {
            if (b.stop == true)
            {
                a.stop = false;
                a.Move();
            }
            if (a.stop == true)
            {
                b.stop = false;
                b.Move();
            }
        }
    }
    class circle
    {
        string direction;
        Point position;
        Brush color;
        Size size;

        public bool stop;
        public event StopMoveDelegate EventStopMoving;
        public delegate void StopMoveDelegate();

        public circle(string newdirection,Brush brush,Point beginposition,bool stopcircle)
        {
            EventStopMoving += StopMoving;
            this.color = brush;
            this.size = new Size(50, 50);
            position = beginposition;
            direction = newdirection;
            stop = stopcircle;
        }
        public void draw(Graphics context)
        {
            context.FillEllipse(color, new Rectangle(position, size));
        }
        public void Move()
        {
            if (direction=="Right")
            {
                position.X += 5;
                if (position.X >= 985 && stop==false)
                {
                    EventStopMoving();
                }
            }
            else
            {
                position.X += -5;
                if (position.X <= -50 && stop==false)
                {
                    EventStopMoving();
                }
            }

        }
        public void StopMoving()
        {
            if (direction == "Right")
            {
                position.X = -50;
                stop = true;
            }
            else
            {
                position.X = 985;
                stop = true;
            }
        }
    }
}
/*Test*/
