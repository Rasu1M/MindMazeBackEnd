using MediatR;
using MindMaze.Core.Application.Interfaces;
using MindMaze.Core.Application.Interfaces.IGenericRepository;
using MindMaze.Core.Domain;
using MindMaze.Core.Domain.Resultobjects;

namespace MindMaze.Core.Application.Features.Command.User.Verify
{
    public class EmailVerifyCommandHandler : IRequestHandler<EmailVerifyCommand, CustomResult>
    {
        private readonly IGenericRepository<Users> _generic;

        private readonly IUnitOfWork _unitOfWork;

        public EmailVerifyCommandHandler(IGenericRepository<Users> generic, IUnitOfWork unitOfWork)
        {
            _generic = generic;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResult> Handle(EmailVerifyCommand request, CancellationToken cancellationToken)
        {
            var user = _generic.Get(x => x.PasswordVerificationToken == request.verificationToken);

            if (user == null)
            {
                return CustomResult.Failure(new Error("User not found"));
            }

            user.IsEmailConfirmed = true;

            _generic.UpdateEntity(user);

            return await _unitOfWork.SaveChangesAsync() > 0 ? true : CustomResult.Failure(new Error(CustomErrorMessages.DatabaseError));
        }
    }
}
