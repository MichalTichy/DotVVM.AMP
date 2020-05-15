using System.Linq;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Routing;

namespace DotVVM.AMP.AmpControls.Internal
{
    public class AmpLinkToAnotherVersion : HtmlGenericControl, IAmpControl
    {
        private readonly RouteBase ampPageRoute;
        private readonly RouteBase fullPageRoute;
        private readonly bool targetIsAmp;

        public AmpLinkToAnotherVersion(RouteBase ampPageRoute, RouteBase fullPageRoute, bool targetIsAmp=false) : base("link")
        {
            this.ampPageRoute = ampPageRoute;
            this.fullPageRoute = fullPageRoute;
            this.targetIsAmp = targetIsAmp;
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.AddAttribute("rel", targetIsAmp ? "amphtml" :"canonical");
            var ampPageHasDefaults = ampPageRoute.DefaultValues.Any();
            var fullPageHasDefaults = fullPageRoute.DefaultValues.Any();
            bool isDefault;

            if (!fullPageHasDefaults)
            {
                isDefault = !context.Parameters.Any();
            }
            else
            {
                var urlWithDefault = ampPageRoute.BuildUrl(ampPageRoute.DefaultValues);
                var urlWithCurrent = ampPageRoute.BuildUrl(context.Parameters);
                isDefault = urlWithDefault == urlWithCurrent;

            }
            string pageUrl;

            if (targetIsAmp)
            {

                pageUrl = isDefault ?
                    ampPageRoute.BuildUrl(ampPageRoute.DefaultValues) :
                    ampPageRoute.BuildUrl(context.Parameters);
            }
            else
            {

                pageUrl = isDefault ?
                    fullPageRoute.BuildUrl(fullPageRoute.DefaultValues) :
                    fullPageRoute.BuildUrl(context.Parameters);
            }

            writer.AddAttribute("href", pageUrl);
            base.AddAttributesToRender(writer, context);
        }
    }
}