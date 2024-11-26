using Kingonomy.Models;
using Microsoft.AspNetCore.Mvc;

namespace KingonomyService.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly IHttpContextAccessor? _httpContextAccessor;
        protected BaseController(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;
        public PlayerModel? Player => (PlayerModel)_httpContextAccessor?.HttpContext?.Items["user"]!;
    }
}
