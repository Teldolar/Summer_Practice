using System;
using System.Media;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Time[] Objects;
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
            Objects = new Time[2];
            Objects[0] = new Time(10, false);
            Objects[1] = new Time(5, true);
            Objects[0].stoptime += Objects[1].StartTimer;
            Objects[1].stoptime += Objects[0].StartTimer;
        }
        private void stop(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            Objects[0].NewTime();
            Objects[1].NewTime();
            Invalidate();
        }
    }
    class Time
    {


        bool stop;
        int count = 0;
        int Minutes;
        public event StopTime stoptime;
        public delegate void StopTime();
        public Time(int minutes, bool bool_)
        {
            Minutes = minutes;
            stop = bool_;
        }
        public void NewTime()
        {

            if (stop == false)
            {
                if (count == 0)
                {
                    playSimpleSound();
                }
                count++;
                if (count == Minutes)
                {

                    stoptime();
                    count = 0;
                    stop = true;
                }
            }
        }
        public void StartTimer()
        {
            stop = false;
        }
        private void playSimpleSound()
        {
            SoundPlayer simpleSound = new SoundPlayer();
            simpleSound.Stream = Properties.Resources.sound;
            simpleSound.Play();
        }
    }
}
