using MediatR;
using MindMaze.Core.Application.Interfaces;
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
    public class GetUsersWithUsernameQueryHandler : IRequestHandler<GetUsersWithUsernameQuery, CustomResult<List<UserInfoResponse>>>
    {
        private readonly IReadGenericRepository<Users> _read;

        public GetUsersWithUsernameQueryHandler(IReadGenericRepository<Users> read)
        {
            _read = read;
        }

        public async Task<CustomResult<List<UserInfoResponse>>> Handle(GetUsersWithUsernameQuery request, CancellationToken cancellationToken)
        {
            var users = (await _read.GetAllAsync(x => x.UserName == request.UserName)).ToList()
                .ConvertAll(x => new UserInfoResponse()
                {
                    IDToken = x.IDToken,
                    UserName = x.UserName,
                    point = x.Point
                });
            
            if(users == null)
            return  CustomResult<List<UserInfoResponse>>.Failure(new Error(CustomErrorMessages.DatabaseError));

            return users;
        }
    }
}
