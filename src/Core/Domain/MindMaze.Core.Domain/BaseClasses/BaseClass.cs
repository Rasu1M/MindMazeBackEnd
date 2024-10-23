using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Domain.BaseClasses
{
    public  abstract class BaseClass
    {
        public Guid ID { get; set; }

        public DateTime Created_Date { get; set; }
    }
}
