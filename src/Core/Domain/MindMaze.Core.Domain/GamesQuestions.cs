using MindMaze.Core.Domain.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Domain
{
    public class GamesQuestions : BaseClass
    {
        public Guid Game_ID { get; set; }

        public Guid Question_ID { get; set; }

        public Games Game { get; set; }

        public Questions Question { get; set; }
    }
}
