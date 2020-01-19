using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace TestSamples.ViewModels
{
    public class QueryStringLocalizableViewModel : DotvvmViewModelBase
    {
        public override Task Init()
        {
            var value = Context.Query.ContainsKey("lang") ? Context.Query["lang"] : "";
            Context.ChangeCurrentCulture(string.IsNullOrWhiteSpace(value) ? "en-US" : value);
            return base.Init();
        }
    }
}