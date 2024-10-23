using MindMaze.Core.Domain.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Domain
{
    public class Notifications : BaseClass
    {
        public Guid Sender_ID { get; set; }

        public Guid Recevier_ID { get; set; }

        public string Status { get; set; }

        public Users Sender { get; set; }
    }
}
