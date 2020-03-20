using DotVVM.AMP.Config;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using Microsoft.Extensions.Logging;

namespace DotVVM.AMP.ControlTransforms.Transforms
{
    public abstract class ControlTransformBase : IControlTransform
    {
        protected readonly DotvvmAmpConfiguration AmpConfiguration;
        private readonly ILogger logger;

        public ControlTransformBase(DotvvmAmpConfiguration ampConfiguration, ILogger logger = null)
        {
            this.AmpConfiguration = ampConfiguration;
            this.logger = logger;
        }
        public abstract bool CanTransform(DotvvmControl control);
        protected abstract DotvvmControl CreateReplacementControl(DotvvmControl control);

        public virtual DotvvmControl Transform(DotvvmControl control, IDotvvmRequestContext context)
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
            var childIndex = parent.Children.IndexOf(currentControl);

            parent.Children.Remove(currentControl);
            parent.Children.Insert(childIndex, newControl);
            currentControl.Children.Clear();
            newControl.Children.Add(children);
        }

        protected virtual void TransferControlProperties(DotvvmControl source, DotvvmControl target)
        {
            if (source is HtmlGenericControl sourceHtmlControl && target is HtmlGenericControl targetHtmlControl)
            {
                foreach (var attr in sourceHtmlControl.Attributes)
                {
                    targetHtmlControl.Attributes.Add(attr.Key, attr.Value);
                }
            }
        }
    }
}