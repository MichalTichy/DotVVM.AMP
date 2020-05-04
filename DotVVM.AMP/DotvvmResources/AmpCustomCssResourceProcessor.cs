using System;
using DotVVM.Framework.ResourceManagement;
using System.Collections.Generic;
using DotVVM.AMP.Validator;

namespace DotVVM.AMP.DotvvmResources
{
    public class AmpCustomCssResourceProcessor : IResourceProcessor
    {
        private readonly Func<IAmpStylesheetResourceCollection> ampStylesheetResourceCollectionFactory;

        public AmpCustomCssResourceProcessor(Func<IAmpStylesheetResourceCollection> ampStylesheetResourceCollectionFactory)
        {
            this.ampStylesheetResourceCollectionFactory = ampStylesheetResourceCollectionFactory;
        }

        public IEnumerable<NamedResource> Process(IEnumerable<NamedResource> source)
        {
            var stylesheetResourceCollection = ampStylesheetResourceCollectionFactory();
            var mergedResource = new AmpCustomStylesheetResource(stylesheetResourceCollection);
            var mergedKeyFrameResource = new AmpKeyframesStylesheetResource(stylesheetResourceCollection);

            
            foreach (var namedResource in source)
            {
                foreach (var namedResource1 in ProcessNamedResource(namedResource,stylesheetResourceCollection))
                    yield return namedResource1;
            }
            yield return new NamedResource("AmpCustomCss",mergedResource);
            yield return new NamedResource("AmpKeyframes", mergedKeyFrameResource);
        }

        private IEnumerable<NamedResource> ProcessNamedResource(NamedResource namedResource, IAmpStylesheetResourceCollection ampStylesheetResourceCollection)
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

            if (!isMerged)
            {
                yield return namedResource;
            }
        }
    }
}