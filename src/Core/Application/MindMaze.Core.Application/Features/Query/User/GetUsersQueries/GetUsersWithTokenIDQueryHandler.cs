using MediatR;
using MindMaze.Core.Application.Interfaces.IGenericRepository;
using MindMaze.Core.Domain;
using MindMaze.Core.Domain.ResponseObjects.User;
using MindMaze.Core.Domain.Resultobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Application.Features.Query.User.GetUsersQueries
{
    public class GetUsersWithTokenIDQueryHandler : IRequestHandler<GetUsersWithTokenIDQuery, CustomResult<UserInfoResponse>>
    {
        private readonly IReadGenericRepository<Users> _read;

        public GetUsersWithTokenIDQueryHandler(IReadGenericRepository<Users> read)
        {
            _read = read;
        }

        public async Task<CustomResult<UserInfoResponse>> Handle(GetUsersWithTokenIDQuery request, CancellationToken cancellationToken)
        {

            var user = await _read.Getasync(x => x.IDToken == request.Token_ID);



            return new UserInfoResponse()
            {
                UserName = user.UserName,
                IDToken = user.IDToken,
                point = user.Point
            };
        }
    }
}
