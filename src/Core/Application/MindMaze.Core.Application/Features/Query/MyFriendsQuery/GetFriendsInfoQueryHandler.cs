using MediatR;
using Microsoft.EntityFrameworkCore;
using MindMaze.Core.Application.Interfaces.IGenericRepository;
using MindMaze.Core.Domain;
using MindMaze.Core.Domain.ResponseObjects.User;
using MindMaze.Core.Domain.Resultobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Application.Features.Query.MyFriendsQuery
{
    public class GetFriendsInfoQueryHandler : IRequestHandler<GetFriendsInfoQuery, CustomResult<List<UserInfoResponse>>>
    {
        private readonly IReadGenericRepository<MyFriends> _read;

        public GetFriendsInfoQueryHandler(IReadGenericRepository<MyFriends> read)
        {
            _read = read;
        }

        public async Task<CustomResult<List<UserInfoResponse>>> Handle(GetFriendsInfoQuery request, CancellationToken cancellationToken)
        {

            List<UserInfoResponse> friends = _read.AsQueryable().Where(x => x.User_ID == request.User_ID)
                .Include(x => x.Friend).Select(x => new UserInfoResponse()
                {
                    IDToken = x.Friend.IDToken,
                    UserName = x.Friend.UserName,
                    point = x.Friend.Point,
                }).ToList();

            return friends;
        }
    }
}
