using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace CWExpert
{
    public partial class Setup : Form
    {
        private CWExpert190519 MainForm;

        public Setup(CWExpert190519 main_form)
        {
            InitializeComponent();
            MainForm = main_form;
            InitAudio();
            GetOptions();
        }

        private void InitAudio()
        {
            try
            {
                GetHosts();

                if (comboAudioDriver.SelectedIndex < 0 &&
                    comboAudioDriver.Items.Count > 0)
                    comboAudioDriver.SelectedIndex = 0;

                if (comboAudioSampleRate.Items.Count > 0)
                    comboAudioSampleRate.SelectedIndex = 0;

                GetDevices();
                comboAudioBuffer.Text = "64";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in init audio!\n" + ex.ToString());
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                SaveOptions();
                DB.Update();
            }
            catch
            {
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                SaveOptions();
                DB.Update();
                this.Hide();
            }
            catch
            {
            }
        }

        private void comboAudioDriver_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               Audio.Host = ((PADeviceInfo)comboAudioDriver.SelectedItem).Index;
               GetDevices();
               if (comboAudioInput.Items.Count != 0)
                   comboAudioInput.SelectedIndex = 0;
               if (comboAudioOutput.Items.Count != 0)
                   comboAudioOutput.SelectedIndex = 0;
            }
            catch
            {
            }
        }

        private void comboAudioInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = ((PADeviceInfo)comboAudioInput.SelectedItem).Index;
                Audio.Input = index;
            }
            catch(Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void comboAudioOutput_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = ((PADeviceInfo)comboAudioOutput.SelectedItem).Index;
                Audio.Output = index;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void Setup_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                SaveOptions();
                DB.Update();
                this.Hide();
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Setup closing!\n" + ex.ToString());
            }
        }

        private void GetDevices()
        {
            try
            {
                comboAudioInput.Items.Clear();
                comboAudioOutput.Items.Clear();
                int host = ((PADeviceInfo)comboAudioDriver.SelectedItem).Index;
                ArrayList a = Audio.GetPAInputDevices(host);

                foreach (PADeviceInfo p in a)
                    comboAudioInput.Items.Add(p);

                a = Audio.GetPAOutputDevices(host);
                foreach (PADeviceInfo p in a)
                    comboAudioOutput.Items.Add(p);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while init audio drivers!\n" + ex.ToString());
            }
        }

        private void GetHosts()
        {
            try
            {
                comboAudioDriver.Items.Clear();
                int host_index = 0;
                foreach (string PAHostName in Audio.GetPAHosts())
                {
                    if (Audio.GetPAInputDevices(host_index).Count > 0)
                    {
                        comboAudioDriver.Items.Add(new PADeviceInfo(PAHostName, host_index));
                    }
                    host_index++; //Increment host index
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error while init audio devices!\n" + ex.ToString());
            }
        }

        public void SaveOptions()
        {
            try
            {
                ArrayList a = new ArrayList();
                ArrayList temp = new ArrayList();

                ControlList(this, ref temp);

                foreach (Control c in temp)				// For each control
                {
                    if (c.GetType() == typeof(CheckBox))
                        a.Add(c.Name + "/" + ((CheckBox)c).Checked.ToString());
                    else if (c.GetType() == typeof(ComboBox))
                    {
                        a.Add(c.Name + "/" + ((ComboBox)c).Text);
                    }
                    else if (c.GetType() == typeof(NumericUpDown))
                        a.Add(c.Name + "/" + ((NumericUpDown)c).Value.ToString());
                    else if (c.GetType() == typeof(RadioButton))
                        a.Add(c.Name + "/" + ((RadioButton)c).Checked.ToString());
                    else if (c.GetType() == typeof(TextBox))
                        a.Add(c.Name + "/" + ((TextBox)c).Text);
                    else if (c.GetType() == typeof(TrackBar))
                        a.Add(c.Name + "/" + ((TrackBar)c).Value.ToString());
                }

                DB.SaveVars("Options", ref a);		// save the values to the DB

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in SaveOptions function!\n" + ex.ToString());
            }
        }

        private void ControlList(Control c, ref ArrayList a)
        {
            if (c.Controls.Count > 0)
            {
                foreach (Control c2 in c.Controls)
                    ControlList(c2, ref a);
            }

            if (c.GetType() == typeof(CheckBox) || c.GetType() == typeof(CheckBox) ||
                c.GetType() == typeof(ComboBox) || c.GetType() == typeof(ComboBox) ||
                c.GetType() == typeof(NumericUpDown) || c.GetType() == typeof(NumericUpDown) ||
                c.GetType() == typeof(RadioButton) || c.GetType() == typeof(RadioButton) ||
                c.GetType() == typeof(TextBox) || c.GetType() == typeof(TextBox) ||
                c.GetType() == typeof(TrackBar) || c.GetType() == typeof(TrackBar))
                a.Add(c);
        }

        public void GetOptions()
        {
            try
            {
                // get list of live controls
                ArrayList temp = new ArrayList();		// list of all first level controls
                ControlList(this, ref temp);

                ArrayList checkbox_list = new ArrayList();
                ArrayList combobox_list = new ArrayList();
                ArrayList numericupdown_list = new ArrayList();
                ArrayList radiobutton_list = new ArrayList();
                ArrayList textbox_list = new ArrayList();
                ArrayList trackbar_list = new ArrayList();
                ArrayList colorbutton_list = new ArrayList();

                foreach (Control c in temp)
                {
                    if (c.GetType() == typeof(CheckBox))
                        checkbox_list.Add(c);
                    else if (c.GetType() == typeof(ComboBox))
                        combobox_list.Add(c);
                    else if (c.GetType() == typeof(NumericUpDown))
                        numericupdown_list.Add(c);
                    else if (c.GetType() == typeof(RadioButton))
                        radiobutton_list.Add(c);
                    else if (c.GetType() == typeof(TextBox))
                        textbox_list.Add(c);
                    else if (c.GetType() == typeof(TrackBar))
                        trackbar_list.Add(c);
                }

                temp.Clear();	// now that we have the controls we want, delete first list 

                ArrayList a = DB.GetVars("Options");
                a.Sort();

                // restore saved values to the controls
                foreach (string s in a)				// string is in the format "name,value"
                {
                    string[] vals = s.Split('/');
                    if (vals.Length > 2)
                    {
                        for (int i = 2; i < vals.Length; i++)
                            vals[1] += "/" + vals[i];
                    }

                    string name = vals[0];
                    string val = vals[1];

                    if (s.StartsWith("chk"))
                    {
                        for (int i = 0; i < checkbox_list.Count; i++)
                        {
                            CheckBox c = (CheckBox)checkbox_list[i];
                            if (c.Name.Equals(name))		// name found
                            {
                                c.Checked = bool.Parse(val);	// restore value
                                i = checkbox_list.Count + 1;
                            }
                            if (i == checkbox_list.Count)
                                MessageBox.Show("Control not found: " + name);
                        }
                    }
                    else if (s.StartsWith("combo"))	// control is a ComboBox
                    {
                        for (int i = 0; i < combobox_list.Count; i++)
                        {	// look through each control to find the matching name
                            ComboBox c = (ComboBox)combobox_list[i];
                            if (c.Name.Equals(name))		// name found
                            {
                                if (c.Items.Count > 0 && c.Items[0].GetType() == typeof(string))
                                {
                                    c.Text = val;
                                }
                                else
                                {
                                    foreach (object o in c.Items)
                                    {
                                        if (o.ToString() == val)
                                            c.Text = val;	// restore value
                                    }
                                }
                                i = combobox_list.Count + 1;
                            }
                            if (i == combobox_list.Count)
                                MessageBox.Show("Control not found: " + name);
                        }
                    }
                    else if (s.StartsWith("ud"))
                    {
                        for (int i = 0; i < numericupdown_list.Count; i++)
                        {	// look through each control to find the matching name
                            NumericUpDown c = (NumericUpDown)numericupdown_list[i];
                            if (c.Name.Equals(name))		// name found
                            {
                                decimal num = decimal.Parse(val);

                                if (num > c.Maximum) num = c.Maximum;		// check endpoints
                                else if (num < c.Minimum) num = c.Minimum;
                                c.Value = num;			// restore value
                                i = numericupdown_list.Count + 1;
                            }
                            if (i == numericupdown_list.Count)
                                MessageBox.Show("Control not found: " + name);
                        }
                    }
                    else if (s.StartsWith("rad"))
                    {	// look through each control to find the matching name
                        for (int i = 0; i < radiobutton_list.Count; i++)
                        {
                            RadioButton c = (RadioButton)radiobutton_list[i];
                            if (c.Name.Equals(name))		// name found
                            {
                                c.Checked = bool.Parse(val);	// restore value
                                i = radiobutton_list.Count + 1;
                            }
                            if (i == radiobutton_list.Count)
                                MessageBox.Show("Control not found: " + name);
                        }
                    }
                    else if (s.StartsWith("txt"))
                    {	// look through each control to find the matching name
                        for (int i = 0; i < textbox_list.Count; i++)
                        {
                            TextBox c = (TextBox)textbox_list[i];
                            if (c.Name.Equals(name))		// name found
                            {
                                c.Text = val;	// restore value
                                i = textbox_list.Count + 1;
                            }
                            if (i == textbox_list.Count)
                                MessageBox.Show("Control not found: " + name);
                        }
                    }
                    else if (s.StartsWith("tb"))
                    {
                        // look through each control to find the matching name
                        for (int i = 0; i < trackbar_list.Count; i++)
                        {
                            TrackBar c = (TrackBar)trackbar_list[i];
                            if (c.Name.Equals(name))		// name found
                            {
                                c.Value = Int32.Parse(val);
                                i = trackbar_list.Count + 1;
                            }
                            if (i == trackbar_list.Count)
                                MessageBox.Show("Control not found: " + name);
                        }
                    }                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in GetOption function!\n" + ex.ToString());
            }
        }

        private void comboAudioSampleRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool pwr = MainForm.MRIsRunning;
            int old_rate = Audio.SampleRate;
            int new_rate = Int32.Parse(comboAudioSampleRate.Text);
            if (new_rate != old_rate)
            {
                if (pwr)
                    MainForm.MRIsRunning = false;
                Audio.SampleRate = new_rate;
                MainForm.MRIsRunning = pwr;
            }
        }

        private void udLatency_ValueChanged(object sender, EventArgs e)
        {
            bool pwr = MainForm.MRIsRunning;
            int old_latency = Audio.Latency;
            int new_latency = (int)udLatency.Value;
            if (new_latency != old_latency)
            {
                if (pwr)
                    MainForm.MRIsRunning = false;
                Audio.Latency = new_latency;
                MainForm.MRIsRunning = pwr;
            }
        }

        private void comboAudioBuffer_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool pwr = MainForm.MRIsRunning;
            int old_block_size = Audio.BlockSize;
            int new_block_size = Int32.Parse(comboAudioBuffer.Text);
            if (old_block_size != new_block_size)
            {
                if (pwr)
                    MainForm.MRIsRunning = false;
                Audio.BlockSize = new_block_size;
                MainForm.MRIsRunning = pwr;
            }
        }

        private void txtCALL_TextChanged(object sender, EventArgs e)
        {

        }
    }

    #region PADeviceInfo Helper Class

    public class PADeviceInfo
    {
        private string _Name;
        private int _Index;

        public string Name
        {
            get { return _Name; }
        }

        public int Index
        {
            get { return _Index; }
        }

        public PADeviceInfo(String argName, int argIndex)
        {
            _Name = argName;
            _Index = argIndex;
        }

        public override string ToString()
        {
            return _Name;
        }
    }

    #endregion
}
