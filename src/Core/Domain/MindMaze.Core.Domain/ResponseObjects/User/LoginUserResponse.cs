using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Domain.ResponseObjects.User
{
    public class LoginUserResponse : UserInfoResponse
    {
        private LoginUserResponse(string token, string token_ID, string username, int point) : base(token_ID, username, point)
        {
            this.Token = token;
        }

        public string Token { get; set; }

        public static LoginUserResponse Create(string token, string token_ID, string username, int point)
            => new LoginUserResponse(token, token_ID, username, point);
    }
}
