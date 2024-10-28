using System;
using System.Diagnostics;

namespace Implement_Algos10
{
    class Program
    {
        static void Main()
        {
            int[] largeArr = GenerateRandomArray(100000, 1, 1000);

            MeasureSortingTime(BubbleSort, (int[])largeArr.Clone(), "Bubble Sort");
            WaitForUserInput();

            MeasureSortingTime(InsertionSort, (int[])largeArr.Clone(), "Insertion Sort");
            WaitForUserInput();

            MeasureSortingTime(MergeSort, (int[])largeArr.Clone(), "Merge Sort");
            WaitForUserInput();

            MeasureSortingTime(QuickSort, (int[])largeArr.Clone(), "Quick Sort");
            WaitForUserInput();
        }

        static int[] GenerateRandomArray(int length, int minValue, int maxValue)
        {
            Random rand = new Random();
            int[] array = new int[length];

            for (int i = 0; i < length; i++)
            {
                array[i] = rand.Next(minValue, maxValue);
            }

            return array;
        }

        static void DisplayRuntime(Stopwatch stopwatch, string sortName)
        {
            Console.WriteLine($"{sortName} took {stopwatch.ElapsedMilliseconds} ms");
        }

        static void MeasureSortingTime(Action<int[]> sortingAlgorithm, int[] array, string sortName)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            sortingAlgorithm(array);
            stopwatch.Stop();
            DisplayRuntime(stopwatch, sortName);
        }

        static void WaitForUserInput()
        {
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        static void BubbleSort(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }

        static void InsertionSort(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                int key = array[i];
                int j = i - 1;

                while (j >= 0 && array[j] > key)
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = key;
            }
        }

        static void MergeSort(int[] array)
        {
            if (array.Length <= 1)
                return;

            int mid = array.Length / 2;
            int[] left = new int[mid];
            int[] right = new int[array.Length - mid];

            Array.Copy(array, 0, left, 0, mid);
            Array.Copy(array, mid, right, 0, array.Length - mid);

            MergeSort(left);
            MergeSort(right);
            Merge(array, left, right);
        }

        static void Merge(int[] array, int[] left, int[] right)
        {
            int i = 0, j = 0, k = 0;

            while (i < left.Length && j < right.Length)
            {
                if (left[i] <= right[j])
                {
                    array[k++] = left[i++];
                }
                else
                {
                    array[k++] = right[j++];
                }
            }

            while (i < left.Length)
            {
                array[k++] = left[i++];
            }

            while (j < right.Length)
            {
                array[k++] = right[j++];
            }
        }

        static void QuickSort(int[] array)
        {
            QuickSort(array, 0, array.Length - 1);
        }

        static void QuickSort(int[] array, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = Partition(array, low, high);
                QuickSort(array, low, pivotIndex - 1);
                QuickSort(array, pivotIndex + 1, high);
            }
        }

        static int Partition(int[] array, int low, int high)
        {
            int pivot = array[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (array[j] <= pivot)
                {
                    i++;
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }

            int swapTemp = array[i + 1];
            array[i + 1] = array[high];
            array[high] = swapTemp;

            return i + 1;
        }
    }
}
