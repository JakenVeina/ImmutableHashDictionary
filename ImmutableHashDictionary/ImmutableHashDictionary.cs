using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace System.Collections.Immutable.Extra
{
    /// <summary>
    /// Implements <see cref="IImmutableDictionary{TKey, TValue}"/> with a focus on maintaining O(1) lookup-via-key operations,
    /// at the cost of extra memory allocations, to maintain immutability.
    /// </summary>
    /// <typeparam name="TKey">The type of keys within the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of values within the dictionary.</typeparam>
    public sealed partial class ImmutableHashDictionary<TKey, TValue>
        : IImmutableDictionary<TKey, TValue>,
            IReadOnlyDictionary<TKey, TValue>,
            IReadOnlyCollection<KeyValuePair<TKey, TValue>>,
            IEnumerable<KeyValuePair<TKey, TValue>>,
            IEnumerable,
            IDictionary<TKey, TValue>,
            IDictionary,
            ICollection<KeyValuePair<TKey, TValue>>,
            ICollection
    {
        #region Singleton Fields

        /// <summary>
        /// An empty <see cref="ImmutableHashDictionary{TKey, TValue}"/> object, with default equality comparison logic,
        /// which may be used to avoid memory allocations.
        /// </summary>
        public static readonly ImmutableHashDictionary<TKey, TValue> Empty;

        #endregion Singleton Fields

        #region Constructors

        static ImmutableHashDictionary()
        {
            // Need to make sure _emptyDictionary gets initialized first, otherwise the constructor for Empty will NRE.
            _emptyDictionary = new Dictionary<TKey, TValue>();

            Empty = new ImmutableHashDictionary<TKey, TValue>();
        }

        internal ImmutableHashDictionary(Dictionary<TKey, TValue>? dictionary = null, IEqualityComparer<TKey>? keyComparer = null, IEqualityComparer<TValue>? valueComparer = null)
        {
            _dictionary = dictionary ?? _emptyDictionary;
            _keyComparer = keyComparer ?? EqualityComparer<TKey>.Default;
            _valueComparer = valueComparer ?? EqualityComparer<TValue>.Default;
        }

        #endregion Constructors

        #region Public Members

        public TValue this[TKey key]
            => _dictionary[key];

        /// <inheritdoc />
        public int Count
            => _dictionary.Count;

        /// <summary>
        /// A flag, indicating whether or not this dictionary instance contains any elements.
        /// </summary>
        public bool IsEmpty
            => _dictionary.Count == 0;

        /// <summary>
        /// The <see cref="IEqualityComparer{T}"/> used to compare <typeparamref name="TKey"/> values within the dictionary.
        /// </summary>
        public IEqualityComparer<TKey> KeyComparer
            => _keyComparer;

        /// <summary>
        /// The set of <see cref="KeyValuePair{TKey, TValue}.Key"/> values of each <see cref="KeyValuePair{TKey, TValue}"/> in the dictionary.
        /// </summary>
        public IReadOnlyCollection<TKey> Keys
            => _dictionary.Keys;

        /// <summary>
        /// The <see cref="IEqualityComparer{T}"/> used to compare <typeparamref name="TValue"/> values within the dictionary.
        /// </summary>
        public IEqualityComparer<TValue> ValueComparer
            => _valueComparer;

        /// <summary>
        /// The set of <see cref="KeyValuePair{TKey, TValue}.Value"/> values of each <see cref="KeyValuePair{TKey, TValue}"/> in the dictionary.
        /// </summary>
        public IReadOnlyCollection<TValue> Values
            => _dictionary.Values;

        /// <summary>
        /// See <see cref="IImmutableDictionary{TKey, TValue}.Add(TKey, TValue)"/>.
        /// </summary>
        [Pure]
        public ImmutableHashDictionary<TKey, TValue> Add(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if(_dictionary.TryGetValue(key, out var existingValue))
                return ValueComparer.Equals(value, existingValue)
                    ? this
                    : throw new ArgumentException(nameof(key), $"An element with the same key but a different value already exists. Key: {key}");

            var newDictionary = new Dictionary<TKey, TValue>(_dictionary, _keyComparer);

            newDictionary.Add(key, value);

            return new ImmutableHashDictionary<TKey, TValue>(newDictionary, _keyComparer, _valueComparer);
        }

        /// <summary>
        /// See <see cref="IImmutableDictionary{TKey, TValue}.AddRange(IEnumerable{KeyValuePair{TKey, TValue}})"/>.
        /// </summary>
        [Pure]
        public ImmutableHashDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
        {
            if (pairs is null)
                throw new ArgumentNullException(nameof(pairs));

            var newDictionary = null as Dictionary<TKey, TValue>?;
            foreach(var pair in pairs)
            {
                if (pair.Key == null)
                    throw new ArgumentException("Cannot contain any null keys", nameof(pairs));

                if (_dictionary.TryGetValue(pair.Key, out var existingValue))
                {
                    if(!ValueComparer.Equals(pair.Value, existingValue))
                        throw new ArgumentException(nameof(pairs), $"An element with the same key but a different value already exists. Key: {pair.Key}");
                }
                else
                {
                    if (newDictionary is null)
                        newDictionary = new Dictionary<TKey, TValue>(_dictionary, _keyComparer);

                    newDictionary.Add(pair.Key, pair.Value);
                }
            }

            return (newDictionary is null)
                ? this
                : new ImmutableHashDictionary<TKey, TValue>(newDictionary, _keyComparer, _valueComparer);
        }

        /// <summary>
        /// See <see cref="IImmutableDictionary{TKey, TValue}.Clear"/>.
        /// </summary>
        [Pure]
        public ImmutableHashDictionary<TKey, TValue> Clear()
            => ReferenceEquals(_keyComparer, EqualityComparer<TKey>.Default) && ReferenceEquals(_valueComparer, EqualityComparer<TValue>.Default)
                ? Empty
                : new ImmutableHashDictionary<TKey, TValue>(_emptyDictionary, _keyComparer, _valueComparer);

        /// <inheritdoc />
        [Pure]
        public bool Contains(KeyValuePair<TKey, TValue> pair)
            => (_dictionary as ICollection<KeyValuePair<TKey, TValue>>).Contains(pair);

        /// <inheritdoc />
        [Pure]
        public bool ContainsKey(TKey key)
            => _dictionary.ContainsKey(key);

        /// <summary>
        /// Determines whether the <see cref="ImmutableHashDictionary{TKey, TValue}"/>
        /// contains an element with the specified value.
        /// </summary>
        /// <param name="value">
        /// The value to locate in the <see cref="ImmutableHashDictionary{TKey, TValue}"/>.
        /// </param>
        /// <returns>
        /// true if the <see cref="ImmutableHashDictionary{TKey, TValue}"/> contains
        /// an element with the specified value; otherwise, false.
        /// </returns>
        [Pure]
        public bool ContainsValue(TValue value)
        {
            foreach (var item in _dictionary)
                if (ValueComparer.Equals(value, item.Value))
                    return true;

            return false;
        }

        /// <inheritdoc />
        [Pure]
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
            => _dictionary.GetEnumerator();

        /// <summary>
        /// See <see cref="IImmutableDictionary{TKey, TValue}.Remove(TKey)"/>.
        /// </summary>
        [Pure]
        public ImmutableHashDictionary<TKey, TValue> Remove(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if (!_dictionary.ContainsKey(key))
                return this;

            var newDictionary = new Dictionary<TKey, TValue>(_dictionary, _keyComparer);

            newDictionary.Remove(key);

            return new ImmutableHashDictionary<TKey, TValue>(newDictionary, _keyComparer, _valueComparer);
        }

        /// <summary>
        /// See <see cref="IImmutableDictionary{TKey, TValue}.RemoveRange(IEnumerable{TKey})"/>.
        /// </summary>
        [Pure]
        public ImmutableHashDictionary<TKey, TValue> RemoveRange(IEnumerable<TKey> keys)
        {
            if (keys is null)
                throw new ArgumentNullException(nameof(keys));

            var newDictionary = null as Dictionary<TKey, TValue>?;
            foreach (var key in keys)
            {
                if (key == null)
                    throw new ArgumentException("Cannot contain any null keys", nameof(keys));

                if (_dictionary.ContainsKey(key))
                {
                    if (newDictionary is null)
                        newDictionary = new Dictionary<TKey, TValue>(_dictionary, _keyComparer);

                    newDictionary.Remove(key);
                }
            }

            return (newDictionary is null)
                ? this
                : new ImmutableHashDictionary<TKey, TValue>(newDictionary, _keyComparer, _valueComparer);
        }

        /// <summary>
        /// See <see cref="IImmutableDictionary{TKey, TValue}.SetItem(TKey, TValue)"/>.
        /// </summary>
        [Pure]
        public ImmutableHashDictionary<TKey, TValue> SetItem(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if (_dictionary.TryGetValue(key, out var existingValue) && ValueComparer.Equals(value, existingValue))
                return this;

            var newDictionary = new Dictionary<TKey, TValue>(_dictionary, _keyComparer);

            newDictionary[key] = value;

            return new ImmutableHashDictionary<TKey, TValue>(newDictionary, _keyComparer, _valueComparer);
        }

        /// <summary>
        /// See <see cref="IImmutableDictionary{TKey, TValue}.SetItems(IEnumerable{KeyValuePair{TKey, TValue}})"/>.
        /// </summary>
        [Pure]
        public ImmutableHashDictionary<TKey, TValue> SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            if (items is null)
                throw new ArgumentNullException(nameof(items));

            var newDictionary = null as Dictionary<TKey, TValue>?;
            foreach (var item in items)
            {
                if (item.Key == null)
                    throw new ArgumentException("Cannot contain any null keys", nameof(items));

                if (!_dictionary.TryGetValue(item.Key, out var existingValue) || !ValueComparer.Equals(item.Value, existingValue))
                {
                    if (newDictionary is null)
                        newDictionary = new Dictionary<TKey, TValue>(_dictionary, _keyComparer);

                    newDictionary[item.Key] = item.Value;
                }
            }

            return (newDictionary is null)
                ? this
                : new ImmutableHashDictionary<TKey, TValue>(newDictionary, _keyComparer, _valueComparer);
        }

        /// <summary>
        /// Creates a collection with the same contents as this collection that
        /// can be efficiently mutated across multiple operations using standard
        /// mutable interfaces.
        /// </summary>
        [Pure]
        public Builder ToBuilder()
            => new Builder(_dictionary, _keyComparer, _valueComparer);

        /// <inheritdoc />
        /// <remarks>
        /// This is an O(N) operation, so long as the underlying dictionary implementation here is <see cref="Dictionary{TKey, TValue}"/>,
        /// as it offers no way to lookup a key or <see cref="KeyValuePair{TKey, TValue}"/>, by hash code.
        /// </remarks>
        [Pure]
        public bool TryGetKey(TKey equalKey, out TKey actualKey)
        {
            if (equalKey != null)
            {
                var hashCode = equalKey.GetHashCode();

                foreach (var pair in _dictionary)
                    if (pair.Key!.GetHashCode() == hashCode)
                    {
                        actualKey = pair.Key;
                        return true;
                    }
            }

            #pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference or unconstrained type parameter.
            actualKey = default;
            #pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference or unconstrained type parameter.
            return false;
        }

        /// <inheritdoc />
        [Pure]
        public bool TryGetValue(TKey key, out TValue value)
            => _dictionary.TryGetValue(key, out value);

        /// <summary>
        /// Constructs a new <see cref="ImmutableHashDictionary{TKey, TValue}"/> with the items from this dictionary,
        /// but with the given <see cref="KeyComparer"/>.
        /// </summary>
        /// <param name="keyComparer">The value to use for <see cref="KeyComparer"/> for the new dictionary.</param>
        /// <returns>A new dictionary containing the same entries as this one, but built from <paramref name="keyComparer"/>.</returns>
        /// <remarks>
        /// Note that this requires a complete duplication of the internal hash table, as its structure depends upon <see cref="KeyComparer"/>.
        /// </remarks>
        [Pure]
        public ImmutableHashDictionary<TKey, TValue> WithComparers(IEqualityComparer<TKey> keyComparer)
            => new ImmutableHashDictionary<TKey, TValue>(new Dictionary<TKey, TValue>(_dictionary, keyComparer), keyComparer, _valueComparer);

        /// <summary>
        /// Constructs a new <see cref="ImmutableHashDictionary{TKey, TValue}"/> with the items from this dictionary,
        /// but with the given <see cref="ValueComparer"/>.
        /// </summary>
        /// <param name="valueComparer">The value to use for <see cref="valueComparer"/> for the new dictionary.</param>
        /// <returns>A new dictionary containing the same entries as this one, but built from <paramref name="valueComparer"/>.</returns>
        /// <remarks>
        /// Note that the new dictionary will reuse this dictionary's internal hash table, as its implementation does not depend upon <see cref="ValueComparer"/>.
        /// </remarks>
        [Pure]
        public ImmutableHashDictionary<TKey, TValue> WithComparers(IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
            => new ImmutableHashDictionary<TKey, TValue>(_dictionary, keyComparer, valueComparer);

        #endregion ImmutableHashDictionary

        #region IImmutableDictionary
        #pragma warning disable CS8616 // Nullability of reference types in return type doesn't match implemented member.
        #pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.

        /// <inheritdoc />
        [Pure]
        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Add(TKey key, TValue value)
            => Add(key, value);

        /// <inheritdoc />
        [Pure]
        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
            => AddRange(pairs);

        /// <inheritdoc />
        [Pure]
        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Clear()
            => Clear();

        /// <inheritdoc />
        [Pure]
        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Remove(TKey key)
            => Remove(key);

        /// <inheritdoc />
        [Pure]
        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.RemoveRange(IEnumerable<TKey> keys)
            => RemoveRange(keys);

        /// <inheritdoc />
        [Pure]
        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItem(TKey key, TValue value)
            => SetItem(key, value);

        /// <inheritdoc />
        [Pure]
        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items)
            => SetItems(items);

        #pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        #pragma warning restore CS8616 // Nullability of reference types in return type doesn't match implemented member.
        #endregion IImmutableDictionary

        #region IReadOnlyDictionary
        #pragma warning disable CS8616 // Nullability of reference types in return type doesn't match implemented member.
        #pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.

        /// <inheritdoc />
        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
            => _dictionary.Keys;

        /// <inheritdoc />
        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
            => _dictionary.Values;

        #pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        #pragma warning restore CS8616 // Nullability of reference types in return type doesn't match implemented member.
        #endregion IReadOnlyDictionary

        #region IEnumerable

        /// <inheritdoc />
        [Pure]
        IEnumerator IEnumerable.GetEnumerator()
            => (_dictionary as IEnumerable).GetEnumerator();

        #endregion IEnumerable

        #region IDictionary

        /// <inheritdoc />
        TValue IDictionary<TKey, TValue>.this[TKey key]
        {
            get => this[key];
            set => throw new NotSupportedException();
        }

        /// <inheritdoc />
        ICollection<TKey> IDictionary<TKey, TValue>.Keys
            => (_dictionary as IDictionary<TKey, TValue>).Keys;

        /// <inheritdoc />
        ICollection<TValue> IDictionary<TKey, TValue>.Values
            => (_dictionary as IDictionary<TKey, TValue>).Values;

        /// <inheritdoc />
        void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
            => throw new NotSupportedException();

        /// <inheritdoc />
        bool IDictionary<TKey, TValue>.Remove(TKey key)
            => throw new NotSupportedException();

        /// <inheritdoc />
        object IDictionary.this[object key]
        {
            get => (_dictionary as IDictionary)[key];
            set => throw new NotSupportedException();
        }
            
        /// <inheritdoc />
        bool IDictionary.IsFixedSize
            => true;

        /// <inheritdoc />
        bool IDictionary.IsReadOnly
            => true;

        /// <inheritdoc />
        ICollection IDictionary.Keys
            => (_dictionary as IDictionary).Keys;

        /// <inheritdoc />
        ICollection IDictionary.Values
            => (_dictionary as IDictionary).Values;

        /// <inheritdoc />
        [Pure]
        void IDictionary.Add(object key, object value)
            => throw new NotSupportedException();

        /// <inheritdoc />
        [Pure]
        void IDictionary.Clear()
            => throw new NotSupportedException();

        /// <inheritdoc />
        [Pure]
        bool IDictionary.Contains(object key)
            => (_dictionary as IDictionary).Contains(key);

        /// <inheritdoc />
        [Pure]
        IDictionaryEnumerator IDictionary.GetEnumerator()
            => (_dictionary as IDictionary).GetEnumerator();

        /// <inheritdoc />
        [Pure]
        void IDictionary.Remove(object key)
            => throw new NotSupportedException();

        #endregion IDictionary

        #region ICollection

        /// <inheritdoc />
        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
            => true;

        /// <inheritdoc />
        [Pure]
        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
            => throw new NotSupportedException();

        /// <inheritdoc />
        [Pure]
        void ICollection<KeyValuePair<TKey, TValue>>.Clear()
            => throw new NotSupportedException();

        /// <inheritdoc />
        [Pure]
        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
            => (_dictionary as ICollection<KeyValuePair<TKey, TValue>>).CopyTo(array, arrayIndex);

        /// <inheritdoc />
        [Pure]
        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
            => throw new NotSupportedException();

        /// <inheritdoc />
        bool ICollection.IsSynchronized
            => true;

        /// <inheritdoc />
        object ICollection.SyncRoot
            => this;

        /// <inheritdoc />
        [Pure]
        void ICollection.CopyTo(Array array, int index)
            => (_dictionary as ICollection).CopyTo(array, index);

        #endregion ICollection

        #region Private Fields

        private readonly Dictionary<TKey, TValue> _dictionary;

        private readonly IEqualityComparer<TKey> _keyComparer;

        private readonly IEqualityComparer<TValue> _valueComparer;

        // Singleton empty dictionary, to help avoid unnecessary memory allocations.
        private static readonly Dictionary<TKey, TValue> _emptyDictionary;

        #endregion Private Fields
    }
}
