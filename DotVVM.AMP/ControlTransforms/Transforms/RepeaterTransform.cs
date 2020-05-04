using DotVVM.AMP.AmpControls;
using DotVVM.AMP.AmpControls.Internal;
using DotVVM.AMP.Config;
using DotVVM.Framework.Controls;
using Microsoft.Extensions.Logging;

namespace DotVVM.AMP.ControlTransforms.Transforms
{
    public class RepeaterTransform : ControlReplacementTransformBase
    {
        public RepeaterTransform(DotvvmAmpConfiguration ampConfiguration, ILogger logger) : base(ampConfiguration, logger)
        {
        }

        public override bool CanTransform(DotvvmControl control)
        {
            return control is Repeater;
        }

        protected override DotvvmControl CreateReplacementControl(DotvvmControl control)
        {
            return new AmpRepeater(AmpConfiguration.ControlTransforms);
        }
    }

}