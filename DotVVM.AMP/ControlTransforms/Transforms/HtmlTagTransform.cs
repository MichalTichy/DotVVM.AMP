using DotVVM.AMP.AmpControls;
using DotVVM.Framework.Controls;

namespace DotVVM.AMP.ControlTransforms.Transforms
{
    public class HtmlTagTransform : ControlTransformBase
    {
        public override bool CanTransform(DotvvmControl control)
        {
            return control is HtmlGenericControl htmlGenericControl && htmlGenericControl.TagName == "html";
        }

        protected override DotvvmControl CreateReplacementControl(DotvvmControl control)
        {
            return new AmpHtmlTag();
        }
    }
}