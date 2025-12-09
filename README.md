# SO2MR - CW Expert for SO2R with Morse Runner

CW robot for two 9A5K Morse Runner instances.

## System Requirements

### Operating System
- Windows 7 or later
- Windows 10/11 (including ARM64 with x86 emulation)

### Audio Requirements
- **PA19.dll** (PortAudio library) - **MUST** be 32-bit (x86) version
- The application is compiled as a 32-bit (x86) executable
- Working audio drivers for input and output devices

### .NET Framework
- .NET Framework 4.0 or later

## Building the Application

The project uses Visual Studio and MSBuild. To build:

```bash
msbuild CWExpert190519.sln /p:Configuration=Release /p:Platform="Any CPU"
```

Note: The project is configured to target x86 (32-bit) architecture in both Debug and Release configurations.

## PA19.dll and PortAudio

### Architecture Compatibility

**CRITICAL**: The PA19.dll file **MUST** match the process architecture:
- This application is built as 32-bit (x86)
- You **MUST** use a 32-bit PA19.dll
- A 64-bit PA19.dll will cause initialization failures

### File Location
- PA19.dll must be in the same directory as CWExpert.exe
- The application will check for the file at startup

## Troubleshooting

### PA19 Initialization Failures

If you encounter "PA19 initialization failed" errors, check the following:

#### 1. DLL Architecture Mismatch (Most Common)
**Error**: `BadImageFormatException` or Error Code 193

**Cause**: PA19.dll is 64-bit but the application is 32-bit (or vice versa)

**Solution**:
- Verify you have the 32-bit (x86) version of PA19.dll
- Use a PE viewer tool or `dumpbin /headers PA19.dll` to check the DLL architecture
- Look for "machine (x86)" or "machine (x64)" in the output

#### 2. Missing DLL
**Error**: `DllNotFoundException`

**Cause**: PA19.dll is not found in the application directory

**Solution**:
- Ensure PA19.dll is in the same directory as CWExpert.exe
- Check that the file is not blocked (right-click → Properties → Unblock)

#### 3. Missing Dependencies
**Error**: Error Code 126 (ERROR_MOD_NOT_FOUND)

**Cause**: PA19.dll has dependencies that are not installed

**Solution**:
- Install Visual C++ Redistributables (both x86 and x64 versions)
- Check if PA19.dll has other DLL dependencies using Dependency Walker

#### 4. Windows ARM64 Compatibility
**Platform**: Windows 11 ARM64

**Status**: The application should work under x86 emulation

**Requirements**:
- Ensure x86 emulation is enabled (it is by default on Windows 11 ARM64)
- Use 32-bit (x86) PA19.dll
- Some audio devices may have limited driver support on ARM64

#### 5. Audio Driver Issues
**Error**: Various PortAudio errors during initialization

**Cause**: Audio drivers are not working properly

**Solution**:
- Update audio drivers to the latest version
- Test audio playback/recording with Windows Sound Recorder
- Try different audio APIs (MME, DirectSound, WASAPI) in the Setup dialog
- Check Windows audio settings and ensure devices are not disabled

### Diagnostic Information

The application now provides comprehensive diagnostic information on failure:

1. **Runtime Information**:
   - Operating system version
   - Process architecture (32-bit vs 64-bit)
   - .NET CLR version

2. **DLL Loading**:
   - PA19.dll location and existence
   - DLL load errors with specific error codes
   - Architecture verification

3. **PortAudio Information**:
   - PortAudio version
   - Available host APIs (MME, DirectSound, WASAPI, etc.)
   - Available audio devices
   - Device capabilities

### Debug Logging

The application writes diagnostic information to the debug output. To view:

1. Run the application from Visual Studio (with debugger)
2. Check the Output window → Debug category
3. Or use DebugView (sysinternals) to capture debug output

## VS2022/2025/2026 Compatibility

### Visual Studio 2022
- Fully supported
- Use "Any CPU" platform with x86 target

### Visual Studio 2025/2026
- Should work with proper x86 configuration
- May require SDK updates
- Test thoroughly on target systems

### ARM64 Development Systems
- Development on ARM64 Windows is supported
- The application will be built as x86 (32-bit)
- Ensure x86 build tools are installed in Visual Studio

## Known Issues

1. **AudioMR.cs Compatibility**: The AudioMR.cs file references `CWExpert120817` which may be from an older version. The main application uses `CWExpert190519`.

2. **PA19 WASAPI Support**: Some versions of PA19.dll may not have proper WASAPI support. Use MME or DirectSound if WASAPI causes issues.

3. **Morse Runner Integration**: Requires Morse Runner application to be running and properly configured.

## Support and Reporting Issues

When reporting issues, please include:
1. The full error message from the diagnostic dialog
2. Your Windows version and architecture (x86, x64, ARM64)
3. The PA19.dll file size and date
4. Output from: `dumpbin /headers PA19.dll` (if available)

## License

This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.

Copyright (C) 2011 S56A YT7PWR
 
