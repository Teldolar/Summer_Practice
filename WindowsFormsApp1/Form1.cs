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
            cir[0].EventMoveLeft+=cir[0].MoveLeft;
            cir[0].EventMoveRight+=cir[0].MoveRight;
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

            if((cir[i].beginpos.X==0&& cir[i].position.X == 900)||(cir[i].beginpos.X == 900 && cir[i].position.X == 0))
            {
                cir[i].EventMoveLeft-=cir[i].MoveLeft;
                cir[i].EventMoveRight-=cir[i].MoveRight;
                
                switch (i)
                {
                    case 2:
                        
                        cir[0].EventMoveLeft+=cir[0].MoveLeft;
                        cir[0].EventMoveRight+=cir[0].MoveRight;
                        i = 0;
                        break;
                    default:
                       
                        cir[i+1].EventMoveLeft+=cir[i+1].MoveLeft;
                        cir[i+1].EventMoveRight+=cir[i+1].MoveRight;
                         i++;
                        break;
                }
                cir[i].beginpos.X=cir[i].position.X;
            }
            cir[i].StartMoving();
        }
    }
    class circle
    {
        public Point position;
        Brush color;
        Size size;
        public Point beginpos;

        public event MoveRightDelegate EventMoveRight;
        public delegate void MoveRightDelegate();

        public event MoveLeftDelegate EventMoveLeft;
        public delegate void MoveLeftDelegate();

        public circle(Point beginposition)
        {
            this.color = Brushes.Red;
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
                this.color = Brushes.Red;
                EventMoveRight(); 
            }
            else
            {
                this.color = Brushes.Blue;
                EventMoveLeft(); 
            }
        }
        public void MoveRight()
        {
            position.X += 25;
        }
        public void MoveLeft()
        {
            position.X -= 25;
        }
    }
}