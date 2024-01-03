using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace CWExpert
{
    unsafe class Audio
    {
        [DllImport("msvcrt.dll", EntryPoint = "memcpy")]
        public static extern void memcpy(void* destptr, void* srcptr, int n);

        #region variable

        unsafe private static PA19.PaStreamCallback callback = new PA19.PaStreamCallback(Callback);
        unsafe private static void* stream1;
        unsafe private static void* stream2;

        public static int callback_return = 0;
        public static CWExpert190519 MainForm = null;

        #endregion

        #region properties

        private static int host = 0;
        public static int Host
        {
            set { host = value; }
        }

        private static int block_size = 2048;
        public static int BlockSize
        {
            get { return block_size; }
            set { block_size = value; }
        }

        private static int sample_rate = 8000;
        public static int SampleRate
        {
            get { return sample_rate; }
            set { sample_rate = value; }
        }

        private static int input_dev = 0;
        public static int Input
        {
            set { input_dev = value; }
        }

        private static int output_dev = 0;
        public static int Output
        {
            set { output_dev = value; }
        }

        private static int latency = 20;
        public static int Latency
        {
            get { return latency; }
            set { latency = value; }
        }

        private static int num_channels = 2;
        public static int NumChannels
        {
            set { num_channels = value; }
        }

        #endregion

        #region misc function

        unsafe public static int Callback(void* input, void* output, int frameCount,
            PA19.PaStreamCallbackTimeInfo* timeInfo, int statusFlags, void* userData)
        {
            try
            {
                int* array_ptr = (int*)input;
                ushort* in_ptr_l = (ushort*)array_ptr[0];
                ushort* in_ptr_r = (ushort*)array_ptr[1];

                int* out_array_ptr = (int*)output;
                ushort* out_l_ptr1 = (ushort*)out_array_ptr[0];
                ushort* out_r_ptr1 = (ushort*)out_array_ptr[1];

                for (int i = 0; i < frameCount; i++)
                {
                    out_l_ptr1[0] = in_ptr_l[0];
                    out_r_ptr1[0] = in_ptr_r[0];
                    out_r_ptr1++;
                    out_l_ptr1++;
                    in_ptr_l++;
                    in_ptr_r++;
                }

                in_ptr_l = (ushort*)array_ptr[0];
                in_ptr_r = (ushort*)array_ptr[1];

                ushort[] buffer_l = new ushort[frameCount];
                ushort[] buffer_r = new ushort[frameCount];

                for (int i = 0; i < frameCount; i++)
                {
                    buffer_l[i] = in_ptr_l[0];
                    in_ptr_l++;

                    buffer_r[i] = in_ptr_r[0];
                    in_ptr_r++;
                }

                if (MainForm.cwDecoder.read_buffer_l != null &&
                    MainForm.cwDecoder.read_buffer_l.Length == frameCount)
                {
                    /*
                                        fixed (void* rptr_l = &buffer_l[0])
                                        fixed (void* wptr_l = &MainForm.cwDecoder.read_buffer_l[0])
                                        memcpy(wptr_l, rptr_l, frameCount*2);
                    */
                    Array.Copy(buffer_l, MainForm.cwDecoder.read_buffer_l, frameCount);
                }

                if (MainForm.cwDecoder.read_buffer_r != null &&
                    MainForm.cwDecoder.read_buffer_r.Length == frameCount)
                {
                    /*                    
                         fixed (void* rptr_r = &buffer_r[0])
                         fixed (void* wptr_r = &MainForm.cwDecoder.read_buffer_r[0])
                         memcpy(wptr_r, rptr_r, frameCount*2);
                    */
                    Array.Copy(buffer_r, MainForm.cwDecoder.read_buffer_r, frameCount);
                }

                MainForm.cwDecoder.AudioEvent.Set();

                return callback_return;

//                Debug.Write("* ");
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return 0;
            }
        }

        public static bool Start()
        {
            bool retval = false;
            try
            {
                retval = StartAudio(ref callback, (uint)block_size, sample_rate,
                    host, input_dev, output_dev, num_channels, 0, latency);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting audio stream!\n" + ex.ToString());
                return false;
            }
        }

        public unsafe static bool StartAudio(ref PA19.PaStreamCallback callback,
            uint block_size, double sample_rate, int host_api_index, int input_dev_index,
            int output_dev_index, int num_channels, int callback_num, int latency_ms)
        {
            try
            {
                int in_dev = PA19.PA_HostApiDeviceIndexToDeviceIndex(host_api_index, input_dev_index);
                int out_dev = PA19.PA_HostApiDeviceIndexToDeviceIndex(host_api_index, output_dev_index);

                PA19.PaStreamParameters inparam = new PA19.PaStreamParameters();
                PA19.PaStreamParameters outparam = new PA19.PaStreamParameters();

                inparam.device = in_dev;
                inparam.channelCount = num_channels;

                inparam.sampleFormat = PA19.paInt16 | PA19.paNonInterleaved;

                inparam.suggestedLatency = ((float)latency_ms / 1000);

                outparam.device = out_dev;
                outparam.channelCount = num_channels;

                outparam.sampleFormat = PA19.paInt16 | PA19.paNonInterleaved;
                outparam.suggestedLatency = ((float)latency_ms / 1000);

                int error = 0;
                if (callback_num == 0)
                    error = PA19.PA_OpenStream(out stream1, &inparam, &outparam, sample_rate, block_size, 0, callback, 0);
                else
                    error = PA19.PA_OpenStream(out stream2, &inparam, &outparam, sample_rate, block_size, 0, callback, 1);

                if (error != 0)
                {
                    MessageBox.Show(PA19.PA_GetErrorText(error), "PortAudio Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (callback_num == 0)
                    error = PA19.PA_StartStream(stream1);
                else
                    error = PA19.PA_StartStream(stream2);

                if (error != 0)
                {
                    MessageBox.Show(PA19.PA_GetErrorText(error), "PortAudio Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                //               Debug.WriteLine(block_size);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in StartAudio function!\n" + ex.ToString());
                return false;
            }
        }

        public unsafe static void StopAudio()
        {
            PA19.PA_AbortStream(stream1);
            PA19.PA_CloseStream(stream1);
            PA19.PA_AbortStream(stream2);
            PA19.PA_CloseStream(stream2);
        }

        public static ArrayList GetPAInputDevices(int hostIndex)
        {
            ArrayList a = new ArrayList();
            PA19.PaHostApiInfo hostInfo = PA19.PA_GetHostApiInfo(hostIndex);
            for (int i = 0; i < hostInfo.deviceCount; i++)
            {
                int devIndex = PA19.PA_HostApiDeviceIndexToDeviceIndex(hostIndex, i);
                PA19.PaDeviceInfo devInfo = PA19.PA_GetDeviceInfo(devIndex);
                if (devInfo.maxInputChannels > 0)
                    a.Add(new PADeviceInfo(devInfo.name, i)/* + " - " + devIndex*/);
            }
            return a;
        }

        public static ArrayList GetPAOutputDevices(int hostIndex)
        {
            ArrayList a = new ArrayList();
            PA19.PaHostApiInfo hostInfo = PA19.PA_GetHostApiInfo(hostIndex);
            for (int i = 0; i < hostInfo.deviceCount; i++)
            {
                int devIndex = PA19.PA_HostApiDeviceIndexToDeviceIndex(hostIndex, i);
                PA19.PaDeviceInfo devInfo = PA19.PA_GetDeviceInfo(devIndex);
                if (devInfo.maxOutputChannels > 0)
                    a.Add(new PADeviceInfo(devInfo.name, i)/* + " - " + devIndex*/);
            }
            return a;
        }

        public static ArrayList GetPAHosts() // returns a text list of driver types
        {
            ArrayList a = new ArrayList();
            for (int i = 0; i < PA19.PA_GetHostApiCount(); i++)
            {
                PA19.PaHostApiInfo info = PA19.PA_GetHostApiInfo(i);
                a.Add(info.name);
            }
            return a;
        }

        #endregion
    }
}
