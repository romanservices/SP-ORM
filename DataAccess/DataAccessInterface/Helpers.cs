using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccessInterface
{
    public static class NullHelper
    {
        /// <summary>
        /// Determines whether [is null or default] [the specified value].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool IsNullOrDefault<T>(T value)
        {
            return object.Equals(value, default(T));
        }
    }
}
