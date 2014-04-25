using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Alchemy;
using Alchemy.Classes;

namespace vocaNODPushToTalkCs
{
    public delegate void onBrowserConnected();
    public delegate void onBrowserDisconnected();
    class wsServer 
    {
        public static event onBrowserConnected OnBrowerConnectedEvent;
        public static event onBrowserDisconnected OnBrowerDisconnectedEvent;
        private static UserContext context;
        private static WebSocketServer aServer;
        public static void run() {
            //subscribe to the right event
            InterceptKeys.OnKeyPressedEvent += new onKeyPressed(listenKeyPressed);
            InterceptKeys.OnKeyReleasedEvent += new onKeyReleased(listenKeyReleased);
            MouseHook.OnMousePressedEvent += new onMousePressed(listenMousePressed);
            MouseHook.OnMouseReleasedEvent += new onMouseReleased(listenMouseReleased);
            //We're running the server locally 
            aServer = new WebSocketServer(3939, IPAddress.Parse("127.0.0.1"))
            {
                OnConnected = OnConnected,
                OnDisconnect = OnDisconnect,
                TimeOut = new TimeSpan(0, 5, 0)
            };

            aServer.Start();
            

        }

        public static void StopServer()
        {

            aServer.Stop();
        }


        //This is what we send, it's simple, when the user press the key he assigned, and when he releases it
        private static void listenKeyPressed() {
            if (context != null)
                context.Send("pressed");
        }

        private static void listenKeyReleased()
        {
            if (context != null)
                context.Send("released");
        }

        private static void listenMousePressed()
        {
           
            if (context != null)
                context.Send("pressed");
        }

        private static void listenMouseReleased()
        {
            if (context != null)
                context.Send("released");
        }

        

        static void OnConnected(UserContext aContext)
        {
            context = aContext;
            OnBrowerConnectedEvent();
            
        }


        static void OnDisconnect(UserContext aContext)
        {
            OnBrowerDisconnectedEvent();
        }
  
    }
}
