using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.AMP.Enums;
using DotVVM.AMP.Extensions;
using DotVVM.AMP.Validator;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.AMP.AmpControls
{
    [ContainsDotvvmProperties]
    public class Image : AmpControl
    {
        public Image() : base("amp-img")
        {

        }

        public string Src
        {
            get { return GetPropertyValueOrAttribute(SrcProperty, "src"); }
            set { SetValue(SrcProperty, value); Attributes.Remove("src"); }
        }

        public static readonly DotvvmProperty SrcProperty
            = DotvvmProperty.Register<string, Image>(c => c.Src, null);


        public string SrcSet
        {
            get { return GetPropertyValueOrAttribute(SrcSetProperty, "srcset"); }
            set { SetValue(SrcSetProperty, value); Attributes.Remove("srcset"); }
        }

        public static readonly DotvvmProperty SrcSetProperty
            = DotvvmProperty.Register<string, Image>(c => c.SrcSet, null);

        public string Alt
        {
            get { return GetPropertyValueOrAttribute(AltProperty, "alt"); }
            set { SetValue(AltProperty, value); Attributes.Remove("alt"); }
        }

        public static readonly DotvvmProperty AltProperty
            = DotvvmProperty.Register<string, Image>(c => c.Alt, null);
        
        [AttachedProperty(typeof(string))]
        public static readonly DotvvmProperty   AttributionProperty
            = DotvvmProperty.Register<string, Image>(() => AttributionProperty, null);

        protected override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Src) && string.IsNullOrWhiteSpace(SrcSet))
                throw new DotvvmControlException($"{nameof(Src)} and / or {nameof(SrcSet)} property must be set!");
            base.Validate();
        }

        protected override IEnumerable<AmpLayout> SupportedLayouts => new []{AmpLayout.fill,AmpLayout.fixedLayout,AmpLayout.fixedHeight,AmpLayout.flexItem,AmpLayout.intrinsic,AmpLayout.noDisplay,AmpLayout.responsive};

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {

            EnsureThatUrlsAreAbsolute(context.HttpContext);
            EnsureThatImageHasDimensions(context);

            AddAttributeIfPresent(writer, "src", Src);
            AddAttributeIfPresent(writer, "srcset", SrcSet);
            AddAttributeIfPresent(writer, "alt", Alt);
            AddAttributeIfPresent(writer, "attribution", GetValue<string>(AttributionProperty));

            base.AddAttributesToRender(writer, context);
        }

        private void EnsureThatImageHasDimensions(IDotvvmRequestContext context)
        {
            var layout = IsPropertySet(LayoutProperty) ? Layout : null;
            var hasWidth = Attributes.ContainsKey("width") && !string.IsNullOrWhiteSpace((string) Attributes["width"]) || IsPropertySet(AmpControl.WidthProperty) && !string.IsNullOrWhiteSpace(GetValue<string>(AmpControl.WidthProperty));
            var hasHeight = Attributes.ContainsKey("height") && !string.IsNullOrWhiteSpace((string) Attributes["height"]) || IsPropertySet(AmpControl.HeightProperty) && !string.IsNullOrWhiteSpace(GetValue<string>(AmpControl.HeightProperty));

            var layoutRequirements = layout != null ?  layout.Value.GetAttribute<LayoutRequirementsAttribute>() : new LayoutRequirementsAttribute(true,true);
            if (layoutRequirements.RequiresHeight && !hasHeight || layoutRequirements.RequiresWidth && !hasWidth)
            {
                if (AmpConfiguration.TryToDetermineExternalResourceDimensions)
                {
                    var src = GetSrc(this);
                    var metadata =
                        AmpConfiguration.AmpExternalResourceMetadataCache.GetResourceMetadata(src, context.HttpContext);
                    Height = $"{metadata.Height}px";
                    Width = $"{metadata.Width}px";
                }
                else
                {
                    throw new AmpException(
                        $"Image must have dimensions specified when the settings {AmpConfiguration.TryToDetermineExternalResourceDimensions} is set to false!");
                }
            }
        }


        private void EnsureThatUrlsAreAbsolute(IHttpContext context)
        {
            if (!string.IsNullOrWhiteSpace(Src))
            {
                Src = context.ToAbsolutePath(Src).AbsoluteUri;
            }

            if (!string.IsNullOrWhiteSpace(SrcSet))
            {
                var result = new StringBuilder();
                var sets = SrcSet.Split(',').Select(t => t.Trim());
                foreach (var setItem in sets)
                {
                    var parts = setItem.Split(' ').Select(t => t.Trim()).ToArray();
                    result.AppendLine($"{context.ToAbsolutePath(parts[0]).AbsoluteUri} {parts[1]},");
                }

                SrcSet = result.ToString();
            }
        }
        private static string GetSrc(DotvvmControl finalControl)
        {
            string GetSrcFromSrcset(string srcset)
            {
                return (srcset?.Trim() ?? string.Empty).Split(',').First().Trim().Split(' ').First();
            }

            string src = null;
            var image = (Image)finalControl;
            if (!string.IsNullOrWhiteSpace(image.Src))
            {
                src = image.Src;
            }

            if (string.IsNullOrWhiteSpace(src) && !string.IsNullOrWhiteSpace(image.SrcSet))
            {
                src = GetSrcFromSrcset(image.SrcSet);
            }

            if (string.IsNullOrWhiteSpace(src))
            {
                throw new AmpException("Unable to get source of image!");
            }

            return src;
        }
    }
}
