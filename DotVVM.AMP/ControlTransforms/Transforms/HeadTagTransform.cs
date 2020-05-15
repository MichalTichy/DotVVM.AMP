using System.Linq;
using DotVVM.AMP.AmpControls;
using DotVVM.AMP.AmpControls.Internal;
using DotVVM.AMP.Config;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using Microsoft.Extensions.Logging;

namespace DotVVM.AMP.ControlTransforms.Transforms
{
    public class HeadTagTransform : ControlTransformBase
    {
        public override bool CanTransform(DotvvmControl control)
        {
            return control is HtmlGenericControl htmlGenericControl && htmlGenericControl.TagName == "head";
        }

        protected override DotvvmControl TransformCore(DotvvmControl control, IDotvvmRequestContext context)
        {
            AddMetaCharset(control, context);
            AddLinkToOriginalPage(control, context);
            AddMetaViewPort(control, context);
            return control;
        }

        private void AddMetaViewPort(DotvvmControl newControl, IDotvvmRequestContext context)
        {
            var mataViewport = new AmpMetaViewport(AmpConfiguration);
            newControl.Children.Add(mataViewport);

            var originalMetaViewport = (HtmlGenericControl)newControl.Children
                .FirstOrDefault(t => t is HtmlGenericControl generic &&
                                     generic.TagName == "meta" &&
                                     generic.Attributes.Any(p =>
                                         p.Key.ToLower() == "name" &&
                                         p.Value.ToString().ToLower() == "viewport"));

            if (originalMetaViewport == null || !originalMetaViewport.Attributes.ContainsKey("content")) return;

            foreach (var prop in originalMetaViewport.Attributes["content"].ToString().Split(','))
            {
                var parts = prop.Split('=');
                mataViewport.ViewPortProperties.Add(parts[0], parts[1]);
            }

            RemoveControlFromTree(originalMetaViewport);
        }

        private void AddLinkToOriginalPage(DotvvmControl newControl, IDotvvmRequestContext context)
        {
            newControl.Children.Add(new AmpLinkToAnotherVersion(context.Route,AmpConfiguration.RouteManager.GetFullPageRoute(context.Route)));
        }

        private void AddMetaCharset(DotvvmControl control, IDotvvmRequestContext context)
        {
            RemoveMetaCharsetIfPresent(control);
            control.Children.Insert(0, new AmpMetaCharset());
        }

        private void RemoveMetaCharsetIfPresent(DotvvmControl control)
        {
            var metaCharsets = control.Children.Where(t => t is HtmlGenericControl genericControl &&
                                                           genericControl.TagName == "meta" &&
                                                           genericControl.Attributes.Any(p => p.Key.ToLower() == "charset")).ToList();

            foreach (var metaCharset in metaCharsets)
                control.Children.Remove(metaCharset);
        }

        public HeadTagTransform(DotvvmAmpConfiguration ampConfiguration, ILogger logger = null) : base(ampConfiguration, logger)
        {
        }
    }
}