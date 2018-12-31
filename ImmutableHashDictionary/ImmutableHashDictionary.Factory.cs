using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace System.Collections.Immutable.Extra
{
    /// <summary>
    /// A set of initialization methods for instances of <see cref="ImmutableHashDictionary{TKey, TValue}"/>.
    /// </summary>
    public static class ImmutableHashDictionary
    {
        /// <summary>
        /// Returns an empty collection.
        /// </summary>
        /// <typeparam name="TKey">The type of keys stored by the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values stored by the dictionary.</typeparam>
        /// <returns>The immutable collection.</returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue> Create<TKey, TValue>()
            => ImmutableHashDictionary<TKey, TValue>.Empty;

        /// <summary>
        /// Returns an empty collection with the specified key comparer.
        /// </summary>
        /// <typeparam name="TKey">The type of keys stored by the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values stored by the dictionary.</typeparam>
        /// <param name="keyComparer">The key comparer.</param>
        /// <returns>
        /// The immutable collection.
        /// </returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue> Create<TKey, TValue>(IEqualityComparer<TKey> keyComparer)
            => throw new NotImplementedException();

        /// <summary>
        /// Returns an empty collection with the specified comparers.
        /// </summary>
        /// <typeparam name="TKey">The type of keys stored by the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values stored by the dictionary.</typeparam>
        /// <param name="keyComparer">The key comparer.</param>
        /// <param name="valueComparer">The value comparer.</param>
        /// <returns>
        /// The immutable collection.
        /// </returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue> Create<TKey, TValue>(IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
            => throw new NotImplementedException();

        /// <summary>
        /// Creates a new immutable collection prefilled with the specified items.
        /// </summary>
        /// <typeparam name="TKey">The type of keys stored by the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values stored by the dictionary.</typeparam>
        /// <param name="items">The items to prepopulate.</param>
        /// <returns>The new immutable collection.</returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue> CreateRange<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> items)
            => throw new NotImplementedException();

        /// <summary>
        /// Creates a new immutable collection prefilled with the specified items.
        /// </summary>
        /// <typeparam name="TKey">The type of keys stored by the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values stored by the dictionary.</typeparam>
        /// <param name="keyComparer">The key comparer.</param>
        /// <param name="items">The items to prepopulate.</param>
        /// <returns>The new immutable collection.</returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue> CreateRange<TKey, TValue>(IEqualityComparer<TKey> keyComparer, IEnumerable<KeyValuePair<TKey, TValue>> items)
            => throw new NotImplementedException();

        /// <summary>
        /// Creates a new immutable collection prefilled with the specified items.
        /// </summary>
        /// <typeparam name="TKey">The type of keys stored by the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values stored by the dictionary.</typeparam>
        /// <param name="keyComparer">The key comparer.</param>
        /// <param name="valueComparer">The value comparer.</param>
        /// <param name="items">The items to prepopulate.</param>
        /// <returns>The new immutable collection.</returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue> CreateRange<TKey, TValue>(IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer, IEnumerable<KeyValuePair<TKey, TValue>> items)
            => throw new NotImplementedException();

        /// <summary>
        /// Creates a new immutable dictionary builder.
        /// </summary>
        /// <typeparam name="TKey">The type of keys stored by the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values stored by the dictionary.</typeparam>
        /// <returns>The new builder.</returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>()
            => throw new NotImplementedException();

        /// <summary>
        /// Creates a new immutable dictionary builder.
        /// </summary>
        /// <typeparam name="TKey">The type of keys stored by the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values stored by the dictionary.</typeparam>
        /// <param name="keyComparer">The key comparer.</param>
        /// <returns>The new builder.</returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>(IEqualityComparer<TKey> keyComparer)
            => throw new NotImplementedException();

        /// <summary>
        /// Creates a new immutable dictionary builder.
        /// </summary>
        /// <typeparam name="TKey">The type of keys stored by the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values stored by the dictionary.</typeparam>
        /// <param name="keyComparer">The key comparer.</param>
        /// <param name="valueComparer">The value comparer.</param>
        /// <returns>The new builder.</returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>(IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
            => throw new NotImplementedException();

        /// <summary>
        /// Constructs an immutable dictionary based on some transformation of a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of element in the sequence.</typeparam>
        /// <typeparam name="TKey">The type of key in the resulting map.</typeparam>
        /// <typeparam name="TValue">The type of value in the resulting map.</typeparam>
        /// <param name="source">The sequence to enumerate to generate the map.</param>
        /// <param name="keySelector">The function that will produce the key for the map from each sequence element.</param>
        /// <param name="elementSelector">The function that will produce the value for the map from each sequence element.</param>
        /// <param name="keyComparer">The key comparer to use for the map.</param>
        /// <param name="valueComparer">The value comparer to use for the map.</param>
        /// <returns>The immutable map.</returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue> ToImmutableHashDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
            => throw new NotImplementedException();

        /// <summary>
        /// Returns an immutable copy of the current contents of the builder's collection.
        /// </summary>
        /// <param name="builder">The builder to create the immutable dictionary from.</param>
        /// <returns>An immutable dictionary.</returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue> ToImmutableHashDictionary<TKey, TValue>(this ImmutableHashDictionary<TKey, TValue>.Builder builder)
            => throw new NotImplementedException();

        /// <summary>
        /// Constructs an immutable dictionary based on some transformation of a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of element in the sequence.</typeparam>
        /// <typeparam name="TKey">The type of key in the resulting map.</typeparam>
        /// <typeparam name="TValue">The type of value in the resulting map.</typeparam>
        /// <param name="source">The sequence to enumerate to generate the map.</param>
        /// <param name="keySelector">The function that will produce the key for the map from each sequence element.</param>
        /// <param name="elementSelector">The function that will produce the value for the map from each sequence element.</param>
        /// <param name="keyComparer">The key comparer to use for the map.</param>
        /// <returns>The immutable map.</returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue> ToImmutableHashDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, IEqualityComparer<TKey> keyComparer)
            => throw new NotImplementedException();

        /// <summary>
        /// Constructs an immutable dictionary based on some transformation of a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of element in the sequence.</typeparam>
        /// <typeparam name="TKey">The type of key in the resulting map.</typeparam>
        /// <param name="source">The sequence to enumerate to generate the map.</param>
        /// <param name="keySelector">The function that will produce the key for the map from each sequence element.</param>
        /// <returns>The immutable map.</returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TSource> ToImmutableHashDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
            => throw new NotImplementedException();

        /// <summary>
        /// Constructs an immutable dictionary based on some transformation of a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of element in the sequence.</typeparam>
        /// <typeparam name="TKey">The type of key in the resulting map.</typeparam>
        /// <param name="source">The sequence to enumerate to generate the map.</param>
        /// <param name="keySelector">The function that will produce the key for the map from each sequence element.</param>
        /// <param name="keyComparer">The key comparer to use for the map.</param>
        /// <returns>The immutable map.</returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TSource> ToImmutableHashDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> keyComparer)
            => throw new NotImplementedException();

        /// <summary>
        /// Constructs an immutable dictionary based on some transformation of a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of element in the sequence.</typeparam>
        /// <typeparam name="TKey">The type of key in the resulting map.</typeparam>
        /// <typeparam name="TValue">The type of value in the resulting map.</typeparam>
        /// <param name="source">The sequence to enumerate to generate the map.</param>
        /// <param name="keySelector">The function that will produce the key for the map from each sequence element.</param>
        /// <param name="elementSelector">The function that will produce the value for the map from each sequence element.</param>
        /// <returns>The immutable map.</returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue> ToImmutableHashDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector)
            => throw new NotImplementedException();

        /// <summary>
        /// Creates an immutable dictionary given a sequence of key=value pairs.
        /// </summary>
        /// <typeparam name="TKey">The type of key in the map.</typeparam>
        /// <typeparam name="TValue">The type of value in the map.</typeparam>
        /// <param name="source">The sequence of key=value pairs.</param>
        /// <param name="keyComparer">The key comparer to use when building the immutable map.</param>
        /// <param name="valueComparer">The value comparer to use for the immutable map.</param>
        /// <returns>An immutable map.</returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue> ToImmutableHashDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
            => throw new NotImplementedException();

        /// <summary>
        /// Creates an immutable dictionary given a sequence of key=value pairs.
        /// </summary>
        /// <typeparam name="TKey">The type of key in the map.</typeparam>
        /// <typeparam name="TValue">The type of value in the map.</typeparam>
        /// <param name="source">The sequence of key=value pairs.</param>
        /// <param name="keyComparer">The key comparer to use when building the immutable map.</param>
        /// <returns>An immutable map.</returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue> ToImmutableHashDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey> keyComparer)
            => throw new NotImplementedException();

        /// <summary>
        /// Creates an immutable dictionary given a sequence of key=value pairs.
        /// </summary>
        /// <typeparam name="TKey">The type of key in the map.</typeparam>
        /// <typeparam name="TValue">The type of value in the map.</typeparam>
        /// <param name="source">The sequence of key=value pairs.</param>
        /// <returns>An immutable map.</returns>
        [Pure]
        public static ImmutableHashDictionary<TKey, TValue> ToImmutableHashDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
            => throw new NotImplementedException();
    }
}