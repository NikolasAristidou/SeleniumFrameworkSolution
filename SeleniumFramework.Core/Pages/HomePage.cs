using OpenQA.Selenium;
using SeleniumFramework.Core.Base;

namespace SeleniumFramework.Core.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        public override string Url => "";

    }
}
