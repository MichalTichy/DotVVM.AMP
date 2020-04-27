using DotVVM.AMP.AmpControls.Decorators;
using DotVVM.AMP.Config;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using Microsoft.Extensions.Logging;

namespace DotVVM.AMP.ControlTransforms.Transforms
{
    public class AmpDecoratorTransform : ControlTransformBase
    {
        public AmpDecoratorTransform(DotvvmAmpConfiguration ampConfiguration, ILogger logger) : base(ampConfiguration, logger)
        {
        }

        public override bool CanTransform(DotvvmControl control)
        {
            return control is AmpDecoratorBase;
        }

        protected override DotvvmControl TransformCore(DotvvmControl control, IDotvvmRequestContext context)
        {
            if (control is AmpDecoratorBase decorator) decorator.Configuration = AmpConfiguration;
            return control;
        }
    }
}