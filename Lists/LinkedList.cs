using System;
using System.Text;

namespace Lists
{
    public class LinkedListNode<T> where T: IEquatable<T>
    {
        public LinkedListNode<T> Next { get; set; }
        public LinkedListNode<T> Previous { get; set; }
        public T Data { get; set; }

        public LinkedListNode(T data)
        {
            Data = data;
        }

        public LinkedListNode(T data, LinkedListNode<T> next, LinkedListNode<T> previous)
            : this(data)
        {
            Next = next;
            Previous = previous;
        }
    }

    // Double-linked list. 
    public class LinkedList<T> where T: IEquatable<T>
    {
        private LinkedListNode<T> _front;
        private LinkedListNode<T> _back;

        public bool IsEmpty => Count == 0;
    
        public int Count { get; private set; }

        public T Front => _front != null ? _front.Data : default(T);

        public T Back => _back != null ? _back.Data : default(T);

        // O(1). Add item to front of list
        public void Push(T data)
        {
            LinkedListNode<T> newNode = new LinkedListNode<T>(data);
            if (IsEmpty)
            {
                _front = _back = newNode;
            } 
            else if (_front == _back)
            {
                newNode.Previous = _back;
                _back.Next = newNode;
                _front = newNode;
            }
            else
            {
                newNode.Previous = _front;
                _front.Next = newNode;
                _front = newNode;
            }

            ++Count;
        }

        // O(1). Add item to end of list
        public void Add(T data)
        {
            LinkedListNode<T> newNode = new LinkedListNode<T>(data);
            if (IsEmpty)
            {
                _front = _back = newNode;
            }
            else if (_front == _back)
            {
                _back.Previous = newNode;
                _back = newNode;
                _back.Next = _front;
            }
            else
            {
                _back.Previous = newNode;
                newNode.Next = _back;
                _back = newNode;
            }

            ++Count;
        }

        // O(1). Remove first item from list
        public T Pop()
        {
            if(IsEmpty)
            {
                throw new Exception("Cannot remove from empty list");
            }

            T value = _front.Data;
            if (_front == _back)
            {
                _front = _back =  null;
            }
            else
            {
                _front.Previous.Next = null;
                _front = _front.Previous;
            }

            --Count;

            return value;
        }

        // O(1). Remove last item in list
        public T RemoveLast()
        {
            if (IsEmpty)
            {
                throw new Exception("Cannot remove from empty list");
            }

            T value = _back.Data;
            if (_front == _back)
            {
                _back = _front = null;
            }
            else
            {
                _back.Next.Previous = null;
                _back = _back.Next;
            }

            --Count;

            return value;
        }


        // Time is O(n)
        public void Reverse()
        {
            LinkedListNode<T> next = null;
            LinkedListNode<T> current = _front;
            while(current != null)
            {
                LinkedListNode<T> previous = current.Previous;
                current.Previous = next;
                next = current;
                current = previous;
            }

            _front = next;
        }

        // O(n). Return index of specified item (-1 if it's not found).
        public int IndexOf(T data)
        {
            if(!IsEmpty)
            {
                int index = 0;
                LinkedListNode<T> node = _front;
                while (node.Previous != null)
                {
                    if(node.Data.Equals(data))
                    {
                        return index;
                    }
                    node = node.Previous;
                    ++index;
                }

                if(node.Data.Equals(data))
                {
                    return index;
                }
            }

            return -1;
        }

        // O(n). Get or set value using indexer.
        public T this[int index]
        {
            get { return GetNodeAt(index).Data; }

            set { GetNodeAt(index).Data = value; }
        }

        // O(1) if removing from front or back, otherwise O(n)
        public void InsertAt(T data, int index)
        {
            if(index < 0 || index > Count)
            {
                throw new IndexOutOfRangeException($"{index} is out of range");
            }

            if(index == 0)
            {
                Push(data);
            }
            else if(index == Count)
            {
                Add(data);
            }
            else
            {
                var prevNode = GetNodeAt(index);
                var newNode = new LinkedListNode<T>(data);
                newNode.Previous = prevNode;
                newNode.Next = prevNode.Next;
                prevNode.Next.Previous = newNode;
                prevNode.Next = newNode;

                ++Count;
            }
        }

        // O(n). Removes first occurrence of item.
        public void Remove(T data)
        {
            int index = IndexOf(data);
            if(index < 0)
            {
                return;
            }

            RemoveAt(index);
        }

        // O(n). Removes all occurrences of item.
        public void RemoveAll(T data)
        {
            int index = IndexOf(data);
            while(index > -1)
            {
                RemoveAt(index);
                index = IndexOf(data);
            }
        }

        // O(n). Converts LinkedList to an array.
        public T[] ToArray()
        {
            T[] ary = new T[Count];
            var current = _front;
            int index = 0;
            while(current != null)
            {
                ary[index++] = current.Data;
                current = current.Previous;
            }

            return ary;
        }

        // O(n). Removes the item at the specified index.
        public T RemoveAt(int index)
        {
            if(index == 0)
            {
                return Pop();
            }

            if(index == Count - 1)
            {
                return RemoveLast();
            }

            var node = GetNodeAt(index);
            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;

            --Count;

            return node.Data;
        }

        // O(n). [1,2,3,4] for example.
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            if(!IsEmpty)
            {
                LinkedListNode<T> node = _front;
                while (node.Previous != null)
                {
                    sb.Append(node.Data);
                    sb.Append(", ");
                    node = node.Previous;
                }
                sb.Append(node.Data);
            }
            sb.Append("]");

            return sb.ToString();
        }

        // O(1) if at front or back of list, otherwise O(n). 
        // Gets node at the specified index.
        private LinkedListNode<T> GetNodeAt(int index)
        {
            if(_front == null)
            {
                throw new Exception("List is empty");
            }

            if(index < 0 || index > Count - 1)
            {
                throw new IndexOutOfRangeException($"{index} is out of range");
            }

            if(index == 0)
            {
                return _front;
            }

            if(index == Count - 1)
            {
                return _back;
            }

            var node = _front.Previous;
            for(int idx = 1; idx < index; ++idx)
            {
                node = node.Previous;
            }

            return node;
        }
    }
}
