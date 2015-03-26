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
        //const string _appPath =     @"E:\BCIT\Courses\Term4\COMP4941\Assignments\SeleniumDemo\MVCMailSystem\MVCMailSystem"; //desktop
        const string _appPath = @"D:\BCIT\Courses\Term4\COMP4941\Assignments\MVCMailSystem\MVCMailSystem"; //laptop
        const string _iisPath =     @"C:\Program Files (x86)\IIS Express\iisexpress.exe";
        const int iisPort = 4444;  //Run the app, check the port, change it here. 
        string siteurl = "http://localhost:" + iisPort + "/";

        private Process iisProc;
        IWebDriver driver = new InternetExplorerDriver();

        private void initIIS()
        {
            // Start IIS on our specified port. 
            iisProc = new Process();
            iisProc.StartInfo.FileName = _iisPath;
            iisProc.StartInfo.Arguments = string.Format(@"/path:{0} /port:{1}", _appPath, iisPort);
            iisProc.Start();
            System.Diagnostics.Debug.WriteLine("Running IIS: path={0} ; port={1}", _appPath, iisPort);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            initIIS();
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
        public void Login_Success()
        {
            string expected = "http://localhost:4444/?username=Manager1";

            if (!iisProc.HasExited)
            {
                //Test procedure
                driver.Url = siteurl;
                driver.Navigate().GoToUrl(siteurl + "Login/");
                driver.FindElement(By.Id("username")).SendKeys("Manager1");
                driver.FindElement(By.Id("submit")).Click();

                //Test condition: 
                Assert.AreEqual(expected, driver.Url);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("ERROR: Failed to launch IIS");
            }
        }
         
        [TestMethod]
        public void Login_Failure()
        {
            string expected = @"http://localhost:4444/Login";
            
            if (!iisProc.HasExited)
            {
                //Test procedure
                driver.Url = siteurl;
                driver.Navigate().GoToUrl(siteurl + "Login/");
                driver.FindElement(By.Id("username")).SendKeys("BadUserName");
                driver.FindElement(By.Id("submit")).Click();   //By.id seems to work best - guarantees we get the element we're looking for. 
                
                //Test condition: 
                Assert.AreEqual(expected, driver.Url);  //Asserts are a fairly standard way to check test success/failure in VS test project. 

            }
            else
            {
                System.Diagnostics.Debug.WriteLine("ERROR: Failed to launch IIS");
            }
        }

    }
}
