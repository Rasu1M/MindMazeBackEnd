using MediatR;
using MindMaze.Core.Application.Interfaces;
using MindMaze.Core.Application.Interfaces.IGenericRepository;
using MindMaze.Core.Domain;
using MindMaze.Core.Domain.Resultobjects;

namespace MindMaze.Core.Application.Features.Command.User.Change
{
    public class ChangeUserinfoCommandHandler : IRequestHandler<ChangeUserInfoCommand, CustomResult>
    {
        private readonly IGenericRepository<Users> _generic;

        private readonly IUnitOfWork _unitOfWork;

        public ChangeUserinfoCommandHandler(IGenericRepository<Users> generic, IUnitOfWork unitOfWork)
        {
            _generic = generic;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResult> Handle(ChangeUserInfoCommand request, CancellationToken cancellationToken)
        {
            var user = _generic.GetByID(request.User_ID);

            if (user == null)
                return CustomResult.Failure(new Error(CustomErrorMessages.UserNotFoundError));

            user.UserName = request.UserName;
            _generic.UpdateEntity(user);

            return await _unitOfWork.SaveChangesAsync() > 0 ? true : CustomResult.Failure(new Error(CustomErrorMessages.DatabaseError));
        }
    }
}
