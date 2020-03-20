using System;
using System.Globalization;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using Riganti.Selenium.AssertApi;
using Riganti.Selenium.Core;
using Riganti.Selenium.Core.Abstractions;
using Xunit;
using Xunit.Sdk;

namespace UiTests
{
    public abstract class UITestBase : SeleniumTest
    {
        protected UITestBase()
            : base(new TestOutputHelper())
        {
            CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = new CultureInfo("en-US");
        }

        protected int ShortWait { get; } = 200;

        protected int DefaultWait { get; } = 1000;

        protected int LongWait { get; } = 5000;

        protected static By SelectByDataUiId(string dataUiId)
            => By.CssSelector($"[data-ui='{dataUiId}']");

        protected void RunInAllBrowsers(Action<IBrowserWrapper> action)
        {
            AssertApiSeleniumTestExecutorExtensions.RunInAllBrowsers(this, action);
        }

        protected void RunInAllBrowsers(string pageUrl, Action<BrowserWrapper> action)
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl(pageUrl);
                action(browser as BrowserWrapper);
            });
        }

        protected void RunInAllBrowsers(string pageUrl, string controlUiId, Action<IBrowserWrapper, IElementWrapper> action)
        {
            RunInAllBrowsers(browser =>
            {
                browser.ActionWaitTime = 400;
                browser.NavigateToUrl(pageUrl);

                browser.Wait(100);
                var control = browser.Single(controlUiId, SelectByDataUiId);
                action(browser, control);
            });
        }

        protected string CollapseWhiteSpace(string input)
        {
            return Regex.Replace(input, @"\s+", " ").Trim();
        }
    }
}
