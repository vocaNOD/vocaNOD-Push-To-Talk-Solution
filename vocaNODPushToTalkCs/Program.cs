using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace vocaNODPushToTalkCs
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            //Running the webSocket server and the hooks on keyboard and mouse
            InterceptKeys.RunKeyHook();
            MouseHook.runMouseHook();
            wsServer.run();
            Application.Run(new Form1());
            // Stopping  the webSocket server and the hooks on keyboard and mouse
            wsServer.StopServer();
            InterceptKeys.StopKeyHook();
            MouseHook.StopMouseHook();
            Application.Exit();
            System.Environment.Exit(1);
            
        }
    }
}
