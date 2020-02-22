namespace DotVVM.AMP
{
    public class AmpRouteBuilder : IAmpRouteBuilder
    {
        public virtual string BuildAmpRouteName(string dotvvmPageRouteName)
        {
            return $"{dotvvmPageRouteName}-amp";
        }

        public virtual string BuildAmpUrl(string dotvvmPageUrl)
        {
            return $"amp/{dotvvmPageUrl}";
        }
    }
}