
//| Method | Mean | Error | StdDev | Gen0 | Gen1 | Gen2 | Allocated |
//| -------------- | -------------:| ------------:| -------------:| ------------:| ------------:| ------------:| -------------:|
//| Builder         | 246.4 us      | 4.91 us       | 8.20 us       | 62.5000     | 62.0117     | 62.0117     | 398.27 KB    |
//| Interpolation   | 179,582.6 us  | 4,871.34 us   | 14,286.81 us  | 332666.6667 | 247666.6667 | 247666.6667 | 956300.48 KB |
//| Addition        | 84,223.4 us   | 1,631.35 us   | 3,748.30 us   | 332666.6667 | 248333.3333 | 247666.6667 | 956612.57 KB |

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

        return output.ToString();
    }
}
