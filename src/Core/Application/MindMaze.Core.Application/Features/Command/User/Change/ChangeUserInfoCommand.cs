using MediatR;
using MindMaze.Core.Domain.Resultobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Application.Features.Command.User.Change
{
    public record ChangeUserInfoCommand(Guid User_ID, string UserName) : IRequest<CustomResult>;
}
