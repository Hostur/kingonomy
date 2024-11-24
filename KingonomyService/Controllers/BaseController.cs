using Kingonomy.Models;
using Microsoft.AspNetCore.Mvc;

namespace KingonomyService.Controllers
{
    public abstract class BaseController : Controller
    {
        public PlayerModel Player { get; private set; }
    }
}
