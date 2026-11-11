using System;
using System.Collections.Generic;

namespace PhikozzLib.DataStructure
{
    public class Heap<TElement> where TElement : IComparable<TElement>
    {
        private readonly List<TElement> _elements;
        private readonly IComparer<TElement> _comparer;

        public int Count => _elements.Count;

        public Heap(IComparer<TElement> comparer = null)
        {
            _elements = new List<TElement>();
            _comparer = comparer ?? Comparer<TElement>.Default;
        }

        public static Heap<TElement> CreateMinHeap()
        {
            return new Heap<TElement>(Comparer<TElement>.Default);
        }

        public static Heap<TElement> CreateMaxHeap()
        {
            return new Heap<TElement>(Comparer<TElement>.Create((x, y) => y.CompareTo(x)));
        }

        public void Push(TElement element)
        {
            _elements.Add(element);
            HeapifyUp(_elements.Count - 1);
        }

        public TElement Pop()
        {
            if (_elements.Count == 0)
            {
                throw new InvalidOperationException("Heap is empty.");
            }

            TElement result = _elements[0];
            _elements[0] = _elements[_elements.Count - 1];
            _elements.RemoveAt(_elements.Count - 1);
            HeapifyDown(0);
            return result;
        }

        public TElement Peek()
        {
            if (_elements.Count == 0)
            {
                throw new InvalidOperationException("Heap is empty.");
            }

            return _elements[0];
        }

        public void Clear()
        {
            _elements.Clear();
        }

        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;
                if (_comparer.Compare(_elements[index], _elements[parentIndex]) >= 0)
                {
                    break;
                }

                Swap(index, parentIndex);
                index = parentIndex;
            }
        }

        private void HeapifyDown(int index)
        {
            int lastIndex = _elements.Count - 1;
            while (index <= lastIndex)
            {
                int leftChildIndex = 2 * index + 1;
                int rightChildIndex = 2 * index + 2;
                int higherPriorityIndex = index;

                if (leftChildIndex <= lastIndex && _comparer.Compare(_elements[leftChildIndex], _elements[higherPriorityIndex]) < 0)
                {
                    higherPriorityIndex = leftChildIndex;
                }

                if (rightChildIndex <= lastIndex && _comparer.Compare(_elements[rightChildIndex], _elements[higherPriorityIndex]) < 0)
                {
                    higherPriorityIndex = rightChildIndex;
                }

                if (higherPriorityIndex == index)
                {
                    break;
                }

                Swap(index, higherPriorityIndex);
                index = higherPriorityIndex;
            }
        }

        private void Swap(int indexA, int indexB)
        {
            TElement temp = _elements[indexA];
            _elements[indexA] = _elements[indexB];
            _elements[indexB] = temp;
        }
    }
}