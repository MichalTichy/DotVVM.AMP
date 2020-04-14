using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DotVVM.AMP.Validator;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ResourceManagement;
using ExCSS;

namespace DotVVM.AMP.DotvvmResources
{
    public class AmpStylesheetResourceCollection : IAmpStylesheetResourceCollection
    {
        private readonly IAmpValidator ampValidator;

        public AmpStylesheetResourceCollection(IAmpValidator ampValidator)
        {
            this.ampValidator = ampValidator;
        }
        public List<IResourceLocation> resources = new List<IResourceLocation>();
        protected bool Modified = false;
        public void Add(IResourceLocation resource)
        {
            resources.Add(resource);
            Modified = true;
        }

        protected string ampCustomCode = null;
        protected string ampKeyFramesCode = null;

        public async Task<string> GetAmpCustomCode(IDotvvmRequestContext context)
        {
            if (ampCustomCode == null || Modified)
            {   
                await ProcessCss(context);
            }

            return ampCustomCode;
        }
        public async Task<string> GetAmpKeyframesCode(IDotvvmRequestContext context)
        {
            if (ampKeyFramesCode == null || Modified)
            {   
                await ProcessCss(context);
            }

            return ampCustomCode;
        }


        private async Task<string> GetCode(IResourceLocation resource, IDotvvmRequestContext context)
        {
            if (resource is ILocalResourceLocation localResourceLocation)
            {
                return localResourceLocation.ReadToString(context);
            }

            if (resource is UrlResourceLocation urlResourceLocation)
            {
                return await GetRemoteResource(urlResourceLocation, context);
            }

            throw new ArgumentException($@"Unable to get code for resource.");
        }

        private static async Task<string> GetRemoteResource(UrlResourceLocation resource, IDotvvmRequestContext context)
        {
            using (var web = new WebClient())
            {
                string address = context.TranslateVirtualPath(resource.Url);

                if (!Uri.TryCreate(address, UriKind.Absolute, out var result))
                {
                    var baseUrl = new Uri(context.HttpContext.Request.Url.GetLeftPart(UriPartial.Authority));
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

        private async Task ProcessCss(IDotvvmRequestContext context)
        {

            var getCodeTasks = resources.Select(t => GetCode(t, context)).ToArray();
            Task.WaitAll(getCodeTasks);
            var strings = getCodeTasks.Select(t => t.Result).ToArray();
            var code = string.Join(string.Empty, strings);
            
            if (string.IsNullOrWhiteSpace(code))
            {
                ampCustomCode = string.Empty;
                ampKeyFramesCode = string.Empty;
                return;
            }

            CheckStyleCodeForPrematureEnding(code);

            var parser = new ExCSS.StylesheetParser();
            var stylesheet = await parser.ParseAsync(code);

            var styleRules=new StringBuilder();
            var keyframeRules=new StringBuilder();
            ampValidator.CheckStylesheet(stylesheet);

            foreach (var styleRule in stylesheet.StyleRules.OfType<IStyleRule>())
            {
                styleRules.Append(styleRule.ToCss());
            }

            foreach (var keyFrame in stylesheet.StyleRules.OfType<IKeyframesRule>())
            {
                keyframeRules.Append(keyFrame.ToCss());
            }

            ampCustomCode = MinifiCss(styleRules.ToString());
            ampKeyFramesCode = MinifiCss(keyframeRules.ToString());
        }
        internal static void CheckStyleCodeForPrematureEnding(string code)
        {
            if (code?.IndexOf("</style", StringComparison.OrdinalIgnoreCase) >= 0)
                throw new ArgumentException($"Inline style can't contain `</style`.");
        }
    }
}