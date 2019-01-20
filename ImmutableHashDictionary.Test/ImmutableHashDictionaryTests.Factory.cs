using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace System.Collections.Immutable.Extra.Test
{
    public partial class ImmutableHashDictionaryTests
    {
        #region Create() Tests

        [Test]
        public void Create_Default_Always_ReturnsEmpty()
        {
            var result = ImmutableHashDictionary.Create<int, int>();

            result.ShouldBeSameAs(ImmutableHashDictionary<int, int>.Empty);
        }

        [Test]
        public void Create_KeyComparer_KeyComparerIsNull_ResultIsEmptyAndHasCorrectComparers()
        {
            var result = ImmutableHashDictionary.Create<int, int>(null!);

            result.ShouldBeEmpty();
            result.KeyComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
            result.ValueComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
        }

        [Test]
        public void Create_KeyComparer_KeyComparerIsNotNull_ResultIsEmptyAndHasCorrectComparers()
        {
            var keyComparer = BuildFakeEqualityComparer<int>();

            var result = ImmutableHashDictionary.Create<int, int>(keyComparer);

            result.ShouldBeEmpty();
            result.KeyComparer.ShouldBeSameAs(keyComparer);
            result.ValueComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
        }

        [Test]
        public void Create_KeyComparerValueComparer_KeyComparerAndValueComparerAreNull_ResultIsEmptyAndHasCorrectComparers()
        {
            var result = ImmutableHashDictionary.Create<int, int>(null!, null!);

            result.ShouldBeEmpty();
            result.KeyComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
            result.ValueComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
        }

        [Test]
        public void Create_KeyComparerValueComparer_KeyComparerAndValueComparerAreNotNull_ResultIsEmptyAndHasCorrectComparers()
        {
            var keyComparer = BuildFakeEqualityComparer<int>();
            var valueComparer = BuildFakeEqualityComparer<int>();

            var result = ImmutableHashDictionary.Create(keyComparer, valueComparer);

            result.ShouldBeEmpty();
            result.KeyComparer.ShouldBeSameAs(keyComparer);
            result.ValueComparer.ShouldBeSameAs(valueComparer);
        }

        #endregion Create() Tests

        #region CreateRange() Tests

        [Test]
        public void CreateRange_Items_ItemsIsNull_ThrowsException()
        {
            Should.Throw<ArgumentNullException>(() =>
            {
                var result = ImmutableHashDictionary.CreateRange<int, int>(null!);
            });
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void CreateRange_Items_Otherwise_ResultMatchesItems(Dictionary<int, int> items)
        {
            var result = ImmutableHashDictionary.CreateRange(items);

            result.ShouldBe(items, ignoreOrder: true);
            result.KeyComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
            result.ValueComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
        }

        [Test]
        public void CreateRange_KeyComparerItems_ItemsIsNull_ThrowsException()
        {
            var keyComparer = BuildFakeEqualityComparer<int>();

            Should.Throw<ArgumentNullException>(() =>
            {
                var result = ImmutableHashDictionary.CreateRange<int, int>(keyComparer, null!);
            });
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void CreateRange_KeyComparerItems_KeyComparerIsNull_ResultMatchesItemsAndHasCorrectComparers(Dictionary<int, int> items)
        {
            var result = ImmutableHashDictionary.CreateRange(null!, items);

            result.ShouldBe(items, ignoreOrder: true);
            result.KeyComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
            result.ValueComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void CreateRange_KeyComparerItems_Otherwise_ResultMatchesItemsAndHasCorrectComparers(Dictionary<int, int> items)
        {
            var keyComparer = BuildFakeEqualityComparer<int>();

            var result = ImmutableHashDictionary.CreateRange(keyComparer, items);

            result.ShouldBe(items, ignoreOrder: true);
            result.KeyComparer.ShouldBeSameAs(keyComparer);
            result.ValueComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
        }

        [Test]
        public void CreateRange_KeyComparerValueComparerItems_ItemsIsNull_ThrowsException()
        {
            var keyComparer = BuildFakeEqualityComparer<int>();
            var valueComparer = BuildFakeEqualityComparer<int>();

            Should.Throw<ArgumentNullException>(() =>
            {
                var result = ImmutableHashDictionary.CreateRange(keyComparer, valueComparer, null!);
            });
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void CreateRange_KeyComparerValueComparerItems_KeyComparerAndValueComparerAreNull_ResultMatchesItemsAndHasCorrectComparers(Dictionary<int, int> items)
        {
            var result = ImmutableHashDictionary.CreateRange(null!, null!, items);

            result.ShouldBe(items, ignoreOrder: true);
            result.KeyComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
            result.ValueComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void CreateRange_KeyComparerValueComparerItems_Otherwise_ResultMatchesItemsAndHasCorrectComparers(Dictionary<int, int> items)
        {
            var keyComparer = BuildFakeEqualityComparer<int>();
            var valueComparer = BuildFakeEqualityComparer<int>();

            var result = ImmutableHashDictionary.CreateRange(keyComparer, valueComparer, items);

            result.ShouldBe(items, ignoreOrder: true);
            result.KeyComparer.ShouldBeSameAs(keyComparer);
            result.ValueComparer.ShouldBeSameAs(valueComparer);
        }

        #endregion CreateRange() Tests

        #region CreateBuilder() Tests

        [Test]
        public void CreateBuilder_Default_Always_ResultIsEmptyAndHasCorrectComparers()
        {
            var result = ImmutableHashDictionary.CreateBuilder<int, int>();

            result.ShouldBeEmpty();
            result.KeyComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
            result.ValueComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
        }

        [Test]
        public void CreateBuilder_KeyComparer_KeyComparerIsNull_ResultIsEmptyAndHasCorrectComparers()
        {
            var result = ImmutableHashDictionary.CreateBuilder<int, int>(null!);

            result.ShouldBeEmpty();
            result.KeyComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
            result.ValueComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
        }

        [Test]
        public void CreateBuilder_KeyComparer_KeyComparerIsNotNull_ResultIsEmptyAndHasCorrectComparers()
        {
            var keyComparer = BuildFakeEqualityComparer<int>();

            var result = ImmutableHashDictionary.CreateBuilder<int, int>(keyComparer);

            result.ShouldBeEmpty();
            result.KeyComparer.ShouldBeSameAs(keyComparer);
            result.ValueComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
        }

        [Test]
        public void CreateBuilder_KeyComparerValueComparer_KeyComparerAndValueComparerAreNull_ResultIsEmptyAndHasCorrectComparers()
        {
            var result = ImmutableHashDictionary.CreateBuilder<int, int>(null!, null!);

            result.ShouldBeEmpty();
            result.KeyComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
            result.ValueComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
        }

        [Test]
        public void CreateBuilder_KeyComparerValueComparer_Otherwise_ResultIsEmptyAndHasCorrectComparers()
        {
            var keyComparer = BuildFakeEqualityComparer<int>();
            var valueComparer = BuildFakeEqualityComparer<int>();

            var result = ImmutableHashDictionary.CreateBuilder(keyComparer, valueComparer);

            result.ShouldBeEmpty();
            result.KeyComparer.ShouldBeSameAs(keyComparer);
            result.ValueComparer.ShouldBeSameAs(valueComparer);
        }

        #endregion CreateBuilder() Tests

        #region ToImmutableHashDictionary() Tests

        [Test]
        public void ToImmutableHashDictionary_SourceKeySelectorValueSelector_SourceIsNull_ThrowsException()
        {
            var source = null as IEnumerable<KeyValuePair<int, int>>;
            var keySelector = new Func<KeyValuePair<int, int>, int>(x => x.Key);
            var valueSelector = new Func<KeyValuePair<int, int>, int>(x => x.Value);

            Should.Throw<ArgumentNullException>(() =>
            {
                var result = source.ToImmutableHashDictionary(keySelector, valueSelector);
            });
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ToImmutableHashDictionary_SourceKeySelectorValueSelector_KeySelectorIsNull_ThrowsException(Dictionary<int, int> source)
        {
            var keySelector = null as Func<KeyValuePair<int, int>, int>;
            var valueSelector = new Func<KeyValuePair<int, int>, int>(x => x.Value);

            Should.Throw<ArgumentNullException>(() =>
            {
                var result = source.ToImmutableHashDictionary(keySelector, valueSelector);
            });
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ToImmutableHashDictionary_SourceKeySelectorValueSelector_ValueSelectorIsNull_ThrowsException(Dictionary<int, int> source)
        {
            var keySelector = new Func<KeyValuePair<int, int>, int>(x => x.Key);
            var valueSelector = null as Func<KeyValuePair<int, int>, int>;

            Should.Throw<ArgumentNullException>(() =>
            {
                var result = source.ToImmutableHashDictionary(keySelector, valueSelector);
            });
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ToImmutableHashDictionary_SourceKeySelectorValueSelector_Otherwise_ResultMatchesSourceAndSelectorsAndHasCorrectComparers(Dictionary<int, int> source)
        {
            var keySelector = new Func<KeyValuePair<int, int>, int>(x => x.Key);
            var valueSelector = new Func<KeyValuePair<int, int>, int>(x => x.Value);

            var result = source.ToImmutableHashDictionary(keySelector, valueSelector);

            result.ShouldBe(source, ignoreOrder: true);
            result.KeyComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
            result.ValueComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
        }

        [Test]
        public void ToImmutableHashDictionary_SourceKeySelectorValueSelectorKeyComparer_SourceIsNull_ThrowsException()
        {
            var source = null as IEnumerable<KeyValuePair<int, int>>;
            var keySelector = new Func<KeyValuePair<int, int>, int>(x => x.Key);
            var valueSelector = new Func<KeyValuePair<int, int>, int>(x => x.Value);
            var keyComparer = BuildFakeEqualityComparer<int>();

            Should.Throw<ArgumentNullException>(() =>
            {
                var result = source.ToImmutableHashDictionary(keySelector, valueSelector, keyComparer);
            });
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ToImmutableHashDictionary_SourceKeySelectorValueSelectorKeyComparer_KeySelectorIsNull_ThrowsException(Dictionary<int, int> source)
        {
            var keySelector = null as Func<KeyValuePair<int, int>, int>;
            var valueSelector = new Func<KeyValuePair<int, int>, int>(x => x.Value);
            var keyComparer = BuildFakeEqualityComparer<int>();

            Should.Throw<ArgumentNullException>(() =>
            {
                var result = source.ToImmutableHashDictionary(keySelector, valueSelector, keyComparer);
            });
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ToImmutableHashDictionary_SourceKeySelectorValueSelectorKeyComparer_ValueSelectorIsNull_ThrowsException(Dictionary<int, int> source)
        {
            var keySelector = new Func<KeyValuePair<int, int>, int>(x => x.Key);
            var valueSelector = null as Func<KeyValuePair<int, int>, int>;
            var keyComparer = BuildFakeEqualityComparer<int>();

            Should.Throw<ArgumentNullException>(() =>
            {
                var result = source.ToImmutableHashDictionary(keySelector, valueSelector, keyComparer);
            });
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ToImmutableHashDictionary_SourceKeySelectorValueSelectorKeyComparer_KeyComparerIsNull_ResultMatchesSourceAndSelectorsAndHasCorrectComparers(Dictionary<int, int> source)
        {
            var keySelector = new Func<KeyValuePair<int, int>, int>(x => x.Key);
            var valueSelector = new Func<KeyValuePair<int, int>, int>(x => x.Value);
            var keyComparer = (null as IEqualityComparer<int>)!;

            var result = source.ToImmutableHashDictionary(keySelector, valueSelector, keyComparer);

            result.ShouldBe(source, ignoreOrder: true);
            result.KeyComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
            result.ValueComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ToImmutableHashDictionary_SourceKeySelectorValueSelectorKeyComparer_Otherwise_ResultMatchesSourceAndSelectorsAndHasCorrectComparers(Dictionary<int, int> source)
        {
            var keySelector = new Func<KeyValuePair<int, int>, int>(x => x.Key);
            var valueSelector = new Func<KeyValuePair<int, int>, int>(x => x.Value);
            var keyComparer = BuildFakeEqualityComparer<int>();

            var result = source.ToImmutableHashDictionary(keySelector, valueSelector, keyComparer);

            result.ShouldBe(source, ignoreOrder: true);
            result.KeyComparer.ShouldBeSameAs(keyComparer);
            result.ValueComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
        }

        [Test]
        public void ToImmutableHashDictionary_SourceKeySelectorValueSelectorKeyComparerValueComparer_SourceIsNull_ThrowsException()
        {
            var source = null as IEnumerable<KeyValuePair<int, int>>;
            var keySelector = new Func<KeyValuePair<int, int>, int>(x => x.Key);
            var valueSelector = new Func<KeyValuePair<int, int>, int>(x => x.Value);
            var keyComparer = BuildFakeEqualityComparer<int>();
            var valueComparer = BuildFakeEqualityComparer<int>();

            Should.Throw<ArgumentNullException>(() =>
            {
                var result = source.ToImmutableHashDictionary(keySelector, valueSelector, keyComparer, valueComparer);
            });
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ToImmutableHashDictionary_SourceKeySelectorValueSelectorKeyComparerValueComparer_KeySelectorIsNull_ThrowsException(Dictionary<int, int> source)
        {
            var keySelector = null as Func<KeyValuePair<int, int>, int>;
            var valueSelector = new Func<KeyValuePair<int, int>, int>(x => x.Value);
            var keyComparer = BuildFakeEqualityComparer<int>();
            var valueComparer = BuildFakeEqualityComparer<int>();

            Should.Throw<ArgumentNullException>(() =>
            {
                var result = source.ToImmutableHashDictionary(keySelector, valueSelector, keyComparer, valueComparer);
            });
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ToImmutableHashDictionary_SourceKeySelectorValueSelectorKeyComparerValueComparer_ValueSelectorIsNull_ThrowsException(Dictionary<int, int> source)
        {
            var keySelector = new Func<KeyValuePair<int, int>, int>(x => x.Key);
            var valueSelector = null as Func<KeyValuePair<int, int>, int>;
            var keyComparer = BuildFakeEqualityComparer<int>();
            var valueComparer = BuildFakeEqualityComparer<int>();

            Should.Throw<ArgumentNullException>(() =>
            {
                var result = source.ToImmutableHashDictionary(keySelector, valueSelector, keyComparer, valueComparer);
            });
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ToImmutableHashDictionary_SourceKeySelectorValueSelectorKeyComparerValueComparer_KeyComparerAndValueComparerAreNull_ResultMatchesSourceAndSelectorsAndHasCorrectComparers(Dictionary<int, int> source)
        {
            var keySelector = new Func<KeyValuePair<int, int>, int>(x => x.Key);
            var valueSelector = new Func<KeyValuePair<int, int>, int>(x => x.Value);
            var keyComparer = (null as IEqualityComparer<int>)!;
            var valueComparer = (null as IEqualityComparer<int>)!;

            var result = source.ToImmutableHashDictionary(keySelector, valueSelector, keyComparer, valueComparer);

            result.ShouldBe(source, ignoreOrder: true);
            result.KeyComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
            result.ValueComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ToImmutableHashDictionary_SourceKeySelectorValueSelectorKeyComparerValueComparer_Otherwise_ResultMatchesSourceAndSelectorsAndHasCorrectComparers(Dictionary<int, int> source)
        {
            var keySelector = new Func<KeyValuePair<int, int>, int>(x => x.Key);
            var valueSelector = new Func<KeyValuePair<int, int>, int>(x => x.Value);
            var keyComparer = BuildFakeEqualityComparer<int>();
            var valueComparer = BuildFakeEqualityComparer<int>();

            var result = source.ToImmutableHashDictionary(keySelector, valueSelector, keyComparer, valueComparer);

            result.ShouldBe(source, ignoreOrder: true);
            result.KeyComparer.ShouldBeSameAs(keyComparer);
            result.ValueComparer.ShouldBeSameAs(valueComparer);
        }

        [Test]
        public void ToImmutableHashDictionary_ValuesKeySelector_ValuesIsNull_ThrowsException()
        {
            var values = null as IEnumerable<int>;
            var keySelector = new Func<int, int>(value => value);

            Should.Throw<ArgumentNullException>(() =>
            {
                var result = values.ToImmutableHashDictionary(keySelector);
            });
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ToImmutableHashDictionary_ValuesKeySelector_KeySelectorIsNull_ThrowsException(Dictionary<int, int> source)
        {
            var values = source.Select(x => x.Value);
            var keySelector = null as Func<int, int>;

            Should.Throw<ArgumentNullException>(() =>
            {
                var result = values.ToImmutableHashDictionary(keySelector);
            });
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ToImmutableHashDictionary_ValuesKeySelector_Otherwise_ResultMatchesValuesAndKeySelectorAndHasCorrectComparers(Dictionary<int, int> source)
        {
            var values = source.Select(x => x.Value);
            var keySelector = new Func<int, int>(value => source.First(x => x.Value == value).Key);

            var result = values.ToImmutableHashDictionary(keySelector);

            result.ShouldBe(source, ignoreOrder: true);
            result.KeyComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
            result.ValueComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
        }

        [Test]
        public void ToImmutableHashDictionary_ValuesKeySelectorKeyComparer_ValuesIsNull_ThrowsException()
        {
            var values = null as IEnumerable<int>;
            var keySelector = new Func<int, int>(value => value);
            var keyComparer = BuildFakeEqualityComparer<int>();

            Should.Throw<ArgumentNullException>(() =>
            {
                var result = values.ToImmutableHashDictionary(keySelector, keyComparer);
            });
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ToImmutableHashDictionary_ValuesKeySelectorKeyComparer_KeySelectorIsNull_ThrowsException(Dictionary<int, int> source)
        {
            var values = source.Select(x => x.Value);
            var keySelector = null as Func<int, int>;
            var keyComparer = BuildFakeEqualityComparer<int>();

            Should.Throw<ArgumentNullException>(() =>
            {
                var result = values.ToImmutableHashDictionary(keySelector, keyComparer);
            });
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ToImmutableHashDictionary_ValuesKeySelectorKeyComparer_KeyComparerIsNull_ResultMatchesValuesAndKeySelectorAndHasCorrectComparers(Dictionary<int, int> source)
        {
            var values = source.Select(x => x.Value);
            var keySelector = new Func<int, int>(value => source.First(x => x.Value == value).Key);
            var keyComparer = (null as IEqualityComparer<int>)!;

            var result = values.ToImmutableHashDictionary(keySelector, keyComparer);

            result.ShouldBe(source, ignoreOrder: true);
            result.KeyComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
            result.ValueComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ToImmutableHashDictionary_ValuesKeySelectorKeyComparer_Otherwise_ResultMatchesValuesAndKeySelectorAndHasCorrectComparers(Dictionary<int, int> source)
        {
            var values = source.Select(x => x.Value);
            var keySelector = new Func<int, int>(value => source.First(x => x.Value == value).Key);
            var keyComparer = BuildFakeEqualityComparer<int>();

            var result = values.ToImmutableHashDictionary(keySelector, keyComparer);

            result.ShouldBe(source, ignoreOrder: true);
            result.KeyComparer.ShouldBeSameAs(keyComparer);
            result.ValueComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
        }

        [Test]
        public void ToImmutableHashDictionary_ValuesKeySelectorKeyComparerValueComparer_ValuesIsNull_ThrowsException()
        {
            var values = null as IEnumerable<int>;
            var keySelector = new Func<int, int>(value => value);
            var keyComparer = BuildFakeEqualityComparer<int>();
            var valueComparer = BuildFakeEqualityComparer<int>();

            Should.Throw<ArgumentNullException>(() =>
            {
                var result = values.ToImmutableHashDictionary(keySelector, keyComparer, valueComparer);
            });
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ToImmutableHashDictionary_ValuesKeySelectorKeyComparerValueComparer_KeySelectorIsNull_ThrowsException(Dictionary<int, int> source)
        {
            var values = source.Select(x => x.Value);
            var keySelector = null as Func<int, int>;
            var keyComparer = BuildFakeEqualityComparer<int>();
            var valueComparer = BuildFakeEqualityComparer<int>();

            Should.Throw<ArgumentNullException>(() =>
            {
                var result = values.ToImmutableHashDictionary(keySelector, keyComparer, valueComparer);
            });
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ToImmutableHashDictionary_ValuesKeySelectorKeyComparerValueComparer_KeyComparerAndValueComparerAreNull_ResultMatchesValuesAndKeySelectorAndHasCorrectComparers(Dictionary<int, int> source)
        {
            var values = source.Select(x => x.Value);
            var keySelector = new Func<int, int>(value => source.First(x => x.Value == value).Key);
            var keyComparer = (null as IEqualityComparer<int>)!;
            var valueComparer = (null as IEqualityComparer<int>)!;

            var result = values.ToImmutableHashDictionary(keySelector, keyComparer, valueComparer);

            result.ShouldBe(source, ignoreOrder: true);
            result.KeyComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
            result.ValueComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ToImmutableHashDictionary_ValuesKeySelectorKeyComparerValueComparer_Otherwise_ResultMatchesValuesAndKeySelectorAndHasCorrectComparers(Dictionary<int, int> source)
        {
            var values = source.Select(x => x.Value);
            var keySelector = new Func<int, int>(value => source.First(x => x.Value == value).Key);
            var keyComparer = BuildFakeEqualityComparer<int>();
            var valueComparer = BuildFakeEqualityComparer<int>();

            var result = values.ToImmutableHashDictionary(keySelector, keyComparer, valueComparer);

            result.ShouldBe(source, ignoreOrder: true);
            result.KeyComparer.ShouldBeSameAs(keyComparer);
            result.ValueComparer.ShouldBeSameAs(valueComparer);
        }

        [Test]
        public void ToImmutableHashDictionary_Items_ItemsIsNull_ThrowsException()
        {
            var items = null as IEnumerable<KeyValuePair<int, int>>;

            Should.Throw<ArgumentNullException>(() =>
            {
                var result = items.ToImmutableHashDictionary();
            });
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ToImmutableHashDictionary_Items_Otherwise_ResultMatchesItemsAndHasCorrectComparers(Dictionary<int, int> items)
        {
            var result = items.ToImmutableHashDictionary();

            result.ShouldBe(items, ignoreOrder: true);
            result.KeyComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
            result.ValueComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
        }

        [Test]
        public void ToImmutableHashDictionary_ItemsKeyComparer_ItemsIsNull_ThrowsException()
        {
            var items = null as IEnumerable<KeyValuePair<int, int>>;
            var keyComparer = BuildFakeEqualityComparer<int>();

            Should.Throw<ArgumentNullException>(() =>
            {
                var result = items.ToImmutableHashDictionary(keyComparer);
            });
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ToImmutableHashDictionary_ItemsKeyComparer_KeyComparerIsNull_ResultMatchesItemsAndHasCorrectComparers(Dictionary<int, int> items)
        {
            var keyComparer = (null as IEqualityComparer<int>)!;

            var result = items.ToImmutableHashDictionary(keyComparer);

            result.ShouldBe(items, ignoreOrder: true);
            result.KeyComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
            result.ValueComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ToImmutableHashDictionary_ItemsKeyComparer_Otherwise_ResultMatchesItemsAndHasCorrectComparers(Dictionary<int, int> items)
        {
            var keyComparer = BuildFakeEqualityComparer<int>();

            var result = items.ToImmutableHashDictionary(keyComparer);

            result.ShouldBe(items, ignoreOrder: true);
            result.KeyComparer.ShouldBeSameAs(keyComparer);
            result.ValueComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
        }

        [Test]
        public void ToImmutableHashDictionary_ItemsKeyComparerValueComparer_ItemsIsNull_ThrowsException()
        {
            var items = null as IEnumerable<KeyValuePair<int, int>>;
            var keyComparer = BuildFakeEqualityComparer<int>();
            var valueComparer = BuildFakeEqualityComparer<int>();

            Should.Throw<ArgumentNullException>(() =>
            {
                var result = items.ToImmutableHashDictionary(keyComparer, valueComparer);
            });
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ToImmutableHashDictionary_ItemsKeyComparerValueComparer_KeyComparerAndValueComparerAreNull_ResultMatchesItemsAndHasCorrectComparers(Dictionary<int, int> items)
        {
            var keyComparer = (null as IEqualityComparer<int>)!;
            var valueComparer = (null as IEqualityComparer<int>)!;

            var result = items.ToImmutableHashDictionary(keyComparer, valueComparer);

            result.ShouldBe(items, ignoreOrder: true);
            result.KeyComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
            result.ValueComparer.ShouldBeSameAs(EqualityComparer<int>.Default);
        }

        [TestCaseSource(nameof(DictionaryTestCaseData))]
        public void ToImmutableHashDictionary_ItemsKeyComparerValueComparer_Otherwise_ResultMatchesItemsAndHasCorrectComparers(Dictionary<int, int> items)
        {
            var keyComparer = BuildFakeEqualityComparer<int>();
            var valueComparer = BuildFakeEqualityComparer<int>();

            var result = items.ToImmutableHashDictionary(keyComparer, valueComparer);

            result.ShouldBe(items, ignoreOrder: true);
            result.KeyComparer.ShouldBeSameAs(keyComparer);
            result.ValueComparer.ShouldBeSameAs(valueComparer);
        }

        #endregion ToImmutableHashDictionary() Tests
    }
}
