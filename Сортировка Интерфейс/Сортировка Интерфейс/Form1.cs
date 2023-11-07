using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Сортировка_Интерфейс
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static List<int> values = new List<int>();

        static char[] getLetters(string line)
        {
            char[] letters = new char[line.Length];
            int index = 0;
            foreach (char c in line)
                letters[index++] = c;
            return letters;
        }
        void getValues()
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("");
                return;
            }

            values.Clear(); 
            int a = 0;
            char[] letters = getLetters(textBox1.Text);
            for(int i=0;  i<letters.Length; i++)
            {
                if (letters[i]==' ' || letters[i]=='\n')
                {
                    values.Add(a);
                    a = 0;
                    continue;
                }
                a = a * 10 + letters[i]-'0';
            }
        }

        string getSortedValues()
        {
            textBox2.Text = "";
            string a = string.Join(" ", values);
            return a;
        }

        void getRadioButton()
        {
            if (radioButton1.Checked) bubbleSort(values.Count);
            if (radioButton2.Checked) selectionSort(values.Count);
            if (radioButton3.Checked) insertionSort(values.Count);
            if (radioButton4.Checked) quickSort(0,values.Count-1);
            if (radioButton5.Checked) mergeSort(0, values.Count-1);
            if (radioButton6.Checked) shellSort(values.Count);
            if (radioButton7.Checked) heapSort(values.Count);
        }

        
        static void swap(int a, int b)
        {
            int tmp = values[a];
            values[a] = values[b];
            values[b] = tmp;
        }

        static List<int> bubbleSort(int listLength)
        {
            while (listLength-- != 0)
            {
                bool swapped = false;

                for (int i = 0; i < listLength; i++)
                {
                    if (values[i] > values[i + 1])
                    {
                        swap(i, i + 1);
                        swapped = true;
                    }
                }

                if (swapped == false)
                    break;
            }

            return values;
        }

        int findSmallestPosition(int startPosition, int listLength)
        {
            int smallestPosition = startPosition;

            for (int i = startPosition; i < listLength; i++)
            {
                if (values[i] < values[smallestPosition])
                    smallestPosition = i;
            }
            return smallestPosition;
        }

        void selectionSort(int listLength)
        {
            for (int i = 0; i < listLength; i++)
            {
                int smallestPosition = findSmallestPosition(i, listLength);
                swap(i, smallestPosition);
            }
            return;
        }

        void insertionSort(int listLength)
        {
            for (int i = 1; i < listLength; i++)
            {
                int j = i - 1;
                while (j >= 0 && values[j] > values[j + 1])
                {
                    swap(j, j + 1);
                    j--;
                }
            }
        }

        int partition(int start, int pivot)
        {
            int i = start;
            while (i < pivot)
            {
                if (values[i] > values[pivot] && i == pivot - 1)
                {
                    swap(i, pivot);
                    pivot--;
                }

                else if (values[i] > values[pivot])
                {
                    swap(pivot - 1, pivot);
                    swap(i, pivot);
                    pivot--;
                }

                else i++;
            }
            return pivot;
        }

        void quickSort(int start, int end)
        {
            if (start < end)
            {
                int pivot = partition(start, end);

                quickSort(start, pivot - 1);
                quickSort(pivot + 1, end);
            }
        }

        void mergeSort(int start, int end)
        {
            int mid;
            if (start < end)
            {

                mid = (start + end) / 2;
                mergeSort(start, mid);
                mergeSort(mid + 1, end);
                merge(start, end, mid);
            }
        }

        void merge(int start, int end, int mid)
        {
            int[] mergedList = new int[values.Count];
            int i, j, k;
            i = start;
            k = start;
            j = mid + 1;

            while (i <= mid && j <= end)
            {
                if (values[i] < values[j])
                {
                    mergedList[k] = values[i];
                    k++;
                    i++;
                }
                else
                {
                    mergedList[k] = values[j];
                    k++;
                    j++;
                }
            }

            while (i <= mid)
            {
                mergedList[k] = values[i];
                k++;
                i++;
            }

            while (j <= end)
            {
                mergedList[k] = values[j];
                k++;
                j++;
            }

            for (i = start; i < k; i++)
            {
                values[i] = mergedList[i];
            }
        }

        void shellSort(int listLength)
        {
            for (int step = listLength / 2; step > 0; step /= 2)
            {
                for (int i = step; i < listLength; i += 1)
                {
                    int j = i;
                    while (j >= step && values[j - step] > values[i])
                    {
                        swap(j, j - step);
                        j -= step;
                    }
                }
            }
        }


        void heapify(int listLength, int root)
        {
            int largest = root;
            int l = 2 * root + 1;
            int r = 2 * root + 2;

            if (l < listLength && values[l] > values[largest])
                largest = l;

            if (r < listLength && values[r] > values[largest])
                largest = r;

            if (largest != root)
            {
                swap(root, largest);
                heapify(listLength, largest);
            }
        }

        void heapSort(int listLength)
        {
            for (int i = listLength / 2 - 1; i >= 0; i--)
                heapify(listLength, i);

            for (int i = listLength - 1; i >= 0; i--)
            {
                swap(0, i);
                heapify(i, 0);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            getValues();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            getRadioButton();
            stopwatch.Stop();

            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds);
            label3.Text = "Время работы: "+elapsedTime;

            textBox2.Text = getSortedValues();
        }
    }
}
