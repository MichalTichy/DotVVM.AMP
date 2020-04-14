using System.Linq;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.AMP.DotvvmResources
{
    public class AmpKeyframesStylesheetResource : ResourceBase, IAmpAllowedResource
    {
        protected IAmpStylesheetResourceCollection resources;

        public AmpKeyframesStylesheetResource(IAmpStylesheetResourceCollection ampStylesheetResourceCollection) : base(ResourceRenderPosition.Body)
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
            var code = resources.GetAmpKeyframesCode(context).Result;
            if (!string.IsNullOrWhiteSpace(code))
            {
                writer.AddAttribute("amp-keyframes", null);
                writer.RenderBeginTag("style");
                writer.WriteUnencodedText(code);
                writer.RenderEndTag();
            }
        }

        public void AddDependencies(string[] resourceDependencies)
        {
            Dependencies = Dependencies.Union(resourceDependencies).ToArray();
        }
    }
}