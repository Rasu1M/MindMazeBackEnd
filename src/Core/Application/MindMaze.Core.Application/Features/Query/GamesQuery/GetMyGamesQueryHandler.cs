using MediatR;
using Microsoft.IdentityModel.Tokens;
using MindMaze.Core.Application.Interfaces.IGenericRepository;
using MindMaze.Core.Domain;
using MindMaze.Core.Domain.ResponseObjects.Games;
using MindMaze.Core.Domain.Resultobjects;
using Microsoft.EntityFrameworkCore;

using Microsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Application.Features.Query.GamesQuery
{
    public class GetMyGamesQueryHandler : IRequestHandler<GetMyGamesQuery, CustomResult<List<GetGamesInfoResponse>>>
    {

        private readonly IReadGenericRepository<Games> _read;

        public GetMyGamesQueryHandler(IReadGenericRepository<Games> read)
        {
            _read = read;
        }

        public async Task<CustomResult<List<GetGamesInfoResponse>>> Handle(GetMyGamesQuery request, CancellationToken cancellationToken)
        {

            var games = _read.AsQueryable().Where(x => x.User_ID == request.User_ID
            && request.Type.IsNullOrEmpty() ? true : request.Type == x.type).Include(x => x.Opponent).ToList();


            return games.ConvertAll(game => new GetGamesInfoResponse()
            {
                Opponent_UserNaame = game.Opponent.UserName,
                MyPoint = game.MyPoint,
                OpponentPoint = game.OpponentPoint,
                Type = game.type

            });

        }
    }
}
