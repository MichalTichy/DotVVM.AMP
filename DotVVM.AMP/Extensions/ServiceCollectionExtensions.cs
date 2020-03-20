using System;
using System.Linq;
using DotVVM.AMP.Config;
using DotVVM.AMP.ControlTransforms;
using DotVVM.AMP.ControlTransforms.Transforms;
using DotVVM.AMP.Presenter;
using DotVVM.AMP.Renderers;
using DotVVM.AMP.Validator;
using DotVVM.AMP.ViewBuilder;
using DotVVM.AMP.Writer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace DotVVM.AMP.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDotvvmAmpSupport(this IDotvvmServiceCollection serviceCollection, Action<DotvvmAmpConfig> modifyConfiguration = null)
        {
            serviceCollection.Services.AddSingleton<IAmpPresenter, DotvvmAmpPresenter>();
            serviceCollection.Services.AddSingleton<IAmpOutputRenderer, AmpOutputRenderer>();
            serviceCollection.Services.AddSingleton<IAmpDotvvmViewBuilder, AmpViewBuilder>();
            serviceCollection.Services.AddSingleton<IAmpControlTransformsRegistry, AmpControlTransformsRegistry>();
            serviceCollection.Services.AddSingleton<IAmpValidator, AmpValidator>();

            serviceCollection.Services.AddSingleton<DotvvmAmpConfig>(provider =>
            {
                var registry = provider.GetService<IAmpControlTransformsRegistry>();
                var config = new DotvvmAmpConfig(registry);
                RegisterTransforms(config);
                modifyConfiguration?.Invoke(config);
                return config;
            });
        }

        private static void RegisterTransforms(DotvvmAmpConfig config)
        {
            config.ControlTransforms.Register(new HtmlTagTransform());
        }
    }
}