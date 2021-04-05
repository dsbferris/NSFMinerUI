using System;
using System.Diagnostics;
using System.Text;
using NSFMinerUI.Helper;

namespace NSFMinerUI
{
    public class AdminProcess : Process
    {
        public void StartAsAdminAndWait()
        {
            var up = SecureDataStorage.GetUsernameAndPassword();
            this.StartInfo.UserName = up.Item1;
            this.StartInfo.Password = up.Item2;
            _ = Start();
            WaitForExit();
        }
    }
}
