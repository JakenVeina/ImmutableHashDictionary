using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace System.Collections.Immutable.Extra.Test
{
    public partial class ImmutableHashDictionaryTests
    {
        public static readonly string[] KeyValuePairsTestCases
            = new[]
            {
                "",
				"1=>2",
				"1=>2, 2=>3",
                "1=>2, 2=>3, 3=>4"
            };

        public static readonly (string keyValuePairsString, int key)[] ValidKeyTestCases
            = new[]
            {
                ("1=>2",             1),
                ("1=>2, 2=>3",       1),
                ("1=>2, 2=>3",       2),
                ("1=>2, 2=>3, 3=>4", 1),
                ("1=>2, 2=>3, 3=>4", 2),
                ("1=>2, 2=>3, 3=>4", 3)
            };

        public static readonly (string keyValuePairsString, int key, int value)[] ValidKeyWithMatchingValueTestCases
            = new[]
            {
                ("1=>2",             1, 2),
                ("1=>2, 2=>3",       1, 2),
                ("1=>2, 2=>3",       2, 3),
                ("1=>2, 2=>3, 3=>4", 1, 2),
                ("1=>2, 2=>3, 3=>4", 2, 3),
                ("1=>2, 2=>3, 3=>4", 3, 4)
            };

        public static readonly (string keyValuePairsString, int key, int value)[] ValidKeyWithDifferentValueTestCases
            = new[]
            {
                ("1=>2",             1, 3),
                ("1=>2, 2=>3",       1, 3),
                ("1=>2, 2=>3",       2, 4),
                ("1=>2, 2=>3, 3=>4", 1, 3),
                ("1=>2, 2=>3, 3=>4", 2, 4),
                ("1=>2, 2=>3, 3=>4", 3, 5)
            };

        public static readonly (string keyValuePairsString, int key)[] InvalidKeyTestCases
            = new[]
            {
                ("",           1),
                ("",           2),
                ("",           3),
                ("1=>2",       2),
                ("1=>2",       3),
                ("1=>2, 2=>3", 3)
            };

        public static readonly (string keyValuePairsString, int key, int value)[] InvalidKeyWithValueTestCases
            = new[]
            {
                ("",           1, 2),
                ("",           2, 3),
                ("",           3, 4),
                ("1=>2",       2, 3),
                ("1=>2",       3, 4),
                ("1=>2, 2=>3", 3, 4)
            };

        public static readonly IEnumerable<TestCaseData> DictionaryTestCaseData
            = KeyValuePairsTestCases
                .Select(x =>
                    new TestCaseData(ParseDictionaryString(x))
                        .SetName($"{{m}}([{x}])"));

        public static readonly IEnumerable<TestCaseData> NonEmptyDictionaryTestCaseData
            = KeyValuePairsTestCases
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x =>
                    new TestCaseData(ParseDictionaryString(x))
                        .SetName($"{{m}}([{x}])"));

        public static readonly IEnumerable<TestCaseData> ValidKeyTestCaseData
            = ValidKeyTestCases
                .Select(x =>
                    new TestCaseData(ParseDictionaryString(x.keyValuePairsString), x.key)
                        .SetName($"{{m}}([{x.keyValuePairsString}], {x.key})"));

        public static readonly IEnumerable<TestCaseData> ValidKeyWithMatchingValueTestCaseData
            = ValidKeyWithMatchingValueTestCases
                .Select(x =>
                    new TestCaseData(ParseDictionaryString(x.keyValuePairsString), x.key, x.value)
                        .SetName($"{{m}}([{x.keyValuePairsString}], {x.key}, {x.value})"));

        public static readonly IEnumerable<TestCaseData> ValidKeyWithDifferentValueTestCaseData
            = ValidKeyWithDifferentValueTestCases
                .Select(x =>
                    new TestCaseData(ParseDictionaryString(x.keyValuePairsString), x.key, x.value)
                        .SetName($"{{m}}([{x.keyValuePairsString}], {x.key}, {x.value})"));

        public static readonly IEnumerable<TestCaseData> InvalidKeyTestCaseData
            = InvalidKeyTestCases
                .Select(x =>
                    new TestCaseData(ParseDictionaryString(x.keyValuePairsString), x.key)
                        .SetName($"{{m}}([{x.keyValuePairsString}], {x.key})"));

        public static readonly IEnumerable<TestCaseData> InvalidKeyWithValueTestCaseData
            = InvalidKeyWithValueTestCases
                .Select(x =>
                    new TestCaseData(ParseDictionaryString(x.keyValuePairsString), x.key, x.value)
                        .SetName($"{{m}}([{x.keyValuePairsString}], {x.key}, {x.value})"));

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
