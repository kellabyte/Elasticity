using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticity
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SchedulerTaskContentAttribute : Attribute
    {
        public SchedulerTaskContentAttribute()
        {
            this.TypeNamespace = "http://tempuri.org/";
        }

        public SchedulerTaskContentAttribute(string typeNamespace)
        {
            this.TypeNamespace = typeNamespace;
        }

        public string TypeNamespace { get; set; }        
    }
}
