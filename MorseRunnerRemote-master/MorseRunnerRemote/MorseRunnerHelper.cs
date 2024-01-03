using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MorseRunnerRemote
{
    class MorseRunnerHelper
    {
        #region Win32
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32")]
        private static extern bool SetForegroundWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern Int32 SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(int hWnd, int Msg, int wParam, ref COPYDATASTRUCT lParam);

        [DllImport("user32.Dll", EntryPoint = "PostMessageA")]
        static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }

        private uint WM_CQWW = 32768 + 3;

        const uint WM_KEYDOWN = 0x100;
        public const int WM_COPYDATA = 0x4A;

        private uint WM_GETTXSTATUS = 32768 + 12;
        private uint WM_SETCALL = 32768 + 7;
        private uint WM_SETRST = 32768 + 8;
        private uint WM_SETNR = 32768 + 9;
        private uint WM_STOP = 32768 + 2;

        private uint WM_SETMYCALL = 32768 + 5;
        private uint WM_SETMYZONE = 32768 + 6;
        private uint WM_SETCWSPEED = 32768 + 10;

        private uint WM_SETACTIVITY = 32768 + 16;
        private uint WM_SETQRN = 32768 + 17;
        private uint WM_SETQRM = 32768 + 18;
        private uint WM_SETQSB = 32768 + 19;
        private uint WM_SETFLUTTER = 32768 + 20;
        private uint WM_SETLIDS = 32768 + 21;
        private uint WM_SETBC = 32768 + 22;
        private uint WM_SETDURATION = 32768 + 30;
        public const int WM_SETFREQUENCY = 32768 + 31;

        public void SendStringData(IntPtr hWnd, uint wParam, string message)
        {
            if (hWnd == IntPtr.Zero)
                return;

            byte[] bytes = Encoding.Default.GetBytes(message);
            int length = bytes.Length;
            COPYDATASTRUCT cds;
            cds.dwData = (IntPtr)wParam;
            cds.lpData = message;
            cds.cbData = length + 1;
            SendMessage((int)hWnd, WM_COPYDATA, 0, ref cds);
        }

        #endregion Win32

        private Process mrProcess;
        private IntPtr mrWindow = IntPtr.Zero;

        private int radioNumber =-1;
        private string callsign;
        private int cwSpeed = 30;
        private int contestActivity = 9;
        private int duration = 10;
        private int cqZone = 14;

        public MorseRunnerHelper(string mrPath, string callsign, int radioNumber)
        {
            this.radioNumber = radioNumber;
            this.callsign = callsign;

            if (!FindMorseRunnerWindow(radioNumber))
            {
                if (File.Exists(mrPath))
                {
                    ProcessStartInfo stinfo = new ProcessStartInfo(mrPath, String.Format("-r{0}", radioNumber));
                    mrProcess = Process.Start(stinfo);
                }
                InitializeDefaults();
            }
        }

        public MorseRunnerHelper(int radioNumber)
        {
            FindMorseRunnerWindow(radioNumber);
        }

        public bool FindMorseRunnerWindow(int radioNumber)
        {
            this.radioNumber = radioNumber;
            int retryCount = 20;
            if (mrWindow == IntPtr.Zero)
            {
                while (mrWindow == IntPtr.Zero && retryCount > 0)
                {
                    if (mrWindow == IntPtr.Zero)
                        System.Threading.Thread.Sleep(100);

                    mrWindow = FindWindowByCaption(IntPtr.Zero, String.Format("Morse Runner:  R{0}", radioNumber));
                    retryCount--;
                }

                if (retryCount > 0)
                    return true;
            }
            return false;
        }

        public bool IsReady()
        {
            if (mrProcess == null)
                return false;

            if (mrWindow.Equals(IntPtr.Zero))
                return false;

            return true;
        }

        public void Start()
        {
            if (mrWindow == IntPtr.Zero)
                return;

            SendKey(Keys.F9);
        }

        public void StartSingleCalls()
        {
            if (mrWindow == IntPtr.Zero)
                return;

            SetForegroundWindow(mrWindow);

            // Special keys described in
            // https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.sendkeys?redirectedfrom=MSDN&view=netframework-4.8
            SendKeys.Send("%+R+S");
        }

        public void StartCQWW()
        {
            if (mrWindow == IntPtr.Zero)
                return;

            SendMessage(mrWindow, WM_CQWW, IntPtr.Zero, IntPtr.Zero);
            System.Threading.Thread.Sleep(300);
        }

        public void Stop()
        {
            if (mrWindow == IntPtr.Zero)
                return;

            SendMessage(mrWindow, WM_STOP, IntPtr.Zero, IntPtr.Zero);
        }

        public void FocusMorseRunnerWindow()
        {
            if (mrWindow == IntPtr.Zero)
                return;

            SetForegroundWindow(mrWindow);
        }

        public bool IsTransmitting()
        {
            if (mrWindow == IntPtr.Zero)
                return false;

            int result = SendMessage(mrWindow, WM_GETTXSTATUS, IntPtr.Zero, IntPtr.Zero);
            return (result == 1);
        }

        public void SetCallsign(string callsign)
        {
            SetTextBoxValues(WM_SETCALL, callsign);
        }

        public void SetReport(string rst)
        {
            SetTextBoxValues(WM_SETRST, rst);
        }

        public void SetSerial(string serial)
        {
            SetTextBoxValues(WM_SETNR, serial);
        }

        private void SetTextBoxValues(uint control, string text)
        {
            if (mrWindow == IntPtr.Zero)
                return;

            SendStringData(mrWindow, control, "");
            SendStringData(mrWindow, control, text);
        }

        public void SendKey(Keys key)
        {
            if (mrWindow == IntPtr.Zero)
                return;

            PostMessage(mrWindow, WM_KEYDOWN, (int)key, 0);
        }

        public void InitializeDefaults()
        {
            SetMyCallsign(callsign);
            SetCWSpeed(cwSpeed);
            SetContestActivity(contestActivity);
            SetDuration(duration);
            SetMyCQZone(cqZone);
            SetBandConditions("QRM", false);
            SetBandConditions("QRN", false);
            SetBandConditions("QSB", false);
            SetBandConditions("FLUTTER", false);
            SetBandConditions("LIDS", false);
        }

        public void SetMyCallsign(String callsign)
        {
            SendStringData(mrWindow, WM_SETMYCALL, callsign);
        }

        public void SetCWSpeed(int speed)
        {
            SendStringData(mrWindow, WM_SETCWSPEED, speed.ToString());
        }

        public void SetContestActivity(int activity)
        {
            SendStringData(mrWindow, WM_SETACTIVITY, activity.ToString());
        }

        public void SetDuration(int duration)
        {
            SendStringData(mrWindow, WM_SETDURATION, duration.ToString());
        }

        public void SetBandConditions(String bcType, Boolean bcValue)
        {
            if (mrWindow == IntPtr.Zero)
                return;

            int nVal = bcValue ? 1 : 0;
            switch (bcType)
            {
                case "QRM":
                    SendMessage(mrWindow, WM_SETBC, (IntPtr)WM_SETQRM, (IntPtr)nVal);
                    break;
                case "QRN":
                    SendMessage(mrWindow, WM_SETBC, (IntPtr)WM_SETQRN, (IntPtr)nVal);
                    break;
                case "QSB":
                    SendMessage(mrWindow, WM_SETBC, (IntPtr)WM_SETQSB, (IntPtr)nVal);
                    break;
                case "FLUTTER":
                    SendMessage(mrWindow, WM_SETBC, (IntPtr)WM_SETFLUTTER, (IntPtr)nVal);
                    break;
                case "LIDS":
                    SendMessage(mrWindow, WM_SETBC, (IntPtr)WM_SETLIDS, (IntPtr)nVal);
                    break;
            }
        }

        public void SetMyCQZone(int cqZone)
        {
            SendStringData(mrWindow, WM_SETMYZONE, cqZone.ToString());
        }
    }
}
