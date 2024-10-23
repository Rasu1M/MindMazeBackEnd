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
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, CustomResult<UserInfoResponse>>
    {
        private readonly IReadGenericRepository<Users> _read;

        public GetUserQueryHandler(IReadGenericRepository<Users> read)
        {
            _read = read;
        }

        public async Task<CustomResult<UserInfoResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _read.Getasync(user => user.IDToken == request.IDToken);

            if (user == null)
            {
                return CustomResult<UserInfoResponse>.Failure(new Error("User nod found"));
            }

            return new UserInfoResponse()
            {
                IDToken = user.IDToken,
                UserName = user.UserName,
                point = user.Point
            };


        }
    }
}
