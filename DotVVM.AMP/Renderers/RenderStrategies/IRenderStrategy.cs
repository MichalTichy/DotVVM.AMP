using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.AMP.Renderers.RenderStrategies
{
    public interface IRenderStrategy
    {
        bool CanHandle(DotvvmControl control);
        void Render(DotvvmControl control, IHtmlWriter htmlWriter, IDotvvmRequestContext context);
    }
}