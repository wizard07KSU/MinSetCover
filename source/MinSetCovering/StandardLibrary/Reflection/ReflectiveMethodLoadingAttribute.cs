using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cannon.Utilities.Standard.Reflection
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ReflectiveMethodLoadingAttribute : Attribute
    {
        #region Properties
        public string Name { get; private set; }
        #endregion

        #region Constructors
        public ReflectiveMethodLoadingAttribute( string name)
        {
            this.Name = name;
        }
        #endregion
    }
}
