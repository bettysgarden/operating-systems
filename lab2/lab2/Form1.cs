using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using os2;

namespace OS2_
{
    public partial class Form1 : Form
    {
        private Task firstCatalog; 
        private Task secondCatalog;
        private Thread mainThread;
        public Form1()
        {
            InitializeComponent();
        }

        public void GetIntersect()
        {
            firstCatalog.Start();
            secondCatalog.Start();

            firstCatalog.thread.Join();
            secondCatalog.thread.Join();

            listBox1.Invoke((MethodInvoker)(() =>
            {
                // Выводим каталоги, в которых нет подкаталогов
                listBox1.Items.AddRange(
                    firstCatalog.Directories.ToArray());
            }));

            listBox2.Invoke((MethodInvoker)(() =>
            {
                listBox2.Items.AddRange(secondCatalog.Directories.ToArray());
            }));
            MessageBox.Show("Работа программы завершена.");

        }

        //выбор первого каталога
        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                firstCatalog = new Task(dialog.SelectedPath);
                textBox1.Text = dialog.SelectedPath;
            }
        }

        //выбор второго каталога
        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                secondCatalog = new Task(dialog.SelectedPath);
                textBox2.Text = dialog.SelectedPath;
            }
        }

        //запуск программы
        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            mainThread = new Thread(GetIntersect);
            mainThread.Start();

            button1.Hide();
            button2.Hide();
            button3.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
