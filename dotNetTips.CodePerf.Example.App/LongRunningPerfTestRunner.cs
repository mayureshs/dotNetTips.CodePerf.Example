// ***********************************************************************
// Assembly         : dotNetTips.CodePerf.Example.App
// Author           : David McCarter
// Created          : 08-23-2018
//
// Last Modified By : David McCarter
// Last Modified On : 09-09-2018
// ***********************************************************************
// <copyright file="LongRunningPerfTestRunner.cs" company="dotNetTips.com - McCarter Consulting">
//     2018 David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************
using BenchmarkDotNet.Attributes;
using dotNetTips.CodePerf.Example.Collections;
using dotNetTips.CodePerf.Example.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace dotNetTips.CodePerf.Example.App
{
    /// <summary>
    /// Class LongRunningPerfTestRunner.
    /// </summary>
    /// <seealso cref="dotNetTips.CodePerf.Example.App.PerfTest" />
    public class LongRunningPerfTestRunner : PerfTest
    {
        /// <summary>
        /// The test email
        /// </summary>
        private string _testEmail;
        /// <summary>
        /// Initializes a new instance of the <see cref="LongRunningPerfTestRunner"/> class.
        /// </summary>
        public LongRunningPerfTestRunner()
        {
            PersonCollection = FillPersonCollection();
            _testEmail = PersonCollection[PersonCollection.Count / 2].Email;
        }

        /// <summary>
        /// Gets or sets the person collection.
        /// </summary>
        /// <value>The person collection.</value>
        private List<Person> PersonCollection { get; set; }

        /// <summary>
        /// Tests any with predicate.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [Benchmark(OperationsPerInvoke = 100, Description = "Checking Collection with Any().")]
        public bool TestAnyWithPredicate()
        {
            return PersonCollection.Any(p => p.Email.Equals(_testEmail));
        }

        /// <summary>
        /// Tests the collection with fast any.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [Benchmark(OperationsPerInvoke = 100, Description = "Checking Collection with FastAny().")]
        public bool TestCollectionWithFastAny()
        {
            return PersonCollection.FastAny(p => p.Email.Equals(_testEmail));
        }

        /// <summary>
        /// Tests the create concurrent dictionary.
        /// </summary>
        [Benchmark(OperationsPerInvoke = 5, Description = "Creating Collection with  ConcurrentDictionary.")]
        public void TestCreateConcurrentDictionary()
        {
            var people = new ConcurrentDictionary<Guid, Person>();

            for (int personCount = 0; personCount < IterationCount; personCount++)
            {
                people.GetOrAdd(Guid.NewGuid(), CreateFakePerson());
            }
        }

        /// <summary>
        /// Tests the creating concurrent hash set.
        /// </summary>
        [Benchmark(OperationsPerInvoke = 5, Description = "Creating Collection with  ConcurrentHashSet.")]
        public void TestCreatingConcurrentHashSet()
        {
            var people = new ConcurrentHashSet<Person>();

            for (int personCount = 0; personCount < IterationCount; personCount++)
            {
                people.Add(CreateFakePerson());
            }
        }

        /// <summary>
        /// Tests the creating fast sorted list.
        /// </summary>
        [Benchmark(OperationsPerInvoke = 5, Description = "Creating FastSortedList.")]
        public void TestCreatingFastSortedList()
        {
            var notSorted = new FastSortedList<string>();

            for (int notSortedCount = 0; notSortedCount < IterationCount; notSortedCount++)
            {
                notSorted.Add(notSortedCount.ToString());
            }

            var notSortedResult = notSorted.ToList();
        }

        /// <summary>
        /// Tests the creating sorted set.
        /// </summary>
        [Benchmark(OperationsPerInvoke = 5, Description = "Creating SortedSet.")]
        public void TestCreatingSortedSet()
        {
            var sorted = new SortedSet<string>();

            for (int sortedCount = 0; sortedCount < IterationCount; sortedCount++)
            {
                sorted.Add(sortedCount.ToString());
            }

            var sortedResult = sorted.ToList();
        }

        /// <summary>
        /// Tests the exception.
        /// </summary>
        /// <returns>System.String.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        [Benchmark(OperationsPerInvoke = 10, Description = "Checking Exception Catch.")]
        public string TestException()
        {
            try
            {
                throw new ArgumentOutOfRangeException($"Test #1 for trapping exceptions.");
            }
            catch (ArgumentNullException anex)
            {
                return anex.Message;
            }
            catch (ArgumentOutOfRangeException aoorex)
            {
                return aoorex.Message;
            }
        }

        /// <summary>
        /// Tests the exception when clause.
        /// </summary>
        /// <returns>System.String.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        [Benchmark(OperationsPerInvoke = 10, Description = "Checking Exception Catch with When() clause.")]
        public string TestExceptionWhenClause()
        {
            try
            {
                throw new ArgumentOutOfRangeException($"Test #2 for trapping exceptions.");
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is ArgumentOutOfRangeException)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Tests the fast any with predicate.
        /// </summary>
        [Benchmark(OperationsPerInvoke = 100, Description = "Checking Collection with FastAny() and predicate.")]
        public void TestFastAnyWithPredicate()
        {
            var result = PersonCollection.FastAny(p => p.Email.Equals(_testEmail));
        }

        /// <summary>
        /// Tests the fast any with validation.
        /// </summary>
        [Benchmark(OperationsPerInvoke = 100, Description = "Checking Collection with FastAny() and Validation.")]
        public void TestFastAnyWithValidation()
        {
            var result = PersonCollection.FastAnyWithValidation(p => p.Email.Equals(_testEmail));
        }

        /// <summary>
        /// Tests the validating with any.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [Benchmark(OperationsPerInvoke = 100, Description = "Validating Collection with Any().")]
        public bool TestValidatingWithAny()
        {
            return PersonCollection.Any();
        }

        /// <summary>
        /// Tests the validating with count.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [Benchmark(OperationsPerInvoke = 100, Description = "Validating Collection with Count().")]
        public bool TestValidatingWithCount()
        {
            return PersonCollection.Count() > 0;
        }


        /// <summary>
        /// Fills the person collection.
        /// </summary>
        /// <returns>List&lt;Person&gt;.</returns>
        private List<Person> FillPersonCollection()
        {
            var people = new List<Person>();

            for (int personCount = 0; personCount < IterationCount; personCount++)
            {
                var person = new Person(Guid.NewGuid(), $"FakeEmail@{personCount * 100}.com")
                {
                    FirstName = "FakePerson" + personCount.ToString(),
                    LastName = "FakeName" + personCount.ToString(),
                    BornOn = DateTime.Now
                };

                people.Add(person);
            }

            return people;
        }
    }
}
