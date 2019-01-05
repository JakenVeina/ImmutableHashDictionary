using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace System.Collections.Immutable.Extra.Benchmark
{
    [MemoryDiagnoser]
    public class AddRange
    {
        [Params(0, 1, 10, 100, 1000, 10000)]
        public int InitialSize { get; set; }

        [Params(0, 1, 10, 100, 1000, 10000)]
        public int AddSize { get; set; }

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

            KeyValuePairs = Enumerable.Range((InitialSize + 1), AddSize)
                .Select(x => new KeyValuePair<int, string>(x, x.ToString()))
                .OrderBy(x => random.Next())
                .ToArray();
        }

        [IterationSetup]
        public void IterationSetup()
        {
            DictionaryUut = new Dictionary<int, string>(InitialKeyValuePairs);
        }

        [Benchmark(Baseline = true)]
        public void DictionaryForeachAdd()
        {
            foreach(var keyValuePair in KeyValuePairs!)
                DictionaryUut!.Add(keyValuePair.Key, keyValuePair.Value);
        }

        [Benchmark]
        public ImmutableDictionary<int, string> ImmutableDictionary()
            => ImmutableDictionaryUut!.AddRange(KeyValuePairs);

        [Benchmark]
        public ImmutableHashDictionary<int, string> ImmutableHashDictionary()
            => ImmutableHashDictionaryUut!.AddRange(KeyValuePairs!);

        private Dictionary<int, string>? DictionaryUut;

        private ImmutableDictionary<int, string>? ImmutableDictionaryUut;

        private ImmutableHashDictionary<int, string>? ImmutableHashDictionaryUut;

        private KeyValuePair<int, string>[]? InitialKeyValuePairs;

        private KeyValuePair<int, string>[]? KeyValuePairs;
    }
}
