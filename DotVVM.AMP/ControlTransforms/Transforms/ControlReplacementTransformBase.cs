using System.Linq;
using DotVVM.AMP.Config;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace DotVVM.AMP.ControlTransforms.Transforms
{
    public abstract class ControlReplacementTransformBase : ControlTransformBase
    {

        public ControlReplacementTransformBase(DotvvmAmpConfiguration ampConfiguration, ILogger logger = null) : base(ampConfiguration,logger)
        {
        }
        protected abstract DotvvmControl CreateReplacementControl(DotvvmControl control);

        protected override DotvvmControl TransformCore(DotvvmControl control, IDotvvmRequestContext context)
        {
            var newControl = CreateReplacementControl(control);
            TransferControlProperties(control, newControl);
            ReplaceControl(control, newControl);
            return newControl;
        }

        
        public void ReplaceControl(DotvvmControl currentControl, DotvvmControl newControl)
        {
            var parent = currentControl.Parent as DotvvmControl;
            DotvvmControl[] children = new DotvvmControl[currentControl.Children.Count];
            currentControl.Children.CopyTo(children, 0);

            if (parent != null)
            {
                var childIndex = parent.Children.IndexOf(currentControl);

                parent.Children.Remove(currentControl);
                parent.Children.Insert(childIndex, newControl);
            }

            currentControl.Children.Clear();
            newControl.Children.Add(children);
        }

        protected virtual void TransferControlProperties(DotvvmControl source, DotvvmControl target)
        {
            TransferHtmlAttributes(source, target); 
            TransferDotvvmProperties(source, target);
        }

        protected virtual void TransferHtmlAttributes(DotvvmControl source, DotvvmControl target)
        {
            if (source is HtmlGenericControl sourceHtmlControl && target is HtmlGenericControl targetHtmlControl)
            {
                foreach (var attr in sourceHtmlControl.Attributes)
                {
                    targetHtmlControl.Attributes.Add(attr.Key, attr.Value);
                }
            }
        }

        protected virtual void TransferDotvvmProperties(DotvvmControl source, DotvvmControl target)
        {
            foreach (var property in source.Properties)
            {
                if (target.Properties.Any(t => t.Key == property.Key))
                {
                    target.SetValueRaw(property.Key, property.Value);
                }
                else
                {
                    target.Properties.Add(property.Key, property.Value);
                }
            }
        }
    }
}