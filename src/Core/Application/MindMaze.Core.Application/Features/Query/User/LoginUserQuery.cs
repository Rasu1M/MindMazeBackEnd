using MediatR;
using MindMaze.Core.Domain.ResponseObjects.User;
using MindMaze.Core.Domain.Resultobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Application.Features.Query.User
{
    public record LoginUserQuery : IRequest<CustomResult<LoginUserResponse>>
    {
        public string Email { get; set;}

        public string Password { get; set;}
    }
}
