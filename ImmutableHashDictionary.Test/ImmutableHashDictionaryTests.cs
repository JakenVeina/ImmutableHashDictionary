using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;
using Shouldly;

namespace System.Collections.Immutable.Extra.Test
{
    [TestFixture]
    public partial class ImmutableHashDictionaryTests
    {
        #region Test Context

        public static (IEqualityComparer<int> keyComparer, IEqualityComparer<int> valueComparer, ImmutableHashDictionary<int, int> uut) BuildTestContext(Dictionary<int, int> dictionary)
        {
            var mockKeyComparer = new Mock<IEqualityComparer<int>>();
            mockKeyComparer
                .Setup(x => x.Equals(It.IsAny<int>(), It.IsAny<int>()))
                .Returns((int x, int y) => EqualityComparer<int>.Default.Equals(x, y));
            mockKeyComparer
                .Setup(x => x.GetHashCode(It.IsAny<int>()))
                .Returns((int x) => EqualityComparer<int>.Default.GetHashCode(x));

            var mockValueComparer = new Mock<IEqualityComparer<int>>();
            mockValueComparer
                .Setup(x => x.Equals(It.IsAny<int>(), It.IsAny<int>()))
                .Returns((int x, int y) => EqualityComparer<int>.Default.Equals(x, y));
            mockValueComparer
                .Setup(x => x.GetHashCode(It.IsAny<int>()))
                .Returns((int x) => EqualityComparer<int>.Default.GetHashCode(x));

            var uut = new ImmutableHashDictionary<int, int>(dictionary, mockKeyComparer.Object, mockValueComparer.Object);

            return (mockKeyComparer.Object, mockValueComparer.Object, uut);
        }

        #endregion Test Context

        #region Empty Tests

        [Test]
        public void Empty_Always_IsEmpty()
        {
            ImmutableHashDictionary<int, int>.Empty.AsEnumerable().ShouldBeEmpty();
            ImmutableHashDictionary<int, int>.Empty.Count.ShouldBe(0);
            ImmutableHashDictionary<int, int>.Empty.IsEmpty.ShouldBeTrue();
        }

        #endregion Empty Tests

        #region Constructor Tests

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void Constructor_DictionaryIsGiven_ResultMatchesDictionary(Dictionary<int, int> dictionary)
        {
            var result = new ImmutableHashDictionary<int, int>(dictionary: dictionary);

            result.AsEnumerable().ShouldBe(dictionary, ignoreOrder: true);
        }

        [Test]
        public void Constructor_DictionaryIsNull_ResultIsEmpty()
        {
            var result = new ImmutableHashDictionary<int, int>(dictionary: null);

            result.AsEnumerable().ShouldBeEmpty();
        }

        [Test]
        public void Constructor_KeyComparerIsGiven_KeyComparerIsGiven()
        {
            var keyComparer = new Mock<IEqualityComparer<int>>().Object;

            var result = new ImmutableHashDictionary<int, int>(keyComparer: keyComparer);

            result.KeyComparer.ShouldBeSameAs(keyComparer);
        }

        [Test]
        public void Constructor_KeyComparerIsNull_KeyComparerIsDefault()
        {
            var result = new ImmutableHashDictionary<int, int>(keyComparer: null);

            result.KeyComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
        }

        [Test]
        public void Constructor_ValueComparerIsGiven_ValueComparerIsGiven()
        {
            var valueComparer = new Mock<IEqualityComparer<int>>().Object;

            var result = new ImmutableHashDictionary<int, int>(valueComparer: valueComparer);

            result.ValueComparer.ShouldBeSameAs(valueComparer);
        }

        [Test]
        public void Constructor_ValueComparerIsGiven_ValueComparerIsDefault()
        {
            var result = new ImmutableHashDictionary<int, int>(keyComparer: null);

            result.ValueComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
        }

        #endregion Constructor Tests

        #region this[] Tests

        [TestCaseSource(nameof(ValidKeyWithMatchingValueTestCaseData))]
        public void This_Get_KeyExists_ReturnsValue(Dictionary<int, int> dictionary, int key, int value)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut[key].ShouldBe(value);
        }

        [TestCaseSource(nameof(InvalidKeyTestCaseData))]
        public void This_Get_KeyDoesNotExist_ThrowsException(Dictionary<int, int> dictionary, int key)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            Should.Throw<KeyNotFoundException>(() =>
            {
                var value = uut[key];
            });
        }

        #endregion this[] Tests

        #region Count Tests

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void Count_Always_ReturnsDictionaryCount(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.Count.ShouldBe(dictionary.Count);
        }

        #endregion Count Tests

        #region IsEmpty Tests

        [Test]
        public void IsEmpty_DictionaryIsEmpty_ReturnsTrue()
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(new Dictionary<int, int>());

            uut.IsEmpty.ShouldBeTrue();
        }

        [TestCaseSource(nameof(NonEmptyDictionaryTestCaseData))]
        public void IsEmpty_DictionaryIsNotEmpty_ReturnsFalse(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.IsEmpty.ShouldBeFalse();
        }

        #endregion IsEmpty Tests

        #region Keys Tests

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void Keys_Always_ReturnsDictionaryKeys(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.Keys.ShouldBe(dictionary.Keys, ignoreOrder: true);
        }

        #endregion Keys Tests

        #region Values Tests

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void Values_Always_ReturnsDictionaryValues(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.Values.ShouldBe(dictionary.Values, ignoreOrder: true);
        }

        #endregion Values Tests

        #region Add() Tests

        [TestCaseSource(nameof(ValidKeyWithMatchingValueTestCaseData))]
        public void Add_KeyExistsWithMatchingValue_ReturnsSelf(Dictionary<int, int> dictionary, int key, int value)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.Add(key, value).ShouldBeSameAs(uut);
            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        [TestCaseSource(nameof(ValidKeyWithDifferentValueTestCaseData))]
        public void Add_KeyExistsWithDifferentValue_ThrowsException(Dictionary<int, int> dictionary, int key, int value)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            Should.Throw<ArgumentException>(() =>
            {
                uut.Add(key, value);
            });

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        [TestCaseSource(nameof(InvalidKeyWithValueTestCaseData))]
        public void Add_KeyDoesNotExist_AddsKeyAndValueToResult(Dictionary<int, int> dictionary, int key, int value)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.Add(key, value).ShouldBe(dictionary.Append(new KeyValuePair<int, int>(key, value)), ignoreOrder: true);

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        #endregion Add() Tests

        #region AddRange() Tests

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void AddRange_KeyValuePairsIsNull_ThrowsException(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            Should.Throw<ArgumentNullException>(() =>
            {
                uut.AddRange(null!);
            });

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        [TestCaseSource(nameof(EmptyKeyValuePairsTestCaseData))]
        [TestCaseSource(nameof(AllKeyValuePairsExistTestCaseData))]
        public void AddRange_AllKeysExistWithMatchingValue_ReturnsSelf(Dictionary<int, int> dictionary, IEnumerable<KeyValuePair<int, int>> keyValuePairs)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.AddRange(keyValuePairs).ShouldBeSameAs(uut);

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        [TestCaseSource(nameof(AllKeyValuePairsInvalidTestCaseData))]
        [TestCaseSource(nameof(SomeKeyValuePairsInvalidTestCaseData))]
        public void AddRange_AnyKeysExistWithDifferentValue_ThrowsException(Dictionary<int, int> dictionary, IEnumerable<KeyValuePair<int, int>> keyValuePairs)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            Should.Throw<ArgumentException>(() =>
            {
                uut.AddRange(keyValuePairs);
            });

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        [TestCaseSource(nameof(AllKeyValuePairsDoNotExistTestCaseData))]
        [TestCaseSource(nameof(SomeKeyValuePairsDoNotExistTestCaseData))]
        public void AddRange_Otherwise_AddsNewKeysAndValuesToResult(Dictionary<int, int> dictionary, IEnumerable<KeyValuePair<int, int>> keyValuePairs)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.AddRange(keyValuePairs).ShouldBe(
                dictionary.Concat(keyValuePairs
                    .Where(x => !dictionary.ContainsKey(x.Key))),
                ignoreOrder: true);

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        #endregion AddRange() Tests

        #region Clear() Tests

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void Clear_Always_ResultIsEmpty(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            var result = uut.Clear();

            result.ShouldBeEmpty();
            result.KeyComparer.ShouldBeSameAs(keyComparer);
            result.ValueComparer.ShouldBeSameAs(valueComparer);
        }

        #endregion Clear() Tests

        #region Contains() Tests

        [TestCaseSource(nameof(ValidKeyWithMatchingValueTestCaseData))]
        public void Contains_KeyValuePairExists_ReturnsTrue(Dictionary<int, int> dictionary, int key, int value)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.Contains(new KeyValuePair<int, int>(key, value)).ShouldBeTrue();
        }

        [TestCaseSource(nameof(ValidKeyWithDifferentValueTestCaseData))]
        [TestCaseSource(nameof(InvalidKeyWithValueTestCaseData))]
        public void Contains_KeyValuePairDoesNotExist_ReturnsFalse(Dictionary<int, int> dictionary, int key, int value)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.Contains(new KeyValuePair<int, int>(key, value)).ShouldBeFalse();
        }

        #endregion Contains() Tests

        #region ContainsKey() Tests

        [TestCaseSource(nameof(ValidKeyTestCaseData))]
        public void ContainsKey_KeyExists_ReturnsTrue(Dictionary<int, int> dictionary, int key)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.ContainsKey(key).ShouldBeTrue();
        }

        [TestCaseSource(nameof(InvalidKeyTestCaseData))]
        public void ContainsKey_KeyDoesNotExist_ReturnsFalse(Dictionary<int, int> dictionary, int key)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.ContainsKey(key).ShouldBeFalse();
        }

        #endregion ContainsKey() Tests

        #region ContainsValue() Tests

        [TestCaseSource(nameof(ValidValueTestCaseData))]
        public void ContainsValue_ValueExists_ReturnsTrue(Dictionary<int, int> dictionary, int value)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.ContainsValue(value).ShouldBeTrue();
        }

        [TestCaseSource(nameof(InvalidValueTestCaseData))]
        public void ContainsValue_ValueDoesNotExist_ReturnsFalse(Dictionary<int, int> dictionary, int value)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.ContainsValue(value).ShouldBeFalse();
        }

        #endregion ContainsValue() Tests

        #region GetEnumerator() Tests

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void GetEnumerator_Always_EnumeratesDictionary(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.AsEnumerable().ShouldBe(dictionary, ignoreOrder: true);
        }

        #endregion GetEnumerator() Tests

        #region Remove() Tests

        [TestCaseSource(nameof(InvalidKeyTestCaseData))]
        public void Remove_KeyDoesNotExist_ReturnsSelf(Dictionary<int, int> dictionary, int key)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.Remove(key).ShouldBeSameAs(uut);

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        [TestCaseSource(nameof(ValidKeyTestCaseData))]
        public void Remove_KeyExists_RemovesKeyFromResult(Dictionary<int, int> dictionary, int key)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.Remove(key).ShouldBe(dictionary.Where(x => x.Key != key), ignoreOrder: true);

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        #endregion Remove() Tests

        #region RemoveRange() Tests

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void RemoveRange_KeysIsNull_ThrowsException(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            Should.Throw<ArgumentNullException>(() =>
            {
                uut.RemoveRange(null!);
            });

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        [TestCaseSource(nameof(EmptyKeysTestCaseData))]
        [TestCaseSource(nameof(AllKeysDoNotExistTestCaseData))]
        public void RemoveRange_AllKeysDoNotExist_ReturnsSelf(Dictionary<int, int> dictionary, IEnumerable<int> keys)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.RemoveRange(keys).ShouldBeSameAs(uut);

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        [TestCaseSource(nameof(AllKeysExistTestCaseData))]
        [TestCaseSource(nameof(SomeKeysDoNotExistTestCaseData))]
        public void RemoveRange_AnyKeysExist_RemovesExistingKeysFromResult(Dictionary<int, int> dictionary, IEnumerable<int> keys)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.RemoveRange(keys).ShouldBe(
                dictionary.Where(x => !keys.Contains(x.Key)),
                ignoreOrder: true);

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        #endregion RemoveRange() Tests

        #region SetItem() Tests

        [TestCaseSource(nameof(ValidKeyWithMatchingValueTestCaseData))]
        public void SetItem_KeyExistsWithMatchingValue_ReturnsSelf(Dictionary<int, int> dictionary, int key, int value)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.SetItem(key, value).ShouldBeSameAs(uut);

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        [TestCaseSource(nameof(ValidKeyWithDifferentValueTestCaseData))]
        public void SetItem_KeyExistsWithDifferentValue_ReplacesValueInResult(Dictionary<int, int> dictionary, int key, int value)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.SetItem(key, value).ShouldBe(
                dictionary
                    .Where(x => x.Key != key)
                    .Append(new KeyValuePair<int, int>(key, value)),
                ignoreOrder: true);

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        [TestCaseSource(nameof(InvalidKeyWithValueTestCaseData))]
        public void SetItem_KeyDoesNotExist_AddsKeyAndValueToResult(Dictionary<int, int> dictionary, int key, int value)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.SetItem(key, value).ShouldBe(dictionary.Append(new KeyValuePair<int, int>(key, value)), ignoreOrder: true);

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        #endregion SetItem() Tests

        #region SetItems() Tests

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void SetItems_KeyValuePairsIsNull_ThrowsException(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            Should.Throw<ArgumentNullException>(() =>
            {
                uut.SetItems(null!);
            });

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        [TestCaseSource(nameof(EmptyKeyValuePairsTestCaseData))]
        [TestCaseSource(nameof(AllKeyValuePairsExistTestCaseData))]
        public void SetItems_AllKeysExistWithMatchingValue_ReturnsSelf(Dictionary<int, int> dictionary, IEnumerable<KeyValuePair<int, int>> keyValuePairs)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.SetItems(keyValuePairs).ShouldBeSameAs(uut);

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        [TestCaseSource(nameof(AllKeyValuePairsInvalidTestCaseData))]
        [TestCaseSource(nameof(SomeKeyValuePairsInvalidTestCaseData))]
        [TestCaseSource(nameof(AllKeyValuePairsDoNotExistTestCaseData))]
        [TestCaseSource(nameof(SomeKeyValuePairsDoNotExistTestCaseData))]
        public void SetItems_Otherwise_ReplacesExistingValuesAndAddsNewKeysAndValuesToResult(Dictionary<int, int> dictionary, IEnumerable<KeyValuePair<int, int>> keyValuePairs)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.SetItems(keyValuePairs).ShouldBe(
                dictionary
                    .Where(x => !keyValuePairs.Any(y => y.Key == x.Key))
                    .Concat(keyValuePairs),
                ignoreOrder: true);

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        #endregion SetItems() Tests

        #region ToBuilder() Tests

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ToBuilder_Always_ResultMatchesSelf(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.ToBuilder().AsEnumerable().ShouldBe(uut, ignoreOrder: true);
        }

        #endregion ToBuilder() Tests

        #region TryGetKey() Tests

        [TestCaseSource(nameof(InvalidKeyTestCaseData))]
        public void TryGetKey_MatchingKeyDoesNotExist_ReturnsFalse(Dictionary<int, int> dictionary, int key)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.TryGetKey(key, out var actualKey).ShouldBeFalse();
        }

        [TestCaseSource(nameof(ValidKeyTestCaseData))]
        public void TryGetKey_MatchingKeyExists_ReturnsTrueAndMatchingKey(Dictionary<int, int> dictionary, int key)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.TryGetKey(key, out var actualKey).ShouldBeTrue();

            actualKey.ShouldBe(key);
        }

        #endregion TryGetKey() Tests

        #region TryGetValue() Tests

        [TestCaseSource(nameof(InvalidKeyTestCaseData))]
        public void TryGetValue_KeyDoesNotExist_ReturnsFalse(Dictionary<int, int> dictionary, int key)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.TryGetValue(key, out var value).ShouldBeFalse();
        }

        [TestCaseSource(nameof(ValidKeyWithMatchingValueTestCaseData))]
        public void TryGetValue_KeyExists_ReturnsTrueAndMatchingValue(Dictionary<int, int> dictionary, int key, int expectedValue)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.TryGetValue(key, out var value).ShouldBeTrue();

            value.ShouldBe(expectedValue);
        }

        #endregion TryGetValue() Tests

        #region WithComparers() Tests

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void WithComparers_KeyComparerIsGiven_ResultKeyComparerMatchesGiven(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            var newKeyComparer = new Mock<IEqualityComparer<int>>().Object;

            var result = uut.WithComparers(newKeyComparer);

            result.KeyComparer.ShouldBeSameAs(newKeyComparer);
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void WithComparers_BothComparerAreGiven_ResultComparersMatchGiven(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            var newKeyComparer = new Mock<IEqualityComparer<int>>().Object;
            var newValueComparer = new Mock<IEqualityComparer<int>>().Object;

            var result = uut.WithComparers(newKeyComparer, newValueComparer);

            result.KeyComparer.ShouldBeSameAs(newKeyComparer);
            result.ValueComparer.ShouldBeSameAs(newValueComparer);
        }

        #endregion WithComparers() Tests

        #region IImmutableDictionary.Add() Tests

        [TestCaseSource(nameof(ValidKeyWithMatchingValueTestCaseData))]
        public void IImmutableDictionary_Add_KeyExistsWithMatchingValue_ReturnsSelf(Dictionary<int, int> dictionary, int key, int value)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            (uut as IImmutableDictionary<int, int>).Add(key, value).ShouldBeSameAs(uut);
            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        [TestCaseSource(nameof(ValidKeyWithDifferentValueTestCaseData))]
        public void IImmutableDictionary_Add_KeyExistsWithDifferentValue_ThrowsException(Dictionary<int, int> dictionary, int key, int value)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            Should.Throw<ArgumentException>(() =>
            {
                (uut as IImmutableDictionary<int, int>).Add(key, value);
            });

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        [TestCaseSource(nameof(InvalidKeyWithValueTestCaseData))]
        public void IImmutableDictionary_Add_KeyDoesNotExist_AddsKeyAndValueToResult(Dictionary<int, int> dictionary, int key, int value)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            (uut as IImmutableDictionary<int, int>).Add(key, value).ShouldBe(dictionary.Append(new KeyValuePair<int, int>(key, value)), ignoreOrder: true);

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        #endregion IImmutableDictionary.Add() Tests

        #region IImmutableDictionary.AddRange() Tests

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void IImmutableDictionary_AddRange_KeyValuePairsIsNull_ThrowsException(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            Should.Throw<ArgumentNullException>(() =>
            {
                uut.AddRange(null!);
            });

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        [TestCaseSource(nameof(EmptyKeyValuePairsTestCaseData))]
        [TestCaseSource(nameof(AllKeyValuePairsExistTestCaseData))]
        public void IImmutableDictionary_AddRange_AllKeysExistWithMatchingValue_ReturnsSelf(Dictionary<int, int> dictionary, IEnumerable<KeyValuePair<int, int>> keyValuePairs)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.AddRange(keyValuePairs).ShouldBeSameAs(uut);

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        [TestCaseSource(nameof(AllKeyValuePairsInvalidTestCaseData))]
        [TestCaseSource(nameof(SomeKeyValuePairsInvalidTestCaseData))]
        public void IImmutableDictionary_AddRange_AnyKeysExistWithDifferentValue_ThrowsException(Dictionary<int, int> dictionary, IEnumerable<KeyValuePair<int, int>> keyValuePairs)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            Should.Throw<ArgumentException>(() =>
            {
                uut.AddRange(keyValuePairs);
            });

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        [TestCaseSource(nameof(AllKeyValuePairsDoNotExistTestCaseData))]
        [TestCaseSource(nameof(SomeKeyValuePairsDoNotExistTestCaseData))]
        public void IImmutableDictionary_AddRange_Otherwise_AddsNewKeysAndValuesToResult(Dictionary<int, int> dictionary, IEnumerable<KeyValuePair<int, int>> keyValuePairs)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.AddRange(keyValuePairs).ShouldBe(
                dictionary.Concat(keyValuePairs
                    .Where(x => !dictionary.ContainsKey(x.Key))),
                ignoreOrder: true);

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        #endregion IImmutableDictionary.AddRange() Tests

        #region IImmutableDictionary.Clear() Tests

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void IImmutableDictionary_Clear_Always_ResultIsEmpty(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            (uut as IImmutableDictionary<int, int>).Clear().ShouldBeEmpty();
        }

        #endregion IImmutableDictionary.Clear() Tests

        #region IImmutableDictionary.Remove() Tests

        [TestCaseSource(nameof(InvalidKeyTestCaseData))]
        public void IImmutableDictionary_Remove_KeyDoesNotExist_ReturnsSelf(Dictionary<int, int> dictionary, int key)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            (uut as IImmutableDictionary<int, int>).Remove(key).ShouldBeSameAs(uut);

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        [TestCaseSource(nameof(ValidKeyTestCaseData))]
        public void IImmutableDictionary_Remove_KeyExists_RemovesKeyFromResult(Dictionary<int, int> dictionary, int key)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            (uut as IImmutableDictionary<int, int>).Remove(key).ShouldBe(dictionary.Where(x => x.Key != key), ignoreOrder: true);

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        #endregion IImmutableDictionary.Remove() Tests

        #region IImmutableDictionary.RemoveRange() Tests

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void IImmutableDictionary_RemoveRange_KeysIsNull_ThrowsException(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            Should.Throw<ArgumentNullException>(() =>
            {
                uut.RemoveRange(null!);
            });

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        [TestCaseSource(nameof(EmptyKeysTestCaseData))]
        [TestCaseSource(nameof(AllKeysDoNotExistTestCaseData))]
        public void IImmutableDictionary_RemoveRange_AllKeysDoNotExist_ReturnsSelf(Dictionary<int, int> dictionary, IEnumerable<int> keys)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.RemoveRange(keys).ShouldBeSameAs(uut);

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        [TestCaseSource(nameof(AllKeysExistTestCaseData))]
        [TestCaseSource(nameof(SomeKeysDoNotExistTestCaseData))]
        public void IImmutableDictionary_RemoveRange_AnyKeysExist_RemovesExistingKeysFromResult(Dictionary<int, int> dictionary, IEnumerable<int> keys)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.RemoveRange(keys).ShouldBe(
                dictionary.Where(x => !keys.Contains(x.Key)),
                ignoreOrder: true);

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        #endregion IImmutableDictionary.RemoveRange() Tests

        #region IImmutableDictionary.SetItem() Tests

        [TestCaseSource(nameof(ValidKeyWithMatchingValueTestCaseData))]
        public void IImmutableDictionary_SetItem_KeyExistsWithMatchingValue_ReturnsSelf(Dictionary<int, int> dictionary, int key, int value)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            (uut as IImmutableDictionary<int, int>).SetItem(key, value).ShouldBeSameAs(uut);

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        [TestCaseSource(nameof(ValidKeyWithDifferentValueTestCaseData))]
        public void IImmutableDictionary_SetItem_KeyExistsWithDifferentValue_ReplacesValueInResult(Dictionary<int, int> dictionary, int key, int value)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            (uut as IImmutableDictionary<int, int>).SetItem(key, value).ShouldBe(
                dictionary
                    .Where(x => x.Key != key)
                    .Append(new KeyValuePair<int, int>(key, value)),
                ignoreOrder: true);

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        [TestCaseSource(nameof(InvalidKeyWithValueTestCaseData))]
        public void IImmutableDictionary_SetItem_KeyDoesNotExist_AddsKeyAndValueToResult(Dictionary<int, int> dictionary, int key, int value)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            (uut as IImmutableDictionary<int, int>).SetItem(key, value).ShouldBe(dictionary.Append(new KeyValuePair<int, int>(key, value)), ignoreOrder: true);

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        #endregion IImmutableDictionary.SetItem() Tests

        #region IImmutableDictionary.SetItems() Tests

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void IImmutableDictionary_SetItems_KeyValuePairsIsNull_ThrowsException(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            Should.Throw<ArgumentNullException>(() =>
            {
                uut.SetItems(null!);
            });

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        [TestCaseSource(nameof(EmptyKeyValuePairsTestCaseData))]
        [TestCaseSource(nameof(AllKeyValuePairsExistTestCaseData))]
        public void IImmutableDictionary_SetItems_AllKeysExistWithMatchingValue_ReturnsSelf(Dictionary<int, int> dictionary, IEnumerable<KeyValuePair<int, int>> keyValuePairs)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.SetItems(keyValuePairs).ShouldBeSameAs(uut);

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        [TestCaseSource(nameof(AllKeyValuePairsInvalidTestCaseData))]
        [TestCaseSource(nameof(SomeKeyValuePairsInvalidTestCaseData))]
        [TestCaseSource(nameof(AllKeyValuePairsDoNotExistTestCaseData))]
        [TestCaseSource(nameof(SomeKeyValuePairsDoNotExistTestCaseData))]
        public void IImmutableDictionary_SetItems_Otherwise_ReplacesExistingValuesAndAddsNewKeysAndValuesToResult(Dictionary<int, int> dictionary, IEnumerable<KeyValuePair<int, int>> keyValuePairs)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            uut.SetItems(keyValuePairs).ShouldBe(
                dictionary
                    .Where(x => !keyValuePairs.Any(y => y.Key == x.Key))
                    .Concat(keyValuePairs),
                ignoreOrder: true);

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        #endregion IImmutableDictionary.SetItems() Tests

        #region IReadOnlyDictionary.Keys Tests

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void IReadOnlyDictionary_Keys_Always_ReturnsDictionaryKeys(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            (uut as IReadOnlyDictionary<int, int>).Keys.ShouldBe(dictionary.Keys, ignoreOrder: true);
        }

        #endregion IReadOnlyDictionary.Keys Tests

        #region IReadOnlyDictionary.Values Tests

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void IReadOnlyDictionary_Values_Always_ReturnsDictionaryValues(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            (uut as IReadOnlyDictionary<int, int>).Values.ShouldBe(dictionary.Values, ignoreOrder: true);
        }

        #endregion IReadOnlyDictionary.Values Tests

        #region IEnumerable.GetEnumerator() Tests

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void IEnumerable_GetEnumerator_Always_EnumeratesDictionary(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            (uut as IEnumerable).Cast<object>().ShouldBe(uut.Cast<object>(), ignoreOrder: true);
        }

        #endregion IEnumerable.GetEnumerator() Tests

        #region IDictionary<TKey, TValue>.this[] Tests

        [TestCaseSource(nameof(ValidKeyWithMatchingValueTestCaseData))]
        public void IDictionary_Generic_This_Get_KeyExists_ReturnsValue(Dictionary<int, int> dictionary, int key, int value)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            (uut as IDictionary<int, int>)[key].ShouldBe(value);
        }

        [TestCaseSource(nameof(InvalidKeyTestCaseData))]
        public void IDictionary_Generic_This_Get_KeyDoesNotExist_ThrowsException(Dictionary<int, int> dictionary, int key)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            Should.Throw<KeyNotFoundException>(() =>
            {
                var value = (uut as IDictionary<int, int>)[key];
            });
        }

        [TestCaseSource(nameof(ValidKeyWithMatchingValueTestCaseData))]
        [TestCaseSource(nameof(ValidKeyWithDifferentValueTestCaseData))]
        [TestCaseSource(nameof(InvalidKeyWithValueTestCaseData))]
        public void IDictionary_Generic_This_Set_Always_ThrowsNotSupported(Dictionary<int, int> dictionary, int key, int value)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            Should.Throw<NotSupportedException>(() =>
            {
                (uut as IDictionary<int, int>)[key] = value;
            });

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        #endregion IDictionary<TKey, TValue>.this[] Tests

        #region IDictionary<TKey, TValue>.Keys Tests

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void IDictionary_Generic_Keys_Always_ReturnsDictionaryKeys(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            (uut as IDictionary<int, int>).Keys.ShouldBe(dictionary.Keys, ignoreOrder: true);
        }

        #endregion IDictionary<TKey, TValue>.Keys Tests

        #region IDictionary<TKey, TValue>.Values Tests

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void IDictionary_Generic_Values_Always_ReturnsDictionaryValues(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            (uut as IDictionary<int, int>).Values.ShouldBe(dictionary.Values, ignoreOrder: true);
        }

        #endregion IDictionary<TKey, TValue>.Values Tests

        #region IDictionary<TKey, TValue>.Add() Tests

        [TestCaseSource(nameof(ValidKeyWithMatchingValueTestCaseData))]
        [TestCaseSource(nameof(ValidKeyWithDifferentValueTestCaseData))]
        [TestCaseSource(nameof(InvalidKeyWithValueTestCaseData))]
        public void IDictionary_Generic_Add_Always_ThrowsNotSupported(Dictionary<int, int> dictionary, int key, int value)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            Should.Throw<NotSupportedException>(() =>
            {
                (uut as IDictionary<int, int>).Add(key, value);
            });

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        #endregion IDictionary<TKey, TValue>.Add() Tests

        #region IDictionary<TKey, TValue>.Remove() Tests

        [TestCaseSource(nameof(ValidKeyTestCaseData))]
        [TestCaseSource(nameof(InvalidKeyTestCaseData))]
        public void IDictionary_Generic_Remove_Always_ThrowsNotSupported(Dictionary<int, int> dictionary, int key)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            Should.Throw<NotSupportedException>(() =>
            {
                (uut as IDictionary<int, int>).Remove(key);
            });

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        #endregion IDictionary<TKey, TValue>.Remove() Tests

        #region IDictionary.this[] Tests

        [TestCaseSource(nameof(ValidKeyWithMatchingValueTestCaseData))]
        public void IDictionary_Legacy_This_Get_KeyExists_ReturnsValue(Dictionary<int, int> dictionary, int key, int value)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            (uut as IDictionary)[key].ShouldBe(value);
        }

        [TestCaseSource(nameof(InvalidKeyTestCaseData))]
        public void IDictionary_Legacy_This_Get_KeyDoesNotExist_ReturnsNull(Dictionary<int, int> dictionary, int key)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            (uut as IDictionary)[key].ShouldBeNull();
        }

        [TestCaseSource(nameof(ValidKeyWithMatchingValueTestCaseData))]
        [TestCaseSource(nameof(ValidKeyWithDifferentValueTestCaseData))]
        [TestCaseSource(nameof(InvalidKeyWithValueTestCaseData))]
        public void IDictionary_Legacy_This_Set_Always_ThrowsNotSupported(Dictionary<int, int> dictionary, int key, int value)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            Should.Throw<NotSupportedException>(() =>
            {
                (uut as IDictionary)[key] = value;
            });

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        #endregion IDictionary.this[] Tests

        #region IDictionary.IsFixedSize Tests

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void IDictionary_Legacy_IsFixedSize_Always_ReturnsTrue(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            (uut as IDictionary).IsFixedSize.ShouldBeTrue();
        }

        #endregion IDictionary.IsFixedSize Tests

        #region IDictionary.IsReadOnly Tests

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void IDictionary_Legacy_IsReadOnly_Always_ReturnsTrue(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            (uut as IDictionary).IsReadOnly.ShouldBeTrue();
        }

        #endregion IDictionary.IsReadOnly Tests

        #region IDictionary.Keys Tests

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void IDictionary_Legacy_Keys_Always_ReturnsDictionaryKeys(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            (uut as IDictionary).Keys.Cast<object>().ShouldBe(dictionary.Keys.Cast<object>(), ignoreOrder: true);
        }

        #endregion IDictionary.Keys Tests

        #region IDictionary.Values Tests

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void IDictionary_Legacy_Values_Always_ReturnsDictionaryValues(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            (uut as IDictionary).Values.Cast<object>().ShouldBe(dictionary.Values.Cast<object>(), ignoreOrder: true);
        }

        #endregion IDictionary.Values Tests

        #region IDictionary.Add() Tests

        [TestCaseSource(nameof(ValidKeyWithMatchingValueTestCaseData))]
        [TestCaseSource(nameof(ValidKeyWithDifferentValueTestCaseData))]
        [TestCaseSource(nameof(InvalidKeyWithValueTestCaseData))]
        public void IDictionary_Legacy_Add_Always_ThrowsNotSupported(Dictionary<int, int> dictionary, int key, int value)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            Should.Throw<NotSupportedException>(() =>
            {
                (uut as IDictionary).Add(key, value);
            });

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        #endregion IDictionary.Add() Tests

        #region IDictionary.Clear() Tests

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void IDictionary_Legacy_Clear_Always_ThrowsNotSupported(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            Should.Throw<NotSupportedException>(() =>
            {
                (uut as IDictionary).Clear();
            });

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        #endregion IDictionary.Clear() Tests

        #region IDictionary.Contains() Tests

        [TestCaseSource(nameof(ValidKeyTestCaseData))]
        public void IDictionary_Legacy_Contains_KeyExists_ReturnsTrue(Dictionary<int, int> dictionary, int key)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            (uut as IDictionary).Contains(key).ShouldBeTrue();
        }

        [TestCaseSource(nameof(InvalidKeyTestCaseData))]
        public void IDictionary_Legacy_Contains_KeyDoesNotExist_ReturnsFalse(Dictionary<int, int> dictionary, int key)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            (uut as IDictionary).Contains(key).ShouldBeFalse();
        }

        #endregion IDictionary.Contains() Tests

        #region IDictionary.GetEnumerator() Tests

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void IDictionary_Legacy_GetEnumerator_Always_EnumeratesDictionary(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            var result = (uut as IDictionary).GetEnumerator();

            var enumeration = new List<KeyValuePair<int, int>>();
            while(result.MoveNext())
                enumeration.Add(new KeyValuePair<int, int>((int)result.Key, (int)result.Value));

            enumeration.ShouldBe(dictionary, ignoreOrder: true);
        }

        #endregion IDictionary.GetEnumerator() Tests

        #region IDictionary.Remove() Tests

        [TestCaseSource(nameof(ValidKeyTestCaseData))]
        [TestCaseSource(nameof(InvalidKeyTestCaseData))]
        public void IDictionary_Legacy_Remove_Always_ThrowsNotSupported(Dictionary<int, int> dictionary, int key)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            Should.Throw<NotSupportedException>(() =>
            {
                (uut as IDictionary).Remove(key);
            });

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        #endregion IDictionary.Remove() Tests

        #region ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly Tests

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ICollection_Generic_IsReadOnly_Always_ReturnsTrue(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            (uut as ICollection<KeyValuePair<int, int>>).IsReadOnly.ShouldBeTrue();
        }

        #endregion ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly Tests

        #region ICollection<KeyValuePair<TKey, TValue>>.Add() Tests

        [TestCaseSource(nameof(ValidKeyWithMatchingValueTestCaseData))]
        [TestCaseSource(nameof(ValidKeyWithDifferentValueTestCaseData))]
        [TestCaseSource(nameof(InvalidKeyWithValueTestCaseData))]
        public void ICollection_Generic_Add_Always_ThrowsNotSupported(Dictionary<int, int> dictionary, int key, int value)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            Should.Throw<NotSupportedException>(() =>
            {
                (uut as ICollection<KeyValuePair<int, int>>).Add(new KeyValuePair<int, int>(key, value));
            });

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        #endregion ICollection<KeyValuePair<TKey, TValue>>.Add() Tests

        #region ICollection<KeyValuePair<TKey, TValue>>.Clear() Tests

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ICollection_Generic_Clear_Always_ThrowsNotSupported(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            Should.Throw<NotSupportedException>(() =>
            {
                (uut as ICollection<KeyValuePair<int, int>>).Clear();
            });

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        #endregion ICollection<KeyValuePair<TKey, TValue>>.Clear() Tests

        #region ICollection<KeyValuePair<TKey, TValue>>.Contains() Tests

        [TestCaseSource(nameof(ValidKeyWithMatchingValueTestCaseData))]
        public void ICollection_Generic_Contains_KeyValuePairExists_ReturnsTrue(Dictionary<int, int> dictionary, int key, int value)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            (uut as ICollection<KeyValuePair<int, int>>).Contains(new KeyValuePair<int, int>(key, value)).ShouldBeTrue();
        }

        [TestCaseSource(nameof(ValidKeyWithDifferentValueTestCaseData))]
        [TestCaseSource(nameof(InvalidKeyWithValueTestCaseData))]
        public void ICollection_Generic_Contains_KeyValuePairDoesNotExist_ReturnsFalse(Dictionary<int, int> dictionary, int key, int value)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            (uut as ICollection<KeyValuePair<int, int>>).Contains(new KeyValuePair<int, int>(key, value)).ShouldBeFalse();
        }

        #endregion ICollection<KeyValuePair<TKey, TValue>>.Contains() Tests

        #region ICollection<KeyValuePair<TKey, TValue>>.CopyTo() Tests

        [TestCaseSource(nameof(ValidCopyToTestCaseData))]
        public void ICollection_Generic_CopyTo_ArrayIsNull_ThrowsException(Dictionary<int, int> dictionary, int arraySize, int arrayIndex)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            Should.Throw<ArgumentNullException>(() =>
            {
                (uut as ICollection<KeyValuePair<int, int>>).CopyTo(null, arrayIndex);
            });
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ICollection_Generic_CopyTo_ArrayIndexIsNegative_ThrowsException(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            var array = new KeyValuePair<int, int>[dictionary.Count];

            Should.Throw<ArgumentOutOfRangeException>(() =>
            {
                (uut as ICollection<KeyValuePair<int, int>>).CopyTo(array, -1);
            });
        }

        [TestCaseSource(nameof(InvalidCopyToTestCaseData))]
        public void ICollection_Generic_CopyTo_ArrayBoundsAreNotValid_ThrowsException(Dictionary<int, int> dictionary, int arraySize, int arrayIndex)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            var array = new KeyValuePair<int, int>[arraySize];

            Should.Throw<ArgumentException>(() =>
            {
                (uut as ICollection<KeyValuePair<int, int>>).CopyTo(array, arrayIndex);
            });
        }

        [TestCaseSource(nameof(ValidCopyToTestCaseData))]
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

        [TestCaseSource(nameof(ValidKeyWithMatchingValueTestCaseData))]
        [TestCaseSource(nameof(ValidKeyWithDifferentValueTestCaseData))]
        [TestCaseSource(nameof(InvalidKeyWithValueTestCaseData))]
        public void ICollection_Generic_Remove_Always_ThrowsNotSupported(Dictionary<int, int> dictionary, int key, int value)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            Should.Throw<NotSupportedException>(() =>
            {
                (uut as ICollection<KeyValuePair<int, int>>).Remove(new KeyValuePair<int, int>(key, value));
            });

            uut.ShouldBe(dictionary, ignoreOrder: true);
        }

        #endregion ICollection<KeyValuePair<TKey, TValue>>.Remove() Tests

        #region ICollection.IsSynchronized Tests

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ICollection_Legacy_IsSynchronized_ReturnsTrue(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            (uut as ICollection).IsSynchronized.ShouldBeTrue();
        }

        #endregion ICollection.IsSynchronized Tests

        #region ICollection.SyncRoot Tests

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ICollection_Legacy_SyncRoot_ReturnsSelf(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            (uut as ICollection).SyncRoot.ShouldBeSameAs(uut);
        }

        #endregion ICollection.SyncRoot Tests

        #region ICollection.CopyTo() Tests

        [TestCaseSource(nameof(ValidCopyToTestCaseData))]
        public void ICollection_Legacy_CopyTo_ArrayIsNull_ThrowsException(Dictionary<int, int> dictionary, int arraySize, int arrayIndex)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            Should.Throw<ArgumentNullException>(() =>
            {
                (uut as ICollection).CopyTo(null, arrayIndex);
            });
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ICollection_Legacy_CopyTo_ArrayIndexIsNegative_ThrowsException(Dictionary<int, int> dictionary)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            var array = new KeyValuePair<int, int>[dictionary.Count];

            Should.Throw<ArgumentOutOfRangeException>(() =>
            {
                (uut as ICollection).CopyTo(array, -1);
            });
        }

        [TestCaseSource(nameof(InvalidCopyToTestCaseData))]
        public void ICollection_Legacy_CopyTo_ArrayBoundsAreNotValid_ThrowsException(Dictionary<int, int> dictionary, int arraySize, int arrayIndex)
        {
            (var keyComparer, var valueComparer, var uut) = BuildTestContext(dictionary);

            var array = new KeyValuePair<int, int>[arraySize];

            Should.Throw<ArgumentException>(() =>
            {
                (uut as ICollection).CopyTo(array, arrayIndex);
            });
        }

        [TestCaseSource(nameof(ValidCopyToTestCaseData))]
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
