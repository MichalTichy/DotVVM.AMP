using DotVVM.Framework.Controls.Infrastructure;
using DotVVM.Framework.Hosting;

namespace DotVVM.AMP.AmpControls.Internal
{
    public class AmpView : DotvvmView, IAmpControl
    {

        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            //not calling OnPreRender of DotVVM will result in absence of required resources.

            context.ResourceManager.AddRequiredResource("amp-boilerplate-css");
            context.ResourceManager.AddRequiredResource("amp-boilerplate-js");
        }
    }
}