using System;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Text;

namespace FirstUITest.UI_pageObject
{
    class BaseClass
    {
        public void WaitBusyEnd(int timeout, IWebDriver driver)
        {
            DateTime end = DateTime.Now.AddMilliseconds(timeout);
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
