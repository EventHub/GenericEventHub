using GenericEventHub.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace GenericEventHub.Authorization
{
    public class AuthorizeEnumAttribute : AuthorizeAttribute
    {
        public IUserService _userService { get; set; }

        public AuthorizeEnumAttribute(params object[] roles)
        {
            if (roles.Any(r => r.GetType().BaseType != typeof(Enum)))
                throw new ArgumentException("roles");

            this.Roles = string.Join(",", roles.Select(r => Enum.GetName(r.GetType(), r)));
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var authorized = false;

            // Default to admin
            if (String.IsNullOrEmpty(Roles))
            {
                this.Roles = Role.Admin.ToString();
            }

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {

            }

            return authorized;
        }
    }
}