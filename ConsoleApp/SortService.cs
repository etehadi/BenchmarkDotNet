//| Method          | Mean          | Error         | StdDev    | Gen0      | Gen1      | Gen2      | Allocated |
//| --------------  | -------------:| ----------:   | ----------| ------    | ---------:| ---------:| ----------|
//| ToListSort      | 2.770 ms      | 0.0032 ms     | 0.0025 ms | 121.0938  | 121.0938  | 121.0938  | 390.72 KB |
//| ArraySort       | 616.7 us      | 1.28 us       | 1.19 us   |        -  |        -  |        -  | 1 B       |
//| SelectionSort   | 3,589.579 ms  | 0.4210 ms     | 0.3515 ms | -         | -         | -         | 5.26 KB   |
//| BubbleSort      | 3,592.351 ms  | 3.1020 ms     | 2.7499 ms | -         | -         | -         | 2.39 KB   |


using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
public class SortService
{
    const int n = 100_000;
    int[] vals = new int[n];

    [GlobalSetup]
    public void GlobalSetup()
    {
        var rnd = new Random();

        for (int i = 0; i < n; i++)
        {
            vals[i] = rnd.Next(1, 100);
        }
    }

    [Benchmark]
    public void ToListSort() => vals.ToList().Sort();

    [Benchmark]
    public void ArraySort() => Array.Sort(vals);


    [Benchmark]
    public void SelectionSort()
    {
        int len = vals.Length;

        for (int i = 0; i < len - 1; i++)
        {
            int min_idx = i;

            for (int j = i + 1; j < len; j++)
            {
                if (vals[j] < vals[min_idx])
                {
                    min_idx = j;
                }
            }

            int temp = vals[min_idx];
            vals[min_idx] = vals[i];
            vals[i] = temp;
        }
    }

    [Benchmark]
    public void BubbleSort()
    {
        int len = vals.Length;

        for (int i = 0; i < len - 1; i++)
        {
            for (int j = 0; j < len - i - 1; j++)
            {
                if (vals[j] > vals[j + 1])
                {
                    int temp = vals[j];
                    vals[j] = vals[j + 1];
                    vals[j + 1] = temp;
                }
            }
        }
    }

    //static void Main(string[] args)
    //{
    //    var summary = BenchmarkRunner.Run<Program>();
    //}
}
