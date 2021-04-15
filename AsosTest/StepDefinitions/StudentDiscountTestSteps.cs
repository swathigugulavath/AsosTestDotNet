using AsosTest.Pages;
using AsosTest.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace AsosTest.StepDefinitions
{
    [Binding]
    public class StudentDiscountTestSteps : Utilities
    {
        private readonly ScenarioContext _scenarioContext;
        private static string MorethanHundredcharacterString;
       
        private StudentDiscountPageObject StudentDiscountPageObject = new StudentDiscountPageObject();
        public StudentDiscountTestSteps(ScenarioContext scenarioContext) : base()
        {
            _scenarioContext = scenarioContext;
            
        }
        [Given(@"I have asos application running")]
        public void GivenIHaveAsosApplicationRunning()
        {
            StudentDiscountPageObject.NavigateTo();
            string pageTitle = GetTitle();
            Assert.IsTrue(pageTitle.Contains("ASOS Student Discount Code & Offers"), $"The page title [{pageTitle}] does not contain ASOS Student Discount Code & Offers");
        }

        [When(@"I enter customer '(.*)', '(.*)', '(.*)', '(.*)', '(.*)', '(.*)' ,'(.*)' details")]
        public void WhenIEnterCustomerDetails(string FirstName, string LastNmae, string Country, string RegesterdEmailAddress, string StudentEmailAddress, int YearOfGraduation, string InterestedCollection)
        {
            ClickClearSendKeys(StudentDiscountPageObject.FirstName, FirstName);
            ClickClearSendKeys(StudentDiscountPageObject.LastName, LastNmae);
            StudentDiscountPageObject.Country.SelectByText(Country);
            ClickClearSendKeys(StudentDiscountPageObject.RegisteredAsosEmailId, RegesterdEmailAddress);
            ClickClearSendKeys(StudentDiscountPageObject.StudentEmailAddress, StudentEmailAddress);
            StudentDiscountPageObject.selectGraduationYear(YearOfGraduation.ToString());
            StudentDiscountPageObject.selectInterestedCollection(InterestedCollection);

        }

        [When(@"I click on submit button")]
        public void WhenIClickOnSubmitButton()
        {
            Click(StudentDiscountPageObject.Submit);
        }

        [Then(@"user should be see successfull message for student discount enrolement")]
        public void ThenUserShouldBeSeeSuccessfullMessageForStudentDiscountEnrolement()
        {
            string confirmationMessage = StudentDiscountPageObject.getconfirmationmessage();
            Assert.IsTrue(confirmationMessage.Contains("We've just sent a verification email to your student email address! Click on the link inside to get your discount code..."), "Incorrect confirmation message is displayed");
        }

        [Then(@"user should see error messages for empty fields")]
        public void ThenUserShouldSeeErrorMessagesForEmptyFields()
        {
            Assert.IsTrue(StudentDiscountPageObject.FirstnameError.Text.Equals("Please enter your first name"), "Incorrect First name error message is displayed");
            Assert.IsTrue(StudentDiscountPageObject.AsosEmailIDError.Text.Equals("Please enter a valid email address"), "Incorrect asos email Id error message is displayed");
            Assert.IsTrue(StudentDiscountPageObject.StudentEmailError.Text.Equals("Please enter a valid email address"), "Incorrect student email ID error message is displayed");
            Assert.IsTrue(StudentDiscountPageObject.GraduationYearError.Text.Equals("Please select which year you will graduate in"), "Incorrect GraduationYear error message is displayed");
            Assert.IsTrue(StudentDiscountPageObject.FashionGenderError.Text.Equals("Please select an option"), "Incorrect Fashion Gender error message is displayed");
        }

        [When(@"check the terms and conditions before submitting the details")]
        public void WhenCheckTheTermsAndConditionsBeforeSubmittingTheDetails()
        {
            Click(StudentDiscountPageObject.TermsAndConditionsLink);
            Assert.IsTrue(StudentDiscountPageObject.TermsAndConditionsDetails.Text.Contains("TERMS & CONDITIONS"), "Incorrect TERMS & CONDITIONS are displayed");
            Click(StudentDiscountPageObject.CloseTermsAndConditions);
        }

        [When(@"I enter more than hundred characters in firstname and last name fields")]
        public void WhenIEnterMoreThanHundredCharactersInFirstnameAndLastNameFields()
        {
            MorethanHundredcharacterString = "Wikipedia is an online free-content encyclopedia project helping to create a world in which everyone can freely share in the sum of all knowledge. It is supported by the Wikimedia Foundation and based on a model of freely editable content.";
            ClickClearSendKeys(StudentDiscountPageObject.FirstName, MorethanHundredcharacterString);
            ClickClearSendKeys(StudentDiscountPageObject.LastName, MorethanHundredcharacterString);

        }

        [Then(@"user should see only first hundred characters allowed in first name and last name fields")]
        public void ThenUserShouldSeeOnlyFirstHundredCharactersAllowedInFirstNameAndLastNameFields()
        {
            string GetFirstName = StudentDiscountPageObject.FirstName.GetAttribute("value");
            string GetLastName = StudentDiscountPageObject.LastName.GetAttribute("value");
            Assert.IsTrue(MorethanHundredcharacterString.Substring(0, 100) == GetFirstName, "First hudred characters are not displayed in first name");
            Assert.IsTrue(GetFirstName.Length == 100);
            Assert.IsTrue(MorethanHundredcharacterString.Substring(0, 100) == GetLastName, "First hudred characters are not displayed in last name");
            Assert.IsTrue(GetLastName.Length == 100);

        }




    }
}
