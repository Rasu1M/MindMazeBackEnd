using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Domain.ResponseObjects.Games
{
    public class GetGamesInfoResponse
    {
        public string Opponent_UserNaame { get; set; }

        public int MyPoint { get; set; }

        public int OpponentPoint { get; set; }

        public string Type { get; set; }
    }
}
