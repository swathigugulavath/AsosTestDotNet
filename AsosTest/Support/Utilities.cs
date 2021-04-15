using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace AsosTest.Support
{
    public class Utilities  
    {
        private string _loadUrl;
        private readonly TimeSpan _waitTimeSpan = new TimeSpan(0, 0, 20);
        protected Utilities(string loadUrl = "")
        {
            this._loadUrl = loadUrl;
        }

        public void NavigateTo()
        {
            BasePage.Driver.Navigate().GoToUrl(_loadUrl);
            BasePage.Driver.Manage().Window.Maximize();
        }
        public void Click(IWebElement webElement)
        {
            WaitForElementDisplayed(webElement);
            try
            {
                webElement.Click();
            }
            catch
            {
                IJavaScriptExecutor jse = (IJavaScriptExecutor)BasePage.Driver;
                jse.ExecuteScript("arguments[0].scrollIntoView()", webElement);
                webElement.Click();
            }
            WaitForPageToLoad();
            WaitForPageToLoad();
        }
        public string GetTitle()
        {
            return BasePage.Driver.Title;
        }
        public void ClickClearSendKeys(IWebElement webElement, string value)
        {
            WaitForElementDisplayed(webElement);
            try
            {
                webElement.Click();
                webElement.Clear();
                webElement.SendKeys(value);
            }
            catch
            {
                IJavaScriptExecutor jse = (IJavaScriptExecutor)BasePage.Driver;
                jse.ExecuteScript("arguments[0].scrollIntoView()", webElement);
                webElement.Click();
                webElement.Clear();
                webElement.SendKeys(value);
            }
            WaitForPageToLoad();
        }
        protected void WaitForPageToLoad()
        {
            try
            {
                var wait = new WebDriverWait(BasePage.Driver, _waitTimeSpan);
                wait.Until(
                    d =>
                        ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState")
                        .ToString().Equals("complete"));
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public void WaitForElementDisplayed(IWebElement element)
        {
            var wait = new WebDriverWait(BasePage.Driver, _waitTimeSpan);
            Console.WriteLine(element.Displayed);
            wait.Until(d => element.Displayed && element.Enabled);
        }
    }
}
