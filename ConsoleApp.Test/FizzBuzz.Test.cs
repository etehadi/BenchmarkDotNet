using BenchmarkDotNet.Attributes;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ConsoleApp.Test
{
    public class FizzBuzzTest
    {
        [Theory]
        [MemberData(nameof(FizzBuzzsTestData))]
        public void Success_Cases(string methodName, int n, string[] expectedResult)
        {
            FizzBuzz fizzBuzz = new FizzBuzz() { N = n };
            fizzBuzz
                .GetType()
                .GetMethod(methodName)!.Invoke(fizzBuzz, null)
                .As<IList<string>>()
                .Should()
                .BeEquivalentTo(expectedResult);
        }


        public static IEnumerable<object[]> FizzBuzzsTestData()
        {
            var methods = typeof(FizzBuzz).GetMethods().Where(m => m.GetCustomAttributes(typeof(BenchmarkAttribute), false).Length > 0);
            foreach (var method in methods)
            {
                yield return new object[] { method.Name, 01, new string[] { "1" } };
                yield return new object[] { method.Name, 03, new string[] { "1", "2", "Fizz" } };
                yield return new object[] { method.Name, 05, new string[] { "1", "2", "Fizz", "4", "Buzz" } };
                yield return new object[] { method.Name, 15, new string[] { "1", "2", "Fizz", "4", "Buzz", "Fizz", "7", "8", "Fizz", "Buzz", "11", "Fizz", "13", "14", "FizzBuzz" } };
            }
        }
    }
}