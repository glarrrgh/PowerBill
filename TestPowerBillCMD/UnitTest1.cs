

namespace LibPBill.Tests
{
    [TestClass]
    public class PowerBillCMDTests
    {
        [TestMethod]
        public void TestZeroValues()
        {
            Assert.AreEqual(0M, PBill.CalculatePBill());            
        }

        [TestMethod]
        public void TestSomeValues()
        {
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
}