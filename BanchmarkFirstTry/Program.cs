using BanchmarkFirstTry.BenchmarkTests;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Diagnostics;
using System.Text;

//BenchmarkRunner.Run<StringTest>();

//BenchmarkRunner.Run<SortTest>();

//Task task1 = Task.Run(() =>
//{
//    SortTest bouble = new SortTest();
//    Stopwatch SW = Stopwatch.StartNew();
//    bouble.BoubleSort();
//    Console.WriteLine(bouble.testData.Length);
//    SW.Stop();
//    Console.WriteLine("bouble: " + SW.ElapsedMilliseconds + "ms");

//    foreach (var el in bouble.testData.Take(5))
//    {
//        Console.WriteLine($"bouble {el}");
//    }
//});

//Task task2 = Task.Run(() =>
//{
//    SortTest cocteil = new SortTest();
//    Stopwatch SW = Stopwatch.StartNew();
//    cocteil.CocktailSort();
//    Console.WriteLine(cocteil.testData.Length);
//    SW.Stop();
//    Console.WriteLine("cocteil: " + SW.ElapsedMilliseconds + "ms");

//    foreach (var el in cocteil.testData.Take(5))
//    {
//        Console.WriteLine($"cocteil {el}");
//    }
//});

Task task3 = Task.Run(() =>
{
    SortTest merge = new SortTest();
    Stopwatch SW = Stopwatch.StartNew();
    merge.MergeSort();
    Console.WriteLine(merge.testData.Length);
    SW.Stop();
    Console.WriteLine("merge: " + SW.ElapsedMilliseconds + "ms");

    foreach (var el in merge.testData.Take(5))
    {
        Console.WriteLine($"merge {el}");
    }
});

task3.Wait();

Task task4 = Task.Run(() =>
{
    SortTest arraySort = new SortTest();
    Stopwatch SW = Stopwatch.StartNew();
    arraySort.ArraySort();
    Console.WriteLine(arraySort.testData.Length);
    SW.Stop();
    Console.WriteLine("arraySort: " + SW.ElapsedMilliseconds + "ms");

    foreach (var el in arraySort.testData.Take(5))
    {
        Console.WriteLine($"arraySort {el}");
    }
});
task4.Wait();

Task task5 = Task.Run(() =>
{

    SortTest radixSort = new SortTest();
    Stopwatch SW = Stopwatch.StartNew();
    radixSort.RadixSort();
    Console.WriteLine(radixSort.testData.Length);
    SW.Stop();
    Console.WriteLine("radixSort: " + SW.ElapsedMilliseconds + "ms");

    foreach (var el in radixSort.testData.Take(5))
    {
        Console.WriteLine($"radixSort {el}");
    }
});
task5.Wait();

// не работает
//Task task6 = Task.Run(() =>
//{

//    SortTest mergeMultiThreading = new SortTest();
//    Stopwatch SW = Stopwatch.StartNew();
//    mergeMultiThreading.MergeSortMultiThreadingStart();
//    Console.WriteLine(mergeMultiThreading.testData.Length);
//    SW.Stop();
//    Console.WriteLine("mergeMultiThreading: " + SW.ElapsedMilliseconds + "ms");

//    foreach (var el in mergeMultiThreading.testData.Take(5))
//    {
//        Console.WriteLine($"mergeMultiThreading {el}");
//    }
//});
//task6.Wait();


Console.ReadKey();
