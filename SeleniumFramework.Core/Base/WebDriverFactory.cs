using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace SeleniumFramework.Core.Base
{
    public static class WebDriverFactory
    {
        public static IWebDriver GetWebDriver()
        {
            ChromeOptions options = new ChromeOptions();

            options.AddArgument("--start-maximized");

            IWebDriver driver = new ChromeDriver(options);

            return driver;
        }
    }
}
