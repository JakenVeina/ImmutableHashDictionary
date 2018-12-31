using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace System.Collections.Immutable.Extra
{
    public partial class ImmutableHashDictionary<TKey, TValue>
    {
		public class Builder : IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>, IDictionary
        {
            #region Builder

            /// <summary>
            /// The <see cref="IEqualityComparer{T}"/> used to compare <typeparamref name="TKey"/> values within the dictionary.
            /// </summary>
            public IEqualityComparer<TKey> KeyComparer
                => throw new NotImplementedException();

            /// <summary>
            /// The set of <see cref="KeyValuePair{TKey, TValue}.Key"/> values of each <see cref="KeyValuePair{TKey, TValue}"/> in the dictionary.
            /// </summary>
            public IReadOnlyCollection<TKey> Keys
                => throw new NotImplementedException();

            /// <summary>
            /// The <see cref="IEqualityComparer{T}"/> used to compare <typeparamref name="TValue"/> values within the dictionary.
            /// </summary>
            public IEqualityComparer<TValue> ValueComparer
                => throw new NotImplementedException();

            /// <summary>
            /// The set of <see cref="KeyValuePair{TKey, TValue}.Value"/> values of each <see cref="KeyValuePair{TKey, TValue}"/> in the dictionary.
            /// </summary>
            public IReadOnlyCollection<TValue> Values
                => throw new NotImplementedException();

            /// <summary>
            /// Determines whether the <see cref="ImmutableDictionary{TKey, TValue}"/>
            /// contains an element with the specified value.
            /// </summary>
            /// <param name="value">
            /// The value to locate in the <see cref="ImmutableDictionary{TKey, TValue}"/>.
            /// </param>
            /// <returns>
            /// true if the <see cref="ImmutableDictionary{TKey, TValue}"/> contains
            /// an element with the specified value; otherwise, false.
            /// </returns>
            [Pure]
            public bool ContainsValue(TValue value)
                => throw new NotImplementedException();

            /// <summary>
            /// Adds a set of key/value entries to the dictionary
            /// </summary>
            /// <param name="items">The entries to be added.</param>
            /// <exception cref="ArgumentException">Throws if <paramref name="items"/> contains a key that already exists within the dictionary, mapped to a different value.</exception>
            public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> items)
                => throw new NotImplementedException();

            /// <summary>
            /// Removes any entries from the dictionary whose keys match those given.
            /// </summary>
            /// <param name="keys">The keys whose entries are to be removed from the dictionary.</param>
            public void RemoveRange(IEnumerable<TKey> keys)
                => throw new NotImplementedException();

            /// <summary>
            /// Gets the value for a given key if a matching key exists in the dictionary.
            /// </summary>
            /// <param name="key">The key to search for.</param>
            /// <returns>The value for the key, or the default value of type <typeparamref name="TValue"/> if no matching key was found.</returns>
            [Pure]
            public TValue GetValueOrDefault(TKey key)
                => throw new NotImplementedException();

            /// <summary>
            /// Gets the value for a given key if a matching key exists in the dictionary.
            /// </summary>
            /// <param name="key">The key to search for.</param>
            /// <param name="defaultValue">The default value to return if no matching key is found in the dictionary.</param>
            /// <returns>
            /// The value for the key, or <paramref name="defaultValue"/> if no matching key was found.
            /// </returns>
            [Pure]
            public TValue GetValueOrDefault(TKey key, TValue defaultValue)
                => throw new NotImplementedException();

            /// <summary>
            /// Converts the current contents of the builder to a new <see cref="ImmutableHashDictionary{TKey, TValue}"/>.
            /// </summary>
            /// <returns>An <see cref="ImmutableHashDictionary{TKey, TValue}"/>, containing the same entries as the buidler instance.</returns>
            /// <remarks>
            /// This method is an O(1) operation, as it simply involves wrapping a new <see cref="ImmutableHashDictionary{TKey, TValue}"/>
            /// around the <see cref="Dictionary{TKey, TValue}"/> that has been built. The next mutation operation performed on this builder
            /// will be at least O(N), as it will require duplicating the entire <see cref="Dictionary{TKey, TValue}"/>, to prevent the
            /// previously-created <see cref="ImmutableHashDictionary{TKey, TValue}"/> from being mutated.
            /// </remarks>
            public ImmutableHashDictionary<TKey, TValue> ToImmutable()
                => throw new NotImplementedException();

            #endregion Builder

            #region IDictionary

            /// <inheritdoc />
            public TValue this[TKey key]
            {
                get => throw new NotImplementedException();
                set => throw new NotImplementedException();
            }

            /// <inheritdoc />
            ICollection<TKey> IDictionary<TKey, TValue>.Keys
                => throw new NotImplementedException();

            /// <inheritdoc />
            ICollection<TValue> IDictionary<TKey, TValue>.Values
                => throw new NotImplementedException();

            /// <inheritdoc />
            public void Add(TKey key, TValue value)
                => throw new NotImplementedException();

            /// <inheritdoc />
            [Pure]
            public bool ContainsKey(TKey key)
                => throw new NotImplementedException();

            /// <inheritdoc />
            public bool Remove(TKey key)
                => throw new NotImplementedException();

            /// <inheritdoc />
            [Pure]
            public bool TryGetValue(TKey key, out TValue value)
                => throw new NotImplementedException();

            /// <inheritdoc />
            object IDictionary.this[object key]
            {
                get => throw new NotImplementedException();
                set => throw new NotImplementedException();
            }

            /// <inheritdoc />
            bool IDictionary.IsFixedSize
                => throw new NotImplementedException();

            /// <inheritdoc />
            bool IDictionary.IsReadOnly
                => throw new NotImplementedException();

            /// <inheritdoc />
            ICollection IDictionary.Keys
                => throw new NotImplementedException();

            /// <inheritdoc />
            ICollection IDictionary.Values
                => throw new NotImplementedException();

            /// <inheritdoc />
            void IDictionary.Add(object key, object value)
                => throw new NotImplementedException();

            /// <inheritdoc />
            void IDictionary.Clear()
                => throw new NotImplementedException();

            /// <inheritdoc />
            bool IDictionary.Contains(object key)
                => throw new NotImplementedException();

            /// <inheritdoc />
            IDictionaryEnumerator IDictionary.GetEnumerator()
                => throw new NotImplementedException();

            /// <inheritdoc />
            void IDictionary.Remove(object key)
                => throw new NotImplementedException();

            #endregion IDictionary

            #region ICollection

            /// <inheritdoc />
            public int Count
                => throw new NotImplementedException();

            /// <inheritdoc />
            bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
                => throw new NotImplementedException();

            /// <inheritdoc />
            public void Add(KeyValuePair<TKey, TValue> item)
                => throw new NotImplementedException();

            /// <inheritdoc />
            public void Clear()
                => throw new NotImplementedException();

            /// <inheritdoc />
            public bool Contains(KeyValuePair<TKey, TValue> item)
                => throw new NotImplementedException();

            /// <inheritdoc />
            void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
                => throw new NotImplementedException();

            /// <inheritdoc />
            bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
                => throw new NotImplementedException();

            /// <inheritdoc />
            bool ICollection.IsSynchronized
                => throw new NotImplementedException();

            /// <inheritdoc />
            object ICollection.SyncRoot
                => throw new NotImplementedException();

            /// <inheritdoc />
            void ICollection.CopyTo(Array array, int index)
                => throw new NotImplementedException();

            #endregion ICollection

            #region IReadOnlyDictionary

            /// <inheritdoc />
            [Pure]
            IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
                => throw new NotImplementedException();

            /// <inheritdoc />
            [Pure]
            IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
                => throw new NotImplementedException();

            #endregion IReadOnlyDictionary

            #region IEnumerable

            /// <inheritdoc />
            [Pure]
            public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
                => throw new NotImplementedException();

            /// <inheritdoc />
            [Pure]
            IEnumerator IEnumerable.GetEnumerator()
                => throw new NotImplementedException();

            #endregion IEnumerable
        }
    }
}
