using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinSetCovering
{
    public class DoubleLinkedList<T>
    {
        public DoubleLinkedListNode<T> Head { get; private set; }
        public DoubleLinkedListNode<T> Tail { get; private set; }

        public int Count { get; private set; }

        public DoubleLinkedList()
            : this(new T[0]) { }

        public DoubleLinkedList( IEnumerable<T> inputSet)
        {
            if (inputSet.Any())
            {
                this.Head = null;
                this.Count = 0;
                return;
            }
            IEnumerator<T> enumerator = inputSet.GetEnumerator();
            enumerator.MoveNext();

            T data = enumerator.Current;
            Head = new DoubleLinkedListNode<T>(data);

            Tail = Head;
            while (enumerator.MoveNext())
            {
                DoubleLinkedListNode<T> current = new DoubleLinkedListNode<T>(enumerator.Current, null, Tail);
                Tail = current;
            }
        }

        #region Methods
        /// <summary>
        /// Adds a new element to the end of the double linked list.
        /// </summary>
        /// <param name="data">
        /// The data to aad to the list.
        /// </param>
        public void Add(T data)
        {
            Tail = new DoubleLinkedListNode<T>(data, null, Tail);
        }

        /// <summary>
        /// Inserts a new element into the list, replacing the provided element.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="element"></param>
        public void Insert(T data, DoubleLinkedListNode<T> element)
        {
            DoubleLinkedListNode<T> newElement = new DoubleLinkedListNode<T>(data, element, element.Prev);
        }
        #endregion
    }
}
