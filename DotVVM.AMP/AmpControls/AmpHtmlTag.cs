using DotVVM.AMP.Routing;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Routing;

namespace DotVVM.AMP.AmpControls
{
    public class AmpHtmlTag : HtmlGenericControl, IAmpControl
    {
        public AmpHtmlTag() : base("html")
        {

        }
        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.AddAttribute("amp", null);
            base.AddAttributesToRender(writer, context);
        }
    }

    public class AmpHead : HtmlGenericControl, IAmpControl
    {
        private readonly IAmpRouteManager ampRouteManager;

        public AmpHead(IAmpRouteManager ampRouteManager) : base("head")
        {
            this.ampRouteManager = ampRouteManager;
        }

        protected override void OnInit(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("amp-boilerplate-css");
            context.ResourceManager.AddRequiredResource("amp-boilerplate-js");

            Children.Insert(0, new AmpMetaCharset());

            var fullPageRoute = ampRouteManager.GetFullPageRoute(context.Route);
            Children.Add(new AmpLinkToFullPage(fullPageRoute));

            base.OnInit(context);
        }
    }

    public class AmpMetaCharset : HtmlGenericControl, IAmpControl
    {
        public AmpMetaCharset() : base("meta")
        {

        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.AddAttribute("charset", "utf-8");
            base.AddAttributesToRender(writer, context);
        }
    }
}