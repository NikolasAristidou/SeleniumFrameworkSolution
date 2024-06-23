using OpenQA.Selenium;

public class ElementPricePair
{
    public IWebElement Element { get; set; }
    public string Price { get; set; }

    public ElementPricePair(IWebElement element, string price)
    {
        Element = element;
        Price = price;
    }
}
