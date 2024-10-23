using MediatR;
using MindMaze.Core.Application.Extensions.PassWordGenerator;
using MindMaze.Core.Application.Interfaces;
using MindMaze.Core.Application.Interfaces.IGenericRepository;
using MindMaze.Core.Domain;
using MindMaze.Core.Domain.Resultobjects;
using System.Security.Cryptography;

namespace MindMaze.Core.Application.Features.Command.User
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CustomResult>
    {

        private readonly IGenericRepository<Users> _generic;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IEmailService emailservice;

        public CreateUserCommandHandler(IGenericRepository<Users> generic, IUnitOfWork unitOfWork, IEmailService emailservice)
        {
            _generic = generic;
            _unitOfWork = unitOfWork;
            this.emailservice = emailservice;
        }

        public async Task<CustomResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            _generic.AsQueryable();

            var user = await _generic.Getasync(x => x.Email == request.Email);

            if (user is not null)

                return CustomResult.Failure(new Error("This Email is Used"));

             

            string password = EncodePassword.EncryptPassWord(request.Password, out byte[] PassWordKey);


            var idtoken = EncodePassword.GetTokenID();

            short checkcount = 0;

            while(await _generic.Getasync(user => user.IDToken == idtoken) != null && checkcount < 10)
            {
                idtoken = EncodePassword.GetTokenID();

                checkcount++;
            }

            if (checkcount == 10)
                return CustomResult.Failure(new Error(CustomErrorMessages.DatabaseError));



            var newuser = new Users()
            {
                UserName = request.UserName,
                IDToken = idtoken,
                Email = request.Email,
                Password = password,
                PassWordKey = PassWordKey,
                Point = 100,
                Status = "Active",
                IsEmailConfirmed = false,
                EmailVerificationToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(64)),
                PasswordVerificationToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(64))
            };

            await _generic.Addasync(newuser);


            //TODO
            //await emailservice.SendEmailtoGmail(newuser.Email);

            return await _unitOfWork.SaveChangesAsync() > 0 ? true : CustomResult.Failure(new Error(CustomErrorMessages.DatabaseError));
        }
    }
}
