namespace LibPBill
{
    public class PBill
    {
        public static Decimal CalculatePBill()
        {
            return 0.0M;
        }

        public static decimal CalculatePBill(
            int startPowerMeter,
            DateOnly startPowerDate,
            int stopPowerMeter,
            DateOnly endPowerDate,
            decimal powerPrice,
            int startNetMeter,
            DateOnly startNetDate,
            int stopNetMeter,
            DateOnly endNetDate,
            decimal netPrice,
            decimal netFixedPrice
        )
        {
            int powerConsumption;
            if (startPowerMeter > stopPowerMeter)
            {
                powerConsumption = 100000 + stopPowerMeter - startPowerMeter;
            }
            else
            {
                powerConsumption = stopPowerMeter - startPowerMeter;
            }
            int netConsumption;
            if (startNetMeter > stopNetMeter)
            {
                netConsumption = 100000 + stopNetMeter - startNetMeter;
            }
            else
            {
                netConsumption = stopNetMeter - startNetMeter;
            }
            decimal result = (powerConsumption * powerPrice) + (netConsumption * netPrice) + (netFixedPrice / 2);
            return result;
        }
    }
}