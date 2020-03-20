using System.Collections.Generic;
using DotVVM.AMP.ControlTransforms.Transforms;
using DotVVM.Framework.Controls;

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
    }
}