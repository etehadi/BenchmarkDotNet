//|                   Method |          Mean |      Error |     StdDev |   Gen0 | Allocated |
//|------------------------- |--------------:|-----------:|-----------:|-------:|----------:|
//|                 ReadList |     0.1858 ns |  0.0050 ns |  0.0047 ns |      - |         - |
//|                ReadArray |     0.0203 ns |  0.0069 ns |  0.0061 ns |      - |         - |
//|           ReadDictionary |     4.1428 ns |  0.0230 ns |  0.0215 ns |      - |         - |
//|       SearchByLinqInList | 3,879.6985 ns | 21.9390 ns | 18.3201 ns | 0.0153 |      40 B |
//|      SearchByLinqInArray | 2,699.7657 ns | 12.4742 ns | 10.4165 ns | 0.0153 |      32 B |
//| SearchByLinqInDictionary | 4,586.2108 ns |  8.9127 ns |  7.4425 ns | 0.0229 |      48 B |

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



    [Benchmark]
    public int SearchByLinqInList() => li.First(p=> p == 500);


    [Benchmark]
    public int SearchByLinqInArray() => arr.First(p => p == 500);


    [Benchmark]
    public int SearchByLinqInDictionary() => di.First(p => p.Value == 500).Value;



}