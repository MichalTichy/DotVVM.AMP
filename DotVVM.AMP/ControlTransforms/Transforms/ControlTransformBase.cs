using DotVVM.AMP.Config;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using Microsoft.Extensions.Logging;

namespace DotVVM.AMP.ControlTransforms.Transforms
{
    public abstract class ControlTransformBase: IControlTransform
    {
        protected readonly DotvvmAmpConfiguration AmpConfiguration;
        protected ILogger Logger { get; set; }


        protected ControlTransformBase(DotvvmAmpConfiguration ampConfiguration, ILogger logger)
        {
            this.AmpConfiguration = ampConfiguration;
            this.Logger = logger;
        }


        public abstract bool CanTransform(DotvvmControl control);
        public abstract DotvvmControl Transform(DotvvmControl control, IDotvvmRequestContext context);
    }
}