using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace System.Collections.Immutable.Extra
{
    public partial class ImmutableHashDictionary<TKey, TValue>
    {
        /// <summary>
        /// A traditional mutable dictionary that can be used to efficiently generate <see cref="ImmutableHashDictionary{TKey, TValue}"/> objects.
        /// </summary>
        [DebuggerDisplay("Count = {Count}")]
		public sealed class Builder
            : IDictionary<TKey, TValue>,
                IReadOnlyDictionary<TKey, TValue>,
                IDictionary
        {
            #region Constructors

            internal Builder(Dictionary<TKey, TValue>? dictionary = null, IEqualityComparer<TKey>? keyComparer = null, IEqualityComparer<TValue>? valueComparer = null)
            {
                _currentDictionary = ((dictionary?.Count ?? 0) == 0)
                    ? null
                    : new Dictionary<TKey, TValue>(dictionary);
                _keyComparer = keyComparer ?? EqualityComparer<TKey>.Default;
                _valueComparer = valueComparer ?? EqualityComparer<TValue>.Default;
            }

            #endregion Constructors

            #region Public Members
            #pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference or unconstrained type parameter.

            /// <inheritdoc />
            public TValue this[TKey key]
            {
                get => CurrentDictionaryForRead[key];
                set => CurrentDictionaryForWrite[key] = value;
            }

            /// <inheritdoc />
            public int Count
                => CurrentDictionaryForRead.Count;

            /// <summary>
            /// The <see cref="IEqualityComparer{T}"/> used to compare <typeparamref name="TKey"/> values within the dictionary.
            /// </summary>
            public IEqualityComparer<TKey> KeyComparer
                => _keyComparer;

            /// <summary>
            /// The set of <see cref="KeyValuePair{TKey, TValue}.Key"/> values of each <see cref="KeyValuePair{TKey, TValue}"/> in the dictionary.
            /// </summary>
            public IReadOnlyCollection<TKey> Keys
                => CurrentDictionaryForRead.Keys;

            /// <summary>
            /// The <see cref="IEqualityComparer{T}"/> used to compare <typeparamref name="TValue"/> values within the dictionary.
            /// </summary>
            public IEqualityComparer<TValue> ValueComparer
                => _valueComparer;

            /// <summary>
            /// The set of <see cref="KeyValuePair{TKey, TValue}.Value"/> values of each <see cref="KeyValuePair{TKey, TValue}"/> in the dictionary.
            /// </summary>
            public IReadOnlyCollection<TValue> Values
                => CurrentDictionaryForRead.Values;

            /// <inheritdoc />
            public void Clear()
                => (CurrentDictionaryForWrite as ICollection<KeyValuePair<TKey, TValue>>).Clear();

            /// <inheritdoc />
            [Pure]
            public bool Contains(KeyValuePair<TKey, TValue> item)
                => (CurrentDictionaryForRead as ICollection<KeyValuePair<TKey, TValue>>).Contains(item);

            /// <inheritdoc />
            [Pure]
            public bool ContainsKey(TKey key)
                => CurrentDictionaryForRead.ContainsKey(key);

            /// <summary>
            /// Determines whether the <see cref="ImmutableHashDictionary{TKey, TValue}.Builder"/>
            /// contains an element with the specified value.
            /// </summary>
            /// <param name="value">
            /// The value to locate in the <see cref="ImmutableHashDictionary{TKey, TValue}.Builder"/>.
            /// </param>
            /// <returns>
            /// true if the <see cref="ImmutableHashDictionary{TKey, TValue}.Builder"/> contains
            /// an element with the specified value; otherwise, false.
            /// </returns>
            [Pure]
            public bool ContainsValue(TValue value)
            {
                foreach (var item in CurrentDictionaryForRead)
                    if (ValueComparer.Equals(value, item.Value))
                        return true;

                return false;
            }

            /// <inheritdoc />
            public void Add(TKey key, TValue value)
                => CurrentDictionaryForWrite.Add(key, value);

            /// <summary>
            /// Adds a set of key/value entries to the dictionary
            /// </summary>
            /// <param name="items">The entries to be added.</param>
            /// <remarks>
            /// This method requires a re-allocation of the entire dictionary, to ensure that the operation remains atomic.
            /// Unless an atomic operation is required (I.E. the dictionary must not mutate in the event of an exception),
            /// you should use <see cref="Add(TKey, TValue)"/> instead.
            /// </remarks>
            public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> items)
            {
                if (items is null)
                    throw new ArgumentNullException(nameof(items));

                var newDictionary = new Dictionary<TKey, TValue>(CurrentDictionaryForRead);
                foreach(var item in items)
                {
                    if (newDictionary.TryGetValue(item.Key, out var existingValue))
                    {
                        if (_valueComparer.Equals(existingValue, item.Value))
                            newDictionary[item.Key] = item.Value;
                        else
                            throw new ArgumentException(nameof(items), $"An element with the same key but a different value already exists. Key: {item.Key}");
                    }
                    else
                        newDictionary.Add(item.Key, item.Value);
                }

                _currentDictionary = newDictionary;
            }

            /// <inheritdoc />
            public bool Remove(TKey key)
                => CurrentDictionaryForWrite.Remove(key);

            /// <summary>
            /// Removes any entries from the dictionary whose keys match those given.
            /// </summary>
            /// <param name="keys">The keys whose entries are to be removed from the dictionary.</param>
            /// <remarks>
            /// This method requires a re-allocation of the entire dictionary, to ensure that the operation remains atomic.
            /// Unless an atomic operation is required (I.E. the dictionary must not mutate in the event of an exception),
            /// you should use <see cref="Remove(TKey)"/> instead.
            /// </remarks>
            public void RemoveRange(IEnumerable<TKey> keys)
            {
                if (keys is null)
                    throw new ArgumentNullException(nameof(keys));

                var newDictionary = new Dictionary<TKey, TValue>(CurrentDictionaryForRead);
                foreach (var key in keys)
                    newDictionary.Remove(key);

                _currentDictionary = newDictionary;
            }

            /// <inheritdoc />
            [Pure]
            public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
                => CurrentDictionaryForRead.GetEnumerator();

            /// <summary>
            /// Gets the value for a given key if a matching key exists in the dictionary.
            /// </summary>
            /// <param name="key">The key to search for.</param>
            /// <returns>The value for the key, or the default value of type <typeparamref name="TValue"/> if no matching key was found.</returns>
            [Pure]
            public TValue GetValueOrDefault(TKey key)
                => (key == null)
                    ? default
                    : CurrentDictionaryForRead.TryGetValue(key, out var value)
                        ? value
                        : default;

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
                => (key == null)
                    ? defaultValue
                    : CurrentDictionaryForRead.TryGetValue(key, out var value)
                        ? value
                        : defaultValue;

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
            {
                var immutableHashDictionary = new ImmutableHashDictionary<TKey, TValue>(CurrentDictionaryForRead, _keyComparer, _valueComparer);

                _lastDictionary = _currentDictionary;
                _currentDictionary = null;

                return immutableHashDictionary;
            }

            /// <inheritdoc />
            [Pure]
            public bool TryGetValue(TKey key, out TValue value)
                => CurrentDictionaryForRead.TryGetValue(key, out value);

            #pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference or unconstrained type parameter.
            #endregion Public Members

            #region IDictionary<TKey, TValue>
            #pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.

            /// <inheritdoc />
            ICollection<TKey> IDictionary<TKey, TValue>.Keys
                => CurrentDictionaryForRead.Keys;

            /// <inheritdoc />
            ICollection<TValue> IDictionary<TKey, TValue>.Values
                => CurrentDictionaryForRead.Values;

            #pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
            #endregion IDictionary<TKey, TValue>

            #region IDictionary

            /// <inheritdoc />
            object IDictionary.this[object key]
            {
                get => (CurrentDictionaryForRead as IDictionary)[key];
                set => (CurrentDictionaryForWrite as IDictionary)[key] = value;
            }

            /// <inheritdoc />
            bool IDictionary.IsFixedSize
                => false;

            /// <inheritdoc />
            bool IDictionary.IsReadOnly
                => false;

            /// <inheritdoc />
            ICollection IDictionary.Keys
                => (CurrentDictionaryForRead as IDictionary).Keys;

            /// <inheritdoc />
            ICollection IDictionary.Values
                => (CurrentDictionaryForRead as IDictionary).Values;

            /// <inheritdoc />
            void IDictionary.Add(object key, object value)
                => (CurrentDictionaryForWrite as IDictionary).Add(key, value);

            /// <inheritdoc />
            void IDictionary.Clear()
                => (CurrentDictionaryForWrite as IDictionary).Clear();

            /// <inheritdoc />
            [Pure]
            bool IDictionary.Contains(object key)
                => (CurrentDictionaryForRead as IDictionary).Contains(key);

            /// <inheritdoc />
            [Pure]
            IDictionaryEnumerator IDictionary.GetEnumerator()
                => (CurrentDictionaryForRead as IDictionary).GetEnumerator();

            /// <inheritdoc />
            void IDictionary.Remove(object key)
                => (CurrentDictionaryForWrite as IDictionary).Remove(key);

            #endregion IDictionary

            #region ICollection<KeyValuePair<TKey, TValue>>

            /// <inheritdoc />
            bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
                => false;

            /// <inheritdoc />
            void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
                => (CurrentDictionaryForWrite as ICollection<KeyValuePair<TKey, TValue>>).Add(item);

            /// <inheritdoc />
            [Pure]
            void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
                => (CurrentDictionaryForWrite as ICollection<KeyValuePair<TKey, TValue>>).CopyTo(array, arrayIndex);

            /// <inheritdoc />
            bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
                => (CurrentDictionaryForWrite as ICollection<KeyValuePair<TKey, TValue>>).Remove(item);

            #endregion ICollection<KeyValuePair<TKey, TValue>>

            #region ICollection

            /// <inheritdoc />
            bool ICollection.IsSynchronized
                => (CurrentDictionaryForRead as ICollection).IsSynchronized;

            /// <inheritdoc />
            object ICollection.SyncRoot
                => (CurrentDictionaryForRead as ICollection).SyncRoot;

            /// <inheritdoc />
            [Pure]
            void ICollection.CopyTo(Array array, int index)
                => (CurrentDictionaryForRead as ICollection).CopyTo(array, index);

            #endregion ICollection

            #region IReadOnlyDictionary<TKey, TValue>

            /// <inheritdoc />
            [Pure]
            IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
                => (CurrentDictionaryForRead as IReadOnlyDictionary<TKey, TValue>).Keys;

            /// <inheritdoc />
            [Pure]
            IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
                => (CurrentDictionaryForRead as IReadOnlyDictionary<TKey, TValue>).Values;

            #endregion IReadOnlyDictionary<TKey, TValue>

            #region IEnumerable

            /// <inheritdoc />
            [Pure]
            IEnumerator IEnumerable.GetEnumerator()
                => (CurrentDictionaryForRead as IEnumerable).GetEnumerator();

            #endregion IEnumerable

            #region Internals

            internal Dictionary<TKey, TValue> CurrentDictionaryForRead
                => _currentDictionary
                    ?? _lastDictionary
                        ?? _emptyDictionary;

            internal Dictionary<TKey, TValue> CurrentDictionaryForWrite
            {
                get
                {
                    if (_currentDictionary is null)
                        _currentDictionary = (_lastDictionary is null)
                            ? new Dictionary<TKey, TValue>(_keyComparer)
                            : new Dictionary<TKey, TValue>(_lastDictionary, _keyComparer);

                    return _currentDictionary;
                }
            }

            #endregion Internals

            #region Private Fields

            private readonly IEqualityComparer<TKey> _keyComparer;

            private readonly IEqualityComparer<TValue> _valueComparer;

            private Dictionary<TKey, TValue>? _currentDictionary
                = null;
                
            private Dictionary<TKey, TValue>? _lastDictionary
                = null;

            #endregion Private Fields
        }
    }
}
