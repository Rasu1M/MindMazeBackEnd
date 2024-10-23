using MediatR;
using MindMaze.Core.Domain.ResponseObjects.User;
using MindMaze.Core.Domain.Resultobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Application.Features.Query.MyFriendsQuery
{
    public record GetFriendsInfoQuery : IRequest<CustomResult<List<UserInfoResponse>>>
    {
        public Guid User_ID { get; set; }

        /*public short count { get; set; }*/
    }
}
