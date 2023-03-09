using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConsoleApp.Test.Extensions
{
    public static class TypeExtensions
    {
        public static IEnumerable<MethodInfo> GetBenchmarkMethods(this Type type) =>
            type.GetMethods().Where(m => m.GetCustomAttributes(typeof(BenchmarkAttribute), false).Length > 0);
    }
}
