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
            /*catch (FormatException) { Console.WriteLine(helptext); return; }
            catch (OverflowException) { Console.WriteLine(helptext); return; }
            catch (IndexOutOfRangeException) { Console.WriteLine(helptext); return; }
            catch (ArgumentOutOfRangeException) { Console.WriteLine(helptext); return; }*/
            Console.WriteLine(PBill.CalculatePBill(
                parsedArgs.getStartMeter(),
                parsedArgs.getEndMeter(),
                parsedArgs.getPowerPrice(),
                parsedArgs.getStartMeter(),
                parsedArgs.getEndMeter(),
                parsedArgs.getNetPrice(),
                parsedArgs.getNetFixedPrice()
            ));
        }
    }
}