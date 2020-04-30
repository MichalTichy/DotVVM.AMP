using System.Linq;
using DotVVM.AMP.AmpControls;
using DotVVM.AMP.Config;
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

        protected override void AfterTransform(DotvvmControl finalControl, IDotvvmRequestContext context)
        {
            var image = finalControl as Image;
            var doesHaveHeightOrWidthSet = image != null && (
                (image.Attributes.ContainsKey("height") && !string.IsNullOrWhiteSpace((string)image.Attributes["height"]) || image.IsPropertySet(AmpControl.HeightProperty) && !string.IsNullOrWhiteSpace(image.GetValue<string>(AmpControl.HeightProperty))) ||
                (image.Attributes.ContainsKey("width") && !string.IsNullOrWhiteSpace((string)image.Attributes["width"]) || image.IsPropertySet(AmpControl.WidthProperty) && !string.IsNullOrWhiteSpace(image.GetValue<string>(AmpControl.WidthProperty))));
            if (!doesHaveHeightOrWidthSet)
            {
                if (AmpConfiguration.TryToDetermineExternalResourceDimensions)
                {
                    var src = GetSrc(finalControl);
                    var metadata = AmpConfiguration.AmpExternalResourceMetadataCache.GetResourceMetadata(src, context.HttpContext);
                    image.Height = $"{metadata.Height}px";
                    image.Width = $"{metadata.Width}px";
                }
                else
                {
                    throw new AmpException($"Image must have dimensions specified when the settings {AmpConfiguration.TryToDetermineExternalResourceDimensions} is set to false!");
                }
            }

            base.AfterTransform(finalControl, context);
        }

        private static string GetSrc(DotvvmControl finalControl)
        {
            string GetSrcFromSrcset(string srcset)
            {
                return (srcset?.Trim() ?? string.Empty).Split(',').First().Trim().Split(' ').First();
            }

            string src = null;

            if (finalControl is HtmlGenericControl genericControl &&
                genericControl.Attributes.ContainsKey("src"))
            {
                src = (string) genericControl.Attributes["src"];
            }

            if (string.IsNullOrWhiteSpace(src) && finalControl is HtmlGenericControl genericControl2 &&
                genericControl2.Attributes.ContainsKey("srcset"))
            {
                src = GetSrcFromSrcset((string) genericControl2.Attributes["srcset"]);
            }

            if (string.IsNullOrWhiteSpace(src))
            {
                throw new AmpException("Unable to get source of image!");
            }

            return src;
        }
    }
}