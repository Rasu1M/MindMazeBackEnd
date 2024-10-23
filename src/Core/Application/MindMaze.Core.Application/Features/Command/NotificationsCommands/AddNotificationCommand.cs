using MediatR;
using MindMaze.Core.Domain.ResponseObjects.Notifications;
using MindMaze.Core.Domain.Resultobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Application.Features.Command.NotificationsCommands
{
    public class AddNotificationCommand : IRequest<CustomResult<List<NotificationsInfoResponse>>>
    {
        public Guid User_ID {  get; set; }

        public string Receiver_TokenID { get; set; }
    }
}
