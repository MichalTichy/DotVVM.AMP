using System.Collections.Generic;
using System.Linq;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Routing;

namespace DotVVM.AMP.AmpControls
{
    public class AmpLinkToFullPage : HtmlGenericControl, IAmpControl
    {
        private readonly RouteBase ampPageRoute;
        private readonly RouteBase fullPageRoute;

        public AmpLinkToFullPage(RouteBase ampPageRoute, RouteBase fullPageRoute) : base("link")
        {
            this.ampPageRoute = ampPageRoute;
            this.fullPageRoute = fullPageRoute;
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.AddAttribute("rel", "canonical");
            var parameters = GeRouteParameters(context);
            writer.AddAttribute("href", fullPageRoute.BuildUrl(parameters));
            base.AddAttributesToRender(writer, context);
        }

        private IDictionary<string, object> GeRouteParameters(IDotvvmRequestContext context)
        {
            var isDefault = context.Parameters.Count == ampPageRoute.DefaultValues.Count;
            if (isDefault)
            { 
                if (context.Parameters.Any(parameter => !ampPageRoute.DefaultValues.Contains(parameter)))
                {
                    isDefault = false;
                }
            }
            return isDefault? fullPageRoute.DefaultValues : context.Parameters;
        }
    }
}