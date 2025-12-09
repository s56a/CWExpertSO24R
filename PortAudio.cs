using System;
using System.Collections;
using System.Text;
using System.Security;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

using PaError = System.Int32;
using PaDeviceIndex = System.Int32;
using PaHostApiIndex = System.Int32;
using PaTime = System.Double;
using PaSampleFormat = System.UInt32;
using PaStreamFlags = System.UInt32;
using PaStreamCallbackFlags = System.UInt32;

namespace CWExpert
{
    public class PA19
    {
        #region Diagnostics
        
        private static bool _isInitialized = false;
        private static string _lastInitializationError = null;
        
        /// <summary>
        /// Flag indicating whether PA19 has been successfully initialized
        /// </summary>
        public static bool IsInitialized 
        { 
            get { return _isInitialized; }
            private set { _isInitialized = value; }
        }
        
        /// <summary>
        /// Stores the last initialization error if initialization failed
        /// </summary>
        public static string LastInitializationError 
        { 
            get { return _lastInitializationError; }
            private set { _lastInitializationError = value; }
        }
        
        /// <summary>
        /// Gets diagnostic information about the runtime environment
        /// </summary>
        public static string GetRuntimeDiagnostics()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== PA19 Runtime Diagnostics ===");
            sb.AppendLine(string.Format("OS Version: {0}", Environment.OSVersion));
            sb.AppendLine(string.Format("CLR Version: {0}", Environment.Version));
            sb.AppendLine(string.Format("Is 64-bit OS: {0}", Environment.Is64BitOperatingSystem));
            sb.AppendLine(string.Format("Is 64-bit Process: {0}", Environment.Is64BitProcess));
            sb.AppendLine(string.Format("Processor Count: {0}", Environment.ProcessorCount));
            
            // Note: RuntimeInformation is not available in .NET Framework 4.0
            // Processor architecture can be inferred from Environment.Is64BitProcess
            sb.AppendLine(string.Format("Process Architecture: {0}", Environment.Is64BitProcess ? "x64" : "x86"));
            
            // Check for PA19.dll
            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            string dllPath = Path.Combine(exePath, "PA19.dll");
            sb.AppendLine(string.Format("Application Path: {0}", exePath));
            sb.AppendLine(string.Format("PA19.dll Path: {0}", dllPath));
            sb.AppendLine(string.Format("PA19.dll Exists: {0}", File.Exists(dllPath)));
            
            if (File.Exists(dllPath))
            {
                FileInfo fi = new FileInfo(dllPath);
                sb.AppendLine(string.Format("PA19.dll Size: {0} bytes", fi.Length));
                sb.AppendLine(string.Format("PA19.dll Modified: {0}", fi.LastWriteTime));
            }
            
            return sb.ToString();
        }
        
        /// <summary>
        /// Attempts to load PA19.dll and verify it's compatible with current process architecture
        /// </summary>
        public static bool VerifyDllArchitecture(out string message)
        {
            try
            {
                // Attempt to load the library
                IntPtr hModule = LoadLibrary("PA19.dll");
                if (hModule == IntPtr.Zero)
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    message = string.Format("Failed to load PA19.dll. Error code: {0} (0x{1:X})\n", errorCode, errorCode);
                    
                    if (errorCode == 193) // ERROR_BAD_EXE_FORMAT
                    {
                        message += "This error typically means the DLL architecture doesn't match the process architecture.\n";
                        message += string.Format("Current process is {0}.\n", Environment.Is64BitProcess ? "64-bit" : "32-bit");
                        message += "PA19.dll must be a 32-bit DLL for 32-bit processes or 64-bit DLL for 64-bit processes.";
                    }
                    else if (errorCode == 126) // ERROR_MOD_NOT_FOUND
                    {
                        message += "The DLL or one of its dependencies could not be found.\n";
                        message += "Ensure PA19.dll and all required dependencies (like PortAudio runtime) are in the application directory.";
                    }
                    
                    return false;
                }
                
                FreeLibrary(hModule);
                message = string.Format("PA19.dll loaded successfully. Process is {0}.", Environment.Is64BitProcess ? "64-bit" : "32-bit");
                return true;
            }
            catch (Exception ex)
            {
                message = string.Format("Exception while verifying DLL: {0}", ex.Message);
                return false;
            }
        }
        
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LoadLibrary(string lpFileName);
        
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FreeLibrary(IntPtr hModule);
        
        #endregion
        #region Constants

        public const PaDeviceIndex paNoDevice = (PaDeviceIndex)(-1);
        public const PaDeviceIndex paUseHostApiSpecificDeviceSpecification = (PaDeviceIndex)(-2);
        public const PaSampleFormat paFloat32 = (PaSampleFormat)0x01;
        public const PaSampleFormat paInt32 = (PaSampleFormat)0x02;
        public const PaSampleFormat paInt24 = (PaSampleFormat)0x04;
        public const PaSampleFormat paInt16 = (PaSampleFormat)0x08;
        public const PaSampleFormat paInt8 = (PaSampleFormat)0x10;
        public const PaSampleFormat paUInt8 = (PaSampleFormat)0x20;
        public const PaSampleFormat paCustomFormat = (PaSampleFormat)0x10000;
        public const PaSampleFormat paNonInterleaved = (PaSampleFormat)0x80000000;
        public const uint paFormatIsSupported = 0;
        public const uint paFramesPerBufferUnspecified = 0;
        public const PaStreamFlags paNoFlag = (PaStreamFlags)0;
        public const PaStreamFlags paClipOff = (PaStreamFlags)0x01;
        public const PaStreamFlags paDitherOff = (PaStreamFlags)0x02;
        public const PaStreamFlags paNeverDropInput = (PaStreamFlags)0x04;
        public const PaStreamFlags paPrimeOutputBuffersUsingStreamCallback = (PaStreamFlags)0x08;
        public const PaStreamFlags paPlatformSpecificFlags = (PaStreamFlags)0xFFFF0000;
        public const PaStreamCallbackFlags paInputUnderflow = (PaStreamCallbackFlags)0x01;
        public const PaStreamCallbackFlags paInputOverflow = (PaStreamCallbackFlags)0x02;
        public const PaStreamCallbackFlags paOutputUnderflow = (PaStreamCallbackFlags)0x04;
        public const PaStreamCallbackFlags paOutputOverflow = (PaStreamCallbackFlags)0x08;
        public const PaStreamCallbackFlags paPrimingOutput = (PaStreamCallbackFlags)0x10;

        #endregion

        #region Enums

        public enum PaErrorCode
        {
            paNoError = 0, paNotInitialized = -10000, paUnanticipatedHostError, paInvalidChannelCount,
            paInvalidSampleRate, paInvalidDevice, paInvalidFlag, paSampleFormatNotSupported,
            paBadIODeviceCombination, paInsufficientMemory, paBufferTooBig, paBufferTooSmall,
            paNullCallback, paBadStreamPtr, paTimedOut, paInternalError,
            paDeviceUnavailable, paIncompatibleHostApiSpecificStreamInfo, paStreamIsStopped, paStreamIsNotStopped,
            paInputOverflowed, paOutputUnderflowed, paHostApiNotFound, paInvalidHostApi,
            paCanNotReadFromACallbackStream, paCanNotWriteToACallbackStream, paCanNotReadFromAnOutputOnlyStream, paCanNotWriteToAnInputOnlyStream,
            paIncompatibleStreamHostApi
        }

        public enum PaHostApiTypeId
        {
            paInDevelopment = 0, paDirectSound = 1, paMME = 2, paASIO = 3,
            paSoundManager = 4, paCoreAudio = 5, paOSS = 7, paALSA = 8,
            paAL = 9, paBeOS = 10
        }

        public enum PaStreamCallbackResult
        { paContinue = 0, paComplete = 1, paAbort = 2 }

        #endregion

        #region Structs

        [StructLayout(LayoutKind.Sequential)]
        public struct PaHostApiInfo
        {
            public int structVersion;
            public int type;
            [MarshalAs(UnmanagedType.LPStr)]
            public string name;
            public int deviceCount;
            public PaDeviceIndex defaultInputDevice;
            public PaDeviceIndex defaultOutputDevice;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PaHostErrorInfo
        {
            public PaHostApiTypeId hostApiType;
            public int errorCode;
            [MarshalAs(UnmanagedType.LPStr)]
            public string errorText;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PaDeviceInfo
        {
            public int structVersion;
            [MarshalAs(UnmanagedType.LPStr)]
            public string name;
            public PaHostApiIndex hostApi;
            public int maxInputChannels;
            public int maxOutputChannels;
            public PaTime defaultLowInputLatency;
            public PaTime defaultLowOutputLatency;
            public PaTime defaultHighInputLatency;
            public PaTime defaultHighOutputLatency;
            public double defaultSampleRate;
        }

        [StructLayout(LayoutKind.Sequential)]
        unsafe public struct PaStreamParameters
        {
            public PaDeviceIndex device;
            public int channelCount;
            public PaSampleFormat sampleFormat;
            public PaTime suggestedLatency;
            public void* hostApiSpecificStreamInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PaStreamCallbackTimeInfo
        {
            public PaTime inputBufferAdcTime;
            public PaTime currentTime;
            public PaTime outputBufferDacTime;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PaStreamInfo
        {
            public int structVersion;
            public PaTime inputLatency;
            public PaTime outputLatency;
            public double sampleRate;
        }

        #endregion

        #region Function Definitions

        [DllImport("PA19.dll")]
        public static extern int PA_GetVersion();

        [DllImport("PA19.dll")]
        public static extern String PA_GetVersionText();

        [DllImport("PA19.dll")]
        public static extern String PA_GetErrorText(PaError errorCode);

        [DllImport("PA19.dll")]
        public static extern PaError PA_Initialize();

        [DllImport("PA19.dll")]
        public static extern PaError PA_Terminate();
        
        /// <summary>
        /// Initializes PortAudio with comprehensive error handling and diagnostics
        /// </summary>
        /// <param name="showDiagnostics">Whether to show diagnostic information on failure</param>
        /// <returns>True if initialization succeeded, false otherwise</returns>
        public static bool InitializeWithDiagnostics(bool showDiagnostics = true)
        {
            if (IsInitialized)
            {
                Debug.WriteLine("PA19 already initialized");
                return true;
            }
            
            LastInitializationError = null;
            
            try
            {
                // First, verify the DLL can be loaded and is the correct architecture
                string dllVerifyMessage;
                if (!VerifyDllArchitecture(out dllVerifyMessage))
                {
                    LastInitializationError = "DLL Architecture Verification Failed:\n" + dllVerifyMessage;
                    
                    if (showDiagnostics)
                    {
                        string fullDiagnostics = LastInitializationError + "\n\n" + GetRuntimeDiagnostics();
                        MessageBox.Show(fullDiagnostics, "PA19 DLL Load Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                        // Log to debug output
                        Debug.WriteLine(fullDiagnostics);
                    }
                    
                    return false;
                }
                
                Debug.WriteLine("PA19.dll loaded successfully, attempting initialization...");
                Debug.WriteLine(dllVerifyMessage);
                
                // Try to get version info before initializing (this tests if DLL functions are callable)
                try
                {
                    int version = PA_GetVersion();
                    string versionText = PA_GetVersionText();
                    Debug.WriteLine(string.Format("PortAudio version: {0} ({1})", version, versionText));
                }
                catch (Exception ex)
                {
                    LastInitializationError = string.Format("Failed to call PA19 version functions: {0}", ex.Message);
                    if (showDiagnostics)
                    {
                        MessageBox.Show(LastInitializationError + "\n\nThis may indicate PA19.dll is corrupt or incompatible.",
                            "PA19 Function Call Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return false;
                }
                
                // Now initialize
                PaError error = PA_Initialize();
                
                if (error != 0)
                {
                    string errorText = PA_GetErrorText(error);
                    LastInitializationError = string.Format("PA_Initialize failed with error {0}: {1}", error, errorText);
                    
                    // Get host error info if available
                    try
                    {
                        PaHostErrorInfo hostError = PA_GetLastHostErrorInfo();
                        if (hostError.errorCode != 0)
                        {
                            LastInitializationError += string.Format("\nHost API Error: {0}, Code: {1}, Message: {2}",
                                hostError.hostApiType, hostError.errorCode, hostError.errorText);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(string.Format("Could not retrieve host error info: {0}", ex.Message));
                    }
                    
                    if (showDiagnostics)
                    {
                        string fullMessage = LastInitializationError + "\n\n" + GetRuntimeDiagnostics();
                        MessageBox.Show(fullMessage, "PA19 Initialization Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Debug.WriteLine(fullMessage);
                    }
                    
                    return false;
                }
                
                IsInitialized = true;
                Debug.WriteLine("PA19 initialized successfully");
                
                // Log available hosts and devices
                try
                {
                    int hostCount = PA_GetHostApiCount();
                    Debug.WriteLine(string.Format("Available host APIs: {0}", hostCount));
                    for (int i = 0; i < hostCount; i++)
                    {
                        PaHostApiInfo hostInfo = PA_GetHostApiInfo(i);
                        Debug.WriteLine(string.Format("  Host {0}: {1} ({2} devices)", i, hostInfo.name, hostInfo.deviceCount));
                    }
                    
                    int deviceCount = PA_GetDeviceCount();
                    Debug.WriteLine(string.Format("Total devices: {0}", deviceCount));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(string.Format("Could not enumerate devices: {0}", ex.Message));
                }
                
                return true;
            }
            catch (DllNotFoundException ex)
            {
                LastInitializationError = string.Format("PA19.dll not found: {0}\n\n", ex.Message) +
                    "Ensure PA19.dll is in the same directory as the application executable.";
                
                if (showDiagnostics)
                {
                    MessageBox.Show(LastInitializationError + "\n\n" + GetRuntimeDiagnostics(),
                        "PA19 DLL Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                return false;
            }
            catch (BadImageFormatException ex)
            {
                LastInitializationError = string.Format("PA19.dll architecture mismatch: {0}\n\n", ex.Message) +
                    string.Format("The DLL is not compatible with this {0} process.\n", Environment.Is64BitProcess ? "64-bit" : "32-bit") +
                    "You need a 32-bit PA19.dll for 32-bit applications or a 64-bit version for 64-bit applications.";
                
                if (showDiagnostics)
                {
                    MessageBox.Show(LastInitializationError + "\n\n" + GetRuntimeDiagnostics(),
                        "PA19 Architecture Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                return false;
            }
            catch (Exception ex)
            {
                LastInitializationError = string.Format("Unexpected error during PA19 initialization: {0}: {1}\n{2}", 
                    ex.GetType().Name, ex.Message, ex.StackTrace);
                
                if (showDiagnostics)
                {
                    MessageBox.Show(LastInitializationError + "\n\n" + GetRuntimeDiagnostics(),
                        "PA19 Initialization Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                return false;
            }
        }
        
        /// <summary>
        /// Terminates PortAudio if it was initialized
        /// </summary>
        public static void SafeTerminate()
        {
            if (!IsInitialized)
            {
                return;
            }
            
            try
            {
                PaError error = PA_Terminate();
                if (error != 0)
                {
                    Debug.WriteLine(string.Format("PA_Terminate returned error {0}: {1}", error, PA_GetErrorText(error)));
                }
                else
                {
                    Debug.WriteLine("PA19 terminated successfully");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Error terminating PA19: {0}", ex.Message));
            }
            finally
            {
                IsInitialized = false;
            }
        }

        [DllImport("PA19.dll")]
        public static extern PaHostApiIndex PA_GetHostApiCount();

        [DllImport("PA19.dll")]
        public static extern PaHostApiIndex PA_GetDefaultHostApi();

        [DllImport("PA19.dll", EntryPoint = "PA_GetHostApiInfo")]
        public static extern IntPtr PA_GetHostApiInfoPtr(int hostId);
        public static PaHostApiInfo PA_GetHostApiInfo(int hostId)
        {
            IntPtr ptr = PA_GetHostApiInfoPtr(hostId);
            PaHostApiInfo info = (PaHostApiInfo)Marshal.PtrToStructure(ptr, typeof(PaHostApiInfo));
            return info;
        }

        [DllImport("PA19.dll")]
        public static extern PaHostApiIndex PA_HostApiTypeIdToHostApiIndex(PaHostApiTypeId type);

        [DllImport("PA19.dll")]
        public static extern PaDeviceIndex PA_HostApiDeviceIndexToDeviceIndex(int hostAPI, int hostApiDeviceIndex);

        [DllImport("PA19.dll", EntryPoint = "PA_GetLastHostErrorInfo")]
        public static extern IntPtr PA_GetLastHostErrorInfoPtr();
        public static PaHostErrorInfo PA_GetLastHostErrorInfo()
        {
            IntPtr ptr = PA_GetLastHostErrorInfoPtr();
            PaHostErrorInfo info = (PaHostErrorInfo)Marshal.PtrToStructure(ptr, typeof(PaHostErrorInfo));
            return info;
        }

        [DllImport("PA19.dll")]
        public static extern PaDeviceIndex PA_GetDeviceCount();

        [DllImport("PA19.dll")]
        public static extern PaDeviceIndex PA_GetDefaultInputDevice();

        [DllImport("PA19.dll")]
        public static extern PaDeviceIndex PA_GetDefaultOutputDevice();

        [DllImport("PA19.dll", EntryPoint = "PA_GetDeviceInfo")]
        public static extern IntPtr PA_GetDeviceInfoPtr(int device);
        public static PaDeviceInfo PA_GetDeviceInfo(int device)
        {
            IntPtr ptr = PA_GetDeviceInfoPtr(device);
            PaDeviceInfo info = (PaDeviceInfo)Marshal.PtrToStructure(ptr, typeof(PaDeviceInfo));
            return info;
        }

        [DllImport("PA19.dll")]
        unsafe public static extern PaError PA_IsFormatSupported(
            PaStreamParameters* inputParameters,
            PaStreamParameters* outputParameters,
            double sampleRate);

        [DllImport("PA19.dll")]
        unsafe public static extern PaError PA_OpenStream(
            out void* stream,
            PaStreamParameters* inputParameters,
            PaStreamParameters* outputParameters,
            double sampleRate,
            uint framesPerBuffer,
            PaStreamFlags streamFlags,
            PaStreamCallback streamCallback,
            int callback_id);

        [DllImport("PA19.dll")]
        unsafe public static extern PaError PA_OpenDefaultStream(
            out void* stream,
            int numInputChannels,
            int numOutputChannels,
            PaSampleFormat sampleFormat,
            double sampleRate,
            uint framesPerBuffer,
            PaStreamCallback streamCallback,
            int callback_id);

        [DllImport("PA19.dll")]
        unsafe public static extern PaError PA_CloseStream(void* stream);

        [DllImport("PA19.dll")]
        unsafe public static extern PaError PA_SetStreamFinishedCallback(
            void* stream, PaStreamFinishedCallback streamFinishedCallback);

        [DllImport("PA19.dll")]
        unsafe public static extern PaError PA_StartStream(void* stream);

        [DllImport("PA19.dll")]
        unsafe public static extern PaError PA_StopStream(void* stream);

        [DllImport("PA19.dll")]
        unsafe public static extern PaError PA_AbortStream(void* stream);

        [DllImport("PA19.dll")]
        unsafe public static extern PaError PA_IsStreamStopped(void* stream);

        [DllImport("PA19.dll")]
        unsafe public static extern PaError PA_IsStreamActive(void* stream);

        [DllImport("PA19.dll", EntryPoint = "PA_GetStreamInfo")]
        unsafe public static extern IntPtr PA_GetStreamInfoPtr(void* stream);
        unsafe public static PaStreamInfo PA_GetStreamInfo(void* stream)
        {
            IntPtr ptr = PA_GetStreamInfoPtr(stream);
            PaStreamInfo info = (PaStreamInfo)Marshal.PtrToStructure(ptr, typeof(PaStreamInfo));
            return info;
        }

        [DllImport("PA19.dll")]
        unsafe public static extern PaTime PA_GetStreamTime(void* stream);

        [DllImport("PA19.dll")]
        unsafe public static extern double PA_GetStreamCpuLoad(void* stream);

        [DllImport("PA19.dll")]
        unsafe public static extern PaError PA_ReadStream(void* stream, void* buffer, uint frames);

        [DllImport("PA19.dll")]
        unsafe public static extern PaError PA_WriteStream(void* stream, void* buffer, uint frames);

        [DllImport("PA19.dll")]
        unsafe public static extern int PA_GetStreamReadAvailable(void* stream);

        [DllImport("PA19.dll")]
        unsafe public static extern int PA_GetStreamWriteAvailable(void* stream);

        [DllImport("PA19.dll")]
        public static extern PaError PA_GetSampleSize(PaSampleFormat format);

        [DllImport("PA19.dll")]
        public static extern void PA_Sleep(int msec);

        unsafe public delegate int PaStreamCallback(void* input, void* output, int frameCount,
            PaStreamCallbackTimeInfo* timeInfo, int statusFlags, void* userData);

        unsafe public delegate void PaStreamFinishedCallback(void* userData);

        #endregion
    }
}
