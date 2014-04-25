using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Alchemy;

// Largely inspired from http://blogs.msdn.com/b/toub/archive/2006/05/03/589423.aspx

namespace vocaNODPushToTalkCs
{
    public delegate void onKeyPressed();
    public delegate void onKeyReleased();
    public delegate void onKeyAssignedChanged(int key);
    class InterceptKeys
    {
        //Some const needed
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        // The events we'll need to fire
        public static event onKeyPressed OnKeyPressedEvent;
        public static event onKeyReleased OnKeyReleasedEvent;
        public static event onKeyAssignedChanged OnKeyAssignedChangedEvent;
        
        // This boolean is to know if we're listening the keyboard for assigning a key
        private  static bool activated;
        //This is to avoid the "pressed" event to be fired every time the hook catches an input
        private static bool pressedBoolean;
        private static int keyCodeAssigned;

        //Boolean to know if you assigned a keyboard key or a mouse button
        private static bool keyboardPttActivated;

        public static void RunKeyHook()
        {
            pressedBoolean = false;
            activated = false;
            Form1.Activation += new onActive(listenActivation);
            Form1.Deactivation += new onDeactive(listenDeactivation);
            MouseHook.OnMouseKeyAssignedChangedEvent += new onMouseKeyAssignedChanged(listenMouseKeyAssignedChanged);
            InterceptKeys.OnKeyAssignedChangedEvent += new onKeyAssignedChanged(listenOnKeyAssignedChanged);
            _hookID = SetHook(_proc);
        }

        public static void StopKeyHook() {

            UnhookWindowsHookEx(_hookID);
        
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(
            int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            int vkCode = Marshal.ReadInt32(lParam);
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                //Here we check if the input caught is the one we assigned and if we actually have a keyboard input
                // assigned in the current session of ptt
                // (it just means that if you assigned a keyboard key, then a mouse key, the keyboard key
                // will still be in memory in this class, so we need the boolean keyboardPttActivated
                // to know if we actually want the event to be fired)

                if (vkCode == keyCodeAssigned && !pressedBoolean && keyboardPttActivated && !activated)
                {
                    OnKeyPressedEvent();
                    pressedBoolean = true;
                } //Here if we're assigning the key, that's waht happens
                else if (activated)
                {
                    keyCodeAssigned = vkCode;
                    OnKeyAssignedChangedEvent(keyCodeAssigned);
                }
            }

            if (nCode >= 0 && wParam == (IntPtr)WM_KEYUP && keyboardPttActivated)
            {
                
                if (vkCode == keyCodeAssigned)
                {
                    OnKeyReleasedEvent();
                    pressedBoolean = false;
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private static void listenActivation() {
            activated = true;
        }

        private static void listenDeactivation()
        {
            activated = false;
        }

        private static void listenMouseKeyAssignedChanged(String mouseKey) 
        {
            keyboardPttActivated = false;
        }

        private static void listenOnKeyAssignedChanged(int key)
        {

            keyboardPttActivated = true;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

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
