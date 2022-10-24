using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LibPBill;

namespace PowerBillCMD
{
    /// <summary>
    /// Class <c>ParseArgs</c> parse command line arguments and throws <exception cref="ParseArgException">ParseArgException</exception>
    /// if there is something in the arguments that prevent the program from providing an answer
    /// </summary>
    public class ParseArgs
    {
        private static string s_helpText =
@"Usage:
PowerBillCMD <startmeter> <startdate> <endmeter> <enddate> <powerprice> <netprice> <netfixedprice>
Use comma (,) as decimal point. 
The meter values need to be integers in the range 0 to 99999.
Dates need to be on regular norwegian format, like day.month.year."; 
        private Meter _startMeter;
        private Meter _endMeter;
        private decimal _powerPrice;
        private decimal _netPrice;
        private decimal _netFixedPrice;
        public ParseArgs(string[] args)
        {
            if (args.Contains("-h"))
            {
                throw new ParseArgException(s_helpText);
            }
            if (args.Length > 7)
            {
                throw new ParseArgException($"{s_helpText}\n\nToo many arguments. We need exactly 7 arguments.");
            }

            // Lots of extra handling to be able to give the user relevant 
            // feedback on what they will have to do to the commandline
            // arguments to get a value.
            try 
            {
                this._startMeter = new Meter(ParseMeterValue(args[0], "startmeter"), ParseDate(args[1], "startdate"));
                this._endMeter = new Meter(ParseMeterValue(args[2], "endmeter"), ParseDate(args[3], "enddate"));
                this._powerPrice = ParsePrice(args[4], "powerprice");
                this._netPrice = ParsePrice(args[5], "netprice");
                this._netFixedPrice = ParsePrice(args[6], "netprice");
            } 
            catch (IndexOutOfRangeException) 
            {                
                throw new ParseArgException(
                    $"{s_helpText}\n\nError: Only {args.Length} arguments were entered. We need exactly 7."
                );
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ParseArgException($"{s_helpText}\n\nError: The meeter values must be between 0 and 99999 (inclusive).");
            }
        }

        /// <summary>
        /// Parses a string into a meter value as an UInt32.
        /// </summary>
        /// <param name="arg">The string to be parsed.</param>
        /// <param name="name">The name of the argument, for error handling.</param>
        /// <returns>A UInt32 that represents a reading on a meter.</returns>
        /// <exception cref="ParseArgException"></exception>
        private UInt32 ParseMeterValue(string arg, string name)
        {
            try
            {
                return UInt32.Parse(arg);
            }
            catch (FormatException)
            {
                throw new ParseArgException($"{s_helpText}\n\nError: {arg} is not a valid number for the {name} argument. Please use whole numbers.");
            }
            catch (OverflowException)
            {
                throw new ParseArgException($"{s_helpText}\n\nError: {name} must be between 0 and 99999 (inclusive).");
            }
        }

        /// <summary>
        /// Parse a date.
        /// </summary>
        /// <param name="arg">The string to be parsed. Must be on form day.month.year.</param>
        /// <param name="name">The name of the argument, for error handling.</param>
        /// <returns>A DateOnly object.</returns>
        /// <exception cref="ParseArgException"></exception>
        private DateOnly ParseDate(string arg, string name)
        {
            try
            {
                return DateOnly.Parse(arg, CultureInfo.GetCultureInfo("nb-NO"));
            }
            catch (FormatException)
            {
                throw new ParseArgException($"{s_helpText}\n\nError: {arg} is not a valid date for the {name} argument. Please use the format day.month.year");
            }
        }

        /// <summary>
        /// Parse a price.
        /// </summary>
        /// <param name="arg">The string to be parsed. Must use , as decimal point</param>
        /// <param name="name">The name of the argument, for error handling.</param>
        /// <returns>A decimal object.</returns>
        /// <exception cref="ParseArgException"></exception>
        private decimal ParsePrice(string arg, string name)
        {
            try
            {
                return decimal.Parse(arg, CultureInfo.GetCultureInfo("nb-NO"));
            }
            catch (FormatException)
            {
                throw new ParseArgException($"{s_helpText}\n\nError: {arg} is not a valid decimal value for the {name} argument. Please use numbers with a comma (,) as a decimal point.");
            }
            catch (OverflowException)
            {
                throw new ParseArgException($"{s_helpText}\n\nError: {name} is overflowing. Its probably to big or small. Please use sensible numbers, and not {arg}");
            }
        }

        public Meter GetStartMeter() => this._startMeter;
        public Meter GetEndMeter() => this._endMeter;
        public decimal GetPowerPrice() => this._powerPrice;
        public decimal GetNetPrice() => this._netPrice;
        public decimal GetNetFixedPrice() => this._netFixedPrice;
    }

    /// <summary>
    /// An exception thrown when ParseArgs objects finds something wrong in the arguments.
    /// The message includes information about the first error that was found, in a way
    /// that can be presented directly to the user.
    /// </summary>
    public class ParseArgException : Exception
    {
        public ParseArgException(string Message) : base(Message) { }
    }
}
