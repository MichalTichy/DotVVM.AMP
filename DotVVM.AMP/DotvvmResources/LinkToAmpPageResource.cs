using System;
using DotVVM.AMP.AmpControls.Internal;
using DotVVM.AMP.Routing;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.AMP.DotvvmResources
{
    public class LinkToAmpPageResource : IResource
    {
        private readonly IAmpRouteManager routeManager;

        public LinkToAmpPageResource(IAmpRouteManager routeManager)
        {
            this.routeManager = routeManager;
        }
        public void Render(IHtmlWriter writer, IDotvvmRequestContext context, string resourceName)
        {
            new AmpLinkToAnotherVersion(routeManager.GetAmpPageRoute(context.Route), context.Route,true).Render(writer,context);
        }

        public ResourceRenderPosition RenderPosition => ResourceRenderPosition.Head;
        public string[] Dependencies { get; } = Array.Empty<string>();
    }
}