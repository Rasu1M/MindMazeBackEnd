using MediatR;
using MindMaze.Core.Application.Extensions.PassWordGenerator;
using MindMaze.Core.Application.Interfaces;
using MindMaze.Core.Application.Interfaces.IGenericRepository;
using MindMaze.Core.Domain;
using MindMaze.Core.Domain.Resultobjects;

namespace MindMaze.Core.Application.Features.Command.User.Change
{
    public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, CustomResult>
    {
        private readonly IGenericRepository<Users> _generic;

        private readonly IUnitOfWork _unitOfWork;

        public ChangeUserPasswordCommandHandler(IGenericRepository<Users> _generic, IUnitOfWork unitOfWork)
        {
            this._generic = _generic;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResult> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _generic.GetByIDAsync(request.UserId);

            if (user == null)
                return CustomResult.Failure(new Error(CustomErrorMessages.DatabaseError));

            if (user.Password != EncodePassword.EncryptPassWordWithKey(request.OldPassword, user.PassWordKey))
                return CustomResult.Failure(new Error("Password is incorrect"));

            user.Password = EncodePassword.EncryptPassWordWithKey(request.NewPassword, user.PassWordKey);

            _generic.UpdateEntity(user);

            return await _unitOfWork.SaveChangesAsync() > 0 ? true : CustomResult.Failure(new Error(CustomErrorMessages.DatabaseError));

        }
    }
}
