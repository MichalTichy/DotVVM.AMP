using System;
using System.Collections.Generic;
using System.Reflection;
using DotVVM.AMP.AmpControls;
using DotVVM.AMP.AmpControls.Decorators;
using DotVVM.AMP.Config;
using DotVVM.AMP.Enums;
using DotVVM.AMP.Validator;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using Microsoft.Extensions.Logging;

namespace DotVVM.AMP.ControlTransforms.Transforms
{
    public abstract class ControlTransformBase: IControlTransform
    {
        protected readonly DotvvmAmpConfiguration AmpConfiguration;
        protected ILogger Logger { get; set; }


        protected ControlTransformBase(DotvvmAmpConfiguration ampConfiguration, ILogger logger)
        {
            this.AmpConfiguration = ampConfiguration;
            this.Logger = logger;
        }
        
        public abstract bool CanTransform(DotvvmControl control);

        public DotvvmControl Transform(DotvvmControl control, IDotvvmRequestContext context)
        {
            if (!ShouldBeRendered(control,context))
            {
                RemoveControlFromTree(control);
                return null;
            }
            BeforeTransform(control,context);
            var finalControl = TransformCore(control, context);

            if (finalControl==null)
                return null;

            SetRequiredSettings(finalControl,context);
            ApplyAttachedProperties(control, context);
            AfterTransform(finalControl,context);
            return finalControl;
        }

        protected virtual bool ShouldBeRendered(DotvvmControl control, IDotvvmRequestContext context)
        {
            return !control.IsPropertySet(Amp.ExcludeProperty) || !control.GetValue<bool>(Amp.ExcludeProperty);
        }

        protected virtual void RemoveControlFromTree(DotvvmControl control)
        {
            var parent = control.Parent as DotvvmControl;
            parent?.Children.Remove(control);
        }

        protected virtual void BeforeTransform(DotvvmControl control, IDotvvmRequestContext context)
        {
        }
        protected virtual void AfterTransform(DotvvmControl finalControl, IDotvvmRequestContext context)
        {
        }

        protected virtual void ApplyAttachedProperties(DotvvmControl control, IDotvvmRequestContext context)
        {
            ApplyPlaceholderProperty(control);
            ApplyFallbackProperty(control);
        }

        private void ApplyFallbackProperty(DotvvmControl control)
        {
            if (control.IsPropertySet(Amp.FallbackProperty) && control.GetValue<bool>(Amp.FallbackProperty) && control is HtmlGenericControl genericControl)
            {
                genericControl.Attributes.Add("fallback",null);
            }
        }

        private void ApplyPlaceholderProperty(DotvvmControl control)
        {
            if (control.IsPropertySet(Amp.PlaceholderProperty) && control.GetValue<bool>(Amp.PlaceholderProperty) && control is HtmlGenericControl genericControl)
            {
                genericControl.Attributes.Add("placeholder", null);
            }
        }
        
        protected abstract DotvvmControl TransformCore(DotvvmControl control, IDotvvmRequestContext context);

        protected virtual void SetRequiredSettings(DotvvmControl control, IDotvvmRequestContext context)
        {
            control.SetValueRaw(RenderSettings.ModeProperty, RenderMode.Server);
        }
    }
}