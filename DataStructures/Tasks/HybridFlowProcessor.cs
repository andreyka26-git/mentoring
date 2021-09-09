using System;
using Tasks.DoNotChange;

namespace Tasks
{
    public class HybridFlowProcessor<T> : IHybridFlowProcessor<T>
    {
        private readonly IDoublyLinkedList<T> _linkedList = new DoublyLinkedList<T>();

        public T Dequeue()
        {
            if (_linkedList.Length == 0)
                throw new InvalidOperationException();

            return _linkedList.RemoveAt(0);
        }

        public void Enqueue(T item)
        {
            _linkedList.Add(item);
        }

        public T Pop()
        {
            if (_linkedList.Length == 0)
                throw new InvalidOperationException();

            var lastIndex = _linkedList.Length - 1;
            return _linkedList.RemoveAt(lastIndex);
        }

        public void Push(T item)
        {
            _linkedList.Add(item);
        }
    }
}
