using System;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Text;

namespace UI_Test.UI_BaseClass
{
    
    class BaseClass
    {
        public const int TIMEOUT_MS = 300000;
        
        //Метод ждет окончание загрузки страницы
        public void WaitBusyEnd(int timeoutMs, IWebDriver driver)
        {
            DateTime end = DateTime.Now.AddMilliseconds(timeoutMs);
            while (DateTime.Now < end)
            {
                try
                {
                    var js = (IJavaScriptExecutor)driver;
                    object o = js.ExecuteScript("return document.readyState;");
                    string readyState = (string)o;
                    if (readyState == "complete" )
                        return;
                }
                catch
                {
                    return;
                }
            }
        }
    }
}
