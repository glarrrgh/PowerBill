

using System.Runtime.ExceptionServices;

namespace LibPBill.Tests
{
    [TestClass]
    public class LibPBillTests
    {
        [TestMethod]
        public void TestZeroValues()
        {
            Assert.AreEqual(0M, PBill.CalculatePBill());            
        }

        [TestMethod]
        public void TestSomeValues()
        {
            Meter first = new Meter(18537, new DateOnly(2022, 8, 1));
            Meter second = new Meter(18975, new DateOnly(2022, 9, 1));

            int startPowerMeter = 18537;
            DateOnly startPowerDate = new DateOnly(2022, 8, 1);
            int stopPowerMeter = 18975;
            DateOnly endPowerDate = new DateOnly(2022, 9, 1);
            decimal powerPrice = 4.391M - 2.7429M;
            int startNetMeter = 18537;
            DateOnly startNetDate = new DateOnly(2022, 8, 1);
            int stopNetMeter = 18975;
            DateOnly endNetDate = new DateOnly(2022, 9, 1);
            decimal netPrice = 0.5676M;
            decimal netFixedPrice = 281.25M;
            Assert.AreEqual(1111.1016M, PBill.CalculatePBill(
                startPowerMeter,
                startPowerDate,
                stopPowerMeter,
                endPowerDate,
                powerPrice,
                startNetMeter,
                startNetDate,
                stopNetMeter,
                endNetDate,
                netPrice,
                netFixedPrice
            ));
        }
    }

    [TestClass]
    public class MeterTests
    {
        [TestMethod]
        public void TestSomeMeter()
        {
            DateOnly date = new DateOnly(2001, 2, 3);
            Meter meter = new Meter(2, date);
            Assert.AreEqual((UInt32)2, meter.GetValue());
            Assert.AreEqual(date, meter.GetStartDate());
            Assert.AreEqual(date, meter.GetEndDate());
        }

        [TestMethod]
        public void TestSubstract()
        {
            Meter first = new Meter(1, new DateOnly(2001, 2, 3));
            Meter second = new Meter(3, new DateOnly(2001, 3, 1));
            MeterDelta difference = new MeterDelta(
                2,
                new DateOnly(2001, 2, 3),
                new DateOnly(2001, 3, 1)
            );
            Assert.AreEqual(difference, first - second);
            Assert.AreEqual(difference, second - first);
        }
    }
}