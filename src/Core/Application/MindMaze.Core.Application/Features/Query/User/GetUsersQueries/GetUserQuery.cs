using MediatR;
using MindMaze.Core.Domain.ResponseObjects.User;
using MindMaze.Core.Domain.Resultobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Application.Features.Query.User.GetUsersQueries
{
    public record GetUserQuery : IRequest<CustomResult<UserInfoResponse>>
    {
        public string IDToken { get; set; }
    }
}
