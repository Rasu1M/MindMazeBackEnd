using MindMaze.Core.Domain.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Domain
{
    public class Games : BaseClass
    {
        public Guid User_ID { get; set; }

        public Guid Opponent_ID { get; set; }

        public int MyPoint { get; set; }

        public int OpponentPoint { get; set; }

        public string type { get; set; }

       
        public Users Opponent { get; set; }

        public ICollection<GamesQuestions> gamesQuestions { get; set; }
    }
}
