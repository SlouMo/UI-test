using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstUITest.PageObjects
{
    class AutorizationPageObject
    {
        private IWebDriver _webDriver;
        private WebDriverWait _wait;

        private readonly By _lodinField = By.XPath("//*[@id='login']");
        private readonly By _passwordField = By.XPath("//*[@id='password']");
        private readonly By _autorizationButton = By.XPath("//*[@type='submit']");

        public AutorizationPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            _wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(600));
        }

        public void Autoruzation(string login, string password)
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(_autorizationButton));

            _webDriver.FindElement(_lodinField).SendKeys(login);
            _webDriver.FindElement(_passwordField).SendKeys(password);
            _webDriver.FindElement(_autorizationButton).Click();
        }
    }
}
