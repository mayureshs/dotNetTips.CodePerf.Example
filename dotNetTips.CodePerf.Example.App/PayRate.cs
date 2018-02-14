using System;
using System.Collections.Generic;
using System.Text;

namespace dotNetTips.CodePerf.Example.App
{
    struct PayRate : IEquatable<PayRate>
    {
        public PayRate(DateTime awardedOn, double rate) : this()
        {
            AwardedOn = awardedOn;
            Rate = rate;
        }

        public DateTime AwardedOn { get; private set; }
        public double Rate { get; private set; }

        public override bool Equals(object obj)
        {
            return obj is PayRate && Equals((PayRate)obj);
        }

        public bool Equals(PayRate other)
        {
            return AwardedOn == other.AwardedOn &&
                   Rate == other.Rate;
        }

        public override int GetHashCode()
        {
            var hashCode = -903919831;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + AwardedOn.GetHashCode();
            hashCode = hashCode * -1521134295 + Rate.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(PayRate rate1, PayRate rate2)
        {
            return rate1.Equals(rate2);
        }

        public static bool operator !=(PayRate rate1, PayRate rate2)
        {
            return !(rate1 == rate2);
        }
    }
}
