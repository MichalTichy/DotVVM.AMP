using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.AMP.AmpControls.Decorators
{
    public class Include : AmpDecoratorBase
    {
        protected override void RenderContents(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            if (IsInAmpPage)
            {
                base.RenderContents(writer, context);
            }
        }
    }
}