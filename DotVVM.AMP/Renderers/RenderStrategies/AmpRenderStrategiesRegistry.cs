using System.Collections.Generic;
using DotVVM.Framework.Controls;

namespace DotVVM.AMP.Renderers.RenderStrategies
{
    public class AmpRenderStrategiesRegistry : IAmpRenderStrategiesRegistry
    {
        List<IRenderStrategy> renderStrategies = new List<IRenderStrategy>();

        public void Register(IRenderStrategy renderStrategy)
        {
            renderStrategies.Add(renderStrategy);
        }

        public IRenderStrategy GetRenderStrategy(DotvvmControl control)
        {
            //gets the last render strategy that can handle given control.
            for (int i = renderStrategies.Count - 1; i >= 0; i--)
            {
                if (renderStrategies[i].CanHandle(control))
                {
                    return renderStrategies[i];
                }
            }

            return null;
        }
    }
}