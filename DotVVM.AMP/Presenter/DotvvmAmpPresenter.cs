using System.Threading.Tasks;
using DotVVM.AMP.DotvvmResources;
using DotVVM.AMP.Renderers;
using DotVVM.AMP.Validator;
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
        private readonly DotvvmConfiguration _configuration;
        private readonly IAmpStylesheetResourceCollection _ampStylesheetResourceCollection;

        public DotvvmAmpPresenter(DotvvmConfiguration configuration,IAmpStylesheetResourceCollection ampStylesheetResourceCollection, IAmpDotvvmViewBuilder viewBuilder, IViewModelLoader viewModelLoader, IViewModelSerializer viewModelSerializer, IAmpOutputRenderer outputRender, ICsrfProtector csrfProtector, IViewModelParameterBinder viewModelParameterBinder, IStaticCommandServiceLoader staticCommandServiceLoader) : base(configuration, viewBuilder, viewModelLoader, viewModelSerializer, outputRender, csrfProtector, viewModelParameterBinder, staticCommandServiceLoader)
        {
            _configuration = configuration;
            _ampStylesheetResourceCollection = ampStylesheetResourceCollection;
        }

        public async Task ProcessRequest(IDotvvmRequestContext context)
        {
            context.ResourceManager.RegisterProcessor(new AmpCustomCssResourceProcessor(_ampStylesheetResourceCollection,_configuration.Resources));
            await base.ProcessRequest(context);
        }
    }
}