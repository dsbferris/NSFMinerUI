using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSFMinerUI
{
    public partial class Form1 : Form
    {

        bool running = false;
        Process p;

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Form1()
        {
            InitializeComponent();
            Application.ApplicationExit += Application_ApplicationExit;

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (p?.HasExited == false) p.Kill(true);
        }

        void startMiner()
        {
            if (running)
            {
                if(MessageBox.Show("Already running?", "Start?", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return;
                }
            }

            p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                FileName = @"F:\nsfminer_1.3.8-windows_10-cuda_11.2-opencl\nsfminer.exe",
                Arguments = @"-P stratum1+tcp://wallet@eth-eu2.nanopool.org:9999/minertest/email"
            };
            psi.WorkingDirectory = Path.GetDirectoryName(psi.FileName);
            p.StartInfo = psi;


            p.Start();
            p.BeginOutputReadLine();

            p.OutputDataReceived += (s, e) =>
            {
                if(!string.IsNullOrEmpty(e.Data)) writeToTextBox(e.Data);
            };
        }

        private delegate void SafeCallDelegate(string text);
        void writeToTextBox(string text)
        {
            if (richTextBox1.InvokeRequired)
            {
                var d = new SafeCallDelegate(writeToTextBox);
                richTextBox1.Invoke(d, new object[] { text });
            }
            else
            {
                string cleanText = Regex.Replace(text, @"[^\u0020-\u007E]", string.Empty);
                cleanText = cleanText.Replace("[97m", string.Empty);
                cleanText = cleanText.Replace("[0m", string.Empty);
                richTextBox1.Text += cleanText + "\n";
                log.Info(cleanText);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            startMiner();
        }



        /*
        void startProcess()
        {
            Process p = new Process();
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            // set admin user and password
            p.StartInfo.UserName = "Administrator";
            string pw = "accountPassword";
            var s = new System.Security.SecureString();
            foreach (char c in pw)
            {
                s.AppendChar(c);
            }

            p.StartInfo.Password = s;
            ProcessStartInfo psi = p.StartInfo;
            psi.UseShellExecute = false;
            //psi.Verb = "runas";
            psi.FileName = @"C:\Users\Ferris\Downloads\nvoclock-0.0.3-win64.exe";
            psi.Arguments = "set pstate -c memory 1200000";
            psi.RedirectStandardOutput = true;

            p.StartInfo = psi;
            p.OutputDataReceived += P_OutputDataReceived;
            p.Start();
            p.BeginOutputReadLine();
        }

        private void P_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                AppendText(e.Data);
            }

        }

        void AppendText(string text)
        {
            if (textBox1.InvokeRequired)
            {
                textBox1.Invoke(new Action<string>(AppendText), new object[] { text });
            }
            else
            {
                textBox1.Text += text;
            }
        }

*/
    }
}
