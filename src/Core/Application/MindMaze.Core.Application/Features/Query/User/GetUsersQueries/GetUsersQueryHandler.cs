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
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, CustomResult<List<UserInfoResponse>>>
    {

        private readonly IReadGenericRepository<Users> _read;

        private const int Count = 100;

        public GetUsersQueryHandler(IReadGenericRepository<Users> read)
        {
            _read = read;
        }

        public async Task<CustomResult<List<UserInfoResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = _read.AsQueryable().OrderByDescending(x => x.Point).Take(Count).ToList();

            if (!users.Any())
                return CustomResult<List<UserInfoResponse>>.Failure(new Error(CustomErrorMessages.DatabaseError));

            return users.ConvertAll(user => new UserInfoResponse()
            {
                IDToken = user.IDToken,
                UserName = user.UserName,
                point = user.Point
            });
        }

    }
}
