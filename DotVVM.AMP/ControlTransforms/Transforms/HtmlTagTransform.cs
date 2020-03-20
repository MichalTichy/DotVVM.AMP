﻿using System;
using DotVVM.AMP.AmpControls;
using DotVVM.AMP.Config;
using DotVVM.AMP.Routing;
using DotVVM.Framework.Controls;
using Microsoft.Extensions.Logging;

namespace DotVVM.AMP.ControlTransforms.Transforms
{
    public class HtmlTagTransform : ControlTransformBase
    {
        public override bool CanTransform(DotvvmControl control)
        {
            return control is HtmlGenericControl htmlGenericControl && htmlGenericControl.TagName == "html";
        }

        protected override DotvvmControl CreateReplacementControl(DotvvmControl control)
        {
            return new AmpHtmlTag();
        }

        public HtmlTagTransform(DotvvmAmpConfiguration ampConfiguration, ILogger logger = null) : base(ampConfiguration, logger)
        {
        }
    }
}