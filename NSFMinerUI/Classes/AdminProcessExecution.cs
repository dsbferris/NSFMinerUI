using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NSFMinerUI
{
    public class AdminProcessExecution
    {
        const string aespw = "FerrisIsAGenius1234!";

        /// <summary>
        /// Takes a prepared Process and executes this with admin Priveleges.
        /// </summary>
        /// <param name="p"></param>
        public static bool StartAsAdmin(Process p)
        {
            var up = CryptoLib.DecryptUsernamePassword("");
            p.StartInfo.UserName = up.Username;
            p.StartInfo.PasswordInClearText = up.Password;
            return p.Start();
        }

    }

    public class UsernamePassword
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
