using System.Threading.Tasks;
using DotVVM.AMP.Renderers;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Runtime;
using DotVVM.Framework.Security;
using DotVVM.Framework.ViewModel.Serialization;

namespace DotVVM.AMP.Presenter
{
    public class DotvvmAmpPresenter : IAmpPresenter
    {
        public DotvvmAmpPresenter(DotvvmConfiguration configuration, IDotvvmViewBuilder viewBuilder, IViewModelLoader viewModelLoader, IViewModelSerializer viewModelSerializer,
            IAmpOutputRenderer outputRender, ICsrfProtector csrfProtector, IViewModelParameterBinder viewModelParameterBinder, IStaticCommandServiceLoader staticCommandServiceLoader)
        {
            InternalPresenter = new DotvvmPresenter(configuration, viewBuilder, viewModelLoader, viewModelSerializer, outputRender, csrfProtector, viewModelParameterBinder, staticCommandServiceLoader);
        }

        public DotvvmPresenter InternalPresenter { get; set; }

        public async Task ProcessRequest(IDotvvmRequestContext context)
        {
            await InternalPresenter.ProcessRequest(context);
        }
    }
}