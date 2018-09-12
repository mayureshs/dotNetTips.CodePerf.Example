// ***********************************************************************
// Assembly         : dotNetTips.CodePerf.Example.App
// Author           : David McCarter
// Created          : 07-18-2018
//
// Last Modified By : David McCarter
// Last Modified On : 07-18-2018
// ***********************************************************************
// <copyright file="CollectionExtensions.cs" company="dotNetTips.com - McCarter Consulting">
//     2018 David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetTips.CodePerf.Example.Extensions
{
    /// <summary>
    /// Class CollectionExtensions.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="newItems">The new items.</param>
        public static void FastAddRange<T>(this ICollection<T> list, IEnumerable<T> newItems)
        {
            Parallel.ForEach(newItems, (item) => { list.Add(item); });
        }

        /// <summary>
        /// Counts the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>System.Int32.</returns>
        public static int Count(this IEnumerable list)
        {
            if (list is ICollection collection)
            {
                return collection.Count;
            }

            var count = 0;

            var enumerator = list.GetEnumerator();

            while (enumerator.MoveNext())
            {
                count++;
            }

            return count;
        }

        /// <summary>
        /// Fasts any.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>System.Boolean.</returns>
        /// <exception cref="ArgumentNullException">predicate - predicate</exception>
        public static bool FastAny<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            var findAny = source.Select(predicate) != null;

            return findAny;
        }

        /// <summary>
        /// Fasts any with validation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException">predicate - predicate</exception>
        public static bool FastAnyWithValidation<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate), $"{nameof(predicate)} is null.");
            }

            var findAny = source.Select(predicate) != null;

            return findAny;
        }

        /// <summary>
        /// Counts the faster.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="ArgumentNullException">source
        /// or
        /// source</exception>
        /// <exception cref="Exception">source
        /// or
        /// source</exception>
        public static int FastCount<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {

            if (source is List<T>)
            {
                var finalCount = 0;
                var list = (List<T>)source;
                var count = list.Count;

                for (var j = 0; j < count; j++)
                {
                    if (predicate(list[j]))
                    {
                        finalCount++;
                    }
                }

                return finalCount;
            }

            return source.Count(predicate);
        }


        /// <summary>
        /// Finds first item or returns null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="match">The match.</param>
        /// <returns>System.Nullable&lt;T&gt;.</returns>
        /// <exception cref="ArgumentNullException">list - Source cannot be null.
        /// or
        /// match - Match cannot be null.</exception>
        public static T? FirstOrNull<T>(this IEnumerable<T> list, Func<T, bool> match)
            where T : struct
        {
            foreach (var local in list)
            {
                if (match?.Invoke(local) ?? default(bool))
                {
                    return new T?(local);
                }
            }

            return null;
        }

        /// <summary>
        /// Converts delimited string to list.
        /// </summary>
        /// <param name="delimitedInput">The string buffer.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        /// <remarks>Code by: Blake Pell</remarks>
        public static IEnumerable<string> FromDelimitedString(this string delimitedInput, char delimiter)
        {
            var items = delimitedInput.Split(delimiter);

            return items.AsEnumerable();
        }

        /// <summary>
        /// Determines whether the specified source has items.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns><c>true</c> if the specified source has items; otherwise, <c>false</c>.</returns>
        public static bool HasItems(this IEnumerable source) => source.Count() > 0;

        /// <summary>
        /// Determines whether the specified source has items.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns><c>true</c> if the specified source has items; otherwise, <c>false</c>.</returns>
        public static bool HasItems(this ICollection source) => source.Count > 0;

        /// <summary>
        /// Returns true if ... is valid (not null and has items).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns><c>true</c> if the specified list is valid; otherwise, <c>false</c>.</returns>
        public static bool IsValid<T>(this ObservableCollection<T> list) => ((list != null) && (list.Any()));

        /// <summary>
        /// Returns true if ... is valid (not null and has items).
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns><c>true</c> if the specified source is valid; otherwise, <c>false</c>.</returns>
        public static bool IsValid(this IEnumerable source) => source?.Count() > 0;

        /// <summary>
        /// Returns true if ... is valid (not null and contains items).
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns><c>true</c> if the specified source is valid; otherwise, <c>false</c>.</returns>
        public static bool IsValid(this ICollection source) => source?.Count > 0;

        /// <summary>
        /// Creates a Generic.List.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns>Task&lt;List&lt;T&gt;&gt;.</returns>
        /// <exception cref="ArgumentNullException">list - Source cannot be null or have a 0 value.</exception>
        public static async Task<List<T>> ToListAsync<T>(this IEnumerable<T> list) => await Task.Run(() => list.ToList());
    }
}