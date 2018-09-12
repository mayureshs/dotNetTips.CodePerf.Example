// ***********************************************************************
// Assembly         : dotNetTips.CodePerf.Example.App
// Author           : David McCarter
// Created          : 02-14-2018
//
// Last Modified By : David McCarter
// Last Modified On : 09-09-2018
// ***********************************************************************
// <copyright file="Program.cs" company="dotNetTips.com - McCarter Consulting">
//     2018 David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************
using BenchmarkDotNet.Running;

namespace dotNetTips.CodePerf.Example.App
{
    /// <summary>
    /// Class Program.
    /// </summary>
    class Program
    {

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        static void Main()
        {
            var config = BenchmarkDotNet.Configs.DefaultConfig.Instance;

            //var summary1 = BenchmarkRunner.Run<PerfTestRunner>(config);
            var summary2 = BenchmarkRunner.Run<LongRunningPerfTestRunner>(config);

            //BenchmarkRunner.Run<PerfTestRunner>(ManualConfig
            //    .Create(DefaultConfig.Instance)
            //    .With(Job.Core.With(Platform.X64)));

            //BenchmarkRunner.Run<LongRunningPerfTestRunner>(ManualConfig
            //    .Create(DefaultConfig.Instance)
            //    .With(Job.Core.With(Platform.X64)));

            //BenchmarkRunner.Run<PerfTestRunner>(ManualConfig
            //    .Create(DefaultConfig.Instance)
            //    .With(Job.Clr.With(Platform.X64)));

            //BenchmarkRunner.Run<LongRunningPerfTestRunner>(ManualConfig
            //    .Create(DefaultConfig.Instance)
            //    .With(Job.Clr.With(Platform.X64)));

            // Console.ReadLine();

            // //warm up
            // //RunTests();
            //// RunLongRunningTests();

            // Console.Clear();
            // Console.WriteLine("Starting tests...");

            // RunTests();


            // RunLongRunningTests();

            //Console.WriteLine("Test Ended!");
            //Console.Beep();
            //Console.ReadLine();
        }

        /// <summary>
        /// Runs the long running tests.
        /// </summary>
        private static void RunLongRunningTests()
        {

        }

        /// <summary>
        /// Runs all tests.
        /// </summary>
        private static void RunTests()
        {

        }

    }
}
