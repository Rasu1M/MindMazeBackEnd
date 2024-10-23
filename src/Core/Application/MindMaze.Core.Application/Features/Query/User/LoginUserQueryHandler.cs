using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using MindMaze.Core.Application.Extensions.PassWordGenerator;
using MindMaze.Core.Application.Extensions.TokenGenerator;
using MindMaze.Core.Application.Interfaces.IGenericRepository;
using MindMaze.Core.Domain;
using MindMaze.Core.Domain.ResponseObjects.User;
using MindMaze.Core.Domain.Resultobjects;
using System.Security.Claims;

namespace MindMaze.Core.Application.Features.Query.User
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, CustomResult<LoginUserResponse>>
    {
        private readonly IReadGenericRepository<Users> _read;
        private readonly IConfiguration _configuration;


        public LoginUserQueryHandler(IReadGenericRepository<Users> read, IConfiguration configuration)
        {
            _read = read;
            _configuration = configuration;
        }

        public async Task<CustomResult<LoginUserResponse>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {

            var user = await _read.Getasync( x => x.Email == request.Email );

            if( user == null )

                return CustomResult<LoginUserResponse>.Failure(new Error("User Not Found"));

            if (!user.IsEmailConfirmed)

                return CustomResult<LoginUserResponse>.Failure(new Error("Email is not confirmed"));

            if (user.Password != EncodePassword.EncryptPassWordWithKey(request.Password, user.PassWordKey))

                return CustomResult<LoginUserResponse>.Failure(new Error("Password or email is wrong"));

          


            var claims = new Claim[]{

                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email )
            };

            

            return LoginUserResponse.Create(JwtBearerTokenGenerator.GenerateToken(claims, _configuration["AuthSecurityKey"]),
                user.IDToken, user.UserName, user.Point);

        }
    }
}
