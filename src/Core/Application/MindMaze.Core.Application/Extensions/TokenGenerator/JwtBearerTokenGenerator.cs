using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Application.Extensions.TokenGenerator
{
    public class JwtBearerTokenGenerator
    { 
        public static string GenerateToken(Claim[] customclaims, string keystring)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keystring));

            var signingcredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(

                issuer: "MindMazeProject",
                audience: "MindMazeProject",
                claims : customclaims,
                expires : DateTime.UtcNow.AddDays(1),
                signingCredentials : signingcredentials
                );
            

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
