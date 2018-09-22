using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AITestBackend.Models
{
    public static class ObjectExtensions
    {
        public static bool IsNotNull(this object @object)
        {
            return IsNotNull<object>(@object);
        }

        public static bool IsNotNull<TObject>(this object @object)
        {
            return !Object.ReferenceEquals(@object, null);
        }

        public static bool IsNull(this object @object)
        {
            return IsNull<object>(@object);
        }

        public static bool IsNull<TObject>(this object @object)
        {
            return Object.ReferenceEquals(@object, null);
        }

    }
}
