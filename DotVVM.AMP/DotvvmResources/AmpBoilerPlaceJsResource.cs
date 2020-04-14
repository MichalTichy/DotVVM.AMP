using DotVVM.AMP.Config;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.AMP.DotvvmResources
{
    public class AmpBoilerPlaceJsResource : ScriptResource, IAmpAllowedResource
    {
        public AmpBoilerPlaceJsResource()
        {
            Location = new UrlResourceLocation(DotvvmAmpConfiguration.AmpJsUrl);
            RenderPosition = ResourceRenderPosition.Head;
        }

        public override void RenderLink(IResourceLocation location, IHtmlWriter writer, IDotvvmRequestContext context, string resourceName)
        {
            writer.AddAttribute("async", null);
            base.RenderLink(location, writer, context, resourceName);
        }

        public override void Render(IHtmlWriter writer, IDotvvmRequestContext context, string resourceName)
        {
            writer.AddAttribute("async", null);
            base.Render(writer, context, resourceName);
        }
    }
}