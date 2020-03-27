using System.Linq;
using DotVVM.AMP.ControlTransforms;
using DotVVM.Framework.Controls.Infrastructure;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Runtime;

namespace DotVVM.AMP.ViewBuilder
{
    public class AmpViewBuilder : IAmpDotvvmViewBuilder
    {
        private readonly IAmpControlTransformsRegistry transformsRegistry;
        protected IDotvvmViewBuilder ViewBuilder { get; private set; }

        public AmpViewBuilder(IDotvvmViewBuilder viewBuilder, IAmpControlTransformsRegistry transformsRegistry)
        {
            this.transformsRegistry = transformsRegistry;
            ViewBuilder = viewBuilder;
        }
        public DotvvmView BuildView(IDotvvmRequestContext context)
        {
            var view = ViewBuilder.BuildView(context);
            var allControls = view.GetThisAndAllDescendants().ToList();

            view = (DotvvmView)(transformsRegistry.GetTransform(view)?.Transform(view, context) ?? view);

            foreach (var control in allControls)
            {
                transformsRegistry.GetTransform(control)?.Transform(control, context);
            }
            return view;
        }
    }
}