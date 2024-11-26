using Kingonomy.Models;
using KingonomyService.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KingonomyService.Controllers
{
    [Route("api/player")]
    [ApiController]
    public sealed class PlayerController : BaseController
    {
        private readonly UserService _userService;

        public PlayerController(UserService userService, IHttpContextAccessor httpContextAccessor) : base(
            httpContextAccessor)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("authorize")]
        [ProducesResponseType(typeof(PlayerModel), 200)]
        [ProducesResponseType( 500)]
        public async Task<ActionResult<PlayerModel?>> Authorize(UnityAuthorizationModel authorizationModel)
        {
            try
            {
                var user = await _userService.Authorize(authorizationModel).ConfigureAwait(false);
                if (user == null)
                {
                    return StatusCode(404);
                }

                return Content(JsonConvert.SerializeObject(user));
            }
            catch
            {
                return StatusCode(500);
            }
        }


    }
}
