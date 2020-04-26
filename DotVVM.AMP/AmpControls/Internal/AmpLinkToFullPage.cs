using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Routing;

namespace DotVVM.AMP.AmpControls.Internal
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
            var urlWithDefault= ampPageRoute.BuildUrl(ampPageRoute.DefaultValues);
            var urlWithCurrent = ampPageRoute.BuildUrl(context.Parameters);
            var isDefault = urlWithDefault == urlWithCurrent;
            var urlFullPage= isDefault ?
                fullPageRoute.BuildUrl(fullPageRoute.DefaultValues) :
                fullPageRoute.BuildUrl(context.Parameters);
            writer.AddAttribute("href", urlFullPage);
            base.AddAttributesToRender(writer, context);
        }
    }
}