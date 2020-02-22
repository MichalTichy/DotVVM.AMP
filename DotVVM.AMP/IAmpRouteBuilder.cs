namespace DotVVM.AMP
{
    public interface IAmpRouteBuilder
    {
        string BuildAmpRouteName(string dotvvmPageRouteName);
        string BuildAmpUrl(string dotvvmPageUrl);
    }
}