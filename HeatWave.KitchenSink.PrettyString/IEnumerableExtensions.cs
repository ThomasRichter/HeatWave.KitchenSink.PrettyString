using HeatWave.KitchenSink.ArgumentChecking;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeatWave.KitchenSink.PrettyString
{
    /// <summary>
    /// This class contains the extension methods for <see cref="IEnumerable{T}"/>
    /// and specializations for <see cref="ISet{T}"/> and <see cref="IDictionary{TKey, TValue}"/>.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Returns a string with the format [ first, ..., last ].
        /// If the <paramref name="enumerable"/> is a <see cref="ISet{T}"/>, the format { first, ..., last } is used.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">Not null.</param>
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
        /// the format { a: 1; b: 2; } is used. Otherwise returns a string with the format [ first, ..., last ].
        /// </summary>
        /// <param name="enumerable">Not null.</param>
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
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">Not null.</param>
        /// <param name="before"></param>
        /// <param name="separator"></param>
        /// <param name="after"></param>
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

        public static string ToPrettyString<T>(this ISet<T> set) => set.ToPrettyString("{", ", ", "}");

        public static string ToPrettyString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) => dictionary.ToPrettyString("{", ": ", "; ", "}");


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
