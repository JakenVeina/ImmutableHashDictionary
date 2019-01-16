using System.Collections.Generic;
using System.Linq;

using BenchmarkDotNet.Attributes;

namespace System.Collections.Immutable.Extra.Benchmark
{
    [MemoryDiagnoser]
    public class CreateRange
    {
        [Params(0, 1, 10, 100, 1000, 10000)]
        public int Size { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            var random = new Random(Program.RandomSeed);

            KeyValuePairs = Enumerable.Range(1, Size)
                .Select(x => new KeyValuePair<int, string>(x, x.ToString()))
                .OrderBy(x => random.Next())
                .ToArray();
        }

        [Benchmark(Baseline = true)]
        public Dictionary<int, string> Dictionary()
            => new Dictionary<int, string>(KeyValuePairs);

        [Benchmark]
        public ImmutableDictionary<int, string> ImmutableDictionary()
            => Immutable.ImmutableDictionary.CreateRange(KeyValuePairs);

        [Benchmark]
        public ImmutableHashDictionary<int, string> ImmutableHashDictionary()
            => Extra.ImmutableHashDictionary.CreateRange(KeyValuePairs!);

        private KeyValuePair<int, string>[]? KeyValuePairs;
    }
}
