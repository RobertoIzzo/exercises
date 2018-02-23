using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication1.Common;
using WebApplicationMVC.Controllers;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Demo demo = new Demo();
            HomeController homeC = new HomeController();
        }
    }
}
