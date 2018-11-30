using System;
using System.Linq;

namespace Lists
{
    public class HashTable<TKey, TVal> where TKey: IEquatable<TKey>
    {
        private readonly LinkedList<Kvp<TKey, TVal>>[] _hashTable;

        public HashTable(int capacity = 64)
        {
            _hashTable = Enumerable.Repeat(default(LinkedList<Kvp<TKey, TVal>>), capacity).ToArray();
        }

        public void Insert(TKey key, TVal value)
        {
            int index = Index(key);
            Kvp<TKey, TVal> kvp = new Kvp<TKey, TVal>(key, value);
            if (IsEmpty(index))
            {
                _hashTable[index] = new LinkedList<Kvp<TKey, TVal>>();
                _hashTable[index].Add(kvp);
            }
            else
            {
                if(_hashTable[index].IndexOf(kvp) < 0)
                {
                    _hashTable[index].Add(kvp);
                }
            }
        }

        public TVal GetValue(TKey key)
        {
            int index = Index(key);
            if(IsEmpty(index))
            {
                return default(TVal);
            }

            Kvp<TKey, TVal> kvp = new Kvp<TKey, TVal>(key);
            int idx = _hashTable[index].IndexOf(kvp);
            if (idx > -1)
            {
                return _hashTable[index][idx].Value;
            }

            return default(TVal);
        }

        public void Remove(TKey key)
        {
            int index = Index(key);
            if (IsEmpty(index))
            {
                return;
            }

            Kvp<TKey, TVal> kvp = new Kvp<TKey, TVal>(key);
            int idx = _hashTable[index].IndexOf(kvp);
            if (idx > -1)
            {
                _hashTable[index].RemoveAt(idx);
               
                if(_hashTable[index].IsEmpty)
                {
                    _hashTable[index] = default(LinkedList<Kvp<TKey, TVal>>);
                }
            }
        }

        private int Index(TKey key)
        {
            return Math.Abs(key.GetHashCode()) % _hashTable.Length;
        }

        private bool IsEmpty(int index)
        {
            if(index < 0 && index > _hashTable.Length - 1)
            {
                throw new ArgumentException("Invalid index");
            }

            return _hashTable[index] == default(LinkedList<Kvp<TKey, TVal>>);
        }

        class Kvp<K, V>: IEquatable<Kvp<K, V>> where K: IEquatable<K>
        {
            public K Key { get; set; }
            public V Value { get; set; }

            internal Kvp(K key, V value = default(V))
            {
                Key = key;
                Value = value;
            }

            public bool Equals(Kvp<K, V> other)
            {
                if(other == default(Kvp<K, V>))
                {
                    return false;
                }

                return other.Key.Equals(Key);
            }
        }
    }
}
