using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace FirstUITest
{
    public class Tests
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        private const string _URL = "https://obp.bars.group/bp_RC/login";
        private const string _password = "123";
        private const string _login = "bars_vl.kuznetsov";
        private const string _expectedFIO = "Кузнецов Владимир Алексеевич";

        private readonly By _lodinField = By.XPath("//*[@id='login']");
        private readonly By _passwordField = By.XPath("//*[@id='password']");
        private readonly By _autorizationButton = By.XPath("//*[@type='submit']");
        private readonly By _FIOview = By.XPath("//*[@class='profile']/child::p");




        [SetUp]
        public void Setup()
        {
            _driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(_URL);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(600));


        }


        [Test]
        public void Test()
        {
            //Thread.Sleep(1000);
            _wait.Until(ExpectedConditions.ElementIsVisible(_autorizationButton));

            _driver.FindElement(_lodinField).SendKeys(_login);
            _driver.FindElement(_passwordField).SendKeys(_password);
            _driver.FindElement(_autorizationButton).Click();
            _wait.Until(ExpectedConditions.ElementIsVisible(_FIOview));

            var actualFIO = _driver.FindElement(_FIOview).Text;
            while (actualFIO == "Загрузка...")
            {
                actualFIO = _driver.FindElement(_FIOview).Text;
            } 
            Assert.AreEqual(_expectedFIO, actualFIO, "ФИО не корректно");
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Close();
        }
    }
}