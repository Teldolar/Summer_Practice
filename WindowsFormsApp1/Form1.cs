using System;
using System.Drawing;
using System.Windows.Forms;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        int i = 0;
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
            cir = new circle[3];
            cir[0] = new circle(new Point(0, 200));
            cir[1] = new circle(new Point(900, 250));
            cir[2] = new circle(new Point(0, 300));
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
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            StartProcess();
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
        public Point position;
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
            beginpos = beginposition;
        }
        public void draw(Graphics context)
        {
            context.FillEllipse(color, new Rectangle(position, size));
        }

        public void StartMoving()
        {
            if(beginpos.X==0)
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
//Test 2