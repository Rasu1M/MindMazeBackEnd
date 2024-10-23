using MediatR;
using MindMaze.Core.Domain.Resultobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Application.Features.Command.User.Change
{
    public class ChangeUserPasswordCommand : IRequest<CustomResult>
    {
        public Guid UserId { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
