using Ninject;
using Ninject.Modules;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using UltiSports.Infrastructure;
using UltiSports.Models;
using Ninject.Web.Mvc.FilterBindingSyntax;

namespace UltiSports.Filters
{
    public class AdminAuthentication : ActionFilterAttribute, IActionFilter
    {
        [Inject]
        public IActivityRepository ActivityDb { get; set; }
        [Inject]
        public IPlayerRepository PlayerDb { get; set; }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            var currentUser = PlayerDb.GetPlayerByUsername(filterContext.HttpContext.User.Identity.Name);
            #region Adding this for debuggin purposes, must remove after admin selection feature is finished
            currentUser.SportsManaged.Add(ActivityDb.GetActivityByName("Football"));
            #endregion
            if (currentUser.SportsManaged.Count == 0)
            {
                filterContext.HttpContext.Response.Redirect("~/Event/Index/?message=Only%20admins%20can%20do%20this!");
            }
        }
    }
}