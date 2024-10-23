using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MindMazeProject.Registrations
{
    public static class AuthenticationRegistratin
    {
        public static IServiceCollection RegisterToAuth(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var singingkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSecurityKey"]));

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = singingkey,
                        ValidAudience = "MindMazeProject",
                        ValidIssuer = "MindMazeProject"
                    };
                });

            return service;
        }
    }
}
