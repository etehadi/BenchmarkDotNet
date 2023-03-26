
using ConsoleApp.Test.Extensions;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace ConsoleApp.Test
{
    public class RevertStringTest
    {
        [Theory]
        [MemberData(nameof(GetTestData))]
        public void Success_Revert(string methodName, string input, string expectedValue)
        {
            RevertString revertString = new RevertString();
            revertString.Input = input;


            revertString
                .GetType()
                .GetMethod(methodName)!.Invoke(revertString, null)
                .As<string>()
                .Should().Be(expectedValue);
        }

        public static IEnumerable<object[]> GetTestData()
        {

            foreach (var method in typeof(RevertString).GetBenchmarkMethods())
            {
                yield return new object[] { method.Name, "Hello", "olleH" };
                yield return new object[] { method.Name, "Hello!", "!olleH" };
                yield return new object[] { method.Name, "Hi", "iH" };
                yield return new object[] { method.Name, "H", "H" };
                yield return new object[] { method.Name, "", "" };
                yield return new object[] { method.Name, "سلام", "مالس" };
            }

        }
    }
}
