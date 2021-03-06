﻿// ***********************************************************************
// Assembly         : dotNetTips.CodePerf.Example.App
// Author           : David McCarter
// Created          : 02-14-2018
//
// Last Modified By : David McCarter
// Last Modified On : 07-18-2018
// ***********************************************************************
// <copyright file="PayRate.cs" company="dotNetTips.com - McCarter Consulting">
//     2018 David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace dotNetTips.CodePerf.Example
{
    /// <summary>
    /// Struct PayRate
    /// </summary>
    /// <seealso cref="System.IEquatable{dotNetTips.CodePerf.Example.PayRate}" />
    /// <seealso cref="System.IEquatable{dotNetTips.CodePerf.Example.App.PayRate}" />
    internal struct PayRate : IEquatable<PayRate>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PayRate" /> struct.
        /// </summary>
        /// <param name="awardedOn">The awarded on.</param>
        /// <param name="rate">The rate.</param>
        public PayRate(DateTime awardedOn, double rate) : this()
        {
            AwardedOn = awardedOn;
            Rate = rate;
        }

        /// <summary>
        /// Gets the awarded on.
        /// </summary>
        /// <value>The awarded on.</value>
        public DateTime AwardedOn { get; private set; }
        /// <summary>
        /// Gets the rate.
        /// </summary>
        /// <value>The rate.</value>
        public double Rate { get; private set; }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            return obj is PayRate && Equals((PayRate)obj);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.</returns>
        public bool Equals(PayRate other)
        {
            return AwardedOn == other.AwardedOn &&
                   Rate == other.Rate;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            var hashCode = -903919831;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + AwardedOn.GetHashCode();
            hashCode = hashCode * -1521134295 + Rate.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="rate1">The rate1.</param>
        /// <param name="rate2">The rate2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(PayRate rate1, PayRate rate2)
        {
            return rate1.Equals(rate2);
        }

        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="rate1">The rate1.</param>
        /// <param name="rate2">The rate2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(PayRate rate1, PayRate rate2)
        {
            return !(rate1 == rate2);
        }
    }
}
