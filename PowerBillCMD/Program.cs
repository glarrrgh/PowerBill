using LibPBill;

namespace PowerBillCMD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Contains("-h") || args.Length == 0){
                Console.WriteLine(
"PowerBillCMD <startmeter> <day.month.year> <stopmeter> <day.month.year> <powerprice> <netprice> <netfixedprice>"
                );
                return;
            }
            int startMeter = Int32.Parse(args[0]);
            int stopMeter = Int32.Parse(args[2]);
            DateOnly startDate = DateOnly.Parse(args[1]);
            DateOnly endDate = DateOnly.Parse(args[3]);
            decimal powerPrice = decimal.Parse(args[4]);
            decimal netprice = decimal.Parse(args[5]);
            decimal netfixedprice = decimal.Parse(args[6]);
            Console.WriteLine(
                PBill.CalculatePBill(
                    startMeter,
                    startDate,
                    stopMeter,
                    endDate,
                    powerPrice,
                    startMeter,
                    startDate,
                    stopMeter,
                    endDate,
                    netprice,
                    netfixedprice
                )
            );
        }
    }
}