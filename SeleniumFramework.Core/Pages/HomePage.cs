using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumFramework.Core.Base;
using SeleniumFramework.Core.Enums;
using System;

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

        public void ChooseMainMenuCategory()
        {
            ClickElement(LocatorType.Id, "nav-hamburger-menu");
            ClickElement(LocatorType.XPath, "//*[@id=\"hmenu-content\"]/ul[1]/li[7]/a");
            ClickElement(LocatorType.XPath, "//*[@id=\"hmenu-content\"]/ul[33]/li[7]/a");
        }

        public void RetrieveMenuItem()
        {
            // HEADERS
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

        public bool CreditCardFlowCheck()
        {
            var addToCartButtonExist = ValidateElementVisibility(LocatorType.Id, "add-to-cart-button");

            var arePricesMatched = false;

            if (addToCartButtonExist)
            {
                arePricesMatched = AddToCartFlowTestCase();
            }
            else
            {
                /*arePricesMatched = */AddToCartFlowTestCase();
            }

            return arePricesMatched;
        }

        private bool AddToCartFlowTestCase()
        {
            IWebElement priceElement = Driver.FindElement(By.XPath("//span[@class='a-price-whole']/parent::span"));

            string wholePart = priceElement.FindElement(By.ClassName("a-price-whole")).Text;

            IWebElement? decimalElement = null;
            try
            {
                decimalElement = priceElement.FindElement(By.ClassName("a-price-decimal"));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Decimal part element not found.");
            }
            string fractionPart = priceElement.FindElement(By.ClassName("a-price-fraction")).Text;

            // Combine the parts to form the complete price string
            string beforeCartPriceChildrenValue = wholePart + (decimalElement != null ? "," : "") + fractionPart;

            ClickElement(LocatorType.Id, "add-to-cart-button");

            //Go to cart button
            ClickElement(LocatorType.XPath, "//*[@id=\"sw-gtc\"]/span/a");

            IWebElement afterCardPriceParent = Driver.FindElement(By.Id("sc-subtotal-amount-buybox"));

            IWebElement afterCardPriceChildren = afterCardPriceParent.FindElement(By.XPath(".//span[@class='a-size-medium a-color-base sc-price sc-white-space-nowrap']"));

            string afterCartPriceChildrenValue = afterCardPriceChildren.Text.Trim().Replace("$", "");

            return beforeCartPriceChildrenValue.Equals(afterCartPriceChildrenValue);

        }

        private bool SeeSimilarItemsFlowTestCase()
        {
            return true;
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
