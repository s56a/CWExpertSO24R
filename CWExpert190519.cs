//=================================================================
// CWExpert.cs
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections;
using System.Threading;
using AutoItHelper;
using System.Text.RegularExpressions;



namespace CWExpert
{
    public partial class CWExpert190519 : Form
    {
        #region variable definition


        MessageHelper msg;
        public Setup SetupForm;
        private int[,] Fxp = new int[2, 22];
        public CWDecode cwDecoder;

        #endregion

        #region properites

        public ListBox clbl = new ListBox();

        public ListBox clbr = new ListBox();

        private bool mrIsRunning = false;

        public int tx_focus = 0;

        public bool so2r = false;

        public bool MRIsRunning
        {
            get { return mrIsRunning; }
            set
            {
                if (mrIsRunning)
                    btnStopMR_Click(null, null);
                Thread.Sleep(100);
                mrIsRunning = value;
                if (value)
                    btnStartMR_Click(null, null);
            }
        }

        public void rx_lights()
        {
            if (tx_focus != 0)
            {
                txb.BackColor = Color.White;
                rxb.BackColor = Color.LightGreen;
                if (so2r) { rxa.BackColor = Color.LightGreen; }
            }
            else
            {
                txa.BackColor = Color.White;
                rxa.BackColor = Color.LightGreen;
                if (so2r) { rxb.BackColor = Color.LightGreen; }
            }
        }

        public void tx_lights()
        {
            if (tx_focus != 0)
            {
                txb.BackColor = Color.LightPink;
                rxb.BackColor = Color.White;
                if (so2r) { rxa.BackColor = Color.LightGreen; }
            }
            else
            {
                txa.BackColor = Color.LightPink;
                rxa.BackColor = Color.White;
                if (so2r) { rxb.BackColor = Color.LightGreen; }
            }
        }

        public string txtCALL
        {
            set { this.txtCall.Text = value; }
        }

        public string txtNR
        {
            set { this.txtNr.Text = value; }
        }

        public string txtRst
        {
            set { this.txtRST.Text = value; }
        }

        public string txtrCALL
        {
            set { this.txtrCall.Text = value; }
        }

        public string txtrNR
        {
            set { this.txtrNbr.Text = value; }
        }

        public string txtrRst
        {
            set { this.txtrRST.Text = value; }
        }

        public void tx_change()
        {
            if (tx_focus == 0)
            {
                tx_focus = 1;
                //   morseRunner2.FocusMorseRunnerWindow();
                //   msg.bringAppToFront(Fxp[1, 0]);
                btncall.BackColor = Color.LightGreen;
                btnrst.BackColor = Color.LightGreen;
                btnnbr.BackColor = Color.LightGreen;

                btnSendCall.BackColor = Color.White;
                btnSendRST.BackColor = Color.White;
                btnSendNr.BackColor = Color.White;
            }
            else
            {
                tx_focus = 0;
                //   morseRunner1.FocusMorseRunnerWindow();
                //   msg.bringAppToFront(Fxp[0, 0]);
                btnSendCall.BackColor = Color.LightPink;
                btnSendRST.BackColor = Color.LightPink;
                btnSendNr.BackColor = Color.LightPink;

                btncall.BackColor = Color.White;
                btnrst.BackColor = Color.White;
                btnnbr.BackColor = Color.White;
            }
            tx_lights();
        }

        #endregion

        #region constructor

        public CWExpert190519()
        {
            InitializeComponent();
            msg = new MessageHelper();
            DB.AppDataPath = Application.StartupPath;
            DB.Init();
            Audio.MainForm = this;
            PA19.PA_Initialize();
            SetupForm = new Setup(this);
            cwDecoder = new CWDecode(this);
            btnStartMR.BackColor = Color.LightBlue;
        }

        #endregion

        #region misc function

        /*
        private bool EnsureRemoteWindow()
        {
            if (!morseRunner1.IsReady())
                return false;
            else if (!morseRunner2.IsReady())
                return false;
            else
            {
                so2r = true;
                return true;
            }
        }
        */
        #endregion

        #region MR Functions keys


        public void btnF1_Click(object sender, EventArgs e)

        {
            try
            {
                if (Fxp[tx_focus, 1] != 0)
                {
                    tx_lights();
                    msg.bringAppToFront(Fxp[tx_focus, 0]);
                    msg.bringAppToFront(Fxp[tx_focus, 1]);
                    AutoItHelper.AutoItX.Send("{F1}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void btnF2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Fxp[tx_focus, 2] != 0)
                {
                    tx_lights();
                    msg.bringAppToFront(Fxp[tx_focus, 0]);
                    msg.bringAppToFront(Fxp[tx_focus, 2]);
                    AutoItHelper.AutoItX.Send("{F2}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void btnF3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Fxp[tx_focus, 3] != 0)
                {
                    tx_lights();
                    msg.bringAppToFront(Fxp[tx_focus, 0]);
                    msg.bringAppToFront(Fxp[tx_focus, 3]);
                    AutoItHelper.AutoItX.Send("{F3}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void btnF4_Click(object sender, EventArgs e)
        {
            try
            {
                if (Fxp[tx_focus, 4] != 0)
                {
                    tx_lights();
                    msg.bringAppToFront(Fxp[tx_focus, 0]);
                    msg.bringAppToFront(Fxp[tx_focus, 4]);
                    AutoItHelper.AutoItX.Send("{F4}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void btnF5_Click(object sender, EventArgs e)
        {
            try
            {
                if (Fxp[tx_focus, 5] != 0)
                {
                    tx_lights();
                    msg.bringAppToFront(Fxp[tx_focus, 0]);
                    msg.bringAppToFront(Fxp[tx_focus, 5]);
                    AutoItHelper.AutoItX.Send("{F5}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void btnF6_Click(object sender, EventArgs e)
        {
            try
            {
                if (Fxp[tx_focus, 6] != 0)
                {
                    tx_lights();
                    msg.bringAppToFront(Fxp[tx_focus, 0]);
                    msg.bringAppToFront(Fxp[tx_focus, 6]);
                    AutoItHelper.AutoItX.Send("{F6}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void btnF7_Click(object sender, EventArgs e)
        {
            try
            {
                if (Fxp[tx_focus, 7] != 0)
                {
                    tx_lights();
                    msg.bringAppToFront(Fxp[tx_focus, 0]);
                    msg.bringAppToFront(Fxp[tx_focus, 7]);
                    AutoItHelper.AutoItX.Send("{F7}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void btnF8_Click(object sender, EventArgs e)
        {
            try
            {
                if (Fxp[tx_focus, 8] != 0)
                {
                    tx_lights();
                    msg.bringAppToFront(Fxp[tx_focus, 0]);
                    msg.bringAppToFront(Fxp[tx_focus, 8]);
                    AutoItHelper.AutoItX.Send("{F8}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void btnF9_Click(object sender, EventArgs e)
        {
            try
            {
                if (Fxp[tx_focus, 9] != 0)
                {
                    tx_lights();
                    msg.bringAppToFront(Fxp[tx_focus, 0]);
                    msg.bringAppToFront(Fxp[tx_focus, 9]);
                    AutoItHelper.AutoItX.Send("{F9}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void btnF10_Click(object sender, EventArgs e)
        {
            try
            {
                if (Fxp[tx_focus, 0] != 0)
                {
                    tx_lights();
                    msg.bringAppToFront(Fxp[tx_focus, 0]);
                    msg.bringAppToFront(Fxp[tx_focus, 10]);
                    AutoItHelper.AutoItX.Send("{F10}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void btnF11_Click(object sender, EventArgs e)
        {
            try
            {
                if (Fxp[tx_focus, 11] != 0)
                {
                    tx_lights();
                    msg.bringAppToFront(Fxp[tx_focus, 0]);
                    msg.bringAppToFront(Fxp[tx_focus, 11]);
                    AutoItHelper.AutoItX.Send("{F11}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void btnF12_Click(object sender, EventArgs e)
        {
            try
            {
                if (Fxp[tx_focus, 12] != 0)
                {
                    tx_lights();
                    msg.bringAppToFront(Fxp[tx_focus, 0]);
                    msg.bringAppToFront(Fxp[tx_focus, 12]);
                    AutoItHelper.AutoItX.Send("{F12}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion


        #region MorseRunner keys

        private void btnStartMR_Click(object sender, EventArgs e)
        {
            try
            {
                if (!mrIsRunning)
                {
                    btnStartMR.BackColor = Color.Gold;
                    btnStopMR.BackColor = Color.LightSkyBlue;
                    mrIsRunning = true;
                    Audio.callback_return = 0;
                    if (cwDecoder.AudioEvent == null)
                        cwDecoder.AudioEvent = new AutoResetEvent(false);
                    Audio.Start();

                    /* 
                    if (EnsureRemoteWindow())  //so2r true!
                        butdva.BackColor = Color.LightPink;
                    tx_focus = 0;



                    if (!morseRunner1.IsReady())
                    {
                        Console.WriteLine("Error radio 1");
                        return;
                    }
                    else if (morseRunner2.IsReady())
                    {
                        tx_focus = 1;
                        tx_change();
                        rx_lights();
                    }
                    */

                    cwDecoder.CWdecodeStart();

                    //Console.WriteLine("Started SO2R " + so2r.ToString());


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Morse Runner not running?\n" + ex.ToString());
            }
        }

        private void Clearbtns()
        {

            btnStartMR.BackColor = Color.LightBlue;
            btnStopMR.BackColor = Color.White;
            rxa.BackColor = Color.White;
            rxb.BackColor = Color.White;
            btnStopMR.BackColor = Color.White;
            txa.BackColor = Color.White;
            txb.BackColor = Color.White;
            btnSendCall.BackColor = Color.White;
            btnSendRST.BackColor = Color.White;
            btnSendNr.BackColor = Color.White;
            btncall.BackColor = Color.White;
            btnrst.BackColor = Color.White;
            btnnbr.BackColor = Color.White;
        }

        private void btnStopMR_Click(object sender, EventArgs e)
        {
            try
            {
                if (mrIsRunning)
                {
                    Clearbtns();
                    mrIsRunning = false;
                    Thread.Sleep(100);
                    morseRunner1.Stop();
                    Thread.Sleep(100);
                    morseRunner2.Stop();
                    Thread.Sleep(100);
                    Audio.callback_return = 2;
                    cwDecoder.CWdecodeStop();
                    Audio.StopAudio();
                    Thread.Sleep(100);
                    cwDecoder.AudioEvent.Close();
                    cwDecoder.AudioEvent = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Morse Runner not running?\n" + ex.ToString());
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void setupMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (SetupForm != null)
                    SetupForm.Show();
                else
                {
                    SetupForm = new Setup(this);
                    SetupForm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening Setup!\n" + ex.ToString());
            }
        }
        /*
                private bool raem_check(String testValue)
                {
                    Regex _raem = new Regex("^(([0-9])|([0-8][0-9])|(90))[NS]((180)|(1[0-7][0-9])|([0-9][0-9])|([0-9]))[OW]$");
                    return (_raem.IsMatch(testValue));
                }
         */

        int i = 0;

        public void btnSendCall_Click(object sender, EventArgs e)      // button call
        {
            i++;
            switch (i)
            {
                case 1:
                    textBox1.Text = txtCall.Text;
                    break;
                case 2:
                    textBox2.Text = txtCall.Text;
                    break;
                case 3:
                    textBox3.Text = txtCall.Text;
                    break;
                case 4:
                    i = 0;
                    break;
            }

            if (tx_focus != 0) { tx_change(); }
            else { tx_lights(); }
            if (Fxp[tx_focus, 13] != 0)
            {
                msg.bringAppToFront(Fxp[tx_focus, 0]);
                msg.bringAppToFront(Fxp[tx_focus, 13]);
                AutoItHelper.AutoItX.Send(txtCall.Text + "{ENTER}");
            }
        }

        int j = 3;

        public void btncall_Click(object sender, EventArgs e)      // button rcall
        {
            j++;
            switch (j)
            {
                case 4:
                    textBox4.Text = txtrCall.Text;
                    break;
                case 5:
                    textBox5.Text = txtrCall.Text;
                    break;
                case 6:
                    textBox6.Text = txtrCall.Text;
                    break;
                case 7:
                    j = 3;
                    break;
            }

            if (tx_focus == 0) { tx_change(); }
            else { tx_lights(); }
            if (Fxp[tx_focus, 13] != 0)
            {
                msg.bringAppToFront(Fxp[tx_focus, 0]);
                msg.bringAppToFront(Fxp[tx_focus, 13]);
                AutoItHelper.AutoItX.Send(txtrCall.Text + "{ENTER}");
            }
        }

        public void btnSendRST_Click(object sender, EventArgs e)       // button RST
        {
            try
            {
                if (tx_focus != 0) { tx_change(); }
                else { tx_lights(); }
                if (Fxp[tx_focus, 16] != 0)
                {
                    msg.bringAppToFront(Fxp[tx_focus, 0]);
                    msg.bringAppToFront(Fxp[tx_focus, 16]);
                    //                    if (raem_check(txtRST.Text))
                    AutoItHelper.AutoItX.Send(txtRST.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void btnrst_Click(object sender, EventArgs e)       // button rRST
        {
            try
            {
                if (tx_focus == 0) { tx_change(); }
                else { tx_lights(); }
                if (Fxp[tx_focus, 16] != 0)
                {
                    //                    string s = txtrRST.Text.Substring(1, 1);
                    msg.bringAppToFront(Fxp[tx_focus, 0]);
                    msg.bringAppToFront(Fxp[tx_focus, 16]);
                    AutoItHelper.AutoItX.Send(txtrRST.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void btnSendNr_Click(object sender, EventArgs e)        // button Nr
        {
            try
            {
                if (tx_focus != 0) { tx_change(); }
                else { tx_lights(); }
                if (Fxp[tx_focus, 17] != 0)
                {
                    msg.bringAppToFront(Fxp[tx_focus, 0]);
                    msg.bringAppToFront(Fxp[tx_focus, 17]);
                    AutoItHelper.AutoItX.Send(txtNr.Text + "{ENTER}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void btnnbr_Click(object sender, EventArgs e)        // button Nr
        {
            try
            {
                if (tx_focus == 0) { tx_change(); }
                else { tx_lights(); }
                if (Fxp[tx_focus, 17] != 0)
                {
                    msg.bringAppToFront(Fxp[tx_focus, 0]);
                    msg.bringAppToFront(Fxp[tx_focus, 17]);
                    AutoItHelper.AutoItX.Send(txtrNbr.Text + "{ENTER}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        public void btnleft_Click(object sender, EventArgs e)       // button RST
        {
            try
            {
                if (tx_focus != 0) { tx_change(); }
                else { tx_lights(); }
                rx_lights();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void btnright_Click(object sender, EventArgs e)       // button RST
        {
            try
            {
                if (tx_focus == 0) { tx_change(); }
                else { tx_lights(); }
                rx_lights();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void butesc_Click(object sender, EventArgs e)
        {
            try
            {
                if (Fxp[tx_focus, 0] != 0)
                {
                    msg.bringAppToFront(Fxp[tx_focus, 0]);
                    msg.bringAppToFront(Fxp[tx_focus, 18]);
                    AutoItHelper.AutoItX.Send("{ESCAPE}");
                    rx_lights();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtNr_KeyUp(object sender, KeyEventArgs e)     // Nr + enter
        {
            if (e.KeyCode == Keys.Enter)
                btnSendNr_Click(null, null);
        }

        private void txtRST_KeyUp(object sender, KeyEventArgs e)    // RST + enter
        {
            if (e.KeyCode == Keys.Enter)
                btnSendRST_Click(null, null);
        }

        private void txtCall_KeyUp(object sender, KeyEventArgs e)   // Call + enter
        {
            if (e.KeyCode == Keys.Enter)
                btnSendCall_Click(null, null);
        }
        private void txtrNbr_KeyUp(object sender, KeyEventArgs e)     // Nr + enter
        {
            if (e.KeyCode == Keys.Enter)
                btnnbr_Click(null, null);
        }

        private void txtrRST_KeyUp(object sender, KeyEventArgs e)    // RST + enter
        {
            if (e.KeyCode == Keys.Enter)
                btnrst_Click(null, null);
        }

        private void txtrCall_KeyUp(object sender, KeyEventArgs e)   // Call + enter
        {
            if (e.KeyCode == Keys.Enter)
                btncall_Click(null, null);
        }

        public void CWExpert_KeyUp(object sender, KeyEventArgs e)      // function keys F1...F12
        {

            switch (e.KeyCode)
            {
                case Keys.F1:
                    btnF1_Click(null, null);
                    break;
                case Keys.F2:
                    btnF2_Click(null, null);
                    break;
                case Keys.F3:
                    btnF3_Click(null, null);
                    break;
                case Keys.F4:
                    btnF4_Click(null, null);
                    break;
                case Keys.F5:
                    btnF5_Click(null, null);
                    break;
                case Keys.F6:
                    btnF6_Click(null, null);
                    break;
                case Keys.F7:
                    btnF7_Click(null, null);
                    break;
                case Keys.F8:
                    btnF8_Click(null, null);
                    break;
                case Keys.F9:
                    btnF9_Click(null, null);
                    break;
                case Keys.F10:
                    btnF10_Click(null, null);
                    break;
                case Keys.F11:
                    btnF11_Click(null, null);
                    break;
                case Keys.F12:
                    btnF12_Click(null, null);
                    break;
            }
        }

        #endregion



        static String mrPath = @"C:\Ham\CWExpertMM\bin\Debug\MorseRunner.exe";
        static String callsign = "S56A";

        // Will start MR and collect all references to process and window

        MorseRunnerHelper morseRunner1 = new MorseRunnerHelper(mrPath, callsign, 1);
        MorseRunnerHelper morseRunner2 = new MorseRunnerHelper(mrPath, callsign, 2);

        /*
        static String mrPath = @"C:\src\MorseRunnerRemote\MorseRunnerRemote\bin\Debug\MorseRunner.exe";
        static String callsign = "DL7NX";

        // Will start MR and collect all references to process and window
        MorseRunnerHelper morseRunner1 = new MorseRunnerHelper(mrPath, callsign, 1);
        MorseRunnerHelper morseRunner2 = new MorseRunnerHelper(mrPath, callsign, 2);
        */

        private void CWExpert_Load(object sender, EventArgs e)
        {

        }

        private void txtrCall_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtrRST_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtrNbr_TextChanged(object sender, EventArgs e)
        {

        }

        private void butdva_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (so2r)
                {
                    so2r = false;
                    butdva.BackColor = Color.White;
                }
                else
                {
                    so2r = true;
                    butdva.BackColor = Color.LightPink;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtRST_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }
    }
}
