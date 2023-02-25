using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3
{
    internal class Win32Snapshot
    {
        private readonly Action<string> printNextProcess;
        private readonly Action<string> printNextAnswerProcesss;
        private readonly Action<string> printSearchState;

        private List<string> maxFreeSpaceSizeProcesses = new List<string>();
        uint maxFreeSpaceSize = 0;

        public Win32Snapshot(Action<String> printNextProcess, Action<String> printNextAnswerProcesss, Action<string> printSearchState)
        {
            this.printNextProcess = printNextProcess;
            this.printNextAnswerProcesss = printNextAnswerProcesss;
            this.printSearchState = printSearchState;
        }

        public ApplicationException BuildError(string message)
        {
            return new ApplicationException(message + " " + Marshal.GetLastWin32Error().ToString());
        }

        public void StartSearch(int maxProcessesCount, int maxHeapsCount, int maxBlocksCount)
        {
            printSearchState("Идёт поиск");

            IntPtr hSnashot = CreateToolhelp32Snapshot((uint)SnapshotFlags.Process, 0);

            if (hSnashot == INVALID_HANDLE_VALUE)
                throw BuildError("Determine process error");

            PROCESSENTRY32 pcEntry = new PROCESSENTRY32();
            pcEntry.dwSize = (UInt32)Marshal.SizeOf(typeof(PROCESSENTRY32));

            try
            {
                if (!Process32First(hSnashot, ref pcEntry))
                    throw BuildError("Get process error");

                int processesCount = 0;

                do
                {
                    try
                    {
                        uint freeSpaceSize = GetFreeSpaceSizeForProcess(pcEntry.th32ProcessID, maxHeapsCount, maxBlocksCount);
                        printNextProcess($"{pcEntry.szExeFile} : {freeSpaceSize} байт");
                        UpdateAnswer(pcEntry.szExeFile, freeSpaceSize);

                    }
                    catch (Exception e)
                    {
                        printNextProcess($"{pcEntry.szExeFile} : Error code = {e.Message}");
                    }
                    processesCount++;
                } while (processesCount < maxProcessesCount && Process32Next(hSnashot, ref pcEntry));
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                CloseHandle(hSnashot);
            }

            printSearchState("Подсчёт ответа");
            PrintAnswer();
            printSearchState("Поиск завершён");
        }

        private uint GetFreeSpaceSizeForProcess(uint ProcessID, int maxHeapsCount, int maxBlocksCount)
        {
            IntPtr hSnapshot = CreateToolhelp32Snapshot((uint)SnapshotFlags.HeapList, ProcessID);
            if (hSnapshot == INVALID_HANDLE_VALUE)
            {
                throw BuildError("");
            }

            HEAPLIST32 hpl = new HEAPLIST32();
            hpl.dwSize = (UInt32)Marshal.SizeOf(typeof(HEAPLIST32));
            HEAPENTRY32 hpe = new HEAPENTRY32();
            hpe.dwSize = (UInt32)Marshal.SizeOf(typeof(HEAPENTRY32));

            uint freeSpaceSize = 0;

            try
            {
                if (!Heap32ListFirst(hSnapshot, ref hpl))
                {
                    throw BuildError("");
                }

                int heapsCount = 0;
                do
                {
                    if (!Heap32First(ref hpe, hpl.th32ProcessID, hpl.th32HeapID))
                        continue;

                    int blocksCount = 0;
                    do
                    {
                        freeSpaceSize += ((hpe.dwFlags & (uint)HeapEntryFlags.Free) != 0 ? hpe.dwBlockSize : 0);
                        blocksCount++;
                    } while (blocksCount < maxBlocksCount && Heap32Next(ref hpe));
                    heapsCount++;

                } while (heapsCount < maxHeapsCount && Heap32ListNext(hSnapshot, ref hpl));
            }
            finally
            {
                CloseHandle(hSnapshot);
            }

            return freeSpaceSize;
        }

        private void UpdateAnswer(string exePath, uint freeSpaceSize)
        {
            if (freeSpaceSize > maxFreeSpaceSize)
            {
                maxFreeSpaceSize = freeSpaceSize;
                maxFreeSpaceSizeProcesses.Clear();
            }
            if (freeSpaceSize == maxFreeSpaceSize)
            {
                maxFreeSpaceSizeProcesses.Add(exePath);
            }
        }

        private void PrintAnswer()
        {
            foreach(var item in maxFreeSpaceSizeProcesses)
            {
                printNextAnswerProcesss($"{item} : {maxFreeSpaceSize} байт");
            }
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

        [Flags]
        internal enum HeapEntryFlags : uint
        {
            Fixed = 0x00000001,
            Free = 0x00000002,
            Moveable = 0x00000004,
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

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct HEAPLIST32
        {
            public uint dwSize;
            public uint th32ProcessID;
            public uint th32HeapID;
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
        static extern bool Process32First([In] IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool Process32Next([In] IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

        [DllImport("kernel32", SetLastError = true)]
        private static extern bool CloseHandle([In] IntPtr hObject);

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool Heap32ListFirst([In]IntPtr hSnapshot, ref HEAPLIST32 lph1);

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool Heap32ListNext([In] IntPtr hSnapshot, ref HEAPLIST32 lph1);

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool Heap32First(ref HEAPENTRY32 lphe, [In]uint th32ProcessID, [In]uint th32HeapID);

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool Heap32Next(ref HEAPENTRY32 lphe);

        #endregion
    }
}
