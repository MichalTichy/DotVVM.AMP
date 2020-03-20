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
}