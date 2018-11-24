using System;

namespace Sorting
{
    public class MergeSort<T> where T: IComparable<T>
    {
        public void Sort(T[] ary)
        {
            if(ary.Length > 1)
            {
                int middle = ary.Length / 2;
                T[] left = Copy(ary, 0, middle);
                T[] right = Copy(ary, middle, ary.Length);

                Sort(left);
                Sort(right);

                int leftIndex = 0;
                int rightIndex = 0;
                int index = 0;
                while(leftIndex < left.Length && rightIndex < right.Length)
                {
                    if(left[leftIndex].CompareTo(right[rightIndex]) < 0)
                    {
                        ary[index] = left[leftIndex];
                        ++leftIndex;
                    }
                    else
                    {
                        ary[index] = right[rightIndex];
                        ++rightIndex;
                    }
                    ++index;
                }

                while(leftIndex < left.Length)
                {
                    ary[index] = left[leftIndex];
                    leftIndex++;
                    index++;
                }

                while (rightIndex < right.Length)
                {
                    ary[index] = right[rightIndex];
                    rightIndex++;
                    index++;
                }
            }
        }

        private T[] Copy(T[] source, int start, int end)
        {
            T[] ary = new T[end - start];
            for(int s = start, i = 0; s < end; ++s, ++i)
            {
                ary[i] = source[s];
            }

            return ary;
        }
    }
}
