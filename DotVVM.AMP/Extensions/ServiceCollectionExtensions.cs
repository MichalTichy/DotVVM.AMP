using System;
using DotVVM.AMP.Config;
using DotVVM.AMP.Presenter;
using DotVVM.AMP.Renderers;
using DotVVM.AMP.Renderers.RenderStrategies;
using Microsoft.Extensions.DependencyInjection;

namespace DotVVM.AMP.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDotvvmAmpSupport(this IDotvvmServiceCollection serviceCollection, Action<DotvvmAmpConfig> modifyConfiguration = null)
        {
            serviceCollection.Services.AddSingleton<IAmpPresenter, DotvvmAmpPresenter>();
            serviceCollection.Services.AddSingleton<IAmpOutputRenderer, AmpOutputRenderer>();
            serviceCollection.Services.AddSingleton<IAmpRenderStrategiesRegistry, AmpRenderStrategiesRegistry>();

            serviceCollection.Services.AddSingleton<DotvvmAmpConfig>(provider =>
            {
                var registry = provider.GetService<IAmpRenderStrategiesRegistry>();
                var config = new DotvvmAmpConfig(registry);
                RegisterRenderStrategies(config);
                modifyConfiguration?.Invoke(config);
                return config;
            });
        }

        private static void RegisterRenderStrategies(DotvvmAmpConfig config)
        {
            config.RenderStrategiesRegistry.Register(new HtmlGenericControlRenderStrategy(config));
        }
    }
}