using DotVVM.AMP.ControlTransforms;
using DotVVM.AMP.Enums;
using DotVVM.AMP.Extensions;

namespace DotVVM.AMP.Config
{
    public class DotvvmAmpConfig
    {
        public DotvvmAmpConfig(IAmpControlTransformsRegistry registry)
        {
            ControlTransforms = registry;
        }

        public ErrorHandlingMode AttributeErrorHandlingMode { get; set; }
        public ErrorHandlingMode KnockoutErrorHandlingMode { get; set; }

        public IAmpControlTransformsRegistry ControlTransforms { get; set; }

    }
}