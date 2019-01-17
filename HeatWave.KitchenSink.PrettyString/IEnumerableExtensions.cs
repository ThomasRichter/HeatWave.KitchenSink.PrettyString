using HeatWave.KitchenSink.ArgumentChecking;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeatWave.KitchenSink.PrettyString
{
    /// <summary>
    /// This class contains the extension methods for:
    /// <para/>
    /// 
    /// <see cref="IEnumerable{T}"/>: Default-Format =[ first, ..., last ]
    /// <para/>
    /// 
    /// <see cref="ISet{T}"/>: Default-Format = { first, ..., last }
    /// <para/>
    /// 
    /// <see cref="IDictionary{TKey, TValue}"/>: Default-Format = { a: 1, b: 2 }
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Returns a string with the format [ first, ..., last ].
        /// If the <paramref name="enumerable"/> is a <see cref="ISet{T}"/>, the format { first, ..., last } is used.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The source of the elements to be printed. May not be null.</param>
        /// <returns>Returns a string with the format [ first, ..., last ].</returns>
        public static string ToPrettyString<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is ISet<T> set)
            {
                return set.ToPrettyString();
            }

            return enumerable.ToPrettyString("[", ", ", "]");
        }

        /// <summary>
        /// A specialized version of <see cref="ToPrettyString{T}(IEnumerable{T})"/> that
        /// accepts KeyValuePairs. If the enumerable is a <see cref="IDictionary{TKey, TValue}"/>, 
        /// the format { a: 1, b: 2 } is used. Otherwise returns a string with the format [ first, ..., last ].
        /// </summary>
        /// <param name="enumerable">The source of the elements to be printed. May not be null.</param>
        /// <returns>Returns a string with the dictionary format if the <paramref name="enumerable"/> is a dictionary, otherwise [ first, ..., last ].</returns>
        public static string ToPrettyString<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> enumerable)
        {
            if (enumerable is IDictionary<TKey, TValue> dictionary)
            {
                return dictionary.ToPrettyString();
            }

            return enumerable.ToPrettyString("[", ", ", "]");
        }

        /// <summary>
        /// Example: [ a, b, c ] is produced by before = "[", separator = ", ", after = "]".
        /// <para />
        /// No arguments passed to this method may be null.
        /// </summary>
        /// <param name="enumerable">The source of the elements to be printed. May not be null.</param>
        /// <param name="before">The prefix of the returned string. Must be non-null.</param>
        /// <param name="separator">The separator between elements. Must be non-null.</param>
        /// <param name="after">The postfix of the returned string. Must be non-null.</param>
        /// <returns></returns>
        public static string ToPrettyString<T>(this IEnumerable<T> enumerable, string before, string separator, string after)
        {
            CheckCallerNotNull(enumerable, nameof(enumerable));

            Argument.NoneNull(before, nameof(before), separator, nameof(separator), after, nameof(after));

            var sb = new StringBuilder(before, 200);

            var firstWasAppended = false;

            foreach (var element in enumerable)
            {
                if (firstWasAppended)
                {
                    sb.Append(separator)
                      .Append(element.ToString());
                }
                else
                {
                    sb.Append(' ')
                      .Append(element.ToString());

                    firstWasAppended = true;
                }
            }

            if (firstWasAppended)
            {
                sb.Append(' ');
            }

            return sb
                .Append(after)
                .ToString();
        }

        /// <summary>
        /// Returns a string with the format { first, ..., last }.
        /// </summary>
        /// <param name="set">The source of the elements to be printed. May not be null.</param>
        /// <returns>Returns a string with the format { first, ..., last }.</returns>
        public static string ToPrettyString<T>(this ISet<T> set) => set.ToPrettyString("{", ", ", "}");

        /// <summary>
        /// Returns a string with the format { a: 1, b: 2 }.
        /// </summary>
        /// <param name="dictionary">The source of the elements to be printed. May not be null.</param>
        /// <returns>Returns a string with the format { a: 1, b: 2 }.</returns>
        public static string ToPrettyString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) => dictionary.ToPrettyString("{", ": ", ", ", "}");

        /// <summary>
        /// Example: { a: 1, b: 2 } is produced by before = "{", keyValueJoiner = ": ", pairSeparator = ", ", after = "}".
        /// <para />
        /// No arguments passed to this method may be null.
        /// </summary>
        /// <param name="dictionary">The source of the elements to be printed. May not be null.</param>
        /// <param name="before">The prefix of the returned string. Must be non-null.</param>
        /// <param name="keyValueJoiner">The string that is placed between key and value of a key value pair. Must be non-null.</param>
        /// <param name="pairSeparator">The separator between key value pairs. Must be non-null.</param>
        /// <param name="after">The postfix of the returned string. Must be non-null.</param>
        /// <returns></returns>
        public static string ToPrettyString<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary, 
            string before, 
            string keyValueJoiner, 
            string pairSeparator, 
            string after)
        {
            CheckCallerNotNull(dictionary, nameof(dictionary));

            Argument.NoneNull(before, nameof(before), keyValueJoiner, nameof(keyValueJoiner), pairSeparator, nameof(pairSeparator), after, nameof(after));

            var sb = new StringBuilder(before, 200);

            var firstWasAppended = false;

            foreach (var pair in dictionary)
            {
                if (firstWasAppended)
                {
                    sb.Append(pairSeparator);
                    AppendPair(pair);
                }
                else
                {
                    sb.Append(' ');
                    AppendPair(pair);

                    firstWasAppended = true;
                }
            }

            if (firstWasAppended)
            {
                sb.Append(' ');
            }

            return sb
                .Append(after)
                .ToString();

            void AppendPair(KeyValuePair<TKey, TValue> pair)
            {
                sb.Append(pair.Key)
                  .Append(keyValueJoiner)
                  .Append(pair.Value);
            }
        }

        private static void CheckCallerNotNull(object caller, string argumentName)
        {
            if (caller == null)
            {
                throw new NullReferenceException($"Object reference '{argumentName}' not set to an instance of an object.");
            }
        }
    }
}
