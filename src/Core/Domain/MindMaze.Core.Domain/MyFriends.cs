using MindMaze.Core.Domain.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Domain
{
    public class MyFriends : BaseClass
    {
        public Guid User_ID { get; set; }

        public Guid Friend_ID { get; set; }

        public string User_Token_ID {get; set;}

        public string Friend_Token_ID { get; set; }

        public Users Friend { get; set; }
    }
}
