using OpenQA.Selenium;
using SeleniumFramework.Core.Base;
using SeleniumFramework.Core.Enums;
using System.Text.RegularExpressions;
using System.Xml.Linq;

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
            ClickElement(LocatorType.XPath, "//*[@id=\"hmenu-content\"]/ul[1]/li[7]/a");
            ClickElement(LocatorType.XPath, "//*[@id=\"hmenu-content\"]/ul[33]/li[7]/a");
        }

        public void RetrieveMenuItem()
        {
            //ClickElement(LocatorType.XPath, "/html/body/div[1]/div[1]/div[1]/div[1]/div/span[1]/div[1]/div[2]/div/div/div/div/span/div/div/div[2]/div[1]/h2/a"); //First Item
            //ClickElement(LocatorType.XPath, "/html/body/div[1]/div[1]/div[1]/div[1]/div/span[1]/div[1]/div[3]/div/div/div/div/span/div/div/div[2]/div[1]/h2/a"); //Second Item
            //ClickElement(LocatorType.XPath, "/html/body/div[1]/div[1]/div[1]/div[1]/div/span[1]/div[1]/div[4]/div/div/div/div/span/div/div/div[2]/div[1]/h2/a"); //Third Item
            ClickElement(LocatorType.XPath, "/html/body/div[1]/div[1]/div[1]/div[1]/div/span[1]/div[1]/div[5]/div/div/span/div/div/div[2]/div[1]/h2/a"); //Forth Item
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
                arePricesMatched = SeeSimilarItemsTestCase();
            }

            return arePricesMatched;
        }

        private bool SeeSimilarItemsTestCase()
        {
            // Click on See Similar items
            ClickElement(LocatorType.XPath, "//*[@id=\"exportAlternativeTriggerButton\"]/span/input");

            List<Dictionary<string, string>> elementsPricesList = [];
            List<IWebElement> SimilarItemsButtonsElements = [];

            IReadOnlyCollection<IWebElement> retrieveSimilarItemsButtonElement = Driver.FindElements(By.CssSelector("td._export-alternative-card-desktop_style_export_alternative_table_cta_cell__1c3f_"));

            foreach (var td in retrieveSimilarItemsButtonElement)
            {
                // Find the input element within the current td
                IWebElement inputElement = td.FindElement(By.CssSelector("input[type='submit'].a-button-input"));

                // Add to list if found
                if (inputElement != null)
                {
                    SimilarItemsButtonsElements.Add(inputElement);
                }
            }

            var retrieveSimilarItemsPrices  = ExtractPrices();

            int count = Math.Min(retrieveSimilarItemsButtonElement.Count, retrieveSimilarItemsPrices.Count);

            for (int i = 0; i < count; i++)
            {
                Dictionary<string, string> pair = new Dictionary<string, string>();

                pair.Add(retrieveSimilarItemsButtonElement.ElementAt(i)., retrieveSimilarItemsPrices.ElementAt(i));

                elementsPricesList.Add(pair);
            }



            return true;
        }

        private bool AddToCartFlowTestCase()
        {
            // Find the price element
            IWebElement priceElement = Driver.FindElement(By.XPath("//span[@class='a-price-whole']/parent::span"));

            // Extract whole, decimal, and fraction parts of the price
            string wholePart = priceElement.FindElement(By.ClassName("a-price-whole")).Text;
            string fractionPart = priceElement.FindElement(By.ClassName("a-price-fraction")).Text;

            // Check if decimal part exists
            bool hasDecimal = false;
            IWebElement decimalElement = null;
            try
            {
                decimalElement = priceElement.FindElement(By.ClassName("a-price-decimal"));
                hasDecimal = true;
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Decimal part element not found.");
            }

            // Construct the complete price string
            string beforeCartPriceChildrenValue = wholePart + (hasDecimal ? "." + fractionPart : "");

            // Click on the Add to Cart button
            ClickElement(LocatorType.Id, "add-to-cart-button");

            // Click on the Go to Cart button
            ClickElement(LocatorType.XPath, "//*[@id='sw-gtc']/span/a");

            // Find the element containing the cart subtotal price
            IWebElement afterCardPriceChildren = Driver.FindElement(By.XPath("//*[@id='sc-subtotal-amount-buybox']//span[@class='a-size-medium a-color-base sc-price sc-white-space-nowrap']"));

            // Extract the text and clean it to get the numeric value
            string afterCartPriceChildrenFullValue = afterCardPriceChildren.Text.Trim().Replace("$", "");
            string afterCartPriceChildrenTrimmedValue = MyRegex().Replace(afterCartPriceChildrenFullValue, "");

            // Compare the prices before and after adding to cart
            return beforeCartPriceChildrenValue.Equals(afterCartPriceChildrenTrimmedValue);
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

        [GeneratedRegex("[^0-9.]")]
        private static partial Regex MyRegex();

        private List<string> ExtractPrices()
        {

            List<string> pricesList = new List<string>();

            // Find all <span> elements containing prices
            IReadOnlyCollection<IWebElement> priceElements = Driver.FindElements(By.XPath("//span[@class='a-color-price a-text-bold']"));

            foreach (IWebElement element in priceElements)
            {
                string priceText = element.Text.Trim();

                string numericPrice = Regex.Replace(priceText, @"[^\d\.]", "");

                pricesList.Add(numericPrice);
            }

            return pricesList;
        }
    }
}
