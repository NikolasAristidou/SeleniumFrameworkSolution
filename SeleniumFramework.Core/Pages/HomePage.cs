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

            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", electronicsLink);

            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", electronicsLink);

            IWebElement computersLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[contains(., 'Computers & Accessories')]")));

            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", computersLink);

            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", computersLink);
        }

        public void RetrieveMenuItem()
        {
            //ClickElement(LocatorType.XPath, "/html/body/div[1]/div[1]/div[1]/div[1]/div/span[1]/div[1]/div[2]/div/div/div/div/span/div/div/div[2]/div[1]/h2/a"); //First Item
            //ClickElement(LocatorType.XPath, "/html/body/div[1]/div[1]/div[1]/div[1]/div/span[1]/div[1]/div[3]/div/div/div/div/span/div/div/div[2]/div[1]/h2/a"); //Second Item
            ClickElement(LocatorType.XPath, "/html/body/div[1]/div[1]/div[1]/div[1]/div/span[1]/div[1]/div[4]/div/div/div/div/span/div/div/div[2]/div[1]/h2/a"); //Third Item
            //ClickElement(LocatorType.XPath, "/html/body/div[1]/div[1]/div[1]/div[1]/div/span[1]/div[1]/div[5]/div/div/span/div/div/div[2]/div[1]/h2/a"); //Forth Item
            //ClickElement(LocatorType.XPath, "/html/body/div[1]/div[1]/div[1]/div[1]/div/span[1]/div[1]/div[6]/div/div/div/div/span/div/div/div[2]/div[1]/h2/a"); //Fifth Item
            //ClickElement(LocatorType.XPath, "/html/body/div[1]/div[1]/div[1]/div[1]/div/span[1]/div[1]/div[7]/div/div/div/div/span/div/div/div[2]/div[1]/h2/a"); //Sixth Item
            //ClickElement(LocatorType.XPath, "/html/body/div[1]/div[1]/div[1]/div[1]/div/span[1]/div[1]/div[8]/div/div/div/div/span/div/div/div[2]/div[1]/h2/a"); //Seventh Item
            //ClickElement(LocatorType.XPath, "/html/body/div[1]/div[1]/div[1]/div[1]/div/span[1]/div[1]/div[9]/div/div/div/div/span/div/div/div[2]/div[1]/h2/a"); //8TH Item
            //ClickElement(LocatorType.XPath, "/html/body/div[1]/div[1]/div[1]/div[1]/div/span[1]/div[1]/div[10]/div/div/div/div/span/div/div/div[2]/div[1]/h2/a"); //9TH Item
            //ClickElement(LocatorType.XPath, "/html/body/div[1]/div[1]/div[1]/div[1]/div/span[1]/div[1]/div[11]/div/div/div/div/span/div/div/div[2]/div[1]/h2/a"); //10TH Item
        }

        public Dictionary<string, string> GetMenuMapping()
        {
            Dictionary<string, string> menuItems = new Dictionary<string, string>();

            ChooseMainMenuCategory();

            // Find all menu items
            IWebElement ulElement = Driver.FindElement(By.XPath("//*[@id=\"hmenu-content\"]/ul[1]"));

            IList<IWebElement> liElements = ulElement.FindElements(By.CssSelector("li > a.menu-item"));

            foreach (IWebElement li in liElements)
            {
                // Get data-menu-id attribute
                string menuId = li.GetAttribute("data-menu-id");

                // Get text from the div element inside the li
                string text = li.FindElement(By.CssSelector("div")).Text.Trim();

                // Add to dictionary
                menuItems.Add(menuId, text);
            }

            return menuItems;
        }
    }
}
