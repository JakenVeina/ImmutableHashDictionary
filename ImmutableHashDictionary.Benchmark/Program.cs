using BenchmarkDotNet.Running;

namespace System.Collections.Immutable.Extra.Benchmark
{
    public static class Program
    {
        public const int RandomSeed
            = 12345;

        static void Main(string[] args)
            => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}
