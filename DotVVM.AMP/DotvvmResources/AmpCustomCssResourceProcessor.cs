using DotVVM.Framework.ResourceManagement;
using System.Collections.Generic;

namespace DotVVM.AMP.DotvvmResources
{
    public class AmpCustomCssResourceProcessor : IResourceProcessor
    {
        public IEnumerable<NamedResource> Process(IEnumerable<NamedResource> source)
        {
            var mergedResource = new AmpInlineStylesheetResource();
            foreach (var namedResource in source)
            {
                var resource = namedResource.Resource;

                var isMerged = false;
                if (resource is StylesheetResource stylesheetResource)
                {
                    mergedResource.AddResource(stylesheetResource.Location);
                    isMerged = true;
                }
                else if (resource is InlineStylesheetResource inlineStylesheetResource)
                {
                    mergedResource.AddResource(inlineStylesheetResource.Code);
                    isMerged = true;
                }

                if (isMerged)
                    mergedResource.AddDependencies(resource.Dependencies);
                else
                    yield return namedResource;
            }

            yield return new NamedResource("AmpCustomCss",mergedResource);
        }
    }
}