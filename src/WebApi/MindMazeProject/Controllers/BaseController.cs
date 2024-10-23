using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MindMazeProject.Controllers
{


    public class BaseController : ControllerBase
    {

        protected Guid User_ID => Guid.Parse((HttpContext?.User.Identities?.First().Claims?
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value));

    }
}
