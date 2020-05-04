using System;
using DotVVM.Framework.Hosting;

namespace DotVVM.AMP.Extensions
{
    public static class UriExtensions
    {
        public static Uri ToAbsolutePath(this IHttpContext context,string location)
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
                loc = new Uri(basePath + (request.PathBase.HasValue() ? request.PathBase.Value : string.Empty) +
                              location.Replace("~", ""));
            }

            return loc;
        }
    }
}