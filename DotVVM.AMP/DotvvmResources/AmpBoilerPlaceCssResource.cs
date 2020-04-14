using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.AMP.DotvvmResources
{
    public class AmpBoilerPlaceCssResource : ResourceBase, IAmpAllowedResource
    {

        public const string BoilerPlateCode = "body{-webkit-animation:-amp-start 8s steps(1,end) 0s 1 normal both;-moz-animation:-amp-start 8s steps(1,end) 0s 1 normal both;-ms-animation:-amp-start 8s steps(1,end) 0s 1 normal both;animation:-amp-start 8s steps(1,end) 0s 1 normal both}@-webkit-keyframes -amp-start{from{visibility:hidden}to{visibility:visible}}@-moz-keyframes -amp-start{from{visibility:hidden}to{visibility:visible}}@-ms-keyframes -amp-start{from{visibility:hidden}to{visibility:visible}}@-o-keyframes -amp-start{from{visibility:hidden}to{visibility:visible}}@keyframes -amp-start{from{visibility:hidden}to{visibility:visible}}";
        public const string BoilerPlateCodeNoScript = "body{-webkit-animation:none;-moz-animation:none;-ms-animation:none;animation:none}";
        public AmpBoilerPlaceCssResource() : base(ResourceRenderPosition.Head)
        {
        }

        public override void Render(IHtmlWriter writer, IDotvvmRequestContext context, string resourceName)
        {

            writer.AddAttribute("amp-boilerplate", null);
            writer.RenderBeginTag("style");
            writer.WriteUnencodedText(BoilerPlateCode);
            writer.RenderEndTag();

            writer.RenderBeginTag("noscript");

            writer.AddAttribute("amp-boilerplate", null);
            writer.RenderBeginTag("style");
            writer.WriteUnencodedText(BoilerPlateCodeNoScript);
            writer.RenderEndTag();

            writer.RenderEndTag();
        }
    }
}