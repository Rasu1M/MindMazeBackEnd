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
    public class AddFriendCommandHandler : IRequestHandler<AddFriendCommand, CustomResult>
    {
        private readonly IWriteGenericRepository<MyFriends> _write;

        private readonly IReadGenericRepository<Users> _read;

        private readonly IUnitOfWork _unitOfWork;

        public AddFriendCommandHandler(IWriteGenericRepository<MyFriends> write, IReadGenericRepository<Users> read, IUnitOfWork unitOfWork)
        {
            _write = write;
            _read = read;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResult> Handle(AddFriendCommand request, CancellationToken cancellationToken)
        {

            var users = (await _read.GetAllAsync(x => x.ID == request.User_ID || x.IDToken == request.Friend_TokenID)).ToList();

            if (users == null)
                return CustomResult.Failure(new Error(CustomErrorMessages.DatabaseError));


            List<MyFriends> myfriends = new List<MyFriends>() {

                new MyFriends()
                {
                    User_ID = users[0].ID,
                    Friend_ID = users[1].ID,
                    User_Token_ID = users[0].IDToken,
                    Friend_Token_ID = users[1].IDToken
                },

                new MyFriends()
                {
                    User_ID = users[1].ID,
                    Friend_ID = users[0].ID,
                    User_Token_ID = users[1].IDToken,
                    Friend_Token_ID = users[0].IDToken
                }
            };

            await _write.AddRangeAsync(myfriends);


            return await _unitOfWork.SaveChangesAsync() > 0 ? true : CustomResult.Failure(new Error(CustomErrorMessages.DatabaseError));


        }
    }
}
