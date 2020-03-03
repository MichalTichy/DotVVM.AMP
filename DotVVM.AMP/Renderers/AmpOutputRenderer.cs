using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DotVVM.AMP.Config;
using DotVVM.AMP.Renderers.RenderStrategies;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Controls.Infrastructure;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Runtime;

namespace DotVVM.AMP.Renderers
{
    public class AmpOutputRenderer : DefaultOutputRenderer, IAmpOutputRenderer
    {
        private readonly IAmpRenderStrategiesRegistry strategiesRegistry;

        public AmpOutputRenderer(DotvvmAmpConfig config)
        {
            strategiesRegistry = config.RenderStrategiesRegistry;
        }
        protected override MemoryStream RenderPage(IDotvvmRequestContext context, DotvvmView view)
        {
            var outStream = new MemoryStream();
            using (var textWriter = new StreamWriter(outStream, Encoding.UTF8, 4096, leaveOpen: true))
            {
                var htmlWriter = new HtmlWriter(textWriter, context);
                var strategy = strategiesRegistry.GetRenderStrategy(view);
                strategy.Render(view, htmlWriter, context);
            }
            outStream.Position = 0;
            return outStream;
        }

        public override async Task WriteViewModelResponse(IDotvvmRequestContext context, DotvvmView view)
        {
            throw new System.NotSupportedException();
        }

        public override async Task WriteStaticCommandResponse(IDotvvmRequestContext context, string json)
        {
            throw new System.NotSupportedException();
        }

        public override IEnumerable<(string name, string html)> RenderPostbackUpdatedControls(IDotvvmRequestContext context, DotvvmView page)
        {
            throw new System.NotSupportedException();
        }
    }
}