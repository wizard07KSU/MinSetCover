using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cannon.Utilities.Standard.Collections
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
            if (!inputSet.Any())
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
        #region Add
        /// <summary>
        /// Adds a new element to the end of the double linked list.
        /// </summary>
        /// <param name="data">
        /// The data to aad to the list.
        /// </param>
        public void Add(T data)
        {
            Tail = new DoubleLinkedListNode<T>(data, null, Tail);
            if (Count == 0)
            {
                Head = Tail;
            }
            Count++;
        }

        /// <summary>
        /// Inserts a new element into the list, replacing the provided element.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="element"></param>
        public void Insert(T data, DoubleLinkedListNode<T> element)
        {
            DoubleLinkedListNode<T> newElement = new DoubleLinkedListNode<T>(data, element, element.Prev);
            Count++;
        }
        #endregion

        #region Remove
        /// <summary>
        /// Removes an element from the head of the list.
        /// </summary>
        /// <returns>
        /// Returns the data from the removed head element.
        /// </returns>
        public T RemoveHead()
        {
            if (Count == 0)
            {
                return default( T );
            }

            DoubleLinkedListNode<T> oldHead = this.Head;

            this.Head = this.Head.Next;
            if ( this.Head != null )
            {
                this.Head.Prev = null;
            }

            Count--;

            return oldHead.Data;
        }

        public T RemoveTail()
        {
            if (Count == 0)
            {
                return default( T );
            }

            DoubleLinkedListNode<T> oldTail = this.Tail;

            this.Tail = this.Tail.Prev;
            if (this.Tail != null)
            {
                this.Tail.Next = null;
            }

            Count--;

            return oldTail.Data;
        }
        #endregion

        #region Object Methods
        public override string ToString()
        {
            return $"Count: {Count}";
        }
        #endregion
        #endregion
    }
}
