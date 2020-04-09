using DotVVM.Framework.Routing;

namespace DotVVM.AMP.Routing
{
    public interface IAmpRouteManager
    {
        string BuildAmpRouteName(string dotvvmPageRouteName);
        string BuildAmpUrl(string dotvvmPageUrl);
        void RegisterRoute(RouteBase ampRoute, RouteBase fullPageRoute);
        RouteBase GetFullPageRoute(RouteBase ampRoute);
        string GetAmpPageRouteName(string fullPageName);
    }
}