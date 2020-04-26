using System;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.AMP.AmpControls.Internal
{
    public class AmpRouteLink : HtmlGenericControl, IAmpControl
    {

        /// <summary>
        /// Gets or sets the name of the route in the route table.
        /// </summary>
        [MarkupOptions(AllowBinding = false, Required = true)]
        public string RouteName
        {
            get { return (string)originalRouteLink.GetValue(RouteLink.RouteNameProperty); }
            set { originalRouteLink.SetValue(RouteLink.RouteNameProperty, value); }
        }

        public bool Enabled
        {
            get { return (bool)originalRouteLink.GetValue(RouteLink.EnabledProperty)!; }
            set { originalRouteLink.SetValue(RouteLink.EnabledProperty, value); }
        }

        /// <summary>
        /// Gets or sets the suffix that will be appended to the generated URL (e.g. query string or URL fragment).
        /// </summary>
        public string? UrlSuffix
        {
            get { return (string?)originalRouteLink.GetValue(RouteLink.UrlSuffixProperty); }
            set { originalRouteLink.SetValue(RouteLink.UrlSuffixProperty, value); }
        }

        /// <summary>
        /// Gets or sets the text of the hyperlink.
        /// </summary>
        public string Text
        {
            get { return (string)originalRouteLink.GetValue(RouteLink.TextProperty)!; }
            set { originalRouteLink.SetValue(RouteLink.TextProperty, value ?? throw new ArgumentNullException(nameof(value))); }
        }

        public VirtualPropertyGroupDictionary<object> Params => new VirtualPropertyGroupDictionary<object>(originalRouteLink, RouteLink.ParamsGroupDescriptor);

        public VirtualPropertyGroupDictionary<object> QueryParameters => new VirtualPropertyGroupDictionary<object>(originalRouteLink, RouteLink.QueryParametersGroupDescriptor);

        private readonly RouteLink originalRouteLink;

        public AmpRouteLink(RouteLink originalRouteLink) : base("a")
        {
            this.originalRouteLink = originalRouteLink;
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            var url = RouteLinkHelpers.EvaluateRouteUrl(RouteName,originalRouteLink,context);

            if (!Enabled)
            {
                writer.AddAttribute("disabled", "disabled");
            }
            else
            {
                writer.AddAttribute("href",url);
            }
        }

        protected override void RenderContents(IHtmlWriter writer, IDotvvmRequestContext context)
        {

            if (!HasOnlyWhiteSpaceContent())
            {
                base.RenderContents(writer, context);
            }
            else
            {
                writer.WriteText(Text);
            }
        }
    }
}