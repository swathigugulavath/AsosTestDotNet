using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Configuration;
using TechTalk.SpecFlow;

namespace AsosTest.Support
{
    [Binding]
    public class BasePage
    {
        private readonly ScenarioContext _scenarioContext;

        public BasePage(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        private readonly TimeSpan _waitTimeSpan = new TimeSpan(0, 0, 60);
        public static string Browser = ConfigurationManager.AppSettings["browser"].ToLower();
        public static IWebDriver Driver { get; set; }

        [BeforeScenario]
        public void Login()
        {
            InitializeDriver();

        }
        [AfterScenario]
        public void AfterScenario()
        {
            _scenarioContext.Get<IWebDriver>().Quit();
            Driver = null;
            _scenarioContext.Clear();
            Console.WriteLine("********************After Scenario************************");
        }
        [AfterTestRun]
        public static void StopSeleniumAfterAllTests()
        {
            if (Driver == null)
            {
                return;
            }

            Driver.Quit();
            Driver.Dispose();
        }
        public void InitializeDriver()
        {
            Console.WriteLine("********************Before Scenario************************");
            if (Driver == null)
            {
                switch (Browser)
                {
                    case "firefox":
                        Driver = new FirefoxDriver();
                        break;

                    case "chrome":
                        Driver = new ChromeDriver();
                        break;

                    default:
                        throw new ConfigurationErrorsException($"The browser specified in the app.config file {Browser} is not supported");
                }
                Driver.Manage().Window.Maximize();
            }
            _scenarioContext.Set(Driver);
        }
    }
}