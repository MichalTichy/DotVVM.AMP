using DotVVM.AMP.Config;
using DotVVM.AMP.Writer;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.AMP.Renderers.RenderStrategies
{
    public class HtmlGenericControlRenderStrategy : RenderStrategyBase
    {
        private readonly DotvvmAmpConfig config;

        public HtmlGenericControlRenderStrategy(DotvvmAmpConfig config)
        {
            this.config = config;
        }
        public override bool CanHandle(DotvvmControl control)
        {
            return true;
        }

        public override void Render(DotvvmControl control, IHtmlWriter htmlWriter, IDotvvmRequestContext context)
        {
            var ampWriter = new AmpHtmlWriter(htmlWriter, config);
            control.Render(ampWriter, context);
        }
    }
}