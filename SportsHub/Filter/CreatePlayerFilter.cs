using System.Web.Http.Filters;

namespace SportsHub.Filter
{
    public class CreatePlayerFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //TODO: This should handle the registration of the player.
        }
    }
}