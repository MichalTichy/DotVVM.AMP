using System.Linq;
using System.Text;
using DotVVM.AMP.AmpControls;
using DotVVM.AMP.Config;
using DotVVM.AMP.Extensions;
using DotVVM.AMP.Validator;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace DotVVM.AMP.ControlTransforms.Transforms
{
    public class AmpImageTransform : AmpControlReplacementTransformBase
    {
        public AmpImageTransform(DotvvmAmpConfiguration ampConfiguration, ILogger logger = null) : base(ampConfiguration, logger)
        {
        }

        public override bool CanTransform(DotvvmControl control)
        {
            return control is HtmlGenericControl genericControl && genericControl.TagName == "img";
        }
        
        protected override DotvvmControl CreateReplacementControl(DotvvmControl control)
        {
            return new Image();
        }
    }
}