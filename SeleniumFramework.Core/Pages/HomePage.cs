using OpenQA.Selenium;
using SeleniumFramework.Core.Base;

namespace SeleniumFramework.Core.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        public override string Url => "";

        public IWebElement UsernameInput => Driver.FindElement(By.Id("username"));
        public IWebElement PasswordInput => Driver.FindElement(By.Id("password"));
        public IWebElement LoginButton => Driver.FindElement(By.CssSelector("button[type='submit']"));

        public void Login(string username, string password)
        {
            UsernameInput.SendKeys(username);
            PasswordInput.SendKeys(password);
            LoginButton.Click();
        }

        public override bool IsAt()
        {
            return Driver.Title.Contains("Login Page");
        }
    }
}
