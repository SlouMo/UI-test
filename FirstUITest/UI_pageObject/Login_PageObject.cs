using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstUITest.PageObjects
{
    class Login_PageObject
    {
        private readonly IWebDriver _webDriver;
        private readonly WebDriverWait _wait;

        private readonly By _lodinField = By.XPath("//*[@id='login']");
        private readonly By _passwordField = By.XPath("//*[@id='password']");
        private readonly By _autorizationButton = By.XPath("//*[@type='submit']");
        private readonly By _errorAutorizationText = By.XPath("//div[@class='errorMessage']");

        public Login_PageObject(IWebDriver webDriver)
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

        public bool CheckErrorAutorization()
        {
            try
            {
                _webDriver.FindElement(_errorAutorizationText);
                return false;
            }
            catch (NoSuchElementException)
            {
                return true;
            }
        }
    }
}
