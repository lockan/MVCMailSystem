using System;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        const string _appName =     @"MVCMailSystem";
        const string _appPath =     @"E:\BCIT\Courses\Term4\COMP4941\Assignments\SeleniumDemo\MVCMailSystem\MVCMailSystem";
        const string _iisPath =     @"C:\Program Files (x86)\IIS Express\iisexpress.exe";
        const int iisPort = 4444;  //Run the app, check the port, change it here. 
        string siteurl = "http://localhost:" + iisPort + "/";

        private Process iisProc;
        IWebDriver driver = new InternetExplorerDriver();

        [TestInitialize]
        public void TestInitialize()
        {
            // Start IIS on our specified port. 
            iisProc = new Process();
            iisProc.StartInfo.FileName = _iisPath;
            iisProc.StartInfo.Arguments = string.Format(@"/path:{0} /port:{1}", _appPath, iisPort);
            iisProc.Start();
            System.Diagnostics.Debug.WriteLine("Running IIS: path={0} ; port={1}", _appPath, iisPort);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Ensure IISExpress is stopped
            if (iisProc.HasExited == false)
            {
                iisProc.Kill();
            }
        }
         
        [TestMethod]
        public void LoginTest()
        {
            if (!iisProc.HasExited)
            {
                driver.Url = siteurl;
                driver.Navigate().GoToUrl(siteurl + "Login/");
                driver.FindElement(By.Id("username")).SendKeys("SeleniumTest");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("ERROR: Failed to launch IIS");
            }
        }
    }
}
