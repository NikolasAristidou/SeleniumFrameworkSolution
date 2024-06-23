using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumFramework.Core.Enums;
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

        public void ClickElement(LocatorType locatorType, string elementValue)
        {
            By locator = GetByLocator(locatorType, elementValue);
            Driver.FindElement(locator).Click();
        }

        public void EnterText(LocatorType locatorType, string elementValue , string text)
        {
            By locator = GetByLocator(locatorType, elementValue);
            Driver.FindElement(locator).SendKeys(text);
        }
        public void HoverToElement(LocatorType locatorType, string elementValue)
        {
            By locator = GetByLocator(locatorType, elementValue);
            IWebElement element = Driver.FindElement(locator);
            Actions action = new Actions(Driver);
            action.MoveToElement(element).Perform();
        }
        public string GetElementText(LocatorType locatorType, string elementName)
        {
            By locator = GetByLocator(locatorType, elementName);
            IWebElement element = Driver.FindElement(locator);
            return element.Text;
        }
        public abstract string Url { get; }

        public virtual void GoToUrl()
        {
            Driver.Navigate().GoToUrl(ConfigReader.GetAppSetting("BaseUrl") + Url);
        }
        public bool ValidateElementVisibility(LocatorType locatorType, string elementName)
        {
            IWebElement element;

            try
            {
                By locator = GetByLocator(locatorType, elementName);
                element = Driver.FindElement(locator);

                return element.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Unknown locator type: {locatorType}", ex);
            }
        }

        private By GetByLocator(LocatorType locatorType, string elementName)
        {
            return locatorType switch
            {
                LocatorType.Id => By.Id(elementName),
                LocatorType.Class => By.ClassName(elementName),
                LocatorType.Name => By.Name(elementName),
                LocatorType.Css => By.CssSelector(elementName),
                LocatorType.XPath => By.XPath(elementName),
                LocatorType.Tag => By.TagName(elementName),
                LocatorType.LinkText => By.LinkText(elementName),
                LocatorType.PartialLinkText => By.PartialLinkText(elementName),
                _ => throw new ArgumentException($"Unknown locator type: {locatorType}")
            };
        }
    }
}
