using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cannon.Utilities.Standard.Reflection
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ReflectiveClassLoadingAttributeAttribute : Attribute
    {
        #region Properties
        /// <summary>
        /// A human-readable name for a type.
        /// </summary>
        public string Name { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Maps the decorated class with the provided name.
        /// </summary>
        /// <param name="name">
        /// The name to map to the class.
        /// </param>
        public ReflectiveClassLoadingAttributeAttribute( string name)
        {
            this.Name = name;
        }
        #endregion
    }
}
