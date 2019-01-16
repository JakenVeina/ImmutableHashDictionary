using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace System.Collections.Immutable.Extra.Benchmark
{
    [MemoryDiagnoser]
    public class RemoveRange
    {
        [Params(1, 10, 100, 1000, 10000)]
        public int InitialSize { get; set; }

        [Params(1, 10, 100, 1000, 10000)]
        public int RemoveSize { get; set; }

        public void GlobalSetup()
        {
            var random = new Random(Program.RandomSeed);

            InitialKeyValuePairs = Enumerable.Range(1, InitialSize)
                .Select(x => new KeyValuePair<int, string>(x, x.ToString()))
                .OrderBy(x => random.Next())
                .ToArray();

            Keys = Enumerable.Range(1, InitialSize)
                .OrderBy(x => random.Next())
                .Take(RemoveSize)
                .ToArray();
        }

        private KeyValuePair<int, string>[]? InitialKeyValuePairs;

        private int[]? Keys;

        [GlobalSetup(Targets = new[] { nameof(DictionaryCreateRange), nameof(DictionaryWithCreateRange) })]
        public void DictionarySetup()
            => GlobalSetup();

        [Benchmark]
        public Dictionary<int, string> DictionaryCreateRange()
            => new Dictionary<int, string>(InitialKeyValuePairs);

        [Benchmark]
        public void DictionaryWithCreateRange()
        {
            var uut = new Dictionary<int, string>(InitialKeyValuePairs);

            foreach (var key in Keys!)
                uut.Remove(key);
        }

        [GlobalSetup(Target = nameof(ImmutableDictionary))]
        public void ImmutableDictionarySetup()
        {
            GlobalSetup();

            ImmutableDictionaryUut = Immutable.ImmutableDictionary.CreateRange(InitialKeyValuePairs);
        }

        private ImmutableDictionary<int, string>? ImmutableDictionaryUut;

        [Benchmark]
        public ImmutableDictionary<int, string> ImmutableDictionary()
            => ImmutableDictionaryUut!.RemoveRange(Keys);

        [GlobalSetup(Target = nameof(ImmutableHashDictionary))]
        public void ImmutableHashDictionarySetup()
        {
            GlobalSetup();

            ImmutableHashDictionaryUut = Extra.ImmutableHashDictionary.CreateRange(InitialKeyValuePairs!);
        }

        private ImmutableHashDictionary<int, string>? ImmutableHashDictionaryUut;

        [Benchmark]
        public ImmutableHashDictionary<int, string> ImmutableHashDictionary()
            => ImmutableHashDictionaryUut!.RemoveRange(Keys!);

    }
}
