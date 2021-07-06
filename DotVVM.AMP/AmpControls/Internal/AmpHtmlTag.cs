using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.AMP.AmpControls.Internal
{
    public class AmpHtmlTag : HtmlGenericControl, IAmpControl
    {
        public AmpHtmlTag() : base("html")
        {

        }

        protected override void OnInit(IDotvvmRequestContext context)
        {
            if (Attributes.ContainsKey("xmlns"))
            {
                Attributes.Remove("xmlns");
            }

            base.OnInit(context);
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.AddAttribute("amp", null);
            base.AddAttributesToRender(writer, context);
        }
    }
}