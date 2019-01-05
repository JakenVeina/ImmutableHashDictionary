using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

namespace System.Collections.Immutable.Extra.Test
{
    public partial class ImmutableHashDictionaryTests
    {
        public static readonly string[] DictionaryTestCases
            = new[]
            {
                "",
				"1=>2",
				"1=>2, 2=>3",
                "1=>2, 2=>3, 3=>4"
            };

        public static readonly (string dictionaryString, int key)[] ValidKeyTestCases
            = new[]
            {
                ("1=>2",             1),
                ("1=>2, 2=>3",       1),
                ("1=>2, 2=>3",       2),
                ("1=>2, 2=>3, 3=>4", 1),
                ("1=>2, 2=>3, 3=>4", 2),
                ("1=>2, 2=>3, 3=>4", 3)
            };

        public static readonly (string dictionaryString, int key)[] InvalidKeyTestCases
            = new[]
            {
                ("",           1),
                ("",           2),
                ("",           3),
                ("1=>2",       2),
                ("1=>2",       3),
                ("1=>2, 2=>3", 3)
            };

        public static readonly (string dictionaryString, int value)[] ValidValueTestCases
            = new[]
            {
                ("1=>2",             2),
                ("1=>2, 2=>3",       2),
                ("1=>2, 2=>3",       3),
                ("1=>2, 2=>3, 3=>4", 2),
                ("1=>2, 2=>3, 3=>4", 3),
                ("1=>2, 2=>3, 3=>4", 4)
            };

        public static readonly (string dictionaryString, int value)[] InvalidValueTestCases
            = new[]
            {
                ("",           2),
                ("",           3),
                ("",           4),
                ("1=>2",       3),
                ("1=>2",       4),
                ("1=>2, 2=>3", 4)
            };

        public static readonly (string dictionaryString, int key, int value)[] ValidKeyWithMatchingValueTestCases
            = new[]
            {
                ("1=>2",             1, 2),
                ("1=>2, 2=>3",       1, 2),
                ("1=>2, 2=>3",       2, 3),
                ("1=>2, 2=>3, 3=>4", 1, 2),
                ("1=>2, 2=>3, 3=>4", 2, 3),
                ("1=>2, 2=>3, 3=>4", 3, 4)
            };

        public static readonly (string dictionaryString, int key, int value)[] ValidKeyWithDifferentValueTestCases
            = new[]
            {
                ("1=>2",             1, 3),
                ("1=>2, 2=>3",       1, 3),
                ("1=>2, 2=>3",       2, 4),
                ("1=>2, 2=>3, 3=>4", 1, 3),
                ("1=>2, 2=>3, 3=>4", 2, 4),
                ("1=>2, 2=>3, 3=>4", 3, 5)
            };

        public static readonly (string dictionaryString, int key, int value)[] InvalidKeyWithValueTestCases
            = new[]
            {
                ("",           1, 2),
                ("",           2, 3),
                ("",           3, 4),
                ("1=>2",       2, 3),
                ("1=>2",       3, 4),
                ("1=>2, 2=>3", 3, 4)
            };

        public static readonly (string dictionaryString, int[] keys)[] EmptyKeysTestCases
            = new[]
            {
                ("",                 new int[0]),
                ("1=>2",             new int[0]),
                ("1=>2, 2=>3",       new int[0]),
                ("1=>2, 2=>3, 3=>4", new int[0])
            };

        public static readonly (string dictionaryString, int[] keys)[] AllKeysExistTestCases
            = new[]
            {
                ("1=>2",             new[] { 1 }),
                ("1=>2, 2=>3",       new[] { 1 }),
                ("1=>2, 2=>3",       new[] { 2 }),
                ("1=>2, 2=>3",       new[] { 2, 3 }),
                ("1=>2, 2=>3, 3=>4", new[] { 1 }),
                ("1=>2, 2=>3, 3=>4", new[] { 2 }),
                ("1=>2, 2=>3, 3=>4", new[] { 3 }),
                ("1=>2, 2=>3, 3=>4", new[] { 1, 2 }),
                ("1=>2, 2=>3, 3=>4", new[] { 1, 3 }),
                ("1=>2, 2=>3, 3=>4", new[] { 2, 3 }),
                ("1=>2, 2=>3, 3=>4", new[] { 1, 2, 3 })
            };

        public static readonly (string dictionaryString, int[] keys)[] SomeKeysDoNotExistTestCases
            = new[]
            {
                ("1=>2",       new[] { 1, 2 }),
                ("1=>2",       new[] { 1, 3 }),
                ("1=>2",       new[] { 1, 2, 3 }),
                ("1=>2, 2=>3", new[] { 1, 3 }),
                ("1=>2, 2=>3", new[] { 2, 3 }),
                ("1=>2, 2=>3", new[] { 1, 2, 3 })
            };

        public static readonly (string dictionaryString, int[] keys)[] AllKeysDoNotExistTestCases
            = new[]
            {
                ("",           new[] { 1 }),
                ("",           new[] { 2 }),
                ("",           new[] { 3 }),
                ("",           new[] { 1, 2 }),
                ("",           new[] { 1, 3 }),
                ("",           new[] { 2, 3 }),
                ("",           new[] { 1, 2, 3 }),
                ("1=>2",       new[] { 2 }),
                ("1=>2",       new[] { 3 }),
                ("1=>2, 2=>3", new[] { 3 })
            };

        public static readonly (string dictionaryString, string keyValuePairsString)[] EmptyKeyValuePairsTestCases
            = new[]
            {
                ("",                 ""),
                ("1=>2",             ""),
                ("1=>2, 2=>3",       ""),
                ("1=>2, 2=>3, 3=>4", "")
            };

        public static readonly (string dictionaryString, string keyValuePairsString)[] AllKeyValuePairsExistTestCases
            = new[]
            {
                ("1=>2",             "1=>2"),
                ("1=>2, 2=>3",       "1=>2"),
                ("1=>2, 2=>3",       "2=>3"),
                ("1=>2, 2=>3",       "1=>2, 2=>3"),
                ("1=>2, 2=>3, 3=>4", "1=>2"),
                ("1=>2, 2=>3, 3=>4", "2=>3"),
                ("1=>2, 2=>3, 3=>4", "3=>4"),
                ("1=>2, 2=>3, 3=>4", "1=>2, 2=>3"),
                ("1=>2, 2=>3, 3=>4", "1=>2, 3=>4"),
                ("1=>2, 2=>3, 3=>4", "2=>3, 3=>4"),
                ("1=>2, 2=>3, 3=>4", "1=>2, 2=>3, 3=>4")
            };

        public static readonly (string dictionaryString, string keyValuePairsString)[] AllKeyValuePairsInvalidTestCases
            = new[]
            {
                ("1=>2",             "1=>3"),
                ("1=>2, 2=>3",       "1=>3"),
                ("1=>2, 2=>3",       "2=>4"),
                ("1=>2, 2=>3",       "1=>3, 2=>4"),
                ("1=>2, 2=>3, 3=>4", "1=>3"),
                ("1=>2, 2=>3, 3=>4", "2=>4"),
                ("1=>2, 2=>3, 3=>4", "3=>5"),
                ("1=>2, 2=>3, 3=>4", "1=>3, 2=>4"),
                ("1=>2, 2=>3, 3=>4", "1=>3, 3=>5"),
                ("1=>2, 2=>3, 3=>4", "2=>4, 3=>5"),
                ("1=>2, 2=>3, 3=>4", "1=>3, 2=>4, 3=>5")
            };

        public static readonly (string dictionaryString, string keyValuePairsString)[] SomeKeyValuePairsInvalidTestCases
            = new[]
            {
                ("1=>2, 2=>3",       "1=>3, 2=>3"),
                ("1=>2, 2=>3",       "1=>2, 2=>4"),
                ("1=>2, 2=>3, 3=>4", "1=>3, 2=>3"),
                ("1=>2, 2=>3, 3=>4", "1=>2, 2=>4"),
                ("1=>2, 2=>3, 3=>4", "1=>3, 3=>4"),
                ("1=>2, 2=>3, 3=>4", "1=>2, 3=>5"),
                ("1=>2, 2=>3, 3=>4", "2=>4, 3=>4"),
                ("1=>2, 2=>3, 3=>4", "2=>3, 3=>5"),
                ("1=>2, 2=>3, 3=>4", "1=>3, 2=>3, 3=>4"),
                ("1=>2, 2=>3, 3=>4", "1=>3, 2=>4, 3=>4"),
                ("1=>2, 2=>3, 3=>4", "1=>3, 2=>3, 3=>5"),
                ("1=>2, 2=>3, 3=>4", "1=>2, 2=>4, 3=>4"),
                ("1=>2, 2=>3, 3=>4", "1=>2, 2=>4, 3=>5"),
                ("1=>2, 2=>3, 3=>4", "1=>2, 2=>3, 3=>5")
            };

        public static readonly (string dictionaryString, string keyValuePairsString)[] SomeKeyValuePairsDoNotExistTestCases
            = new[]
            {
                ("1=>2",       "1=>2, 2=>3"),
                ("1=>2",       "1=>2, 3=>4"),
                ("1=>2",       "1=>2, 2=>3, 3=>4"),
                ("1=>2, 2=>3", "1=>2, 3=>4"),
                ("1=>2, 2=>3", "2=>3, 3=>4"),
                ("1=>2, 2=>3", "1=>2, 2=>3, 3=>4")
            };

        public static readonly (string dictionaryString, string keyValuePairsString)[] AllKeyValuePairsDoNotExistTestCases
            = new[]
            {
                ("",           "1=>2"),
                ("",           "2=>3"),
                ("",           "3=>4"),
                ("",           "1=>2, 2=>3"),
                ("",           "1=>2, 3=>4"),
                ("",           "2=>3, 3=>4"),
                ("",           "1=>2, 2=>3, 3=>4"),
                ("1=>2",       "2=>3"),
                ("1=>2",       "3=>4"),
                ("1=>2",       "2=>3, 3=>4"),
                ("1=>2, 2=>3", "3=>4")
            };

        public static readonly (string dictionaryString, int arraySize, int arrayIndex)[] InvalidCopyToTestCases
            = new[]
            {
                ("1=>2",             0, 0),
                ("1=>2",             1, 1),
                ("1=>2",             2, 2),
                ("1=>2, 2=>3",       1, 0),
                ("1=>2, 2=>3",       2, 1),
                ("1=>2, 2=>3",       3, 2),
                ("1=>2, 2=>3, 3=>4", 2, 0),
                ("1=>2, 2=>3, 3=>4", 3, 1),
                ("1=>2, 2=>3, 3=>4", 4, 2)
            };

        public static readonly (string dictionaryString, int arraySize, int arrayIndex)[] ValidCopyToTestCases
            = new[]
            {
                ("",                 0, 0),
                ("",                 1, 0),
                ("",                 2, 0),
                ("",                 3, 0),
                ("1=>2",             1, 0),
                ("1=>2",             2, 0),
                ("1=>2",             2, 1),
                ("1=>2",             3, 0),
                ("1=>2",             3, 1),
                ("1=>2",             3, 2),
                ("1=>2, 2=>3",       2, 0),
                ("1=>2, 2=>3",       3, 0),
                ("1=>2, 2=>3",       3, 1),
                ("1=>2, 2=>3",       4, 0),
                ("1=>2, 2=>3",       4, 1),
                ("1=>2, 2=>3",       4, 2),
                ("1=>2, 2=>3, 3=>4", 3, 0),
                ("1=>2, 2=>3, 3=>4", 4, 0),
                ("1=>2, 2=>3, 3=>4", 4, 1),
                ("1=>2, 2=>3, 3=>4", 5, 0),
                ("1=>2, 2=>3, 3=>4", 5, 1),
                ("1=>2, 2=>3, 3=>4", 5, 2)
            };

        public static readonly IEnumerable<TestCaseData> DictionaryTestCaseData
            = DictionaryTestCases
                .Select(x =>
                    new TestCaseData(ParseDictionaryString(x))
                        .SetName($"{{m}}([{x}])"));

        public static readonly IEnumerable<TestCaseData> NonEmptyDictionaryTestCaseData
            = DictionaryTestCases
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x =>
                    new TestCaseData(ParseDictionaryString(x))
                        .SetName($"{{m}}([{x}])"));

        public static readonly IEnumerable<TestCaseData> ValidKeyTestCaseData
            = ValidKeyTestCases
                .Select(x =>
                    new TestCaseData(ParseDictionaryString(x.dictionaryString), x.key)
                        .SetName($"{{m}}([{x.dictionaryString}], {x.key})"));

        public static readonly IEnumerable<TestCaseData> InvalidKeyTestCaseData
            = InvalidKeyTestCases
                .Select(x =>
                    new TestCaseData(ParseDictionaryString(x.dictionaryString), x.key)
                        .SetName($"{{m}}([{x.dictionaryString}], {x.key})"));

        public static readonly IEnumerable<TestCaseData> ValidValueTestCaseData
            = ValidValueTestCases
                .Select(x =>
                    new TestCaseData(ParseDictionaryString(x.dictionaryString), x.value)
                        .SetName($"{{m}}([{x.dictionaryString}], {x.value})"));

        public static readonly IEnumerable<TestCaseData> InvalidValueTestCaseData
            = InvalidValueTestCases
                .Select(x =>
                    new TestCaseData(ParseDictionaryString(x.dictionaryString), x.value)
                        .SetName($"{{m}}([{x.dictionaryString}], {x.value})"));

        public static readonly IEnumerable<TestCaseData> ValidKeyWithMatchingValueTestCaseData
            = ValidKeyWithMatchingValueTestCases
                .Select(x =>
                    new TestCaseData(ParseDictionaryString(x.dictionaryString), x.key, x.value)
                        .SetName($"{{m}}([{x.dictionaryString}], {x.key}, {x.value})"));

        public static readonly IEnumerable<TestCaseData> ValidKeyWithDifferentValueTestCaseData
            = ValidKeyWithDifferentValueTestCases
                .Select(x =>
                    new TestCaseData(ParseDictionaryString(x.dictionaryString), x.key, x.value)
                        .SetName($"{{m}}([{x.dictionaryString}], {x.key}, {x.value})"));

        public static readonly IEnumerable<TestCaseData> InvalidKeyWithValueTestCaseData
            = InvalidKeyWithValueTestCases
                .Select(x =>
                    new TestCaseData(ParseDictionaryString(x.dictionaryString), x.key, x.value)
                        .SetName($"{{m}}([{x.dictionaryString}], {x.key}, {x.value})"));

        public static readonly IEnumerable<TestCaseData> EmptyKeysTestCaseData
            = EmptyKeysTestCases
                .Select(x =>
                    new TestCaseData(ParseDictionaryString(x.dictionaryString), x.keys)
                        .SetName($"{{m}}([{x.dictionaryString}], [{string.Join(", ", x.keys)}])"));

        public static readonly IEnumerable<TestCaseData> AllKeysExistTestCaseData
            = AllKeysExistTestCases
                .Select(x =>
                    new TestCaseData(ParseDictionaryString(x.dictionaryString), x.keys)
                        .SetName($"{{m}}([{x.dictionaryString}], [{string.Join(", ", x.keys)}])"));

        public static readonly IEnumerable<TestCaseData> SomeKeysDoNotExistTestCaseData
            = SomeKeysDoNotExistTestCases
                .Select(x =>
                    new TestCaseData(ParseDictionaryString(x.dictionaryString), x.keys)
                        .SetName($"{{m}}([{x.dictionaryString}], [{string.Join(", ", x.keys)}])"));

        public static readonly IEnumerable<TestCaseData> AllKeysDoNotExistTestCaseData
            = AllKeysDoNotExistTestCases
                .Select(x =>
                    new TestCaseData(ParseDictionaryString(x.dictionaryString), x.keys)
                        .SetName($"{{m}}([{x.dictionaryString}], [{string.Join(", ", x.keys)}])"));

        public static readonly IEnumerable<TestCaseData> EmptyKeyValuePairsTestCaseData
            = EmptyKeyValuePairsTestCases
                .Select(x =>
                    new TestCaseData(ParseDictionaryString(x.dictionaryString), ParseKeyValuePairsString(x.keyValuePairsString))
                        .SetName($"{{m}}([{x.dictionaryString}], [{x.keyValuePairsString}])"));

        public static readonly IEnumerable<TestCaseData> AllKeyValuePairsExistTestCaseData
            = AllKeyValuePairsExistTestCases
                .Select(x =>
                    new TestCaseData(ParseDictionaryString(x.dictionaryString), ParseKeyValuePairsString(x.keyValuePairsString))
                        .SetName($"{{m}}([{x.dictionaryString}], [{x.keyValuePairsString}])"));

        public static readonly IEnumerable<TestCaseData> AllKeyValuePairsInvalidTestCaseData
            = AllKeyValuePairsInvalidTestCases
                .Select(x =>
                    new TestCaseData(ParseDictionaryString(x.dictionaryString), ParseKeyValuePairsString(x.keyValuePairsString))
                        .SetName($"{{m}}([{x.dictionaryString}], [{x.keyValuePairsString}])"));

        public static readonly IEnumerable<TestCaseData> SomeKeyValuePairsInvalidTestCaseData
            = SomeKeyValuePairsInvalidTestCases
                .Select(x =>
                    new TestCaseData(ParseDictionaryString(x.dictionaryString), ParseKeyValuePairsString(x.keyValuePairsString))
                        .SetName($"{{m}}([{x.dictionaryString}], [{x.keyValuePairsString}])"));

        public static readonly IEnumerable<TestCaseData> SomeKeyValuePairsDoNotExistTestCaseData
            = SomeKeyValuePairsDoNotExistTestCases
                .Select(x =>
                    new TestCaseData(ParseDictionaryString(x.dictionaryString), ParseKeyValuePairsString(x.keyValuePairsString))
                        .SetName($"{{m}}([{x.dictionaryString}], [{x.keyValuePairsString}])"));

        public static readonly IEnumerable<TestCaseData> AllKeyValuePairsDoNotExistTestCaseData
            = AllKeyValuePairsDoNotExistTestCases
                .Select(x =>
                    new TestCaseData(ParseDictionaryString(x.dictionaryString), ParseKeyValuePairsString(x.keyValuePairsString))
                        .SetName($"{{m}}([{x.dictionaryString}], [{x.keyValuePairsString}])"));

        public static readonly IEnumerable<TestCaseData> ValidCopyToTestCaseData
            = ValidCopyToTestCases
                .Select(x =>
                    new TestCaseData(ParseDictionaryString(x.dictionaryString), x.arraySize, x.arrayIndex)
                        .SetName($"{{m}}([{x.dictionaryString}], {x.arraySize}, {x.arrayIndex})"));

        public static readonly IEnumerable<TestCaseData> InvalidCopyToTestCaseData
            = InvalidCopyToTestCases
                .Select(x =>
                    new TestCaseData(ParseDictionaryString(x.dictionaryString), x.arraySize, x.arrayIndex)
                        .SetName($"{{m}}([{x.dictionaryString}], {x.arraySize}, {x.arrayIndex})"));

        private static IEnumerable<KeyValuePair<int, int>> ParseKeyValuePairsString(string keyValuePairsString)
            => keyValuePairsString
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split("=>"))
                .Select(x => new KeyValuePair<int, int>(int.Parse(x[0]), int.Parse(x[1])));

        private static Dictionary<int, int> ParseDictionaryString(string dictionaryString)
            => ParseKeyValuePairsString(dictionaryString)
                .ToDictionary(x => x.Key, x => x.Value);
    }
}
