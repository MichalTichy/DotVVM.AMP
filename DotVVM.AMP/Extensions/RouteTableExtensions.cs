using System;
using DotVVM.AMP.Presenter;
using DotVVM.AMP.Routing;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace DotVVM.AMP.Extensions
{
    public static class RouteTableExtensions
    {
        public static void AddWithAmp(this DotvvmRouteTable routeTable, string routeName, string url, string virtualPath,
                                        object dotVVMPageDefaultValues = null, object ampPageDefaultValues = null,
                                        Func<IServiceProvider, IDotvvmPresenter> dotvvmPagePresenterFactory = null, Func<IServiceProvider, IAmpPresenter> ampPagePresenterFactory = null,
                                        IAmpRouteBuilder ampRouteBuilder = null)
        {
            ampRouteBuilder ??= new AmpRouteBuilder();
            routeTable.Add(routeName, url, virtualPath, dotVVMPageDefaultValues, dotvvmPagePresenterFactory);
            routeTable.Add(ampRouteBuilder.BuildAmpRouteName(routeName), ampRouteBuilder.BuildAmpUrl(url), virtualPath, ampPageDefaultValues, ampPagePresenterFactory ?? new Func<IServiceProvider, IDotvvmPresenter>(GetDefaultAmpPresenter));
        }

        public static void AddWithAmp(this DotvvmRouteTable routeTable, string routeName, string url, Func<IServiceProvider, IDotvvmPresenter> dotvvmPagePresenterFactory = null, Func<IServiceProvider, IAmpPresenter> ampPagePresenterFactory = null, object dotvvmPageDefaultValues = null, object ampPageDefaultValues = null, IAmpRouteBuilder ampRouteBuilder = null)
        {
            ampRouteBuilder ??= new AmpRouteBuilder();
            routeTable.Add(routeName, url, dotvvmPagePresenterFactory, dotvvmPageDefaultValues);
            routeTable.Add(ampRouteBuilder.BuildAmpRouteName(routeName), ampRouteBuilder.BuildAmpUrl(url), ampPagePresenterFactory ?? new Func<IServiceProvider, IDotvvmPresenter>(GetDefaultAmpPresenter), ampPageDefaultValues);
        }

        public static void AddWithAmp(this DotvvmRouteTable routeTable, string routeName, string url, Type dotvvmPagePresenterType, Type ampPagePresenterType, object dotvvmPageDefaultValues = null, object ampPageDefaultValues = null, IAmpRouteBuilder ampRouteBuilder = null)
        {
            if (!typeof(IAmpPresenter).IsAssignableFrom(ampPagePresenterType))
                throw new ArgumentException($"ampPagePresenterType has to inherit from {nameof(IAmpPresenter)}.", nameof(ampPagePresenterType));

            ampRouteBuilder ??= new AmpRouteBuilder();
            routeTable.Add(routeName, url, dotvvmPagePresenterType, dotvvmPageDefaultValues);
            routeTable.Add(ampRouteBuilder.BuildAmpRouteName(routeName), ampRouteBuilder.BuildAmpUrl(url), ampPagePresenterType, ampPageDefaultValues);
        }

        private static IAmpPresenter GetDefaultAmpPresenter(IServiceProvider provider)
        {
            return provider.GetService<IAmpPresenter>();
        }

    }
}
