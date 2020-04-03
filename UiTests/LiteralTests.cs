using System.Linq;
using DotVVM.AMP.DotvvmResources;
using OpenQA.Selenium;
using Riganti.Selenium.Core;
using Xunit;

namespace UiTests
{
    public class LiteralTests : UITestBase
    {
        private const string SampleUrl = "/amp/Literal";

        [Theory]
        [InlineData("hardcoded", true)]
        [InlineData("binding", true)]
        [InlineData("date", true, "1/1/2000")]
        [InlineData("hardcoded-no-span", false)]
        [InlineData("binding-no-span", false)]
        [InlineData("data-no-span", false, "1/1/2000")]
        public void HtmlTagHasAmpAttribute(string dataui, bool hasSpanWrapper, string expectedText = "Hello")
        {
            RunInAllBrowsers(SampleUrl, wrapper =>
            {
                var textWrapper = wrapper.Single(dataui, SelectByDataUiId);

                if (hasSpanWrapper)
                {
                    textWrapper = textWrapper.Single("span", By.TagName);
                }

                AssertUI.InnerTextEquals(textWrapper, expectedText);
            });
        }
    }
}