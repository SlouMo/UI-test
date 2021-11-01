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
        private const string _expectedFIO = "Кузнецов Владимир Алексеевич";
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
            //Вызов метода для ввода логина и пароля
            Login_PageObject autorization = new Login_PageObject(_driver);
            autorization.Autoruzation(_login, _password);
            
            //Ожидание загрузки страницы
            BaseClass baseClass = new BaseClass();
            baseClass.WaitBusyEnd(BaseClass.TIMEOUT_MS, _driver);

            //Проверка, что логин и пароль вверный
            if (autorization.CheckErrorAutorization())
            {
                //Ожидание появление элемента с ФИО ии его сравнение
                _wait.Until(ExpectedConditions.ElementIsVisible(_FIOview));

                var actualFIO = _driver.FindElement(_FIOview).Text;
                while (actualFIO == "Загрузка...")
                {
                    actualFIO = _driver.FindElement(_FIOview).Text;
                }
                Assert.AreEqual(_expectedFIO, actualFIO, "ФИО не корректно");
            }
            else
            {
                Assert.Fail("Неверный логин или пароль.");
            }


        }

        [TearDown]
        public void TearDown()
        {
            _driver.Close();
        }
    }
}