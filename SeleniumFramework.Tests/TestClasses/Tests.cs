using OpenQA.Selenium;
using SeleniumFramework.Core.Pages;
using SeleniumFramework.Core.Base;
using SeleniumFramework.Core.Utilities;
using SeleniumFramework.Core.Enums;

namespace SeleniumFramework.Tests.TestClasses
{
    [TestFixture]
    public class Tests
    {
        private IWebDriver _driver;
        private HomePage _homePage;
        private LoginPage _loginPage;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _driver = WebDriverFactory.GetWebDriver();
            _homePage = new HomePage(_driver);
            _homePage.GoToUrl();
            _loginPage = new LoginPage(_driver);
        }

        [Test, Description("Verifies amazon home page loaded by validating persisting logo on the left side"), Order(1)]
        public void VerifyHomePageLoad()
        {
            bool isLogoDisplayed = _homePage.ValidateElementVisibility(LocatorType.Id, "nav-logo-sprites");
            Assert.That(isLogoDisplayed);
            Logger.Log("Home Page loaded");
        }

        [Test, Description("Verifies Navigation bar is displayed"), Order(2)]
        public void VerifyNavigationBarIsDisplayed()
        {
            bool isNavBarVisible = _homePage.ValidateElementVisibility(LocatorType.Id, "nav-xshop");
            Assert.That(isNavBarVisible);
        }

        [Test, Description("Verify User logged in successfully"), Order(3)]
        public void VerifyUserLogin()
        {
            string nonLoggedInValue = _homePage.GetElementText(LocatorType.Id, "nav-link-accountList-nav-line-1");
            _homePage.NavigateToLoginPage();
            _loginPage.Login();
            string username = _homePage.GetElementText(LocatorType.Id, "nav-link-accountList-nav-line-1");
            Assert.That(nonLoggedInValue, Is.Not.EqualTo(username));
        }

        [Test, Description("Verify item price card matches with cart"), Order(4)]
        public void TestMenuValues()
        {
            _homePage.ChooseMainMenuCategory();
            _homePage.RetrieveMenuItem();
            bool arePricesMatched = _homePage.CreditCardFlowCheck();
            Assert.That(arePricesMatched);
            //_homePage.GetMenuMapping();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
