using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace TestSamples.ViewModels
{
    public class DefaultViewModel : DotvvmViewModelBase
    {
        public string Title { get; set; }
        public List<RouteData> Routes { get; set; }

        public override Task Init()
        {
            Routes = Context.Configuration.RouteTable
                .Select(r => new RouteData()
                {
                    RouteName = r.RouteName,
                    Url = Context.TranslateVirtualPath(r.BuildUrl(r.DefaultValues))
                })
                .ToList();

            return base.Init();
        }

        public class RouteData
        {
            public string Url { get; set; }

            public string RouteName { get; set; }
        }
    }
}