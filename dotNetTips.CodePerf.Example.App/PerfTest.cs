// ***********************************************************************
// Assembly         : dotNetTips.CodePerf.Example.App
// Author           : David McCarter
// Created          : 08-23-2018
//
// Last Modified By : David McCarter
// Last Modified On : 09-09-2018
// ***********************************************************************
// <copyright file="PerfTest.cs" company="dotNetTips.com - McCarter Consulting">
//     2018 David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************
using BenchmarkDotNet.Attributes;
using System;

namespace dotNetTips.CodePerf.Example.App
{
    /// <summary>
    /// Class PerfTest.
    /// </summary>
    [ClrJob, CoreJob]
    [HtmlExporter]
    public abstract class PerfTest
    {
        /// <summary>
        /// The email1
        /// </summary>
        protected string Email1 = "davidmccarter@live.com";

        /// <summary>
        /// The email2
        /// </summary>
        protected string Email2 = "dotnetdave@live.com";

        /// <summary>
        /// The lower case string
        /// </summary>
        protected string lowerCaseString = "david mccarter";
        /// <summary>
        /// The proper case string
        /// </summary>
        protected string properCaseString = "David McCarter";

        /// <summary>
        /// The upper case string
        /// </summary>
        protected string upperCaseString = "DAVID MCCARTER";

        /// <summary>
        /// The random
        /// </summary>
        private Random _random = new Random(999);

        /// <summary>
        /// Gets the iteration count.
        /// </summary>
        /// <value>The iteration count.</value>
        protected int IterationCount { get; private set; } = 10000;

        /// <summary>
        /// Creates the fake person.
        /// </summary>
        /// <returns>Person.</returns>
        protected Person CreateFakePerson()
        {
            var number = GetRandomNumber();

            Person person = null;

            person = new Person(Guid.NewGuid(), $"FakeEmail@{number}.com")
            {
                FirstName = $"FakeFirstName{number}",
                LastName = $"FakeLastName{number}",
                BornOn = DateTime.Now.Subtract(new TimeSpan(10000, 0, 0, 0))
            };

            return person;
        }

        /// <summary>
        /// Creates the fake person fixed.
        /// </summary>
        /// <returns>PersonFixed.</returns>
        protected PersonFixed CreateFakePersonFixed()
        {
            var number = GetRandomNumber();

            return new PersonFixed(Guid.NewGuid(), $"FakeEmail@{number}.com")
            {
                FirstName = $"FakeFirstName{number}",
                LastName = $"FakeLastName{number}",
                BornOn = DateTime.Now.Subtract(new TimeSpan(10000, 0, 0, 0))
            };
        }

        /// <summary>
        /// Gets the random number.
        /// </summary>
        /// <returns>System.Int32.</returns>
        protected int GetRandomNumber()
        {
            return _random.Next(1000, 10000000);
        }
    }
}
