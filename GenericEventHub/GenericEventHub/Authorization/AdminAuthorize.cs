using Autofac.Integration.WebApi;
using GenericEventHub.Repositories;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace GenericEventHub.Authorization
{
    public class AdminAuthorize : AuthorizeAttribute, IAutofacAuthorizationFilter
    {
        public IUserRepository Users { get; set; }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var authorized = false;

            var currentUser = HttpContext.Current.User.Identity;

            if (currentUser.IsAuthenticated)
            {
                var user = Users.GetUserByWindowsName(currentUser.Name);

                if (user != null)
                {
                    authorized = user.IsAdmin;
                }
            }

            return authorized;
        }
    }
}