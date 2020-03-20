using DotVVM.AMP;
using DotVVM.AMP.Config;
using DotVVM.AMP.Enums;
using DotVVM.AMP.Extensions;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;
using DotVVM.Framework.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TestSamples
{
    public class DotvvmStartup : IDotvvmStartup, IDotvvmServiceConfigurator
    {
        // For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {

            config.AddDotvvmAmp();
            ConfigureRoutes(config, applicationPath);
            ConfigureControls(config, applicationPath);
            ConfigureResources(config, applicationPath);
        }

        private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteTable.Add("Default", "", "Views/Default.dothtml");
            config.RouteTable.Add("TestRoute", "ControlSamples/RouteLink/TestRoute/{Id}", "Views/ControlSamples/RouteLink/TestRoute.dothtml", new { Id = 0 });

            config.RouteTable.AddWithAmp("empty", "empty", "Views/SimplePages/Empty.dothtml", config);
        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            // register code-only controls and markup controls
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
        }

        public void ConfigureServices(IDotvvmServiceCollection options)
        {
            options.AddDotvvmAmpSupport(config =>
                {
                    config.AttributeErrorHandlingMode = ErrorHandlingMode.LogAndIgnore;
                });
            options.AddDefaultTempStorages("temp");
        }
    }
}
