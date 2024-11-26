using System.Net.Http.Headers;
using Kingonomy;
using Microsoft.AspNetCore.Mvc.Filters;
using KingonomyService.Services;
using Microsoft.AspNetCore.Mvc;

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
            try
            {
                _userService = context.HttpContext.RequestServices.GetService<UserService>();

                var header = context.HttpContext.Request.Headers["Authorization"];
                AuthenticationHeaderValue headerValue = AuthenticationHeaderValue.Parse(header);
                if (headerValue.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) && headerValue.Parameter != null)
                {
                    string token = headerValue.Parameter;
                    var user = await _userService.GetPlayer(token).ConfigureAwait(false);
                    if (user == null)
                    {
                        context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
                        return;
                    }
                    else
                    {
                        if (!_requiredRole.Contains(user.Role))
                        {
                            context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                            return;
                        }

                        context.HttpContext.Items["user"] = user;
                        return;
                    }
                }

                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            }
            catch
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
                return;
            }
        }
    }
}
