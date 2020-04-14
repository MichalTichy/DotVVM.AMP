using System.Threading;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.AMP.DotvvmResources
{
    public class AmpCustomStylesheetResource : ResourceBase
    {
        protected IAmpStylesheetResourceCollection resources;

        public AmpCustomStylesheetResource(IAmpStylesheetResourceCollection ampStylesheetResourceCollection) : base(
            ResourceRenderPosition.Head)
        {
            resources = ampStylesheetResourceCollection;
        }

        public void AddResource(IResourceLocation resource)
        {
            resources.Add(resource);
        }

        public void AddResource(string inlineStyle)
        {
            resources.Add(new InlineResourceLocation(inlineStyle));
        }

        public override void Render(IHtmlWriter writer, IDotvvmRequestContext context, string resourceName)
        {
            var code = resources.GetAmpCustomCode(context).Result;
            if (!string.IsNullOrWhiteSpace(code))
            {
                writer.AddAttribute("amp-custom", null);
                writer.RenderBeginTag("style");
                writer.WriteUnencodedText(code);
                writer.RenderEndTag();
            }
        }
    }
}