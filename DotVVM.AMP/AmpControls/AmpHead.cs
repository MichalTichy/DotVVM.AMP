using DotVVM.AMP.Routing;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.AMP.AmpControls
{
    public class AmpHead : HtmlGenericControl, IAmpControl
    {

        public AmpHead() : base("head")
        {
        }

        protected override void RenderEndTag(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            base.RenderEndTag(writer, context);
        }
    }
}