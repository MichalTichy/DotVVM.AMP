using System;
using DotVVM.AMP.Config;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using Microsoft.Extensions.Logging;

namespace DotVVM.AMP.ControlTransforms.Transforms
{
    public abstract class ControlValidatorTransformBase<TControl> : ControlTransformBase where TControl : class
    {
        protected ControlValidatorTransformBase(DotvvmAmpConfiguration ampConfiguration, ILogger logger) : base(ampConfiguration, logger)
        {
        }

        public override bool CanTransform(DotvvmControl control)
        {
            return control is TControl;
        }

        protected void IsPropertySupported(TControl control, Func<TControl, bool> IsSupported, Action<TControl> setToSupportedState, string propertyName)
        {
            var message = $"Property {propertyName} is not supported";


        }

        public override DotvvmControl Transform(DotvvmControl control, IDotvvmRequestContext context)
        {
            var gridView = control as TControl;
            ValidateControl(gridView, context);
            return control;
        }

        protected abstract void ValidateControl(TControl control, IDotvvmRequestContext context);
    }
}