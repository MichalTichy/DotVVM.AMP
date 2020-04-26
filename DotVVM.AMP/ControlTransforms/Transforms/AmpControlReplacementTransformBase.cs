using DotVVM.AMP.AmpControls;
using DotVVM.AMP.Config;
using DotVVM.AMP.Enums;
using DotVVM.Framework.Controls;
using Microsoft.Extensions.Logging;

namespace DotVVM.AMP.ControlTransforms.Transforms
{
    public abstract class AmpControlReplacementTransformBase : ControlReplacementTransformBase
    {
        protected AmpControlReplacementTransformBase(DotvvmAmpConfiguration ampConfiguration, ILogger logger = null) : base(ampConfiguration, logger)
        {
        }

        protected override void TransferControlProperties(DotvvmControl source, DotvvmControl target)
        {
            if (source.IsPropertySet(Amp.LayoutProperty))
            {
                target.SetValueRaw(AmpControl.LayoutProperty, source.GetValue<AmpLayout>(Amp.LayoutProperty));
            }

            if (source.IsPropertySet(Amp.WidthProperty))
            {
                target.SetValueRaw(AmpControl.WidthProperty, source.GetValue<string>(Amp.WidthProperty));
            }

            if (source.IsPropertySet(Amp.HeightProperty))
            {
                target.SetValueRaw(AmpControl.HeightProperty, source.GetValue<string>(Amp.HeightProperty));
            }

            base.TransferControlProperties(source, target);
        }
    }
}