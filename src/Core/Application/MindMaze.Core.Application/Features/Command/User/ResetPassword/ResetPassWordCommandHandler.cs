using MediatR;
using MindMaze.Core.Application.Extensions.PassWordGenerator;
using MindMaze.Core.Application.Interfaces;
using MindMaze.Core.Application.Interfaces.IGenericRepository;
using MindMaze.Core.Domain;
using MindMaze.Core.Domain.Resultobjects;

namespace MindMaze.Core.Application.Features.Command.User.ResetPassword
{
    public class ResetPassWordCommandHandler : IRequestHandler<ResetPassWordCommand, CustomResult>
    {
        private readonly IGenericRepository<Users> _generic;

        private readonly IUnitOfWork _unitOfWork;

        public ResetPassWordCommandHandler(IGenericRepository<Users> generic, IUnitOfWork unitOfWork)
        {
            _generic = generic;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResult> Handle(ResetPassWordCommand request, CancellationToken cancellationToken)
        {
            var user = _generic.Get(x => x.Email == request.email);

            if (user == null)
            {
                return CustomResult.Failure(new Error("User nod found"));
            }

            if (user.PasswordVerificationToken != request.ResetToken)
            {
                return CustomResult.Failure(new Error("User nod found"));
            }

            user.Password = EncodePassword.EncryptPassWord(request.password, out byte[] passkey);

            user.PassWordKey = passkey;


            _generic.UpdateEntity(user);


            return await _unitOfWork.SaveChangesAsync() > 0 ? true : CustomResult.Failure(new Error(CustomErrorMessages.DatabaseError));
        }
    }
}
