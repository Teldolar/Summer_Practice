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
            cir = new circle[3];
            cir[0] = new circle(new Point(0, 200),"Right",false);
            cir[1] = new circle(new Point(930, 250),"Left",true);
            cir[2] = new circle(new Point(0, 300),"Right",true);
            cir[0].stopmoving+=cir[1].StartMoving;
            cir[1].stopmoving+=cir[2].StartMoving;
            cir[2].stopmoving+=cir[0].StartMoving;
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
        private void StartProcess()
        {
            cir[0].Moving();
            cir[1].Moving();
            cir[2].Moving();
        }
    }
    class circle
    {
        public Point position;
        Brush color;
        Size size;
        bool stop;
        public event StopMovingDelegate stopmoving;
        public delegate void StopMovingDelegate();
        string direction;

        public circle(Point beginposition, string Direction, bool Stop)
        {
            this.color = Brushes.Red;
            this.size = new Size(50, 50);
            position = beginposition;
            direction = Direction;
            stopmoving+=StopMoving;
            stop=Stop;
        }
        public void draw(Graphics context)
        {
            context.FillEllipse(color, new Rectangle(position, size));
        }
        public void Moving()
        {
            if(stop==false)
            {
                if(direction=="Right")
                {
                    position.X+=10;
                }
                if(direction=="Left")
                {
                    position.X-=10;
                }
                if(direction=="Right"&&position.X==930)
                {
                    stopmoving();
                    direction="Left";
                }
                if(direction=="Left"&&position.X==0)
                {
                    stopmoving();
                    direction="Right";
                }
            }
        }
        public void StopMoving()
        {
            stop=true;
        }
        public void StartMoving()
        {
            stop=false;
        }
    }
}