using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Timers;
namespace vocaNODPushToTalkCs
{
           // Here we declare the delegates of this class
        public delegate void onActive();
        public delegate void onDeactive();
        public delegate void onWaitKeyAssigned();
        public delegate void ChangeKeyFromGui();
        public delegate void myDelegate();
  
    public partial class Form1 : Form
    {

        public static event onActive Activation;
        public static event onDeactive Deactivation;
        // Here are the variables needed
        private ContextMenu ctxMenu;
        private MenuItem openMenuItem;
        private MenuItem closeMenuItem;
        private bool waitForKeyAssigning = false;
        private char keyAssigned;

        //This is a timer used to wait if GUI has caught a key or not, important when you want to assign a control key (ctrl, shift, capital...)
        //Those one will not be caught by GUI but will be caught by the Windows hook (that catches every keystroke)
        // That's why we use the timer, you'll see that later on
        private static System.Timers.Timer aTimer;


        public Form1()
        {
            // This one is needed since we use Windows form
            InitializeComponent();

            // Here we subscribe to events
            InterceptKeys.OnKeyAssignedChangedEvent += new onKeyAssignedChanged(listenEventKeyAssignedChanged);
            wsServer.OnBrowerConnectedEvent += new onBrowserConnected(listenOnBrowserConnected);
            wsServer.OnBrowerDisconnectedEvent += new onBrowserDisconnected(listenOnBrowserDisconnected);
            MouseHook.OnMouseKeyAssignedChangedEvent += new onMouseKeyAssignedChanged(listenEventMouseKeyAssignedChanged);
            Form1.Activation += new onActive(listenActivation);
            Form1.Deactivation += new onDeactive(listenDeactivation);


            CreateContextMenu();
            aTimer = new System.Timers.Timer(50); // The timer is set to 50 ms which will be sufficient
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            
        }


        private void CreateContextMenu()
        {
            //This context menu will be used for the notification icon
            // When you'll press right click on it, il will display it
            ctxMenu = new ContextMenu();
            openMenuItem = new MenuItem();
            openMenuItem.Text = "Open";
            ctxMenu.MenuItems.Add(openMenuItem);
            openMenuItem.Click += new EventHandler(this.openMenuItem_onClick);
            closeMenuItem = new MenuItem();
            closeMenuItem.Text = "Close";
            ctxMenu.MenuItems.Add(closeMenuItem);
            closeMenuItem.Click += new EventHandler(this.closeMenuItem_onClick);
            this.notifyIcon1.ContextMenu = ctxMenu;

        }

        private void openMenuItem_onClick(object sender, EventArgs e)
        {
            this.Show();
            this.ShowInTaskbar = true;
            this.notifyIcon1.Visible = false;
        }

        private void closeMenuItem_onClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            //We need to override the OnClosing event to minimize the window to the notification bar
            this.ShowInTaskbar = false;
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.ShowBalloonTip(100,this.notifyIcon1.BalloonTipTitle,this.notifyIcon1.BalloonTipText,this.notifyIcon1.BalloonTipIcon);
            this.Hide();
            e.Cancel = true;
            base.OnClosing(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            
            Console.WriteLine("onClosed");
            MessageBox.Show("coucou");
            base.OnClosed(e);
        }

        private void listenOnBrowserConnected()
        {
            //If the browser is connected, this will show a green light
            this.DotConnection.Image = vocaNODPushToTalkCs.Properties.Resources.green;
        }

        private void listenOnBrowserDisconnected()
        {
            //If the browser is not connected, this will show a red light
            this.DotConnection.Image = vocaNODPushToTalkCs.Properties.Resources.red;
        }

        private void listenEventKeyAssignedChanged (int key) 
        {
            //This is what happens when we catch the event KeyAssignedChanged from the Windows Hook
            // Since there are qwerty and azerty keyboard, we need to create something that can
            // handle every kind of keyboard
            // Since special characters caught by the windows hook are not explicit (for example OemPeriod for ; in french keyboard)
            // We prefer using the KeyPress event of the GUI (which will display ; for ; and not OemPeriod)
            // But the GUI does not catch  control,shift, capital and others key
            // So we want to use this event to get round this deficiency
            // That's what happens here, we set a timer. At the end of this timer, this will deactivate listening the key 
            // for assigning. So if you type control (not caught by gui), it will display control enventually
            // If you type "a", it will display the "a" caught by GUI.
            // This is a trick, this is not beautiful, but it works.
            // It works because Windows Hook event is caught first.


            //Enjoy

            if (waitForKeyAssigning)
            {
                if(((Keys)key).ToString() == "Back") // Back makes a weird sign if caught by GUI so you have to deactivate listening right away
                    Invoke(new myDelegate(listenDeactivation));
                this.toolStripStatusLabel1.Text = "You wish to assign key : " + ((Keys)key).ToString();
                this.keyAssignedLabel.Text = "Key assigned : " + ((Keys)key).ToString();
                aTimer.Enabled = true;
            }
            }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            aTimer.Enabled = false;
            if (InvokeRequired)
            {
                // At the end of the timer, we deactivate the assign key possibility
                // InvokeRequired is necessary here since this event doesn't take place is the same thread
                // as the original one
                Invoke(new myDelegate(listenDeactivation));
            }
        }

         
       

        private void listenEventMouseKeyAssignedChanged(String mouseKey)
        {
            // We have no particular problem with the mouse key assigning, so we don't need
            // timer and tricks
            if (waitForKeyAssigning)
            {

                
                this.toolStripStatusLabel1.Text = "You wish to assign key : " + mouseKey;
                this.keyAssignedLabel.Text = "Key assigned : " + mouseKey;
                Deactivation();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // Firing a event that will activate the listening of keys for assigning one to puh to talk
            Activation();
        }

        private void listenActivation()
        {
            // This catch the activation event, we subscribe a KeyPressEvent on the form
            waitForKeyAssigning = true;
            this.assignKeyButton.Text = "Type a key";
            this.toolStripStatusLabel1.Text = "Type a key";
            this.assignKeyButton.KeyPress += new KeyPressEventHandler(button1_OnKeyPress);
        }

        private void listenDeactivation()
        {
            // This catch the deactivation event, we unsubscribe here since the GUI doesn't need to
            // listen to keys pressed anymore
            waitForKeyAssigning = false;
           this.assignKeyButton.Text = "Assign a key";
           this.assignKeyButton.KeyPress -= new KeyPressEventHandler(button1_OnKeyPress);
        }



        private void assignKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activation();
        }

        private void helpAndAboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This application lets you enable push to talk to the vocaNOD website. \r \r" +
               "You need to click on the 'Assign a key' button (or in the Menu Panel) then to type the key" +
               "you want enabled for push to talk, it can be a keyboard key or a mouse button. \r \r" +
               "The state dot is a visual indicator to tell whether the application is connected or not to"+
               "your web browser. If you opened the executable before opening the web browser then it should be green"+
               "otherwise you need to click on the 'Enable push to talk' button. Once you assigned a key and the "+
               "indicator is green, the push to talk option is enabled. \r \r" +
               "© vocaNOD 2014", "Help and About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void notifyIcon1_Click(object sender, MouseEventArgs e)
        {
            // When you left click to the notification icon, it shows the window
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.ShowInTaskbar = true;
                this.Show();
            }
        }

        protected override void OnLoad(EventArgs e)
        {

            Properties.Settings.Default.Upgrade();
            Console.WriteLine(Properties.Settings.Default.CharSaved.Equals('\0'));
            
            if (!Properties.Settings.Default.CharSaved.Equals('\0'))
            {
                this.keyAssignedLabel.Text = "Key assigned : " + Properties.Settings.Default.CharSaved;
            }
            base.OnLoad(e);
        }

        private void button1_OnKeyPress(object sender,KeyPressEventArgs e)
        {
            // this catches key press when activation has been fired, this is the GUI keyboard hook
            if (waitForKeyAssigning)
            {
                keyAssigned = e.KeyChar;
                Properties.Settings.Default.CharSaved = keyAssigned.ToString();
                this.toolStripStatusLabel1.Text = "You wish to assign key : " + keyAssigned;
                this.keyAssignedLabel.Text = "Key assigned : " + keyAssigned;
                Deactivation();
            }
        }

    }
}

