using System;
using System.Collections.Generic;
using DotVVM.AMP.Config;
using DotVVM.AMP.Presenter;
using DotVVM.AMP.Routing;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace DotVVM.AMP.Extensions
{
    public static class RouteTableExtensions
    {
        public static void AddWithAmp(this DotvvmRouteTable routeTable, string routeName, string url, string virtualPath, DotvvmConfiguration dotvvmConfiguration,
                                        object dotVVMPageDefaultValues = null, object ampPageDefaultValues = null,
                                        Func<IServiceProvider, IDotvvmPresenter> dotvvmPagePresenterFactory = null, Func<IServiceProvider, IAmpPresenter> ampPagePresenterFactory = null)
        {

            var ampRouteManager = GetAmpConfiguration(dotvvmConfiguration).AmpRouteManager;

            var ampPageRoute = ampRouteManager.BuildAmpRouteName(routeName);


            routeTable.Add(routeName, url, virtualPath, dotVVMPageDefaultValues, dotvvmPagePresenterFactory);
            routeTable.Add(ampPageRoute, ampRouteManager.BuildAmpUrl(url), virtualPath, ampPageDefaultValues, ampPagePresenterFactory ?? new Func<IServiceProvider, IDotvvmPresenter>(GetDefaultAmpPresenter));

            ampRouteManager.RegisterRoute(routeTable[ampPageRoute], routeTable[routeName]);

        }

        public static void AddWithAmp(this DotvvmRouteTable routeTable, string routeName, string url, DotvvmConfiguration dotvvmConfiguration, Func<IServiceProvider, IDotvvmPresenter> dotvvmPagePresenterFactory = null, Func<IServiceProvider, IAmpPresenter> ampPagePresenterFactory = null, object dotvvmPageDefaultValues = null, object ampPageDefaultValues = null)
        {
            var ampRouteManager = GetAmpConfiguration(dotvvmConfiguration).AmpRouteManager;

            var ampPageRoute = ampRouteManager.BuildAmpRouteName(routeName);

            routeTable.Add(routeName, url, dotvvmPagePresenterFactory, dotvvmPageDefaultValues);
            routeTable.Add(ampPageRoute, ampRouteManager.BuildAmpUrl(url), ampPagePresenterFactory ?? new Func<IServiceProvider, IDotvvmPresenter>(GetDefaultAmpPresenter), ampPageDefaultValues);

            ampRouteManager.RegisterRoute(routeTable[ampPageRoute], routeTable[routeName]);
        }

        public static void AddWithAmp(this DotvvmRouteTable routeTable, string routeName, string url, Type dotvvmPagePresenterType, Type ampPagePresenterType, DotvvmConfiguration dotvvmConfiguration, object dotvvmPageDefaultValues = null, object ampPageDefaultValues = null)
        {
            if (!typeof(IAmpPresenter).IsAssignableFrom(ampPagePresenterType))
                throw new ArgumentException($"ampPagePresenterType has to inherit from {nameof(IAmpPresenter)}.", nameof(ampPagePresenterType));

            var ampRouteManager = GetAmpConfiguration(dotvvmConfiguration).AmpRouteManager;

            var ampPageRoute = ampRouteManager.BuildAmpRouteName(routeName);

            routeTable.Add(routeName, url, dotvvmPagePresenterType, dotvvmPageDefaultValues);
            routeTable.Add(ampPageRoute, ampRouteManager.BuildAmpUrl(url), ampPagePresenterType, ampPageDefaultValues);

            ampRouteManager.RegisterRoute(routeTable[ampPageRoute], routeTable[routeName]);

        }

        private static IAmpPresenter GetDefaultAmpPresenter(IServiceProvider provider)
        {
            return provider.GetService<IAmpPresenter>();
        }

        private static DotvvmAmpConfiguration GetAmpConfiguration(DotvvmConfiguration configuration)
        {
            return configuration.ServiceProvider.GetService<DotvvmAmpConfiguration>();
        }

    }
}
