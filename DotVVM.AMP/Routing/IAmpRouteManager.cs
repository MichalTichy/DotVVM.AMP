using DotVVM.Framework.Routing;

namespace DotVVM.AMP.Routing
{
    public interface IAmpRouteManager
    {
        string BuildAmpRouteName(string dotvvmPageRouteName);
        string BuildAmpUrl(string dotvvmPageUrl);
        void RegisterRoute(string ampRoute, string fullPageRoute);
        string GetFullPageRouteName(string ampRouteName);
    }
}