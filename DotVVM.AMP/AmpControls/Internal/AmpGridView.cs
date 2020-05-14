using DotVVM.AMP.ControlTransforms;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.AMP.AmpControls.Internal
{
    public class AmpGridView : GridView
    {
        private readonly IAmpControlTransformsRegistry transformsRegistry;

        public AmpGridView(IAmpControlTransformsRegistry ampControlTransformsRegistry)
        {
            this.transformsRegistry = ampControlTransformsRegistry;
        }
        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            base.OnPreRender(context);
            transformsRegistry.ApplyTransforms(this,context);
        }
    }
}