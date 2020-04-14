using System.Collections.Generic;
using System.Linq;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.AMP.DotvvmResources
{
    public class AmpResourcesProcessor : IResourceProcessor
    {
        public IEnumerable<NamedResource> Process(IEnumerable<NamedResource> source)
        {
            return source.Where(t => t.Resource is IAmpAllowedResource);
        }
    }
}