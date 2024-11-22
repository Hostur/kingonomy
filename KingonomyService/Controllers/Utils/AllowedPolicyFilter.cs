using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace KingonomyService.Controllers
{
    public class AllowedPolicyFilter : IAsyncAuthorizationFilter
    {
        readonly IAuthorizationService _authorization;
        public string Policies { get; }

        public AllowedPolicyFilter(string policies, IAuthorizationService authorization)
        {
            Policies = policies;
            _authorization = authorization;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var authorized = await _authorization.AuthorizeAsync(context.HttpContext.User, context.HttpContext, policy);

            context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
        }
    }
}
