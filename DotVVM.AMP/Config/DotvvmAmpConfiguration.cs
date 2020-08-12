using DotVVM.AMP.ControlTransforms;
using DotVVM.AMP.Enums;
using DotVVM.AMP.Extensions;
using DotVVM.AMP.ExternalResourceManager;
using DotVVM.AMP.Routing;

namespace DotVVM.AMP.Config
{
    public class DotvvmAmpConfiguration
    {
        public DotvvmAmpConfiguration(IAmpControlTransformsRegistry registry, IAmpRouteManager routeManager, IAmpExternalResourceMetadataCache externalResourceMetadataCache)
        {
            ControlTransforms = registry;
            RouteManager = routeManager;
            ExternalResourceMetadataCache = externalResourceMetadataCache;
        }

        public IAmpControlTransformsRegistry ControlTransforms { get; set; }
        public IAmpRouteManager RouteManager { get; set; }
        public IAmpExternalResourceMetadataCache ExternalResourceMetadataCache { get; set; }

        public ErrorHandlingMode AttributeHandlingMode { get; set; }
        public ErrorHandlingMode KnockoutHandlingMode { get; set; }
        public ErrorHandlingMode HtmlTagHandlingMode { get; set; }
        public ErrorHandlingMode StylesHandlingMode { get; set; }
        public ErrorHandlingMode UnsupportedControlPropertiesHandlingMode { get; set; }
        
        public bool TryToDetermineExternalResourceDimensions { get; set; } = true;
        public static string AmpJsUrl { get; set; } = @"https://cdn.ampproject.org/v0.js";
        public static string AmpCdnUrl { get; set; } = @"https://cdn.ampproject.org/";
        public bool StyleRemoveForbiddenImportant { get; set; }

        public static uint MaximumAmpCustomStylesheetSize = 75000;
    }
}