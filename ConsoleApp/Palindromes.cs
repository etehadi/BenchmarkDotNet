//| Method | Mean | Error | StdDev | Gen0 | Allocated |
//| SimpleRun   | 2.051 ns | 0.0114 ns  | 0.0107 ns | -      | -     |
//| UsingLinq   | 42.150 ns | 0.2148 ns | 0.2009 ns | 0.0765 | 160 B |


using BenchmarkDotNet.Attributes;

namespace ConsoleApp
{
    [MemoryDiagnoser]
    public class Palindromes
    {
        public string Input { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            Input = new Bogus.Randomizer().ListItem<string>(
                new string[]{
                "aa",
                "abc",
                "aba",
                "abba",
                "abca",
                "abcba",
                });
        }

        [Benchmark]
        public bool SimpleRun()
        {
            for (int i = 0; i < Input.Length / 2; i++)
            {
                if (!Input[i].Equals(Input[^(i + 1)]))
                    return false;
            }
            return true;
        }

        [Benchmark]
        public bool UsingLinq()
        {
            int i = 1;
            return Input[..(Input.Length / 2)].All(p => p == Input[^(i++)]);
        }
    }
}
