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
    }
}
