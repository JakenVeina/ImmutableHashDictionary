using System.Collections.Generic;
using System.Linq;

using BenchmarkDotNet.Attributes;

namespace System.Collections.Immutable.Extra.Benchmark
{
    [MemoryDiagnoser]
    public class Remove
    {
        [Params(1, 10, 100, 1000, 10000)]
        public int InitialSize { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            var random = new Random(Program.RandomSeed);

            InitialKeyValuePairs = Enumerable.Range(1, InitialSize)
                .Select(x => new KeyValuePair<int, string>(x, x.ToString()))
                .OrderBy(x => random.Next())
                .ToArray();

            ImmutableDictionaryUut = Immutable.ImmutableDictionary.CreateRange(InitialKeyValuePairs);
            ImmutableHashDictionaryUut = Extra.ImmutableHashDictionary.CreateRange(InitialKeyValuePairs);

            Key = (InitialSize + 1) / 2;
        }

        [IterationSetup]
        public void IterationSetup()
        {
            DictionaryUut = new Dictionary<int, string>(InitialKeyValuePairs);
        }

        [Benchmark(Baseline = true)]
        public void Dictionary()
            => DictionaryUut!.Remove(Key);

        [Benchmark]
        public ImmutableDictionary<int, string> ImmutableDictionary()
            => ImmutableDictionaryUut!.Remove(Key);

        [Benchmark]
        public ImmutableHashDictionary<int, string> ImmutableHashDictionary()
            => ImmutableHashDictionaryUut!.Remove(Key);

        private Dictionary<int, string>? DictionaryUut;

        private ImmutableDictionary<int, string>? ImmutableDictionaryUut;

        private ImmutableHashDictionary<int, string>? ImmutableHashDictionaryUut;

        private KeyValuePair<int, string>[]? InitialKeyValuePairs;

        private int Key;
    }
}
