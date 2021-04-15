using AsosTest.Support;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Configuration;

namespace AsosTest.Pages
{
    public class StudentDiscountPageObject : Utilities
    {
        private readonly IWebDriver _driver;
        private static readonly string _baseUrl = ConfigurationManager.AppSettings["HomePageUrl"];
        private static readonly string _startUrl = _baseUrl + ConfigurationManager.AppSettings["StudentDiscountPage"];
        private static string strGraduationYear ;

        #region Page Elements declaration
        public StudentDiscountPageObject() : base(_startUrl) { _driver = BasePage.Driver; }
        public IWebElement FirstName => _driver.FindElement(By.Id("firstName"));
        public IWebElement LastName => _driver.FindElement(By.Id("lastName"));
        public SelectElement Country => new SelectElement(_driver.FindElement(By.Id("territory")));
        public IWebElement RegisteredAsosEmailId => _driver.FindElement(By.Id("asosEmail"));
        public IWebElement StudentEmailAddress => _driver.FindElement(By.Id("studentEmail"));
        public IWebElement GraduationYear => _driver.FindElement(By.XPath("//*[@id='"+ strGraduationYear + "']"));
        public IWebElement Menswear => _driver.FindElement(By.XPath("//label[@for='Male']"));
        public IWebElement Womenswear => _driver.FindElement(By.XPath("//label[@for='Female']"));
        public IWebElement SignmeUpForNewsletter => _driver.FindElement(By.Id("asosEmailOptin"));
        public IWebElement Submit => _driver.FindElement(By.Id("submitButton"));
        public IWebElement confirmationMessage => _driver.FindElement(By.XPath("//*[@id='creative']/div[2]/section[2]/div/div"));
        public IWebElement FirstnameError => _driver.FindElement(By.Id("firstName-error"));
        public IWebElement AsosEmailIDError => _driver.FindElement(By.Id("asosEmail-error"));
        public IWebElement StudentEmailError => _driver.FindElement(By.Id("studentEmail-error"));
        public IWebElement GraduationYearError => _driver.FindElement(By.Id("graduationYear-error"));
        public IWebElement FashionGenderError => _driver.FindElement(By.Id("fashionGender-error"));
        public IWebElement TermsAndConditionsLink => _driver.FindElement(By.XPath("//a[@class='js-openModal']"));
        public IWebElement TermsAndConditionsDetails => _driver.FindElement(By.XPath("//*[@id='creative']/div[1]/div[3]"));
        public IWebElement CloseTermsAndConditions => _driver.FindElement(By.XPath("//*[@id='creative']/div[1]/div[3]/button"));

        #endregion
        public void selectGraduationYear(string yearOfGraduation)
        {
            strGraduationYear = yearOfGraduation;
            GraduationYear.Click();
        }
        public void selectInterestedCollection(string strCollection)
        {
            if(strCollection == "Womenswear") { Womenswear.Click(); }
            if(strCollection == "Menswear") { Menswear.Click(); }
        }
        public string getconfirmationmessage()
        {
            WaitForElementDisplayed(confirmationMessage);
            return confirmationMessage.Text;
        }


    }
}
