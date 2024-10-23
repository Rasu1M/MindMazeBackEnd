using MediatR;
using MindMaze.Core.Application.Interfaces;
using MindMaze.Core.Application.Interfaces.IGenericRepository;
using MindMaze.Core.Domain;
using MindMaze.Core.Domain.ResponseObjects.Notifications;
using MindMaze.Core.Domain.Resultobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Application.Features.Command.NotificationsCommands
{
    public class AddNotificationCommandHandler : IRequestHandler<AddNotificationCommand, CustomResult>
    {

        private readonly IReadGenericRepository<Users> _read;

        private readonly IWriteGenericRepository<Notifications> _write;

        private readonly IUnitOfWork _unitOfWork;

        public AddNotificationCommandHandler(IReadGenericRepository<Users> read, IWriteGenericRepository<Notifications> write, IUnitOfWork unitOfWork)
        {
            _read = read;
            _write = write;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResult> Handle(AddNotificationCommand request, CancellationToken cancellationToken)
        {

            _write.GetAsQueryable();

           await _unitOfWork.SaveChangesAsync();


            var user = await _read.Getasync(x => x.IDToken == request.Receiver_TokenID);

            if (user == null)
                return CustomResult<List<NotificationsInfoResponse>>.Failure(new Error(CustomErrorMessages.DatabaseError));

            var notif = new Notifications()
            {
                Sender_ID = request.User_ID,
                Recevier_ID = user.ID,
                Status = "Send"
            };
            await _write.Addasync(notif);

            return await _unitOfWork.SaveChangesAsync() > 0 ? true : CustomResult.Failure(new Error(CustomErrorMessages.DatabaseError));
        }
    }
}
