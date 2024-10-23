using MediatR;
using MindMaze.Core.Domain.Resultobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Application.Features.Command.GamesCommands
{
    public record CreateGameCommand : IRequest<CustomResult>
    {
        public Guid User_ID { get; set; }

        public string Oppenent_IDToken { get; set; }

        public int MyPoint { get; set; }

        public int OpponentPoint { get; set; }
    }
}

