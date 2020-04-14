using DotVVM.Framework.ResourceManagement;
using System.Collections.Generic;
using DotVVM.AMP.Validator;

namespace DotVVM.AMP.DotvvmResources
{
    public class AmpCustomCssResourceProcessor : IResourceProcessor
    {
        private readonly IAmpStylesheetResourceCollection ampStylesheetResourceCollection;
        private readonly DotvvmResourceRepository _resources;
        private HashSet<string> ReturnedResources = new HashSet<string>();

        public AmpCustomCssResourceProcessor(IAmpStylesheetResourceCollection ampStylesheetResourceCollection,
            DotvvmResourceRepository resources)
        {
            this.ampStylesheetResourceCollection = ampStylesheetResourceCollection;
            _resources = resources;
        }

        public IEnumerable<NamedResource> Process(IEnumerable<NamedResource> source)
        {
            var mergedResource = new AmpCustomStylesheetResource(ampStylesheetResourceCollection);
            var mergedKeyFrameResource = new AmpKeyframesStylesheetResource(ampStylesheetResourceCollection);

            foreach (var namedResource in source)
            {
                foreach (var namedResource1 in ProcessNamedResource(namedResource))
                    yield return namedResource1;
            }
            yield return new NamedResource("AmpCustomCss",mergedResource);
            yield return new NamedResource("AmpKeyframes", mergedKeyFrameResource);
        }

        private IEnumerable<NamedResource> ProcessNamedResource(NamedResource namedResource)
        {
            var resource = namedResource.Resource;

            var isMerged = false;
            if (resource is StylesheetResource stylesheetResource)
            {
                ampStylesheetResourceCollection.Add(stylesheetResource.Location);
                isMerged = true;
            }
            else if (resource is InlineStylesheetResource inlineStylesheetResource)
            {
                ampStylesheetResourceCollection.Add(new InlineResourceLocation(inlineStylesheetResource.Code));
                isMerged = true;
            }

            if (isMerged)
            {
                foreach (var dependencyName in namedResource.Resource.Dependencies)
                {
                    var dependency = _resources.FindNamedResource(dependencyName);
                    if (dependency != null && !ReturnedResources.Contains(dependencyName))
                    {
                        foreach (var dependedResource in ProcessNamedResource(dependency))
                        {
                            yield return dependedResource;
                        }
                    }
                }
            }
            else if (!ReturnedResources.Contains(namedResource.Name))
            {
                ReturnedResources.Add(namedResource.Name);
                yield return namedResource;
            }
        }
    }
}