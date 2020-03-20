using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Routing;

namespace DotVVM.AMP.AmpControls
{
    public class AmpLinkToFullPage : HtmlGenericControl, IAmpControl
    {
        private readonly RouteBase fullPageRoute;

        public AmpLinkToFullPage(RouteBase fullPageRoute) : base("link")
        {
            this.fullPageRoute = fullPageRoute;
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.AddAttribute("rel", "canonical");
            writer.AddAttribute("href", fullPageRoute.BuildUrl(context.Parameters));
            base.AddAttributesToRender(writer, context);
        }
    }
}