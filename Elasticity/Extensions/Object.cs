using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticity.Extensions
{
    public static class PrivateReflectionDynamicObjectExtensions
    {
        public static dynamic AsDynamic(this object o)
        {
            return PrivateReflectionDynamicObject.WrapObjectIfNeeded(o);
        }
    }
}
