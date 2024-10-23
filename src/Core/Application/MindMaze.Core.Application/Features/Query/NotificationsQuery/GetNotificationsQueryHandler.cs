using MediatR;
using Microsoft.EntityFrameworkCore;
using MindMaze.Core.Application.Interfaces.IGenericRepository;
using MindMaze.Core.Domain;
using MindMaze.Core.Domain.ResponseObjects.Notifications;
using MindMaze.Core.Domain.Resultobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Application.Features.Query.NotificationsQuery
{
    public class GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQuery, CustomResult<List<NotificationsInfoResponse>>>
    {
        private readonly IReadGenericRepository<Notifications> _read;

        public GetNotificationsQueryHandler(IReadGenericRepository<Notifications> read)
        {
            _read = read;
        }

        public async Task<CustomResult<List<NotificationsInfoResponse>>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
        {



            var listofnotificaitons =  _read.AsQueryable().Where(x => x.Recevier_ID == request.Receiver_ID)
                .Include(x => x.Sender).Select(x => new NotificationsInfoResponse()
                {
                    IDToken = x.Sender.IDToken,
                    UserName = x.Sender.UserName,
                    point = x.Sender.Point
                }).ToList();


            return listofnotificaitons;
        }
    }
}
