using dotNetTips.Utility.Standard.Collections.Generic.Concurrent;
using dotNetTips.Utility.Standard.Diagnostics;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace dotNetTips.CodePerf.Example.App
{
    class Program
    {
        static List<Person> _people = new List<Person>();
        const int InterationCount = 100000;

        static void Main()
        {
            // TestCountVsAny();

            // TestValueComparison();

            //  TestSortedList();

            // TestStringInterpolation();

            //TestConcurrentDictionaryVsConcurrentHashSet();

            TestLowerVsUpperStringCheck();

            Console.ReadLine();
        }

        static void TestLowerVsUpperStringCheck()
        {
            Console.WriteLine(InterationCount);

            Console.WriteLine("Testing Upper vs Lower string check...");

            var lowerStopWatch = PerformanceStopwatch.StartNew();

            const string lowerCaseString = "david mccarter";
            const string properCaseString = "David McCarter";
            const string upperCaseString = "DAVID MCCARTER";

            for (int countCounter = 0; countCounter < InterationCount; countCounter++)
            {
                var result = lowerCaseString == properCaseString.ToLower();
            }

            var lowerTime = lowerStopWatch.StopReset();

            var upperStopwatch = PerformanceStopwatch.StartNew();

            for (int anyCounter = 0; anyCounter < InterationCount; anyCounter++)
            {
                var result = upperCaseString == properCaseString.ToUpper();
            }

            var upperTime = upperStopwatch.StopReset();

            var message = $"Lower time={lowerTime.TotalMilliseconds} - Upper time={upperTime.TotalMilliseconds}. Percent of Change={CalculatePercentage(lowerTime.Milliseconds, upperTime.Milliseconds)}";
         
            Console.WriteLine(message);

            Console.ReadLine();

        }

        private static string CalculatePercentage(int first, int second)
        {
            // Increase ÷ Original Number × 100.
            double percent = (Math.Max(first, second)/Math.Min(first,second)) * 100;
            return $"{percent}%";
        }

        static void TestCountVsAny()
        {
            Console.WriteLine(InterationCount);

            FillPersonCollection();

            Console.WriteLine("Testing Count vs Any...");

            var countStopwatch = PerformanceStopwatch.StartNew();

            for (int countCounter = 0; countCounter < InterationCount; countCounter++)
            {
                var count = _people.Count;
            }

            var countTime = countStopwatch.StopReset();

            var anyStopwatch = PerformanceStopwatch.StartNew();

            for (int anyCounter = 0; anyCounter < InterationCount; anyCounter++)
            {
                var result = _people.Any();
            }


            var anyTime = anyStopwatch.StopReset();

            var message = $"Count [time={countTime.TotalMilliseconds} avg={InterationCount / countTime.TotalMilliseconds}] - Any [time={anyTime.TotalMilliseconds} avg={InterationCount / anyTime.TotalMilliseconds}]";

            Console.WriteLine(message);

        }

        static void TestConcurrentDictionaryVsConcurrentHashSet()
        {
            Console.WriteLine(InterationCount);

            Console.WriteLine("Testing ConcurrentDictionary...");

            var dictionaryStopwatch = PerformanceStopwatch.StartNew();

            var dictionary = TestConcurrentDictionary();

            var dictionaryTime = dictionaryStopwatch.StopReset();

            var hashSetStopwatch = PerformanceStopwatch.StartNew();

            var hashSet = TestConcurrentHashSet();


            var hashSetTime = hashSetStopwatch.StopReset();

            Console.WriteLine("Dictionary memory=" + SizeOf(dictionary));
            Console.WriteLine("HashSet memory=" + SizeOf(hashSet));


            var message = $"ConcurrentDictionary [time={dictionaryTime.TotalMilliseconds} - HashSet [time={hashSetTime.TotalMilliseconds} %{(dictionaryTime.TotalMilliseconds / hashSetTime.TotalMilliseconds) * 100}]";

            Console.WriteLine(message);

        }

        static private int SizeOfObj(Type T, object thevalue)
        {
            var type = T;
            int returnval = 0;
            if (type.IsValueType)
            {
                var nulltype = Nullable.GetUnderlyingType(type);
                returnval = System.Runtime.InteropServices.Marshal.SizeOf(nulltype ?? type);
            }
            else if (thevalue == null)
                return 0;
            else if (thevalue is string)
                returnval = Encoding.Default.GetByteCount(thevalue as string);
            else if (type.IsArray && type.GetElementType().IsValueType)
            {
                returnval = ((Array)thevalue).GetLength(0) * System.Runtime.InteropServices.Marshal.SizeOf(type.GetElementType());
            }
            else if (thevalue is Stream)
            {
                var thestram = thevalue as Stream;
                returnval = (int)thestram.Length;
            }
            else if (type.IsSerializable)
            {
                try
                {
                    using (Stream s = new MemoryStream())
                    {
                        var formatter = new BinaryFormatter();
                        formatter.Serialize(s, thevalue);
                        returnval = (int)s.Length;
                    }
                }
                catch { }
            }
            else
            {
                var fields = type.GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                for (int i = 0; i < fields.Length; i++)
                {
                    var t = fields[i].FieldType;
                    var v = fields[i].GetValue(thevalue);
                    returnval += 4 + SizeOfObj(t, v);
                }
            }
            if (returnval == 0)
                try
                {
                    returnval = System.Runtime.InteropServices.Marshal.SizeOf(thevalue);
                }
                catch { }

            return returnval;
        }

        static int SizeOf<T>(T value)
        {
            return SizeOfObj(typeof(T), value);
        }

        static void FillPersonCollection()
        {
            _people.Clear();

            Console.WriteLine("Filling person collection...");

            var sw = PerformanceStopwatch.StartNew();

            for (int personCount = 0; personCount < InterationCount; personCount++)
            {
                var person = new Person(Guid.NewGuid(), $"FakeEmail@{personCount}.com")
                {
                    FirstName = "Fake",
                    LastName = "Name",
                    BornOn = DateTime.Now
                };
            }

            var time = sw.StopReset();

            Console.WriteLine($"Creating Person collection [time={time.TotalMilliseconds} avg={InterationCount / time.TotalMilliseconds}]");
        }

        static ConcurrentDictionary<int, Person> TestConcurrentDictionary()
        {
            Console.WriteLine("Filling person concurrent dictionary...");

            var people = new ConcurrentDictionary<int, Person>();

            for (int personCount = 0; personCount < InterationCount; personCount++)
            {
                var person = new Person(Guid.NewGuid(), $"FakeEmail@{personCount}.com")
                {
                    FirstName = "Fake",
                    LastName = "Name",
                    BornOn = DateTime.Now
                };

                people.GetOrAdd(personCount, person);
            }

            return people;

        }

        static ConcurrentHashSet<Person> TestConcurrentHashSet()
        {
            Console.WriteLine("Filling person concurrent dictionary...");

            var people = new ConcurrentHashSet<Person>();

            for (int personCount = 0; personCount < InterationCount; personCount++)
            {
                var person = new Person(Guid.NewGuid(), $"FakeEmail@{personCount}.com")
                {
                    FirstName = "Fake",
                    LastName = "Name",
                    BornOn = DateTime.Now
                };

                people.Add(person);
            }

            return people;

        }

        static void TestSortedList()
        {
            var sw = PerformanceStopwatch.StartNew();

            var sorted = new SortedSet<string>();

            for (int sortedCount = 0; sortedCount < InterationCount; sortedCount++)
            {
                sorted.Add(sortedCount.ToString());
            }

            var sortedResult = sorted.ToList();

            var sortedTime = sw.StopReset();
            sw.Start();

            var notSorted = new dotNetTips.Utility.Standard.Collections.Generic.FastSortedList<string>();

            for (int notSortedCount = 0; notSortedCount < InterationCount; notSortedCount++)
            {
                notSorted.Add(notSortedCount.ToString());
            }

            var notSortedResult = notSorted.ToList();

            var notSortedTime = sw.StopReset();


            var message = $"Sorted [time={sortedTime.TotalMilliseconds} - Not Sorted [time={notSortedTime.TotalMilliseconds} %{(notSortedTime.TotalMilliseconds / sortedTime.TotalMilliseconds) * 100}]";

            Console.WriteLine(message);
        }

        static void TestValueComparison()
        {

            var sw = PerformanceStopwatch.StartNew();

            //Test ToLower & ==
            for (int lowerCount = 0; lowerCount < InterationCount; lowerCount++)
            {
                CompareValues("p", "PG");
                CompareValues("P", "pg");
                CompareValues("x", "px");
            }

            var lowerTest = sw.StopReset();

            sw.Start();

            //Test Equals
            for (int lowerCount = 0; lowerCount < InterationCount; lowerCount++)
            {
                CompareValuesFixed("p", "PG");
                CompareValuesFixed("P", "pg");
                CompareValuesFixed("x", "px");
            }

            var equalsTest = sw.StopReset();

            var message = $"ToLower [time={lowerTest.TotalMilliseconds} - Equals [time={equalsTest.TotalMilliseconds} %{(equalsTest.TotalMilliseconds / lowerTest.TotalMilliseconds) * 100}]";

            Console.WriteLine(message);
        }

        static void TestStringInterpolation()
        {

            var sw = PerformanceStopwatch.StartNew();

            //Test string.Format
            for (int count = 0; count < InterationCount; count++)
            {
                Console.WriteLine(string.Format("The time is: {0}.", DateTime.Now));
            }

            var formatTest = sw.StopReset();

            sw.Start();

            //Test Interpolation
            for (int count = 0; count < InterationCount; count++)
            {
                Console.WriteLine($"The time is: {DateTime.Now}.");
            }

            var interpolationTest = sw.StopReset();

            var message = $"string.Format [time={formatTest.TotalMilliseconds} - String Interpolation [time={interpolationTest.TotalMilliseconds} %{(interpolationTest.TotalMilliseconds / formatTest.TotalMilliseconds) * 100}]";

            Console.WriteLine(message);
        }

        static string CompareValues(string param1, string param2)
        {
            if ((param1.ToLower() == "p") || (param2.ToLower() == "pg"))
            {
                return "success";
            }
            else
            {
                return "failed";
            }
        }

        static string CompareValuesFixed(string param1, string param2)
        {
            const string testValueP = "p";
            const string testValuePg = "pg";

            if ((param1.Equals(testValueP, StringComparison.CurrentCultureIgnoreCase)) || (param2.Equals(testValuePg, StringComparison.CurrentCultureIgnoreCase)))
            {
                return "success";
            }
            else
            {
                return "failed";
            }

        }

    }
}
