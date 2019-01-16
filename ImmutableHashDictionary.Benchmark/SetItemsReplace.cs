using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BenchmarkDotNet.Attributes;

namespace System.Collections.Immutable.Extra.Benchmark
{
    [MemoryDiagnoser]
    public class SetItemsReplace
    {
        [Params(1, 10, 100, 1000, 10000)]
        public int InitialSize { get; set; }

        [Params(1, 10, 100, 1000, 10000)]
        public int ReplaceSize { get; set; }

        public void GlobalSetup()
        {
            var random = new Random(Program.RandomSeed);

            InitialKeyValuePairs = Enumerable.Range(1, InitialSize)
                .Select(x => new KeyValuePair<int, string>(x, x.ToString()))
                .OrderBy(x => random.Next())
                .ToArray();

            KeyValuePairs = Enumerable.Range(InitialSize + 1, ReplaceSize)
                .Select(x => new KeyValuePair<int, string>(x, x.ToString()))
                .OrderBy(x => random.Next())
                .ToArray();
        }

        private KeyValuePair<int, string>[]? InitialKeyValuePairs;

        private KeyValuePair<int, string>[]? KeyValuePairs;

        [GlobalSetup(Target = nameof(Dictionary))]
        public void DictionarySetup()
        {
            GlobalSetup();

            DictionaryUut = new Dictionary<int, string>(InitialKeyValuePairs);
        }

        private Dictionary<int, string>? DictionaryUut;

        [Benchmark(Baseline = true)]
        public void Dictionary()
        {
            foreach(var keyValuePair in KeyValuePairs!)
                DictionaryUut![keyValuePair.Key] = keyValuePair.Value;
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
            => ImmutableDictionaryUut!.SetItems(KeyValuePairs!);

        [GlobalSetup(Target = nameof(ImmutableHashDictionary))]
        public void ImmutableHashDictionarySetup()
        {
            GlobalSetup();

            ImmutableHashDictionaryUut = Extra.ImmutableHashDictionary.CreateRange(InitialKeyValuePairs!);
        }

        private ImmutableHashDictionary<int, string>? ImmutableHashDictionaryUut;

        [Benchmark]
        public ImmutableHashDictionary<int, string> ImmutableHashDictionary()
            => ImmutableHashDictionaryUut!.SetItems(KeyValuePairs!);
    }
}
