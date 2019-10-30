using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp7
{
    public partial class Form1 : Form
    {
        Semaphore s;
        List<Thread> threads1;
        List<Thread> threads2;
        List<Thread> threads3;
        int counter;
        public Form1()
        {
            InitializeComponent();
           
            s = new Semaphore(2, 2);
            threads1 = new List<Thread>();
            threads2 = new List<Thread>();
            threads3 = new List<Thread>();
            counter = 0;
            numericUpDown1.Value = 2;
        }

        private void UpdateListBox(object listBox, object tmp, bool isAdded)
        {
            ListBox list = listBox as ListBox;
            if (list.InvokeRequired)
            {
                list.Invoke(new Action<object, object, bool>(UpdateListBox), listBox, tmp, isAdded);
            }
            else
            {
                if (isAdded)
                {
                    list.Items.Add(tmp);
                }
                else
                    list.Items.RemoveAt((int)tmp);
            }
        }

        private void GoToWork()
        {
            if (counter < numericUpDown1.Value && threads2.Count > 0)
            {
                Thread t = threads2[0];
                threads2.Remove(t);
                UpdateListBox(listBox2 as object, 0, false);
                threads3.Add(t);
                UpdateListBox(listBox3 as object, t.ManagedThreadId, true);
                counter++;
                t.Start();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(() => ThreadMethod());
            threads1.Add(t);
            listBox1.Items.Add(t.ManagedThreadId);
         
        }

        private void ThreadMethod()
        {
            if (s.WaitOne())
            {
                Thread.Sleep(5000);
                s.Release();
                threads3.RemoveAt(0);
                UpdateListBox(listBox3 as object, 0, false);
                counter--;
              
            }  GoToWork();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            GoToWork();
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;

            Thread t = threads1[listBox1.SelectedIndex];
            threads2.Add(t);
            listBox2.Items.Add(t.ManagedThreadId);
            threads1.Remove(t);
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            GoToWork();
        }
    }
}