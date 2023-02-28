//| Method | Mean | Error | StdDev | Median | Allocated |
//| ---------------------- | ---------:| --------:| ---------:| ---------:| ----------:|
//| AssignProp | 134.2 ns | 3.83 ns | 11.30 ns | 126.8 ns | - |
//| FieldProp | 149.0 ns | 0.05 ns | 0.04 ns | 149.0 ns | - |
//| AssignPropDrivedClass | 134.5 ns | 3.49 ns | 10.28 ns | 127.7 ns | - |
//| FieldPropDrivedClass | 149.0 ns | 0.10 ns | 0.09 ns | 149.0 ns | - |



using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
public class DirectAssign
{
    public class Test
    {
        public string StringProp { get; set; }
        public string StringField;
    }


    public class TestBase
    { 
        public string StringProp { get; set; }
        public string StringField;
    }

    public class TestDrivedClass : TestBase
    { 

    }

    Test test;
    TestDrivedClass drivedTest;

    [GlobalSetup]
    public void GlobalSetup()
    { 
        test = new Test();
        drivedTest = new TestDrivedClass();
    }

    [Benchmark]
    public void AssignLocalVariable()
    {
        string name;
        for (int i = 0; i < 100; i++)
        {
            name = "Value";
        }
    }

    [Benchmark]
    public void AssignProp()
    {
        for (int i = 0; i < 100; i++)
        {
            test.StringProp = "Value";
        }
    }


    [Benchmark]
    public void FieldProp()
    {
        for (int i = 0; i < 100; i++)
        {
            test.StringField = "Value";
        }
    }

    [Benchmark]
    public void AssignPropDrivedClass()
    {
        for (int i = 0; i < 100; i++)
        {
            drivedTest.StringProp = "Value";
        }
    }


    [Benchmark]
    public void FieldPropDrivedClass()
    {
        for (int i = 0; i < 100; i++)
        {
            drivedTest.StringField = "Value";
        }
    }
}