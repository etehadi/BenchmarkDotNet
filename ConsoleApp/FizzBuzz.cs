
using BenchmarkDotNet.Attributes;
//[SimpleJob(RuntimeMoniker.Net48, baseline: true)]
//[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser]
public class FizzBuzz
{
    const int n = 10_0;
    

    [Benchmark]
    public IList<string> Run()
    {
        List<string> result = new List<string>(n);
        for (int i = 1; i <= n; i++)
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
        List<string> result = new List<string>(n);
        for (int i = 1; i <= n; i++)
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
        string[] result = new string[n];
        for (int i = 1; i <= n; i++)
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
        Span<string> result = new Span<string>();
        for (int i = 1; i <= n; i++)
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
        List<string> result = new List<string>(n);
        for (int i = 1; i <= n; i++)
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