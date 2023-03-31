//|             Method |      Mean |    Error |   StdDev |   Gen0 | Allocated |
//|------------------- |----------:|---------:|---------:|-------:|----------:|
//|  ByReverseFunction | 751.98 ns | 8.310 ns | 7.367 ns | 0.4740 |     992 B |
//| ByReverseFunction2 |  77.50 ns | 0.226 ns | 0.188 ns | 0.2142 |     448 B |
//|          ByForLoop |  91.17 ns | 0.771 ns | 0.721 ns | 0.2142 |     448 B |


using BenchmarkDotNet.Attributes;
using Bogus;

namespace ConsoleApp
{
    [MemoryDiagnoser]
    public class RevertString
    {
        public string Input { get; set; }
        public int Size { get; set; } = 100;

        [GlobalSetup]
        public void Setup()
        {
            Input = new string(new Randomizer().Chars(count: Size));
        }

        [Benchmark]
        public string? ByReverseFunction() => new string(Input!.Reverse().ToArray());

        [Benchmark]
        public string? ByReverseFunction2()
        {
            var inputArray = Input.ToCharArray();
            Array.Reverse(inputArray);
            return new string(inputArray);
        }

        [Benchmark]
        public string? ByForLoop()
        {
            char[] result = new char[Input!.Length];

            int middleIndex = Input.Length / 2;

            if (Input.Length % 2 == 1)
                result[middleIndex] = Input[middleIndex];

            for (int i = 0; i < Input.Length / 2; i++)
            {
                result[^(i + 1)] = Input[i];
                result[i] = Input[^(i + 1)];
            }

            return new string(result);
        }
    }
}
