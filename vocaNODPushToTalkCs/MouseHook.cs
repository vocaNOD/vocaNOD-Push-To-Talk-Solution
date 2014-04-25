using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

// Pretty much the same as the class InterceptKeys
// The Windows mouse hook here used have some particularities
// The system sees the usual inputs of mouse (left, right,middle and mousewheel (but not used here)
// as well as some not so usual inputs, called XBUTTON
// So actually we can parse usual inputs but we have to know what Xbutton has been pressed, then display it

// That's what happens in this class

// The events and booleans remain the same


//largely inspired from http://blogs.msdn.com/b/toub/archive/2006/05/03/589468.aspx

namespace vocaNODPushToTalkCs
{

    public delegate void onMousePressed();
    public delegate void onMouseReleased();
    public delegate void onMouseKeyAssignedChanged(String mouseKey);
    class MouseHook
    {
        private static LowLevelMouseProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;
        public static event onMousePressed OnMousePressedEvent;
        public static event onMouseReleased OnMouseReleasedEvent;
        public static event onMouseKeyAssignedChanged OnMouseKeyAssignedChangedEvent;
        private static bool activated;
        private static bool pressedBoolean;
        private static int mouseKeyCodeAssigned;
        private static bool mousePttActivated;

        public static void runMouseHook()
        {
            _hookID = SetHook(_proc);
            activated = false;
            if (Properties.Settings.Default.MouseKeyCodeSaved == 0)
            {
                mousePttActivated = false;
            }
            else
            {
                Console.WriteLine("not = 0 ");
                mousePttActivated = true;
                mouseKeyCodeAssigned = Properties.Settings.Default.MouseKeyCodeSaved;

            }
            Form1.Activation += new onActive(listenActivation);
            Form1.Deactivation += new onDeactive(listenDeactivation);
            InterceptKeys.OnKeyAssignedChangedEvent += new onKeyAssignedChanged(listenOnKeyAssignedChanged);
            MouseHook.OnMouseKeyAssignedChangedEvent += new onMouseKeyAssignedChanged(listenOnMouseKeyAssignedChanged);
          
        }

        public static void StopMouseHook() {
            UnhookWindowsHookEx(_hookID);
        }

        private static void listenActivation(){
            activated = true;
        }

        private static void listenDeactivation()
        {
            activated = false;
        }

        private static void listenOnKeyAssignedChanged(int key) {
            mousePttActivated = false;
        }

        private static void listenOnMouseKeyAssignedChanged(String mouseKey) {

            mousePttActivated = true;
        
        }

        private static IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {

           
            switch ((MouseMessages)wParam)
            {

                case MouseMessages.WM_RBUTTONUP: // When the key has been released
                    {
                        if (mouseKeyCodeAssigned == (int)MouseMessages.WM_RBUTTONUP  && mousePttActivated && !activated) {
                            OnMouseReleasedEvent();
                            pressedBoolean = false;
                        }
                        else if (activated)
                        {
                            OnMouseKeyAssignedChangedEvent("Right click");
                            mouseKeyCodeAssigned = (int)MouseMessages.WM_RBUTTONUP;
                            Properties.Settings.Default.MouseKeyCodeSaved = (int)MouseMessages.WM_RBUTTONUP;
                        }
                       
                    }
                    break;

                case MouseMessages.WM_RBUTTONDOWN: // When the key has been pressed
                    {

                        if (mouseKeyCodeAssigned == (int)MouseMessages.WM_RBUTTONUP && mousePttActivated && !pressedBoolean)
                        {
                            OnMousePressedEvent();
                            pressedBoolean = true;
                        }
                    
                       
                    }
                    break;

                case MouseMessages.WM_LBUTTONUP: // When the key has been released
                    {
                        if (mouseKeyCodeAssigned == (int)MouseMessages.WM_LBUTTONUP && mousePttActivated && !activated)
                        {
                            OnMouseReleasedEvent();
                            pressedBoolean = false;
                        }
                        else if (activated)
                        {
                            OnMouseKeyAssignedChangedEvent("Left click");
                            mouseKeyCodeAssigned = (int)MouseMessages.WM_LBUTTONUP;
                            Properties.Settings.Default.MouseKeyCodeSaved = (int)MouseMessages.WM_LBUTTONUP;
                        }
                    }
                    break;

                case MouseMessages.WM_LBUTTONDOWN: // When the key has been pressed
                    {

                        if (mouseKeyCodeAssigned == (int)MouseMessages.WM_LBUTTONUP && mousePttActivated && !pressedBoolean)
                        {
                            OnMousePressedEvent();
                            pressedBoolean = true;
                        }

                    }
                    break;
                case MouseMessages.WM_MBUTTONUP: // When the key has been released
                    {
                        if (mouseKeyCodeAssigned == (int)MouseMessages.WM_MBUTTONUP && mousePttActivated && !activated)
                        {
                            OnMouseReleasedEvent();
                            pressedBoolean = false;
                        }
                        else if (activated)
                        {
                            OnMouseKeyAssignedChangedEvent("Middle click");
                            mouseKeyCodeAssigned = (int)MouseMessages.WM_MBUTTONUP;
                            Properties.Settings.Default.MouseKeyCodeSaved = (int)MouseMessages.WM_MBUTTONUP;
                            
                        }
                    }
                    break;

                case MouseMessages.WM_MBUTTONDOWN: // When the key has been pressed
                    {

                        if (mouseKeyCodeAssigned == (int)MouseMessages.WM_LBUTTONUP && mousePttActivated && !pressedBoolean)
                        {
                            OnMousePressedEvent();
                            pressedBoolean = true;
                        }

                    }
                    break;
                case MouseMessages.WM_XBUTTONUP: // When the key has been released
                    { //This is X button, we have to know what kind of X button we're talking about
                        MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));

                        if (mouseKeyCodeAssigned == (int)hookStruct.mouseData && mousePttActivated && !activated)
                        {
                            OnMouseReleasedEvent();
                            pressedBoolean = false;
                        }
                        else if (activated)
                        {
                            OnMouseKeyAssignedChangedEvent("Mouse " + (int)hookStruct.mouseData/65536);
                            mouseKeyCodeAssigned = (int)hookStruct.mouseData;
                            Properties.Settings.Default.MouseKeyCodeSaved = (int)hookStruct.mouseData;

                        }
                       
                    
                    }
                    break;

                case MouseMessages.WM_XBUTTONDOWN: // When the key has been pressed
                    {
                        MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                        if (mouseKeyCodeAssigned == (int)hookStruct.mouseData && mousePttActivated && !pressedBoolean)
                        {
                            OnMousePressedEvent();
                            pressedBoolean = true;
                        }

                   
                    }
                    break;
                default:
                    break;
                    
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);

        }

        private const int WH_MOUSE_LL = 14;

        private enum MouseMessages
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_MOUSEWHEEL = 0x020A,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205,
            WM_XBUTTONDOWN = 0x020B,
            WM_XBUTTONUP = 0x020C,
            WM_MBUTTONDOWN = 0x0207,
            WM_MBUTTONUP = 0x0208

        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
