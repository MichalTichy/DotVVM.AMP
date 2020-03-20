using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DotVVM.AMP.Config;
using DotVVM.AMP.Validator;
using DotVVM.AMP.Writer;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Controls.Infrastructure;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Runtime;

namespace DotVVM.AMP.Renderers
{
    public class AmpOutputRenderer : DefaultOutputRenderer, IAmpOutputRenderer
    {
        private readonly DotvvmAmpConfig config;
        private readonly IAmpValidator validator;

        public AmpOutputRenderer(DotvvmAmpConfig config, IAmpValidator validator)
        {
            this.config = config;
            this.validator = validator;
        }
        protected override MemoryStream RenderPage(IDotvvmRequestContext context, DotvvmView view)
        {
            var outStream = new MemoryStream();
            using (var textWriter = new StreamWriter(outStream, Encoding.UTF8, 4096, leaveOpen: true))
            {
                var htmlWriter = CreateAmpHtmlWriter(context, textWriter);
                view.Render(htmlWriter, context);
            }
            outStream.Position = 0;
            return outStream;
        }

        protected virtual AmpHtmlWriter CreateAmpHtmlWriter(IDotvvmRequestContext context, StreamWriter textWriter)
        {
            return new AmpHtmlWriter(config, textWriter, context, validator);
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