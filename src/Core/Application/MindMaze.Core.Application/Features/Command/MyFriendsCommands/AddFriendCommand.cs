using MediatR;
using MindMaze.Core.Domain.Resultobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Application.Features.Command.MyFriendsCommands
{
    public class AddFriendCommand : IRequest<CustomResult>
    {
        public Guid User_ID { get; set; }
        public string Friend_TokenID { get; set; }
    }
}
