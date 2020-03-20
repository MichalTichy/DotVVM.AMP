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
    }
}