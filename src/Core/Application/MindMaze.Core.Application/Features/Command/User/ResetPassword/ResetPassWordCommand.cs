using MediatR;
using MindMaze.Core.Domain.Resultobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Application.Features.Command.User.ResetPassword
{
    public record ResetPassWordCommand :IRequest<CustomResult>
    {
        public string ResetToken { get; set; }

        public string email { get; set; }

        public string password { get; set; }
    }
}
