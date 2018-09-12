// ***********************************************************************
// Assembly         : dotNetTips.CodePerf.Example.App
// Author           : David McCarter
// Created          : 07-04-2018
//
// Last Modified By : David McCarter
// Last Modified On : 08-13-2018
// ***********************************************************************
// <copyright file="Coordinates.cs" company="dotNetTips.com - McCarter Consulting">
//     2018 David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace dotNetTips.CodePerf.Example
{
    /// <summary>
    /// Struct CoordinatesFixed
    /// </summary>
    /// <seealso cref="System.IEquatable{dotNetTips.CodePerf.Example.CoordinatesFixed}" />
    /// <seealso cref="System.IEquatable{dotNetTips.CodePerf.Example.App.CoordinatesFixed}" />
    internal struct CoordinatesFixed : IEquatable<CoordinatesFixed>
    {
        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>The x.</value>
        public int X { get; set; }
        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>The y.</value>
        public int Y { get; set; }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            return obj is CoordinatesFixed && Equals((CoordinatesFixed)obj);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.</returns>
        public bool Equals(CoordinatesFixed other)
        {
            return X == other.X && Y == other.Y;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="fixed1">The fixed1.</param>
        /// <param name="fixed2">The fixed2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(CoordinatesFixed fixed1, CoordinatesFixed fixed2)
        {
            return fixed1.Equals(fixed2);
        }

        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="fixed1">The fixed1.</param>
        /// <param name="fixed2">The fixed2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(CoordinatesFixed fixed1, CoordinatesFixed fixed2)
        {
            return !(fixed1 == fixed2);
        }
    }
}
