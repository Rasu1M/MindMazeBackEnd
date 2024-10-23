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

namespace MindMaze.Core.Application.Features.Command.User.Remove
{
    public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand, CustomResult>
    {

        private readonly IGenericRepository<Users> _generic;

        private readonly IUnitOfWork _unitOfWork;

        public RemoveUserCommandHandler(IGenericRepository<Users> generic, IUnitOfWork unitOfWork)
        {
            _generic = generic;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResult> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            _generic.DeletebyID(request.User_ID);

            return await _unitOfWork.SaveChangesAsync() > 0 ? true : CustomResult.Failure(new Error(CustomErrorMessages.DatabaseError));
        }
    }
}
