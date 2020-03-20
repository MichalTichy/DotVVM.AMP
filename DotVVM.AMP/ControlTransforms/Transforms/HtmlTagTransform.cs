using System;
using System.Linq;
using DotVVM.AMP.AmpControls;
using DotVVM.AMP.Routing;
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

    public class HeadTagTransform : ControlTransformBase
    {
        private readonly IAmpRouteManager ampRouteManager;

        public HeadTagTransform(IAmpRouteManager ampRouteManager)
        {
            this.ampRouteManager = ampRouteManager;
        }
        public override bool CanTransform(DotvvmControl control)
        {
            return control is HtmlGenericControl htmlGenericControl && htmlGenericControl.TagName == "head";
        }

        public override void Transform(DotvvmControl control)
        {
            RemoveMetaCharsetIfPresent(control);
            base.Transform(control);
        }

        private void RemoveMetaCharsetIfPresent(DotvvmControl control)
        {
            var metaCharsets = control.Children.Where(t => t is HtmlGenericControl genericControl &&
                                                          genericControl.TagName == "meta" &&
                                                          genericControl.Properties.Any(p => p.Key.Name.ToLower() == "charset"));

            foreach (var metaCharset in metaCharsets)
                control.Children.Remove(metaCharset);
        }

        protected override DotvvmControl CreateReplacementControl(DotvvmControl control)
        {
            return new AmpHead(ampRouteManager);
        }
    }
}