using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPBill
{
    public class Meter
    {
        private UInt32 value;
        private DateOnly startDate;
        public Meter(UInt32 value, DateOnly date)
        {
            this.value = value;
            this.startDate = date;
        }

        public UInt32 GetValue() => this.value;
        public DateOnly GetStartDate() => this.startDate;
        public DateOnly GetEndDate() => this.startDate;

        public static Meter operator -(Meter first, Meter second)
        {
            if (first.GetStartDate() > second.GetStartDate())
            {
                Meter midl = second;
                second = first;
                first = midl;
            }
            return new MeterDelta(GetDeltaValue(first.GetValue(), second.GetValue()), first.GetStartDate(), second.GetEndDate());
        }

        private static UInt32 GetDeltaValue(UInt32 first, UInt32 second)
        {
            if (first > second)
            {
                return 100000 + second - first;
            } else
            {
                return second - first;
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || this.GetType() != obj.GetType())
            {
                return false;
            }
            Meter someobj = (Meter)obj;
            return  this.GetValue() == someobj.GetValue() && 
                    this.GetStartDate() == someobj.GetStartDate() && 
                    this.GetEndDate() == someobj.GetEndDate();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.GetValue(), this.GetStartDate(), this.GetEndDate());
        }

        public override string ToString()
        {
            return $"{this.GetType()}: {this.GetValue()}, {this.GetStartDate()}, {this.GetEndDate()}";
        }
    }

    public class MeterDelta : Meter
    {
        private DateOnly endDate;

        public MeterDelta(UInt32 value, DateOnly startDate, DateOnly endDate) : base(value, startDate)
        {
            this.endDate = endDate;
        }

        public new DateOnly GetEndDate() => endDate;
    }
}
