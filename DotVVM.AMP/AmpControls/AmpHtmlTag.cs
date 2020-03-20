using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

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
        public AmpHead() : base("head")
        {

        }

        protected override void OnInit(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("amp-boilerplate-css");
            context.ResourceManager.AddRequiredResource("amp-boilerplate-js");

            Children.Add(new AmpMetaCharset());

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