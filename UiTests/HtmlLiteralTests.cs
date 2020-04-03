using System.Linq;
using DotVVM.AMP.DotvvmResources;
using OpenQA.Selenium;
using Riganti.Selenium.Core;
using Xunit;

namespace UiTests
{
    public class HtmlLiteralTests : UITestBase
    {
        private const string SampleUrl = "/amp/HtmlLiteral";

        [Theory]
        [InlineData("wrapper-tag", true)]
        [InlineData("no-wrappert-tag", false)]
        public void HtmlTagHasAmpAttribute(string dataui, bool hasWrapper)
        {
            RunInAllBrowsers(SampleUrl, wrapper =>
            {
                var textWrapper = wrapper.Single(dataui, SelectByDataUiId);

                if (hasWrapper)
                {
                    textWrapper = textWrapper.Single("div", By.TagName);
                }

                var bold = textWrapper.Single("b", By.TagName);
                AssertUI.InnerTextEquals(textWrapper, "Hello value");
                AssertUI.InnerTextEquals(bold, "value");
            });
        }
    }
}