using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace OS_LAB3
{
    public class Tool32Snap
    {

        public static IEnumerable<PROCESSENTRY32> GetProcessList()
        {
            IntPtr handleToSnapshot = IntPtr.Zero;
            try
            {
                PROCESSENTRY32 procEntry = new PROCESSENTRY32();
                procEntry.dwSize = (UInt32)Marshal.SizeOf(typeof(PROCESSENTRY32));

                handleToSnapshot = CreateToolhelp32Snapshot((uint)SnapshotFlags.All, 0); // снимок системы в заданный момент времени 

                if (Process32First(handleToSnapshot, ref procEntry)) // имея дескриптор снимка, можно последовательно посмотреть сведения о в
                {
                    do
                    {
                        yield return procEntry;
                    } while (Process32Next(handleToSnapshot, ref procEntry)); // следующий процесс
                }
                else
                {
                    throw new ApplicationException(string.Format("Failed with win32 error code {0}", Marshal.GetLastWin32Error()));
                }
            }
            finally
            {
                CloseHandle(handleToSnapshot); // освобождение ресурсов
            }
        }
        public static bool GetTreadPriority(uint pid, int cntTh, int threadPriority)
        {
            bool amountThreadsPrior = false;

            IntPtr hSnapshot = CreateToolhelp32Snapshot((uint)(SnapshotFlags.Thread), pid);

            if (hSnapshot != INVALID_HANDLE_VALUE)
            {
                try
                {
                    THREADENTRY32 te = new THREADENTRY32();
                    te.dwSize = (uint)Marshal.SizeOf(te);
                    var bRes = Thread32First(hSnapshot, ref te);

                    if (bRes)
                    {
                        do
                        {
                            var curPri = te.tpDeltaPri + te.tpBasePri;
                            if (threadPriority == curPri)
                                amountThreadsPrior = true;
                            cntTh--;

                        } while (Thread32Next(hSnapshot, ref te) && cntTh != 0 && !amountThreadsPrior);
                    }
                }
                finally
                {
                    CloseHandle(hSnapshot);
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("error=" + Marshal.GetLastWin32Error());
            }
            return amountThreadsPrior;
        }

        public static List<int> GetTreadPriorityList(uint pid, int cntTh)
        {
            List<int> thPriListForProc = new List<int>();

            IntPtr hSnapshot = CreateToolhelp32Snapshot((uint)(SnapshotFlags.Thread | SnapshotFlags.NoHeaps), pid);

            if (hSnapshot != INVALID_HANDLE_VALUE)
            {
                try
                {
                    THREADENTRY32 te = new THREADENTRY32();
                    te.dwSize = (uint)Marshal.SizeOf(te);
                    var bRes = Thread32First(hSnapshot, ref te);

                    if (bRes)
                    {
                        do
                        {
                            var curPri = te.tpDeltaPri + te.tpBasePri;
                            thPriListForProc.Add(curPri);
                            cntTh--;

                        } while (Thread32Next(hSnapshot, ref te) && cntTh != 0);
                    }
                }
                finally
                {
                    CloseHandle(hSnapshot);
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("error=" + Marshal.GetLastWin32Error());
            }
            return thPriListForProc;
        }


        #region Win32 Tool snapshot structures

        [Flags]
        internal enum SnapshotFlags : uint
        {
            HeapList = 0x00000001,
            Process = 0x00000002,
            Thread = 0x00000004,
            Module = 0x00000008,
            Module32 = 0x00000010,
            Inherit = 0x80000000,
            All = 0x0000001F,
            NoHeaps = 0x40000000
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct PROCESSENTRY32
        {
            public uint dwSize;
            public uint cntUsage;
            public uint th32ProcessID;
            public IntPtr th32DefaultHeapID;
            public uint th32ModuleID;
            public uint cntThreads;
            public uint th32ParentProcessID;
            public int pcPriClassBase;
            public uint dwFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szExeFile;
        }
        public struct THREADENTRY32
        {
            public uint dwSize;
            public uint cntUsage;
            public uint th32ThreadID;
            public uint th32OwnerProcessID;
            public int tpBasePri;
            public int tpDeltaPri;
            public uint dwFlags;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct HEAPENTRY32
        {
            public uint dwSize;
            public IntPtr hHandle;
            public uint dwAdress;
            public uint dwBlockSize;
            public uint dwFlags;
            public uint dwLockCount;
            public uint dwResvd;
            public uint th32ProcessID;
            public uint th32HeapID;
        }
        private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        #endregion

    #region Win32 Tool snapshot functions

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        static extern IntPtr CreateToolhelp32Snapshot([In] UInt32 dwFlags, [In] UInt32 th32ProcessID);

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        // имея дескриптор снимка, можно последовательно посмотреть сведения о всех процессах
        static extern bool Process32First([In] IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        // след процесс
        static extern bool Process32Next([In] IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

        [DllImport("kernel32", SetLastError = true)]
        private static extern bool CloseHandle([In] IntPtr hObject);
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool Thread32First(IntPtr hSnapshot, ref THREADENTRY32 lpte);

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool Thread32Next(IntPtr hSnapshot, ref THREADENTRY32 lpte);
        #endregion
    }
}