using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using LibPBill;

namespace PowerBillCMD
{
    public class ParseArgs
    {
        private static string helptext =
@"Usage:
PowerBillCMD <startmeter> <day.month.year> <stopmeter> <day.month.year> <powerprice> <netprice> <netfixedprice>
Use point (.) as decimal point, as that works best with command lines. 
The meter values need to be integers in the range 0 to 99999
Prices can "; 
        private Meter startMeter;
        private Meter endMeter;
        private decimal powerPrice;
        private decimal netPrice;
        private decimal netFixedPrice;
        public ParseArgs(string[] args)
        {
            if (args.Contains("-h"))
            {
                throw new ParseArgException(helptext);
            }
            try 
            {
                (
                    string startMeter,
                    string startDate,
                    string endMeter,
                    string endDate,
                    string powerPrice,
                    string netPrice,
                    string netFixedPrice
                ) = (args[0], args[1], args[2], args[3], args[4], args[5], args[6]);
                this.startMeter = new Meter(UInt32.Parse(startMeter), DateOnly.Parse(startDate));
                this.endMeter = new Meter(UInt32.Parse(endMeter), DateOnly.Parse(endDate));
                this.powerPrice = decimal.Parse(powerPrice, CultureInfo.InvariantCulture);
                this.netPrice = decimal.Parse(netPrice, CultureInfo.InvariantCulture);
                this.netFixedPrice = decimal.Parse(netFixedPrice, CultureInfo.InvariantCulture);
            } 
            catch (IndexOutOfRangeException) 
            {                
                throw new ParseArgException(
                    $"{helptext}\n\nError: Only {args.Length} arguments were entered. We need exactly 7."
                );
            }
            catch (FormatException e)
            {
                throw new ParseArgException(
                    $"{helptext}\n\nError: {e.StackTrace} is not on the proper format."
                );
            }
        }

        public Meter getStartMeter() => this.startMeter;
        public Meter getEndMeter() => this.endMeter;
        public decimal getPowerPrice() => this.powerPrice;
        public decimal getNetPrice() => this.netPrice;
        public decimal getNetFixedPrice() => this.netFixedPrice;
    }

    public class ParseArgException : Exception
    {
        public ParseArgException(string Message) : base(Message) { }
    }
}
