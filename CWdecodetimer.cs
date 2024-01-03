using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;

namespace CWExpert
{
    public class CWDecode
    {
        #region variable

        delegate void CrossThreadCallback(string command, string data);

        public bool eot = false;
        public bool so2r = false;
        public bool transmit = false;
        public bool right_tx = false;
        public bool left_tx = false;
        public int once = 0;
        public int rtc = 0;
        public int rcvd = 0;
        public int moni = 12;
        public int repeat = 0;
        public const int rate = 8000;
        public const int FFTlen = 64;
        public const int F2L = 128;
        public const int noise = 128;
        public const int aver = 10;
        public const int bwl = 4;
        public const int bwh = 20;
        public bool run_thread = false;
        public Thread CWThread;
        public AutoResetEvent AudioEvent;
        public short[] read_buffer;
        public short right, left = 0;
        public int b = 0;
        public int grab = 0;
        public char ch = (char)0;
        public bool key = false;
        public bool nragn = false;
        public bool call_found = false;
        public bool rprt_found = false;
        public bool nrst_found = false;
        public string mystr = new string(' ', 64);
        public string mycall = new string(' ', 14);
        public string rst = new string(' ', 3);
        public string report = new string(' ', 4);
        public string call_sent = new string(' ', 14);
        public string new_call = new string(' ', 14);
        public string old_call = new string(' ', 14);
        public string nmbr = new string(' ', 7);
        public string morse = new string(' ', 64);
        public string[] output = new string[F2L];
        public string[] grabs = new string[F2L];
        public int[] sum = new int[F2L];
        public int[] ave = new int[F2L];
        public double[] RealF = new double[F2L];
        public double[] ImagF = new double[F2L];
        public double[] Mag = new double[F2L];
        public double[,] Num = new double[F2L, 4];
        public double[] Maxi = new double[F2L];
        public double[] prag = new double[F2L];
        public int[] kanal = new int[F2L];
        public int active = 0;
        public int lastch = 0;
        public int buflen = 0;
        public int[] broj = new int[F2L];
        public double[] si = new double[F2L];
        public double[] co = new double[F2L];
        public double[] wd = new double[F2L];
        public int[] tim = new int[F2L];
        public double v = 0;
        public double thd = 0;
        private double Period = 0.0f;
        public int t = 0;
        public int tx_timer = 0;
        public int rx_timer = 0;
        public int kuzza = 16;
        public int ctr = 0;
        public int j = 0;
        public int n = 0;
        public int n1 = 0;
        public int w = 0;
        public int x = 0;
        public int y = 0;
        public int z = 0;
        public int l = 0;
        public int indeks = 0;
        public int loopend = 0;
        public int totalsamples = 0;
        public int dotmin = 0;
        public int number = 0;
        public byte[] bitrev = new byte[F2L];
        public short[] old1 = new short[F2L];
        public short[] old2 = new short[F2L];
        public bool[] keyes = new bool[F2L];
        public int[] peaks = new int[F2L];

        private CWExpert MainForm;

        #endregion

        #region constructor and destructor

        public CWDecode(CWExpert mainForm)
        {
            try
            {
                MainForm = mainForm;
                read_buffer = new short[2048];
                AudioEvent = new AutoResetEvent(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        ~CWDecode()
        {
        }

        #endregion


        #region crossthread

        private void CrossThreadCommand(string action, string data)
        {
            try
            {
                //  Select an action to perform on the form:

                switch (action)
                {
                    case "Send CALL":
                        {
                            MainForm.txtCALL = data;
                            MainForm.btnSendCall_Click(null, null);
                        }
                        break;

                    case "Send RST":
                        {
                            MainForm.txtRst = data;
                            MainForm.btnSendRST_Click(null, null);
                        }
                        break;

                    case "Send NR":
                        {
                            MainForm.txtNR = data;
                            MainForm.btnSendNr_Click(null, null);
                        }
                        break;

                    case "Send F1":
                        {
                            MainForm.btnF1_Click(null, null);
                        }
                        break;

                    case "Send F2":
                        {
                            MainForm.btnF2_Click(null, null);
                        }
                        break;

                    case "Send F3":
                        {
                            MainForm.btnF3_Click(null, null);
                        }
                        break;

                    case "Send F4":
                        {
                            MainForm.btnF4_Click(null, null);
                        }
                        break;

                    case "Send F5":
                        {
                            MainForm.btnF5_Click(null, null);
                        }
                        break;
                    case "Send F7":
                        {
                            MainForm.btnF7_Click(null, null);
                        }
                        break;

                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex + "\n\n" + ex.StackTrace.ToString());
            }
        }

        #endregion

        public void CW_Thread()
        {
            try
            {
                ctr = 0;
                while (run_thread)
                {
                    AudioEvent.WaitOne();
                    Audio2ASCII();
                    Analyse();
                    TRtiming();
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                run_thread = false;
            }
        }

        public bool CWdecodeStart()
        {
            bool result = false;

            if (Init())
            {
                run_thread = true;
                CWThread = new Thread(new ThreadStart(CW_Thread));
                CWThread.Name = "CW Thread";
                CWThread.Priority = ThreadPriority.Normal;
                CWThread.IsBackground = true;
                CWThread.Start();
            }

            return result;
        }

        public void CWdecodeStop()
        {
            run_thread = false;
            AudioEvent.Set();
        }

        public bool Init()
        {
            try
            {
                totalsamples = F2L;
                l = (int)(Math.Round(Math.Log(totalsamples) / Math.Log(2.0)));
                Period = (double)totalsamples / (double)rate;
                for (n = 0; n < totalsamples; n++)
                {
                    x = n;
                    y = 0;
                    n1 = totalsamples;
                    for (w = 1; w <= l; w++)
                    {
                        n1 = n1 >> 1;
                        if (x >= n1)
                        {
                            if (w == 1)
                                y++;
                            else
                                y = y + (2 << (w - 2));
                            x = x - n1;
                        }
                    }
                    bitrev[n] = (byte)y;
                }
                dotmin = (int)Math.Round(0.03 / Period);  // 40 wpm, 30 msec dot
                morse = "ETIANMSURWDKGOHVF*L*PJBXCYZQ**54*3***2 ******16*/*****7***8*90**";
                for (n = 0; n < F2L; n++)
                {
                    peaks[n] = 0;
                    Maxi[n] = noise;
                    prag[n] = noise;
                    ave[n] = aver;
                    sum[n] = 0;
                    tim[n] = 1;
                    old1[n] = 0;
                    old2[n] = 0;
                    keyes[n] = false;
                    output[n] = "";
                    broj[n] = 0;
                    for (z = 0; z < 4; z++)
                        Num[n, z] = 0;
                }
                v = 2 * Math.PI / totalsamples;
                for (n = 0; n < totalsamples; n++)
                {
                    RealF[n] = 0;
                    ImagF[n] = 0;
                    si[n] = -Math.Sin(n * v);
                    co[n] = Math.Cos(n * v);
                    wd[n] = (1 + co[n]) / F2L;
                }
                grab = 0;
                buflen = 0;
                mycall = "N1YU";
                old_call = mycall;
                call_sent = mycall;
                call_found = false;
                rprt_found = false;
                nragn = false;
                rx_timer = kuzza;
                tx_timer = 0;
                transmit = false;
                active = moni;
                lastch = moni;
                once = 0;

                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        private void cw2asc()
        {
            if (sum[z] < 63)
                ch = morse[sum[z] - 1];
            else if (sum[z] == 75)
                ch = '?';
            else
                ch = '*';
            output[z] += ch;
            sum[z] = 0;
            rx_timer = kuzza;
        }

        private void CWdecode()
        {
            try
            {
                keyes[z] = key;
                t = ctr - tim[z];
                tim[z] = ctr;
                if (key)
                {
                    if (t > ave[z])
                    {
                        if (sum[z] > 0)
                            cw2asc();
                    }
                    else if (t > dotmin)
                    {
                        ave[z] = t + (ave[z] / 2);
                        sum[z] = 2 * sum[z];
                        if (sum[z] > 255)
                            sum[z] = 0;
                    }
                }
                else
                {
                    if (t > ave[z])
                        sum[z] += 2;
                    else if (t > dotmin)
                    {
                        ave[z] = t + (ave[z] / 2);
                        sum[z]++;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void Sig2Sym()
        {
            for (z = bwl; z <= bwh; z++)
            {
                v = Mag[z];
                //               v = (v + Mag[z - 1] + Mag[z + 1]) / 3;
                if (v > Maxi[z])
                    Maxi[z] = v;
                if ((Mag[z] > Mag[z - 1]) && (Mag[z] > Mag[z + 1]))
                    peaks[z]++;
                thd = (prag[z] + noise) / 2;
                if (thd < noise)
                    thd = noise;
                b = broj[z];
                if (v > thd)
                {
                    if (b < 3)
                        b++;
                }
                else if (b > 0)
                    b--;
                broj[z] = b;
                key = b > 1;
                if (key != keyes[z])
                    CWdecode();
                else if (!key)
                {
                    if ((ctr - tim[z]) == (2 * ave[z]))
                    {
                        if (sum[z] > 0)
                            cw2asc();
                        if (output[z].Length >= 2)
                            output[z] += " -";
                    }
                    /*
                else if ((ctr - tim[z]) == (5 * ave[z]))
                {
                    if (sum[z] > 0)
                        cw2asc();
                    if (output[z].Length >= 3)
                        output[z] += "-";
                }
                     */
                }
            }
        }

        private void CallFFT()
        {
            int i, k, m, mx, I1, I2, I3, I4, I5;
            double A1, A2, B1, B2, Z1, Z2;
            I1 = totalsamples / 2;
            I2 = 1;
            for (i = 1; i <= l; i++)
            {
                I3 = 0;
                I4 = I1;
                for (k = 1; k <= I2; k++)
                {
                    x = I3 / I1;
                    I5 = bitrev[x];
                    Z1 = co[I5];
                    Z2 = si[I5];
                    loopend = I4 - 1;
                    for (m = I3; m <= loopend; m++)
                    {
                        A1 = RealF[m];
                        A2 = ImagF[m];
                        mx = m + I1;
                        B1 = Z1 * RealF[mx] - Z2 * ImagF[mx];
                        B2 = Z2 * RealF[mx] + Z1 * ImagF[mx];
                        RealF[m] = (A1 + B1);
                        ImagF[m] = (A2 + B2);
                        RealF[mx] = (A1 - B1);
                        ImagF[mx] = (A2 - B2);
                    }
                    I3 = I3 + (I1 << 1);
                    I4 = I4 + (I1 << 1);
                }
                I1 = I1 >> 1;
                I2 = I2 << 1;
            }
        }

        private void Median(double mag)
        {
            Num[z, 1] = Num[z, 2]; Num[z, 2] = Num[z, 3]; Num[z, 3] = mag;

            if ((Num[z, 1] >= Num[z, 2]) && (Num[z, 2] >= Num[z, 3]))
                Num[z, 0] = Num[z, 2];
            else if ((Num[z, 1] <= Num[z, 2]) && (Num[z, 2] <= Num[z, 3]))
                Num[z, 0] = Num[z, 2];
            else if ((Num[z, 1] <= Num[z, 2]) && (Num[z, 2] >= Num[z, 3]) && (Num[z, 1] <= Num[z, 3]))
                Num[z, 0] = Num[z, 3];
            else if ((Num[z, 1] <= Num[z, 2]) && (Num[z, 2] >= Num[z, 3]) && (Num[z, 1] >= Num[z, 3]))
                Num[z, 0] = Num[z, 1];
            else if ((Num[z, 1] >= Num[z, 2]) && (Num[z, 2] <= Num[z, 3]) && (Num[z, 1] >= Num[z, 3]))
                Num[z, 0] = Num[z, 3];
            else
                Num[z, 0] = Num[z, 1];
        }

        private void Spectra()
        {
            for (z = bwl - 1; z <= bwh + 1; z++)
            {
                y = bitrev[z];
                Mag[z] = Math.Sqrt((RealF[y] * RealF[y]) + (ImagF[y] * ImagF[y]));
                Median(Mag[z]);
                Mag[z] = Num[z, 0];
                prag[z] = (31 * prag[z] + Mag[z]) / 32;
            }
        }

        private void Audio2ASCII()
        {
            try
            {
                indeks = 0;
                while (indeks < read_buffer.Length)
                {
                    for (n = 0; n < FFTlen; n++)
                    {
                        left = read_buffer[indeks + n];
                        ImagF[n] = 0;
                        ImagF[n + FFTlen] = 0;
                        RealF[n + FFTlen] = wd[n + FFTlen] * old1[n];
                        old1[n] = left;
                        RealF[n] = wd[n] * old1[n];
                    }
                    indeks += FFTlen;
                    ctr++;  // 8 msec counter for dot and dash timing
                    CallFFT();
                    Spectra();
                    Sig2Sym();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /*
                public bool IsCutNumber(string str)
                {
                    if (str.StartsWith("R5"))
                        str.Remove(0, 1);
                    else if (str.IndexOf("NN") != -1)
                    {
                        str.Substring(str.IndexOf("NN"), (str.Length - str.IndexOf("NN")));
                        str.Insert(0, "5");
                    }
                    nmbr = str.Replace("N", "9");
                    nmbr = nmbr.Replace("O", "0");
                    nmbr = nmbr.Replace("T", "0");
                    //            nmbr = nmbr.Replace("A", "1");
                    //            nmbr = nmbr.Replace("E", "5");
                    rprt_found = (Int32.TryParse(nmbr, out number));
                    return rprt_found;
                }
        */

        public void Report_Filter(string nmbr)
        {
            try
            {

                nmbr = nmbr.Replace("N", "9");
                nmbr = nmbr.Replace("O", "0");
                nmbr = nmbr.Replace("T", "0");
                //            nmbr = nmbr.Replace("A", "1");
                //            nmbr = nmbr.Replace("E", "5");
                rprt_found = (Int32.TryParse(nmbr, out number));
                rst = nmbr.Substring(0, 3);
                if (Int32.TryParse(rst, out number))
                {
                    if ((number == 559) || (number == 569) || (number == 579) || (number == 589) || (number == 599))
                    {
                        report = nmbr.Substring(3, nmbr.Length - 3);
                        //                       if (number != 599)
                        //                            MainForm.Invoke(new CrossThreadCallback(CrossThreadCommand), "Send RST", rst);
                    }
                }
                else
                {
                    report = nmbr;
                    rst = "599";
                }
                rprt_found = (Int32.TryParse(report, out number) && (number < 999) && (number > 0));
                if (rprt_found)
                { rcvd = number; }
                //               else { rcvd = 0; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void Call_Filter(string znak)
        {
            if ((!znak.Contains(mycall)) && (!znak.Contains(call_sent)) && (!znak.Contains(old_call)))
                call_found = ((znak.IndexOf("/") > 1) || (((char.IsDigit(znak, 1)) || (char.IsDigit(znak, 2)) && (Char.IsLetter(znak, znak.Length - 1)))));
            else
                call_found = false;
        }


        private void Analyse()
        {
            try
            {
                char[] delimiterChars = { ' ', '-' };

                z = moni;
                do
                {
                    string text = output[z];
                    if (text.Contains("-"))
                    {
                        string[] words = text.Split(delimiterChars);
                        output[z] = " ";
                        foreach (string mystr1 in words)
                        {
                            mystr = mystr1;
                            if ((mystr.IndexOf("R5") == 0) && (mystr.IndexOf("N") > 1) && (mystr.IndexOf("N") < 4))
                                mystr = mystr.Remove(0, 1);
                            else if (mystr.IndexOf("DE") == 0)
                                mystr = mystr.Remove(0, 2);


                            if (mystr.Length > 6)
                            {
                                int ind1 = mystr.IndexOf("5NN");
                                if (ind1 > 3)
                                {
                                    string sn = mystr.Substring(ind1, mystr.Length - ind1);
                                    mystr = mystr.Substring(0, ind1);
                                    if (!transmit)
                                        Report_Filter(sn);
                                }
                            }
                            if ((((mystr.Length) % 2) == 0) && (mystr.Length > 6))
                            {
                                string s1 = mystr.Substring(0, mystr.Length / 2);
                                string s2 = mystr.Substring(mystr.Length / 2, mystr.Length / 2);
                                if (s1.Equals(s2))
                                    mystr = s1;
                            }

//                            if ((mystr.Length >= 2) && (z == moni))
//                                Debug.Write(z.ToString() + "-" + mystr + "-" + grab.ToString() + "  ");

                            if (mystr.Equals("PARIS")) /* || ((mystr.Length >= 2) && (z == moni) && ((char.IsDigit(mystr, mystr.Length - 1)) || (mystr.Contains("TU")) || (mystr.Contains(mycall))))) */
                            {
                                eot = true;
                                Debug.WriteLine(mystr + " dot =  " + (((ctr - rtc-6)/50).ToString()) + " ");
  //                              if (once < 13)
 //                                once++;
   //                             else
//                                {
                                    once = 0;
//                                    Debug.WriteLine(ave[moni].ToString());
//                                }
                            }

                            if ((z == active) && ((mystr.Contains("NR?")) || (mystr.Contains("AGN"))))
                                rcvd = 9999;

                            if ((mystr.Length > 3) && (mystr.Length < 15) && (!mystr.Contains("*")) && (!mystr.Contains("?"))
                            && (peaks[z] > peaks[z - 1]) && (peaks[z] > peaks[z + 1]))
                            {
                                Call_Filter(mystr);
                                if (call_found)
                                {
                                    old_call = mystr;
                                    grabs[grab] = mystr;
                                    kanal[grab] = z;
                                    if (grab < F2L)
                                        grab++;
                                }
                                if ((!transmit) && (((grab > 0) && Math.Abs(z - active) < 2) || ((grab == 0) && Math.Abs(z - lastch) < 2)))
                                {
                                    Report_Filter(mystr);
                                    //                                    Debug.WriteLine("nr " + rcvd.ToString());
                                }

                            }
                        }
                    }
                    z++;
                }
                while (z < moni);
                for (j = bwl; j < bwh; j++)
                    peaks[j] = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /* 
               Transmmition response in pile-up mode to grab & rprt

               1. RX timeout (if no Morse chars coming in CW2ASC) and empty buffer - call CQ 
               1a.   Send F1, clear buffer, call_sent set to mycall
                * 
               2. Callsign found as buffer index grab>0 and call sent was mycall 
               2a.   ESM send grab[0] and report, set active channel, buflen = grab at exit;
                * 
               3. Report received and no buffer change
               3a. Send Number, grab-1 
                   if grab>0 {shift buffer, go to 2a}
                * 
               4. RX timeout and no buffer change (grab=buflen)
               4a.repeat callsign and report 2 more times - Send F5 + F2 
                  if repeat = 3 then Go to 1
                * 
               5. Buffer change with corrected callsign 
                  Send Call
                  if report_found then F3 else F2
                * 
       */

        private void Enter_Sends_Message()
        {
            call_sent = grabs[0];
            active = kanal[0];
            MainForm.Invoke(new CrossThreadCallback(CrossThreadCommand), "Send CALL", call_sent);
            tx_timer = 20;
            rx_timer = kuzza;
            rcvd = 0;
            buflen = grab;
        }

        private void TRtiming()
        {

            try
            {
                if (once == 0)
/*
                {
                    call_sent = "PARIS";
                    output[moni] = " ";
                    rtc = ctr;
                    Debug.Write(" ESM ");
                    MainForm.Invoke(new CrossThreadCallback(CrossThreadCommand), "Send CALL", call_sent);
                    once++;
                }
                if (once == 2)
                {
                    report = "273";
                    output[moni] = " ";
                    rtc = ctr;
                    Debug.Write(" NR  ");
                    MainForm.Invoke(new CrossThreadCallback(CrossThreadCommand), "Send NR", report);
                    once++;
                }
                if (once == 4)
                {
                    output[moni] = " ";
                    rtc = ctr;
                    Debug.Write(" F1  ");
                    MainForm.Invoke(new CrossThreadCallback(CrossThreadCommand), "Send F1", "");
                    once++;
                }
                if (once == 6)
                {
                    output[moni] = " ";
                    rtc = ctr;
                    Debug.Write(" F2  ");
                    MainForm.Invoke(new CrossThreadCallback(CrossThreadCommand), "Send F2", "");
                    once++;
                }
                if (once == 8)
                {
                    output[moni] = " ";
                    rtc = ctr;
                    Debug.Write(" F3  ");
                    MainForm.Invoke(new CrossThreadCallback(CrossThreadCommand), "Send F3", "");
                    once++;
                }
                if (once == 10)
                {
                    output[moni] = " ";
                    rtc = ctr;
                    Debug.Write(" F4  ");
                    MainForm.Invoke(new CrossThreadCallback(CrossThreadCommand), "Send F4", "");
                    once++;
                }
                if (once == 12)
 */
                {
                    output[moni] = " ";
                    rtc = ctr;
                    Debug.Write(" F5  ");
                    MainForm.Invoke(new CrossThreadCallback(CrossThreadCommand), "Send F5", "");
                    once++;
                }

                /*
                               do
                                   n = j;
                               while ((!output[moni].EndsWith("-")) && ((rtc - n) < 20));

                               Debug.WriteLine("ESM " + (rtc - n).ToString());
                               t0 = rtc;
                               MainForm.Invoke(new CrossThreadCallback(CrossThreadCommand), "Send NR", call_sent);
                               Debug.WriteLine("NR  " + (rtc - t0).ToString());
                               t0 = rtc;
                               MainForm.Invoke(new CrossThreadCallback(CrossThreadCommand), "Send F1","");
                               Debug.WriteLine("F1  " + (rtc - t0).ToString());
                               t0 = rtc;
                               MainForm.Invoke(new CrossThreadCallback(CrossThreadCommand), "Send F2","");
                               Debug.WriteLine("F2  " + (rtc - t0).ToString());
                               t0 = rtc;
                               MainForm.Invoke(new CrossThreadCallback(CrossThreadCommand), "Send F3","");
                               Debug.WriteLine("F3  " + (rtc - t0).ToString());
                               t0 = rtc;
                               MainForm.Invoke(new CrossThreadCallback(CrossThreadCommand), "Send F4","");
                               Debug.WriteLine("F4  " + (rtc - t0).ToString());
                               t0 = rtc;
                               MainForm.Invoke(new CrossThreadCallback(CrossThreadCommand), "Send F5","");
                               Debug.WriteLine("F5  " + (rtc - t0).ToString());
                 */

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}

/*
                
                if (tx_timer == 0)
                {
                    if (rx_timer > 0)
                        rx_timer--;

                    if (grab == 0)
                    {
                        if (rcvd > 0)
                        {
                            // confirm double report
                            MainForm.Invoke(new CrossThreadCallback(CrossThreadCommand), "Send F3", "");
                            tx_timer = 5;
                            rcvd = 0;
                        }
                        else if (rx_timer == 0)
                        {
                            // calling CQ timed loop
                            MainForm.Invoke(new CrossThreadCallback(CrossThreadCommand), "Send F1", "");
                            tx_timer = 27;
                            rx_timer = kuzza;
                            rcvd = 0;
                            grab = 0;
                            buflen = 0;
                            repeat = 0;
                            call_sent = mycall;
                            old_call = mycall;
                        }
                    }
                    else
                    {
                        if (rcvd == 9999)
                        {
                            // repeat report if nr? or agn detected
                            MainForm.Invoke(new CrossThreadCallback(CrossThreadCommand), "Send F2", "");
                            tx_timer = 8;
                            rcvd = 0;
                        }
                        if ((rx_timer == 0) && (rcvd == 0))
                        {
                            MainForm.Invoke(new CrossThreadCallback(CrossThreadCommand), "Send F5", "");
                            MainForm.Invoke(new CrossThreadCallback(CrossThreadCommand), "Send F2", "");
                            tx_timer = 17;
                            buflen = grab;
                            repeat++;
                            if (repeat == 1)
                            {
                                repeat = 0;
                                grab--;
                                if (grab > 0)
                                {
                                    for (n = 0; n < grab; n++)
                                    {
                                        grabs[n] = grabs[n + 1];
                                        kanal[n] = kanal[n + 1];
                                    }
                                    Enter_Sends_Message();
                                }
                            }
                        }
                        if ((rcvd == 0) && call_sent.Contains(mycall))
                        {
                            // first ESM call && report sent FIFO
                            Enter_Sends_Message();
                        }
                        if ((rcvd > 0) && call_sent.Contains(grabs[0]) && (grab == buflen))
                        {
                            // end of regular QSO 
                            MainForm.Invoke(new CrossThreadCallback(CrossThreadCommand), "Send NR", report);
                            tx_timer = 4;
                            lastch = kanal[0];
                            rcvd = 0;
                            grab--;
                            if (grab > 0)
                            {
                                for (n = 0; n < grab; n++)
                                {
                                    grabs[n] = grabs[n + 1];
                                    kanal[n] = kanal[n + 1];
                                }
                                Enter_Sends_Message();
                            }
                        }
                        if ((rcvd > 0) && (grab > buflen))
                        {
                            // End of QSO with new corrected callsign LIFO
                            grab--;
                            call_sent = grabs[grab];
                            grabs[0] = call_sent;
                            MainForm.Invoke(new CrossThreadCallback(CrossThreadCommand), "Send CALL", call_sent);
                            MainForm.Invoke(new CrossThreadCallback(CrossThreadCommand), "Send F3", "");
                            tx_timer = 15;
                            rcvd = 0;
                            if (grab > 0)
                            {
                                for (n = 0; n < grab; n++)
                                {
                                    grabs[n] = grabs[n + 1];
                                    kanal[n] = kanal[n + 1];
                                }
                                Enter_Sends_Message();
                            }
                        }
                        if ((grab > buflen) && (rcvd == 0))
                        {

                            // Possible callsign correction or lid or new caller LIFO
                            grab--;
                            call_sent = grabs[grab];
                            active = kanal[grab];
                            grabs[0] = call_sent;
                            buflen = grab;
                            MainForm.Invoke(new CrossThreadCallback(CrossThreadCommand), "Send CALL", call_sent);
                            MainForm.Invoke(new CrossThreadCallback(CrossThreadCommand), "Send F2", "");
                            tx_timer = 17;
                        }

                    }
                }

                if (tx_timer > 0)
                {
                    tx_timer--;
                    transmit = true;
                    if (eot || (tx_timer == 0))
                    {
                        Debug.WriteLine("RX " + tx_timer.ToString());
                        eot = false;
                        transmit = false;
                        rx_timer = kuzza;
                        for (z = moni - 1; z < moni + 1; z++)
                        {
                            output[z] = " ";
                            peaks[z] = 0;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

    }
}

*/
