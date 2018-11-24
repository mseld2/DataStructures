using System;

namespace Sorting
{
    public class QuickSort<T> where T: IComparable<T>
    {
        public void Sort(T[] ary)
        {
            Sort(ary, 0, ary.Length - 1);
        }

        private void Sort(T[] ary, int first, int last)
        {
            if(first < last)
            {
                int split = Partition(ary, first, last);
                Sort(ary, first, split - 1);
                Sort(ary, split + 1, last);
            }
        }

        private int Partition(T[] ary, int first, int last)
        {
            int left = first + 1;
            int right = last;
            bool done = false;

            while(!done)
            {
                while(left <= right && ary[left].CompareTo(ary[first]) <= 0)
                {
                    ++left;
                }

                while(right >= left && ary[right].CompareTo(ary[first]) >= 0)
                {
                    --right;
                }

                done = right < left;
                if(!done)
                {
                    Swap(ary, left, right);
                }
            }

            Swap(ary, first, right);

            return right;
        }

        private void Swap(T[] ary, int leftIndex, int rightIndex)
        {
            T temp = ary[leftIndex];
            ary[leftIndex] = ary[rightIndex];
            ary[rightIndex] = temp;
        }
    }
}
