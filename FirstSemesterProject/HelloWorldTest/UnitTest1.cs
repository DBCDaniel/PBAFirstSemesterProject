using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HelloWorldNameSpace;

namespace HelloWorldTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void TestSayIt()
        {
            HelloWorld hw = new HelloWorld();
            string result = hw.SayIt();
            Assert.AreEqual("Hello World", result);
        }
    }
}
