using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Net;
using DotVVM.AMP.ExternalResourceManager;
using DotVVM.AMP.Validator;
using DotVVM.Framework.Hosting;
using ExCSS;

namespace DotVVM.AMP.Config
{
    public class AmpExternalResourceMetadataCache : IAmpExternalResourceMetadataCache
    {
        ConcurrentDictionary<string,ExternalResourceMetatada> Cache=new ConcurrentDictionary<string, ExternalResourceMetatada>();
        public ExternalResourceMetatada GetResourceMetadata(string location,IHttpContext context)
        {
            return Cache.GetOrAdd(location, GetMetadata(location,context));
        }

        protected virtual ExternalResourceMetatada GetMetadata(string location, IHttpContext context)
        {
            try
            {

                using (var response = WebRequest.Create(new Uri(location)).GetResponse().GetResponseStream())
                {
                    var image = Image.FromStream(response);
                    return new ExternalResourceMetatada()
                    {
                        Width = image.Width,
                        Height = image.Height
                    };
                }
            }
            catch (Exception e)
            {
                throw new AmpException($"Unable to get matadata for {location}",e);
            }
        }
    }
}