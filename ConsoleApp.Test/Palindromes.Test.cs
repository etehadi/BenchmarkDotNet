using ConsoleApp.Test.Extensions;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace ConsoleApp.Test
{
    public class PalindromesTest
    {
        [Theory]
        [MemberData(nameof(GetTestData))]
        public void Success_Cases(string methodName, string input, bool expectedResult)
        {
            Palindromes palindromes = new Palindromes() { Input = input };
            //palindromes.SimpleRun2(input).Should().Be(expectedResult);
            palindromes
                .GetType()
                .GetMethod(methodName)!.Invoke(palindromes,null)
                .As<bool>()
                .Should().Be(expectedResult);


        }


        public static IEnumerable<object[]> GetTestData()
        {

            foreach (var method in typeof(Palindromes).GetBenchmarkMethods())
            {
                yield return new object[] { method.Name, "aa", true };
                yield return new object[] { method.Name, "abc", false };
                yield return new object[] { method.Name, "aba", true };
                yield return new object[] { method.Name, "abba", true };
                yield return new object[] { method.Name, "abca", false };
                yield return new object[] { method.Name, "abcba", true };
            }

        }
    }
}
