using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindMaze.Core.Application.Features.Command.User;
using MindMaze.Core.Application.Features.Command.User.Change;
using MindMaze.Core.Application.Features.Command.User.ResetPassword;
using MindMaze.Core.Application.Features.Command.User.Verify;
using MindMaze.Core.Application.Features.Query.MyFriendsQuery;
using MindMaze.Core.Application.Features.Query.User;
using MindMaze.Core.Application.Features.Query.User.GetUsersQueries;

namespace MindMazeProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseController
    {
        private readonly ISender _sender;

        public UserController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginUserQuery query)
        {
            return Ok(await _sender.Send(query));
        }

        [HttpPost]
        [Route("LoginGmail")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWithGmail([FromBody] LoginUserQuery query)
        {
            return Ok(await _sender.Send(query));
        }

        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {

            return Ok("Testing if Deploying is true");
        }

        [HttpPost]
        [Route("Create")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody]CreateUserCommand command)
        {
            return Ok(await _sender.Send(command));
        }

        [HttpPost]
        [Route("ChangePassWord")]
        public async Task<IActionResult> ChangePassWord([FromBody] ChangeUserPasswordCommand command)
        {
            command.UserId = User_ID;

            return Ok(await _sender.Send(command));
        }

        [HttpPost]
        [Route("ChangeUserInfo")]
        public async Task<IActionResult> ChangeUserInfo([FromBody]string username)
        {
            ChangeUserInfoCommand command = new ChangeUserInfoCommand(User_ID, username);

            return Ok(await _sender.Send(command));
        }

        [HttpPost]
        [Route("Verify")]
        [AllowAnonymous]
        public async Task<IActionResult> Verify([FromQuery]EmailVerifyCommand command)
        {  
            return Ok(await _sender.Send(command));
        }

        [HttpPost]
        [Route("ResetPassWord")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassWord([FromBody]ResetPassWordCommand command)
        {
            return Ok(await _sender.Send(command));
        }

        /*[HttpGet]
        [Route("Info")]
        public async Task<IActionResult> GetUserInfo(GetUserQuery query)
        {
            return Ok(await _sender.Send(query));
        }*/


        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetUsers()
        {
            GetUsersQuery query = new GetUsersQuery();
            return Ok(await _sender.Send(query));
        }

        [HttpGet]
        [Route("Friends")]
        public async Task<IActionResult> GetFriendsInfo()
        {
            GetFriendsInfoQuery query = new GetFriendsInfoQuery();
            query.User_ID = User_ID;
            return Ok(await _sender.Send(query));
        }

        [HttpGet]
        [Route("Username")]
        public async Task<IActionResult> GetUserInfowithUsername([FromQuery]GetUsersWithUsernameQuery query)
        {

            return Ok(await _sender.Send(query));
        }


        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> GetUserInfowithUsernameandToken([FromQuery]GetUserWithUserNameAndTokenIDQuery query)
        {
            return Ok(await _sender.Send(query));
        }

    }
}
