using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using Shouldly;

namespace System.Collections.Immutable.Extra.Test
{
    public partial class ImmutableHashDictionaryTests
    {
		[TestFixture]
		public class Builder
        {
            #region Test Context

            public static (IEqualityComparer<int> keyComparer, IEqualityComparer<int> valueComparer, ImmutableHashDictionary<int, int>.Builder uut) BuildTestContext(Dictionary<int, int> dictionary)
            {
                var keyComparer = BuildFakeEqualityComparer<int>();
                var valueComparer = BuildFakeEqualityComparer<int>();
                var uut = new ImmutableHashDictionary<int, int>.Builder(new Dictionary<int, int>(dictionary), keyComparer, valueComparer);

                return (keyComparer, valueComparer, uut);
            }

            #endregion Test Context

            #region Constructor Tests

            [Test]
            public void Constructor_DictionaryIsNull_ResultIsEmpty()
            {
                var keyComparer = BuildFakeEqualityComparer<int>();
                var valueComparer = BuildFakeEqualityComparer<int>();

                var result = new ImmutableHashDictionary<int, int>.Builder(null, keyComparer, valueComparer);

                result.ShouldBeEmpty();
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(DictionaryTestCaseData))]
            public void Constructor_DictionaryIsNotNull_ResultMatchesDictionary(Dictionary<int, int> dictionary)
            {
                var keyComparer = BuildFakeEqualityComparer<int>();
                var valueComparer = BuildFakeEqualityComparer<int>();

                var result = new ImmutableHashDictionary<int, int>.Builder(dictionary, keyComparer, valueComparer);

                result.ShouldBe(dictionary, ignoreOrder: true);
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(DictionaryTestCaseData))]
            public void Constructor_KeyComparerAndValueComparerAreNull_ResultKeyComparerAndValueComparerAreDefault(Dictionary<int, int> dictionary)
            {
                var result = new ImmutableHashDictionary<int, int>.Builder(dictionary, null, null);

                result.KeyComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
                result.ValueComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(DictionaryTestCaseData))]
            public void Constructor_KeyComparerAndValueComparerAreNotNull_ResultKeyComparerAndValueComparerAreGiven(Dictionary<int, int> dictionary)
            {
                var keyComparer = BuildFakeEqualityComparer<int>();
                var valueComparer = BuildFakeEqualityComparer<int>();

                var result = new ImmutableHashDictionary<int, int>.Builder(dictionary, keyComparer, valueComparer);

                result.KeyComparer.ShouldBeSameAs(keyComparer);
                result.ValueComparer.ShouldBeSameAs(valueComparer);
            }

            #endregion Constructor Tests

            #region value = this[] Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(ValidKeyWithMatchingValueTestCaseData))]
            public void This_Get_KeyExists_ReturnsValue(Dictionary<int, int> dictionary, int key, int value)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                uut[key].ShouldBe(value);
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(InvalidKeyTestCaseData))]
            public void This_Get_KeyDoesNotExist_ThrowsException(Dictionary<int, int> dictionary, int key)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                Should.Throw<KeyNotFoundException>(() =>
                {
                    var value = uut[key];
                });
            }

            #endregion value = this[] Tests
            
			#region this[] = value Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(ValidKeyWithMatchingValueTestCaseData))]
            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(ValidKeyWithDifferentValueTestCaseData))]
            public void This_Set_KeyExists_ReplacesValueInResult(Dictionary<int, int> dictionary, int key, int value)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                uut[key] = value;

                uut.ShouldBe(
                    dictionary
                        .Where(x => x.Key != key)
                        .Append(new KeyValuePair<int, int>(key, value)),
                    ignoreOrder: true);
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(InvalidKeyWithValueTestCaseData))]
            public void This_Set_KeyDoesNotExist_AddsKeyAndValueToResult(Dictionary<int, int> dictionary, int key, int value)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                uut[key] = value;

                uut.ShouldBe(
                    dictionary
                        .Append(new KeyValuePair<int, int>(key, value)),
                    ignoreOrder: true);
            }

            #endregion this[] = value Tests

            #region Count Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(DictionaryTestCaseData))]
            public void Count_Always_ReturnsDictionaryCount(Dictionary<int, int> dictionary)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                uut.Count.ShouldBe(dictionary.Count);
            }

            #endregion Count Tests

            #region Keys Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(DictionaryTestCaseData))]
            public void Keys_Always_ReturnsDictionaryKeys(Dictionary<int, int> dictionary)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                uut.Keys.ShouldBe(dictionary.Keys, ignoreOrder: true);
            }

            #endregion Keys Tests

            #region Values Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(DictionaryTestCaseData))]
            public void Values_Always_ReturnsDictionaryValues(Dictionary<int, int> dictionary)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                uut.Values.ShouldBe(dictionary.Values, ignoreOrder: true);
            }

            #endregion Values Tests

            #region Add() Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(ValidKeyWithMatchingValueTestCaseData))]
            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(ValidKeyWithDifferentValueTestCaseData))]
            public void Add_KeyExists_ThrowsException(Dictionary<int, int> dictionary, int key, int value)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                Should.Throw<ArgumentException>(() =>
                {
                    uut.Add(key, value);
                });

                uut.ShouldBe(dictionary, ignoreOrder: true);
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(InvalidKeyWithValueTestCaseData))]
            public void Add_KeyDoesNotExist_AddsKeyAndValueToResult(Dictionary<int, int> dictionary, int key, int value)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                uut.Add(key, value);

                uut.ShouldBe(
					dictionary
						.Append(new KeyValuePair<int, int>(key, value)),
					ignoreOrder: true);
            }

            #endregion Add() Tests

            #region AddRange() Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(DictionaryTestCaseData))]
            public void AddRange_KeyValuePairsIsNull_ThrowsException(Dictionary<int, int> dictionary)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                Should.Throw<ArgumentNullException>(() =>
                {
                    uut.AddRange(null);
                });

                uut.ShouldBe(dictionary, ignoreOrder: true);
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(EmptyKeyValuePairsTestCaseData))]
            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(AllKeyValuePairsExistTestCaseData))]
            public void AddRange_AllKeysExistWithMatchingValue_DoesNotModifySelf(Dictionary<int, int> dictionary, IEnumerable<KeyValuePair<int, int>> keyValuePairs)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                uut.AddRange(keyValuePairs);

                uut.ShouldBe(dictionary, ignoreOrder: true);
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(AllKeyValuePairsInvalidTestCaseData))]
            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(SomeKeyValuePairsInvalidTestCaseData))]
            public void AddRange_AnyKeysExistWithDifferentValue_ThrowsException(Dictionary<int, int> dictionary, IEnumerable<KeyValuePair<int, int>> keyValuePairs)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                Should.Throw<ArgumentException>(() =>
                {
                    uut.AddRange(keyValuePairs);
                });

                uut.ShouldBe(dictionary, ignoreOrder: true);
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(AllKeyValuePairsDoNotExistTestCaseData))]
            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(SomeKeyValuePairsDoNotExistTestCaseData))]
            public void AddRange_Otherwise_AddsNewKeysAndValues(Dictionary<int, int> dictionary, IEnumerable<KeyValuePair<int, int>> keyValuePairs)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                uut.AddRange(keyValuePairs);
				
				uut.ShouldBe(
                    dictionary.Concat(keyValuePairs
                        .Where(x => !dictionary.ContainsKey(x.Key))),
                    ignoreOrder: true);
            }

            #endregion AddRange() Tests

            #region Clear() Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(DictionaryTestCaseData))]
            public void Clear_Always_ClearsSelf(Dictionary<int, int> dictionary)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                uut.Clear();

                uut.ShouldBeEmpty();
            }

            #endregion Clear() Tests

            #region Contains() Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(ValidKeyWithMatchingValueTestCaseData))]
            public void Contains_KeyValuePairExists_ReturnsTrue(Dictionary<int, int> dictionary, int key, int value)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                uut.Contains(new KeyValuePair<int, int>(key, value)).ShouldBeTrue();
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(ValidKeyWithDifferentValueTestCaseData))]
            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(InvalidKeyWithValueTestCaseData))]
            public void Contains_KeyValuePairDoesNotExist_ReturnsFalse(Dictionary<int, int> dictionary, int key, int value)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                uut.Contains(new KeyValuePair<int, int>(key, value)).ShouldBeFalse();
            }

            #endregion Contains() Tests

            #region ContainsKey() Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(ValidKeyTestCaseData))]
            public void ContainsKey_KeyExists_ReturnsTrue(Dictionary<int, int> dictionary, int key)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                uut.ContainsKey(key).ShouldBeTrue();
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(InvalidKeyTestCaseData))]
            public void ContainsKey_KeyDoesNotExist_ReturnsFalse(Dictionary<int, int> dictionary, int key)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                uut.ContainsKey(key).ShouldBeFalse();
            }

            #endregion ContainsKey() Tests

            #region ContainsValue() Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(ValidValueTestCaseData))]
            public void ContainsValue_ValueExists_ReturnsTrue(Dictionary<int, int> dictionary, int value)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                uut.ContainsValue(value).ShouldBeTrue();
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(InvalidValueTestCaseData))]
            public void ContainsValue_ValueDoesNotExist_ReturnsFalse(Dictionary<int, int> dictionary, int value)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                uut.ContainsValue(value).ShouldBeFalse();
            }

            #endregion ContainsValue() Tests

            #region GetEnumerator() Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(DictionaryTestCaseData))]
            public void GetEnumerator_Always_EnumeratesDictionary(Dictionary<int, int> dictionary)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                uut.AsEnumerable().ShouldBe(dictionary, ignoreOrder: true);
            }

            #endregion GetEnumerator() Tests

            #region Remove() Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(InvalidKeyTestCaseData))]
            public void Remove_KeyDoesNotExist_DoesNotModifySelfAndReturnsFalse(Dictionary<int, int> dictionary, int key)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                uut.Remove(key).ShouldBeFalse();

                uut.ShouldBe(dictionary, ignoreOrder: true);
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(ValidKeyTestCaseData))]
            public void Remove_KeyExists_RemovesKeyAndReturnsTrue(Dictionary<int, int> dictionary, int key)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                uut.Remove(key).ShouldBeTrue();
					
				uut.ShouldBe(
					dictionary
						.Where(x => x.Key != key),
					ignoreOrder: true);
            }

            #endregion Remove() Tests

            #region RemoveRange() Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(DictionaryTestCaseData))]
            public void RemoveRange_KeysIsNull_ThrowsException(Dictionary<int, int> dictionary)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                Should.Throw<ArgumentNullException>(() =>
                {
                    uut.RemoveRange(null!);
                });

                uut.ShouldBe(dictionary, ignoreOrder: true);
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(EmptyKeysTestCaseData))]
            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(AllKeysDoNotExistTestCaseData))]
            public void RemoveRange_AllKeysDoNotExist_DoesNotModifySelf(Dictionary<int, int> dictionary, IEnumerable<int> keys)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                uut.RemoveRange(keys);

                uut.ShouldBe(dictionary, ignoreOrder: true);
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(AllKeysExistTestCaseData))]
            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(SomeKeysDoNotExistTestCaseData))]
            public void RemoveRange_AnyKeysExist_RemovesExistingKeys(Dictionary<int, int> dictionary, IEnumerable<int> keys)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                uut.RemoveRange(keys);
				
				uut.ShouldBe(
                    dictionary
						.Where(x => !keys.Contains(x.Key)),
                    ignoreOrder: true);
            }

            #endregion RemoveRange() Tests

            #region TryGetValue() Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(InvalidKeyTestCaseData))]
            public void TryGetValue_KeyDoesNotExist_ReturnsFalse(Dictionary<int, int> dictionary, int key)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                uut.TryGetValue(key, out var value).ShouldBeFalse();
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(ValidKeyWithMatchingValueTestCaseData))]
            public void TryGetValue_KeyExists_ReturnsTrueAndMatchingValue(Dictionary<int, int> dictionary, int key, int expectedValue)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                uut.TryGetValue(key, out var value).ShouldBeTrue();

                value.ShouldBe(expectedValue);
            }

            #endregion TryGetValue() Tests

            #region IReadOnlyDictionary.Keys Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(DictionaryTestCaseData))]
            public void IReadOnlyDictionary_Keys_Always_ReturnsDictionaryKeys(Dictionary<int, int> dictionary)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                (uut as IReadOnlyDictionary<int, int>).Keys.ShouldBe(dictionary.Keys, ignoreOrder: true);
            }

            #endregion IReadOnlyDictionary.Keys Tests

            #region IReadOnlyDictionary.Values Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(DictionaryTestCaseData))]
            public void IReadOnlyDictionary_Values_Always_ReturnsDictionaryValues(Dictionary<int, int> dictionary)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                (uut as IReadOnlyDictionary<int, int>).Values.ShouldBe(dictionary.Values, ignoreOrder: true);
            }

            #endregion IReadOnlyDictionary.Values Tests

            #region IEnumerable.GetEnumerator() Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(DictionaryTestCaseData))]
            public void IEnumerable_GetEnumerator_Always_EnumeratesDictionary(Dictionary<int, int> dictionary)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                (uut as IEnumerable).Cast<object>().ShouldBe(uut.Cast<object>(), ignoreOrder: true);
            }

            #endregion IEnumerable.GetEnumerator() Tests

            #region IDictionary<TKey, TValue>.Keys Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(DictionaryTestCaseData))]
            public void IDictionary_Generic_Keys_Always_ReturnsDictionaryKeys(Dictionary<int, int> dictionary)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                (uut as IDictionary<int, int>).Keys.ShouldBe(dictionary.Keys, ignoreOrder: true);
            }

            #endregion IDictionary<TKey, TValue>.Keys Tests

            #region IDictionary<TKey, TValue>.Values Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(DictionaryTestCaseData))]
            public void IDictionary_Generic_Values_Always_ReturnsDictionaryValues(Dictionary<int, int> dictionary)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                (uut as IDictionary<int, int>).Values.ShouldBe(dictionary.Values, ignoreOrder: true);
            }

            #endregion IDictionary<TKey, TValue>.Values Tests

            #region value = IDictionary.this[] Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(ValidKeyWithMatchingValueTestCaseData))]
            public void IDictionary_Legacy_This_Get_KeyExists_ReturnsValue(Dictionary<int, int> dictionary, int key, int value)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                (uut as IDictionary)[key].ShouldBe(value);
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(InvalidKeyTestCaseData))]
            public void IDictionary_Legacy_This_Get_KeyDoesNotExist_ReturnsNull(Dictionary<int, int> dictionary, int key)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                (uut as IDictionary)[key].ShouldBeNull();
            }

            #endregion value = IDictionary.this[] Tests

            #region IDictionary.this[] = value Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(ValidKeyWithMatchingValueTestCaseData))]
            public void IDictionary_Legacy_This_Set_KeyExistsWithMatchingValue_DoesNotModifySelf(Dictionary<int, int> dictionary, int key, int value)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                (uut as IDictionary)[key] = value;

                uut.ShouldBe(dictionary, ignoreOrder: true);
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(ValidKeyWithDifferentValueTestCaseData))]
            public void IDictionary_Legacy_This_Set_KeyExistsWithDifferentValue_ReplacesValueForKey(Dictionary<int, int> dictionary, int key, int value)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                (uut as IDictionary)[key] = value;

                uut.ShouldBe(
					dictionary
						.Where(x => x.Key != key)
						.Append(new KeyValuePair<int, int>(key, value)),
					ignoreOrder: true);
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(InvalidKeyWithValueTestCaseData))]
            public void IDictionary_Legacy_This_Set_KeyDoesNotExist_AddsKeyAndValue(Dictionary<int, int> dictionary, int key, int value)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                (uut as IDictionary)[key] = value;

                uut.ShouldBe(
                    dictionary
                        .Append(new KeyValuePair<int, int>(key, value)),
                    ignoreOrder: true);
            }

            #endregion IDictionary.this[] = value Tests

            #region IDictionary.IsFixedSize Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(DictionaryTestCaseData))]
            public void IDictionary_Legacy_IsFixedSize_Always_ReturnsFalse(Dictionary<int, int> dictionary)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                (uut as IDictionary).IsFixedSize.ShouldBeFalse();
            }

            #endregion IDictionary.IsFixedSize Tests

            #region IDictionary.IsReadOnly Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(DictionaryTestCaseData))]
            public void IDictionary_Legacy_IsReadOnly_Always_ReturnsFalse(Dictionary<int, int> dictionary)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                (uut as IDictionary).IsReadOnly.ShouldBeFalse();
            }

            #endregion IDictionary.IsReadOnly Tests

            #region IDictionary.Keys Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(DictionaryTestCaseData))]
            public void IDictionary_Legacy_Keys_Always_ReturnsDictionaryKeys(Dictionary<int, int> dictionary)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                (uut as IDictionary).Keys.Cast<object>().ShouldBe(dictionary.Keys.Cast<object>(), ignoreOrder: true);
            }

            #endregion IDictionary.Keys Tests

            #region IDictionary.Values Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(DictionaryTestCaseData))]
            public void IDictionary_Legacy_Values_Always_ReturnsDictionaryValues(Dictionary<int, int> dictionary)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                (uut as IDictionary).Values.Cast<object>().ShouldBe(dictionary.Values.Cast<object>(), ignoreOrder: true);
            }

            #endregion IDictionary.Values Tests

            #region IDictionary.Add() Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(ValidKeyWithMatchingValueTestCaseData))]
            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(ValidKeyWithDifferentValueTestCaseData))]
            public void IDictionary_Legacy_Add_KeyExists_ThrowsException(Dictionary<int, int> dictionary, int key, int value)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                Should.Throw<ArgumentException>(() =>
                {
                    (uut as IDictionary).Add(key, value);
                });

                uut.ShouldBe(dictionary, ignoreOrder: true);
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(InvalidKeyWithValueTestCaseData))]
            public void IDictionary_Legacy_Add_KeyDoesNotExist_AddsKeyAndValue(Dictionary<int, int> dictionary, int key, int value)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                (uut as IDictionary).Add(key, value);

                uut.ShouldBe(
					dictionary
						.Append(new KeyValuePair<int, int>(key, value)),
					ignoreOrder: true);
            }

            #endregion IDictionary.Add() Tests

            #region IDictionary.Clear() Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(DictionaryTestCaseData))]
            public void IDictionary_Legacy_Clear_Always_ClearsSelf(Dictionary<int, int> dictionary)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                (uut as IDictionary).Clear();

                uut.ShouldBeEmpty();
            }

            #endregion IDictionary.Clear() Tests

            #region IDictionary.Contains() Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(ValidKeyTestCaseData))]
            public void IDictionary_Legacy_Contains_KeyExists_ReturnsTrue(Dictionary<int, int> dictionary, int key)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                (uut as IDictionary).Contains(key).ShouldBeTrue();
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(InvalidKeyTestCaseData))]
            public void IDictionary_Legacy_Contains_KeyDoesNotExist_ReturnsFalse(Dictionary<int, int> dictionary, int key)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                (uut as IDictionary).Contains(key).ShouldBeFalse();
            }

            #endregion IDictionary.Contains() Tests

            #region IDictionary.GetEnumerator() Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(DictionaryTestCaseData))]
            public void IDictionary_Legacy_GetEnumerator_Always_EnumeratesDictionary(Dictionary<int, int> dictionary)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                var result = (uut as IDictionary).GetEnumerator();

                var enumeration = new List<KeyValuePair<int, int>>();
                while (result.MoveNext())
                    enumeration.Add(new KeyValuePair<int, int>((int)result.Key, (int)result.Value));

                enumeration.ShouldBe(dictionary, ignoreOrder: true);
            }

            #endregion IDictionary.GetEnumerator() Tests

            #region IDictionary.Remove() Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(ValidKeyTestCaseData))]
            public void IDictionary_Legacy_Remove_KeyExists_RemovesKey(Dictionary<int, int> dictionary, int key)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                (uut as IDictionary).Remove(key);

                uut.ShouldBe(
					dictionary
						.Where(x => x.Key != key),
					ignoreOrder: true);
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(InvalidKeyTestCaseData))]
            public void IDictionary_Legacy_Remove_KeyDoesNotExist_DoesNotModifySelf(Dictionary<int, int> dictionary, int key)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                (uut as IDictionary).Remove(key);

                uut.ShouldBe(dictionary, ignoreOrder: true);
            }

            #endregion IDictionary.Remove() Tests

            #region ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(DictionaryTestCaseData))]
            public void ICollection_Generic_IsReadOnly_Always_ReturnsFalse(Dictionary<int, int> dictionary)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                (uut as ICollection<KeyValuePair<int, int>>).IsReadOnly.ShouldBeFalse();
            }

            #endregion ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly Tests

            #region ICollection<KeyValuePair<TKey, TValue>>.Add() Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(ValidKeyWithMatchingValueTestCaseData))]
            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(ValidKeyWithDifferentValueTestCaseData))]
            public void ICollection_Generic_Add_ItemKeyExists_ThrowsException(Dictionary<int, int> dictionary, int key, int value)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                Should.Throw<ArgumentException>(() =>
                {
                    (uut as ICollection<KeyValuePair<int, int>>).Add(new KeyValuePair<int, int>(key, value));
                });

                uut.ShouldBe(dictionary, ignoreOrder: true);
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(InvalidKeyWithValueTestCaseData))]
            public void ICollection_Generic_Add_ItemKeyDoesNotExist_InsertsItem(Dictionary<int, int> dictionary, int key, int value)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                (uut as ICollection<KeyValuePair<int, int>>).Add(new KeyValuePair<int, int>(key, value));

                uut.ShouldBe(
                    dictionary
                        .Append(new KeyValuePair<int, int>(key, value)),
                    ignoreOrder: true);
            }

            #endregion ICollection<KeyValuePair<TKey, TValue>>.Add() Tests

            #region ICollection<KeyValuePair<TKey, TValue>>.Clear() Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(DictionaryTestCaseData))]
            public void ICollection_Generic_Clear_Always_ClearsSelf(Dictionary<int, int> dictionary)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                (uut as ICollection<KeyValuePair<int, int>>).Clear();

                uut.ShouldBeEmpty();
            }

            #endregion ICollection<KeyValuePair<TKey, TValue>>.Clear() Tests

            #region ICollection<KeyValuePair<TKey, TValue>>.Contains() Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(ValidKeyWithMatchingValueTestCaseData))]
            public void ICollection_Generic_Contains_KeyValuePairExists_ReturnsTrue(Dictionary<int, int> dictionary, int key, int value)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                (uut as ICollection<KeyValuePair<int, int>>).Contains(new KeyValuePair<int, int>(key, value)).ShouldBeTrue();
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(ValidKeyWithDifferentValueTestCaseData))]
            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(InvalidKeyWithValueTestCaseData))]
            public void ICollection_Generic_Contains_KeyValuePairDoesNotExist_ReturnsFalse(Dictionary<int, int> dictionary, int key, int value)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                (uut as ICollection<KeyValuePair<int, int>>).Contains(new KeyValuePair<int, int>(key, value)).ShouldBeFalse();
            }

            #endregion ICollection<KeyValuePair<TKey, TValue>>.Contains() Tests

            #region ICollection<KeyValuePair<TKey, TValue>>.CopyTo() Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(ValidCopyToTestCaseData))]
            public void ICollection_Generic_CopyTo_ArrayIsNull_ThrowsException(Dictionary<int, int> dictionary, int arraySize, int arrayIndex)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                Should.Throw<ArgumentNullException>(() =>
                {
                    (uut as ICollection<KeyValuePair<int, int>>).CopyTo(null, arrayIndex);
                });
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(DictionaryTestCaseData))]
            public void ICollection_Generic_CopyTo_ArrayIndexIsNegative_ThrowsException(Dictionary<int, int> dictionary)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                var array = new KeyValuePair<int, int>[dictionary.Count];

                Should.Throw<ArgumentOutOfRangeException>(() =>
                {
                    (uut as ICollection<KeyValuePair<int, int>>).CopyTo(array, -1);
                });
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(InvalidCopyToTestCaseData))]
            public void ICollection_Generic_CopyTo_ArrayBoundsAreNotValid_ThrowsException(Dictionary<int, int> dictionary, int arraySize, int arrayIndex)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                var array = new KeyValuePair<int, int>[arraySize];

                Should.Throw<ArgumentException>(() =>
                {
                    (uut as ICollection<KeyValuePair<int, int>>).CopyTo(array, arrayIndex);
                });
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(ValidCopyToTestCaseData))]
            public void ICollection_Generic_CopyTo_Otherwise_CopiesSelfIntoArrayAtIndex(Dictionary<int, int> dictionary, int arraySize, int arrayIndex)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                var array = new KeyValuePair<int, int>[arraySize];

                (uut as ICollection<KeyValuePair<int, int>>).CopyTo(array, arrayIndex);

                array
                    .Skip(arrayIndex)
                    .Take(dictionary.Count)
                    .ShouldBe(uut, ignoreOrder: true);

                array
                    .Take(arrayIndex)
                    .Concat(array
                        .Skip(arrayIndex + dictionary.Count))
                    .ShouldBe(Enumerable.Repeat(default(KeyValuePair<int, int>), arraySize - dictionary.Count));
            }

            #endregion ICollection<KeyValuePair<TKey, TValue>>.CopyTo() Tests

            #region ICollection<KeyValuePair<TKey, TValue>>.Remove() Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(ValidKeyWithMatchingValueTestCaseData))]
            public void ICollection_Generic_Remove_ItemExists_RemovesItemAndReturnsTrue(Dictionary<int, int> dictionary, int key, int value)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                (uut as ICollection<KeyValuePair<int, int>>).Remove(new KeyValuePair<int, int>(key, value)).ShouldBeTrue();

                uut.ShouldBe(
					dictionary
						.Where(x => (x.Key != key) || (x.Value != value)),
					ignoreOrder: true);
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(ValidKeyWithDifferentValueTestCaseData))]
            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(InvalidKeyWithValueTestCaseData))]
            public void ICollection_Generic_Remove_ItemDoesNotExist_DoesNotRemoveItemAndReturnsFalse(Dictionary<int, int> dictionary, int key, int value)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                (uut as ICollection<KeyValuePair<int, int>>).Remove(new KeyValuePair<int, int>(key, value)).ShouldBeFalse();

                uut.ShouldBe(dictionary, ignoreOrder: true);
            }

            #endregion ICollection<KeyValuePair<TKey, TValue>>.Remove() Tests

            #region ICollection.IsSynchronized Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(DictionaryTestCaseData))]
            public void ICollection_Legacy_IsSynchronized_ReturnsFalse(Dictionary<int, int> dictionary)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                (uut as ICollection).IsSynchronized.ShouldBeFalse();
            }

            #endregion ICollection.IsSynchronized Tests

            #region ICollection.SyncRoot Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(DictionaryTestCaseData))]
            public void ICollection_Legacy_SyncRoot_ReturnsObject(Dictionary<int, int> dictionary)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                var result = (uut as ICollection);
				
				result.SyncRoot.ShouldNotBeNull();
                result.SyncRoot.ShouldNotBeSameAs(uut);
            }

            #endregion ICollection.SyncRoot Tests

            #region ICollection.CopyTo() Tests

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(ValidCopyToTestCaseData))]
            public void ICollection_Legacy_CopyTo_ArrayIsNull_ThrowsException(Dictionary<int, int> dictionary, int arraySize, int arrayIndex)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                Should.Throw<ArgumentNullException>(() =>
                {
                    (uut as ICollection).CopyTo(null, arrayIndex);
                });
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(DictionaryTestCaseData))]
            public void ICollection_Legacy_CopyTo_ArrayIndexIsNegative_ThrowsException(Dictionary<int, int> dictionary)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                var array = new KeyValuePair<int, int>[dictionary.Count];

                Should.Throw<ArgumentOutOfRangeException>(() =>
                {
                    (uut as ICollection).CopyTo(array, -1);
                });
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(InvalidCopyToTestCaseData))]
            public void ICollection_Legacy_CopyTo_ArrayBoundsAreNotValid_ThrowsException(Dictionary<int, int> dictionary, int arraySize, int arrayIndex)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                var array = new KeyValuePair<int, int>[arraySize];

                Should.Throw<ArgumentException>(() =>
                {
                    (uut as ICollection).CopyTo(array, arrayIndex);
                });
            }

            [TestCaseSource(typeof(ImmutableHashDictionaryTests), nameof(ValidCopyToTestCaseData))]
            public void ICollection_Legacy_CopyTo_Otherwise_CopiesSelfIntoArrayAtIndex(Dictionary<int, int> dictionary, int arraySize, int arrayIndex)
            {
                (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

                var array = new KeyValuePair<int, int>[arraySize];

                (uut as ICollection).CopyTo(array, arrayIndex);

                array
                    .Skip(arrayIndex)
                    .Take(dictionary.Count)
                    .ShouldBe(uut, ignoreOrder: true);

                array
                    .Take(arrayIndex)
                    .Concat(array
                        .Skip(arrayIndex + dictionary.Count))
                    .ShouldBe(Enumerable.Repeat(default(KeyValuePair<int, int>), arraySize - dictionary.Count));
            }

            #endregion ICollection.CopyTo() Tests
        }
    }
}
