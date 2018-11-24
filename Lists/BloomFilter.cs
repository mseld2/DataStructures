using System.Collections;
using System;

namespace Lists
{
    public class BloomFilter<T>
    {
        private readonly BitArray _bitAry;

        public BloomFilter(int capacity = 64)
        {
            _bitAry = new BitArray(capacity, false);
        }

        public void Insert(T item)
        {
            Tuple<int, int, int> indexes = GetIndexes(item);
            _bitAry[indexes.Item1] = _bitAry[indexes.Item2] = _bitAry[indexes.Item3] = true;
        }

        public bool Contains(T item)
        {
            Tuple<int, int, int> indexes = GetIndexes(item);
            return _bitAry[indexes.Item1] && _bitAry[indexes.Item2] && _bitAry[indexes.Item3];
        }

        private Tuple<int, int, int> GetIndexes(T data)
        {
            return Tuple.Create(IndexOne(data), IndexTwo(data), IndexThree(data));
        }

        private int IndexOne(T data)
        {
            return Math.Abs(data.GetHashCode()) % _bitAry.Length;
        }

        private int IndexTwo(T data)
        {
            long hash = data.GetHashCode();
            long highOrder = hash & 0xf8000000;
            hash = (hash << 5) ^ (highOrder >> 27) ^ 17;

            return (int)(Math.Abs(hash) % _bitAry.Length);
        }

        private int IndexThree(T data)
        {
            long hash = data.GetHashCode();
            hash = (hash << 4) + 17;
            long g = hash & 0xf0000000;
            if(g != 0)
            {
                hash = hash ^ (g >> 24) ^ g;
            }

            return (int)(Math.Abs(hash) % _bitAry.Length);
        }
    }
}
