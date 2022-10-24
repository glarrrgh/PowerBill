using LibPBill;
using System.Globalization;

namespace PowerBillCMD
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            ParseArgs parsedArgs;
            try
            {
                parsedArgs = new ParseArgs(args);
            }
            catch (ParseArgException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            Console.WriteLine(PBill.CalculatePBill(
                parsedArgs.GetStartMeter(),
                parsedArgs.GetEndMeter(),
                parsedArgs.GetPowerPrice(),
                parsedArgs.GetStartMeter(),
                parsedArgs.GetEndMeter(),
                parsedArgs.GetNetPrice(),
                parsedArgs.GetNetFixedPrice()
            ));
        }
    }
}