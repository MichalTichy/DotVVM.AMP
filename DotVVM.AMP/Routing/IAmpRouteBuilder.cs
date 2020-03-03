namespace DotVVM.AMP.Routing
{
    public interface IAmpRouteBuilder
    {
        string BuildAmpRouteName(string dotvvmPageRouteName);
        string BuildAmpUrl(string dotvvmPageUrl);
    }
}