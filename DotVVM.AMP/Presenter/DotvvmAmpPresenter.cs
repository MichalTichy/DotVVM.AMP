using System.Threading.Tasks;
using DotVVM.AMP.DotvvmResources;
using DotVVM.AMP.Renderers;
using DotVVM.AMP.ViewBuilder;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Runtime;
using DotVVM.Framework.Security;
using DotVVM.Framework.ViewModel.Serialization;

namespace DotVVM.AMP.Presenter
{
    public class DotvvmAmpPresenter : DotvvmPresenter, IAmpPresenter
    {
        public DotvvmAmpPresenter(DotvvmConfiguration configuration, IAmpDotvvmViewBuilder viewBuilder, IViewModelLoader viewModelLoader, IViewModelSerializer viewModelSerializer, IAmpOutputRenderer outputRender, ICsrfProtector csrfProtector, IViewModelParameterBinder viewModelParameterBinder, IStaticCommandServiceLoader staticCommandServiceLoader) : base(configuration, viewBuilder, viewModelLoader, viewModelSerializer, outputRender, csrfProtector, viewModelParameterBinder, staticCommandServiceLoader)
        {
        }

        public async Task ProcessRequest(IDotvvmRequestContext context)
        {
            context.ResourceManager.RegisterProcessor(new AmpCustomCssResourceProcessor());
            await base.ProcessRequest(context);
        }
    }
}