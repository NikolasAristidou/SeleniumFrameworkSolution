using OpenQA.Selenium;
using SeleniumFramework.Core.Pages;
using SeleniumFramework.Core.Base;
using SeleniumFramework.Core.Utilities;
using SeleniumFramework.Core.Enums;


namespace SeleniumFramework.Tests.TestClasses
{
    [TestFixture, Order(1)]
    public class Tests
    {
        private IWebDriver _driver;
        private HomePage _homePage;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _driver = WebDriverFactory.GetWebDriver();
            _homePage = new HomePage(_driver);
            _homePage.GoToUrl();
        }

        [Test, Description("Verifies amazon home page loaded by validating persisting logo on the left side")]
        public void HomePageLoadTest()
        {
            bool isLogoDisplayed = _homePage.ValidateElementVisibility(LocatorType.Id, "nav-logo-sprites");
            Assert.That(isLogoDisplayed, "The Amazon logo is displayed on the home page - page loaded");
            Logger.Log("Home Page loaded");
        }

        [Test, Description("Verifies Navigation has loaded sucessfully")]
        public void VerifyNavigationLinkIsDisplayed()
        {
            Assert.That(_homePage.ValidateElementVisibility(LocatorType.Id, "nav-xshop"));
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
