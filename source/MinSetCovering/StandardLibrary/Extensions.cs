using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cannon.Utilities.Standard
{
    public static class Extensions
    {
        public static IReadOnlyCollection<T> AsReadOnly<T>( this IEnumerable<T> target )
        {
            return target.ToList().AsReadOnly();
        }

        public static int BinarySearch<T>( this T[] target, T data )
            where T:IComparable
        {
            int min = 0;
            int max = target.Length - 1;
            int mid = -1;
            while ( min <= max )
            {
                mid = (min + max) / 2;
                T current = target[mid];
                int compare = current.CompareTo( data );
                // compare <  0 => compare <  data
                // compare == 0 => compare == data
                // compare >  0 => compare >  data
                if ( compare > 0 )
                {
                    max = mid - 1;
                }
                else if (compare < 0)
                {
                    min = mid + 1;
                }
                else
                {
                    return mid;
                }
            }

            return -( mid + 1 );
        }
    }
}
