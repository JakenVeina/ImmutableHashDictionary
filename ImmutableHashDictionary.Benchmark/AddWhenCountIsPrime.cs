using System.Collections.Generic;
using System.Linq;

using BenchmarkDotNet.Attributes;

namespace System.Collections.Immutable.Extra.Benchmark
{
    [MemoryDiagnoser]
    public class AddWhenCountIsPrime
    {
        [Params(0, 3, 11, 107, 1103, 10103)]
        public int InitialSize { get; set; }

        public void GlobalSetup()
        {
            var random = new Random(Program.RandomSeed);

            InitialKeyValuePairs = Enumerable.Range(1, InitialSize)
                .Select(x => new KeyValuePair<int, string>(x, x.ToString()))
                .OrderBy(x => random.Next())
                .ToArray();

            Key = InitialSize + 1;
            Value = Key.ToString();
        }

        private KeyValuePair<int, string>[]? InitialKeyValuePairs;

        private int Key;

        private string? Value;

        [GlobalSetup(Targets = new[] { nameof(DictionaryCreateRange), nameof(DictionaryWithCreateRange)})]
        public void DictionarySetup()
            => GlobalSetup();

        [Benchmark]
        public Dictionary<int, string> DictionaryCreateRange()
            => new Dictionary<int, string>(InitialKeyValuePairs);

        [Benchmark]
        public void DictionaryWithCreateRange()
            => new Dictionary<int, string>(InitialKeyValuePairs)
                .Add(Key, Value!);

        [GlobalSetup(Target = nameof(ImmutableDictionary))]
        public void ImmutableDictionarySetup()
        {
            GlobalSetup();

            ImmutableDictionaryUut = Immutable.ImmutableDictionary.CreateRange(InitialKeyValuePairs);
        }

        private ImmutableDictionary<int, string>? ImmutableDictionaryUut;

        [Benchmark]
        public ImmutableDictionary<int, string> ImmutableDictionary()
            => ImmutableDictionaryUut!.Add(Key, Value!);

        [GlobalSetup(Target = nameof(ImmutableHashDictionary))]
        public void ImmutableHashDictionarySetup()
        {
            GlobalSetup();

            ImmutableHashDictionaryUut = Extra.ImmutableHashDictionary.CreateRange(InitialKeyValuePairs!);
        }

        private ImmutableHashDictionary<int, string>? ImmutableHashDictionaryUut;

        [Benchmark]
        public ImmutableHashDictionary<int, string> ImmutableHashDictionary()
            => ImmutableHashDictionaryUut!.Add(Key, Value!);
    }
}
