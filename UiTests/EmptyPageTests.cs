using System.Linq;
using DotVVM.AMP.DotvvmResources;
using OpenQA.Selenium;
using Riganti.Selenium.Core;
using Xunit;

namespace UiTests
{
    public class EmptyPageTests : UITestBase
    {
        private const string SampleUrl = "/amp/empty";

        [Fact]
        public void HtmlTagHasAmpAttribute()
        {
            RunInAllBrowsers(SampleUrl, wrapper =>
            {
                var html = wrapper.Single("html", By.TagName);
                AssertUI.HasAttribute(html, "amp");
            });
        }
        [Fact]
        public void HtmlTagLangAttribute()
        {
            RunInAllBrowsers(SampleUrl, wrapper =>
            {
                var html = wrapper.Single("html", By.TagName);
                AssertUI.Attribute(html, "lang", "en");
            });
        }

        [Fact]
        public void HtmlTagDoesNotHaveXmlsAttribute()
        {
            RunInAllBrowsers(SampleUrl, wrapper =>
            {
                var html = wrapper.Single("html", By.TagName);
                Assert.False(html.HasAttribute("xmlns"), "Element contains forbidden attribute xmlns");
            });
        }
        [Fact]
        public void HeadDoesHaveCssBoilerPlate()
        {
            RunInAllBrowsers(SampleUrl, wrapper =>
            {
                var cssBoilerplate = wrapper.Single("head > style[amp-boilerplate]", By.CssSelector);
                var cssBoilerplateNoScript = wrapper.Single("head > noscript", By.CssSelector);

                AssertUI.JsPropertyInnerHtmlEquals(cssBoilerplate, AmpBoilerPlaceCssResource.BoilerPlateCode);
                AssertUI.JsPropertyInnerHtmlEquals(cssBoilerplateNoScript, $"<style amp-boilerplate>{AmpBoilerPlaceCssResource.BoilerPlateCodeNoScript}</style>");
            });
        }
    }
}