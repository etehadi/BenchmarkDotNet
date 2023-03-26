//|    Method |      Mean |    Error |   StdDev |   Gen0 | Allocated |
//|---------- |----------:|---------:|---------:|-------:|----------:|
//|    ByLinq | 851.01 ns | 2.994 ns | 2.654 ns | 0.5808 |    1216 B |
//| ByForLoop |  89.94 ns | 0.438 ns | 0.389 ns | 0.2142 |     448 B |


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
