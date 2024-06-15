using OpenQA.Selenium;
using SeleniumFramework.Core.Base;
using SeleniumFramework.Core.Enums;
using SeleniumFramework.Core.Utilities;

namespace SeleniumFramework.Core.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        public override string Url => "";

        public void Login()
        {
            ClickElement(LocatorType.Id, "ap_email");
            EnterUsername("Username");
            ClickElement(LocatorType.XPath, "//*[@id=\"continue\"]");
            ClickElement(LocatorType.Id, "ap_password");
            EnterUsername("Password");
            ClickElement(LocatorType.Id, "signInSubmit");
        }

        public void EnterUsername(string username)
        {
            EnterText(LocatorType.Id, "ap_email", ConfigReader.GetAppSetting(username));
        }

        public void EnterPassword(string password)
        {
            EnterText(LocatorType.Id, "ap_email", ConfigReader.GetAppSetting(password));
        }

        public void ClickSignIn()
        {
            ClickElement(LocatorType.Id, "dummy");
        }
    }
}
