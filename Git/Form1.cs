using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Memory;

namespace Git
{
    public partial class Form1 : Form
    {
        public static string RifleAmmo = "ac_client.exe+0x00109B74,150";
        public static string PIDMc = "0xCDD9BEA0"; //memory vita
        Mem memo = new Mem();

        public Form1()
        {
            InitializeComponent();
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(colorChanger));
            thread.Start();
            thread.Priority = ThreadPriority.Lowest;
            Console.WriteLine(Process.GetCurrentProcess().ProcessName);
        }

        private void colorChanger()
        {
            int a = 255;
            int b = 0;
            int c = 0;
            while (true)
            {

                if(a == 255 && c == 0)
                {
                    b++;
                }
                if(b == 255 && c == 0)
                {
                    a--;
                }
                if(a == 0 && b == 255)
                {
                    c++;
                }
                if (c == 255 && a == 0)
                {
                    b--;
                }
                if (b == 0 && c == 255)
                {
                    a++;
                }
                if (a == 255 && b == 0)
                {
                    c--;
                }
                label1.ForeColor = Color.FromArgb(a, b, c);
                Thread.Sleep(5);
            }
        }

        private void Destructer()
        {
            //string tempFolder = Environment.ExpandEnvironmentVariables("%TEMP%");
            string recent = Environment.GetFolderPath(Environment.SpecialFolder.Recent);
            string prefetch = Environment.ExpandEnvironmentVariables("%SYSTEMROOT%") + "\\Prefetch";
            EmptyFolderContents(recent);
            EmptyFolderContents(prefetch);
            Process.GetCurrentProcess().Kill();
        }

        private void EmptyFolderContents(string folderName)
        {
            foreach (var file in Directory.GetFiles(folderName))
            {
                if (file.StartsWith(Process.GetCurrentProcess().ProcessName) || file.StartsWith(Process.GetCurrentProcess().ProcessName.ToUpper()))
                {
                    File.Delete(file);
                }
            }
        }

        private void metroButton7_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            //Hide();
        }

        private void metroButton8_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(Destructer));
            thread.Start();
        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            int PID = memo.GetProcIdFromName("javaw.exe");
            if (PID > 0)
            {
                memo.OpenProcess(PID);
                memo.WriteMemory(PIDMc, "int", "1000");
            }
        }
    }
}
