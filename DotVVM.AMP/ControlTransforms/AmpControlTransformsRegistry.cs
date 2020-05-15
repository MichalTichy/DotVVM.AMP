using System.Collections.Generic;
using System.Linq;
using DotVVM.AMP.ControlTransforms.Transforms;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.AMP.ControlTransforms
{
    public class AmpControlTransformsRegistry : IAmpControlTransformsRegistry
    {
        List<IControlTransform> transforms = new List<IControlTransform>();

        public void Register(IControlTransform transform)
        {
            transforms.Add(transform);
        }

        public IControlTransform GetTransform(DotvvmControl control)
        {
            //gets the last render strategy that can handle given control.
            for (int i = transforms.Count - 1; i >= 0; i--)
            {
                var transform = transforms[i];
                if (transform.CanTransform(control))
                {
                    return transform;
                }
            }

            return null;
        }

        public void ApplyTransforms(DotvvmControl  root,IDotvvmRequestContext context)
        {
            ApplyTransforms(root.GetAllDescendants().ToList(), context);
        }

        public void ApplyTransforms(List<DotvvmControl> allControls, IDotvvmRequestContext context)
        {
            foreach (var control in allControls)
            {
                GetTransform(control)?.Transform(control, context);
            }
        }
    }
}