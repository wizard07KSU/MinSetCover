using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cannon.Utilities.Standard;

namespace MinSetCoveringUtilities
{
    /// <summary>
    /// Special implementation of a priority queue for a minimal set covering
    /// solution.
    /// </summary>
    public sealed class MscPriorityQueue<TSortKey, TLookupKey, TData>
    {
        #region Properties
        private IDictionary<TSortKey  , int> SortKeyLookup = new Dictionary<TSortKey  , int>();
        private IDictionary<TLookupKey, int> LookupTable   = new Dictionary<TLookupKey, int>();

        int initCapicity;
        int currentCapicity;
        private TData[] Data;
        private int insertIndex = 0;
        private int removeIndex = 0;
        private int size = 0;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new doubled-keyed priority queue with the provided capitity.
        /// Should be used when the caller knows the approximate size of the 
        /// queue.
        /// </summary>
        /// <param name="capicity">
        /// The initial size of the queue.
        /// </param>
        public MscPriorityQueue(int capicity)
        {
            initCapicity = capicity;
            currentCapicity = capicity;
            Data = new TData[ capicity ];
        }

        /// <summary>
        /// Creates a new double-keyed priority queue with the default capicity.
        /// </summary>
        public MscPriorityQueue()
            : this( 10 ) { }
        #endregion

        #region Methods
        public void Add( 
            TSortKey   sortKey, 
            TLookupKey lookupKey, 
            TData      data )
        {
            SortKeyLookup.Add( sortKey, insertIndex );
            LookupTable.Add( lookupKey, insertIndex );
            Data[ insertIndex ] = data;
            size++;

            // need to resize our data structure.
            if ( size >= currentCapicity )
            {
                int newCapicity = currentCapicity * 2;
                TData[] newData = new TData[ newCapicity ];

                int i = 0;
                for ( int j = removeIndex; j < insertIndex && j < currentCapicity; j++, i++ )
                {
                    newData[ i ] = Data[ j ];
                }
                for ( int j = 0; j < insertIndex; j++, i++ )
                {
                    newData[ i ] = Data[ j ];
                }

                Data = newData;
                currentCapicity = newCapicity;
            }
        }

        public TData Dequeue()
        {
            TData data = this.Data[removeIndex++];



            return data;
        }
        #endregion
    }
}
