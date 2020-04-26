using System.Linq;
using DotVVM.AMP.Config;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Controls.Infrastructure;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ResourceManagement;
using Microsoft.Extensions.Logging;

namespace DotVVM.AMP.ControlTransforms.Transforms
{
    public class InlineStyleTransform : ControlTransformBase
    {
        public InlineStyleTransform(DotvvmAmpConfiguration ampConfiguration, ILogger logger) : base(ampConfiguration, logger)
        {
        }

        public override bool CanTransform(DotvvmControl control)
        {
            return control is HtmlGenericControl genericControl && genericControl.TagName == "style" && genericControl.Children.SingleOrDefault(t=>t is RawLiteral) !=null;
        }

        protected override DotvvmControl TransformCore(DotvvmControl control, IDotvvmRequestContext context)
        {
            RemoveControlFromParent(control);

            var styleText = control.Children.OfType<RawLiteral>().Single().UnencodedText;
            var inlineResource = new InlineStylesheetResource(styleText);
            context.ResourceManager.AddRequiredResource(inlineResource);
            return null;
        }

        private static void RemoveControlFromParent(DotvvmControl control)
        {
            var parent = control.Parent as DotvvmControl;
            parent.Children.Remove(control);
        }
    }
}