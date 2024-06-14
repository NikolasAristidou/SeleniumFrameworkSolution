using OpenQA.Selenium;
using SeleniumFramework.Core.Pages;
using SeleniumFramework.Core.Base;
using NUnit.Framework;


namespace SeleniumFramework.Tests.TestClasses
{
    [TestFixture]
    public class HomePageTests
    {
        private IWebDriver _driver;
        private HomePage _homePage;

        [SetUp]
        public void Setup()
        {
            _driver = WebDriverFactory.GetWebDriver();
            _homePage = new HomePage(_driver);
            _homePage.GoToUrl();
        }

        [Test]
        public void HomePageLoadTest()
        {
            bool isLogoDisplayed = _homePage.ValidateLogo("id", "nav-logo-sprites");
            Assert.That(isLogoDisplayed, "The Amazon logo is not displayed on the home page.");
        }


        [TearDown]
        public void Teardown()
        {
            _driver.Quit();
        }
    }
}
