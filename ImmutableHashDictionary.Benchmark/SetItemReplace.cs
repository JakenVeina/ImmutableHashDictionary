using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BenchmarkDotNet.Attributes;

namespace System.Collections.Immutable.Extra.Benchmark
{
    [MemoryDiagnoser]
    public class SetItemReplace
    {
        [Params(1, 10, 100, 1000, 10000)]
        public int Size { get; set; }

        public void GlobalSetup()
        {
            var random = new Random(Program.RandomSeed);

            KeyValuePairs = Enumerable.Range(1, Size)
                .Select(x => new KeyValuePair<int, string>(x, x.ToString()))
                .OrderBy(x => random.Next())
                .ToArray();

            Key = (Size + 1) / 2;
            Value = (Size + 1).ToString();
        }

        private KeyValuePair<int, string>[]? KeyValuePairs;

        private int Key;

        private string? Value;

        [GlobalSetup(Target = nameof(Dictionary))]
        public void DictionarySetup()
        {
            GlobalSetup();

            DictionaryUut = new Dictionary<int, string>(KeyValuePairs);
        }

        private Dictionary<int, string>? DictionaryUut;

        [Benchmark(Baseline = true)]
        public void Dictionary()
            => DictionaryUut![Key] = Value!;

        [GlobalSetup(Target = nameof(ImmutableDictionary))]
        public void ImmutableDictionarySetup()
        {
            GlobalSetup();

            ImmutableDictionaryUut = Immutable.ImmutableDictionary.CreateRange(KeyValuePairs);
        }

        private ImmutableDictionary<int, string>? ImmutableDictionaryUut;

        [Benchmark]
        public ImmutableDictionary<int, string> ImmutableDictionary()
            => ImmutableDictionaryUut!.SetItem(Key, Value!);

        [GlobalSetup(Target = nameof(ImmutableHashDictionary))]
        public void ImmutableHashDictionarySetup()
        {
            GlobalSetup();

            ImmutableHashDictionaryUut = Extra.ImmutableHashDictionary.CreateRange(KeyValuePairs!);
        }

        private ImmutableHashDictionary<int, string>? ImmutableHashDictionaryUut;

        [Benchmark]
        public ImmutableHashDictionary<int, string> ImmutableHashDictionary()
            => ImmutableHashDictionaryUut!.SetItem(Key, Value!);
    }
}
