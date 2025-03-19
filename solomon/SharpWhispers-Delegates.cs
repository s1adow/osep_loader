// DInvoke delegates taken from https://github.com/TheWover/DInvoke/blob/15924897d9992ae90ec43aaf3b74915df3e4518b/DInvoke/DInvoke/DynamicInvoke/Native.cs
// Additional delegates generated with https://github.com/jaredpar/pinvoke-interop-assistant
using System;

// Delegates
using System.Runtime.InteropServices;

// Data types
using Data = SharpWhispers.Data;

namespace Delegates
{
    class SyscallDelegates
    {
		/* Delegates */

	public struct Delegate
	{

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
public delegate UInt32 NtOpenProcess(
	ref IntPtr ProcessHandle,
	Data.Win32.Kernel32.ProcessAccessFlags DesiredAccess,
	ref Data.Native.OBJECT_ATTRIBUTES ObjectAttributes,
	ref Data.Native.CLIENT_ID ClientId);

[UnmanagedFunctionPointer(CallingConvention.StdCall)]
public delegate UInt32 NtAllocateVirtualMemory(
	IntPtr ProcessHandle,
	ref IntPtr BaseAddress,
	IntPtr ZeroBits,
	ref IntPtr RegionSize,
	UInt32 AllocationType,
	UInt32 Protect);

[UnmanagedFunctionPointer(CallingConvention.StdCall)]
public delegate UInt32 NtWriteVirtualMemory(
	IntPtr ProcessHandle,
	IntPtr BaseAddress,
	IntPtr Buffer,
	UInt32 BufferLength,
	ref UInt32 BytesWritten);

[UnmanagedFunctionPointer(CallingConvention.StdCall)]
public delegate UInt32 NtProtectVirtualMemory(
	IntPtr ProcessHandle,
	ref IntPtr BaseAddress,
	ref IntPtr RegionSize,
	UInt32 NewProtect,
	ref UInt32 OldProtect);

[UnmanagedFunctionPointer(CallingConvention.StdCall)]
public delegate Data.Native.NTSTATUS NtCreateThreadEx(
	out IntPtr threadHandle,
	Data.Win32.WinNT.ACCESS_MASK desiredAccess,
	IntPtr objectAttributes,
	IntPtr processHandle,
	IntPtr startAddress,
	IntPtr parameter,
	bool createSuspended,
	int stackZeroBits,
	int sizeOfStack,
	int maximumStackSize,
	IntPtr attributeList);


            
        }
    }
}
