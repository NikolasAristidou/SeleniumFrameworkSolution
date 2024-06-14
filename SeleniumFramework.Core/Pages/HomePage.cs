using OpenQA.Selenium;
using SeleniumFramework.Core.Base;
using SeleniumFramework.Core.Enums;

namespace SeleniumFramework.Core.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        public override string Url => "";

        public void NavigateToLoginPage()
        {
            HoverToElement(LocatorType.Id, "nav-link-accountList");
            ClickElement(LocatorType.XPath, "//*[@id=\"nav-flyout-ya-signin\"]/a/span");
        }
    }
}
