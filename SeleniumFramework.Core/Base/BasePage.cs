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

        public virtual void GoToUrl()
        {
            Driver.Navigate().GoToUrl(ConfigReader.GetAppSetting("BaseUrl") + Url);
        }
        public bool ValidateLogo(string elementType, string elementName)
        {
            try
            {
                IWebElement element = null;

                switch (elementType.ToLower())
                {
                    case "id":
                        element = Driver.FindElement(By.Id(elementName));
                        break;
                    case "class":
                        element = Driver.FindElement(By.ClassName(elementName));
                        break;
                    case "name":
                        element = Driver.FindElement(By.Name(elementName));
                        break;
                    case "css":
                        element = Driver.FindElement(By.CssSelector(elementName));
                        break;
                    case "xpath":
                        element = Driver.FindElement(By.XPath(elementName));
                        break;
                    case "tag":
                        element = Driver.FindElement(By.TagName(elementName));
                        break;
                    case "link":
                        element = Driver.FindElement(By.LinkText(elementName));
                        break;
                    case "partiallink":
                        element = Driver.FindElement(By.PartialLinkText(elementName));
                        break;
                    default:
                        throw new ArgumentException($"Unknown element type: {elementType}");
                }

                return element.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
