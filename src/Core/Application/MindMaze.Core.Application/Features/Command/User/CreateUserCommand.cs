using MediatR;
using MindMaze.Core.Domain.Resultobjects;

namespace MindMaze.Core.Application.Features.Command.User
{
    public record CreateUserCommand :IRequest<CustomResult>
    {
        public string UserName { get; set;}

        public string Email { get; set;}

        public string Password { get; set;}
    }
}
