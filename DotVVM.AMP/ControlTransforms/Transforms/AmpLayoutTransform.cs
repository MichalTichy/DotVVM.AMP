using DotVVM.AMP.AmpControls;
using DotVVM.AMP.Config;
using DotVVM.AMP.Enums;
using DotVVM.Framework.Controls;
using Microsoft.Extensions.Logging;

namespace DotVVM.AMP.ControlTransforms.Transforms
{
    public class AmpLayoutTransform: ControlReplacementTransformBase
    {
        public AmpLayoutTransform(DotvvmAmpConfiguration ampConfiguration, ILogger logger = null) : base(ampConfiguration, logger)
        {
        }

        public override bool CanTransform(DotvvmControl control)
        {
            return control is HtmlGenericControl genericControl && genericControl.TagName=="div" && control.IsPropertySet(Amp.LayoutProperty);
        }

        protected override DotvvmControl CreateReplacementControl(DotvvmControl control)
        {
            var replacementControl = new Layout
            {
                Layout = control.GetValue<AmpLayout>(Amp.LayoutProperty)
            };
            return replacementControl;
        }
    }
}