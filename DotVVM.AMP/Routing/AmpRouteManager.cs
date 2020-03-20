using System;
using System.Collections.Generic;
using DotVVM.AMP.Validator;
using DotVVM.Framework.Routing;

namespace DotVVM.AMP.Routing
{
    public class AmpRouteManager : IAmpRouteManager
    {
        protected Dictionary<string, string> RouteMap = new Dictionary<string, string>();

        public static IAmpRouteManager Instance { get; } = new AmpRouteManager();

        public void RegisterRoute(string ampRoute, string fullPageRoute)
        {
            RouteMap.Add(ampRoute, fullPageRoute);
        }

        protected AmpRouteManager()
        {
        }

        public virtual string GetFullPageRouteName(string ampRouteName)
        {
            if (!RouteMap.TryGetValue(ampRouteName, out var fullPageRoute))
                throw new ArgumentException($"Could not find full version of {ampRouteName}.");

            return fullPageRoute;
        }
        public virtual string BuildAmpRouteName(string dotvvmPageRouteName)
        {
            return $"{dotvvmPageRouteName}-amp";
        }

        public virtual string BuildAmpUrl(string dotvvmPageUrl)
        {
            return $"amp/{dotvvmPageUrl}";
        }
    }
}