﻿using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;




//var summary = BenchmarkRunner.Run<SortService>();
//var summary = BenchmarkRunner.Run<StringService>();
//var summary = BenchmarkRunner.Run<FizzBuzz>();
//var summary = BenchmarkRunner.Run<StructureVsClass>();
//var summary = BenchmarkRunner.Run<DirectAssign>();
var summary = BenchmarkRunner.Run<CollectionTypeMemberAccess>();
