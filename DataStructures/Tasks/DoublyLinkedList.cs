using System;
using System.Collections;
using System.Collections.Generic;
using Tasks.DoNotChange;

namespace Tasks
{
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        private Node<T> _head;

        public int Length
        {
            get
            {
                var count = 0;
                var node = _head;
                while (node != null)
                {
                    node = node.Next;
                    count++;
                }

                return count;
            }
        }

        public void Add(T e)
        {
            if (Length == 0)
            {
                _head = new Node<T> { Value = e };
            }
            else
            {
                var lastNode = FindNodeByIndex(Length - 1);
                lastNode.Next = new Node<T> { Value = e, Prev = lastNode };
            }
        }

        public void AddAt(int index, T e)
        {
            if (index == 0)
            {
                _head = new Node<T> { Value = e, Next = _head };
                return;
            }

            var prevNode = FindNodeByIndex(index - 1);
            prevNode.Next = new Node<T> { Value = e, Next = prevNode.Next };
        }

        public T ElementAt(int index)
        {
            if (Length == 0 || index >= Length || index < 0)
            {
                throw new IndexOutOfRangeException("Index is outside the bound.");
            }

            var findIndex = 0;
            var node = _head;
            while (findIndex != index)
            {
                node = node.Next;
                findIndex++;
            }

            return node.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var list = new List<T>();
            var node = _head;
            for (int i = 0; i < Length; i++)
            {
                list.Add(node.Value);
                node = node.Next;
            }

            return list.GetEnumerator();
        }

        public void Remove(T item)
        {
            var node = _head;
            for (int i = 0; i < Length; i++)
            {
                if (node.Value.Equals(item))
                {
                    RemoveAt(i);
                    break;
                }

                node = node.Next;
            }
        }

        public T RemoveAt(int index)
        {
            if (index < 0 || index >= Length)
                throw new IndexOutOfRangeException("Index is outside the bound.");

            T value;
            if (index == 0)
            {
                value = _head.Value;
                _head = _head.Next;
                return value;
            }

            var node = FindNodeByIndex(index - 1);
            value = node.Next.Value;
            node.Next = node.Next.Next;
            return value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private Node<T> FindNodeByIndex(int index)
        {
            var node = _head;
            for (int i = 0; i < index; i++)
                node = node.Next;

            return node;
        }
    }
}
