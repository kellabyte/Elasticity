using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Elasticity
{
    [DataContract]    
    public enum SchedulerTaskState
    {
        [EnumMember]
        Disabled = 0,

        [EnumMember]
        Active = 1
    }
}
