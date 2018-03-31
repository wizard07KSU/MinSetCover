using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cannon.Utilities.Standard.Collections
{
    public class DoubleLinkedListNode<T>
    {
        public T Data { get; private set; }

        public DoubleLinkedListNode<T> Next { get; private set; }
        public DoubleLinkedListNode<T> Prev { get; private set; }

        public DoubleLinkedListNode(T data)
            : this(data, null, null) { }

        public DoubleLinkedListNode( T data, DoubleLinkedListNode<T> next, DoubleLinkedListNode<T> prev )
        {
            this.Data = data;
            this.Next = next;
            this.Prev = prev;

            if (next != null)
            {
                next.Prev = this;
            }
            if (prev != null)
            {
                prev.Next = this;
            }
        }
    }
}
