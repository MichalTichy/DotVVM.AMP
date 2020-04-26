using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Net;
using DotVVM.Framework.Hosting;

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
            bool isAbsolute = Uri.IsWellFormedUriString(location,UriKind.Absolute);
            Uri loc;
            if (isAbsolute)
            {
                loc = new Uri(location);
            }
            else
            {
                var request = context.Request;
                var basePath = context.Request.Url.GetLeftPart(UriPartial.Authority);
                loc = new Uri(new Uri(basePath), location);
            }

            using (var response = WebRequest.Create(loc).GetResponse().GetResponseStream())
            {
                var image = Image.FromStream(response);
                return new ExternalResourceMetatada()
                {
                    Width = image.Width,
                    Height = image.Height
                };
            }
        }
    }
}