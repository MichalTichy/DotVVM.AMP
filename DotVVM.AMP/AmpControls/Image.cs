using System;
using System.Collections.Generic;
using System.Text;
using DotVVM.AMP.Enums;
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
            get { return (string) GetValue(SrcProperty); }
            set { SetValue(SrcProperty, value); }
        }

        public static readonly DotvvmProperty SrcProperty
            = DotvvmProperty.Register<string, Image>(c => c.Src, null);


        public string SrcSet
        {
            get { return (string) GetValue(SrcSetProperty); }
            set { SetValue(SrcSetProperty, value); }
        }

        public static readonly DotvvmProperty SrcSetProperty
            = DotvvmProperty.Register<string, Image>(c => c.SrcSet, null);

        public string Alt
        {
            get { return (string) GetValue(AltProperty); }
            set { SetValue(AltProperty, value); }
        }

        public static readonly DotvvmProperty AltProperty
            = DotvvmProperty.Register<string, Image>(c => c.Alt, null);
        
        [AttachedProperty(typeof(string))]
        public static readonly DotvvmProperty   AttributionProperty
            = DotvvmProperty.Register<string, Image>(() => AttributionProperty, null);


        protected override void LoadPropertiesFromAttributes()
        {
            TransferAttribute("src", nameof(Src),SrcProperty);
            TransferAttribute("srcset", nameof(SrcSet),SrcSetProperty);
            TransferAttribute("alt", nameof(Alt),AltProperty);
            base.LoadPropertiesFromAttributes();
        }

        protected override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Src) && string.IsNullOrWhiteSpace(SrcSet))
                throw new DotvvmControlException($"{Src} and / or {SrcSet} property must be set!");
            base.Validate();
        }

        protected override IEnumerable<AmpLayout> SupportedLayouts => new []{AmpLayout.fill,AmpLayout.fixedLayout,AmpLayout.fixedHeight,AmpLayout.flexItem,AmpLayout.intrinsic,AmpLayout.noDisplay,AmpLayout.responsive};

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            AddAttributeIfPresent(writer, "src", Src);
            AddAttributeIfPresent(writer, "srcset", SrcSet);
            AddAttributeIfPresent(writer, "alt", Alt);
            AddAttributeIfPresent(writer, "attribution", GetValue<string>(AttributionProperty));

            base.AddAttributesToRender(writer, context);
        }
    }
}
