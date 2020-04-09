﻿using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.AMP.Validator;
using DotVVM.Framework.Routing;

namespace DotVVM.AMP.Routing
{
    public class AmpRouteManager : IAmpRouteManager
    {
        protected Dictionary<RouteBase, RouteBase> RouteMap = new Dictionary<RouteBase, RouteBase>();
        public void RegisterRoute(RouteBase ampRoute, RouteBase fullPageRoute)
        {
            RouteMap.Add(ampRoute, fullPageRoute);
        }

        public virtual RouteBase GetFullPageRoute(RouteBase ampRoute)
        {
            if (!RouteMap.TryGetValue(ampRoute, out var fullPageRoute))
                throw new ArgumentException($"Could not find full version of {ampRoute}.");

            return fullPageRoute;
        }

        public string GetAmpPageRouteName(string fullPageName)
        {
            var ampRouteName = BuildAmpRouteName(fullPageName);
            if (RouteMap.Any(t => t.Key.RouteName == ampRouteName))
                return ampRouteName;
            return fullPageName;
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