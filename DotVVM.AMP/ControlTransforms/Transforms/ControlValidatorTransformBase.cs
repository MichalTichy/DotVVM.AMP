using System;
using DotVVM.AMP.Config;
using DotVVM.AMP.Enums;
using DotVVM.AMP.Validator;
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
            if (IsSupported(control))
                return;
            
            var message = $"Property {propertyName} is not supported";

            switch (AmpConfiguration.UnsupportedControlPropertiesHandlingMode)
            {
                case ErrorHandlingMode.Throw:
                    throw new AmpException(message);
                case ErrorHandlingMode.LogAndIgnore:
                    Logger?.LogError(message);
                    setToSupportedState(control);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

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