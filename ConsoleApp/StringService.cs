
//|        Method |         Mean |       Error |      StdDev |        Gen0 |        Gen1 |        Gen2 |    Allocated |
//|-------------- |-------------:|------------:|------------:|------------:|------------:|------------:|-------------:|
//|       Builder |     217.9 us |     1.64 us |     1.45 us |     62.5000 |     62.2559 |     62.2559 |    398.27 KB |
//| Interpolation | 160,822.6 us | 3,191.62 us | 6,299.95 us | 332666.6667 | 247666.6667 | 247666.6667 | 956300.48 KB |
//|      Addition |  78,168.7 us | 1,554.72 us | 3,540.87 us | 332714.2857 | 248285.7143 | 247714.2857 | 956612.52 KB |

using BenchmarkDotNet.Attributes;
using System.Text;

[MemoryDiagnoser]
public class StringService
{
    int n = 10_000;

    [Benchmark]
    public string Builder()
    {
        StringBuilder output = new StringBuilder();

        for (int i = 0; i < n; i++)
        {
            output.Append("falcon").Append(i);
        }

        return output.ToString();
    }

    [Benchmark]
    public string Interpolation()
    {
        string output = string.Empty;

        for (int i = 0; i < n; i++)
        {
            output = $"{output}falcon{i}";
        }

        return output;
    }

    [Benchmark]
    public string Addition()
    {
        string output = string.Empty;

        for (int i = 0; i < n; i++)
        {
            output += "falcon" + i;
        }

        return output;
    }
}
