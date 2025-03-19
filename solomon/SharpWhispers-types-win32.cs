// Author: Ryan Cobb (@cobbr_io)
// Project: SharpSploit (https://github.com/cobbr/SharpSploit)
// License: BSD 3-Clause

using System;
using System.Runtime.InteropServices;

namespace SharpWhispers.Data
{
    /// <summary>
    /// Win32 is a library of enums and structures for Win32 API functions.
    /// </summary>
    /// <remarks>
    /// A majority of this library is adapted from signatures found at www.pinvoke.net.
    /// </remarks>
    public static class Win32
    {
        public static class Kernel32
        {
            public const uint MEM_COMMIT = 0x1000;
            public const uint MEM_RESERVE = 0x2000;
            public const uint MEM_RELEASE = 0x8000;

            [Flags]
public enum ProcessAccessFlags : UInt32
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms684880%28v=vs.85%29.aspx?f=255&MSPPError=-2147217396
    PROCESS_ALL_ACCESS = 0x001F0FFF,
    PROCESS_CREATE_PROCESS = 0x0080,
    PROCESS_CREATE_THREAD = 0x0002,
    PROCESS_DUP_HANDLE = 0x0040,
    PROCESS_QUERY_INFORMATION = 0x0400,
    PROCESS_QUERY_LIMITED_INFORMATION = 0x1000,
    PROCESS_SET_INFORMATION = 0x0200,
    PROCESS_SET_QUOTA = 0x0100,
    PROCESS_SUSPEND_RESUME = 0x0800,
    PROCESS_TERMINATE = 0x0001,
    PROCESS_VM_OPERATION = 0x0008,
    PROCESS_VM_READ = 0x0010,
    PROCESS_VM_WRITE = 0x0020,
    SYNCHRONIZE = 0x00100000
}


        }

        public static class WinNT
        {
            public const uint PAGE_READONLY = 0x02;
            public const uint PAGE_READWRITE = 0x04;
            public const uint PAGE_EXECUTE = 0x10;
            public const uint PAGE_EXECUTE_READ = 0x20;
            public const uint PAGE_EXECUTE_READWRITE = 0x40;

            public const uint SEC_IMAGE = 0x1000000;
            
            
            [Flags]
public enum ACCESS_MASK : uint
{
    DELETE = 0x00010000,
    READ_CONTROL = 0x00020000,
    WRITE_DAC = 0x00040000,
    WRITE_OWNER = 0x00080000,
    SYNCHRONIZE = 0x00100000,
    STANDARD_RIGHTS_REQUIRED = 0x000F0000,
    STANDARD_RIGHTS_READ = 0x00020000,
    STANDARD_RIGHTS_WRITE = 0x00020000,
    STANDARD_RIGHTS_EXECUTE = 0x00020000,
    STANDARD_RIGHTS_ALL = 0x001F0000,
    SPECIFIC_RIGHTS_ALL = 0x0000FFF,
    ACCESS_SYSTEM_SECURITY = 0x01000000,
    MAXIMUM_ALLOWED = 0x02000000,
    GENERIC_READ = 0x80000000,
    GENERIC_WRITE = 0x40000000,
    GENERIC_EXECUTE = 0x20000000,
    GENERIC_ALL = 0x10000000,
    DESKTOP_READOBJECTS = 0x00000001,
    DESKTOP_CREATEWINDOW = 0x00000002,
    DESKTOP_CREATEMENU = 0x00000004,
    DESKTOP_HOOKCONTROL = 0x00000008,
    DESKTOP_JOURNALRECORD = 0x00000010,
    DESKTOP_JOURNALPLAYBACK = 0x00000020,
    DESKTOP_ENUMERATE = 0x00000040,
    DESKTOP_WRITEOBJECTS = 0x00000080,
    DESKTOP_SWITCHDESKTOP = 0x00000100,
    WINSTA_ENUMDESKTOPS = 0x00000001,
    WINSTA_READATTRIBUTES = 0x00000002,
    WINSTA_ACCESSCLIPBOARD = 0x00000004,
    WINSTA_CREATEDESKTOP = 0x00000008,
    WINSTA_WRITEATTRIBUTES = 0x00000010,
    WINSTA_ACCESSGLOBALATOMS = 0x00000020,
    WINSTA_EXITWINDOWS = 0x00000040,
    WINSTA_ENUMERATE = 0x00000100,
    WINSTA_READSCREEN = 0x00000200,
    WINSTA_ALL_ACCESS = 0x0000037F,

    SECTION_ALL_ACCESS = 0x10000000,
    SECTION_QUERY = 0x0001,
    SECTION_MAP_WRITE = 0x0002,
    SECTION_MAP_READ = 0x0004,
    SECTION_MAP_EXECUTE = 0x0008,
    SECTION_EXTEND_SIZE = 0x0010
}


        }
    }
}
