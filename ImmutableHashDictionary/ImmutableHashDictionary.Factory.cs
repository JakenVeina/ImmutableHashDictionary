using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace System.Collections.Immutable.Extra
{
    /// <summary>
    /// Provides a variety of factory methods for creating <see cref="ImmutableHashDictionary{TKey, TValue}"/> objects.
    /// </summary>
    public static class ImmutableHashDictionary
    {
        /// <summary>
        /// Creates an empty <see cref="ImmutableHashDictionary{TKey, TValue}"/> object.
        /// </summary>
        /// <typeparam name="TKey">The type of key values to be used for dictionary entries.</typeparam>
        /// <typeparam name="TValue">The type of data values to be used for dictionary entries.</typeparam>
        /// <returns>An empty <see cref="ImmutableHashDictionary{TKey, TValue}"/> object.</returns>
        /// <remarks>
        /// This method actually just returns <see cref="ImmutableHashDictionary{TKey, TValue}.Empty"/>,
        /// to avoid unnecessary memory allocations.
        /// </remarks>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue> Create<TKey, TValue>()
            => ImmutableHashDictionary<TKey, TValue>.Empty;

        /// <summary>
        /// Creates an empty <see cref="ImmutableHashDictionary{TKey, TValue}"/> object,
        /// with the given <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TKey"/> values.
        /// </summary>
        /// <typeparam name="TKey">The type of key values to be used for dictionary entries.</typeparam>
        /// <typeparam name="TValue">The type of data values to be used for dictionary entries.</typeparam>
        /// <param name="keyComparer">
        /// The <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TKey"/> values
        /// within the created dictionary, and derived dictionaries.
        /// </param>
        /// <returns>
        /// An empty <see cref="ImmutableHashDictionary{TKey, TValue}"/> object,
        /// which will use <paramref name="keyComparer"/> for comparing <typeparamref name="TKey"/> values.
        /// </returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue> Create<TKey, TValue>(
                IEqualityComparer<TKey> keyComparer)
            => new ImmutableHashDictionary<TKey, TValue>(keyComparer: keyComparer);

        /// <summary>
        /// Creates an empty <see cref="ImmutableHashDictionary{TKey, TValue}"/> object,
        /// with the given <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TKey"/> values,
        /// and the given <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TValue"/> values.
        /// </summary>
        /// <typeparam name="TKey">The type of key values to be used for dictionary entries.</typeparam>
        /// <typeparam name="TValue">The type of data values to be used for dictionary entries.</typeparam>
        /// <param name="keyComparer">
        /// The <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TKey"/> values
        /// within the created dictionary, and derived dictionaries.
        /// </param>
        /// <param name="valueComparer">
        /// The <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TValue"/> values
        /// within the created dictionary, and derived dictionaries.
        /// </param>
        /// <returns>
        /// An empty <see cref="ImmutableHashDictionary{TKey, TValue}"/> object,
        /// which will use <paramref name="keyComparer"/> for comparing <typeparamref name="TKey"/> values,
        /// and <paramref name="valueComparer"/> for comparing <typeparamref name="TValue"/> values.
        /// </returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue> Create<TKey, TValue>(
                IEqualityComparer<TKey> keyComparer,
                IEqualityComparer<TValue> valueComparer)
            => new ImmutableHashDictionary<TKey, TValue>(keyComparer: keyComparer, valueComparer: valueComparer);

        /// <summary>
        /// Creates a new <see cref="ImmutableHashDictionary{TKey, TValue}"/> object,
        /// containing the given set of <see cref="KeyValuePair{TKey, TValue}"/> entries.
        /// </summary>
        /// <typeparam name="TKey">The type of key values to be used for dictionary entries.</typeparam>
        /// <typeparam name="TValue">The type of data values to be used for dictionary entries.</typeparam>
        /// <param name="items">
        /// The sequence of <see cref="KeyValuePair{TKey, TValue}"/> entries
        /// that the new <see cref="ImmutableHashDictionary{TKey, TValue}"/> should contain.
        /// </param>
        /// <exception cref="ArgumentNullException">Throws for <paramref name="items"/>.</exception>
        /// <returns>A <see cref="ImmutableHashDictionary{TKey, TValue}"/> that contains <paramref name="items"/>.</returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue> CreateRange<TKey, TValue>(
                IEnumerable<KeyValuePair<TKey, TValue>> items)
            => CreateRangeInternal(null, null, items);

        /// <summary>
        /// Creates a new <see cref="ImmutableHashDictionary{TKey, TValue}"/> object,
        /// containing the given set of <see cref="KeyValuePair{TKey, TValue}"/> entries,
        /// and with the given <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TKey"/> values.
        /// </summary>
        /// <typeparam name="TKey">The type of key values to be used for dictionary entries.</typeparam>
        /// <typeparam name="TValue">The type of data values to be used for dictionary entries.</typeparam>
        /// <param name="keyComparer">
        /// The <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TKey"/> values
        /// within the created dictionary, and derived dictionaries.
        /// </param>
        /// <param name="items">
        /// The sequence of <see cref="KeyValuePair{TKey, TValue}"/> entries
        /// that the new <see cref="ImmutableHashDictionary{TKey, TValue}"/> should contain.
        /// </param>
        /// <exception cref="ArgumentNullException">Throws for <paramref name="items"/>.</exception>
        /// <returns>
        /// A <see cref="ImmutableHashDictionary{TKey, TValue}"/> that contains <paramref name="items"/>,
        /// which will use <paramref name="keyComparer"/> for comparing <typeparamref name="TKey"/> values.
        /// </returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue> CreateRange<TKey, TValue>(
                IEqualityComparer<TKey> keyComparer,
                IEnumerable<KeyValuePair<TKey, TValue>> items)
            => CreateRangeInternal(keyComparer, null, items);

        /// <summary>
        /// Creates a new <see cref="ImmutableHashDictionary{TKey, TValue}"/> object,
        /// containing the given set of <see cref="KeyValuePair{TKey, TValue}"/> entries,
        /// with the given <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TKey"/> values,
        /// and with the given <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TValue"/> values.
        /// </summary>
        /// <typeparam name="TKey">The type of key values to be used for dictionary entries.</typeparam>
        /// <typeparam name="TValue">The type of data values to be used for dictionary entries.</typeparam>
        /// <param name="keyComparer">
        /// The <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TKey"/> values
        /// within the created dictionary, and derived dictionaries.
        /// </param>
        /// <param name="valueComparer">
        /// The <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TValue"/> values
        /// within the created dictionary, and derived dictionaries.
        /// </param>
        /// <param name="items">
        /// The sequence of <see cref="KeyValuePair{TKey, TValue}"/> entries
        /// that the new <see cref="ImmutableHashDictionary{TKey, TValue}"/> should contain.
        /// </param>
        /// <exception cref="ArgumentNullException">Throws for <paramref name="items"/>.</exception>
        /// <returns>
        /// A <see cref="ImmutableHashDictionary{TKey, TValue}"/> that contains <paramref name="items"/>,
        /// which will use <paramref name="keyComparer"/> for comparing <typeparamref name="TKey"/> values,
        /// and <paramref name="valueComparer"/> for comparing <typeparamref name="TValue"/> values.
        /// </returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue> CreateRange<TKey, TValue>(
                IEqualityComparer<TKey> keyComparer,
                IEqualityComparer<TValue> valueComparer,
                IEnumerable<KeyValuePair<TKey, TValue>> items)
            => CreateRangeInternal(keyComparer, valueComparer, items);

        /// <summary>
        /// Creates a new, empty, <see cref="ImmutableHashDictionary{TKey, TValue}.Builder"/> object,
        /// which may be used to create <see cref="ImmutableHashDictionary{TKey, TValue}"/> objects.
        /// </summary>
        /// <typeparam name="TKey">The type of key values to be used for dictionary entries.</typeparam>
        /// <typeparam name="TValue">The type of data values to be used for dictionary entries.</typeparam>
        /// <returns>A new, empty, <see cref="ImmutableHashDictionary{TKey, TValue}.Builder"/> object.</returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>()
            => new ImmutableHashDictionary<TKey, TValue>.Builder();

        /// <summary>
        /// Creates a new, empty, <see cref="ImmutableHashDictionary{TKey, TValue}.Builder"/> object,
        /// which may be used to create <see cref="ImmutableHashDictionary{TKey, TValue}"/> objects,
        /// with the given <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TKey"/> values.
        /// </summary>
        /// <typeparam name="TKey">The type of key values to be used for dictionary entries.</typeparam>
        /// <typeparam name="TValue">The type of data values to be used for dictionary entries.</typeparam>
        /// <param name="keyComparer">
        /// The <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TKey"/> values
        /// within the created dictionary, and derived dictionaries.
        /// </param>
        /// <returns>
        /// An empty <see cref="ImmutableHashDictionary{TKey, TValue}.Builder"/> object,
        /// which will use <paramref name="keyComparer"/> for comparing <typeparamref name="TKey"/> values.
        /// </returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>(
                IEqualityComparer<TKey> keyComparer)
            => new ImmutableHashDictionary<TKey, TValue>.Builder(keyComparer: keyComparer);

        /// <summary>
        /// Creates an empty <see cref="ImmutableHashDictionary{TKey, TValue}.Builder"/> object,
        /// which may be used to create <see cref="ImmutableHashDictionary{TKey, TValue}"/> objects,
        /// with the given <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TKey"/> values,
        /// and the given <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TValue"/> values.
        /// </summary>
        /// <typeparam name="TKey">The type of key values to be used for dictionary entries.</typeparam>
        /// <typeparam name="TValue">The type of data values to be used for dictionary entries.</typeparam>
        /// <param name="keyComparer">
        /// The <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TKey"/> values
        /// within the created dictionary, and derived dictionaries.
        /// </param>
        /// <param name="valueComparer">
        /// The <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TValue"/> values
        /// within the created dictionary, and derived dictionaries.
        /// </param>
        /// <returns>
        /// An empty <see cref="ImmutableHashDictionary{TKey, TValue}.Builder"/> object,
        /// which will use <paramref name="keyComparer"/> for comparing <typeparamref name="TKey"/> values,
        /// and <paramref name="valueComparer"/> for comparing <typeparamref name="TValue"/> values.
        /// </returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>(
                IEqualityComparer<TKey> keyComparer, 
                IEqualityComparer<TValue> valueComparer)
            => new ImmutableHashDictionary<TKey, TValue>.Builder(keyComparer: keyComparer, valueComparer: valueComparer);

        /// <summary>
        /// Creates a new <see cref="ImmutableHashDictionary{TKey, TValue}"/> object,
        /// containing <see cref="KeyValuePair{TKey, TValue}"/> entries built from the given sequence,
        /// and <typeparamref name="TKey"/> and <typeparamref name="TValue"/> selector delegates.
        /// </summary>
        /// <typeparam name="TSource">The type of values to be used to derive <see cref="KeyValuePair{TKey, TValue}"/> entries, for the dictionary.</typeparam>
        /// <typeparam name="TKey">The type of key values to be used for dictionary entries.</typeparam>
        /// <typeparam name="TValue">The type of data values to be used for dictionary entries.</typeparam>
        /// <param name="source">The sequence whose elements are to be used to derive <see cref="KeyValuePair{TKey, TValue}"/> entries, for the dictionary.</param>
        /// <param name="keySelector">The delegate to be used to derive <see cref="KeyValuePair{TKey, TValue}.Key"/> values from elements of <paramref name="source"/>.</param>
        /// <param name="valueSelector">The delegate to be used to derive <see cref="KeyValuePair{TKey, TValue}.Value"/> values from elements of <paramref name="source"/>.</param>
        /// <exception cref="ArgumentNullException">Throws for <paramref name="source"/>, <paramref name="keySelector"/>, and <paramref name="valueSelector"/>.</exception>
        /// <returns>
        /// An <see cref="ImmutableHashDictionary{TKey, TValue}"/> object containing <see cref="KeyValuePair{TKey, TValue}"/> entries
        /// derived from <paramref name="source"/>, using <paramref name="keySelector"/> and <paramref name="valueSelector"/>.
        /// </returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue> ToImmutableHashDictionary<TSource, TKey, TValue>(
                this IEnumerable<TSource> source,
                Func<TSource, TKey> keySelector,
                Func<TSource, TValue> valueSelector)
            => source.ToImmutableHashDictionaryInternal(keySelector, valueSelector, null, null);

        /// <summary>
        /// Creates a new <see cref="ImmutableHashDictionary{TKey, TValue}"/> object,
        /// containing <see cref="KeyValuePair{TKey, TValue}"/> entries built from the given sequence,
        /// and <typeparamref name="TKey"/> and <typeparamref name="TValue"/> selector delegates,
        /// with the given <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TKey"/> values.
        /// </summary>
        /// <typeparam name="TSource">The type of values to be used to derive <see cref="KeyValuePair{TKey, TValue}"/> entries, for the dictionary.</typeparam>
        /// <typeparam name="TKey">The type of key values to be used for dictionary entries.</typeparam>
        /// <typeparam name="TValue">The type of data values to be used for dictionary entries.</typeparam>
        /// <param name="source">The sequence whose elements are to be used to derive <see cref="KeyValuePair{TKey, TValue}"/> entries, for the dictionary.</param>
        /// <param name="keySelector">The delegate to be used to derive <see cref="KeyValuePair{TKey, TValue}.Key"/> values from elements of <paramref name="source"/>.</param>
        /// <param name="valueSelector">The delegate to be used to derive <see cref="KeyValuePair{TKey, TValue}.Value"/> values from elements of <paramref name="source"/>.</param>
        /// <param name="keyComparer">
        /// The <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TKey"/> values
        /// within the created dictionary, and derived dictionaries.
        /// </param>
        /// <exception cref="ArgumentNullException">Throws for <paramref name="source"/>, <paramref name="keySelector"/>, and <paramref name="valueSelector"/>.</exception>
        /// <returns>
        /// An <see cref="ImmutableHashDictionary{TKey, TValue}"/> object containing <see cref="KeyValuePair{TKey, TValue}"/> entries
        /// derived from <paramref name="source"/>, using <paramref name="keySelector"/> and <paramref name="valueSelector"/>,
        /// which will use <paramref name="keyComparer"/> for comparing <typeparamref name="TKey"/> values.
        /// </returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue> ToImmutableHashDictionary<TSource, TKey, TValue>(
                this IEnumerable<TSource> source,
                Func<TSource, TKey> keySelector,
                Func<TSource, TValue> valueSelector,
                IEqualityComparer<TKey> keyComparer)
            => source.ToImmutableHashDictionaryInternal(keySelector, valueSelector, keyComparer, null);

        /// <summary>
        /// Creates a new <see cref="ImmutableHashDictionary{TKey, TValue}"/> object,
        /// containing <see cref="KeyValuePair{TKey, TValue}"/> entries built from the given sequence,
        /// and <typeparamref name="TKey"/> and <typeparamref name="TValue"/> selector delegates,
        /// with the given <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TKey"/> values,
        /// and the given <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TValue"/> values.
        /// </summary>
        /// <typeparam name="TSource">The type of values to be used to derive <see cref="KeyValuePair{TKey, TValue}"/> entries, for the dictionary.</typeparam>
        /// <typeparam name="TKey">The type of key values to be used for dictionary entries.</typeparam>
        /// <typeparam name="TValue">The type of data values to be used for dictionary entries.</typeparam>
        /// <param name="source">The sequence whose elements are to be used to derive <see cref="KeyValuePair{TKey, TValue}"/> entries, for the dictionary.</param>
        /// <param name="keySelector">The delegate to be used to derive <see cref="KeyValuePair{TKey, TValue}.Key"/> values from elements of <paramref name="source"/>.</param>
        /// <param name="valueSelector">The delegate to be used to derive <see cref="KeyValuePair{TKey, TValue}.Value"/> values from elements of <paramref name="source"/>.</param>
        /// <param name="keyComparer">
        /// The <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TKey"/> values
        /// within the created dictionary, and derived dictionaries.
        /// </param>
        /// <param name="valueComparer">
        /// The <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TValue"/> values
        /// within the created dictionary, and derived dictionaries.
        /// </param>
        /// <exception cref="ArgumentNullException">Throws for <paramref name="source"/>, <paramref name="keySelector"/>, and <paramref name="valueSelector"/>.</exception>
        /// <returns>
        /// An <see cref="ImmutableHashDictionary{TKey, TValue}"/> object containing <see cref="KeyValuePair{TKey, TValue}"/> entries
        /// derived from <paramref name="source"/>, using <paramref name="keySelector"/> and <paramref name="valueSelector"/>,
        /// which will use <paramref name="keyComparer"/> for comparing <typeparamref name="TKey"/> values,
        /// and <paramref name="valueComparer"/> for comparing <typeparamref name="TValue"/> values.
        /// </returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue> ToImmutableHashDictionary<TSource, TKey, TValue>(
                this IEnumerable<TSource> source,
                Func<TSource, TKey> keySelector,
                Func<TSource, TValue> valueSelector,
                IEqualityComparer<TKey> keyComparer,
                IEqualityComparer<TValue> valueComparer)
            => source.ToImmutableHashDictionaryInternal(keySelector, valueSelector, keyComparer, valueComparer);

        /// <summary>
        /// Creates a new <see cref="ImmutableHashDictionary{TKey, TValue}"/> object,
        /// containing <see cref="KeyValuePair{TKey, TValue}"/> entries built from the given sequence
        /// of <typeparamref name="TValue"/> values, and <typeparamref name="TKey"/> selector delegate.
        /// </summary>
        /// <typeparam name="TKey">The type of key values to be used for dictionary entries.</typeparam>
        /// <typeparam name="TValue">The type of data values to be used for dictionary entries.</typeparam>
        /// <param name="values">The sequence of <typeparamref name="TValue"/> values to be used to derivce <see cref="KeyValuePair{TKey, TValue}"/> entries, for the dictionary.</param>
        /// <param name="keySelector">The delegate to be used to derive <see cref="KeyValuePair{TKey, TValue}.Key"/> values from elements of <paramref name="values"/>.</param>
        /// <exception cref="ArgumentNullException">Throws for <paramref name="values"/> and <paramref name="keySelector"/>.</exception>
        /// <returns>
        /// An <see cref="ImmutableHashDictionary{TKey, TValue}"/> object containing <see cref="KeyValuePair{TKey, TValue}"/> entries
        /// derived from <paramref name="values"/>, using <paramref name="keySelector"/>.
        /// </returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue> ToImmutableHashDictionary<TKey, TValue>(
                this IEnumerable<TValue> values,
                Func<TValue, TKey> keySelector)
            => values.ToImmutableHashDictionaryInternal(keySelector, null, null);

        /// <summary>
        /// Creates a new <see cref="ImmutableHashDictionary{TKey, TValue}"/> object,
        /// containing <see cref="KeyValuePair{TKey, TValue}"/> entries built from the given sequence
        /// of <typeparamref name="TValue"/> values, and <typeparamref name="TKey"/> selector delegate,
        /// with the given <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TKey"/> values,
        /// </summary>
        /// <typeparam name="TKey">The type of key values to be used for dictionary entries.</typeparam>
        /// <typeparam name="TValue">The type of data values to be used for dictionary entries.</typeparam>
        /// <param name="values">The sequence of <typeparamref name="TValue"/> values to be used to derivce <see cref="KeyValuePair{TKey, TValue}"/> entries, for the dictionary.</param>
        /// <param name="keySelector">The delegate to be used to derive <see cref="KeyValuePair{TKey, TValue}.Key"/> values from elements of <paramref name="values"/>.</param>
        /// <param name="keyComparer">
        /// The <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TKey"/> values
        /// within the created dictionary, and derived dictionaries.
        /// </param>
        /// <exception cref="ArgumentNullException">Throws for <paramref name="values"/> and <paramref name="keySelector"/>.</exception>
        /// <returns>
        /// An <see cref="ImmutableHashDictionary{TKey, TValue}"/> object containing <see cref="KeyValuePair{TKey, TValue}"/> entries
        /// derived from <paramref name="values"/>, using <paramref name="keySelector"/>,
        /// which will use <paramref name="keyComparer"/> for comparing <typeparamref name="TKey"/> values.
        /// </returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue> ToImmutableHashDictionary<TKey, TValue>(
                this IEnumerable<TValue> values,
                Func<TValue, TKey> keySelector,
                IEqualityComparer<TKey> keyComparer)
            => values.ToImmutableHashDictionaryInternal(keySelector, keyComparer, null);

        /// <summary>
        /// Creates a new <see cref="ImmutableHashDictionary{TKey, TValue}"/> object,
        /// containing <see cref="KeyValuePair{TKey, TValue}"/> entries built from the given sequence
        /// of <typeparamref name="TValue"/> values, and <typeparamref name="TKey"/> selector delegate,
        /// with the given <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TKey"/> values,
        /// and the given <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TValue"/> values.
        /// </summary>
        /// <typeparam name="TKey">The type of key values to be used for dictionary entries.</typeparam>
        /// <typeparam name="TValue">The type of data values to be used for dictionary entries.</typeparam>
        /// <param name="values">The sequence of <typeparamref name="TValue"/> values to be used to derivce <see cref="KeyValuePair{TKey, TValue}"/> entries, for the dictionary.</param>
        /// <param name="keySelector">The delegate to be used to derive <see cref="KeyValuePair{TKey, TValue}.Key"/> values from elements of <paramref name="values"/>.</param>
        /// <param name="keyComparer">
        /// The <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TKey"/> values
        /// within the created dictionary, and derived dictionaries.
        /// </param>
        /// <param name="valueComparer">
        /// The <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TValue"/> values
        /// within the created dictionary, and derived dictionaries.
        /// </param>
        /// <exception cref="ArgumentNullException">Throws for <paramref name="values"/> and <paramref name="keySelector"/>.</exception>
        /// <returns>
        /// An <see cref="ImmutableHashDictionary{TKey, TValue}"/> object containing <see cref="KeyValuePair{TKey, TValue}"/> entries
        /// derived from <paramref name="values"/>, using <paramref name="keySelector"/>,
        /// which will use <paramref name="keyComparer"/> for comparing <typeparamref name="TKey"/> values,
        /// and <paramref name="valueComparer"/> for comparing <typeparamref name="TValue"/> values.
        /// </returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue> ToImmutableHashDictionary<TKey, TValue>(
                this IEnumerable<TValue> values,
                Func<TValue, TKey> keySelector,
                IEqualityComparer<TKey> keyComparer,
                IEqualityComparer<TValue> valueComparer)
            => values.ToImmutableHashDictionaryInternal(keySelector, keyComparer, valueComparer);

        /// <summary>
        /// Creates a new <see cref="ImmutableHashDictionary{TKey, TValue}"/> object,
        /// with the given sequence of <see cref="KeyValuePair{TKey, TValue}"/> entries.
        /// </summary>
        /// <typeparam name="TKey">The type of key values to be used for dictionary entries.</typeparam>
        /// <typeparam name="TValue">The type of data values to be used for dictionary entries.</typeparam>
        /// <param name="items">The sequence of <see cref="KeyValuePair{TKey, TValue}"/> entries, for the dictionary.</param>
        /// <exception cref="ArgumentNullException">Throws for <paramref name="items"/>.</exception>"
        /// <returns>
        /// An <see cref="ImmutableHashDictionary{TKey, TValue}"/> object containing the sequence
        /// of <see cref="KeyValuePair{TKey, TValue}"/> entries given by <paramref name="items"/>.
        /// </returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue> ToImmutableHashDictionary<TKey, TValue>(
                this IEnumerable<KeyValuePair<TKey, TValue>> items)
            => CreateRangeInternal(null, null, items);

        /// <summary>
        /// Creates a new <see cref="ImmutableHashDictionary{TKey, TValue}"/> object,
        /// with the given sequence of <see cref="KeyValuePair{TKey, TValue}"/> entries,
        /// and the given <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TKey"/> values,
        /// </summary>
        /// <typeparam name="TKey">The type of key values to be used for dictionary entries.</typeparam>
        /// <typeparam name="TValue">The type of data values to be used for dictionary entries.</typeparam>
        /// <param name="items">The sequence of <see cref="KeyValuePair{TKey, TValue}"/> entries, for the dictionary.</param>
        /// <param name="keyComparer">
        /// The <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TKey"/> values
        /// within the created dictionary, and derived dictionaries.
        /// </param>
        /// <exception cref="ArgumentNullException">Throws for <paramref name="items"/>.</exception>"
        /// <returns>
        /// An <see cref="ImmutableHashDictionary{TKey, TValue}"/> object containing the sequence
        /// of <see cref="KeyValuePair{TKey, TValue}"/> entries given by <paramref name="items"/>,
        /// which will use <paramref name="keyComparer"/> for comparing <typeparamref name="TKey"/> values.
        /// </returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue> ToImmutableHashDictionary<TKey, TValue>(
                this IEnumerable<KeyValuePair<TKey, TValue>> items,
                IEqualityComparer<TKey> keyComparer)
            => CreateRangeInternal(keyComparer, null, items);

        /// <summary>
        /// Creates a new <see cref="ImmutableHashDictionary{TKey, TValue}"/> object,
        /// with the given sequence of <see cref="KeyValuePair{TKey, TValue}"/> entries,
        /// the given <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TKey"/> values,
        /// and the given <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TValue"/> values.
        /// </summary>
        /// <typeparam name="TKey">The type of key values to be used for dictionary entries.</typeparam>
        /// <typeparam name="TValue">The type of data values to be used for dictionary entries.</typeparam>
        /// <param name="items">The sequence of <see cref="KeyValuePair{TKey, TValue}"/> entries, for the dictionary.</param>
        /// <param name="keyComparer">
        /// The <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TKey"/> values
        /// within the created dictionary, and derived dictionaries.
        /// </param>
        /// <param name="valueComparer">
        /// The <see cref="IEqualityComparer{T}"/> to be used for comparing <typeparamref name="TValue"/> values
        /// within the created dictionary, and derived dictionaries.
        /// </param>
        /// <exception cref="ArgumentNullException">Throws for <paramref name="items"/>.</exception>"
        /// <returns>
        /// An <see cref="ImmutableHashDictionary{TKey, TValue}"/> object containing the sequence
        /// of <see cref="KeyValuePair{TKey, TValue}"/> entries given by <paramref name="items"/>,
        /// which will use <paramref name="keyComparer"/> for comparing <typeparamref name="TKey"/> values,
        /// and <paramref name="valueComparer"/> for comparing <typeparamref name="TValue"/> values.
        /// </returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue> ToImmutableHashDictionary<TKey, TValue>(
                this IEnumerable<KeyValuePair<TKey, TValue>> items,
                IEqualityComparer<TKey> keyComparer,
                IEqualityComparer<TValue> valueComparer)
            => CreateRangeInternal(keyComparer, valueComparer, items);

        [Pure]
        private static ImmutableHashDictionary<TKey, TValue> CreateRangeInternal<TKey, TValue>(
            IEqualityComparer<TKey>? keyComparer,
            IEqualityComparer<TValue>? valueComparer,
            IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            if (items is null)
                throw new ArgumentNullException(nameof(items));

            var dictionary = null as Dictionary<TKey, TValue>;
            foreach (var item in items)
            {
                if (dictionary is null)
                    dictionary = new Dictionary<TKey, TValue>(keyComparer);

                dictionary.Add(item.Key, item.Value);
            }

            return new ImmutableHashDictionary<TKey, TValue>(dictionary: dictionary, keyComparer: keyComparer, valueComparer: valueComparer);
        }

        [Pure]
        private static ImmutableHashDictionary<TKey, TValue> ToImmutableHashDictionaryInternal<TSource, TKey, TValue>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TValue> elementSelector,
            IEqualityComparer<TKey>? keyComparer,
            IEqualityComparer<TValue>? valueComparer)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            if (keySelector is null)
                throw new ArgumentNullException(nameof(keySelector));

            if (elementSelector is null)
                throw new ArgumentNullException(nameof(elementSelector));

            return CreateRangeInternal(
                keyComparer,
                valueComparer,
                source.Select(x => new KeyValuePair<TKey, TValue>(keySelector.Invoke(x), elementSelector.Invoke(x))));
        }

        [Pure]
        private static ImmutableHashDictionary<TKey, TValue> ToImmutableHashDictionaryInternal<TKey, TValue>(
            this IEnumerable<TValue> values,
            Func<TValue, TKey> keySelector,
            IEqualityComparer<TKey>? keyComparer,
            IEqualityComparer<TValue>? valueComparer)
        {
            if (values is null)
                throw new ArgumentNullException(nameof(values));

            if (keySelector is null)
                throw new ArgumentNullException(nameof(keySelector));

            return CreateRangeInternal(
                keyComparer,
                valueComparer,
                values.Select(value => new KeyValuePair<TKey, TValue>(keySelector.Invoke(value), value)));
        }
    }
}
