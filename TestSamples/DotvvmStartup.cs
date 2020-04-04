﻿using DotVVM.AMP;
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

            config.RouteTable.AddWithAmp("ContentPlaceHolderPage", "ContentPlaceHolderPage", "Views/ControlSamples/ContentPlaceHolder/ContentPlaceHolderPage.dothtml", config);
            config.RouteTable.AddWithAmp("ContentPlaceHolderPage_ContentTest", "ContentPlaceHolderPage_ContentTest", "Views/ControlSamples/ContentPlaceHolder/ContentPlaceHolderPage_ContentTest.dothtml", config);
            config.RouteTable.AddWithAmp("DoubleContentPlaceHolderPage_ContentTest", "DoubleContentPlaceHolderPage_ContentTest", "Views/ControlSamples/ContentPlaceHolder/DoubleContentPlaceHolderPage_ContentTest.dothtml", config);

            config.RouteTable.AddWithAmp("Literal", "Literal", "Views/ControlSamples/Literal/Literal.dothtml", config);
            config.RouteTable.AddWithAmp("HtmlLiteral", "HtmlLiteral", "Views/ControlSamples/HtmlLiteral/HtmlLiteral.dothtml", config);

            config.RouteTable.AddWithAmp("CssSingle", "CssSingle", "Views/SimplePages/WithSingleCss.dothtml", config);

        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            // register code-only controls and markup controls
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            config.Resources.Register("styles", new StylesheetResource(new UrlResourceLocation("Resources/styles.css")));
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
