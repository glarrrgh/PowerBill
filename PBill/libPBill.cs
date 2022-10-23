namespace LibPBill
{
    public class PBill
    {
        public static Decimal CalculatePBill()
        {
            return 0.0M;
        }

        public static decimal CalculatePBill(
            Meter startPowerMeter,
            Meter endPowerMeter,
            decimal powerPrice,
            Meter startNetMeter,
            Meter endNetMeter,
            decimal netPrice,
            decimal netFixedPrice
        )
        {
            MeterDelta powerDifference = startPowerMeter - endPowerMeter;
            MeterDelta netDifference = startNetMeter - endNetMeter;

            return  (powerDifference.GetValue() * powerPrice)
                    + (netDifference.GetValue() * netPrice)
                    + (netFixedPrice / 2);
        }            
    }
}