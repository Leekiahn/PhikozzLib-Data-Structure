using System;
using System.Collections.Generic;

namespace PhikozzLib.DataStructure
{
    public class PriorityQueue<TElement, TPriority>
    {
        private readonly Heap<(TElement element, TPriority priority)> _heap;

        public int Count => _heap.Count;

        public PriorityQueue(IComparer<TPriority> priorityComparer = null)
        {
            IComparer<TPriority> comparer = priorityComparer ?? Comparer<TPriority>.Default;
            _heap = new Heap<(TElement element, TPriority priority)>(
                Comparer<(TElement element, TPriority priority)>.Create((x, y) => comparer.Compare(x.priority, y.priority))
            );
        }

        public static PriorityQueue<TElement, TPriority> CreateMinPriorityQueue()
        {
            return new PriorityQueue<TElement, TPriority>(Comparer<TPriority>.Default);
        }

        public static PriorityQueue<TElement, TPriority> CreateMaxPriorityQueue()
        {
            return new PriorityQueue<TElement, TPriority>(
                Comparer<TPriority>.Create((x, y) => Comparer<TPriority>.Default.Compare(y, x)));
        }

        public void Enqueue(TElement element, TPriority priority)
        {
            _heap.Push((element, priority));
        }

        public TElement Dequeue()
        {
            if (_heap.Count == 0)
            {
                throw new InvalidOperationException("Priority queue is empty.");
            }

            return _heap.Pop().element;
        }

        public TElement Peek()
        {
            if (_heap.Count == 0)
            {
                throw new InvalidOperationException("Priority queue is empty.");
            }

            return _heap.Peek().element;
        }

        public void Clear()
        {
            _heap.Clear();
        }
    }
}
