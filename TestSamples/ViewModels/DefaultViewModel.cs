using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace TestSamples.ViewModels
{
    public class DefaultViewModel : DotvvmViewModelBase
    {
        public string Title { get; set; }
        public List<RouteData> NormalRoutes { get; set; }
        public List<RouteData> AmpRoutes { get; set; }

        public override Task Init()
        {
            var routes = Context.Configuration.RouteTable
                .Select(r => new RouteData()
                {
                    RouteName = r.RouteName,
                    Url = Context.TranslateVirtualPath(r.BuildUrl(r.DefaultValues))
                })
                .ToList();
            NormalRoutes = routes.Where(t => !t.RouteName.EndsWith("-amp")).ToList();
            AmpRoutes = routes.Where(t => t.RouteName.EndsWith("-amp")).ToList();
            return base.Init();
        }

        public class RouteData
        {
            public string Url { get; set; }

            public string RouteName { get; set; }
        }
    }
}