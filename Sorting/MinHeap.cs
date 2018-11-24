using System;

namespace Sorting
{
    public class MinHeap
    {
        private int[] _elements;

        public MinHeap()
        {
            _elements = new int[16];
            Size = 0;
        }

        public MinHeap(int[] elements)
        {
            _elements = new int[elements.Length];
            foreach(int element in elements)
            {
                Insert(element);
            }
        }

        public bool IsEmpty => Size == 0;

        public int Capacity => _elements.Length;

        public int Size { get; private set; }

        public int Peek()
        {
            if(IsEmpty)
            {
                throw new IndexOutOfRangeException("Heap is empty");
            }

            return _elements[0];
        }

        public int Pop()
        {
            if (IsEmpty)
            {
                throw new IndexOutOfRangeException("Heap is empty");
            }

            int minimum = _elements[0];
            _elements[0] = _elements[Size - 1];
            --Size;

            RecalculateDown();

            return minimum;
        }

        public void Insert(int element)
        {
            if(Size == Capacity)
            {
                Resize();
            }

            _elements[Size++] = element;

            RecalculateUp();
        }

        public int[] Sort()
        {
            int[] sorted = new int[Size];
            int index = 0;
            while (!IsEmpty)
            {
                sorted[index++] = Pop();
            }

            return sorted;
        }

        private int RightChildIndex(int index) => 2 * index + 2;

        private int LeftChildIndex(int index) => 2 * index + 1;

        private int ParentIndex(int index) => (index - 1) / 2;

        private int RightChild(int index) => _elements[RightChildIndex(index)];

        private int LeftChild(int index) => _elements[LeftChildIndex(index)];

        private int Parent(int index) => _elements[ParentIndex(index)];

        private bool HasRightChild(int index) => RightChildIndex(index) < Size;

        private bool HasLeftChild(int index) => LeftChildIndex(index) < Size;

        private bool IsRoot(int index) => index == 0;

        private void RecalculateDown()
        {
            int parent = 0;
            while(HasLeftChild(parent))
            {
                int smallest = LeftChildIndex(parent);
                if(HasRightChild(parent) && RightChild(parent) < LeftChild(parent))
                {
                    smallest = RightChildIndex(parent);
                }

                if (_elements[parent] < _elements[smallest])
                {
                    break;
                }

                Swap(smallest, parent);
                parent = smallest;
            }
        }

        private void RecalculateUp()
        {
            int index = Size - 1;
            while(!IsRoot(index) && _elements[index] < Parent(index))
            {
                int parent = ParentIndex(index);
                Swap(parent, index);
                index = parent;
            }
        }

        private void Resize() => Array.Resize(ref _elements, Size * 2);

        private void Swap(int leftIndex, int rightIndex)
        {
            int temp = _elements[leftIndex];
            _elements[leftIndex] = _elements[rightIndex];
            _elements[rightIndex] = temp;
        }
    }
}
