using DotVVM.AMP.AmpControls;
using DotVVM.AMP.AmpControls.Internal;
using DotVVM.AMP.Config;
using DotVVM.Framework.Controls;
using Microsoft.Extensions.Logging;

namespace DotVVM.AMP.ControlTransforms.Transforms
{
    public class BodyTagTransform : ControlReplacementTransformBase
    {
        public BodyTagTransform(DotvvmAmpConfiguration ampConfiguration, ILogger logger = null) : base(ampConfiguration, logger)
        {
        }

        public override bool CanTransform(DotvvmControl control)
        {
            return control is HtmlGenericControl generic && generic.TagName == "body";
        }

        protected override DotvvmControl CreateReplacementControl(DotvvmControl control)
        {
            return new AmpBody();
        }
    }
}