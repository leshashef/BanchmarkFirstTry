using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BanchmarkFirstTry.BenchmarkTests
{
    public class SortTest
    {
        public int[] testData;
        public SortTest()
        {
            testData = Enumerable.Range(0, 1_000_000).ToArray();
            Random.Shared.Shuffle(testData);
        }
        [Benchmark]
        public void BoubleSort() //сортировка пузырьком
        {

            bool swapped;
            for (int i = 0; i < testData.Length - 1; i++)
            {
                swapped = false;
                for (int j = 0; j < testData.Length - i - 1; j++)
                {
                    if (testData[j] > testData[j + 1])
                    {
                        // Swap elements
                        int temp = testData[j];
                        testData[j] = testData[j + 1];
                        testData[j + 1] = temp;
                        swapped = true;
                    }
                }
                // Если на каком-то проходе не было обменов, массив отсортирован
                if (!swapped)
                    break;
            }

        }
        [Benchmark]
        public void CocktailSort() // шейкерная сортировка
        {
            bool swapped = true;
            int start = 0;
            int end = testData.Length;

            while (swapped)
            {
                swapped = false;

                for (int i = start; i < end - 1; ++i)
                {
                    if (testData[i] > testData[i + 1])
                    {
                        int temp = testData[i];
                        testData[i] = testData[i + 1];
                        testData[i + 1] = temp;
                        swapped = true;
                    }
                }

                if (!swapped)
                    break;

                swapped = false;
                end--;

                for (int i = end - 1; i >= start; --i)
                {
                    if (testData[i] > testData[i + 1])
                    {
                        int temp = testData[i];
                        testData[i] = testData[i + 1];
                        testData[i + 1] = temp;
                        swapped = true;
                    }
                }

                start++;
            }


        }

        [Benchmark]
        public void MergeSort()
        {
            int l = 0;
            int r = testData.Length - 1;
            int q;

            if (l < r)
            {
                q = (l + r) / 2;
                MergeSort(testData, l, q);
                MergeSort(testData, q + 1, r);
                Merge(testData, l, q, r);
            }
        }

        private void MergeSort(int[] a, int l, int r)
        {
            int q;

            if (l < r)
            {
                q = (l + r) / 2;
                MergeSort(testData, l, q);
                MergeSort(testData, q + 1, r);
                Merge(testData, l, q, r);
            }
        }

        private void Merge(int[] a, int l, int m, int r)
        {
            int i, j, k;

            int n1 = m - l + 1;
            int n2 = r - m;

            int[] left = new int[n1 + 1];
            int[] right = new int[n2 + 1];

            for (i = 0; i < n1; i++)
            {
                left[i] = a[l + i];
            }

            for (j = 1; j <= n2; j++)
            {
                right[j - 1] = a[m + j];
            }

            left[n1] = int.MaxValue;
            right[n2] = int.MaxValue;

            i = 0;
            j = 0;

            for (k = l; k <= r; k++)
            {
                if (left[i] < right[j])
                {
                    a[k] = left[i];
                    i = i + 1;
                }
                else
                {
                    a[k] = right[j];
                    j = j + 1;
                }
            }
        }

        [Benchmark]
        public void ArraySort()
        {
            Array.Sort(testData);
        }

        [Benchmark]
        public void RadixSort() // поразрядная сортировка
        {
            var maxVal = GetMaxVal(testData, testData.Length);
            for (int exponent = 1; maxVal / exponent > 0; exponent *= 10)
                CountingSort(testData, testData.Length, exponent);

        }

        private static int GetMaxVal(int[] array, int size)
        {
            var maxVal = array[0];
            for (int i = 1; i < size; i++)
                if (array[i] > maxVal)
                    maxVal = array[i];
            return maxVal;
        }

        private static void CountingSort(int[] array, int size, int exponent)
        {
            var outputArr = new int[size];
            var occurences = new int[10];
            for (int i = 0; i < 10; i++)
                occurences[i] = 0;
            for (int i = 0; i < size; i++)
                occurences[(array[i] / exponent) % 10]++;
            for (int i = 1; i < 10; i++)
                occurences[i] += occurences[i - 1];
            for (int i = size - 1; i >= 0; i--)
            {
                outputArr[occurences[(array[i] / exponent) % 10] - 1] = array[i];
                occurences[(array[i] / exponent) % 10]--;
            }
            for (int i = 0; i < size; i++)
                array[i] = outputArr[i];
        }

        public void MergeSortMultiThreadingStart() //сортировка слиянием в многопотоке
        {
            MergeSortMultiThreading(testData);
        }

        private Random rand = new Random();
        private int[] MergeSortMultiThreading(int[] array)
        {
            if (array.Length <= 1)
                return array;


            int l = 0;
            int r = array.Length - 1;

            int middle = (l + r) / 2;
            int[] derp1 = new int[middle];
            int[] derp2 = new int[array.Length - middle];

            int j = 0;
            for (int i = 0; i < middle; i++)
            {
                derp1[i] = array[j++];
            }

            for (int i = 0; j < array.Length; i++)
            {
                derp2[i] = array[j++];
            }

            Task<int[]> future1 = Task.Factory.StartNew<int[]>(() => MergeSortMultiThreading(derp1));
            Task<int[]> future2 = Task.Factory.StartNew<int[]>(() => MergeSortMultiThreading(derp2));

            derp1 = future1.Result;
            derp2 = future2.Result;
            return Task.Factory.StartNew<int[]>(() => Merge(derp1, derp2)).Result;
        }


        private int[] Merge(int[] left, int[] right)
        {
            int[] derp = new int[left.Length + right.Length];
            int i = 0, j = 0, k = 0;
            while ((i < left.Length) && (k < right.Length))
            {
                if (left[i] < right[k])
                {

                    derp[j] = left[i];
                    i++;
                }
                else
                {
                    derp[j] = right[k];
                    k++;
                }
                j++;
            }
            while (i < left.Length)
            {
                derp[j] = left[i];
                j++; i++;
            }

            while (k < right.Length)
            {
                derp[j] = right[k];
                j++; k++;
            }

            return derp;
        }
    }
}
