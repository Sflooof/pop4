using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ClassLibrary.Tests
{
    [TestClass()]
    public class TestingTests
    {
        [TestMethod()]
        public void ValidateBackTest()
        {
            bool click_on = true;
            bool actual = Testing.ValidateBack(click_on);
            Assert.IsTrue(click_on);
        }

        [TestMethod()]
        public void ValidatePriceTest()
        {
            string coust = "3000";

            bool actual = Testing.ValidatePrice(coust);
            Assert.IsTrue(actual);
        }

        [TestMethod()]
        public void ValidateUserTest()
        {

            string login = "higge4-6gyY";
            string password = "6566565";

            bool actual = Testing.ValidateUser(password, login);
            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public void ValidateUserTest1()
        {

            string login = "loginDErvb2018";
            string password = "ceAf&R";

            bool actual = Testing.ValidateUser(password, login);
            Assert.IsTrue(actual);
        }

        [TestMethod()]
        public void ValidateUserTest2()
        {

            string login = "loginDErvb2018";
            string password = " ";

            bool actual = Testing.ValidateUser(password, login);
            Assert.IsFalse(actual);
        }
    }
}