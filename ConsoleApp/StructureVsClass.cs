
using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
public class StructureVsClass
{
    struct MyStructure
    {
        public string Name;
        public string Surname;
    }
    class MyClass
    {
        public string Name;
        public string Surname;
    }
    [Benchmark]
    public void RunStruct()
    {
        MyStructure[] objStruct = new MyStructure[1000];
        for (int i = 0; i < 1000; i++)
        {
            objStruct[i] = new MyStructure();
            objStruct[i].Name = "Sourav";
            objStruct[i].Surname = "Kayal";
        }
    }

    [Benchmark]
    public void RunClass()
    {
        MyClass[] objClass = new MyClass[1000];

        for (int i = 0; i < 1000; i++)
        {
            objClass[i] = new MyClass();
            objClass[i].Name = "Sourav";
            objClass[i].Surname = "Kayal";
        }
    }
}
