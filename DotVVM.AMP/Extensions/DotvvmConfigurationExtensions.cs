using DotVVM.AMP.DotvvmResources;
using DotVVM.Framework.Configuration;

namespace DotVVM.AMP.Extensions
{
    public static class DotvvmConfigurationExtensions
    {
        public static void AddDotvvmAmp(this DotvvmConfiguration dotvvmConfiguration)
        {
            RegisterResources(dotvvmConfiguration);
        }

        private static void RegisterResources(DotvvmConfiguration dotvvmConfiguration)
        {
            dotvvmConfiguration.Resources.Register("amp-boilerplate-css", new AmpBoilerPlaceCssResource());
            dotvvmConfiguration.Resources.Register("amp-boilerplate-js", new AmpBoilerPlaceJsResource());
        }
    }
}