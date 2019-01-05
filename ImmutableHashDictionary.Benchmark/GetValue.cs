using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace System.Collections.Immutable.Extra.Benchmark
{
    public class GetValue
    {
        [Params(1, 10, 100, 1000, 10000)]
        public int Size { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            var random = new Random(Program.RandomSeed);

            var initialKeyValuePairs = Enumerable.Range(1, Size)
                .Select(x => new KeyValuePair<int, string>(x, x.ToString()))
                .OrderBy(x => random.Next())
                .ToArray();

            DictionaryUut = new Dictionary<int, string>(initialKeyValuePairs);
            ImmutableDictionaryUut = Immutable.ImmutableDictionary.CreateRange(initialKeyValuePairs);
            ImmutableHashDictionaryUut = Extra.ImmutableHashDictionary.CreateRange(initialKeyValuePairs);

            Key = (Size + 1) / 2;
        }

        [Benchmark(Baseline = true)]
        public string Dictionary()
            => DictionaryUut![Key];

        [Benchmark]
        public string ImmutableDictionary()
            => ImmutableDictionaryUut![Key];

        [Benchmark]
        public string ImmutableHashDictionary()
            => ImmutableHashDictionaryUut![Key];

        private Dictionary<int, string>? DictionaryUut;

        private ImmutableDictionary<int, string>? ImmutableDictionaryUut;

        private ImmutableHashDictionary<int, string>? ImmutableHashDictionaryUut;

        private int Key;
    }
}
