using System.Web.Mvc;

namespace SportsHub.HelperMethods
{    
    public static class CustomHtmlHelpers
    {
        /// <summary>
        /// This is a custom HTML helper created by Jota to stay within ASP.NET best practices when implementing action links as buttons.
        /// </summary>
        /// <param name ="htmlClass">By default this project uses this with Twitter's Bootstratp, and this is intended to select the button type.</param>
        /// <param name ="action">The action to call in a controller</param>
        /// <param name ="controller">The controller in which the action will be called.</param>
        /// <param name ="id">This is an id element that we can use to locate the event in the Db that is going to have the current active player added.</param>
        /// <param name ="textToDisplay">The text displayed by the action link</param>
        /// <param name ="elementStatus">This element status controlls whether or not the element is grayed out for a user. This is a default parameter set to Enabled, so if no value is passed an element will be enabled. It expects "Disabled" as a value, other values may produce unexpected behavior.</param>
        public static MvcHtmlString BootstrapCustomizableButtonActionLink(this HtmlHelper helper, string htmlClass, string action, string controller, int id, string textToDisplay, string elementStatus = "Enabled")
        {
            return new MvcHtmlString(string.Format(
                @"<a  class=""{0}"" href=""{1}/{2}/{3}"" {4} >{5}</a>",
                htmlClass,
                controller,
                action,
                id,
                elementStatus,
                textToDisplay
            ));
        }

        
        public static MvcHtmlString BootstrapRemovePlusOneButton(this HtmlHelper helper, int modelId, int plusOneId)
        {
            return new MvcHtmlString(string.Format(
                @"<a type=""button"" class=""close remove-plus-one"" href=""Attendance/RemovePlusOne/{0}?plusOneId={1}"">&times;</a>",
                modelId,
                plusOneId
            ));
        }
    }
}