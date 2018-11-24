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

    // This uses front and back pointers. This makes it so all inserts
    // and deletions are O(1) time.
    public class LinkedList<T> where T: IEquatable<T>
    {
        private LinkedListNode<T> _front;
        private LinkedListNode<T> _back;

        public bool IsEmpty => Size == 0;
    
        public int Size { get; private set; }

        // Time is O(1)
        public void InsertFront(T data)
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

            ++Size;
        }

        // Time is O(1)
        public void InsertBack(T data)
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

            ++Size;
        }

        // Time is O(1)
        public void InsertBefore(T data, LinkedListNode<T> nextNode)
        {
            if(nextNode == null)
            {
                throw new ArgumentNullException("nextNode cannot be null");
            }

            LinkedListNode<T> newNode = new LinkedListNode<T>(data);
            if((nextNode == _front && nextNode == _back) || nextNode == _front)
            {
                InsertFront(data);
            }
            else
            {
                newNode.Next = nextNode;
                newNode.Previous = nextNode.Previous;
                nextNode.Previous.Next = newNode;
                nextNode.Previous = newNode;

                ++Size;
            }
        }

        // Time is O(1)
        public void InsertAfter(T data, LinkedListNode<T> previousNode)
        {
            if (previousNode == null)
            {
                throw new ArgumentNullException("previousNode cannot be null");
            }

            LinkedListNode<T> newNode = new LinkedListNode<T>(data);
            if ((previousNode == _front && previousNode == _back) || previousNode == _back)
            {
                InsertBack(data);
            }
            else
            {
                newNode.Previous = previousNode;
                newNode.Next = previousNode.Next;
                previousNode.Next.Previous = newNode;
                previousNode.Next = newNode;

                ++Size;
            }
        }

        // Time is O(1)
        public void RemoveFront()
        {
            if(IsEmpty)
            {
                throw new Exception("Cannot remove from empty list");
            }

            if (_front == _back)
            {
                _front = _back =  null;
            }
            else
            {
                _front.Previous.Next = null;
                _front = _front.Previous;
            }

            --Size;
        }

        // Time is O(1)
        public void RemoveBack()
        {
            if (IsEmpty)
            {
                throw new Exception("Cannot remove from empty list");
            }

            if (_front == _back)
            {
                _back = _front = null;
            }
            else
            {
                _back.Next.Previous = null;
                _back = _back.Next;
            }

            --Size;
        }

        // Time is O(1)
        public void RemoveAfter(LinkedListNode<T> nextNode)
        {
            if (nextNode == null)
            {
                throw new ArgumentNullException("nextNode cannot be null");
            }

            if((nextNode == _front && nextNode == _back) || nextNode == _front)
            {
                RemoveBack();
            }
            else
            {
                nextNode.Previous = nextNode.Previous.Previous;
                nextNode.Previous.Next = nextNode;
                --Size;
            }
        }

        // Time is O(1)
        public void RemoveBefore(LinkedListNode<T> previousNode)
        {
            if (previousNode == null)
            {
                throw new ArgumentNullException("previousNode cannot be null");
            }

            if ((previousNode == _front && previousNode == _back) || previousNode == _back)
            {
                RemoveFront();
            }
            else
            {
                previousNode.Next = previousNode.Next.Next;
                previousNode.Next.Previous = previousNode;
                --Size;
            }
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

        // This searches from the head, so worst-case time is O(n)
        public LinkedListNode<T> Find(T data)
        {
            if(!IsEmpty)
            {
                LinkedListNode<T> node = _front;
                while (node.Previous != null)
                {
                    if(node.Data.Equals(data))
                    {
                        return node;
                    }
                    node = node.Previous;
                }

                if(node.Data.Equals(data))
                {
                    return node;
                }
            }

            return null;
        }

        // Time is O(n)
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
    }
}
