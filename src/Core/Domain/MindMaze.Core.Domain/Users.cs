using MindMaze.Core.Domain.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Domain
{
    public class Users : BaseClass
    {

        public string IDToken { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public byte[] PassWordKey { get; set; }

        public int Point { get; set; }

        public string Status { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public string EmailVerificationToken { get; set; }

        public string PasswordVerificationToken { get; set; }

        public DateTime? PassWordResetExpire_Date { get; set; }

        public ICollection<Games> Games { get; set; }

        public ICollection<Notifications> Notifications { get; set; }

        public ICollection<MyFriends> MyFirends { get; set; }


    }
}
