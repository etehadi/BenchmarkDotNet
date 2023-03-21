
//| Method | Mean | Error | StdDev | Gen0 | Gen1 | Allocated |
//| ----------------- | -----------:| ---------:| ---------:| ---------:| ---------:| -----------:|
//| UsingCSVHelper      | 3,072.3 us    | 23.80 us  | 21.10 us  | 214.8438 | 58.5938    | 670.64 KB     |
//| UsingFileIO         | 961.5 us      | 6.83 us   | 6.39 us   | 270.5078 | 35.1563    | 684.5 KB      |
//| UsingFileIOAsync    | 3,052.6 us    | 29.86 us  | 27.93 us  | 363.2813 | 101.5625   | 1142.99 KB    |


using BenchmarkDotNet.Attributes;
using CsvHelper;
using System.Globalization;
using static ConsoleApp.Product;

namespace ConsoleApp
{
    [MemoryDiagnoser]
    public class ReadCSV
    {
        const string filePath = "file.csv";

        public int SampleCount { get; set; } = 2000;
        public IList<Product>? Products { private set; get; }

        [GlobalSetup]
        public void Setup()
        {
            if (File.Exists(filePath))
                File.Delete(filePath);

            Products = new List<Product>();
            for (int i = 0; i < SampleCount; i++)
            {
                Products.Add(new Product
                {
                    Category = (Categories)(i % 3),
                    ID = i,
                    Name = $"product {i}",
                    Price = i
                });
            }

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(Products);
            }

        }


        [Benchmark]
        public List<Product> UsingCSVHelper()
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                return csv.GetRecords<Product>().ToList();
        }


        [Benchmark]
        public List<Product> UsingFileIO()
        {
            List<Product> produncts = new List<Product>();
            using (var reader = new StreamReader(filePath))
            {
                string? headerLine = reader.ReadLine();
                string? currentLine;
                while ((currentLine = reader.ReadLine()) != null)
                {
                    var currentLineValues = currentLine.Split(',');
                    produncts.Add(new Product
                    {
                        Name = currentLineValues[0],
                        Category = Enum.Parse<Categories>(currentLineValues[1]),
                        ID = Convert.ToInt32(currentLineValues[2]),
                        Price = Convert.ToDecimal(currentLineValues[3])
                    });
                }
            }
            return produncts;
        }


        [Benchmark]
        public async Task<List<Product>> UsingFileIOAsync()
        {
            List<Product> produncts = new List<Product>();
            using (var reader = new StreamReader(filePath))
            {
                //
                string? headerLine = await reader.ReadLineAsync();
                string? currentLine;
                while ((currentLine = await reader.ReadLineAsync()) != null)
                {
                    produncts.Add(await ReadFromString(currentLine));
                }
            }
            return produncts;
        }

        private static Task<Product> ReadFromString(string? currentLine)
        {
            return Task.Run(() =>
                 {
                     var currentLineValues = currentLine!.Split(',');
                     return new Product
                     {
                         Name = currentLineValues[0],
                         Category = Enum.Parse<Categories>(currentLineValues[1]),
                         ID = Convert.ToInt32(currentLineValues[2]),
                         Price = Convert.ToDecimal(currentLineValues[3])
                     };
                 });
        }
    }


    public class Product
    {
        public string? Name { get; set; }
        public Categories Category { get; set; }
        public int ID { get; set; }
        public decimal Price { get; set; }

        public enum Categories
        {
            Cat1,
            Cat2,
            Cat3,
        }
    }
}
