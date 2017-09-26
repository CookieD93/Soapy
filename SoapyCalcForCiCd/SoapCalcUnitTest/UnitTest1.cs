using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoapyCalcForCiCd;

namespace SoapCalcUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private Service1 service;
        [TestInitialize]
        public void TestInitializer()
        {
            service = new Service1();
        }
        [TestMethod]
        public void AddTest()
        {
            Assert.AreEqual(7, service.Add(2, 5));
        }

        [TestMethod]
        public void SubtractTest()
        {
            Assert.AreEqual(10, service.Subtract(15, 5));
        }
        [TestMethod]
        public void DivideTest()
        {
            Assert.AreEqual(2, service.Divide(10, 5));
        }
        [TestMethod]
        //[ExpectedException(typeof(DivideByZeroException))]
        public void DivideZeroTest()
        {
            service.Divide(5, 0);
        }
    }
}
