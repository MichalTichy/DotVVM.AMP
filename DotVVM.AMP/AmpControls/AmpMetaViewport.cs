using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.AMP.Config;
using DotVVM.AMP.Enums;
using DotVVM.AMP.Validator;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using Microsoft.Extensions.Logging;

namespace DotVVM.AMP.AmpControls
{
    public class AmpMetaViewport : HtmlGenericControl, IAmpControl
    {
        private readonly DotvvmAmpConfiguration ampConfiguration;
        private readonly ILogger logger;
        public Dictionary<string, string> ViewPortProperties = new Dictionary<string, string>();
        public AmpMetaViewport(DotvvmAmpConfiguration ampConfiguration, ILogger logger = null) : base("meta")
        {
            this.ampConfiguration = ampConfiguration;
            this.logger = logger;
        }


        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            AddRequiredProperties();
            base.AddAttributesToRender(writer, context);
            writer.AddAttribute("name", "viewport");
            var props = ViewPortProperties.Select(t => $"{t.Key}:{t.Value}");
            writer.AddAttribute("content", string.Join(",", props), true);
        }

        private void AddRequiredProperties()
        {
            var requiredWidth = "device-width";
            if (ViewPortProperties.TryGetValue("width", out var width) && width != requiredWidth)
            {
                switch (ampConfiguration.AttributeErrorHandlingMode)
                {
                    case ErrorHandlingMode.Throw:
                        throw new AmpException($"Meta viewport must have width={requiredWidth}");
                    case ErrorHandlingMode.LogAndIgnore:
                        logger?.LogWarning($@"Meta viewport must have width={requiredWidth}. Current value {width} replaced by required value.");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            if (width == null)
                ViewPortProperties.Add("width", requiredWidth);
            else
                ViewPortProperties["width"] = requiredWidth;


            var requiredMinScale = "1";
            if (ViewPortProperties.TryGetValue("minimum-scale", out var minScale) && minScale != requiredMinScale)
            {
                switch (ampConfiguration.AttributeErrorHandlingMode)
                {
                    case ErrorHandlingMode.Throw:
                        throw new AmpException($"Meta viewport must have minimum-scale={requiredMinScale}");
                    case ErrorHandlingMode.LogAndIgnore:
                        logger?.LogWarning($@"Meta viewport must have minimum-scale={requiredMinScale}. Current value {minScale} replaced by required value.");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            if (minScale == null)
                ViewPortProperties.Add("minimum-scale", requiredMinScale);
            else
                ViewPortProperties["minimum-scale"] = requiredMinScale;
        }
    }
}