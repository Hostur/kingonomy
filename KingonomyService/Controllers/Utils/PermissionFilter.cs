using Kingonomy;
using Microsoft.AspNetCore.Mvc.Filters;
using KingonomyService.Services;

namespace KingonomyService.Controllers
{
    public class PermissionFilter : Attribute, IAsyncAuthorizationFilter
    {
        private UserService? _userService;
        private readonly Role[] _requiredRole;
        public PermissionFilter(params Role[] requiredRole)
        {
            _requiredRole = requiredRole;
        }

        public PermissionFilter()
        {
            _requiredRole = new Role[] { Role.Player, Role.Admin, Role.Server };
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            _userService = context.HttpContext.RequestServices.GetService<UserService>();

            //var authorized = await _authorization.AuthorizeAsync(context.HttpContext.User, context.HttpContext, policy);

            //context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
        }
    }
}
