using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NarcoFeeder.Helpers;
using System.Threading;

namespace NarcoFeeder
{
    public partial class frmMain : Form
    {
        private const int WM_HOTKEY = 0x0312;
        private const int SECOND = 1000;
        private const int MINUTE = 60 * SECOND;

        private System.Windows.Forms.Timer nextActionTimer;
        private System.Windows.Forms.Timer playerFeedTimer;
        private Win32.Point ffLocation;
        private Win32.Point nLocation;
        private Win32.Point ccpLocation;
        private Random rnd;
        

        public frmMain()
        {
            InitializeComponent();

            nextActionTimer = new System.Windows.Forms.Timer();
            nextActionTimer.Enabled = false;
            nextActionTimer.Tick += new EventHandler(FeedTimerTick);
            nextActionTimer.Interval = 5 * SECOND;

            playerFeedTimer = new System.Windows.Forms.Timer();
            playerFeedTimer.Enabled = false;
            playerFeedTimer.Tick += new EventHandler(playerFeedTimerTick);
            playerFeedTimer.Interval = 29 * SECOND;

            Win32.RegisterHotKey(this.Handle, 0, 0, 0x6A); // numeric multiply
            Win32.RegisterHotKey(this.Handle, 1, 0, 0x6B); // numeric +
            Win32.RegisterHotKey(this.Handle, 2, 0, 0x6D); // numeric -
            Win32.RegisterHotKey(this.Handle, 3, 0, 0x7A); // F11

            feedDelayText.Text = nextActionTimer.Interval.ToString();
            feedDelay1.Text = playerFeedTimer.Interval.ToString();
            typeSelector.SelectedIndex = 0;
            typeSelector1.SelectedIndex = 0;
            rnd = new Random();

            Console.Out.WriteLine("NarcoFeeder Initialized.");

        }

        private void FeedTimerTick(Object myObject, EventArgs myEventArgs)
        {
            timerHandler(nextActionTimer, typeSelector);
        }

        private void playerFeedTimerTick(Object myObject, EventArgs myEventArgs)
        {
            if (typeSelector1.SelectedIndex == 1)
            {
                Win32.SendKey(textBox1.Text);
            }
        }

        private void timerHandler(System.Windows.Forms.Timer t, ComboBox s)
        {
            Thread.Sleep(rnd.Next(100));
            Console.Out.WriteLine("TimerTick. " + t.Interval);
            if (s.SelectedIndex == 1)
            {
                Win32.MoveMouse(nLocation);
                Thread.Sleep(100);
                Win32.SendMouseClick(nLocation, 100);
                Thread.Sleep(100);
                Win32.MoveMouse(ffLocation);
                Thread.Sleep(600);
                Win32.SendMouseClick(ffLocation, 50);
            }
            else if (s.SelectedIndex == 2)
            {
                Win32.SendMouseClick(ccpLocation, 30);
            }
            else if (s.SelectedIndex == 3)
            {
                Win32.SendMouseDoubleClick(ccpLocation);
            }
            else if (s.SelectedIndex == 4)
            {
                Win32.SendKey("t");
            }
            else if (s.SelectedIndex == 5)
            {
                Win32.SendKey("0");
            }
            else if (s.SelectedIndex == 6)
            {
                Win32.SendKey("e");
            }
            else if (s.SelectedIndex == 7)
            {
                Win32.SendKey("o");
            }
            else if (s.SelectedIndex == 8)
            {
                Win32.SendMouseRClick(ccpLocation,30);
            }

        }


        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_HOTKEY)
            {
                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);                  // The key of the hotkey that was pressed.
                int id = m.WParam.ToInt32();                                        // The id of the hotkey that was pressed.

                if (id == 0 || id == 3) //start-stop
                {
                    if (!nextActionTimer.Enabled && !playerFeedTimer.Enabled)
                    {
                        Win32.Rect r = Win32.GetARKRectangle();
                        if (r.Right != 0)
                        {
                            nextActionTimer.Enabled = typeSelector.SelectedIndex!=0;
                            statusLabel.Text = "Feeder switched on.";
                            Console.Out.WriteLine("Feeder switched on. " + r.Right + " " + r.Bottom);
                            ccpLocation = Win32.GetCurrentCursorPoint();
                            Console.Out.WriteLine("CCP. " + ccpLocation.x + " " + ccpLocation.y);
                            ffLocation.x = 1055 * r.Right / 1920;
                            ffLocation.y = 768 * r.Bottom / 1080;
                            if (r.Bottom == 1024 && r.Right == 1600)
                            {
                                ffLocation.y = 700;
                            }
                            nLocation.x = 920 * r.Right / 1920;
                            nLocation.y = 320 * r.Bottom / 1080;

                            playerFeedTimer.Enabled = typeSelector1.SelectedIndex != 0;
                        }
                        else
                        {
                            Console.Out.WriteLine("ARK window not found.");
                            statusLabel.Text = "ARK window not found. " + feedDelayText.Text + " " + typeSelector.SelectedIndex;
                            //                            nextActionTimer.Enabled = true;
                        }
                    }
                    else
                    {
                        Console.Out.WriteLine("Feeder switched off.");
                        statusLabel.Text = "Feeder switched off.";
                        nextActionTimer.Enabled = false;
                        playerFeedTimer.Enabled = false;
                    }
                }
                else if (id == 1 ) // increaserate
                {
                    if (nextActionTimer.Interval > 100)
                    {
                        nextActionTimer.Interval -= 100; // one tenth second
                    }
                    feedDelayText.Text = nextActionTimer.Interval.ToString();

                }
                else if (id == 2) // decreaserate
                {
                    nextActionTimer.Interval += 100;
                    feedDelayText.Text = nextActionTimer.Interval.ToString();
                }

            }
        }


        private void feedDelayText_TextChanged(object sender, EventArgs e)
        {
            int pint = nextActionTimer.Interval;
            if (String.IsNullOrWhiteSpace(feedDelayText.Text)) return;
            pint = int.Parse(feedDelayText.Text);
            if (nextActionTimer.Interval != pint && pint > 10)
            {
                nextActionTimer.Interval = pint;
            }
        }
        private void feedDelay1_TextChanged(object sender, EventArgs e)
        {
            int pint = playerFeedTimer.Interval;
            if (String.IsNullOrWhiteSpace(feedDelay1.Text)) return;
            pint = int.Parse(feedDelay1.Text);
            if (playerFeedTimer.Interval != pint && pint > 10)
            {
                playerFeedTimer.Interval = pint;
            }
        }

    }
}
