using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindMaze.Core.Application.Features.Query.GamesQuery;
using MindMaze.Core.Application.Interfaces;

namespace MindMazeProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class Games : BaseController
    {

        private readonly ISender _sender;

        public Games(ISender sender)
        {
            this._sender = sender;
        }


        [HttpPost]
        [Route("/GetGames")]
        public async Task<IActionResult> GetGames([FromBody] GetMyGamesQuery query)
        {

            query.User_ID = User_ID;
            return Ok(await _sender.Send(query));
        }
    }
}
