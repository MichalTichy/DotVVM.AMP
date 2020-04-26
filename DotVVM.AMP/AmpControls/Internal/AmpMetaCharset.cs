using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.AMP.AmpControls.Internal
{
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