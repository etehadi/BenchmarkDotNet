using ConsoleApp.Test.Extensions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ConsoleApp.Test
{
    public class ReadCSVTest
    {
        [Theory]
        [MemberData(nameof(GetTestData))]
        public void Success_Cases(string methodName, int sampleCount)
        {
            ReadCSV readCsv = new() { SampleCount = sampleCount };
            readCsv.Setup();

            var methodResult = readCsv.GetType().GetMethod(methodName)!.Invoke(readCsv, null);

            List<Product> productResult = methodResult switch
            {
                List<Product> productList => productList,
                Task<List<Product>> productListTask => productListTask.Result,
                _ => throw new Exception("Invalid return value")
            };
            readCsv.Products.Should().BeEquivalentTo(productResult);
        }


        public static IEnumerable<object[]> GetTestData()
        {
            foreach (var method in typeof(ReadCSV).GetBenchmarkMethods())
            {
                yield return new object[] { method.Name, 100 };
            }
        }
    }
}
