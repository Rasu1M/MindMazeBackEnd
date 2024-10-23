using MailKit.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Infrastructure.infrastructure.Services.EmailMessages
{
    public class EmailOptions
    {
        public string VerificationMessage { get; set; }

        public string ResetPassWord { get; set; }

        public string Gmail { get; set; }

        public int GmailPort { get; set; }

        public string ServerGmail { get; set; }

        public string ServerGmailPassword { get; set; }

        public SecureSocketOptions socketOption = SecureSocketOptions.StartTls;
    }
}
