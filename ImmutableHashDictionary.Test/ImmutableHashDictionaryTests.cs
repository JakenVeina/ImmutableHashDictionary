using NUnit.Framework;

namespace ImmutableHashDictionary.Test
{
    [TestFixture]
    public class ImmutableHashDictionaryTests
    {
        #region Empty Tests

        [Test]
        public void Empty_Always_IsEmpty()
            => throw new IgnoreException("Not yet implemented");

        #endregion Empty Tests

        #region this[] Tests

        [Test]
        public void This_Get_KeyExists_ReturnsValue()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void This_Get_KeyDoesNotExist_ThrowsException()
            => throw new IgnoreException("Not yet implemented");

        #endregion this[] Tests

        #region Count Tests

        [Test]
        public void Count_Always_ReturnsDictionaryCount()
            => throw new IgnoreException("Not yet implemented");

        #endregion Count Tests

        #region IsEmpty Tests

        [Test]
        public void IsEmpty_DictionaryIsEmpty_ReturnsTrue()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void IsEmpty_DictionaryIsNotEmpty_ReturnsFalse()
            => throw new IgnoreException("Not yet implemented");

        #endregion IsEmpty Tests

        #region Keys Tests

        [Test]
        public void Keys_Always_ReturnsDictionaryKeys()
            => throw new IgnoreException("Not yet implemented");

        #endregion Keys Tests

        #region Values Tests

        [Test]
        public void Values_Always_ReturnsDictionaryValues()
            => throw new IgnoreException("Not yet implemented");

        #endregion Values Tests

        #region Add() Tests

        [Test]
        public void Add_KeyExistsWithMatchingValue_ReturnsSelf()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void Add_KeyExistsWithDifferentValue_ThrowsException()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void Add_KeyDoesNotExist_AddsKeyAndValueToResult()
            => throw new IgnoreException("Not yet implemented");

        #endregion Add() Tests

        #region AddRange() Tests

        [Test]
        public void AddRange_AllKeysExistWithMatchingValue_ReturnsSelf()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void AddRange_AnyKeysExistWithDifferentValue_ThrowsException()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void AddRange_Otherwise_AddsNewKeysAndValuesToResult()
            => throw new IgnoreException("Not yet implemented");

        #endregion AddRange() Tests

        #region Clear() Tests

        [Test]
        public void Clear_Always_ResultIsEmpty()
            => throw new IgnoreException("Not yet implemented");

        #endregion Clear() Tests

        #region Contains() Tests

        [Test]
        public void Contains_KeyValuePairExists_ReturnsTrue()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void Contains_KeyValuePairDoesNotExist_ReturnsFalse()
            => throw new IgnoreException("Not yet implemented");

        #endregion Contains() Tests

        #region ContainsKey() Tests

        [Test]
        public void ContainsKey_KeyExists_ReturnsTrue()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void ContainsKey_KeyDoesNotExist_ReturnsFalse()
            => throw new IgnoreException("Not yet implemented");

        #endregion ContainsKey() Tests

        #region ContainsValue() Tests

        [Test]
        public void ContainsValue_ValueExists_ReturnsTrue()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void ContainsValue_ValueDoesNotExist_ReturnsFalse()
            => throw new IgnoreException("Not yet implemented");

        #endregion ContainsValue() Tests

        #region GetEnumerator() Tests

        [Test]
        public void GetEnumerator_Always_EnumeratesDictionary()
            => throw new IgnoreException("Not yet implemented");

        #endregion GetEnumerator() Tests

        #region Remove() Tests

        [Test]
        public void Remove_KeyDoesNotExist_ReturnsSelf()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void Remove_KeyExists_RemovesKeyFromResult()
            => throw new IgnoreException("Not yet implemented");

        #endregion Remove() Tests

        #region RemoveRange() Tests

        [Test]
        public void RemoveRange_AllKeysDoNotExist_ReturnsSelf()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void RemoveRange_AnyKeysExist_RemovesExistingKeysFromResult()
            => throw new IgnoreException("Not yet implemented");

        #endregion RemoveRange() Tests

        #region SetItem() Tests

        [Test]
        public void SetItem_KeyExistsWithMatchingValue_ReturnsSelf()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void SetItem_KeyExistsWithDifferentValue_ReplacesValueInResult()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void SetItem_KeyDoesNotExist_AddsKeyAndValueToResult()
            => throw new IgnoreException("Not yet implemented");

        #endregion SetItem() Tests

        #region SetItems() Tests

        [Test]
        public void SetItems_AllKeysExistWithMatchingValue_ReturnsSelf()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void SetItems_Otherwise_ReplacesExistingValuesAndAddsNewKeysAndValuesToResult()
            => throw new IgnoreException("Not yet implemented");

        #endregion SetItems() Tests

        #region ToBuilder() Tests

        [Test]
        public void ToBuilder_Always_ResultMatchesSelf()
            => throw new IgnoreException("Not yet implemented");

        #endregion ToBuilder() Tests

        #region TryGetKey() Tests

        [Test]
        public void TryGetKey_MatchingKeyDoesNotExist_ReturnsFalse()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void TryGetKey_MatchingKeyExists_ReturnsTrueAndMatchingKey()
            => throw new IgnoreException("Not yet implemented");

        #endregion TryGetKey() Tests

        #region TryGetValue() Tests

        [Test]
        public void TryGetValue_KeyDoesNotExist_ReturnsFalse()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void TryGetValue_KeyExists_ReturnsTrueAndMatchingValue()
            => throw new IgnoreException("Not yet implemented");

        #endregion TryGetValue() Tests

        #region WithComparers() Tests

        [Test]
        public void WithComparers_KeyComparerIsGiven_ResultKeyComparerMatchesSelf()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void WithComparers_BothComparerAreGiven_ResultComparersMatchSelf()
            => throw new IgnoreException("Not yet implemented");

        #endregion WithComparers() Tests

        #region IImmutableDictionary.Add() Tests

        [Test]
        public void IImmutableDictionary_Add_KeyExistsWithMatchingValue_ReturnsSelf()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void IImmutableDictionary_Add_KeyExistsWithDifferentValue_ThrowsException()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void IImmutableDictionary_Add_KeyDoesNotExist_AddsKeyAndValueToResult()
            => throw new IgnoreException("Not yet implemented");

        #endregion IImmutableDictionary.Add() Tests

        #region IImmutableDictionary.AddRange() Tests

        [Test]
        public void IImmutableDictionary_AddRange_AllKeysExistWithMatchingValue_ReturnsSelf()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void IImmutableDictionary_AddRange_AnyKeysExistWithDifferentValue_ThrowsException()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void IImmutableDictionary_AddRange_Otherwise_AddsNewKeysAndValuesToResult()
            => throw new IgnoreException("Not yet implemented");

        #endregion IImmutableDictionary.AddRange() Tests

        #region IImmutableDictionary.Clear() Tests

        [Test]
        public void IImmutableDictionary_Clear_Always_ResultIsEmpty()
            => throw new IgnoreException("Not yet implemented");

        #endregion IImmutableDictionary.Clear() Tests

        #region IImmutableDictionary.Remove() Tests

        [Test]
        public void IImmutableDictionary_Remove_KeyDoesNotExist_ReturnsSelf()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void IImmutableDictionary_Remove_KeyExists_RemovesKeyFromResult()
            => throw new IgnoreException("Not yet implemented");

        #endregion IImmutableDictionary.Remove() Tests

        #region IImmutableDictionary.RemoveRange() Tests

        [Test]
        public void IImmutableDictionary_RemoveRange_AllKeysDoNotExist_ReturnsSelf()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void IImmutableDictionary_RemoveRange_AnyKeysExist_RemovesExistingKeysFromResult()
            => throw new IgnoreException("Not yet implemented");

        #endregion IImmutableDictionary.RemoveRange() Tests

        #region IImmutableDictionary.SetItem() Tests

        [Test]
        public void IImmutableDictionary_SetItem_KeyExistsWithMatchingValue_ReturnsSelf()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void IImmutableDictionary_SetItem_KeyExistsWithDifferentValue_ReplacesValueInResult()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void IImmutableDictionary_SetItem_KeyDoesNotExist_AddsKeyAndValueToResult()
            => throw new IgnoreException("Not yet implemented");

        #endregion IImmutableDictionary.SetItem() Tests

        #region IImmutableDictionary.SetItems() Tests

        [Test]
        public void IImmutableDictionary_SetItems_AllKeysExistWithMatchingValue_ReturnsSelf()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void IImmutableDictionary_SetItems_Otherwise_ReplacesExistingValuesAndAddsNewKeysAndValuesToResult()
            => throw new IgnoreException("Not yet implemented");

        #endregion IImmutableDictionary.SetItems() Tests

        #region IReadOnlyDictionary.Keys Tests

        [Test]
        public void IReadOnlyDictionary_Keys_Always_ReturnsDictionaryKeys()
            => throw new IgnoreException("Not yet implemented");

        #endregion IReadOnlyDictionary.Keys Tests

        #region IReadOnlyDictionary.Values Tests

        [Test]
        public void IReadOnlyDictionary_Values_Always_ReturnsDictionaryValues()
            => throw new IgnoreException("Not yet implemented");

        #endregion IReadOnlyDictionary.Values Tests

        #region IEnumerable.GetEnumerator() Tests

        [Test]
        public void IEnumerable_GetEnumerator_Always_EnumeratesDictionary()
            => throw new IgnoreException("Not yet implemented");

        #endregion IEnumerable.GetEnumerator() Tests

        #region IDictionary<TKey, TValue>.this[] Tests

        [Test]
        public void IDictionary_Generic_This_Get_KeyExists_ReturnsValue()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void IDictionary_Generic_This_Get_KeyDoesNotExist_ThrowsException()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void IDictionary_Generic_This_Set_Always_ThrowsNotSupported()
            => throw new IgnoreException("Not yet implemented");

        #endregion IDictionary<TKey, TValue>.this[] Tests

        #region IDictionary<TKey, TValue>.Keys Tests

        [Test]
        public void IDictionary_Generic_Keys_Always_ReturnsDictionaryKeys()
            => throw new IgnoreException("Not yet implemented");

        #endregion IDictionary<TKey, TValue>.Keys Tests

        #region IDictionary<TKey, TValue>.Values Tests

        [Test]
        public void IDictionary_Generic_Values_Always_ReturnsDictionaryValues()
            => throw new IgnoreException("Not yet implemented");

        #endregion IDictionary<TKey, TValue>.Values Tests

        #region IDictionary<TKey, TValue>.Add() Tests

        [Test]
        public void IDictionary_Generic_Add_Always_ThrowsNotSupported()
            => throw new IgnoreException("Not yet implemented");

        #endregion IDictionary<TKey, TValue>.Add() Tests

        #region IDictionary<TKey, TValue>.Remove() Tests

        [Test]
        public void IDictionary_Generic_Remove_Always_ThrowsNotSupported()
            => throw new IgnoreException("Not yet implemented");

        #endregion IDictionary<TKey, TValue>.Remove() Tests

        #region IDictionary.this[] Tests

        [Test]
        public void IDictionary_Legacy_This_Get_KeyExists_ReturnsValue()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void IDictionary_Legacy_This_Get_KeyDoesNotExist_ThrowsException()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void IDictionary_Legacy_This_Set_Always_ThrowsNotSupported()
            => throw new IgnoreException("Not yet implemented");

        #endregion IDictionary.this[] Tests

        #region IDictionary.IsFixedSize Tests

        [Test]
        public void IDictionary_Legacy_IsFixedSize_Always_ReturnsTrue()
            => throw new IgnoreException("Not yet implemented");

        #endregion IDictionary.IsFixedSize Tests

        #region IDictionary.IsReadOnly Tests

        [Test]
        public void IDictionary_Legacy_IsReadOnly_Always_ReturnsTrue()
            => throw new IgnoreException("Not yet implemented");

        #endregion IDictionary.IsReadOnly Tests

        #region IDictionary.Keys Tests

        [Test]
        public void IDictionary_Legacy_Keys_Always_ReturnsDictionaryKeys()
            => throw new IgnoreException("Not yet implemented");

        #endregion IDictionary.Keys Tests

        #region IDictionary.Values Tests

        [Test]
        public void IDictionary_Legacy_Values_Always_ReturnsDictionaryValues()
            => throw new IgnoreException("Not yet implemented");

        #endregion IDictionary.Values Tests

        #region IDictionary.Add() Tests

        [Test]
        public void IDictionary_Legacy_Add_Always_ThrowsNotSupported()
            => throw new IgnoreException("Not yet implemented");

        #endregion IDictionary.Add() Tests

        #region IDictionary.Clear() Tests

        [Test]
        public void IDictionary_Legacy_Clear_Always_ThrowsNotSupported()
            => throw new IgnoreException("Not yet implemented");

        #endregion IDictionary.Clear() Tests

        #region IDictionary.Contains() Tests

        [Test]
        public void IDictionary_Legacy_Contains_KeyExists_ReturnsTrue()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void IDictionary_Legacy_Contains_KeyDoesNotExist_ReturnsFalse()
            => throw new IgnoreException("Not yet implemented");

        #endregion IDictionary.Contains() Tests

        #region IDictionary.GetEnumerator() Tests

        [Test]
        public void IDictionary_Legacy_GetEnumerator_Always_EnumeratesDictionary()
            => throw new IgnoreException("Not yet implemented");

        #endregion IDictionary.GetEnumerator() Tests

        #region IDictionary.Remove() Tests

        [Test]
        public void IDictionary_Legacy_Remove_Always_ThrowsNotSupported()
            => throw new IgnoreException("Not yet implemented");

        #endregion IDictionary.Remove() Tests

        #region ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly Tests

        [Test]
        public void ICollection_Generic_IsReadOnly_Always_ReturnsTrue()
            => throw new IgnoreException("Not yet implemented");

        #endregion ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly Tests

        #region ICollection<KeyValuePair<TKey, TValue>>.Add() Tests

        [Test]
        public void ICollection_Generic_Add_Always_ThrowsNotSupported()
            => throw new IgnoreException("Not yet implemented");

        #endregion ICollection<KeyValuePair<TKey, TValue>>.Add() Tests

        #region ICollection<KeyValuePair<TKey, TValue>>.Clear() Tests

        [Test]
        public void ICollection_Generic_Clear_Always_ThrowsNotSupported()
            => throw new IgnoreException("Not yet implemented");

        #endregion ICollection<KeyValuePair<TKey, TValue>>.Clear() Tests

        #region ICollection<KeyValuePair<TKey, TValue>>.Contains() Tests

        [Test]
        public void ICollection_Generic_Contains_KeyValuePairExists_ReturnsTrue()
            => throw new IgnoreException("Not yet implemented");

        [Test]
        public void ICollection_Generic_Contains_KeyValuePairDoesNotExist_ReturnsFalse()
            => throw new IgnoreException("Not yet implemented");

        #endregion ICollection<KeyValuePair<TKey, TValue>>.Contains() Tests

        #region ICollection<KeyValuePair<TKey, TValue>>.CopyTo() Tests

        [Test]
        public void ICollection_Generic_CopyTo_Always_ResultMatchesSelf()
            => throw new IgnoreException("Not yet implemented");

        #endregion ICollection<KeyValuePair<TKey, TValue>>.CopyTo() Tests

        #region ICollection<KeyValuePair<TKey, TValue>>.Remove() Tests

        [Test]
        public void ICollection_Generic_Remove_Always_ThrowsNotSupported()
            => throw new IgnoreException("Not yet implemented");

        #endregion ICollection<KeyValuePair<TKey, TValue>>.Remove() Tests

        #region ICollection.IsSynchronized Tests

        [Test]
        public void ICollection_Legacy_IsSynchronized_ReturnsTrue()
            => throw new IgnoreException("Not yet implemented");

        #endregion ICollection.IsSynchronized Tests

        #region ICollection.SyncRoot Tests

        [Test]
        public void ICollection_Legacy_IsSynchronized_ReturnsSelf()
            => throw new IgnoreException("Not yet implemented");

        #endregion ICollection.SyncRoot Tests

        #region ICollection.CopyTo() Tests

        [Test]
        public void ICollection_Legacy_CopyTo_Always_ResultMatchesSelf()
            => throw new IgnoreException("Not yet implemented");

        #endregion ICollection.CopyTo() Tests
    }
}
