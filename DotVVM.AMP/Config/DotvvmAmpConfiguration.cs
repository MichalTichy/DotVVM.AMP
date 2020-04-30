﻿using DotVVM.AMP.ControlTransforms;
using DotVVM.AMP.Enums;
using DotVVM.AMP.Extensions;
using DotVVM.AMP.ExternalResourceManager;
using DotVVM.AMP.Routing;

namespace DotVVM.AMP.Config
{
    public class DotvvmAmpConfiguration
    {
        public DotvvmAmpConfiguration(IAmpControlTransformsRegistry registry, IAmpRouteManager ampRouteManager, IAmpExternalResourceMetadataCache ampExternalResourceMetadataCache)
        {
            ControlTransforms = registry;
            AmpRouteManager = ampRouteManager;
            AmpExternalResourceMetadataCache = ampExternalResourceMetadataCache;
        }

        public ErrorHandlingMode AttributeErrorHandlingMode { get; set; }
        public ErrorHandlingMode KnockoutErrorHandlingMode { get; set; }
        public ErrorHandlingMode HtmlTagErrorHandlingMode { get; set; }
        public ErrorHandlingMode StylesErrorHandlingMode { get; set; }
        public ErrorHandlingMode UnsupportedControlPropertiesHandlingMode { get; set; }
        
        public IAmpControlTransformsRegistry ControlTransforms { get; set; }
        public IAmpRouteManager AmpRouteManager { get; set; }
        public IAmpExternalResourceMetadataCache AmpExternalResourceMetadataCache { get; set; }
        public bool TryToDetermineExternalResourceDimensions { get; set; } = true;
        public static string AmpJsUrl { get; set; } = @"https://cdn.ampproject.org/v0.js";
        public static uint MaximumAmpCustomStylesheetSize = 75000;
    }
}