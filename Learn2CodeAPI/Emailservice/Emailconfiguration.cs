using System;
using System.Collections.Generic;
using System.Text;

namespace Emailservice
{
    public class Emailconfiguration
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    }
}
