using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.AMP.Renderers.RenderStrategies
{
    public abstract class RenderStrategyBase : IRenderStrategy
    {
        public abstract bool CanHandle(DotvvmControl control);

        public abstract void Render(DotvvmControl control, IHtmlWriter htmlWriter, IDotvvmRequestContext context);
    }
}