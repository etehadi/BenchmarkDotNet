//|                 | Mean | Error | StdDev | Ratio | RatioSD | Gen0 | Allocated | Alloc Ratio |
//| Run             | 863.8 ns | 2.87 ns | 2.68 ns | 1.19 | 0.01 | 1.1435 | 2.34 KB | 1.01 |
//| Run2            | 834.6 ns | 4.41 ns | 3.91 ns | 1.15 | 0.01 | 1.1435 | 2.34 KB | 1.01 |
//| RunArray        | 724.9 ns | 7.59 ns | 6.73 ns | 1.00 | 0.00 | 1.1282 | 2.3 KB | 1.00 |
//| RunSpan         | 849.1 ns | 6.76 ns | 5.99 ns | 1.17 | 0.01 | 1.5221 | 3.11 KB | 1.35 |
//| RunWithConst    | 862.6 ns | 6.39 ns | 5.66 ns | 1.19 | 0.02 | 1.1435 | 2.34 KB | 1.01 |

using BenchmarkDotNet.Attributes;
//[SimpleJob(RuntimeMoniker.Net48, baseline: true)]
//[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser]
public class FizzBuzz
{
    public int N { get; init; } = 10_0;


    [Benchmark]
    public IList<string> Run()
    {
        List<string> result = new List<string>(N);
        for (int i = 1; i <= N; i++)
        {
            if (i % 3 == 0 && i % 5 == 0)
            {
                result.Add("FizzBuzz");
            }
            else if (i % 3 == 0)
            {
                result.Add("Fizz");
            }
            else if (i % 5 == 0)
            {
                result.Add("Buzz");
            }
            else
            {
                result.Add(i.ToString());
            }
        }

        return result;
    }

    [Benchmark]
    public IList<string> Run2()
    {
        List<string> result = new List<string>(N);
        for (int i = 1; i <= N; i++)
        {
            if (i % 3 == 0)
            {
                if (i % 5 == 0)
                    result.Add("FizzBuzz");
                else
                    result.Add("Fizz");
            }
            else if (i % 5 == 0)
            {
                result.Add("Buzz");
            }
            else
            {
                result.Add(i.ToString());
            }
        }

        return result;
    }



    [Benchmark(Baseline = true)]
    public IList<string> RunArray()
    {
        string[] result = new string[N];
        for (int i = 1; i <= N; i++)
        {
            if (i % 3 == 0 && i % 5 == 0)
            {
                result[i - 1] = "FizzBuzz";
            }
            else if (i % 3 == 0)
            {
                result[i - 1] = "Fizz";
            }
            else if (i % 5 == 0)
            {
                result[i - 1] = "Buzz";
            }
            else
            {
                result[i - 1] = i.ToString();
            }
        }

        return result;
    }

    [Benchmark]
    public IList<string> RunSpan()
    {
        Span<string> result = new string[N];
        for (int i = 1; i <= N; i++)
        {
            if (i % 3 == 0 && i % 5 == 0)
            {
                result[i - 1] = "FizzBuzz";
            }
            else if (i % 3 == 0)
            {
                result[i - 1] = "Fizz";
            }
            else if (i % 5 == 0)
            {
                result[i - 1] = "Buzz";
            }
            else
            {
                result[i - 1] = i.ToString();
            }
        }

        return result.ToArray();
    }

    const string Fizz = "Fizz";
    const string Buzz = "Buzz";
    const string Fizz_Buzz = "FizzBuzz";

    [Benchmark]
    public IList<string> RunWithConst()
    {
        List<string> result = new List<string>(N);
        for (int i = 1; i <= N; i++)
        {
            if (i % 3 == 0 && i % 5 == 0)
            {
                result.Add(Fizz_Buzz);
            }
            else if (i % 3 == 0)
            {
                result.Add(Fizz);
            }
            else if (i % 5 == 0)
            {
                result.Add(Buzz);
            }
            else
            {
                result.Add(i.ToString());
            }
        }

        return result;
    }
}