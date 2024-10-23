using MediatR;
using MindMaze.Core.Domain.ResponseObjects.Notifications;
using MindMaze.Core.Domain.Resultobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Application.Features.Query.NotificationsQuery
{
    public record GetNotificationsQuery(Guid Receiver_ID) : IRequest<CustomResult<List<NotificationsInfoResponse>>>;
}
