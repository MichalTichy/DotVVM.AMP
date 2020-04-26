using System;
using System.Linq;
using DotVVM.AMP.Config;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using Microsoft.Extensions.Logging;

namespace DotVVM.AMP.ControlTransforms.Transforms
{
    public class StylesheetLinkTransform : ControlTransformBase
    {
        public StylesheetLinkTransform(DotvvmAmpConfiguration ampConfiguration, ILogger logger) : base(ampConfiguration, logger)
        {
        }

        public override bool CanTransform(DotvvmControl control)
        {
            return control is HtmlGenericControl genericControl &&
                   genericControl.TagName == "link" &&
                   genericControl.Attributes.Any(a => a.Key == "rel" && (string) a.Value == "stylesheet") && genericControl.Attributes.Any(a=>a.Key=="href" && a.Value is string);
        }

        protected override DotvvmControl TransformCore(DotvvmControl control, IDotvvmRequestContext context)
        {
            var genericControl = control as HtmlGenericControl;
            RemoveControlFromParent(control);
            string href = (string)genericControl.Attributes["href"];
            context.ResourceManager.AddRequiredStylesheetFile(href,href);
            return null;
        }

        private static void RemoveControlFromParent(DotvvmControl control)
        {
            var parent = control.Parent as DotvvmControl;
            parent.Children.Remove(control);
        }
    }
}