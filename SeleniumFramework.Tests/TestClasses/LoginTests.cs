using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumFramework.Core.Enums;
using SeleniumFramework.Core.Pages;

namespace SeleniumFramework.Tests.TestClasses
{
    [TestFixture, Order(2)]
    public class LoginTests
    {
        private LoginPage _loginPage;
        private HomePage _homePage;
        private IWebDriver Driver;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _homePage = new HomePage(Driver);
            _homePage.NavigateToLoginPage(); // Navigate to login page from home page
            _loginPage = new LoginPage(Driver);
        }

        [Test, Description("Verifies the login page is displayed correctly")]
        public void VerifyLoginPageIsDisplayed()
        {
            bool isLoginPageDisplayed = _loginPage.ValidateElementVisibility(LocatorType.XPath, "//*[@id=\"authportal-main-section\"]/div[2]/div[2]/div[1]/form/div/div/div/h1");
            Assert.That(isLoginPageDisplayed, "Login Page displayed successfully");
        }

        [Test, Description("Verifies the user can login with valid credentials")]
        public void VerifyUserCanLogin()
        {
            _loginPage.Login();
        }
    }
}
