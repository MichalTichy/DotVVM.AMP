﻿using DotVVM.AMP.AmpControls;
using DotVVM.AMP.AmpControls.Internal;
using DotVVM.AMP.Config;
using DotVVM.Framework.Controls;
using Microsoft.Extensions.Logging;

namespace DotVVM.AMP.ControlTransforms.Transforms
{
    public class RouteLinkTransform : ControlReplacementTransformBase
    {
        public RouteLinkTransform(DotvvmAmpConfiguration ampConfiguration, ILogger logger) : base(ampConfiguration, logger)
        {
        }

        public override bool CanTransform(DotvvmControl control)
        {
            return control is Framework.Controls.RouteLink;
        }

        protected override DotvvmControl CreateReplacementControl(DotvvmControl control)
        {
            var routeLink = control as RouteLink;
            return new AmpRouteLink(routeLink)
            {
                Config = AmpConfiguration
            };
        }
    }
}