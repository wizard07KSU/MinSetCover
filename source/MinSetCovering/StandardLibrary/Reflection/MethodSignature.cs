using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cannon.Utilities.Standard.Reflection
{
    public class MethodSignature
    {
        #region Properties
        #region Argument Types
        private readonly IEnumerable<Type> argumentTypes;

        public IReadOnlyCollection<Type> ArgumentTypes
        {
            get
            {
                return argumentTypes.AsReadOnly();
            }
        }
        #endregion

        /// <summary>
        /// Gets the return type for this signature.
        /// </summary>
        public Type ReturnType { get; private set; }
        #endregion

        #region Constructors
        public MethodSignature( IEnumerable<Type> arguments, Type returnType )
        {
            this.argumentTypes = arguments;
            this.ReturnType = returnType;
        }
        #endregion

        #region Equality Operators
        private static bool Compare( Type leftReturnType, IEnumerable<Type> leftArgTypes, Type rightReturnType, IEnumerable<Type> rightArgTypes )
        {
            if ( leftReturnType != rightReturnType )
            {
                return false;
            }

            if (object.ReferenceEquals( leftArgTypes, rightArgTypes))
            {
                // handles NULL == NULL, and if these are actually the same collection.
                return true;
            }

            int leftArgCount = leftArgTypes.Count();
            int rightArgCount = rightArgTypes.Count();
            if (leftArgCount != rightArgCount)
            {
                return false;
            }

            IEnumerator<Type> rightEnumerator = rightArgTypes.GetEnumerator();
            foreach ( Type param in leftArgTypes)
            {
                rightEnumerator.MoveNext();
                Type paramType = rightEnumerator.Current;
                if ( param != paramType )
                {
                    return false;
                }
            }

            return true;
        }
        #region Compare To Method
        public static bool operator==(MethodSignature signature, MethodInfo method)
        {
            if ( object.ReferenceEquals( signature, null ) ^ object.ReferenceEquals( method, null ) )
            {
                return false;
            }

            // check for null
            if (object.ReferenceEquals(signature, method))
            {
                // NULL == NULL
                return true;
            }

            return Compare( 
                signature.ReturnType, 
                signature.argumentTypes, 
                method.ReturnType, 
                method.GetParameters().Select( pi => pi.ParameterType ) );
        }

        public static bool operator !=(MethodSignature signature, MethodInfo method)
        {
            if ( object.ReferenceEquals( signature, null ) ^ object.ReferenceEquals( method, null ) )
            {
                return true;
            }

            // check for null
            if ( object.ReferenceEquals( signature, method ) )
            {
                // NULL == NULL
                return false;
            }

            return !Compare(
                signature.ReturnType,
                signature.argumentTypes,
                method.ReturnType,
                method.GetParameters().Select( pi => pi.ParameterType ) );
        }
        #endregion

        #region Compare With Signature
        public static bool operator==(MethodSignature left, MethodSignature right)
        {
            if ( object.ReferenceEquals( left, null ) ^ object.ReferenceEquals( right, null ) )
            {
                return false;
            }

            // check for null
            if ( object.ReferenceEquals( left, right ) )
            {
                // NULL == NULL
                return true;
            }

            return Compare(
                left.ReturnType,
                left.argumentTypes,
                right.ReturnType,
                right.argumentTypes );
        }

        public static bool operator!=(MethodSignature left, MethodSignature right)
        {
            if ( object.ReferenceEquals( left, null ) ^ object.ReferenceEquals( right, null ) )
            {
                return true;
            }

            // check for null
            if ( object.ReferenceEquals( left, right ) )
            {
                // NULL == NULL
                return false;
            }

            return !Compare(
                left.ReturnType,
                left.argumentTypes,
                right.ReturnType,
                right.argumentTypes );
        }
        #endregion

        public override bool Equals( object obj )
        {
            if (object.ReferenceEquals( obj, null))
            {
                return false;
            }

            if (obj is MethodInfo)
            {
                return this == (MethodInfo)obj;
            }
            else if (obj is MethodSignature)
            {
                return this == (MethodSignature)obj;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return this.argumentTypes.GetHashCode()
                    + 17 * this.ReturnType.GetHashCode();
            }
        }
        #endregion
    }
}
