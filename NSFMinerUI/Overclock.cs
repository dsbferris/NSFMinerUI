using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NSFMinerUI
{
    public static class Overclock
    {
        private const string username = "Administrator";
        private const string password = "PASSWORD";

        /// <summary>
        /// Sets GPU Memory Clock offset
        /// </summary>
        /// <param name="offset">Offset in MHz. 0-1500MHz</param>
        public static void SetMemoryClock(uint offset)
        {
            if (offset > 1500) throw new Exception("Offset too big and not supported!");

            Process p = new Process();
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            // set admin user and password
            p.StartInfo.UserName = username;
            var s = new System.Security.SecureString();
            foreach (char c in password)
            {
                s.AppendChar(c);
            }

            p.StartInfo.Password = s;
            ProcessStartInfo psi = p.StartInfo;
            psi.UseShellExecute = false;
            //psi.Verb = "runas";
            psi.FileName = @"nvoclock-0.0.3-win64.exe";
            psi.Arguments = "set pstate -c memory 0";
            psi.RedirectStandardOutput = true;

            p.StartInfo = psi;
            //p.OutputDataReceived += P_OutputDataReceived;
            p.Start();
            p.BeginOutputReadLine();
        }
    }
}
