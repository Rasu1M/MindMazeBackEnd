using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindMaze.Core.Application.Features.Query.QuestionsQuery;

namespace MindMazeProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class Questions : BaseController
    {

        private readonly ISender _sender;

        public Questions(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetQuestions([FromQuery]GetQuestionsQuery query)
        {
            return Ok(await _sender.Send(query));

        }

    }
}
