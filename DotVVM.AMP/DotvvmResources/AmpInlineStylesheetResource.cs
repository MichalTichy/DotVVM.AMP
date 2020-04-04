using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.AMP.DotvvmResources
{
    public class AmpInlineStylesheetResource : ResourceBase
    {
        protected List<IResourceLocation> resources;
        private Lazy<string> code;

        public AmpInlineStylesheetResource() : base(ResourceRenderPosition.Head)
        {
            this.resources = new List<IResourceLocation>();
        }

        public AmpInlineStylesheetResource(ICollection<IResourceLocation> resourceLocations) : base(ResourceRenderPosition.Head)
        {
            resources = resourceLocations.ToList();
        }

        public void AddResource(IResourceLocation resource)
        {
            resources.Add(resource);
        }

        public void AddResource(string inlineStyle)
        {
            resources.Add(new InlineResourceLocation(inlineStyle));
        }
        public override void Render(IHtmlWriter writer, IDotvvmRequestContext context, string resourceName)
        {
            if (this.code == null)
            {
                var newCode = new Lazy<string>(() =>
                {
                    var getCodeTasks = resources.Select(t=>GetCode(t,context)).ToArray();
                    Task.WaitAll(getCodeTasks);
                    var strings = getCodeTasks.Select(t=>t.Result).ToArray();
                    var c = string.Join(string.Empty, strings);
                    CheckStyleCodeForPrematureEnding(c);
                    c = MinifiCss(c);
                    return c;
                });
                // assign the `newValue` into `this.code` iff it's still null
                Interlocked.CompareExchange(ref this.code, value: newCode, comparand: null);
            }


            var code = this.code.Value;

            if (!string.IsNullOrWhiteSpace(code))
            {
                writer.AddAttribute("amp-custom", null);
                writer.RenderBeginTag("style");
                writer.WriteUnencodedText(code);
                writer.RenderEndTag();
            }
        }

        private async Task<string> GetCode(IResourceLocation resource, IDotvvmRequestContext context)
        {
            if (resource is ILocalResourceLocation localResourceLocation)
            {
                return localResourceLocation.ReadToString(context);
            }

            if (resource is UrlResourceLocation urlResourceLocation)
            {
                return await GetRemoteResource(urlResourceLocation,context);
            }

            throw new ArgumentException($@"Unable to get code for resource.");
        }

        private static async Task<string> GetRemoteResource(UrlResourceLocation resource,IDotvvmRequestContext context)
        {
            using (var web = new WebClient())
            {
                string address = context.TranslateVirtualPath(resource.Url);

                if (!Uri.TryCreate(address, UriKind.Absolute, out var result))
                {
                    var baseUrl=new Uri(context.HttpContext.Request.Url.GetLeftPart(UriPartial.Authority));
                    address = context.TranslateVirtualPath($"~/{resource.Url}");
                    result = new Uri(baseUrl, address);
                };

                using (Stream stream = web.OpenRead(result))
                using (var reader = new StreamReader(stream ?? throw new ArgumentException($"Unable to load resource at {resource}")))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }

        private string MinifiCss(string css)
        {
            //This was taken from Mads Kristensen blog https://madskristensen.net/blog/efficient-stylesheet-minification-in-c
            css = Regex.Replace(css, @"[a-zA-Z]+#", "#");

            css = Regex.Replace(css, @"[\n\r]+\s*", string.Empty);

            css = Regex.Replace(css, @"\s+", " ");

            css = Regex.Replace(css, @"\s?([:,;{}])\s?", "$1");

            css = css.Replace(";}", "}");

            css = Regex.Replace(css, @"([\s:]0)(px|pt|%|em)", "$1");

            css = Regex.Replace(css, @"/\*[\d\D]*?\*/", string.Empty);

            return css;
        }

        internal static void CheckStyleCodeForPrematureEnding(string code)
        {
            if (code?.IndexOf("</style", StringComparison.OrdinalIgnoreCase) >= 0)
                throw new ArgumentException($"Inline style can't contain `</style`.");
        }

        public void AddDependencies(string[] resourceDependencies)
        {
            Dependencies = Dependencies.Union(resourceDependencies).ToArray();
        }
    }
}