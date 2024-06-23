using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumFramework.Core.Base;
using SeleniumFramework.Core.Enums;

namespace SeleniumFramework.Core.Pages
{
    public partial class HomePage : BasePage
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

        public void ChooseMainMenuCategory()
        {
            ClickElement(LocatorType.Id, "nav-hamburger-menu");

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            IWebElement electronicsLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[contains(., 'Electronics')]")));

            Thread.Sleep(TimeSpan.FromSeconds(1));

            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", electronicsLink);

            Thread.Sleep(TimeSpan.FromSeconds(1));

            By computersAccessoriesLinkLocator = By.XPath("//a[contains(@class, 'hmenu-item') and contains(text(), 'Computers & Accessories')]");
            IWebElement computersAccessoriesLink = wait.Until(ExpectedConditions.ElementToBeClickable(computersAccessoriesLinkLocator));

            // Scroll the element into view if needed
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", computersAccessoriesLink);

            // Click on the Computers & Accessories link using JavaScript
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", computersAccessoriesLink);
        }

        public void RetrieveMenuItem(int scenario)
        {
            switch (scenario)
            {
                case 1:
                    ClickElement(LocatorType.XPath, "/html/body/div[1]/div[1]/div[1]/div[1]/div/span[1]/div[1]/div[4]/div/div/div/div/span/div/div/div[2]/div[1]/h2/a"); // Third Item
                    break;

                case 2:
                    ClickElement(LocatorType.XPath, "/html/body/div[1]/div[1]/div[1]/div[1]/div/span[1]/div[1]/div[5]/div/div/span/div/div/div[2]/div[1]/h2/a"); // Fourth Item
                    break;

                default:
                    throw new ArgumentException("Invalid scenario value");
            }
        }
    }
}
