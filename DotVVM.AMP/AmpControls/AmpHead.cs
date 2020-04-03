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

        protected override void OnInit(IDotvvmRequestContext context)
        {
            base.OnInit(context);
        }
    }
}