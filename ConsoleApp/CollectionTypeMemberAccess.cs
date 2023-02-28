//| Method          | Mean          | Error         | StdDev        | Allocated |
//| --------------- | ----------:   | ----------:   | ----------:   | ----------:|
//| ReadList        | 0.2146 ns     | 0.0227 ns     | 0.0212 ns     | -         |
//| ReadArray       | 0.0485 ns     | 0.0059 ns     | 0.0055 ns     | -         |
//| ReadDictionary  | 4.2243 ns     | 0.0789 ns     | 0.0700 ns     | -         |

using BenchmarkDotNet.Attributes;


[MemoryDiagnoser]
public class CollectionTypeMemberAccess
{
    List<int> li = new(1000);
    Dictionary<int, int> di = new(1000);
    int[] arr = new int[1000];


    [GlobalSetup]
    public void Setup()
    {
        for (int i = 0; i < 1000; i++)
        {
            li.Add(i);
            di.Add(i, i);
            arr[i] = i;
        }
    }

    [Benchmark]
    public int ReadList() => li[500];


    [Benchmark]
    public int ReadArray() => arr[500];


    [Benchmark]
    public int ReadDictionary() => di[500];
}