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
using Microsoft.Extensions.Logging;

namespace DotVVM.AMP.Renderers
{
    public class AmpOutputRenderer : DefaultOutputRenderer, IAmpOutputRenderer
    {
        private readonly DotvvmAmpConfiguration configuration;
        private readonly IAmpValidator validator;
        private readonly ILogger logger;

        public AmpOutputRenderer(DotvvmAmpConfiguration configuration, IAmpValidator validator,ILogger logger=null)
        {
            this.configuration = configuration;
            this.validator = validator;
            this.logger = logger;
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
            return new AmpHtmlWriter(configuration, textWriter, context, validator,logger);
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