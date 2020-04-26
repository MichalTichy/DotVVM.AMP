using DotVVM.AMP.Config;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using Microsoft.Extensions.Logging;

namespace DotVVM.AMP.ControlTransforms.Transforms
{
    public class AllControlTransform : ControlTransformBase
    {
        public AllControlTransform(DotvvmAmpConfiguration ampConfiguration, ILogger logger) : base(ampConfiguration, logger)
        {
        }

        public override bool CanTransform(DotvvmControl control)
        {
            return true;
        }

        protected override DotvvmControl TransformCore(DotvvmControl control, IDotvvmRequestContext context)
        {
            return control;
        }
    }
}