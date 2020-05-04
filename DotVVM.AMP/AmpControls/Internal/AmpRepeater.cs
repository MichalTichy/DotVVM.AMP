using System.Linq;
using DotVVM.AMP.ControlTransforms;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.AMP.AmpControls.Internal
{
    public class AmpRepeater : Repeater
    {
        private readonly IAmpControlTransformsRegistry transformsRegistry;

        public AmpRepeater(IAmpControlTransformsRegistry ampControlTransformsRegistry)
        {
            this.transformsRegistry = ampControlTransformsRegistry;
        }
        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            base.OnPreRender(context);
            foreach (var control in GetAllDescendants().ToList())
            {
                transformsRegistry.GetTransform(control)?.Transform(control, context);
            }
        }
    }
}