using DotVVM.AMP.Enums;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.AMP.AmpControls
{

    [ContainsDotvvmProperties]
    public class Amp
    {
        [AttachedProperty(typeof(AmpLayout))]
        public static readonly DotvvmProperty LayoutProperty =
            DotvvmProperty.Register<AmpLayout, Amp>(() => LayoutProperty);

        [AttachedProperty(typeof(string))]
        public static readonly DotvvmProperty HeightProperty =
            DotvvmProperty.Register<string, Amp>(() => HeightProperty, null);

        [AttachedProperty(typeof(string))]
        public static readonly DotvvmProperty WidthProperty =
            DotvvmProperty.Register<string, Amp>(() => WidthProperty, null);

        [AttachedProperty(typeof(bool))]
        public static readonly DotvvmProperty FallbackProperty =
            DotvvmProperty.Register<bool, Amp>(() => FallbackProperty, false);

        [AttachedProperty(typeof(bool))]
        public static readonly DotvvmProperty PlaceholderProperty =
            DotvvmProperty.Register<bool, Amp>(() => PlaceholderProperty, false);

        [AttachedProperty(typeof(bool))]
        public static readonly DotvvmProperty ExcludeProperty =
            DotvvmProperty.Register<bool, Amp>(() => ExcludeProperty, false);
    }
}