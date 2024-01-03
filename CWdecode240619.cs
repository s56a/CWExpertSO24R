//=================================================================
// CWDecoder.cs
//=================================================================
// Copyright (C) 2011 S56A YT7PWR
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
//=================================================================


using System;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

// include doubled callsign or report check, thanks E71dx de S56A!

namespace CWExpert
{
    public class CWDecode
    {
        #region variable

        delegate void CrossThreadCallback(string command, string data);

        public int radio = 0;
        public int rts1 = 0;  // ready2send enter=1, f2=2, f52=3
        public int rts2 = 0;
        // public int activech1 = 0;
        // public int activech2 = 0;
        public int rxt1 = 0;
        public int rxt2 = 0;
        public int freq1 = 0;
        public int freq2 = FFTlen;
        public bool qso1 = false;
        public bool qso2 = false;
        public bool swl = false;
        public const int moni0 = (500 * 256) / 8000;
        public const int moni1 = FFTlen + (900 * 256)/8000;
        // double maxL = 0;
        // double maxD = 0;
        public const int ponovi = 24;
        public bool repeat1 = false;
        public bool repeat2 = false;
        public int loopend = 0;
        public int logf2l = 0;
        //       public int agc = 32;
        public const int rate = 8000;
        public const int F2L = 256;
        public int totalsamples = F2L;
        public const int FFTlen = F2L / 2;
        public const int wndw = 64;
        public int ovrlp = F2L / wndw;
        public const int nofs = 32; // number of overlaped segments
        public double thld0 = 400.0;
        public double thld1 = 400.0;
        public int aver = 8;
        public int bwl0 = moni0 - 10;
        public int bwh0 = moni0 + 10;
        public int bwl1 = moni1 - 10;
        public int bwh1 = moni1 + 10;
        public bool run_thread = false;
        public Thread CWThread;
        public AutoResetEvent AudioEvent;
        public ushort[] read_buffer_l;
        public ushort[] read_buffer_r;
        public bool key = false;
        public bool nr_agn1 = false;
        public bool call_found1 = false;
        public bool rprt_found1 = false;
        public bool nr_agn2 = false;
        public bool call_found2 = false;
        public bool rprt_found2 = false;
        public bool rprt_error1 = false;
        public bool rprt_error2 = false;
        public string rprt = new string(' ', 4);
        public int serial = 1;
        public string mycall = new string(' ', 14);
        public string rst = new string(' ', 3);
        public string report = new string(' ', 4);
        //       public string call_sent = new string(' ', 14);
        public string call = new string(' ', 14);
        public string morse = new string(' ', 64);
        public string[] output = new string[F2L];
        public string[] scp = new string[37317];
        public string[] cona = new string[37317];
        public string[] callers = new string[256];
        public string[] reports = new string[256];
        public int[] sum = new int[F2L];
        public int[] ave = new int[F2L];
        public double[] maxi1 = new double[F2L];
        public double[] maxi2 = new double[F2L];
        public double[] noise = new double[F2L];
        public double[] RealF = new double[F2L];
        public double[] ImagF = new double[F2L];
        public double[,] Mag = new double[F2L, nofs];
        public double[,] Num = new double[F2L, 4];
        public double[] prag = new double[F2L];
        public double[] si = new double[F2L];
        public double[] co = new double[F2L];
        public double[] wd = new double[F2L];
        public double[] sigs = new double[F2L];
        public int[] tim = new int[F2L];
        private double Period = 0.0f;
        public int ctr = 0;
        public int rx_timer1 = ponovi;
        public int rx_timer2 = ponovi;
        public int dotmin = 2;
        public int[] bitrev = new int[F2L];
        public ushort[] old_l = new ushort[F2L];
        public ushort[] old_r = new ushort[F2L];
        public bool[] keyes = new bool[F2L];
        public bool[] valid = new bool[F2L];
        public bool[] enable = new bool[F2L];

        private CWExpert190519 MainForm;

        MorseRunnerHelper morseRunner1 = new MorseRunnerHelper(1);
        MorseRunnerHelper morseRunner2 = new MorseRunnerHelper(2);

        #endregion

        #region constructor and destructor

        public CWDecode(CWExpert190519 mainForm)
        {
            try
            {
                MainForm = mainForm;
                read_buffer_l = new ushort[2048];
                read_buffer_r = new ushort[2048];
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

        /*
                #region crossthread

                private void CrossThreadCommand(string action, string data)
                {
                    try
                    {
                        switch (action)
                        {

                            case "Send rCALL":
                                {
                                    MainForm.txtrCALL = data;
                                    MainForm.btncall_Click(null, null);
                                }
                                break;

                            case "Send rRST":
                                {
                                    MainForm.txtrRst = data;
                                    MainForm.btnrst_Click(null, null);
                                }
                                break;

                            case "Send rNR":
                                {
                                    MainForm.txtrNR = data;
                                    MainForm.btnnbr_Click(null, null);
                                }
                                break;

                            case "Escape":
                                {
                                    MainForm.butesc_Click(null, null);
                                }
                                break;

                            case "RXLights":
                                {
                                    MainForm.rx_lights();
                                }
                                break;

                            case "TXLights":
                                {
                                    MainForm.tx_lights();
                                }
                                break;

                            case "TXexch":
                                {
                                    MainForm.tx_change();
                                }
                                break;

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

                            case "Send F6":
                                {
                                    MainForm.btnF6_Click(null, null);
                                }
                                break;

                            case "Send F7":
                                {
                                    MainForm.btnF7_Click(null, null);
                                }
                                break;

                            case "Send F8":
                                {
                                    MainForm.btnF8_Click(null, null);
                                }
                                break;

                            case "Send F9":
                                {
                                    MainForm.btnF9_Click(null, null);
                                }
                                break;

                            case "Send F10":
                                {
                                    MainForm.btnF10_Click(null, null);
                                }
                                break;

                            case "Send F11":
                                {
                                    MainForm.btnF11_Click(null, null);
                                }
                                break;

                            case "Send F12":
                                {
                                    MainForm.btnF12_Click(null, null);
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
        */
        public void CW_Thread()
        {
            try
            {
                Channel();
                ctr = 0;
                while (run_thread)
                {
                    AudioEvent.WaitOne();
                    StereoProc();  // stereo power spectra, left low channels, right above fftlengh
                    if (!morseRunner1.IsTransmitting()) // && morseRunner1.IsReady())
                    {
                        AnalyseLevi();
                        RespondLevi();
                    }

                    if (!morseRunner2.IsTransmitting()) // && morseRunner2.IsReady())
                    {
                        AnalyseDesni();
                        RespondDesni();
                    }
                    ctr += nofs;

                    // while (morseRunner1.IsTransmitting() || morseRunner2.IsTransmitting()) ;

                    if (!qso1 && rts2 > 0) TX2();
                    else if (!qso2 && rts1 > 0) TX1();

                    if (radio == 1  && !morseRunner1.IsTransmitting()) { radio = 2; TX2(); }
                    else if (radio == 2 && !morseRunner2.IsTransmitting()) { radio = 1; TX1(); }
                }

                /*
                if (radio==1) 
                {
                    if (rts2 > 0) { radio = 2; TX2(); }
                    else if (rts1 > 0) { TX1(); }
                }

                else if (radio == 2)
                {
                    if (rts1 > 0) { radio = 1; TX1(); }
                    else if (rts2 > 0) { TX2(); }
                }
                */
            }

            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                run_thread = false;
            }
        }

        public void TX1()
        {
            if (rts1 == 1) { morseRunner1.SendKey(Keys.Enter); qso1 = true; }
            else if (rts1 == 6) { morseRunner1.SendKey(Keys.Enter); qso1 = false; }
            else if (rts1 == 4) { morseRunner1.SendKey(Keys.F1); qso1 = false; }
            else if (rts1 == 5) { morseRunner1.SendKey(Keys.F3); qso1 = false; }
            else if (rts1 == 3) { morseRunner1.SendKey(Keys.F5); qso1 = true; }
            if (rts1 == 2 || rts1 == 3) { morseRunner1.SendKey(Keys.F2); qso1 = true; }
            rts1 = 0;
            // Debug.Write(" Q1 " + qso1.ToString());
        }

        public void TX2()
        {
            if (rts2 == 1) { morseRunner2.SendKey(Keys.Enter); qso2 = true; }
            else if (rts2 == 6) { morseRunner2.SendKey(Keys.Enter); qso2 = false; }
            else if (rts2 == 4) { morseRunner2.SendKey(Keys.F1); qso2 = false; }
            else if (rts2 == 5) { morseRunner2.SendKey(Keys.F3); qso2 = false; }
            else if (rts2 == 3) { morseRunner2.SendKey(Keys.F5); qso2 = true; }
            if (rts2 == 2 || rts2 == 3) { morseRunner2.SendKey(Keys.F2); qso2 = true; }
            rts2 = 0;
            // Debug.Write(" Q2 " + qso2.ToString());
        }

        /*
                private void FFTproc(levi) // Short overlapping segments spectra
                {
                    try
                    {
                        int n = 0;
                        int z = 0;
                        int i = 0;

                        for (n = bwl1 - 1; n <= bwh1 + 1; n++)
                            signal[n] = 0.0;

                        double maxim = 0.0;


                        while (z < nofs)
                        {
                            if (z < ovrlp - 1)
                            {
                                int bp = wndw * (ovrlp - z - 1);
                                for (n = 0; n < F2L; n++)
                                {
                                    ImagF[n] = 0;
                                    if (n < bp)
                                        RealF[n] = wd[n] * (short)old1[n + (z * wndw)];
                                    else
                                        RealF[n] = wd[n] * (short)audio_buffer[n - bp];
                                }
                            }
                            else
                            {
                                for (n = 0; n < F2L; n++)
                                {
                                    ImagF[n] = 0;
                                    RealF[n] = wd[n] * (short)audio_buffer[i + n];
                                }
                                i += wndw;
                            }

                            MyFFT();

                            for (n = bwl - 1; n <= bwh + 1; n++)
                            {
                                int y = bitrev[n];
                                Mag[n, z] = Math.Sqrt(RealF[y] * RealF[y] + ImagF[y] * ImagF[y]);
                                if (medijan)
                                    Mag[n, z] = Median(Mag[n, z], n);
                                if (logmagn)
                                {
                                    if (Mag[n, z] > 0.001)
                                        Mag[n, z] = Math.Log10(Mag[n, z]);
                                }
                                signal[n] += Mag[n, z] / nofs;
                                if (signal[n] > maxi[n])
                                    maxi[n] = signal[n];
                                if (maxi[n] / Noise[n] > maxim) maxim = maxi[n] / Noise[n];
                            }
                            z++;
                        }

                        for (n = 0; n < (ovrlp - 1) * wndw; n++)  // Save last 3 x  64 samples
                            old1[n] = audio_buffer[i + n];


                        for (n = bwl; n <= bwh; n++)
                        {
                            prag[n] = (prag[n] + signal[n - 1] + signal[n] + signal[n + 1]) / 4;
                            if (prag[n] < Noise[n])
                                prag[n] = Noise[n];


                        }

                        // Debug.Write(Math.Round(maxim).ToString() + "  ");
                    }
                    */



        public void Channel()
        {
            try
            {
                int i, j;

                morseRunner1.Start();
                Thread.Sleep(100);

                morseRunner2.Start();
                Thread.Sleep(100);

                for (i = 0; i < F2L; i++) noise[i] = 0;

                for (i = 0; i < 10; i++)
                {
                    AudioEvent.WaitOne();
                    StereoProc();
                    if (i > 3 && i < 8)
                    {
                        for (j = bwl0 - 1; j <= bwh0 + 1; j++) { noise[j] += sigs[j]; prag[j] = noise[j]; }
                        for (j = bwl1 - 1; j <= bwh1 + 1; j++) { noise[j] += sigs[j]; prag[j] = noise[j]; }
                    }
                }

                Debug.WriteLine(moni0 + "|" + moni1 + "  " + Math.Round(noise[moni0]).ToString() + "|" + Math.Round(noise[moni1]).ToString());

                radio = 1;
                rts1 = 4;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CWdecodeStart()
        {
            bool result = false;

            if (Init())
            {
                run_thread = true;
                CWThread = new Thread(new ThreadStart(CW_Thread));
                //   CWThread.Name = "CW Thread";
                //   CWThread.Priority = ThreadPriority.Normal;
                //   CWThread.IsBackground = true;
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
                int n, z;
                logf2l = (int)(Math.Round(Math.Log(totalsamples) / Math.Log(2.0)));
                for (n = 0; n < totalsamples; n++)
                {
                    int x = n;
                    int y = 0;
                    int n1 = totalsamples;
                    for (int w = 1; w <= logf2l; w++)
                    {
                        n1 >>= 1;
                        if (x >= n1)
                        {
                            if (w == 1)
                                y++;
                            else
                                y += (2 << (w - 2));
                            x -= n1;
                        }
                    }
                    bitrev[n] = (byte) y;
                }

                Period = (double)totalsamples / (double)rate;
                dotmin = (int)Math.Truncate(1.2 / (40 * Period));  // 40 wpm, 30 msec dot
                aver = (int)Math.Round(1.2 * 2 * rate / (30 * wndw));
                morse = "ETIANMSURWDKGOHVF*L*PJBXCYZQ**54*3***2 ******16*/*****7***8*90**";
                for (n = 0; n < F2L; n++)
                {
                    // temp[n] = 0;
                    sigs[n] = 0;
                    if (n < FFTlen)
                    {
                        prag[n] = thld0;
                        noise[n] = thld0;
                    }
                    else
                    {
                        prag[n] = thld1;
                        noise[n] = thld1;
                    }
                    ave[n] = aver;
                    sum[n] = 0;
                    tim[n] = 1;
                    old_l[n] = 0;
                    old_r[n] = 0;
                    keyes[n] = false;
                    valid[n] = false;
                    enable[n] = false;
                    output[n] = "";
                    for (z = 0; z < 4; z++)
                        Num[n, z] = 1;
                }
                Cleanup1();
                Cleanup2();
                double v = 2 * Math.PI / totalsamples;
                for (n = 0; n < totalsamples; n++)
                {
                    RealF[n] = 0;
                    ImagF[n] = 0;
                    si[n] = -Math.Sin(n * v);
                    co[n] = Math.Cos(n * v);
                    wd[n] = (0.54 - 0.46 * co[n]) / F2L;
                }
                mycall = MainForm.SetupForm.txtCALL.Text;
                nr_agn1 = false;
                nr_agn2 = false;
                rx_timer1 = ponovi;
                rx_timer2 = ponovi;
                serial = 1;
                repeat1 = false;
                repeat2 = false;
                SCPLoad();

                /*
                morseRunner1.StartSingleCalls();
                Thread.Sleep(100);

                morseRunner2.StartSingleCalls();
                Thread.Sleep(100);

                // SetForegroundWindow();
                */
                int focus = 0;
                MainForm.tx_focus = focus;


                callers[0] = String.Empty;
                callers[FFTlen] = String.Empty;
                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        private void SCPLoad()
        {
            try
            {
                int counter = 0;
                string line;

                System.IO.StreamReader file1 = new System.IO.StreamReader("mr_db.txt");  // sorted calls by mmm

                while ((line = file1.ReadLine()) != null)
                {
                    if (!line.Contains("#")) // && !line.Contains("/") && line.Length > 3 && line.Length < 7)
                    {
                        string[] parts = line.Split(';');
                        scp[counter] = parts[0];
                        cona[counter] = parts[1];
                        counter++;
                    }
                }
                // Array.Sort(scp);
                file1.Close();
                /*
                for (int i = 0; i < counter; i++)
                {
                    line = scp[i];
                    int j = line.IndexOf(";");
                    call = line.Substring(0, j);
                    j++;
                    cona[i] = line.Substring(j, line.Length - j);
                    scp[i] = call;
                }
                */
                Debug.WriteLine("SCP " + counter.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void CW2ASCII(int z)
        {
            try
            {
                char ch = '*';
                if (sum[z] < 63)
                {
                    ch = morse[sum[z] - 1];
                    if (char.IsDigit(ch) || ch == '/' || ch == 'N')
                    {
                        if (z >= FFTlen) { rx_timer2 = ponovi; }
                        else rx_timer1 = ponovi;
                        if (char.IsDigit(ch)) valid[z] = true;
                    }
                }
                else if (sum[z] == 75) ch = '?';
                output[z] += ch;
                sum[z] = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void CWdecode(int z, int t)
        {
            int dt;
            try
            {
                keyes[z] = key;
                dt = t - tim[z];
                tim[z] = t;
                if (key)
                {
                    if (dt > ave[z])
                    {
                        if (sum[z] > 0)
                            CW2ASCII(z);
                    }

                    else
                    {
                        if (dt > dotmin)
                            ave[z] = dt + (ave[z] / 2);
                        sum[z] = 2 * sum[z];
                        if (sum[z] > 75)
                            sum[z] = 0;
                    }
                }

                else
                {
                    sum[z]++;
                    if (dt > ave[z])
                        sum[z]++;
                    else if (dt > dotmin)
                        ave[z] = dt + (ave[z] / 2);
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void Sig2Sym(int z)
        {
            try
            {
                for (int t = 0; t < nofs; t++)
                {
                    if (Mag[z, t] > prag[z])
                        key = true;
                    else
                        key = false;

                    if (key != keyes[z])
                    {
                        CWdecode(z, t + ctr);
                    }
                    else if (!key)
                    {
                        if ((ctr + t - tim[z]) == (ave[z]))
                        {
                            if (sum[z] > 0) { CW2ASCII(z); }
                        }
                        else if ((ctr + t - tim[z]) == (2 * ave[z]))
                        {
                            output[z] += " ";
                            // enable[z] = true; // !qso;  
                        }

                        else if ((ctr + t - tim[z]) == (3 * ave[z]))
                        {
                            enable[z] = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void PowerSpectra(int seq)
        {
            try
            {
                int n, z;
                double[] im0 = new double[F2L];
                double[] re0 = new double[F2L];
                double[] re1 = new double[F2L];
                double[] im1 = new double[F2L];
                double[] im2 = new double[F2L];
                double[] re2 = new double[F2L];

                for (n = 1; n < FFTlen; n++)
                {
                    z = bitrev[n];
                    re0[n] = RealF[z];
                    im0[n] = ImagF[z];



                    z = bitrev[F2L - n];
                    re0[F2L - n] = RealF[z];
                    im0[F2L - n] = ImagF[z];

                    re1[n] = (re0[n] + re0[F2L - n]) / 2;
                    im1[n] = (im0[n] - im0[F2L - n]) / 2;
                    re2[n] = (im0[n] + im0[F2L - n]) / 2;
                    im2[n] = (re0[F2L - n] - re0[n]) / 2;

                    z = n;
                    Mag[z, seq] = Median(Math.Sqrt((re1[n] * re1[n]) + (im1[n] * im1[n])), z);
                    sigs[z] += Mag[z, seq] / nofs;

                    // if (sigs[z] > maxL) { maxL = sigs[z]; }


                    z = n + FFTlen;
                    Mag[z, seq] = Median(Math.Sqrt((re2[n] * re2[n]) + (im2[n] * im2[n])), z);
                    sigs[z] += Mag[z, seq] / nofs;

                    // if (sigs[z] > maxD) { maxD = sigs[z]; }

                }

                Threshold();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void Threshold()
        {
            try
            {
                int n;

                for (n = bwl0; n <= bwh0; n++)
                {
                    prag[n] = (prag[n] + sigs[n - 1] + sigs[n] + sigs[n + 1]) / 4;
                    if (prag[n] < noise[n])
                        prag[n] = noise[n];
                    else
                        if (prag[n] > maxi1[n])
                    {
                        maxi1[n] = prag[n];
                        // cl = n;
                    }

                }


                for (n = bwl1; n <= bwh1; n++)
                {
                    prag[n] = (prag[n] + sigs[n - 1] + sigs[n] + sigs[n + 1]) / 4;
                    if (prag[n] < noise[n])
                        prag[n] = noise[n];
                    else
                        if (prag[n] > maxi2[n])
                    {
                        maxi2[n] = prag[n];
                        // cd = n;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void StereoProc()  //256 data segments with 32 segments and overlap of 4
        {
            try
            {
                int n = 0;
                int fftSegment = 0;
                int inputDataEntry = 0;

                for (n = bwl0 - 1; n <= bwh1 + 1; n++)
                    sigs[n] = 0;

                while (fftSegment < nofs)
                {
                    if (fftSegment < ovrlp - 1)  // old data first
                    {
                        int idePoint = fftSegment * wndw;

                        int oldDataPointer = (ovrlp - 1) * wndw - idePoint;


                        for (n = 0; n < oldDataPointer; n++)

                        {
                            RealF[n] = wd[n] * (short)old_l[n + idePoint]; // (fftSegment * wndw)];
                            ImagF[n] = wd[n] * (short)old_r[n + idePoint]; // (fftSegment * wndw)];
                        }

                        for (n = oldDataPointer; n < F2L; n++)
                        {
                            RealF[n] = wd[n] * (short)read_buffer_l[n + idePoint];
                            ImagF[n] = wd[n] * (short)read_buffer_r[n + idePoint];
                        }

                    }

                    else
                    {
                        for (n = 0; n < F2L; n++)
                        {
                            RealF[n] = wd[n] * (short)read_buffer_l[n + inputDataEntry];
                            ImagF[n] = wd[n] * (short)read_buffer_r[n + inputDataEntry];
                        }
                        inputDataEntry += wndw;
                    }

                    CalcFFT();

                    PowerSpectra(fftSegment);

                    fftSegment++;

                }

                // Debug.Write(Math.Round(maxL / thld0).ToString() + "|" + Math.Round(maxD / thld1).ToString() + " ");
                // maxL = 0; maxD = 0;

                for (n = 0; n < (ovrlp - 1) * wndw; n++)
                {
                    old_l[n] = read_buffer_l[n + inputDataEntry];
                    old_r[n] = read_buffer_r[n + inputDataEntry];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void CalcFFT()
        {
            int i, k, m, mx, I1, I2, I3, I4, I5, x;
            double A1, A2, B1, B2, Z1, Z2;

            I1 = totalsamples / 2;
            I2 = 1;
            for (i = 1; i <= logf2l; i++)
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
                    I3 += (I1 << 1);
                    I4 += (I1 << 1);
                }
                I1 >>= 1;
                I2 <<= 1;
            }
        }

        private double Median(double mag, int z)
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

            return Num[z, 0];
        }

        private void CQ1()
        {
            rts1 = 4;
            // morseRunner1.SendKey(Keys.F1);
            repeat1 = false;
            freq1 = 0;
            call = "";
            Debug.Write(" CQ1 " + (ctr / 32).ToString());
            Cleanup1();
            morseRunner1.SendKey(Keys.F11);
        }


        private void CQ2()
        {
            rts2 = 4;
            // morseRunner2.SendKey(Keys.F1);
            repeat2 = false;
            freq2 = FFTlen;
            call = "";
            Debug.Write(" CQ2 " + (ctr / 32).ToString());
            Cleanup2();
            morseRunner2.SendKey(Keys.F11);
        }


        private void AnalyseDesni()  // finding call or report or AGAIN
        {
            try
            {
                if (rx_timer2 > 0)
                    rx_timer2--;

                for (int z = bwl1; z <= bwh1; z++)
                {
                    Sig2Sym(z);

                    if (output[z].Contains(" ")) // && enable[z] && maxi1[z] > maxi1[z - 1] && maxi1[z] > maxi1[z + 1])  // peak!)
                    {
                        string mystr = output[z].Substring(0, output[z].IndexOf(" "));

                        if (mystr.Contains("R?") || mystr.Contains("AGN"))
                            nr_agn2 = true;

                        else if (mystr.Length >= 3)
                        {
                            if (z >= freq2 - 1 && z <= freq2 + 1) // && mystr.Contains("N"))
                            {
                                int i = mystr.Length;
                                String stev = mystr.Substring(i - 3, 3);
                                rst = "599";
                                //if (i > 0 && mystr.Length > i + 2)
                                //    stev = (mystr.Substring(i + 2, mystr.Length - i - 2));
                                if (stev.Length > 1)
                                {
                                    stev = stev.Replace("N", "9");
                                    stev = stev.Replace("O", "0");
                                    stev = stev.Replace("T", "0");
                                    stev = stev.Replace("A", "1");

                                    Int32.TryParse(stev, out int nr);

                                    if (nr > 0 && nr <= 339 && !rprt_found2)  // zone span limit
                                    {
                                        report = stev;
                                        reports[freq2] = stev;
                                        rprt_found2 = true;
                                        //if (!morseRunner2.IsTransmitting())
                                        morseRunner2.SetSerial(report);
                                        //   if (!morseRunner1.IsTransmitting()) morseRunner2.SendKey(Keys.Enter);
                                    }
                                    /*
                                    else if (i > 0) // && !rprt_found2)
                                    {
                                        //rprt_error2 = true;
                                        Debug.WriteLine("  " + z.ToString() + " - " + stev);
                                    }
                                    */
                                }
                            }

                            if (valid[z])
                            {
                                rxt2 = Array.BinarySearch(scp, mystr);

                                if (rxt2 >= 0 && !mystr.Equals(mycall) && !mystr.Equals(callers[z]) && !call_found2)
                                {
                                    call_found2 = true;
                                    call = mystr;
                                    callers[z] = call;
                                    reports[z] = cona[rxt2];
                                    freq2 = z;
                                    //   activech2 = z;
                                    Debug.WriteLine("  " + freq2.ToString() + "  " + call);
                                    //if (!morseRunner1.IsTransmitting())
                                    morseRunner2.SetCallsign(call);
                                    //   for (int i = z - 2; i < z + 3; i++) maxi1[i] = 0;
                                }
                            }
                        }
                        output[z] = String.Empty;
                        valid[z] = false;
                        // enable[z] = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /*
                    if (freq2 > FFTlen && z >= freq2 - 1 && z <= freq2 + 1) // && maxi2[z] > maxi2[z - 1] && maxi2[z] > maxi2[z + 1])  // peak!
                    {
                        //  for (int k = z - 2; k < z + 3; k++) maxi2[k] = 0;
                        //  if (mystr.StartsWith("R")) mystr = mystr.Remove(0, 1);
                        int i = mystr.IndexOf("NN");
                        //  int j = mystr.IndexOf("NT");  // leading zero case
                        String stev = mystr;
                        //    report = String.Empty;
                        //    reports[freq2] = String.Empty;
                        rst = "599";
                        if (i > 0 && mystr.Length > i + 2)
                            stev = (mystr.Substring(i + 2, mystr.Length - i - 2));
                        //  else if (j > 0 && mystr.Length > j + 2)
                        //      stev = mystr.Substring(j + 1, mystr.Length - j - 1);
                        if (stev.Length > 0)
                        {
                            stev = stev.Replace("N", "9");
                            stev = stev.Replace("O", "0");
                            stev = stev.Replace("T", "0");
                            stev = stev.Replace("A", "1");

                            if (stev.Contains(reports[freq2]) && !rprt_found2)
                            {
                                rprt_found2 = true;
                                report = reports[freq2];
                                morseRunner2.SetSerial(report);
                            }
                            //else rprt_error2 = true;



                            Int32.TryParse(stev, out int nr);

                            if (nr > 0 && nr <= 40 && !rprt_found2)  // zone span limit
                            {
                                report = stev;
                                reports[freq2] = stev;
                                rprt_found2 = true;
                                //if (!morseRunner2.IsTransmitting())
                                morseRunner2.SetSerial(report);
                                //   if (!morseRunner1.IsTransmitting()) morseRunner2.SendKey(Keys.Enter);
                            }

                            else if (i > 0 || j > 0) // && !rprt_found2)
                            {
                                //rprt_error2 = true;
                                Debug.WriteLine("  " + z.ToString() + " - " + stev);
                            }

                        */

        private void AnalyseLevi()  // finding call or report or AGAIN
        {
            try
            {
                if (rx_timer1 > 0)
                    rx_timer1--;

                for (int z = bwl0; z <= bwh0; z++)
                {
                    Sig2Sym(z);

                    if (output[z].Contains(" ")) // && enable[z] && maxi1[z] > maxi1[z - 1] && maxi1[z] > maxi1[z + 1])  // peak!)
                    {
                        string mystr = output[z].Substring(0, output[z].IndexOf(" "));

                        if (mystr.Contains("R?") || mystr.Contains("AGN"))
                            nr_agn1 = true;

                        else if (mystr.Length >= 3)
                        {

                            if (z >= freq1 - 1 && z <= freq1 + 1) // && mystr.Contains("N"))
                            {
                                //  for (int k = z - 2; k < z + 3; k++) maxi1[k] = 0;
                                //   if (mystr.StartsWith("R")) mystr = mystr.Remove(0, 1);
                                //int i = mystr.IndexOf("NN");
                                //int j = mystr.IndexOf("NT");  // leading zero case
                                int i = mystr.Length;
                                String stev = mystr.Substring(i - 3, 3);
                                //   report = String.Empty;
                                //   reports[z] = String.Empty;
                                rst = "599";
                                //if (i > 0 && mystr.Length > i + 2)
                                //    stev = (mystr.Substring(i + 2, mystr.Length - i - 2));
                                //else if (j > 0 && mystr.Length > j + 2)
                                //    stev = mystr.Substring(j + 1, mystr.Length - j - 1);


                                if (stev.Length > 0)
                                {
                                    stev = stev.Replace("N", "9");
                                    stev = stev.Replace("O", "0");
                                    stev = stev.Replace("T", "0");
                                    stev = stev.Replace("A", "1");

                                    /*  zone confirmation
                                    if (stev.Contains(reports[freq1]) && !rprt_found1)
                                    {
                                        rprt_found1 = true;
                                        report = reports[freq1];
                                        morseRunner1.SetSerial(report);
                                    }
                                    //else rprt_error1 = true;
                                    */


                                    Int32.TryParse(stev, out int nr);
                                    if (nr > 0 && nr <= 339 && !rprt_found1)  // zone span limit
                                    {
                                        report = stev;
                                        reports[freq1] = report;
                                        rprt_found1 = true;
                                        //if (!morseRunner1.IsTransmitting())
                                        morseRunner1.SetSerial(report);
                                    }
                                    /*
                                    else if (i > 0) // && !rprt_found1)
                                    {
                                        //rprt_error1 = true;
                                        Debug.WriteLine("  " + z.ToString() + " - " + stev);
                                    }
                                    */
                                }
                            }

                            if (valid[z])
                            {
                                rxt1 = Array.BinarySearch(scp, mystr);

                                if (rxt1 >= 0 && !mystr.Equals(mycall) && !mystr.Equals(callers[z])) // && !call_found1)
                                {
                                    call_found1 = true;
                                    call = mystr;
                                    callers[z] = call;
                                    reports[z] = cona[rxt1];
                                    freq1 = z;
                                    // activech1 = z;
                                    Debug.WriteLine("  " + freq1.ToString() + "  " + call);
                                    //if (!morseRunner1.IsTransmitting())
                                    morseRunner1.SetCallsign(call);
                                    //   for (int i = z - 2; i < z + 3; i++) maxi1[i] = 0;
                                }
                            }

                        }
                        // enable[z] = false;
                        output[z] = string.Empty;
                        valid[z] = false;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void Cleanup1()
        {
            try
            {
                for (int n = bwl0; n <= bwh0; n++)
                {
                    reports[n] = String.Empty;
                    callers[n] = String.Empty;
                    enable[n] = false;
                    output[n] = String.Empty;
                    //   prag[n] = thld0;
                    valid[n] = false;
                    ave[n] = aver;
                    maxi1[n] = 0;
                }
                call_found1 = false;
                rprt_found1 = false;
                rprt_error1 = false;
                nr_agn1 = false;
                rx_timer1 = ponovi;
                // freq1 = 0;
                //qso1 = false;
                repeat1 = false;
                //  morseRunner1.SendKey(Keys.F11);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Cleanup2()
        {
            for (int n = bwl1; n <= bwh1; n++)
            {
                reports[n] = String.Empty;
                callers[n] = String.Empty;
                enable[n] = false;
                output[n] = String.Empty;
                //  prag[n] = thld0;
                valid[n] = false;
                ave[n] = aver;
                maxi2[n] = 0;
            }
            call_found2 = false;
            rprt_found2 = false;
            rprt_error2 = false;
            nr_agn2 = false;
            rx_timer2 = ponovi;
            // freq2 = FFTlen;
            //qso2 = false;
            repeat2 = false;
            //   morseRunner2.SendKey(Keys.F11);
        }


        private void RespondLevi()  // TX procedure
        {
            try
            {
                if (!qso1 && freq1 > 0 && freq1 < FFTlen && call_found1 && enable[freq1]) // initiate ESM
                {
                    enable[freq1] = false;
                    call = callers[freq1];
                    morseRunner1.SetCallsign(call);
                    //     morseRunner1.SendKey(Keys.Enter);
                    rts1 = 1;
                    rx_timer1 = ponovi;
                    repeat1 = true;
                    //qso1 = true;
                    call_found1 = false;
                }

                else if (nr_agn1)  // repeat report
                {
                    repeat1 = false;
                    nr_agn1 = false;
                    //     morseRunner1.SendKey(Keys.F2);
                    rts1 = 2;
                    rx_timer1 = ponovi;
                }
                /*
                else if (freq1 > 0 && freq1 < FFTlen && rprt_error1)
                {
                    rprt_error1 = false;
                    morseRunner1.SendKey(Keys.F7);
                    reports[freq1] = String.Empty;
                    rx_timer1 = ponovi;
                }
                */
                else if (freq1 > 0 && freq1 < FFTlen && rprt_found1 && enable[freq1])
                {
                    enable[freq1] = false;
                    report = reports[freq1];
                    //    morseRunner1.SetSerial(report);
                    //   morseRunner1.SendKey(Keys.Enter);
                    //   morseRunner1.SendKey(Keys.F4);
                    //if (qso1)
                    rts1 = 6;
                    //else rts1 = 5;
                    Cleanup1();
                    //   activech1 = 0;
                    call_found1 = false;
                    rprt_found1 = false;
                }

                else if (rx_timer1 == 0 && repeat1)
                {
                    rx_timer1 = ponovi;
                    repeat1 = false;
                    rts1 = 3;
                    //    morseRunner1.SendKey(Keys.F5);
                    //   morseRunner1.SendKey(Keys.F2);
                }

                else if (rx_timer1 == 0)
                    CQ1();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }




        private void RespondDesni()  // TX procedure
        {
            try
            {
                if (!qso2 && freq2 > FFTlen && call_found2 && enable[freq2]) // initiate ESM
                {
                    call = callers[freq2];
                    //  morseRunner2.SetCallsign(call);
                    // morseRunner2.SendKey(Keys.Enter);
                    rts2 = 1;
                    rx_timer2 = ponovi;
                    repeat2 = true;
                    //qso2 = true;
                    enable[freq2] = false;
                }

                else if (nr_agn2)  // repeat report
                {
                    rx_timer2 = ponovi;
                    repeat2 = false;
                    nr_agn2 = false;
                    //  morseRunner2.SendKey(Keys.F2);
                    rts2 = 2;
                }
                /*
                else if (freq2 > FFTlen && rprt_error2)
                {
                    rprt_error2 = false;
                    morseRunner2.SendKey(Keys.F7);
                    reports[freq2] = String.Empty;
                    rx_timer2 = ponovi;
                }
                */
                else if (freq2 > FFTlen && rprt_found2 && enable[freq2])
                {
                    enable[freq2] = false;
                    rprt_found2 = false;
                    report = reports[freq2];

                    //  rst = "599";
                    //  morseRunner2.SetReport(rst);
                    //   morseRunner2.SetSerial(report);
                    //  morseRunner2.SendKey(Keys.Enter);
                    //   morseRunner2.SendKey(Keys.F4);
                    rts2 = 6;
                    Cleanup2();
                    // activech2 = FFTlen;
                    call_found2 = false;

                }

                else if (rx_timer2 == 0 && repeat2)
                {
                    repeat2 = false;
                    //   morseRunner2.SendKey(Keys.F5);
                    //   morseRunner2.SendKey(Keys.F2);
                    rx_timer2 = ponovi;
                    rts2 = 3;
                }

                else if (rx_timer2 == 0)
                    CQ2();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
