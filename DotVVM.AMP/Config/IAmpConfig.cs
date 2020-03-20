﻿using DotVVM.AMP.ControlTransforms;
using DotVVM.AMP.Enums;
using DotVVM.AMP.Extensions;
using DotVVM.AMP.Routing;

namespace DotVVM.AMP.Config
{
    public class DotvvmAmpConfiguration
    {
        public DotvvmAmpConfiguration(IAmpControlTransformsRegistry registry, IAmpRouteManager ampRouteManager)
        {
            ControlTransforms = registry;
            AmpRouteManager = ampRouteManager;
        }

        public ErrorHandlingMode AttributeErrorHandlingMode { get; set; }
        public ErrorHandlingMode KnockoutErrorHandlingMode { get; set; }

        public IAmpControlTransformsRegistry ControlTransforms { get; set; }
        public IAmpRouteManager AmpRouteManager { get; set; }
    }
}