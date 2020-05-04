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
using DotVVM.AMP.DotvvmResources;
using DotVVM.AMP.ExternalResourceManager;
using Microsoft.Extensions.Logging;

namespace DotVVM.AMP.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddFactory<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            services.AddTransient<TService, TImplementation>();
            services.AddSingleton<Func<TService>>(x => () => x.GetService<TService>());
        }
        public static void AddDotvvmAmpSupport(this IDotvvmServiceCollection serviceCollection, Action<DotvvmAmpConfiguration> modifyConfiguration = null)
        {
            serviceCollection.Services.AddSingleton<IAmpPresenter, DotvvmAmpPresenter>();
            serviceCollection.Services.AddSingleton<IAmpOutputRenderer, AmpOutputRenderer>();
            serviceCollection.Services.AddSingleton<IAmpDotvvmViewBuilder, AmpViewBuilder>();
            serviceCollection.Services.AddSingleton<IAmpControlTransformsRegistry, AmpControlTransformsRegistry>();
            serviceCollection.Services.AddSingleton<IAmpValidator, AmpValidator>();
            serviceCollection.Services.AddSingleton<IAmpRouteManager, AmpRouteManager>();
            serviceCollection.Services.AddFactory<IAmpStylesheetResourceCollection, AmpStylesheetResourceCollection>();
            serviceCollection.Services.AddSingleton<IAmpExternalResourceMetadataCache, AmpExternalResourceMetadataCache>();
            
            serviceCollection.Services.AddSingleton<DotvvmAmpConfiguration>(provider =>
            {
                var registry = provider.GetService<IAmpControlTransformsRegistry>();
                var routeManager = provider.GetService<IAmpRouteManager>();
                var logger = provider.GetService<ILogger>();
                var externalResourceMetadataCache = provider.GetService<IAmpExternalResourceMetadataCache>();
                var config = new DotvvmAmpConfiguration(registry, routeManager,externalResourceMetadataCache);
                RegisterTransforms(config, logger);
                modifyConfiguration?.Invoke(config);
                return config;
            });
        }

        private static void RegisterTransforms(DotvvmAmpConfiguration configuration, ILogger logger)
        {
            configuration.ControlTransforms.Register(new AllControlTransform(configuration, logger));

            configuration.ControlTransforms.Register(new DotvvmViewTransform(configuration, logger));
            configuration.ControlTransforms.Register(new HtmlTagTransform(configuration, logger));
            configuration.ControlTransforms.Register(new HeadTagTransform(configuration, logger));
            configuration.ControlTransforms.Register(new BodyTagTransform(configuration, logger));
            configuration.ControlTransforms.Register(new StylesheetLinkTransform(configuration, logger));
            configuration.ControlTransforms.Register(new RouteLinkTransform(configuration, logger));
            configuration.ControlTransforms.Register(new GridViewTransform(configuration, logger));
            configuration.ControlTransforms.Register(new GridViewColumnTransform(configuration,logger));
            configuration.ControlTransforms.Register(new GridViewTextColumnTransform(configuration,logger));
            configuration.ControlTransforms.Register(new GridViewTemplateColumnTransform(configuration,logger));
            configuration.ControlTransforms.Register(new AmpLayoutTransform(configuration,logger));
            configuration.ControlTransforms.Register(new AmpImageTransform(configuration,logger));
            configuration.ControlTransforms.Register(new AmpDecoratorTransform(configuration,logger));
            configuration.ControlTransforms.Register(new RepeaterTransform(configuration,logger));

        }
    }
}