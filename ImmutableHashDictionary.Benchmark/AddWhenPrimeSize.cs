using System.Collections.Generic;
using System.Linq;

using BenchmarkDotNet.Attributes;

namespace System.Collections.Immutable.Extra.Benchmark
{
    [MemoryDiagnoser]
    public class AddWhenPrimeSize
    {
        [Params(0, 3, 11, 107, 1103, 10103)]
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

            Key = InitialSize + 1;
            Value = Key.ToString();
        }

        [IterationSetup]
        public void IterationSetup()
        {
            DictionaryUut = new Dictionary<int, string>(InitialKeyValuePairs);
        }

        [Benchmark(Baseline = true, OperationsPerInvoke = 1)]
        public void Dictionary()
            => DictionaryUut!.Add(Key, Value!);

        [Benchmark]
        public ImmutableDictionary<int, string> ImmutableDictionary()
            => ImmutableDictionaryUut!.Add(Key, Value!);

        [Benchmark]
        public ImmutableHashDictionary<int, string> ImmutableHashDictionary()
            => ImmutableHashDictionaryUut!.Add(Key, Value!);

        private Dictionary<int, string>? DictionaryUut;

        private ImmutableDictionary<int, string>? ImmutableDictionaryUut;

        private ImmutableHashDictionary<int, string>? ImmutableHashDictionaryUut;

        private KeyValuePair<int, string>[]? InitialKeyValuePairs;

        private int Key;

        private string? Value;
    }
}
