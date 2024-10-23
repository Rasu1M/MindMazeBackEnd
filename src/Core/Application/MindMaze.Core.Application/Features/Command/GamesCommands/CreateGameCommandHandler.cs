using MediatR;
using MindMaze.Core.Application.Interfaces;
using MindMaze.Core.Application.Interfaces.IGenericRepository;
using MindMaze.Core.Domain;
using MindMaze.Core.Domain.Resultobjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Application.Features.Command.GamesCommands
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, CustomResult>
    {

        private readonly IWriteGenericRepository<Games> _write;
        private readonly IGenericRepository<Users> _generic;
        private readonly IUnitOfWork _unitOfWork;

        public CreateGameCommandHandler(IWriteGenericRepository<Games> write, IGenericRepository<Users> generic, IUnitOfWork unitOfWork)
        {
            _write = write;
            _generic = generic;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResult> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            // we can change that eachone send their own game but we didnt that here

            var users = (await _generic.GetAllAsync(x => x.IDToken == request.Oppenent_IDToken || x.ID == request.User_ID)).ToList();

            if (users == null)
                return CustomResult.Failure(new Error(CustomErrorMessages.DatabaseError));



            Users Winner = request.User_ID == users[0].ID ? users[0] : users[1];

            Users Loser = request.User_ID == users[0].ID ? users[1] : users[0];


            List<Games> games = new List<Games>(2)
            {
                new Games()
                {
                User_ID = Winner.ID,
                Opponent_ID = Loser.ID,
                MyPoint = request.MyPoint,
                OpponentPoint = request.OpponentPoint
                },

                new Games()
                {
                    User_ID = Loser.ID,
                    Opponent_ID = Winner.ID,
                    MyPoint = request.MyPoint,
                    OpponentPoint = request.OpponentPoint
                } 
            };



            //TODO Wrute More Continously

           Winner.Point += request.MyPoint;
           Loser.Point += request.OpponentPoint;

            _generic.UpdateEntities(users);

           await _write.AddRangeAsync(games);

            return await _unitOfWork.SaveChangesAsync() > 0 ? true : CustomResult.Failure(new Error(CustomErrorMessages.DatabaseError));

        }
    }
}
