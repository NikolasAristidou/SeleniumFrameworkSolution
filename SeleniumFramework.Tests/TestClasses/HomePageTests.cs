using OpenQA.Selenium;
using SeleniumFramework.Core.Pages;
using SeleniumFramework.Core.Base;

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
            _homePage.Open();
        }

        [Test]
        public void HomePageLoadTest()
        {
            // Assert that the home page is loaded successfullya
            Assert.That(_homePage.IsAt(), Is.True, "Amazon home page did not load successfully.");
        }


        [TearDown]
        public void Teardown()
        {
            _driver.Quit();
        }
    }
}
