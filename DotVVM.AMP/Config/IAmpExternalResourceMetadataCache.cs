using System.Collections.Generic;
using DotVVM.Framework.Hosting;

namespace DotVVM.AMP.Config
{
    public interface IAmpExternalResourceMetadataCache
    {
        ExternalResourceMetatada GetResourceMetadata(string location, IHttpContext context);
    }
}