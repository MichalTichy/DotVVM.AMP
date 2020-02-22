using DotVVM.Framework.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotVVM.AMP
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDotvvmAmpSupport(this IDotvvmServiceCollection serviceCollection)
        {
            serviceCollection.Services.AddSingleton<IAmpPresenter, DotvvmAmpPresenter>();
        }
    }
}