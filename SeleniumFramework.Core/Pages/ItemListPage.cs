using OpenQA.Selenium;
using SeleniumFramework.Core.Base;
using SeleniumFramework.Core.Enums;
using System.Text.RegularExpressions;

namespace SeleniumFramework.Core.Pages
{
    public partial class ItemListPage : BasePage
    {
        public ItemListPage(IWebDriver driver) : base(driver)
        {
        }

        public override string Url => "";

        public bool CreditCardFlowCheck()
        {
            bool addToCartButtonExist = ValidateElementVisibility(LocatorType.Id, "add-to-cart-button");

            return addToCartButtonExist ? AddToCartFlowTestCase() : SeeSimilarItemsTestCase();
        }

        private bool SeeSimilarItemsTestCase()
        {
            // Click on See Similar items
            ClickElement(LocatorType.XPath, "//*[@id=\"exportAlternativeTriggerButton\"]/span/input");

            // List<Dictionary<string, string>> elementsPricesList = [];
            List<IWebElement> SimilarItemsButtonsElements = [];
            List<ElementPricePair> elementsPricesList = [];

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

            var retrieveSimilarItemsPrices = ExtractPrices();

            int count = Math.Min(retrieveSimilarItemsButtonElement.Count, retrieveSimilarItemsPrices.Count);

            for (int i = 0; i < count; i++)
            {
                ElementPricePair pair = new(SimilarItemsButtonsElements[i], retrieveSimilarItemsPrices[i]);
                elementsPricesList.Add(pair);
            }

            // Select a random elementsPriceList to click and validate later for the cart

            ElementPricePair randomPair = GetRandomElementPricePair(elementsPricesList);

            string randomPrice = randomPair.Price;

            randomPair.Element.Click();

            Thread.Sleep(TimeSpan.FromSeconds(1));

            ClickElement(LocatorType.Id, "nav-cart");

            IWebElement afterCardPriceChildren = Driver.FindElement(By.XPath("//*[@id='sc-subtotal-amount-buybox']//span[@class='a-size-medium a-color-base sc-price sc-white-space-nowrap']"));

            // Extract the text and clean it to get the numeric value
            string afterCartPriceChildrenFullValue = afterCardPriceChildren.Text.Trim().Replace("$", "");
            string afterCartPriceChildrenTrimmedValue = MyRegex().Replace(afterCartPriceChildrenFullValue, "");

            return randomPrice.Equals(afterCartPriceChildrenTrimmedValue);

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
        private ElementPricePair GetRandomElementPricePair(List<ElementPricePair> elementsPricesList)
        {
            Random random = new Random();
            int randomIndex = random.Next(elementsPricesList.Count);
            return elementsPricesList[randomIndex];
        }
    }
}
