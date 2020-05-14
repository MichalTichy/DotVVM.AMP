using System;
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
        private readonly Func<IAmpStylesheetResourceCollection> ampStylesheetResourceCollectionFactory;

        public DotvvmAmpPresenter(DotvvmConfiguration configuration,Func<IAmpStylesheetResourceCollection> ampStylesheetResourceCollectionFactory, IAmpDotvvmViewBuilder viewBuilder, IViewModelLoader viewModelLoader, IViewModelSerializer viewModelSerializer, IAmpOutputRenderer outputRender, ICsrfProtector csrfProtector, IViewModelParameterBinder viewModelParameterBinder, IStaticCommandServiceLoader staticCommandServiceLoader) : base(configuration, viewBuilder, viewModelLoader, viewModelSerializer, outputRender, csrfProtector, viewModelParameterBinder, staticCommandServiceLoader)
        {
            this.ampStylesheetResourceCollectionFactory = ampStylesheetResourceCollectionFactory;
        }

        public async Task ProcessRequest(IDotvvmRequestContext context)
        {
            context.ResourceManager.RegisterProcessor(GetCssBundlerProcessor());
            context.ResourceManager.RegisterProcessor(GetAmpAllowedResourcesProcessor());
            await base.ProcessRequest(context);
        }

        protected static AmpResourcesProcessor GetAmpAllowedResourcesProcessor()
        {
            return new AmpResourcesProcessor();
        }

        protected virtual AmpCustomCssResourceProcessor GetCssBundlerProcessor()
        {
            return new AmpCustomCssResourceProcessor(ampStylesheetResourceCollectionFactory);
        }
    }
}