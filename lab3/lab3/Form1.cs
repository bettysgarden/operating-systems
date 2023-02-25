using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3
{
    public partial class Form1 : Form
    {
        Win32Snapshot win32Snapshot;
        Thread searchThread;

        public Form1()
        {
            InitializeComponent();
            win32Snapshot = new Win32Snapshot(PrintProcess, AnswerPrint, PrintSearchState);
        }

        class ThreadSt
        {
            private readonly Win32Snapshot win32Snapshot;

            public ThreadSt(Win32Snapshot win32Snapshot)
            {
                this.win32Snapshot = win32Snapshot;
            }
            public void Start()
            {
                win32Snapshot.StartSearch(1000, 5, 30);
            }
        }

        private void сделатьСнимокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int maxProcessesCount = Int32.Parse(maxProcCntTextBox.Text),
                    maxHeapsCount = Int32.Parse(maxHeapsCntTextBox.Text),
                    maxBlocksCount = Int32.Parse(maxBlocksCntTextBox.Text);

                if (maxProcessesCount < 1 || maxHeapsCount < 1 || maxBlocksCount < 1)
                    throw new Exception();
            }
            catch(Exception)
            {
                MessageBox.Show("Укажите корректное число процессов, куч и блоков.\nДолжно быть натуральное число");
                return;
            }

            processesListBox.Items.Clear();
            answerListBox.Items.Clear();
            searchThread = new Thread(new ThreadSt(win32Snapshot).Start);
            searchThread.Start();
        }

        private void PrintProcess(string message)
        {
            processesListBox.Invoke(new Action(() =>
                processesListBox.Items.Add(message)
            ));
        }

        private void AnswerPrint(string message)
        {
            answerListBox.Invoke(new Action(() =>
                answerListBox.Items.Add(message)
            ));
        }

        private void PrintSearchState(string message)
        {
            StateLabel.Invoke(new Action(() => StateLabel.Text = message));
        }

        private void остановитьПоискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            searchThread?.Abort();
            PrintSearchState("Поиск остановлен");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            searchThread?.Abort();
        }
    }
}
