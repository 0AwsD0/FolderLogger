using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolderLogger
{
    public partial class Form1 : Form
    {

        string delay_txt_box;
        int delay = 600000;
        bool file_switch = false;
        int filenumber = 1;
        public void recovery()
        {
             try
             {
                 StreamReader Reader = new StreamReader("list"+filenumber+".txt");
                 string data = Reader.ReadLine();

                 while (data != null)
                 {
                     //Process.Start(@"C:\Windows\explorer.exe");
                     Process.Start("explorer.exe", data);
                     //Console.WriteLine(data);
                     data = Reader.ReadLine();
                }

                 Reader.Close();

            }
             catch (Exception e)
             {
                label7.Text = "Error on file1, trying file 2.";
                filenumber = 2;
                recovery();
            }
             
            label7.Text = "Folders loaded.";
        }

        //SECTION START tray hide shot and taskbar
        /* private void Form1_Resize(object sender, System.EventArgs e)
         {
             if (FormWindowState.Minimized == WindowState)
                 WindowState = FormWindowState.Minimized;
                 ShowInTaskbar = false;
         }*/
        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
        }

        public void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
        }

        //SECTION END tray hide shot and taskbar

        public Form1()
        {
            InitializeComponent();
           /* recovery(); */
            this.WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
            timer1.Interval = delay;
            label5.Text = Convert.ToString(delay);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                delay_txt_box = textBox1.Text;
                delay = Convert.ToInt32(delay_txt_box);
                timer1.Interval = delay;
                label5.Text = textBox1.Text;
            }
            catch(Exception error)
            {
                delay = 600000;
                timer1.Interval = 600000;
                label5.Text = Convert.ToString(delay);
            }
        }

        private void FileWrite(int filenumber)
        {
            StreamWriter File = new StreamWriter("list" + filenumber + ".txt");
            string date = DateTime.Now.ToString("yyyyMMdd_hhmmss");

            foreach (SHDocVw.InternetExplorer window in new SHDocVw.ShellWindows())
            {
                if (Path.GetFileNameWithoutExtension(window.FullName).ToLowerInvariant() == "explorer")
                {
                    if (Uri.IsWellFormedUriString(window.LocationURL, UriKind.Absolute))
                        Console.WriteLine(new Uri(window.LocationURL).LocalPath);
                    try
                    {
                        File.WriteLine(new Uri(window.LocationURL).LocalPath);
                        label7.Text = "Save complite. Time: " + date;
                    }
                    catch (Exception error)
                    {
                        label7.Text = "There was an error while saving opened folders.";
                    }
                }
            }
            File.Close();
            if(filenumber == 2)
            {
                CopyFile(date);
            }
        }

        public void CopyFile(string data)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(@".\Archive\");
            file.Directory.Create();
            try
            {
                File.Copy("list2.txt", @".\Archive\list2_" + data + ".txt");
            }
            catch
            {
                label7.Text = "There was an error while executing copy file order.";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            FileWrite(1);
            FileWrite(2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            recovery();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FileWrite(1);
            FileWrite(2);
        }
    }
}
