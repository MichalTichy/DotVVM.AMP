using DotVVM.AMP.Enums;
using DotVVM.AMP.Renderers.RenderStrategies;

namespace DotVVM.AMP.Config
{
    public class DotvvmAmpConfig
    {
        public DotvvmAmpConfig(IAmpRenderStrategiesRegistry renderStrategiesRegistry)
        {
            RenderStrategiesRegistry = renderStrategiesRegistry;
        }

        public ErrorHandlingMode ErrorHandlingMode { get; set; }
        public IAmpRenderStrategiesRegistry RenderStrategiesRegistry { get; set; }
    }
}