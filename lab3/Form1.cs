using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace OS_LAB3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            int treadPriority = int.Parse(textBox2.Text);
            List<string> list = new List<string>();

/*          List<(string, List<double>)> procAllThPri = new List<(string, List<double>)>();
            List<int> priorityList = new List<int>();*/

            var processList = Tool32Snap.GetProcessList();

            foreach (var processentry32 in processList)
            {
                // если ли в потоках данного процесса поток заданного приоритета
                bool priority = Tool32Snap.GetTreadPriority(processentry32.th32ProcessID, (int)processentry32.cntThreads, treadPriority);
                if (priority)
                    list.Add(processentry32.szExeFile);

/*                priorityList.Clear();
                список приоритетов всех потоков данного процесса
                priorityList = Tool32Snap.GetTreadPriorityList(processentry32.th32ProcessID, (int)processentry32.cntThreads);
                procAllThPri.Add((processentry32.szExeFile, priorityList));*/

            }

            var result = list.ToArray();
            foreach (var valueTuple in result)
            {
                listBox1.Items.Add(valueTuple);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            var processList = Tool32Snap.GetProcessList();

            List<int> priorityList = new List<int>();
            HashSet<int> prioritySet = new HashSet<int>();
            foreach (var processentry32 in processList)
            {
                // соберем множество значений приоритетов потоков, для того, чтобы вывести, какие бывают
                priorityList = Tool32Snap.GetTreadPriorityList(processentry32.th32ProcessID, (int)processentry32.cntThreads);
                priorityList.ForEach(x => prioritySet.Add(x));
            }

            foreach (var valueTuple in prioritySet)
            {
                listBox2.Items.Add(valueTuple);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }
    }
}
