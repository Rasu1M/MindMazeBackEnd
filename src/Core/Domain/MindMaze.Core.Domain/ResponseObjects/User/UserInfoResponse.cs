using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Domain.ResponseObjects.User
{
    public class UserInfoResponse
    {
        public UserInfoResponse()
        {
        }

        protected UserInfoResponse(string iDToken, string userName, int point)
        {
            IDToken = iDToken;
            UserName = userName;
            this.point = point;
        }

        public string IDToken { get; set; }

        public string UserName { get; set; }

        public int point { get; set; }


    }
}
