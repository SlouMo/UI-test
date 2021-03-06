using UI_Test.PageObjects;
using UI_Test.UI_BaseClass;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace FirstUITest
{
    public class Tests
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        private const string _URL = "https://obp.bars.group/bp_RC/login";
        private const string _expectedFIO = "???????? ???????? ??????????";
        private const string _password = "Qwerty1!";
        private const string _login = "bars_vl.kuznetsov";

        private readonly By _FIOview = By.XPath("//*[@class='profile']/child::p");




        [SetUp]
        public void Setup()
        {
            _driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(_URL);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));


        }


        [Test]
        public void Test()
        {
            //????? ?????? ??? ????? ?????? ? ??????
            Login_PageObject autorization = new Login_PageObject(_driver);
            autorization.Autoruzation(_login, _password);
            
            //???????? ???????? ????????
            BaseClass baseClass = new BaseClass();
            baseClass.WaitBusyEnd(BaseClass.TIMEOUT_MS, _driver);

            //????????, ??? ????? ? ?????? ???????
            if (autorization.CheckErrorAutorization())
            {
                //???????? ????????? ???????? ? ??? ?? ??? ?????????
                _wait.Until(ExpectedConditions.ElementIsVisible(_FIOview));

                var actualFIO = _driver.FindElement(_FIOview).Text;
                while (actualFIO == "????????...")
                {
                    actualFIO = _driver.FindElement(_FIOview).Text;
                }
                Assert.AreEqual(_expectedFIO, actualFIO, "??? ?? ?????????");
            }
            else
            {
                Assert.Fail("???????? ????? ??? ??????.");
            }


        }

        [TearDown]
        public void TearDown()
        {
            _driver.Close();
        }
    }
}