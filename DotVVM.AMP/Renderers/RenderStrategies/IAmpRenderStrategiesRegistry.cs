using DotVVM.Framework.Controls;

namespace DotVVM.AMP.Renderers.RenderStrategies
{
    public interface IAmpRenderStrategiesRegistry
    {
        void Register(IRenderStrategy renderStrategy);
        IRenderStrategy GetRenderStrategy(DotvvmControl control);
    }
}