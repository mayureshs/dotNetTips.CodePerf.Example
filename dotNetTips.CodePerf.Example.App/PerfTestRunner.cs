// ***********************************************************************
// Assembly         : dotNetTips.CodePerf.Example.App
// Author           : David McCarter
// Created          : 08-23-2018
//
// Last Modified By : David McCarter
// Last Modified On : 09-08-2018
// ***********************************************************************
// <copyright file="PerfTestRunner.cs" company="dotNetTips.com - McCarter Consulting">
//     2018 David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************
using BenchmarkDotNet.Attributes;
using System;

namespace dotNetTips.CodePerf.Example.App
{
    /// <summary>
    /// Class PerfTestRunner.
    /// </summary>
    /// <seealso cref="dotNetTips.CodePerf.Example.App.PerfTest" />
    public class PerfTestRunner : PerfTest
    {
        /// <summary>
        /// The coordinates1
        /// </summary>
        private Coordinates _coordinates1;
        /// <summary>
        /// The coordinates1 fixed
        /// </summary>
        private CoordinatesFixed _coordinates1Fixed;
        /// <summary>
        /// The coordinates2
        /// </summary>
        private Coordinates _coordinates2;
        /// <summary>
        /// The coordinates2 fixed
        /// </summary>
        private CoordinatesFixed _coordinates2Fixed;
        /// <summary>
        /// The person1
        /// </summary>
        private Person _person1;
        /// <summary>
        /// The person1 fixed
        /// </summary>
        private PersonFixed _person1Fixed;
        /// <summary>
        /// The person2
        /// </summary>
        private Person _person2;
        /// <summary>
        /// The person2 fixed
        /// </summary>
        private PersonFixed _person2Fixed;

        /// <summary>
        /// Initializes a new instance of the <see cref="PerfTestRunner"/> class.
        /// </summary>
        public PerfTestRunner()
        {
            _person1 = CreateFakePerson();
            _person1Fixed = CreateFakePersonFixed();
            _person2 = CreateFakePerson();
            _person2Fixed = CreateFakePersonFixed();

            _coordinates1 = new Coordinates { X = GetRandomNumber(), Y = GetRandomNumber() };
            _coordinates2 = new Coordinates { X = GetRandomNumber(), Y = GetRandomNumber() };

            _coordinates1Fixed = new CoordinatesFixed { X = GetRandomNumber(), Y = GetRandomNumber() };
            _coordinates2Fixed = new CoordinatesFixed { X = GetRandomNumber(), Y = GetRandomNumber() };
        }

        /// <summary>
        /// Tests the checking string with is null or empty.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [Benchmark(OperationsPerInvoke = 500, Description = "Validating String with IsNullOrEmpty()")]
        public bool TestCheckingStringWithIsNullOrEmpty()
        {
            return string.IsNullOrEmpty(Email1);
        }

        /// <summary>
        /// Tests the checking string with is null or white space.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [Benchmark(OperationsPerInvoke = 500, Description = "Validating String with IsNullOrWhitespace()")]
        public bool TestCheckingStringWithIsNullOrWhiteSpace()
        {
            return string.IsNullOrWhiteSpace(Email1);
        }

        /// <summary>
        /// Tests the checking string with null.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [Benchmark(OperationsPerInvoke = 500, Description = "Validating String with null and quotes.")]
        public bool TestCheckingStringWithNull()
        {
            if (Email1 == null || Email1.Trim() == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Tests the creating person.
        /// </summary>
        /// <returns>Person.</returns>
        [Benchmark(OperationsPerInvoke = 100, Description = "Created reference type using default state.")]
        public Person TestCreatingPerson()
        {
            return CreateFakePerson();
        }

        /// <summary>
        /// Tests the creating person properly.
        /// </summary>
        /// <returns>PersonFixed.</returns>
        [Benchmark(OperationsPerInvoke = 100, Description = "Created reference type properly.")]
        public PersonFixed TestCreatingPersonProperly()
        {
            return CreateFakePersonFixed();
        }

        /// <summary>
        /// Tests the person get hash code.
        /// </summary>
        /// <returns>System.Int32.</returns>
        [Benchmark(OperationsPerInvoke = 100, Description = "Person GetHashCode()")]
        public int TestPersonGetHashCode()
        {
            return _person1.GetHashCode();
        }

        /// <summary>
        /// Tests the person get hash code override.
        /// </summary>
        /// <returns>System.Int32.</returns>
        [Benchmark(OperationsPerInvoke = 100, Description = "Person GetHashCode() with override")]
        public int TestPersonGetHashCodeOverride()
        {
            return _person1Fixed.GetHashCode();
        }

        /// <summary>
        /// Tests the person to string.
        /// </summary>
        /// <returns>System.String.</returns>
        [Benchmark(OperationsPerInvoke = 100, Description = "Person ToString()")]
        public string TestPersonToString()
        {
            return _person1.ToString();
        }

        /// <summary>
        /// Tests the person to string override.
        /// </summary>
        /// <returns>System.String.</returns>
        [Benchmark(OperationsPerInvoke = 100, Description = "Person ToString() with override")]
        public string TestPersonToStringOverride()
        {
            return _person1Fixed.ToString();
        }

        /// <summary>
        /// Tests the person with override.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [Benchmark(OperationsPerInvoke = 100, Description = "Validating Person with Override")]
        public bool TestPersonWithOverride()
        {
            return _person1Fixed.Equals(_person2Fixed);
        }

        /// <summary>
        /// Tests the person with reflection.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [Benchmark(OperationsPerInvoke = 100, Description = "Validating Person with Reflection")]
        public bool TestPersonWithReflection()
        {
            return _person1.Equals(_person2);
        }

        /// <summary>
        /// Tests the string format.
        /// </summary>
        /// <returns>System.String.</returns>
        [Benchmark(OperationsPerInvoke = 100, Description = "Formatting String with string.Format().")]
        public string TestStringFormat()
        {
            return string.Format("The time is now: {0} count: {1}.", DateTime.Now, GetRandomNumber());
        }

        /// <summary>
        /// Tests the string interpolation.
        /// </summary>
        /// <returns>System.String.</returns>
        [Benchmark(OperationsPerInvoke = 100, Description = "Formatting String with  interpolation.")]
        public string TestStringInterpolation()
        {
            return $"The time is now: {DateTime.Now} count: {GetRandomNumber()}.";
        }


        /// <summary>
        /// Tests the string to lower.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [Benchmark(OperationsPerInvoke = 100, Description = "Validating String using ToLower()")]
        public bool TestStringToLower()
        {
            return lowerCaseString == properCaseString.ToLower();
        }

        /// <summary>
        /// Tests the string to upper.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [Benchmark(OperationsPerInvoke = 100, Description = "Validating String using ToUpper()")]
        public bool TestStringToUpper()
        {
            return upperCaseString == properCaseString.ToUpper();
        }

        /// <summary>
        /// Tests the string with equals.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [Benchmark(OperationsPerInvoke = 100, Description = "Validating String with Equals().")]
        public bool TestStringWithEquals()
        {
            return Email1.Equals(Email2);
        }

        /// <summary>
        /// Tests the string with equals operator.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [Benchmark(OperationsPerInvoke = 100, Description = "Validating String with '=='.")]
        public bool TestStringWithEqualsOperator()
        {
            if (Email1 == Email2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Tests the structure equals.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [Benchmark(OperationsPerInvoke = 100, Description = "Structure Equals().")]
        public bool TestStructureEquals()
        {
            return _coordinates1.Equals(_coordinates2);
        }

        /// <summary>
        /// Tests the structure equals override.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [Benchmark(OperationsPerInvoke = 100, Description = "Structure Equals() with override.")]
        public bool TestStructureEqualsOverride()
        {
            return _coordinates1Fixed.Equals(_coordinates2Fixed);
        }

        /// <summary>
        /// Tests the structure get hash code.
        /// </summary>
        /// <returns>System.Int32.</returns>
        [Benchmark(OperationsPerInvoke = 100, Description = "Structure GetHashCode().")]
        public int TestStructureGetHashCode()
        {
            return _coordinates1.GetHashCode();
        }

        /// <summary>
        /// Tests the structure get hash code override.
        /// </summary>
        /// <returns>System.Int32.</returns>
        [Benchmark(OperationsPerInvoke = 100, Description = "Structure GetHashCode() override.")]
        public int TestStructureGetHashCodeOverride()
        {
            return _coordinates1.GetHashCode();
        }

        /// <summary>
        /// Tests the value comparison.
        /// </summary>
        /// <returns>System.String.</returns>
        [Benchmark(OperationsPerInvoke = 500, Description = "String comparison with '=='.")]
        public string TestValueComparison()
        {
            return CompareValues("davidmccarter@live.com", Email1);
        }

        /// <summary>
        /// Tests the value comparison with equals.
        /// </summary>
        /// <returns>System.String.</returns>
        [Benchmark(OperationsPerInvoke = 500, Description = "String comparison with Equals().")]
        public string TestValueComparisonWithEquals()
        {
            return CompareValuesFixed("davidmccarter@live.com", Email1);
        }

        /// <summary>
        /// Compares the values.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <returns>System.String.</returns>
        private string CompareValues(string param1, string param2)
        {
            return (param1.ToLower() == "DavidMcCarter@live.com") || (param2.ToLower() == "dotnetdave@Live.com") ? "success" : "failed";
        }

        /// <summary>
        /// Compares the values fixed.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <returns>System.String.</returns>
        private string CompareValuesFixed(string param1, string param2)
        {
            const string testValueP = "DavidMcCarter@live.com";
            const string testValuePg = "dotnetdave@Live.com";

            return (param1.Equals(testValueP, StringComparison.CurrentCultureIgnoreCase)) || (param2.Equals(testValuePg, StringComparison.CurrentCultureIgnoreCase)) ? "success" : "failed";

        }
    }
}