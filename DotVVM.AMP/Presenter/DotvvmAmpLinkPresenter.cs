using System.Threading.Tasks;
using DotVVM.AMP.DotvvmResources;
using DotVVM.AMP.Routing;
using DotVVM.Framework.Hosting;

namespace DotVVM.AMP.Presenter
{
    public class DotvvmAmpLinkPresenter : IDotvvmPresenter
    {
        private readonly IAmpRouteManager ampRouteManager;
        protected IDotvvmPresenter NextPresenter { get; set; }

        public DotvvmAmpLinkPresenter(IDotvvmPresenter nextPresenter, IAmpRouteManager ampRouteManager)
        {
            this.ampRouteManager = ampRouteManager;
            NextPresenter = nextPresenter;
        }

        public virtual Task ProcessRequest(IDotvvmRequestContext context)
        {
            var resource = new LinkToAmpPageResource(ampRouteManager);
            context.ResourceManager.AddRequiredResource(resource);
            return NextPresenter.ProcessRequest(context);
        }
    }
}