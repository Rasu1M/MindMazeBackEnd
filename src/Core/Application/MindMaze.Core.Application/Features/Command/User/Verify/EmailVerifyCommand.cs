using MediatR;
using MindMaze.Core.Domain.Resultobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Application.Features.Command.User.Verify
{
    public record EmailVerifyCommand : IRequest<CustomResult>
    {
        public string verificationToken { get; set; }

        public string email{ get; set; }


    }
}
