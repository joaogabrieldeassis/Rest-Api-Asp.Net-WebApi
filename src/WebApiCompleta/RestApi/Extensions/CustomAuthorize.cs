using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;
using System.Security.Claims;

namespace RestApi.Extensions
{
    public class CustomAuthorize
    {
        public static bool ValidarClaimsUusuario(HttpContext httpcontext,string claimName,string claimValue)
        {
            return httpcontext.User.Identity.IsAuthenticated &&
                httpcontext.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
        }
    }
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue)
            : base(typeof(RequisitoClaimFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }
    public class RequisitoClaimFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;
        public RequisitoClaimFilter(Claim claim)
        {
            _claim = claim;
        }
    
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new StatusCodeResult(400);
                return;
            }
            if (!CustomAuthorize.ValidarClaimsUusuario(context.HttpContext,_claim.Type,_claim.Value))
            {
                context.Result = new StatusCodeResult(403);
            }
        }
    }
}
