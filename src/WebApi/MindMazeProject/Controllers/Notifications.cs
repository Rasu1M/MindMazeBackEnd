using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindMaze.Core.Application.Features.Command.MyFriendsCommands;
using MindMaze.Core.Application.Features.Command.NotificationsCommands;
using MindMaze.Core.Application.Features.Query.NotificationsQuery;

namespace MindMazeProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class Notifications : BaseController
    {

        private readonly ISender _sender;

        public Notifications(ISender sender)
        {
            this._sender = sender;
        }

        [HttpGet]
        [Route("Notifications")]
        public async Task<IActionResult> GetNotifications()
        {
            return Ok(await _sender.Send(new GetNotificationsQuery(User_ID)));
        }

        [HttpGet]
        [Route("friendnotif")]
        public async Task<IActionResult> AddNotification([FromQuery] AddNotificationCommand command)
        {
            command.User_ID = User_ID;
            return Ok(await _sender.Send(command));
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddFriend([FromBody] AddFriendCommand command)
        {
            command.User_ID = User_ID;
            return Ok(await _sender.Send(command));
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> DeleteFriend([FromBody]DeleteFriendCommand command)
        {
            command.User_ID = User_ID;
            return Ok(await _sender.Send(command));
        }



    }
}
