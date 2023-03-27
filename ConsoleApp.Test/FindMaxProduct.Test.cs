using ConsoleApp.Test.Extensions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ConsoleApp.Test
{
    public class FindMaxProductTest
    {
        [Theory]
        [MemberData(nameof(FizzBuzzsTestData))]
        public void Success_Cases(string methodName, int[] input, int expectedResult)
        {
            FindMaxProduct findMaxProduct = new FindMaxProduct() { Input = input };

            findMaxProduct
                .GetType()
                .GetMethod(methodName)!.Invoke(findMaxProduct, null)
                .As<int>()
                .Should()
                .Be(expectedResult);

        }


        public static IEnumerable<object[]> FizzBuzzsTestData()
        {
            foreach (var method in typeof(FindMaxProduct).GetBenchmarkMethods())
            {
                yield return new object[] { method.Name, new[] { -2, -1 }, 2 };
                yield return new object[] { method.Name, new[] { 2, 1 }, 2 };
                yield return new object[] { method.Name, new[] { 2, 1, 2 }, 4 };
                yield return new object[] { method.Name, new[] { 2, -1, 3 }, 6 };
                yield return new object[] { method.Name, new[] { -2, -1, -3, 4, -8 }, 24 };
                yield return new object[] { method.Name, new[] { 5, 3, 2, 5, 7, 0, 1 }, 35 };
                yield return new object[] { method.Name, new[] { -20, -10, 3, 9, -8 }, 200 };
            }
        }
    }
}
