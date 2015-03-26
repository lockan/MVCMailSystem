using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;

namespace MVCMailSystem
{
    public class SeleniumDriver
    {
        const int iisPort = 47327;  //Run the app, check the port, change it here. 
        IWebDriver driver;
        string siteurl = "http://www.localhost:" + iisPort + "/";
        
        public SeleniumDriver()
        {
            this.driver = new InternetExplorerDriver();
        }

        public void LoginTest() {
            driver.Url = siteurl;
            driver.Navigate().GoToUrl(siteurl + "/Login/");
            driver.FindElement(By.Id("username")).SendKeys("SeleniumTest");
            
        }

    }
}