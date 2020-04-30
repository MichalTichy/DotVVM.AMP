using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Net;
using DotVVM.AMP.ExternalResourceManager;
using DotVVM.AMP.Validator;
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
            try
            {

                bool isAbsolute = Uri.IsWellFormedUriString(location, UriKind.Absolute);
                Uri loc;
                if (isAbsolute)
                {
                    loc = new Uri(location);
                }
                else
                {
                    var request = context.Request;
                    var basePath = request.Url.GetLeftPart(UriPartial.Authority);
                    loc = new Uri(basePath + (request.PathBase.HasValue() ? request.PathBase.Value : string.Empty) + location.Replace("~", ""));
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
            catch (Exception e)
            {
                throw new AmpException($"Unable to get matadata for {location}",e);
            }
        }
    }
}