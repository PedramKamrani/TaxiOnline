using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Snap.Core.Interface;

namespace Snap.Core.Securities
{
    public class RoleAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private string _roleName;
        private IAccountService? _accountService;
        public RoleAttribute(string roleName)
        {
            _roleName = roleName;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string userName = context.HttpContext.User.Identity?.Name ?? "";
            _accountService = (IAccountService)context.HttpContext.RequestServices.GetService(typeof(IAccountService))!;
            if (context.HttpContext.User.Identity != null && context.HttpContext.User.Identity.IsAuthenticated)
            {
                if (_accountService != null && !_accountService.CheckUserRole(_roleName, userName).Result)
                {
                    context.Result = new RedirectResult("/Account/Register");
                }
            }
            else
            {
                context.Result = new RedirectResult("/Account/Register");
            }
        }
    }
}
