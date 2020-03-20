using DotVVM.AMP.Config;
using DotVVM.AMP.ControlTransforms;
using DotVVM.AMP.ControlTransforms.Transforms;
using DotVVM.AMP.Presenter;
using DotVVM.AMP.Renderers;
using DotVVM.AMP.Routing;
using DotVVM.AMP.Validator;
using DotVVM.AMP.ViewBuilder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotVVM.AMP.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDotvvmAmpSupport(this IDotvvmServiceCollection serviceCollection, Action<DotvvmAmpConfiguration> modifyConfiguration = null)
        {
            serviceCollection.Services.AddSingleton<IAmpPresenter, DotvvmAmpPresenter>();
            serviceCollection.Services.AddSingleton<IAmpOutputRenderer, AmpOutputRenderer>();
            serviceCollection.Services.AddSingleton<IAmpDotvvmViewBuilder, AmpViewBuilder>();
            serviceCollection.Services.AddSingleton<IAmpControlTransformsRegistry, AmpControlTransformsRegistry>();
            serviceCollection.Services.AddSingleton<IAmpValidator, AmpValidator>();
            serviceCollection.Services.AddSingleton<IAmpRouteManager, AmpRouteManager>();

            serviceCollection.Services.AddSingleton<DotvvmAmpConfiguration>(provider =>
            {
                var registry = provider.GetService<IAmpControlTransformsRegistry>();
                var routeManager = provider.GetService<IAmpRouteManager>();
                var config = new DotvvmAmpConfiguration(registry, routeManager);
                RegisterTransforms(config);
                modifyConfiguration?.Invoke(config);
                return config;
            });
        }

        private static void RegisterTransforms(DotvvmAmpConfiguration configuration)
        {
            configuration.ControlTransforms.Register(new HtmlTagTransform());
            configuration.ControlTransforms.Register(new HeadTagTransform(configuration.AmpRouteManager));
        }
    }
}