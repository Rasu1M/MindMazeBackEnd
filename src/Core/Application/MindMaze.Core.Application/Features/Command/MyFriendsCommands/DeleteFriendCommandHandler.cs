using MediatR;
using MindMaze.Core.Application.Interfaces;
using MindMaze.Core.Application.Interfaces.IGenericRepository;
using MindMaze.Core.Domain;
using MindMaze.Core.Domain.Resultobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Application.Features.Command.MyFriendsCommands
{
    public class DeleteFriendCommandHandler : IRequestHandler<DeleteFriendCommand, CustomResult>
    {

        private readonly IWriteGenericRepository<MyFriends> _write;

        private readonly IReadGenericRepository<MyFriends> _read;

        private readonly IUnitOfWork _unitOfWork;

        public DeleteFriendCommandHandler(IWriteGenericRepository<MyFriends> write, IReadGenericRepository<MyFriends> read, IUnitOfWork unitOfWork)
        {
            _write = write;
            _read = read;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResult> Handle(DeleteFriendCommand request, CancellationToken cancellationToken)
        {
           var friends = (await _read.GetAllAsync(x => (x.User_ID == request.User_ID && x.Friend_Token_ID == request.Friend_TokenID)
           || (x.Friend_ID ==request.User_ID && x.User_Token_ID ==request.Friend_TokenID))).ToList();




            if (!friends.Any()) return CustomResult.Failure(new Error(CustomErrorMessages.DatabaseError));

            _write.DeleteRange(friends);

            return await _unitOfWork.SaveChangesAsync() > 0 ? true : CustomResult.Failure(new Error(CustomErrorMessages.DatabaseError));
        }
    }
}
