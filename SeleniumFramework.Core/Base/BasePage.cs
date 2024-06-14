using OpenQA.Selenium;
using SeleniumFramework.Core.Pages;
using SeleniumFramework.Core.Utilities;

namespace SeleniumFramework.Core.Base
{
    public abstract class BasePage : IPage
    {
        protected IWebDriver Driver;

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
        }

        public void ClickElement(By locator)
        {
            Driver.FindElement(locator).Click();
        }

        public void EnterText(By locator, string text)
        {
            Driver.FindElement(locator).SendKeys(text);
        }

        public void ClearText(By locator)
        {
            Driver.FindElement(locator).Clear();
        }

        public abstract string Url { get; }

        public virtual void Open()
        {
            Driver.Navigate().GoToUrl(ConfigReader.GetAppSetting("BaseUrl") + Url);
        }

        public abstract bool IsAt();

    }
}
