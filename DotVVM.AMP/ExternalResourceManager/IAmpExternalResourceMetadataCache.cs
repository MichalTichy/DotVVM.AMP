using DotVVM.AMP.Config;
using DotVVM.Framework.Hosting;

namespace DotVVM.AMP.ExternalResourceManager
{
    public interface IAmpExternalResourceMetadataCache
    {
        ExternalResourceMetatada GetResourceMetadata(string location, IHttpContext context);
    }
}