using BenchmarkDotNet.Attributes;


//|                    Method |       Mean |    Error |   StdDev |   Gen0 | Allocated |
//|-------------------------- |-----------:|---------:|---------:|-------:|----------:|
//|       GetMaxProductBySort |   229.0 ns |  1.10 ns |  0.98 ns |      - |         - |
//|    GetMaxProductByForLoop | 4,100.8 ns | 43.64 ns | 36.44 ns |      - |         - |
//| GetMaxProductByForLoop2_1 |   150.3 ns |  0.65 ns |  0.55 ns |      - |         - |




namespace ConsoleApp
{
    [MemoryDiagnoser]
    public class FindMaxProduct
    {
        public int[] Input { get; set; }
        public int Size { get; set; } = 100;

        [GlobalSetup]
        public void Setup()
        {
            Random random = new Random();
            Input = new int[Size];
            for (int i = 0; i < Input.Length; i++)
                Input[i] = random.Next(int.MinValue, int.MaxValue);
        }
        private void validate()
        {
            if (Input.Length < 2) throw new ArgumentException("Provide at least 2 input integer.");
        }

        [Benchmark]
        public int GetMaxProductBySort()
        {
            validate();
            if (Input.Length == 2) return Input[0] * Input[1];

            Array.Sort(Input);

            return Math.Max(Input[0] * Input[1], Input[^1] * Input[^2]);
        }

        [Benchmark]
        public int GetMaxProductByForLoop()
        {
            validate();
            if (Input.Length == 2) return Input[0] * Input[1];

            int num1 = Input[0];
            int num2 = Input[1];
            //{ -2, -1, -3, 4, -8 };

            for (int i = 1; i < Input.Length; i++)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    if (Input[i] * Input[j] > num1 * num2)
                    {
                        num1 = Input[i];
                        num2 = Input[j];
                    }
                }
            }

            return num1 * num2;
        }

        [Benchmark]
        public int GetMaxProductByForLoop2_1()
        {
            validate();
            if (Input.Length == 2) return Input[0] * Input[1];

            int max1 = Math.Max(Input[0], Input[1]);
            int max2 = Math.Min(Input[0], Input[1]);


            int min1 = Math.Min(Input[0], Input[1]);
            int min2 = Math.Max(Input[0], Input[1]);


            for (int i = 2; i < Input.Length; i++)
            {
                if (Input[i] > max1)
                {
                    max2 = max1;
                    max1 = Input[i];
                }
                else if (Input[i] > max2)
                    max2 = Input[i];

                if (Input[i] < min1)
                {
                    min2 = min1;
                    min1 = Input[i];
                }
                else if (Input[i] < min2)
                    min2 = Input[i];
            }

            return Math.Max(max1 * max2, min1 * min2);

        }
    }
}
