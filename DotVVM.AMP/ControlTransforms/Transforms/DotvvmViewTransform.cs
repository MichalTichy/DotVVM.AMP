using DotVVM.AMP.AmpControls;
using DotVVM.AMP.AmpControls.Internal;
using DotVVM.AMP.Config;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Controls.Infrastructure;
using DotVVM.Framework.Hosting;
using Microsoft.Extensions.Logging;

namespace DotVVM.AMP.ControlTransforms.Transforms
{
    public class DotvvmViewTransform : ControlReplacementTransformBase
    {
        public DotvvmViewTransform(DotvvmAmpConfiguration ampConfiguration, ILogger logger = null) : base(ampConfiguration, logger)
        {
        }

        protected override void TransferControlProperties(DotvvmControl source, DotvvmControl target)
        {
            base.TransferControlProperties(source, target);
            var dotvvmView = (DotvvmView)source;
            var ampView = (AmpView)target;

            ampView.ViewModelType = dotvvmView.ViewModelType;
        }

        public override bool CanTransform(DotvvmControl control)
        {
            return control is DotvvmView;
        }


        protected override DotvvmControl CreateReplacementControl(DotvvmControl control)
        {
            return new AmpView();
        }
    }
}