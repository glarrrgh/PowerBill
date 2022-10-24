using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerBillCMD.Tests
{
    [TestClass]
    public class PowerBillCMDTests
    {
        [TestMethod]
        public void TestParseArgsWithNothing()
        {
            Assert.ThrowsException<ParseArgException>(() => { new ParseArgs(new string[] { }); });
        }

        [TestMethod]
        public void TestMissingArgument()
        {
            Assert.ThrowsException<ParseArgException>(() =>
            {
                new ParseArgs(new string[] { "1", "1.2.3", "2", "1.3.3", "3,2", "2,3" });
            });
        }

        [TestMethod]
        public void TestMeterArgumentTooLarge()
        {
            Assert.ThrowsException<ParseArgException>(() =>
            {
                new ParseArgs(new string[] { "100000", "1.2.3", "2", "1.3.3", "3,2", "2,3" });
            });
        }
    }

    [TestClass]
    public class ParseArgumentTests
    {
        [TestMethod]
        public void TestStartMeterValue()
        {
            Assert.ThrowsException<ParseArgException>(() =>
            {
                new ParseArgs(new string[] { "a", "1.2.3", "2", "1.3.3", "3,2", "2,3", "4,5" });
            });
            Assert.ThrowsException<ParseArgException>(() =>
            {
                new ParseArgs(new string[] { "-1", "1.2.3", "2", "1.3.3", "3,2", "2,3", "4,5" });
            });
            Assert.ThrowsException<ParseArgException>(() =>
            {
                new ParseArgs(new string[] { "4294967296", "1.2.3", "2", "1.3.3", "3,2", "2,3", "4,5" });
            });
        }

        [TestMethod]
        public void TestEndMeterValue()
        {
            Assert.ThrowsException<ParseArgException>(() =>
            {
                new ParseArgs(new string[] { "1", "1.2.3", "blargh", "1.3.3", "3,2", "2,3", "4,5" });
            });
            Assert.ThrowsException<ParseArgException>(() =>
            {
                new ParseArgs(new string[] { "1", "1.2.3", "-2", "1.3.3", "3,2", "2,3", "4,5" });
            });
            Assert.ThrowsException<ParseArgException>(() =>
            {
                new ParseArgs(new string[] { "1", "1.2.3", "4294967296", "1.3.3", "3,2", "2,3", "4,5" });
            });
        }

        [TestMethod]
        public void TestStartDate()
        {
            Assert.ThrowsException<ParseArgException>(() =>
            {
                new ParseArgs(new string[] { "1", "2022/24/5", "2", "1.3.3", "3,2", "2,3", "4,5" });
            });
            Assert.ThrowsException<ParseArgException>(() =>
            {
                new ParseArgs(new string[] { "1", "29.2.2021", "2", "1.3.3", "3,2", "2,3", "4,5" });
            });
            Assert.ThrowsException<ParseArgException>(() =>
            {
                new ParseArgs(new string[] { "1", "smurf", "2", "1.3.3", "3,2", "2,3", "4,5" });
            });
        }

        [TestMethod]
        public void TestEndDate()
        {
            Assert.ThrowsException<ParseArgException>(() =>
            {
                new ParseArgs(new string[] { "1", "1.2.3", "2", "2022/24/5", "3,2", "2,3", "4,5" });
            });
            Assert.ThrowsException<ParseArgException>(() =>
            {
                new ParseArgs(new string[] { "1", "1.2.3", "2", "29.2.2021", "3,2", "2,3", "4,5" });
            });
            Assert.ThrowsException<ParseArgException>(() =>
            {
                new ParseArgs(new string[] { "1", "1.2.3", "2", "smurf", "3,2", "2,3", "4,5" });
            });
        }

        [TestMethod]
        public void TestPowerPrice()
        {
            Assert.ThrowsException<ParseArgException>(() =>
            {
                new ParseArgs(new string[] { "1", "1.2.3", "2", "1.3.3", "f", "2,3", "4,5" });
            });
            Assert.ThrowsException<ParseArgException>(() =>
            {
                new ParseArgs(new string[] { "1", "1.2.3", "2", "1.3.3", "3.2", "2,3", "4,5" });
            });
            Assert.ThrowsException<ParseArgException>(() =>
            {
                new ParseArgs(new string[] { "1", "1.2.3", "2", "1.3.3", "3,2,4", "2,3", "4,5" });
            });
            Assert.ThrowsException<ParseArgException>(() =>
            {
                new ParseArgs(new string[] { "1", "1.2.3", "2", "1.3.3", "79228162514264337593543950336", "2,3", "4,5" });
            });
        }

        [TestMethod]
        public void TestNetPrice()
        {
            Assert.ThrowsException<ParseArgException>(() =>
            {
                new ParseArgs(new string[] { "1", "1.2.3", "2", "1.3.3", "2,3", "f", "4,5" });
            });
            Assert.ThrowsException<ParseArgException>(() =>
            {
                new ParseArgs(new string[] { "1", "1.2.3", "2", "1.3.3", "2,3", "3.2", "4,5" });
            });
            Assert.ThrowsException<ParseArgException>(() =>
            {
                new ParseArgs(new string[] { "1", "1.2.3", "2", "1.3.3", "2,3", "3,2,4", "4,5" });
            });
            Assert.ThrowsException<ParseArgException>(() =>
            {
                new ParseArgs(new string[] { "1", "1.2.3", "2", "1.3.3", "2,3", "79228162514264337593543950336", "4,5" });
            });
        }

        [TestMethod]
        public void TestNetFixedPrice()
        {
            Assert.ThrowsException<ParseArgException>(() =>
            {
                new ParseArgs(new string[] { "1", "1.2.3", "2", "1.3.3", "2,3", "4,5", "f" });
            });
            Assert.ThrowsException<ParseArgException>(() =>
            {
                new ParseArgs(new string[] { "1", "1.2.3", "2", "1.3.3", "2,3", "4,5", "3.2" });
            });
            Assert.ThrowsException<ParseArgException>(() =>
            {
                new ParseArgs(new string[] { "1", "1.2.3", "2", "1.3.3", "2,3", "4,5", "3,2,4" });
            });
            Assert.ThrowsException<ParseArgException>(() =>
            {
                new ParseArgs(new string[] { "1", "1.2.3", "2", "1.3.3", "2,3", "4,5", "79228162514264337593543950336" });
            });
        }
    }
}
