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
        [Fact]
        public void HeadDoesHaveJsBoilerPlate()
        {
            RunInAllBrowsers(SampleUrl, wrapper =>
            {
                var headScripts = wrapper.FindElements("head > script", By.CssSelector);
                var boilerPlate = headScripts.SingleOrDefault(t =>
                    t.HasAttribute("async") &&
                    (t.GetAttribute("src")?.Equals("https://cdn.ampproject.org/v0.js") ?? false) &&
                    (t.GetAttribute("type")?.Equals("text/javascript") ?? false));

                Assert.True(boilerPlate != null, "Unable to find amp js boilerplate. Ensure that head contains script with correct src and type");
            });
        }
        [Fact]
        public void HeadDoesHaveMetaViewport()
        {
            RunInAllBrowsers(SampleUrl, wrapper =>
            {
                var metaViewport = wrapper.Single("head > meta[name='viewport']", By.CssSelector);
                AssertUI.Attribute(metaViewport,"content", "width:device-width,minimum-scale:1");
            });
        }
    }
}