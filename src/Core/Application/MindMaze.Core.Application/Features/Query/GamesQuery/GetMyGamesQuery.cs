using MediatR;
using MindMaze.Core.Domain.ResponseObjects.Games;
using MindMaze.Core.Domain.Resultobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Application.Features.Query.GamesQuery
{
    public record GetMyGamesQuery : IRequest<CustomResult<List<GetGamesInfoResponse>>>
    {
        public Guid User_ID { get; set; }

        public string Type { get; set; }
    }
}
